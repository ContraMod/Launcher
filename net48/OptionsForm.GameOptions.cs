using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Contra
{
    /// <summary>
    /// The "Graphics" and "Game Options" pages: the contraZH engine settings, which the retail
    /// options screen knows nothing about. Keys, values and defaults follow the Options.ini handoff
    /// taken from OptionPreferences.cpp and OptionsMenu.cpp.
    ///
    /// The controls are declared and positioned in OptionsForm.Designer.cs / OptionsForm.resx
    /// with the rest of the form, so they show on the WinForms designer canvas and can be
    /// dragged there. This file holds only the behaviour: page swapping, enable/disable rules,
    /// translations, and reading and writing Options.ini.
    ///
    /// The page swap has to happen in code. The form is a fixed 800x600 skinned window and a real
    /// TabControl would paint its own grey page over the background image, so instead the existing
    /// controls are gathered onto one panel at startup, the new ones sit on two more, and flat
    /// transparent buttons switch between them.
    ///
    /// Naming: GraphicsPage holds the retail settings and is the tab labelled DISPLAY. ShadersPage
    /// is the tab labelled GRAPHICS -- named after the game's own string id for that page,
    /// GUI:Shaders, which is also what its code calls it.
    /// </summary>
    public partial class OptionsForm
    {
        private static readonly Color TabActiveColor = Color.White;
        private static readonly Color TabInactiveColor = Color.FromArgb(150, 150, 150);

        // The vertical band the existing controls occupy, from OptionsForm.resx. The graphics
        // content starts at the "Resolution:" / "Camera Height" labels on y=72, so the page has to
        // begin above those rather than below them -- they belong to the graphics page and must
        // hide with it. Accept/Close at y=525 stay on the form, shared.
        //
        // Keep PageTop in step with the page panels' Location in the resx: the controls moved onto
        // the graphics page are shifted up by exactly this much.
        private const int PageTop = 70;
        private const int PageBottom = 515;

        private const string DefaultGridLayout = "QWERTYUIOASDFGHJKL";

        // The in-game menu's three layout presets, in the drop-down's order. The box shows the
        // rows each one puts on the command bar rather than the eighteen-key string.
        private static readonly string[] GridLayouts = { "QWERTYUIOASDFGHJKL", "ASDFGHJKLZXCVBNM,.", "QWERTYUIOZXCVBNM,." };

        // A layout written into Options.ini by hand, kept until a preset is picked -- as the
        // in-game menu does. Null when the file holds one of the presets.
        private string handWrittenGridLayout;

        // The values behind the drop-downs that show something friendlier than the raw number.
        // Each list is in the same order as the box's items.
        private static readonly int[] AnisotropyLevels = { 2, 4, 8, 16 };
        private static readonly int[] AntiAliasingValues = { 0, 2, 4, 8 };
        private static readonly int[] SuperSamplingValues = { 100, 150, 200 };
        private static readonly int[] ShadowMapSizes = { 512, 1024, 2048, 4096 };

        private Panel[] pages;
        private Button[] tabs;

        // The skin's own boxes were drawn for the retail settings, so on the two newer pages they
        // cut straight through the controls. Those pages get a background of their own instead:
        // _bg_options_plain -- the same art with the page boxes taken out, made once from the skin
        // -- with a box drawn round each group of settings. Made on first view and kept, so
        // switching tabs costs the same as it did with the plain image.
        private Image skinBackground;
        private readonly Dictionary<Panel, Image> pageBackgrounds = new Dictionary<Panel, Image>();

        // Where the two columns of boxes sit, inside the skin's frame -- the right one starting
        // just left of the settings in it.
        private const int LeftBoxLeft = 38, LeftBoxRight = 410, RightBoxLeft = 422, RightBoxRight = 762;
        private static readonly Color BoxBorder = Color.FromArgb(80, 53, 23);
        private static readonly Color BoxFill = Color.FromArgb(80, 0, 0, 0);

        private void BuildGameOptionsPage()
        {
            // The controls, their geometry and their parenting all come from the designer now.
            // What is left is the part the designer cannot express: moving the pre-existing
            // graphics controls onto their page so the pages can be swapped.
            //
            // Captured before the loop because reparenting mutates Controls while it runs.
            Control[] existing = new Control[Controls.Count];
            Controls.CopyTo(existing, 0);

            foreach (Control c in existing)
            {
                // The pages themselves and the tab row are not graphics content.
                if (c == GraphicsPage || c == ShadersPage || c == GameOptionsPage
                        || c == GraphicsTabButton || c == ShadersTabButton || c == GameOptionsTabButton)
                {
                    continue;
                }

                if (c.Top < PageTop || c.Top > PageBottom)
                {
                    continue;
                }

                Point where = c.Location;
                Controls.Remove(c);
                GraphicsPage.Controls.Add(c);
                c.Location = new Point(where.X, where.Y - PageTop);
            }

            skinBackground = BackgroundImage;
            Disposed += delegate
            {
                foreach (Image generated in pageBackgrounds.Values)
                {
                    generated.Dispose();
                }
            };

            pages = new[] { GraphicsPage, ShadersPage, GameOptionsPage };
            tabs = new[] { GraphicsTabButton, ShadersTabButton, GameOptionsTabButton };

            for (int i = 0; i < pages.Length; i++)
            {
                EnableDoubleBuffering(pages[i]);
                StyleTabButton(tabs[i]);

                Panel page = pages[i];
                tabs[i].Click += delegate { ShowPage(page); };
            }

            ConfigureGameOptionControls();

            // Each group greys out with the switch it depends on, so a setting that the game would
            // ignore does not look like it does something.
            KeyboardOverlayCheckBox.CheckedChanged += delegate { UpdateOverlayEnabledState(); };
            KeyboardOverlayBackdropCheckBox.CheckedChanged += delegate { UpdateOverlayEnabledState(); };
            GridHotkeysCheckBox.CheckedChanged += delegate { UpdateGridEnabledState(); };
            NewRadarCheckBox.CheckedChanged += delegate { UpdateRadarEnabledState(); };
            CameraHeightCheckBox.CheckedChanged += delegate { UpdateCameraHeightState(); };

            EventHandler graphics = delegate { UpdateGraphicsEnabledState(); };
            TextureFilterComboBox.SelectedIndexChanged += graphics;
            AntiAliasingComboBox.SelectedIndexChanged += graphics;
            SuperSamplingComboBox.SelectedIndexChanged += graphics;
            ShadowMapCheckBox.CheckedChanged += graphics;
            BloomCheckBox.CheckedChanged += graphics;
            DynamicLightsCheckBox.CheckedChanged += graphics;

            // The swatch and the channels are two views of one colour, so each updates the other.
            OverlayColourButton.Click += delegate { PickColour(OverlayRedUpDown, OverlayGreenUpDown, OverlayBlueUpDown); };
            BackdropColourButton.Click += delegate { PickColour(BackdropRedUpDown, BackdropGreenUpDown, BackdropBlueUpDown); };

            EventHandler overlaySwatch = delegate { UpdateColourSwatches(); };
            OverlayRedUpDown.ValueChanged += overlaySwatch;
            OverlayGreenUpDown.ValueChanged += overlaySwatch;
            OverlayBlueUpDown.ValueChanged += overlaySwatch;
            BackdropRedUpDown.ValueChanged += overlaySwatch;
            BackdropGreenUpDown.ValueChanged += overlaySwatch;
            BackdropBlueUpDown.ValueChanged += overlaySwatch;

            ApplyGameOptionsLanguage();
            ApplyGameOptionTooltips();
            ShowPage(GraphicsPage);
        }

        /// <summary>
        /// The bits of setup the designer cannot serialise: combo box items, numeric ranges and
        /// the hint colour. Positions and parenting come from the designer.
        /// </summary>
        private void ConfigureGameOptionControls()
        {
            // Game Options. The mode names are the values written to Options.ini.
            HealthBarModeComboBox.Items.AddRange(new object[] { "Classic", "Damaged", "Always" });
            BuildTimerComboBox.Items.AddRange(new object[] { "None", "Seconds", "Auto" });
            AlliedDecalComboBox.Items.AddRange(new object[] { "Hidden", "House", "Army" });
            CastModeComboBox.Items.AddRange(new object[] { "Normal", "QuickCast", "QuickCastWithIndicator" });
            ActionLinesComboBox.Items.AddRange(new object[] { "Off", "All", "AttackOnly" });

            GridHotkeyLayoutComboBox.Items.AddRange(new object[] { "QWER / ASDF", "ASDF / ZXCV", "QWER / ZXCV" });
            GridHotkeyLayoutComboBox.SelectedIndexChanged += delegate { handWrittenGridLayout = null; };

            // Graphics.
            TextureFilterComboBox.Items.AddRange(new object[] { "None", "Point", "Bilinear", "Trilinear", "Anisotropic" });
            // The engine rounds down to one of these, so offering anything else would only mislead.
            AnisotropicLevelComboBox.Items.AddRange(new object[] { "2", "4", "8", "16" });
            AntiAliasingComboBox.Items.AddRange(new object[] { "Off", "2x", "4x", "8x" });
            SuperSamplingComboBox.Items.AddRange(new object[] { "Off", "150%", "200%" });
            ShadowMapResolutionComboBox.Items.AddRange(new object[] { "512 x 512", "1024 x 1024", "2048 x 2048", "4096 x 4096" });

            // Stored as 0..1 but edited as a percentage, as the in-game box is.
            BloomStrengthUpDown.Minimum = 0;
            BloomStrengthUpDown.Maximum = 100;

            SetChannelRange(OverlayRedUpDown);
            SetChannelRange(OverlayGreenUpDown);
            SetChannelRange(OverlayBlueUpDown);
            SetChannelRange(BackdropRedUpDown);
            SetChannelRange(BackdropGreenUpDown);
            SetChannelRange(BackdropBlueUpDown);
            // Opacity is the backdrop alpha, so it takes the same 0-255 range as a channel.
            SetChannelRange(BackdropOpacityUpDown);

            OverlayRgbHintLabel.ForeColor = TabInactiveColor;
            BackdropRgbHintLabel.ForeColor = TabInactiveColor;
            ShadersD3D9HintLabel.ForeColor = TabInactiveColor;
        }

        private static void SetChannelRange(NumericUpDown n)
        {
            n.Minimum = 0;
            n.Maximum = 255;
        }

        private const int WM_SETREDRAW = 0x000B;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

        /// <summary>
        /// Stop the control painting at all. Paired with ResumeDrawing in a finally, so a throw
        /// mid-swap cannot leave the window frozen.
        /// </summary>
        private static void SuspendDrawing(Control control)
        {
            if (control.IsHandleCreated)
            {
                SendMessage(control.Handle, WM_SETREDRAW, false, 0);
            }
        }

        private static void ResumeDrawing(Control control)
        {
            if (control.IsHandleCreated)
            {
                SendMessage(control.Handle, WM_SETREDRAW, true, 0);
            }

            // WM_SETREDRAW leaves the window unpainted rather than dirty, so it needs an explicit
            // invalidate to come back -- and the children with it, they were frozen too.
            control.Invalidate(true);
        }

        /// <summary>
        /// Panel.DoubleBuffered is protected and these panels come from the designer, so it has to
        /// go on through the property rather than a subclass.
        /// </summary>
        private static void EnableDoubleBuffering(Control control)
        {
            typeof(Control)
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(control, true, null);
        }

        /// <summary>
        /// Flat, borderless and transparent, so the tabs read as text on the skin rather than as
        /// Windows buttons sitting on top of it. Not expressible in the resx, hence in code.
        /// </summary>
        private static void StyleTabButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = Color.Transparent;
            b.FlatAppearance.MouseDownBackColor = Color.Transparent;
            b.TabStop = false;
            b.Cursor = Cursors.Hand;
        }

        private void ShowPage(Panel wanted)
        {
            // Every control on these pages is BackColor.Transparent over the form's background
            // image, so each one repaints by compositing against its parent. Flipping the panels
            // sends that through as a cascade of individual paints, which is what made the swap
            // feel sluggish. Freeze painting for the whole swap and send one paint at the end.
            SuspendDrawing(this);
            try
            {
                SuspendLayout();

                // Hide first, then show and raise. The panels cover the same rectangle, and
                // BackColor.Transparent on a WinForms panel means "let whatever is behind me show
                // through", not "draw nothing" -- so leaving an old page visible underneath lets
                // its controls paint through the new one.
                foreach (Panel page in pages)
                {
                    if (page != wanted)
                    {
                        page.Visible = false;
                    }
                }

                wanted.Visible = true;
                wanted.BringToFront();

                Image background = wanted == GraphicsPage ? skinBackground : PageBackground(wanted);
                if (BackgroundImage != background)
                {
                    BackgroundImage = background;
                }

                // The tab buttons live on the form, not on a page, so they have to stay above all.
                for (int i = 0; i < tabs.Length; i++)
                {
                    tabs[i].BringToFront();
                    tabs[i].ForeColor = pages[i] == wanted ? TabActiveColor : TabInactiveColor;
                }

                ResumeLayout(false);
            }
            finally
            {
                ResumeDrawing(this);
            }
        }

        private Image PageBackground(Panel page)
        {
            Image background;
            if (!pageBackgrounds.TryGetValue(page, out background))
            {
                background = MakePageBackground(page);
                pageBackgrounds[page] = background;
            }

            return background;
        }

        /// <summary>
        /// The box-free copy of the skin with a box drawn round every group on the page. The art
        /// is untouched, so it stays as sharp as on the DISPLAY tab.
        /// </summary>
        private Image MakePageBackground(Panel page)
        {
            Bitmap result = new Bitmap(skinBackground.Width, skinBackground.Height);
            using (Image plain = Properties.Resources._bg_options_plain)
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(plain, 0, 0, result.Width, result.Height);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                foreach (Rectangle box in GroupBoxes(page))
                {
                    DrawBox(g, box);
                }
            }

            return result;
        }

        /// <summary>
        /// One box per group, in form coordinates. A group is a bold header label and the controls
        /// stacked directly beneath it in the same column; a gap of more than a couple of rows ends
        /// it, which leaves a lone note such as the Direct3D 9 hint outside every box. Read from
        /// the controls themselves, so the boxes follow when the layout changes in the designer.
        /// </summary>
        private static List<Rectangle> GroupBoxes(Panel page)
        {
            const int split = (LeftBoxRight + RightBoxLeft) / 2;
            const int maxGap = 24;

            List<Control> headers = new List<Control>();
            List<Control> rest = new List<Control>();
            foreach (Control c in page.Controls)
            {
                (c is Label && c.Font.Bold ? headers : rest).Add(c);
            }

            rest.Sort((a, b) => a.Top.CompareTo(b.Top));

            List<Rectangle> boxes = new List<Rectangle>();
            foreach (Control header in headers)
            {
                bool left = header.Left < split;
                int bottom = header.Bottom;

                foreach (Control c in rest)
                {
                    if ((c.Left < split) != left || c.Top < header.Top)
                    {
                        continue;
                    }

                    if (c.Top - bottom > maxGap)
                    {
                        break;
                    }

                    bottom = Math.Max(bottom, c.Bottom);
                }

                // A header further down the same column starts its own group.
                foreach (Control other in headers)
                {
                    if (other != header && (other.Left < split) == left && other.Top > header.Top)
                    {
                        bottom = Math.Min(bottom, other.Top - 12);
                    }
                }

                int top = page.Top + header.Top - 6;
                int x = left ? LeftBoxLeft : RightBoxLeft;
                int right = left ? LeftBoxRight : RightBoxRight;
                boxes.Add(new Rectangle(x, top, right - x, page.Top + bottom + 5 - top));
            }

            return boxes;
        }

        /// <summary>The skin's box: a thin muted-gold rounded border over a dark wash.</summary>
        private static void DrawBox(Graphics g, Rectangle box)
        {
            const int r = 8;
            using (GraphicsPath path = new GraphicsPath())
            using (SolidBrush fill = new SolidBrush(BoxFill))
            using (Pen border = new Pen(BoxBorder))
            {
                path.AddArc(box.Left, box.Top, r * 2, r * 2, 180, 90);
                path.AddArc(box.Right - r * 2, box.Top, r * 2, r * 2, 270, 90);
                path.AddArc(box.Right - r * 2, box.Bottom - r * 2, r * 2, r * 2, 0, 90);
                path.AddArc(box.Left, box.Bottom - r * 2, r * 2, r * 2, 90, 90);
                path.CloseFigure();

                g.FillPath(fill, path);
                g.DrawPath(border, path);
            }
        }

        private void UpdateOverlayEnabledState()
        {
            bool on = KeyboardOverlayCheckBox.Checked;
            KeyboardOverlayBackdropCheckBox.Enabled = on;
            OverlayRedUpDown.Enabled = on;
            OverlayGreenUpDown.Enabled = on;
            OverlayBlueUpDown.Enabled = on;
            OverlayColourButton.Enabled = on;

            // The backdrop colours only do anything when there is a backdrop to paint.
            bool backdrop = on && KeyboardOverlayBackdropCheckBox.Checked;
            BackdropRedUpDown.Enabled = backdrop;
            BackdropGreenUpDown.Enabled = backdrop;
            BackdropBlueUpDown.Enabled = backdrop;
            BackdropOpacityUpDown.Enabled = backdrop;
            BackdropColourButton.Enabled = backdrop;
        }

        private void UpdateGridEnabledState()
        {
            bool on = GridHotkeysCheckBox.Checked;
            GridHotkeyLayoutComboBox.Enabled = on;
            GridHotkeyUnitOrdersCheckBox.Enabled = on;
        }

        /// <summary>
        /// MaxCameraHeight is only read while UseCustomMaxCameraHeight is on; otherwise the game
        /// uses the mod's own limit, which the launcher cannot see -- so the slider greys out and
        /// the caption drops its number.
        /// </summary>
        private void UpdateCameraHeightState()
        {
            bool custom = CameraHeightCheckBox.Checked;
            string caption = Messages.GenerateMessage("CameraHeightString", Globals.currentLanguage);

            CameraHeightTrackBar.Enabled = custom;
            CameraHeightCheckBox.Text = custom
                ? caption + CameraHeightTrackBar.Value
                : caption.TrimEnd(' ', ':');
        }

        /// <summary>BlipSize does nothing unless the simplified radar is on.</summary>
        private void UpdateRadarEnabledState()
        {
            BlipSizeCheckBox.Enabled = NewRadarCheckBox.Checked;
        }

        /// <summary>
        /// The Graphics page's dependencies, as the game applies them. The file accepts any
        /// combination; these only grey out what the game would ignore.
        /// </summary>
        private void UpdateGraphicsEnabledState()
        {
            // The sample count is only read when the filter is Anisotropic.
            AnisotropicLevelComboBox.Enabled = string.Equals(
                SelectedText(TextureFilterComboBox, "Bilinear"), "Anisotropic", StringComparison.OrdinalIgnoreCase);

            int superSampling = SelectedValue(SuperSamplingComboBox, SuperSamplingValues, 100);
            int antiAliasing = SelectedValue(AntiAliasingComboBox, AntiAliasingValues, 0);

            // Supersampling takes the place of MSAA; the in-game menu greys Anti-aliasing out too.
            AntiAliasingComboBox.Enabled = superSampling == 100;

            // Ambient occlusion is forced off under MSAA unless supersampling is doing the work.
            AmbientOcclusionCheckBox.Enabled = !(antiAliasing > 0 && superSampling == 100);

            ShadowMapResolutionComboBox.Enabled = ShadowMapCheckBox.Checked;
            BloomStrengthUpDown.Enabled = BloomCheckBox.Checked;
            PixelLightsCheckBox.Enabled = DynamicLightsCheckBox.Checked;
        }

        /// <summary>
        /// Open the standard colour picker on the colour the three channels currently describe, and
        /// write the choice back into them. Typing three numbers to guess at a colour is the part
        /// of this page that most wanted a real control.
        /// </summary>
        private void PickColour(NumericUpDown red, NumericUpDown green, NumericUpDown blue)
        {
            using (ColorDialog picker = new ColorDialog())
            {
                picker.Color = ChannelColour(red, green, blue);
                // The engine takes any 0-255 triple, so let the user mix one rather than limiting
                // them to the basic swatches.
                picker.FullOpen = true;
                picker.AnyColor = true;

                if (picker.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                red.Value = picker.Color.R;
                green.Value = picker.Color.G;
                blue.Value = picker.Color.B;
            }

            UpdateColourSwatches();
        }

        private static Color ChannelColour(NumericUpDown red, NumericUpDown green, NumericUpDown blue)
        {
            return Color.FromArgb((int)red.Value, (int)green.Value, (int)blue.Value);
        }

        /// <summary>
        /// Paint each swatch with the colour its channels describe, and pick a readable caption
        /// colour for it -- the text sits on the swatch, so a dark colour needs a light label.
        /// </summary>
        private void UpdateColourSwatches()
        {
            PaintSwatch(OverlayColourButton, ChannelColour(OverlayRedUpDown, OverlayGreenUpDown, OverlayBlueUpDown));
            PaintSwatch(BackdropColourButton, ChannelColour(BackdropRedUpDown, BackdropGreenUpDown, BackdropBlueUpDown));
        }

        private static void PaintSwatch(Button swatch, Color colour)
        {
            swatch.BackColor = colour;
            swatch.FlatAppearance.BorderColor = Color.FromArgb(96, 96, 96);

            // Rec. 601 luma, the usual cheap test for whether a colour reads as light or dark.
            int luma = (299 * colour.R + 587 * colour.G + 114 * colour.B) / 1000;
            swatch.ForeColor = luma < 128 ? Color.White : Color.Black;
        }

        /// <summary>
        /// Read the engine settings out of Options.ini. Every fallback here matches the engine's own
        /// default, so an absent key shows what the game will actually do.
        ///
        /// Booleans come in two kinds. The legacy keys (GetBool) are on only for "yes"; the newer
        /// ones (GetFlag) also take true, 1 and the rest. Reading a newer key the legacy way would
        /// turn a hand written "true" off on the next save.
        /// </summary>
        private void LoadGameOptions()
        {
            string s = GameOptionsIni.ReadAll();

            // Display
            HealthBarModeComboBox.SelectedItem =
                MatchMode(HealthBarModeComboBox, GameOptionsIni.GetValue(s, "HealthBarDisplayMode", "Classic"), "Classic", true);
            BuildTimerComboBox.SelectedItem =
                MatchMode(BuildTimerComboBox, GameOptionsIni.GetValue(s, "BuildTimerDisplayMode", "None"), "None", true);
            AlliedDecalComboBox.SelectedItem =
                MatchMode(AlliedDecalComboBox, GameOptionsIni.GetValue(s, "AlliedDecalMode", "House"), "House", true);
            NumericalHealthCheckBox.Checked = GameOptionsIni.GetBool(s, "NumericalHealth", false);
            SmartPipsCheckBox.Checked = GameOptionsIni.GetBool(s, "SmartPips", false);
            SelectionCircleCheckBox.Checked = GameOptionsIni.GetBool(s, "SelectionCircle", false);
            ObjectDecalsCheckBox.Checked = GameOptionsIni.GetFlag(s, "ObjectDecals", true);
            DefensesRangeCircleCheckBox.Checked = GameOptionsIni.GetFlag(s, "DefensesRangeCircle", false);
            SmoothUnitMotionCheckBox.Checked = GameOptionsIni.GetFlag(s, "SmoothUnitMotion", true);

            // Input
            CastModeComboBox.SelectedItem =
                MatchMode(CastModeComboBox, GameOptionsIni.GetValue(s, "CastMode", "Normal"), "Normal", true);
            ActionLinesComboBox.SelectedItem =
                MatchMode(ActionLinesComboBox, GameOptionsIni.GetValue(s, "ActionLines", "Off"), "Off", true);
            EasyMilitaryDragCheckBox.Checked = GameOptionsIni.GetBool(s, "EasyMilitaryDrag", false);
            SmartSelectionCheckBox.Checked = GameOptionsIni.GetFlag(s, "SmartSelection", true);
            SmartSelectionUseMouseCheckBox.Checked = GameOptionsIni.GetFlag(s, "SmartSelectionUseMouse", true);
            SmartCommandGroupCheckBox.Checked = GameOptionsIni.GetFlag(s, "SmartCommandGroup", true);
            LowLatencyCheckBox.Checked = GameOptionsIni.GetFlag(s, "LowLatency", false);

            // Grid hotkeys. A layout that is not one of the presets shows as the first one, as in
            // the game, but is written back unchanged unless a preset is chosen.
            GridHotkeysCheckBox.Checked = GameOptionsIni.GetBool(s, "GridHotkeys", false);
            string layout = GameOptionsIni.GetValue(s, "GridHotkeyLayout", DefaultGridLayout);
            int preset = Array.FindIndex(GridLayouts, l => string.Equals(l, layout, StringComparison.OrdinalIgnoreCase));
            GridHotkeyLayoutComboBox.SelectedIndex = Math.Max(preset, 0);
            handWrittenGridLayout = preset < 0 ? layout : null;
            GridHotkeyUnitOrdersCheckBox.Checked = GameOptionsIni.GetFlag(s, "GridHotkeyUnitOrders", true);

            // Hotkey letters
            KeyboardOverlayCheckBox.Checked = GameOptionsIni.GetBool(s, "KeyboardOverlay", false);
            // On by default in the engine: it is what makes the letter readable.
            KeyboardOverlayBackdropCheckBox.Checked =
                GameOptionsIni.GetBool(s, "KeyboardOverlayBackdrop", true);
            OverlayRedUpDown.Value = Clamp(GameOptionsIni.GetInt(s, "KeyboardOverlayRed", 255), OverlayRedUpDown);
            OverlayGreenUpDown.Value = Clamp(GameOptionsIni.GetInt(s, "KeyboardOverlayGreen", 255), OverlayGreenUpDown);
            OverlayBlueUpDown.Value = Clamp(GameOptionsIni.GetInt(s, "KeyboardOverlayBlue", 255), OverlayBlueUpDown);
            // Black at half alpha in the engine, which is what makes a light letter readable.
            BackdropRedUpDown.Value = Clamp(GameOptionsIni.GetInt(s, "KeyboardOverlayBackdropRed", 0), BackdropRedUpDown);
            BackdropGreenUpDown.Value = Clamp(GameOptionsIni.GetInt(s, "KeyboardOverlayBackdropGreen", 0), BackdropGreenUpDown);
            BackdropBlueUpDown.Value = Clamp(GameOptionsIni.GetInt(s, "KeyboardOverlayBackdropBlue", 0), BackdropBlueUpDown);
            BackdropOpacityUpDown.Value = Clamp(GameOptionsIni.GetInt(s, "KeyboardOverlayBackdropOpacity", 128), BackdropOpacityUpDown);

            // Camera height. The game clamps MaxCameraHeight into the slider's 210..1000, and a
            // missing or zero value means the mod's own limit; the slider then starts from 350,
            // a value only written once the custom height is switched on.
            CameraHeightCheckBox.Checked = GameOptionsIni.GetFlag(s, "UseCustomMaxCameraHeight", false);
            int cameraHeight = GameOptionsIni.GetInt(s, "MaxCameraHeight", 0);
            CameraHeightTrackBar.Value = cameraHeight > 0
                ? Math.Max(CameraHeightTrackBar.Minimum, Math.Min(CameraHeightTrackBar.Maximum, cameraHeight))
                : 350;
            UpdateCameraHeightState();

            // Radar. BlipSize is a named mode, Small or Large, with its index also accepted.
            NewRadarCheckBox.Checked = GameOptionsIni.GetBool(s, "NewRadar", false);
            string blips = GameOptionsIni.GetValue(s, "BlipSize", "Large");
            BlipSizeCheckBox.Checked = !(string.Equals(blips, "Small", StringComparison.OrdinalIgnoreCase) || blips == "0");

            // Graphics. TextureFilter takes names only, and unknown text is None to the engine --
            // so a typo shows as None here too rather than as the default it is not getting.
            TextureFilterComboBox.SelectedItem =
                MatchMode(TextureFilterComboBox, GameOptionsIni.GetValue(s, "TextureFilter", "Bilinear"), "None", false);
            // Clamped to 2..16 and rounded down to a power of two by the game, as snapping down does.
            AnisotropicLevelComboBox.SelectedIndex =
                SnapDown(GameOptionsIni.GetInt(s, "AnisotropyLevel", 2), AnisotropyLevels);
            // Anything else is clamped and rounded down by the game, which snapping down mirrors.
            AntiAliasingComboBox.SelectedIndex =
                SnapDown(GameOptionsIni.GetInt(s, "AntiAliasing", 0), AntiAliasingValues);
            SuperSamplingComboBox.SelectedIndex =
                SnapDown(GameOptionsIni.GetInt(s, "SuperSampling", 100), SuperSamplingValues);
            AmbientOcclusionCheckBox.Checked = GameOptionsIni.GetFlag(s, "AmbientOcclusion", true);
            SoftParticlesCheckBox.Checked = GameOptionsIni.GetFlag(s, "SoftParticles", true);
            ShadowMapCheckBox.Checked = GameOptionsIni.GetFlag(s, "ShadowMap", true);
            // Unlike the others, an odd shadow map size snaps up to the next one.
            ShadowMapResolutionComboBox.SelectedIndex =
                SnapUp(GameOptionsIni.GetInt(s, "ShadowMapResolution", 4096), ShadowMapSizes);
            BloomCheckBox.Checked = GameOptionsIni.GetFlag(s, "Bloom", false);
            BloomStrengthUpDown.Value = ReadBloomPercent(GameOptionsIni.GetValue(s, "BloomStrength", null));
            LaserRefCheckBox.Checked = GameOptionsIni.GetFlag(s, "LaserRef", false);

            SpecularCheckBox.Checked = GameOptionsIni.GetFlag(s, "Specular", true);
            NormalMapsCheckBox.Checked = GameOptionsIni.GetFlag(s, "NormalMaps", true);
            HeightBlendCheckBox.Checked = GameOptionsIni.GetFlag(s, "HeightBlend", true);
            DynamicLightsCheckBox.Checked = GameOptionsIni.GetFlag(s, "DynamicLights", true);
            PixelLightsCheckBox.Checked = GameOptionsIni.GetFlag(s, "PixelLights", true);
            HQSkyCheckBox.Checked = GameOptionsIni.GetFlag(s, "HQSky", true);
            WaterReflectionsCheckBox.Checked = GameOptionsIni.GetFlag(s, "WaterReflections", true);
            ShorelineFoamCheckBox.Checked = GameOptionsIni.GetFlag(s, "ShorelineFoam", true);
            FlameShadersCheckBox.Checked = GameOptionsIni.GetFlag(s, "FlameShaders", true);
            ElectricShadersCheckBox.Checked = GameOptionsIni.GetFlag(s, "ElectricShaders", true);
            LaserShadersCheckBox.Checked = GameOptionsIni.GetFlag(s, "LaserShaders", true);
            CryoShadersCheckBox.Checked = GameOptionsIni.GetFlag(s, "CryoShaders", true);

            UpdateOverlayEnabledState();
            UpdateGridEnabledState();
            UpdateRadarEnabledState();
            UpdateGraphicsEnabledState();
            UpdateColourSwatches();
        }

        private static decimal Clamp(int value, NumericUpDown control)
        {
            if (value < control.Minimum)
            {
                return control.Minimum;
            }

            if (value > control.Maximum)
            {
                return control.Maximum;
            }

            return value;
        }

        /// <summary>
        /// BloomStrength is stored as 0.00..1.00 and edited as a percentage, rounded as the
        /// in-game box rounds it. Parsed with the invariant culture: a German or Russian Windows
        /// would otherwise read "0.50" as fifty.
        /// </summary>
        private static decimal ReadBloomPercent(string stored)
        {
            double strength;
            if (stored == null || !double.TryParse(stored, NumberStyles.Float, CultureInfo.InvariantCulture, out strength))
            {
                strength = 0.5;
            }

            strength = Math.Max(0.0, Math.Min(1.0, strength));
            return (int)(strength * 100.0 + 0.5);
        }

        /// <summary>The index of the largest value not above the one given, or 0.</summary>
        private static int SnapDown(int value, int[] values)
        {
            int index = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] <= value)
                {
                    index = i;
                }
            }

            return index;
        }

        /// <summary>The index of the smallest value not below the one given, or the last.</summary>
        private static int SnapUp(int value, int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] >= value)
                {
                    return i;
                }
            }

            return values.Length - 1;
        }

        private static int SelectedValue(ComboBox box, int[] values, int fallback)
        {
            return box.SelectedIndex >= 0 && box.SelectedIndex < values.Length ? values[box.SelectedIndex] : fallback;
        }

        /// <summary>
        /// The item a stored mode names. Named modes take the name, case-insensitively, or -- when
        /// acceptIndex is set -- their 0-based index, as the engine reads them. Anything else is
        /// whatever the engine falls back to, which is not always the first item: None is a real
        /// texture filter and sits first in its list.
        /// </summary>
        private static object MatchMode(ComboBox box, string value, string fallback, bool acceptIndex)
        {
            // FindStringExact ignores case, as the game does for these names.
            int found = box.FindStringExact(value);

            int index;
            if (found < 0 && acceptIndex && int.TryParse(value, out index) && index >= 0 && index < box.Items.Count)
            {
                found = index;
            }

            if (found < 0)
            {
                found = box.FindStringExact(fallback);
            }

            return found >= 0 ? box.Items[found] : null;
        }

        /// <summary>
        /// Write the engine settings back. One read and one write for both pages, rather than a
        /// read/write pair per setting as the retail options do. Keys the launcher does not show --
        /// the debug views among them -- are left exactly as they are.
        /// </summary>
        private void SaveGameOptions()
        {
            if (!GameOptionsIni.Exists())
            {
                return;
            }

            string s = GameOptionsIni.ReadAll();

            // Display
            s = GameOptionsIni.SetValue(s, "HealthBarDisplayMode", SelectedText(HealthBarModeComboBox, "Classic"));
            s = GameOptionsIni.SetValue(s, "BuildTimerDisplayMode", SelectedText(BuildTimerComboBox, "None"));
            s = GameOptionsIni.SetValue(s, "AlliedDecalMode", SelectedText(AlliedDecalComboBox, "House"));
            s = GameOptionsIni.SetBool(s, "NumericalHealth", NumericalHealthCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "SmartPips", SmartPipsCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "SelectionCircle", SelectionCircleCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "ObjectDecals", ObjectDecalsCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "DefensesRangeCircle", DefensesRangeCircleCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "SmoothUnitMotion", SmoothUnitMotionCheckBox.Checked);

            // Input
            s = GameOptionsIni.SetValue(s, "CastMode", SelectedText(CastModeComboBox, "Normal"));
            s = GameOptionsIni.SetValue(s, "ActionLines", SelectedText(ActionLinesComboBox, "Off"));
            s = GameOptionsIni.SetBool(s, "EasyMilitaryDrag", EasyMilitaryDragCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "SmartSelection", SmartSelectionCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "SmartSelectionUseMouse", SmartSelectionUseMouseCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "SmartCommandGroup", SmartCommandGroupCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "LowLatency", LowLatencyCheckBox.Checked);

            // Grid hotkeys
            s = GameOptionsIni.SetBool(s, "GridHotkeys", GridHotkeysCheckBox.Checked);
            s = GameOptionsIni.SetValue(s, "GridHotkeyLayout",
                handWrittenGridLayout ?? GridLayouts[Math.Max(GridHotkeyLayoutComboBox.SelectedIndex, 0)]);
            s = GameOptionsIni.SetBool(s, "GridHotkeyUnitOrders", GridHotkeyUnitOrdersCheckBox.Checked);

            // Hotkey letters
            s = GameOptionsIni.SetBool(s, "KeyboardOverlay", KeyboardOverlayCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "KeyboardOverlayBackdrop", KeyboardOverlayBackdropCheckBox.Checked);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayRed", (int)OverlayRedUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayGreen", (int)OverlayGreenUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayBlue", (int)OverlayBlueUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayBackdropRed", (int)BackdropRedUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayBackdropGreen", (int)BackdropGreenUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayBackdropBlue", (int)BackdropBlueUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayBackdropOpacity", (int)BackdropOpacityUpDown.Value);

            // Camera height. Switched off, MaxCameraHeight is left as it is; the game ignores it.
            s = GameOptionsIni.SetBool(s, "UseCustomMaxCameraHeight", CameraHeightCheckBox.Checked);
            if (CameraHeightCheckBox.Checked)
            {
                s = GameOptionsIni.SetInt(s, "MaxCameraHeight", CameraHeightTrackBar.Value);
            }

            // Radar
            s = GameOptionsIni.SetBool(s, "NewRadar", NewRadarCheckBox.Checked);
            s = GameOptionsIni.SetValue(s, "BlipSize", BlipSizeCheckBox.Checked ? "Large" : "Small");

            // Graphics
            int superSampling = SelectedValue(SuperSamplingComboBox, SuperSamplingValues, 100);
            s = GameOptionsIni.SetValue(s, "TextureFilter", SelectedText(TextureFilterComboBox, "Bilinear"));
            s = GameOptionsIni.SetValue(s, "AnisotropyLevel", SelectedText(AnisotropicLevelComboBox, "2"));
            s = GameOptionsIni.SetInt(s, "SuperSampling", superSampling);
            // MSAA and supersampling are not meant to be set together, and the in-game menu greys
            // Anti-aliasing out while supersampling is on -- so that is when it is written as off.
            s = GameOptionsIni.SetInt(s, "AntiAliasing",
                superSampling > 100 ? 0 : SelectedValue(AntiAliasingComboBox, AntiAliasingValues, 0));
            s = GameOptionsIni.SetBool(s, "AmbientOcclusion", AmbientOcclusionCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "SoftParticles", SoftParticlesCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "ShadowMap", ShadowMapCheckBox.Checked);
            s = GameOptionsIni.SetInt(s, "ShadowMapResolution", SelectedValue(ShadowMapResolutionComboBox, ShadowMapSizes, 4096));
            s = GameOptionsIni.SetBool(s, "Bloom", BloomCheckBox.Checked);
            s = GameOptionsIni.SetValue(s, "BloomStrength",
                ((double)BloomStrengthUpDown.Value / 100.0).ToString("0.00", CultureInfo.InvariantCulture));
            s = GameOptionsIni.SetBool(s, "LaserRef", LaserRefCheckBox.Checked);

            s = GameOptionsIni.SetBool(s, "Specular", SpecularCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "NormalMaps", NormalMapsCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "HeightBlend", HeightBlendCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "DynamicLights", DynamicLightsCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "PixelLights", PixelLightsCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "HQSky", HQSkyCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "WaterReflections", WaterReflectionsCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "ShorelineFoam", ShorelineFoamCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "FlameShaders", FlameShadersCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "ElectricShaders", ElectricShadersCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "LaserShaders", LaserShadersCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "CryoShaders", CryoShadersCheckBox.Checked);

            GameOptionsIni.WriteAll(s);
        }

        private static string SelectedText(ComboBox box, string fallback)
        {
            return box.SelectedItem != null ? box.SelectedItem.ToString() : fallback;
        }

        /// <summary>
        /// One line of help per setting, shown on hover, in English, Russian or German to match
        /// the captions.
        /// </summary>
        private void ApplyGameOptionTooltips()
        {
            // A long tip on a slow popup is easy to miss, so show them quickly and leave them up.
            GameOptionsToolTip.InitialDelay = 350;
            GameOptionsToolTip.ReshowDelay = 100;
            GameOptionsToolTip.AutoPopDelay = 20000;

            // ---- Game Options: Display
            SetTip(Tip("Classic: original look. Damaged: only on damaged units. Always: on every unit. Cycle in game with Ctrl+`.",
                       "Classic: обычный вид. Damaged: только у повреждённых. Always: у всех. В игре переключается через Ctrl+`.",
                       "Classic: normale Ansicht. Damaged: nur bei beschädigten Einheiten. Always: bei allen. Im Spiel mit Strg+` umschaltbar."),
                   HealthBarModeLabel, HealthBarModeComboBox);
            // Auto switches to m:ss once a minute or more is left, and shows plain seconds below
            // that -- see the countdown formatting in W3DPushButton.cpp.
            SetTip(Tip("Show remaining build time. Seconds: always in seconds. Auto: minutes and seconds from 1:00 up.",
                       "Показывать оставшееся время постройки. Seconds: всегда в секундах. Auto: минуты и секунды от 1:00.",
                       "Verbleibende Bauzeit anzeigen. Seconds: immer in Sekunden. Auto: Minuten und Sekunden ab 1:00."),
                   BuildTimerLabel, BuildTimerComboBox);
            SetTip(Tip("Show your allies' special power markers. House: in the ally's player colour. Army: in their faction colour. Hidden: only your own.",
                       "Показывать метки спецспособностей союзников. House: цветом игрока. Army: цветом фракции. Hidden: только свои.",
                       "Spezialfähigkeits-Markierungen der Verbündeten zeigen. House: in der Spielerfarbe. Army: in der Fraktionsfarbe. Hidden: nur die eigenen."),
                   AlliedDecalLabel, AlliedDecalComboBox);
            SetTip(Tip("Show exact hit points beside the health bar.",
                       "Показывать точное число очков здоровья рядом с полосой.",
                       "Genaue Trefferpunkte neben dem Lebensbalken anzeigen."),
                   NumericalHealthCheckBox);
            SetTip(Tip("Never hide ammo and passenger pips.",
                       "Всегда показывать метки боезапаса и пассажиров.",
                       "Munitions- und Passagieranzeigen nie ausblenden."),
                   SmartPipsCheckBox);
            SetTip(Tip("Draw a ring under selected units.",
                       "Рисовать круг под выделенными юнитами.",
                       "Einen Ring unter ausgewählten Einheiten zeichnen."),
                   SelectionCircleCheckBox);
            SetTip(Tip("Draw the ground markers that units and structures ask for.",
                       "Рисовать метки на земле, которые задают юниты и здания.",
                       "Die Bodenmarkierungen zeichnen, die Einheiten und Gebäude vorsehen."),
                   ObjectDecalsCheckBox);
            SetTip(Tip("Ring the attack range of a defensive structure while you place it.",
                       "Показывать радиус атаки оборонительного здания при его размещении.",
                       "Beim Platzieren eines Verteidigungsgebäudes dessen Reichweite anzeigen."),
                   DefensesRangeCircleCheckBox);
            SetTip(Tip("Move units smoothly between game ticks. Single player, skirmish and replays only.",
                       "Плавно двигать юнитов между тактами игры. Только кампания, схватка и записи.",
                       "Einheiten zwischen den Spieltakten flüssig bewegen. Nur Einzelspieler, Gefecht und Wiederholungen."),
                   SmoothUnitMotionCheckBox);

            // ---- Game Options: Input
            SetTip(Tip("QuickCast: fire at the cursor on key press, no second click.",
                       "QuickCast: применять по курсору сразу при нажатии, без второго щелчка.",
                       "QuickCast: auf Tastendruck sofort am Cursor auslösen, ohne zweiten Klick."),
                   CastModeLabel, CastModeComboBox);
            SetTip(Tip("Draw lines from selected units to where they are ordered. AttackOnly: only for attack orders.",
                       "Рисовать линии от выделенных юнитов к цели приказа. AttackOnly: только для атаки.",
                       "Linien von ausgewählten Einheiten zu ihrem Befehlsziel zeichnen. AttackOnly: nur bei Angriffsbefehlen."),
                   ActionLinesLabel, ActionLinesComboBox);
            SetTip(Tip("Drag selection picks combat units only. Click a builder to select it.",
                       "Рамка выделяет только боевые юниты. Рабочего можно выделить щелчком.",
                       "Der Auswahlrahmen erfasst nur Kampfeinheiten. Bauarbeiter per Klick auswählen."),
                   EasyMilitaryDragCheckBox);
            SetTip(Tip("A row of icons above the command bar, one per unit type selected. Click one to narrow the selection; Tab cycles.",
                       "Ряд значков над панелью команд, по одному на тип выделенных юнитов. Щелчок сужает выделение; Tab переключает.",
                       "Eine Symbolreihe über der Befehlsleiste, eine pro ausgewähltem Einheitentyp. Klick grenzt die Auswahl ein; Tab wechselt."),
                   SmartSelectionCheckBox);
            SetTip(Tip("On: double click an icon to keep only those units. Off: Ctrl+Shift+click does it.",
                       "Вкл.: двойной щелчок по значку оставляет только эти юниты. Выкл.: то же через Ctrl+Shift+щелчок.",
                       "An: Doppelklick auf ein Symbol behält nur diese Einheiten. Aus: dasselbe mit Strg+Umschalt+Klick."),
                   SmartSelectionUseMouseCheckBox);
            SetTip(Tip("A row of icons for your control groups, under the smart selection row.",
                       "Ряд значков для групп юнитов под строкой умного выделения.",
                       "Eine Symbolreihe für die Einheitengruppen, unter der Reihe der intelligenten Auswahl."),
                   SmartCommandGroupCheckBox);
            SetTip(Tip("Cut input lag by letting fewer frames queue up. Direct3D 9 build only.",
                       "Уменьшает задержку ввода, сокращая очередь кадров. Только версия Direct3D 9.",
                       "Verringert die Eingabeverzögerung durch weniger gepufferte Bilder. Nur Direct3D-9-Version."),
                   LowLatencyCheckBox);

            // ---- Game Options: Grid hotkeys
            // Ctrl and Alt keep their meta events; Shift deliberately does not, being how the
            // command bar batches. See MetaEvent.cpp's (keyModState & ~SHIFT) test.
            SetTip(Tip("The command bar mirrors your keyboard: each row of slots answers to the matching row of layout keys, "
                     + "by position rather than by what the command is. Replaces per-command mnemonic hotkeys while enabled. "
                     + "Ctrl and Alt combinations keep their normal bindings.",
                       "Панель команд повторяет клавиатуру: каждый ряд ячеек отвечает соответствующему ряду клавиш раскладки, "
                     + "по положению, а не по названию команды. Пока включено, заменяет мнемонические горячие клавиши команд. "
                     + "Сочетания с Ctrl и Alt сохраняют обычные назначения.",
                       "Die Befehlsleiste spiegelt die Tastatur: Jede Reihe von Feldern folgt der entsprechenden Reihe der Belegung, "
                     + "nach Position statt nach Befehl. Ersetzt bei Aktivierung die mnemonischen Befehlstasten. "
                     + "Kombinationen mit Strg und Alt behalten ihre normalen Belegungen."),
                   GridHotkeysCheckBox);
            SetTip(Tip("Which rows of keys the command bar answers to, top row first. A layout written into Options.ini by hand is kept until you pick one here.",
                       "Какие ряды клавиш отвечают панели команд, верхний ряд первым. Раскладка, вписанная в Options.ini вручную, сохраняется, пока вы не выберете вариант здесь.",
                       "Welche Tastenreihen die Befehlsleiste bedienen, obere Reihe zuerst. Eine von Hand in Options.ini eingetragene Belegung bleibt, bis hier eine gewählt wird."),
                   GridHotkeyLayoutLabel, GridHotkeyLayoutComboBox);
            SetTip(Tip("Unit orders such as stop and guard keep their own letter instead of taking a grid key.",
                       "Приказы юнитов, например стоп и охрана, сохраняют свою букву вместо клавиши сетки.",
                       "Einheitenbefehle wie Halt und Bewachen behalten ihren eigenen Buchstaben statt einer Rastertaste."),
                   GridHotkeyUnitOrdersCheckBox);

            // ---- Game Options: Hotkey letters on cameos
            SetTip(Tip("Show each button's hotkey on its icon.",
                       "Показывать горячую клавишу каждой кнопки на её значке.",
                       "Das Tastenkürzel jeder Schaltfläche auf ihrem Symbol anzeigen."),
                   KeyboardOverlayCheckBox);
            SetTip(Tip("Dark panel behind letters for readability.",
                       "Тёмная подложка за буквами для читаемости.",
                       "Dunkle Platte hinter den Buchstaben für bessere Lesbarkeit."),
                   KeyboardOverlayBackdropCheckBox);
            SetTip(Tip("Letter colour (RGB, 0-255).",
                       "Цвет букв (RGB, 0-255).",
                       "Farbe der Buchstaben (RGB, 0-255)."),
                   OverlayColourLabel, OverlayRedUpDown, OverlayGreenUpDown, OverlayBlueUpDown,
                   OverlayRgbHintLabel, OverlayColourButton);
            SetTip(Tip("Panel colour and opacity (RGBA, 0-255).",
                       "Цвет и непрозрачность подложки (RGBA, 0-255).",
                       "Farbe und Deckkraft der Platte (RGBA, 0-255)."),
                   BackdropColourLabel, BackdropRedUpDown, BackdropGreenUpDown, BackdropBlueUpDown,
                   BackdropOpacityUpDown, BackdropRgbHintLabel, BackdropColourButton);

            // ---- Game Options: Radar
            SetTip(Tip("Outline radar blips and the shoreline, at double radar resolution. Takes effect after a game restart.",
                       "Обводить метки и береговую линию на радаре, с удвоенным разрешением. Действует после перезапуска игры.",
                       "Radarpunkte und Küstenlinie umranden, bei doppelter Radarauflösung. Wirkt nach einem Neustart des Spiels."),
                   NewRadarCheckBox);
            SetTip(Tip("Bigger blips on the simplified radar: easier to read, but they run together sooner in a crowd. Takes effect after a game restart.",
                       "Крупные метки на упрощённом радаре: легче читать, но в толпе быстрее сливаются. Действует после перезапуска игры.",
                       "Größere Punkte auf dem vereinfachten Radar: besser lesbar, verschwimmen aber schneller in Gruppen. Wirkt nach einem Neustart des Spiels."),
                   BlipSizeCheckBox);

            // ---- Graphics: image quality
            SetTip(Tip("Texture smoothing at distance. Anisotropic = best quality, negligible cost.",
                       "Сглаживание текстур на расстоянии. Anisotropic — лучшее качество почти без затрат.",
                       "Texturglättung in der Ferne. Anisotropic = beste Qualität, kaum Kosten."),
                   TextureFilterLabel, TextureFilterComboBox);
            SetTip(Tip("Higher = sharper ground textures at flat angles. 16 is safe on modern GPUs.",
                       "Больше — чётче текстуры земли под острым углом. 16 безопасно на современных видеокартах.",
                       "Höher = schärfere Bodentexturen bei flachen Winkeln. 16 ist auf modernen GPUs unbedenklich."),
                   AnisotropicLevelLabel, AnisotropicLevelComboBox);
            SetTip(Tip("Multisample anti-aliasing. Turns ambient occlusion off. Saved as Off while supersampling is on.",
                       "Сглаживание MSAA. Отключает фоновое затенение. При включённом суперсэмплинге сохраняется как Off.",
                       "Multisample-Kantenglättung. Schaltet die Umgebungsverdeckung ab. Wird als Off gespeichert, solange Supersampling an ist."),
                   AntiAliasingLabel, AntiAliasingComboBox);
            SetTip(Tip("Render at a higher resolution and scale down; replaces anti-aliasing and keeps ambient occlusion. 200% draws four times the pixels.",
                       "Рендер в повышенном разрешении с уменьшением; заменяет сглаживание и сохраняет затенение. 200% — вчетверо больше пикселей.",
                       "Mit höherer Auflösung rendern und verkleinern; ersetzt die Kantenglättung und behält die Verdeckung. 200 % zeichnet viermal so viele Pixel."),
                   SuperSamplingLabel, SuperSamplingComboBox);
            SetTip(Tip("Soft shadowing in corners and creases. Off in game while anti-aliasing is on, unless supersampling is.",
                       "Мягкое затенение в углах и складках. В игре отключено при сглаживании, если не включён суперсэмплинг.",
                       "Weiche Schatten in Ecken und Falten. Im Spiel aus, solange Kantenglättung an ist, außer bei Supersampling."),
                   AmbientOcclusionCheckBox);
            SetTip(Tip("Particles fade where they meet the ground instead of cutting into it.",
                       "Частицы плавно исчезают у земли, а не обрезаются о неё.",
                       "Partikel blenden am Boden weich aus, statt hart abzuschneiden."),
                   SoftParticlesCheckBox);

            // ---- Graphics: shadows and glow
            SetTip(Tip("Sun shadows from a shadow map instead of volumes and blobs. 3D and 2D Shadows on the DISPLAY tab still pick what casts.",
                       "Тени от солнца по карте теней вместо объёмов и пятен. 3D и 2D тени на вкладке ЭКРАН по-прежнему решают, что отбрасывает тень.",
                       "Sonnenschatten per Shadow Map statt Volumen und Flecken. 3D- und 2D-Schatten im Reiter ANZEIGE bestimmen weiter, was Schatten wirft."),
                   ShadowMapCheckBox);
            SetTip(Tip("Sharper shadows at higher sizes. 4096 x 4096 uses 64 MB of video memory.",
                       "Чем больше, тем чётче тени. 4096 x 4096 занимает 64 МБ видеопамяти.",
                       "Größer = schärfere Schatten. 4096 x 4096 belegt 64 MB Grafikspeicher."),
                   ShadowMapResolutionLabel, ShadowMapResolutionComboBox);
            SetTip(Tip("Glow around fire, lasers, muzzle flashes and other bright effects.",
                       "Свечение вокруг огня, лазеров, вспышек выстрелов и других ярких эффектов.",
                       "Leuchten um Feuer, Laser, Mündungsfeuer und andere helle Effekte."),
                   BloomCheckBox);
            SetTip(Tip("How strong the glow is. 0 is off.",
                       "Сила свечения. 0 — выключено.",
                       "Stärke des Leuchtens. 0 ist aus."),
                   BloomStrengthLabel, BloomStrengthUpDown);
            SetTip(Tip("Laser beams cast coloured light on the ground.",
                       "Лазерные лучи отбрасывают цветной свет на землю.",
                       "Laserstrahlen werfen farbiges Licht auf den Boden."),
                   LaserRefCheckBox);

            // ---- Graphics: surfaces, lights, sky and water
            SetTip(Tip("Shiny highlights on units and structures.",
                       "Блики на юнитах и зданиях.",
                       "Glanzlichter auf Einheiten und Gebäuden."),
                   SpecularCheckBox);
            SetTip(Tip("Bump detail on units, structures and terrain.",
                       "Рельефная детализация юнитов, зданий и ландшафта.",
                       "Reliefdetails auf Einheiten, Gebäuden und Gelände."),
                   NormalMapsCheckBox);
            SetTip(Tip("Terrain textures blend by height instead of a plain fade.",
                       "Текстуры ландшафта смешиваются по высоте, а не простым переходом.",
                       "Geländetexturen gehen nach Höhe ineinander über statt einfach zu überblenden."),
                   HeightBlendCheckBox);
            SetTip(Tip("Explosions, muzzle flashes and lasers light up their surroundings.",
                       "Взрывы, вспышки выстрелов и лазеры освещают окружение.",
                       "Explosionen, Mündungsfeuer und Laser erhellen ihre Umgebung."),
                   DynamicLightsCheckBox);
            SetTip(Tip("Light each pixel rather than each vertex, for smoother lighting. Needs dynamic lights.",
                       "Освещение каждого пикселя, а не вершины, — плавнее. Требует динамического света.",
                       "Licht pro Pixel statt pro Eckpunkt, für weicheres Licht. Braucht dynamische Lichter."),
                   PixelLightsCheckBox);
            SetTip(Tip("Soft drifting cloud shadows. Needs Cloud Shadows on the DISPLAY tab.",
                       "Мягкие плывущие тени облаков. Нужны «Тени облаков» на вкладке ЭКРАН.",
                       "Weiche ziehende Wolkenschatten. Braucht Wolkenschatten im Reiter ANZEIGE."),
                   HQSkyCheckBox);
            SetTip(Tip("Reflections on water. Needs Smooth Water Borders on the DISPLAY tab.",
                       "Отражения в воде. Нужны «Ровные края воды» на вкладке ЭКРАН.",
                       "Spiegelungen auf dem Wasser. Braucht weiche Wasserränder im Reiter ANZEIGE."),
                   WaterReflectionsCheckBox);
            SetTip(Tip("Foam where water meets the shore. Needs Smooth Water Borders on the DISPLAY tab.",
                       "Пена у кромки воды. Нужны «Ровные края воды» на вкладке ЭКРАН.",
                       "Schaum am Ufer. Braucht weiche Wasserränder im Reiter ANZEIGE."),
                   ShorelineFoamCheckBox);
            SetTip(Tip("Shader effects for each family of weapon effects. Not on the in-game menu.",
                       "Шейдерные эффекты для каждого семейства эффектов оружия. В игровом меню их нет.",
                       "Shader-Effekte für jede Art von Waffeneffekt. Nicht im Spielmenü."),
                   ShadersEffectsLabel, FlameShadersCheckBox, ElectricShadersCheckBox,
                   LaserShadersCheckBox, CryoShadersCheckBox);
        }

        /// <summary>
        /// Pick the tip for the language the launcher is running in. English is the fallback, as it
        /// is for the captions -- the launcher has five languages but the other two have no
        /// translated tips, so they read the English rather than nothing.
        /// </summary>
        private static string Tip(string english, string russian, string german)
        {
            if (Globals.RU_Checked)
            {
                return russian;
            }

            if (Globals.DE_Checked)
            {
                return german;
            }

            return english;
        }

        /// <summary>
        /// Put one tip on every control that makes up a setting -- the label, the input, and any
        /// hint beside it -- so hovering anywhere over the row explains it rather than only the
        /// part that happens to carry the text.
        /// </summary>
        private void SetTip(string text, params Control[] controls)
        {
            foreach (Control c in controls)
            {
                GameOptionsToolTip.SetToolTip(c, text);

                // A NumericUpDown is a composite: the text box and the arrows are separate child
                // windows that take the mouse themselves, so a tip on the parent alone never shows
                // when hovering the part the user actually points at.
                foreach (Control child in c.Controls)
                {
                    GameOptionsToolTip.SetToolTip(child, text);
                }
            }
        }

        /// <summary>
        /// Russian and German overrides. The English text is the designer default in the resx, so
        /// this only replaces it when the launcher is running in another language -- matching how
        /// the rest of OptionsForm handles translation.
        /// </summary>
        private void ApplyGameOptionsLanguage()
        {
            if (!Globals.RU_Checked && !Globals.DE_Checked)
            {
                return;
            }

            if (Globals.RU_Checked)
            {
                GraphicsTabButton.Text = "ЭКРАН";
                ShadersTabButton.Text = "ГРАФИКА";
                GameOptionsTabButton.Text = "ИГРА";

                GameOptDisplayLabel.Text = "Отображение";
                HealthBarModeLabel.Text = "Полосы здоровья:";
                BuildTimerLabel.Text = "Таймеры постройки:";
                AlliedDecalLabel.Text = "Метки союзников:";
                NumericalHealthCheckBox.Text = "Показывать числа здоровья";
                SmartPipsCheckBox.Text = "Всегда показывать боезапас";
                SelectionCircleCheckBox.Text = "Круг выделения";
                ObjectDecalsCheckBox.Text = "Метки объектов";
                DefensesRangeCircleCheckBox.Text = "Радиус атаки при размещении";
                SmoothUnitMotionCheckBox.Text = "Плавное движение юнитов";
                GameOptInputLabel.Text = "Управление";
                CastModeLabel.Text = "Спецспособности:";
                ActionLinesLabel.Text = "Линии приказов:";
                EasyMilitaryDragCheckBox.Text = "Рамка не выделяет рабочих";
                SmartSelectionCheckBox.Text = "Умное выделение";
                SmartSelectionUseMouseCheckBox.Text = "Умное выделение мышью";
                SmartCommandGroupCheckBox.Text = "Строка групп юнитов";
                LowLatencyCheckBox.Text = "Режим низкой задержки";
                GridHotkeysHeaderLabel.Text = "Сетка горячих клавиш";
                GridHotkeysCheckBox.Text = "Включить сетку";
                GridHotkeyLayoutLabel.Text = "Раскладка:";
                GridHotkeyUnitOrdersCheckBox.Text = "Приказы юнитов со своими клавишами";
                HotkeyOverlayHeaderLabel.Text = "Подписи горячих клавиш";
                KeyboardOverlayCheckBox.Text = "Показывать буквы";
                KeyboardOverlayBackdropCheckBox.Text = "Подложка под буквой";
                OverlayColourLabel.Text = "Цвет букв:";
                OverlayRgbHintLabel.Text = "красный / зелёный / синий";
                BackdropColourLabel.Text = "Цвет подложки:";
                BackdropRgbHintLabel.Text = "красный / зелёный / синий / непрозрачность";
                OverlayColourButton.Text = "Выбрать…";
                BackdropColourButton.Text = "Выбрать…";
                RadarHeaderLabel.Text = "Радар";
                NewRadarCheckBox.Text = "Упрощённый радар";
                BlipSizeCheckBox.Text = "Крупные метки на радаре";

                ShadersQualityLabel.Text = "Качество изображения";
                TextureFilterLabel.Text = "Фильтрация текстур:";
                AnisotropicLevelLabel.Text = "Уровень анизотропии:";
                AntiAliasingLabel.Text = "Сглаживание:";
                SuperSamplingLabel.Text = "Суперсэмплинг:";
                AmbientOcclusionCheckBox.Text = "Фоновое затенение";
                SoftParticlesCheckBox.Text = "Мягкие частицы";
                ShadersShadowsLabel.Text = "Тени";
                ShadowMapCheckBox.Text = "Карта теней";
                ShadowMapResolutionLabel.Text = "Разрешение теней:";
                ShadersGlowLabel.Text = "Свечение";
                BloomStrengthLabel.Text = "Сила %:";
                LaserRefCheckBox.Text = "Лазеры освещают землю";
                ShadersSurfacesLabel.Text = "Поверхности";
                SpecularCheckBox.Text = "Блики";
                NormalMapsCheckBox.Text = "Детализация поверхностей";
                HeightBlendCheckBox.Text = "Смешивание по высоте";
                ShadersLightsLabel.Text = "Освещение";
                DynamicLightsCheckBox.Text = "Динамический свет";
                PixelLightsCheckBox.Text = "Попиксельный свет";
                ShadersSkyWaterLabel.Text = "Небо и вода";
                HQSkyCheckBox.Text = "Небо высокого качества";
                WaterReflectionsCheckBox.Text = "Отражения в воде";
                ShorelineFoamCheckBox.Text = "Пена у берега";
                ShadersEffectsLabel.Text = "Шейдеры эффектов";
                FlameShadersCheckBox.Text = "Огонь";
                ElectricShadersCheckBox.Text = "Ток";
                LaserShadersCheckBox.Text = "Лазер";
                CryoShadersCheckBox.Text = "Крио";
                ShadersD3D9HintLabel.Text = "Большинство этих настроек работает только в версии Direct3D 9.";
                return;
            }

            GraphicsTabButton.Text = "ANZEIGE";
            ShadersTabButton.Text = "GRAFIK";
            GameOptionsTabButton.Text = "SPIEL";

            GameOptDisplayLabel.Text = "Anzeige";
            HealthBarModeLabel.Text = "Lebensbalken:";
            BuildTimerLabel.Text = "Bauzeit-Anzeige:";
            AlliedDecalLabel.Text = "Verbündete Marker:";
            NumericalHealthCheckBox.Text = "Lebenspunkte als Zahl";
            SmartPipsCheckBox.Text = "Munition immer anzeigen";
            SelectionCircleCheckBox.Text = "Auswahlring";
            ObjectDecalsCheckBox.Text = "Objektmarkierungen";
            DefensesRangeCircleCheckBox.Text = "Reichweite beim Platzieren";
            SmoothUnitMotionCheckBox.Text = "Flüssige Einheitenbewegung";
            GameOptInputLabel.Text = "Steuerung";
            CastModeLabel.Text = "Spezialfähigkeiten:";
            ActionLinesLabel.Text = "Befehlslinien:";
            EasyMilitaryDragCheckBox.Text = "Auswahlrahmen ohne Bauarbeiter";
            SmartSelectionCheckBox.Text = "Intelligente Auswahl";
            SmartSelectionUseMouseCheckBox.Text = "Intelligente Auswahl per Maus";
            SmartCommandGroupCheckBox.Text = "Gruppenleiste";
            LowLatencyCheckBox.Text = "Niedrige Latenz";
            GridHotkeysHeaderLabel.Text = "Raster-Hotkeys";
            GridHotkeysCheckBox.Text = "Raster-Hotkeys benutzen";
            GridHotkeyLayoutLabel.Text = "Belegung:";
            GridHotkeyUnitOrdersCheckBox.Text = "Einheitenbefehle behalten ihre Tasten";
            HotkeyOverlayHeaderLabel.Text = "Hotkey-Anzeige";
            KeyboardOverlayCheckBox.Text = "Buchstaben anzeigen";
            KeyboardOverlayBackdropCheckBox.Text = "Hintergrundplatte";
            OverlayColourLabel.Text = "Farbe der Buchstaben:";
            OverlayRgbHintLabel.Text = "Rot / Grün / Blau";
            BackdropColourLabel.Text = "Farbe der Hintergrundplatte:";
            BackdropRgbHintLabel.Text = "Rot / Grün / Blau / Deckkraft";
            OverlayColourButton.Text = "Wählen…";
            BackdropColourButton.Text = "Wählen…";
            RadarHeaderLabel.Text = "Radar";
            NewRadarCheckBox.Text = "Vereinfachtes Radar";
            BlipSizeCheckBox.Text = "Große Radarpunkte";

            ShadersQualityLabel.Text = "Bildqualität";
            TextureFilterLabel.Text = "Texturfilter:";
            AnisotropicLevelLabel.Text = "Anisotropie-Stufe:";
            AntiAliasingLabel.Text = "Kantenglättung:";
            SuperSamplingLabel.Text = "Supersampling:";
            AmbientOcclusionCheckBox.Text = "Umgebungsverdeckung";
            SoftParticlesCheckBox.Text = "Weiche Partikel";
            ShadersShadowsLabel.Text = "Schatten";
            ShadowMapCheckBox.Text = "Shadow Mapping";
            ShadowMapResolutionLabel.Text = "Schattenauflösung:";
            ShadersGlowLabel.Text = "Leuchten";
            BloomStrengthLabel.Text = "Stärke %:";
            LaserRefCheckBox.Text = "Laser beleuchten den Boden";
            ShadersSurfacesLabel.Text = "Oberflächen";
            SpecularCheckBox.Text = "Glanzlichter";
            NormalMapsCheckBox.Text = "Oberflächendetails";
            HeightBlendCheckBox.Text = "Höhenüberblendung";
            ShadersLightsLabel.Text = "Licht";
            DynamicLightsCheckBox.Text = "Dynamische Lichter";
            PixelLightsCheckBox.Text = "Licht pro Pixel";
            ShadersSkyWaterLabel.Text = "Himmel und Wasser";
            HQSkyCheckBox.Text = "HQ-Himmel";
            WaterReflectionsCheckBox.Text = "Wasserspiegelungen";
            ShorelineFoamCheckBox.Text = "Uferschaum";
            ShadersEffectsLabel.Text = "Effekt-Shader";
            FlameShadersCheckBox.Text = "Feuer";
            ElectricShadersCheckBox.Text = "Elektro";
            LaserShadersCheckBox.Text = "Laser";
            CryoShadersCheckBox.Text = "Kryo";
            ShadersD3D9HintLabel.Text = "Die meisten dieser Einstellungen brauchen die Direct3D-9-Version des Spiels.";
        }
    }
}
