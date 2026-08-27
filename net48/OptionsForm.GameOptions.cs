using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Contra
{
    /// <summary>
    /// The "Game Options" page: the contraZH engine settings, which the retail options screen
    /// knows nothing about.
    ///
    /// The controls are declared and positioned in OptionsForm.Designer.cs / OptionsForm.resx
    /// with the rest of the form, so they show on the WinForms designer canvas and can be
    /// dragged there. This file holds only the behaviour: page swapping, enable/disable rules,
    /// translations, and reading and writing Options.ini.
    ///
    /// The page swap has to happen in code. The form is a fixed 800x600 skinned window and a real
    /// TabControl would paint its own grey page over the background image, so instead the existing
    /// controls are gathered onto one panel at startup, the new ones sit on another, and two flat
    /// transparent buttons switch between them.
    /// </summary>
    public partial class OptionsForm
    {
        private static readonly Color TabActiveColor = Color.White;
        private static readonly Color TabInactiveColor = Color.FromArgb(150, 150, 150);

        // The vertical band the existing controls occupy, from OptionsForm.resx. The graphics
        // content starts at the "Resolution:" / "Camera Height" labels on y=72, so the page has to
        // begin above those rather than below them -- they belong to the graphics page and must
        // hide with it. That leaves y=57..72 for the tab row, the gap between the window buttons
        // (which end at y=57) and those labels. Accept/Close at y=525 stay on the form, shared.
        //
        // Keep PageTop in step with the GraphicsPage/GameOptionsPage Location in the resx: the
        // controls moved onto the graphics page are shifted up by exactly this much.
        private const int PageTop = 70;
        private const int PageBottom = 515;

        private void BuildGameOptionsPage()
        {
            // The controls, their geometry and their parenting all come from the designer now.
            // What is left is the part the designer cannot express: moving the pre-existing
            // graphics controls onto their page so the two can be swapped.
            //
            // Captured before the loop because reparenting mutates Controls while it runs.
            Control[] existing = new Control[Controls.Count];
            Controls.CopyTo(existing, 0);

            foreach (Control c in existing)
            {
                // The pages themselves and the tab row are not graphics content.
                if (c == GraphicsPage || c == GameOptionsPage
                        || c == GraphicsTabButton || c == GameOptionsTabButton)
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

            EnableDoubleBuffering(GraphicsPage);
            EnableDoubleBuffering(GameOptionsPage);

            ConfigureGameOptionControls();
            StyleTabButton(GraphicsTabButton);
            StyleTabButton(GameOptionsTabButton);

            GraphicsTabButton.Click += delegate { ShowPage(false); };
            GameOptionsTabButton.Click += delegate { ShowPage(true); };

            // The overlay colours only matter when the overlay is on, and the grid fields only when
            // grid hotkeys are, so grey each group out with its switch.
            KeyboardOverlayCheckBox.CheckedChanged += delegate { UpdateOverlayEnabledState(); };
            KeyboardOverlayBackdropCheckBox.CheckedChanged += delegate { UpdateOverlayEnabledState(); };
            GridHotkeysCheckBox.CheckedChanged += delegate { UpdateGridEnabledState(); };

            // The column count is what folds the layout into rows, so the grid has to follow it.
            GridHotkeyColumnsUpDown.ValueChanged += delegate
            {
                GridHotkeyEditor.Columns = (int)GridHotkeyColumnsUpDown.Value;
            };
            TextureFilterComboBox.SelectedIndexChanged += delegate { UpdateTextureFilterEnabledState(); };

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
            ShowPage(false);
        }

        /// <summary>
        /// The bits of setup the designer cannot serialise: combo box items, numeric ranges and
        /// the hint colour. Positions and parenting come from the designer.
        /// </summary>
        private void ConfigureGameOptionControls()
        {

            HealthBarModeComboBox.Items.AddRange(new object[] { "Classic", "Damaged", "Always" });
            BuildTimerComboBox.Items.AddRange(new object[] { "None", "Seconds", "Auto" });
            CastModeComboBox.Items.AddRange(new object[] { "Normal", "QuickCast", "QuickCastWithIndicator" });
            TextureFilterComboBox.Items.AddRange(new object[] { "None", "Point", "Bilinear", "Trilinear", "Anisotropic" });
            // The engine rounds down to one of these, so offering anything else would only mislead.
            AnisotropicLevelComboBox.Items.AddRange(new object[] { "2", "4", "8", "16" });

            GridHotkeyColumnsUpDown.Minimum = 0;
            GridHotkeyColumnsUpDown.Maximum = 20;

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
        }

        private static void SetChannelRange(NumericUpDown n)
        {
            n.Minimum = 0;
            n.Maximum = 255;
        }

        /// <summary>
        /// Flat, borderless and transparent, so the tabs read as text on the skin rather than as
        /// Windows buttons sitting on top of it. Not expressible in the resx, hence in code.
        /// </summary>
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

        private static void StyleTabButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = Color.Transparent;
            b.FlatAppearance.MouseDownBackColor = Color.Transparent;
            b.TabStop = false;
            b.Cursor = Cursors.Hand;
        }

        private void ShowPage(bool gameOptions)
        {
            Panel wanted = gameOptions ? GameOptionsPage : GraphicsPage;
            Panel other = gameOptions ? GraphicsPage : GameOptionsPage;

            // Every control on these pages is BackColor.Transparent over the form's background
            // image, so each one repaints by compositing against its parent. Flipping the two
            // panels sends that through as a cascade of individual paints, which is what made the
            // swap feel sluggish. Freeze painting for the whole swap and send one paint at the end.
            SuspendDrawing(this);
            try
            {
                SuspendLayout();

                // Hide first, then show and raise. Both panels cover the same rectangle, and
                // BackColor.Transparent on a WinForms panel means "let whatever is behind me show
                // through", not "draw nothing" -- so leaving the old page visible underneath lets
                // its controls paint through the new one.
                other.Visible = false;
                wanted.Visible = true;
                wanted.BringToFront();

                // The tab buttons live on the form, not on a page, so they have to stay above both.
                GraphicsTabButton.BringToFront();
                GameOptionsTabButton.BringToFront();

                GraphicsTabButton.ForeColor = gameOptions ? TabInactiveColor : TabActiveColor;
                GameOptionsTabButton.ForeColor = gameOptions ? TabActiveColor : TabInactiveColor;

                ResumeLayout(false);
            }
            finally
            {
                ResumeDrawing(this);
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

        /// <summary>
        /// The sample count is only read when the filter is Anisotropic, so grey it out otherwise
        /// rather than letting it look like it does something.
        /// </summary>
        private void UpdateTextureFilterEnabledState()
        {
            AnisotropicLevelComboBox.Enabled = string.Equals(
                SelectedText(TextureFilterComboBox, "Bilinear"), "Anisotropic", StringComparison.OrdinalIgnoreCase);
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

        private void UpdateGridEnabledState()
        {
            bool on = GridHotkeysCheckBox.Checked;
            GridHotkeyEditor.Enabled = on;
            GridHotkeyColumnsUpDown.Enabled = on;
            NonGridOverflowTextBox.Enabled = on;
        }

        /// <summary>
        /// Read the engine settings out of Options.ini. Every fallback here matches the engine's own
        /// default in OptionPreferences.cpp, so an absent key shows what the game will actually do.
        /// </summary>
        private void LoadGameOptions()
        {
            string s = GameOptionsIni.ReadAll();

            HealthBarModeComboBox.SelectedItem =
                MatchOrFirst(HealthBarModeComboBox, GameOptionsIni.GetValue(s, "HealthBarDisplayMode", "Classic"));
            BuildTimerComboBox.SelectedItem =
                MatchOrFirst(BuildTimerComboBox, GameOptionsIni.GetValue(s, "BuildTimerDisplayMode", "None"));
            CastModeComboBox.SelectedItem =
                MatchOrFirst(CastModeComboBox, GameOptionsIni.GetValue(s, "CastMode", "Normal"));

            // Bilinear and 2 are what the engine falls back to when the key is absent. Match on the
            // value rather than MatchOrFirst: "None" is a real mode and sits first in the list, so a
            // value that does not match would otherwise land there and silently turn filtering off.
            TextureFilterComboBox.SelectedItem =
                MatchOrDefault(TextureFilterComboBox, GameOptionsIni.GetValue(s, "TextureFilter", "Bilinear"), "Bilinear");
            AnisotropicLevelComboBox.SelectedItem =
                MatchOrFirst(AnisotropicLevelComboBox, NearestAnisotropyLevel(GameOptionsIni.GetInt(s, "AnisotropyLevel", 2)));

            NumericalHealthCheckBox.Checked = GameOptionsIni.GetBool(s, "NumericalHealth", false);
            SmartPipsCheckBox.Checked = GameOptionsIni.GetBool(s, "SmartPips", false);
            SelectionCircleCheckBox.Checked = GameOptionsIni.GetBool(s, "SelectionCircle", false);
            EasyMilitaryDragCheckBox.Checked = GameOptionsIni.GetBool(s, "EasyMilitaryDrag", false);

            GridHotkeysCheckBox.Checked = GameOptionsIni.GetBool(s, "GridHotkeys", false);
            GridHotkeyColumnsUpDown.Value =
                Clamp(GameOptionsIni.GetInt(s, "GridHotkeyColumns", 9), GridHotkeyColumnsUpDown);

            // Columns before layout, so the grid is already the right shape when the keys land, and
            // exclusions last, since which cells they mark depends on the layout being in place.
            GridHotkeyEditor.Columns = (int)GridHotkeyColumnsUpDown.Value;
            GridHotkeyEditor.Layout =
                GameOptionsIni.GetValue(s, "GridHotkeyLayout", "QWERTYUIOASDFGHJKL");
            GridHotkeyEditor.Exclusions = GameOptionsIni.GetValue(s, "NonGridHotkeys", string.Empty);
            NonGridOverflowTextBox.Text = GridHotkeyEditor.OverflowExclusions;

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

            UpdateOverlayEnabledState();
            UpdateGridEnabledState();
            UpdateTextureFilterEnabledState();
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
        /// Mirror what getTextureAnisotropyLevel does to a hand edited value: clamp it into 2..16
        /// and then keep only the highest set bit, so the list shows the level the game will
        /// actually use rather than snapping to the first item.
        /// </summary>
        private static string NearestAnisotropyLevel(int level)
        {
            if (level < 2)
            {
                return "2";
            }

            if (level > 16)
            {
                return "16";
            }

            // Highest set bit, matching the engine's highestBit().
            int highest = 2;
            while (highest * 2 <= level)
            {
                highest *= 2;
            }

            return highest.ToString();
        }

        /// <summary>
        /// Like MatchOrFirst, but falls back to a named item instead of the first one. Used where
        /// the first entry is a meaningful setting in its own right rather than a safe default.
        /// </summary>
        private static object MatchOrDefault(ComboBox box, string value, string fallback)
        {
            foreach (object item in box.Items)
            {
                if (string.Equals(item.ToString(), value, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }

            return MatchOrFirst(box, fallback);
        }

        private static object MatchOrFirst(ComboBox box, string value)
        {
            foreach (object item in box.Items)
            {
                if (string.Equals(item.ToString(), value, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }

            return box.Items.Count > 0 ? box.Items[0] : null;
        }

        /// <summary>
        /// Write the engine settings back. One read and one write for the whole page, rather than a
        /// read/write pair per setting as the retail options do.
        /// </summary>
        private void SaveGameOptions()
        {
            if (!GameOptionsIni.Exists())
            {
                return;
            }

            string s = GameOptionsIni.ReadAll();

            s = GameOptionsIni.SetValue(s, "HealthBarDisplayMode", SelectedText(HealthBarModeComboBox, "Classic"));
            s = GameOptionsIni.SetValue(s, "BuildTimerDisplayMode", SelectedText(BuildTimerComboBox, "None"));
            s = GameOptionsIni.SetValue(s, "CastMode", SelectedText(CastModeComboBox, "Normal"));
            s = GameOptionsIni.SetValue(s, "TextureFilter", SelectedText(TextureFilterComboBox, "Bilinear"));
            s = GameOptionsIni.SetValue(s, "AnisotropyLevel", SelectedText(AnisotropicLevelComboBox, "2"));

            s = GameOptionsIni.SetBool(s, "NumericalHealth", NumericalHealthCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "SmartPips", SmartPipsCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "SelectionCircle", SelectionCircleCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "EasyMilitaryDrag", EasyMilitaryDragCheckBox.Checked);

            s = GameOptionsIni.SetBool(s, "GridHotkeys", GridHotkeysCheckBox.Checked);
            // Whatever is typed in the overflow box is the authority on keys with no slot; the
            // editor supplies the ones that do.
            GridHotkeyEditor.OverflowExclusions = NonGridOverflowTextBox.Text.Trim();

            s = GameOptionsIni.SetValue(s, "GridHotkeyLayout", GridHotkeyEditor.Layout);
            s = GameOptionsIni.SetInt(s, "GridHotkeyColumns", (int)GridHotkeyColumnsUpDown.Value);
            s = GameOptionsIni.SetValue(s, "NonGridHotkeys", GridHotkeyEditor.Exclusions);

            s = GameOptionsIni.SetBool(s, "KeyboardOverlay", KeyboardOverlayCheckBox.Checked);
            s = GameOptionsIni.SetBool(s, "KeyboardOverlayBackdrop", KeyboardOverlayBackdropCheckBox.Checked);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayRed", (int)OverlayRedUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayGreen", (int)OverlayGreenUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayBlue", (int)OverlayBlueUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayBackdropRed", (int)BackdropRedUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayBackdropGreen", (int)BackdropGreenUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayBackdropBlue", (int)BackdropBlueUpDown.Value);
            s = GameOptionsIni.SetInt(s, "KeyboardOverlayBackdropOpacity", (int)BackdropOpacityUpDown.Value);

            GameOptionsIni.WriteAll(s);
        }

        private static string SelectedText(ComboBox box, string fallback)
        {
            return box.SelectedItem != null ? box.SelectedItem.ToString() : fallback;
        }

        /// <summary>
        /// One line of help per setting, shown on hover. The graphics page has its own info panel
        /// for this, but that panel is part of the graphics layout rather than the form, so these
        /// use a plain ToolTip instead of borrowing it.
        ///
        /// English only, like the rest of the tooltips the launcher does not translate -- the
        /// captions are translated above, and a half translated tip would read worse than a
        /// consistent English one.
        /// </summary>
        private void ApplyGameOptionTooltips()
        {
            // A long tip on a slow popup is easy to miss, so show them quickly and leave them up.
            GameOptionsToolTip.InitialDelay = 350;
            GameOptionsToolTip.ReshowDelay = 100;
            GameOptionsToolTip.AutoPopDelay = 20000;

            // Display
            SetTip(Tip("Classic: original look. Damaged: only on damaged units. Always: on every unit. Cycle in game with Ctrl+`.",
                       "Classic: обычный вид. Damaged: только у повреждённых. Always: у всех. В игре переключается через Ctrl+`.",
                       "Classic: normale Ansicht. Damaged: nur bei beschädigten Einheiten. Always: bei allen. Im Spiel mit Strg+` umschaltbar."),
                   HealthBarModeLabel, HealthBarModeComboBox);
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
            // Auto switches to m:ss once a minute or more is left, and shows plain seconds below
            // that -- see the countdown formatting in W3DPushButton.cpp.
            SetTip(Tip("Show remaining build time. Seconds: always in seconds. Auto: minutes and seconds from 1:00 up.",
                       "Показывать оставшееся время постройки. Seconds: всегда в секундах. Auto: минуты и секунды от 1:00.",
                       "Verbleibende Bauzeit anzeigen. Seconds: immer in Sekunden. Auto: Minuten und Sekunden ab 1:00."),
                   BuildTimerLabel, BuildTimerComboBox);

            // Input
            SetTip(Tip("QuickCast: fire at the cursor on key press, no second click.",
                       "QuickCast: применять по курсору сразу при нажатии, без второго щелчка.",
                       "QuickCast: auf Tastendruck sofort am Cursor auslösen, ohne zweiten Klick."),
                   CastModeLabel, CastModeComboBox);
            SetTip(Tip("Drag selection picks combat units only. Click a builder to select it.",
                       "Рамка выделяет только боевые юниты. Рабочего можно выделить щелчком.",
                       "Der Auswahlrahmen erfasst nur Kampfeinheiten. Bauarbeiter per Klick auswählen."),
                   EasyMilitaryDragCheckBox);

            // Graphics
            SetTip(Tip("Texture smoothing at distance. Anisotropic = best quality, negligible cost.",
                       "Сглаживание текстур на расстоянии. Anisotropic — лучшее качество почти без затрат.",
                       "Texturglättung in der Ferne. Anisotropic = beste Qualität, kaum Kosten."),
                   TextureFilterLabel, TextureFilterComboBox);
            SetTip(Tip("Higher = sharper ground textures at flat angles. 16 is safe on modern GPUs.",
                       "Больше — чётче текстуры земли под острым углом. 16 безопасно на современных видеокартах.",
                       "Höher = schärfere Bodentexturen bei flachen Winkeln. 16 ist auf modernen GPUs unbedenklich."),
                   AnisotropicLevelLabel, AnisotropicLevelComboBox);

            // Grid hotkeys
            // Rows come from the layout's length over the column count, so the shipped 18 letters
            // at 9 columns are two rows -- hence no row letters here, they would be wrong for any
            // other layout. Ctrl and Alt keep their meta events; Shift deliberately does not,
            // being how the command bar batches. See MetaEvent.cpp's (keyModState & ~SHIFT) test.
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
            SetTip(Tip("One key per slot, laid out as the command bar is. Type to set a key; click a key to exclude it.",
                       "По одной клавише на ячейку, в порядке панели команд. Введите клавишу; щёлкните по ней, чтобы исключить.",
                       "Eine Taste pro Feld, angeordnet wie die Befehlsleiste. Tippen setzt eine Taste; Klicken schließt sie aus."),
                   GridHotkeyLayoutLabel, GridHotkeyEditor);
            SetTip(Tip("Keys per row before wrapping. Match the command bar's columns.",
                       "Клавиш в строке до переноса. Должно совпадать со столбцами панели команд.",
                       "Tasten pro Zeile bis zum Umbruch. Muss den Spalten der Befehlsleiste entsprechen."),
                   GridHotkeyColumnsLabel, GridHotkeyColumnsUpDown);
            SetTip(Tip("Excluded keys that are not on the grid. Keys on the grid are excluded by clicking them.",
                       "Исключённые клавиши, которых нет в сетке. Клавиши из сетки исключаются щелчком по ним.",
                       "Ausgeschlossene Tasten, die nicht im Raster liegen. Tasten im Raster werden per Klick ausgeschlossen."),
                   NonGridHotkeysLabel, NonGridOverflowTextBox);

            // Hotkey letters on cameos
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
        }

        /// <summary>
        /// Pick the tip for the language the launcher is running in. English is the fallback, as it
        /// is for the captions -- the launcher has five languages but the other three have no
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
                GraphicsTabButton.Text = "ГРАФИКА";
                GameOptionsTabButton.Text = "ИГРА";
                GameOptDisplayLabel.Text = "Отображение";
                HealthBarModeLabel.Text = "Полосы здоровья:";
                NumericalHealthCheckBox.Text = "Показывать числа здоровья";
                SmartPipsCheckBox.Text = "Всегда показывать боезапас";
                SelectionCircleCheckBox.Text = "Круг выделения";
                BuildTimerLabel.Text = "Таймеры постройки:";
                GameOptInputLabel.Text = "Управление";
                CastModeLabel.Text = "Спецспособности:";
                EasyMilitaryDragCheckBox.Text = "Рамка не выделяет рабочих";
                GridHotkeysHeaderLabel.Text = "Сетка горячих клавиш";
                GridHotkeysCheckBox.Text = "Включить сетку";
                GridHotkeyLayoutLabel.Text = "Раскладка (клавиша на слот):";
                GridHotkeyColumnsLabel.Text = "Столбцов:";
                NonGridHotkeysLabel.Text = "Исключения вне сетки:";
                HotkeyOverlayHeaderLabel.Text = "Подписи горячих клавиш";
                KeyboardOverlayCheckBox.Text = "Показывать буквы";
                KeyboardOverlayBackdropCheckBox.Text = "Подложка под буквой";
                OverlayColourLabel.Text = "Цвет букв:";
                OverlayRgbHintLabel.Text = "красный / зелёный / синий";
                BackdropColourLabel.Text = "Цвет подложки:";
                BackdropRgbHintLabel.Text = "красный / зелёный / синий / непрозрачность";
                OverlayColourButton.Text = "Выбрать…";
                BackdropColourButton.Text = "Выбрать…";
                GameOptGraphicsLabel.Text = "Графика";
                TextureFilterLabel.Text = "Фильтрация текстур:";
                AnisotropicLevelLabel.Text = "Уровень анизотропии:";
                return;
            }

            GraphicsTabButton.Text = "GRAFIK";
            GameOptionsTabButton.Text = "SPIEL";
            GameOptDisplayLabel.Text = "Anzeige";
            HealthBarModeLabel.Text = "Lebensbalken:";
            NumericalHealthCheckBox.Text = "Lebenspunkte als Zahl";
            SmartPipsCheckBox.Text = "Munition immer anzeigen";
            SelectionCircleCheckBox.Text = "Auswahlring";
            BuildTimerLabel.Text = "Bauzeit-Anzeige:";
            GameOptInputLabel.Text = "Steuerung";
            CastModeLabel.Text = "Spezialfähigkeiten:";
            EasyMilitaryDragCheckBox.Text = "Auswahlrahmen ohne Bauarbeiter";
            GridHotkeysHeaderLabel.Text = "Raster-Hotkeys";
            GridHotkeysCheckBox.Text = "Raster-Hotkeys benutzen";
            GridHotkeyLayoutLabel.Text = "Belegung (Taste je Feld):";
            GridHotkeyColumnsLabel.Text = "Spalten:";
            NonGridHotkeysLabel.Text = "Ausschlüsse ohne Raster:";
            HotkeyOverlayHeaderLabel.Text = "Hotkey-Anzeige";
            KeyboardOverlayCheckBox.Text = "Buchstaben anzeigen";
            KeyboardOverlayBackdropCheckBox.Text = "Hintergrundplatte";
            OverlayColourLabel.Text = "Farbe der Buchstaben:";
            OverlayRgbHintLabel.Text = "Rot / Grün / Blau";
            BackdropColourLabel.Text = "Farbe der Hintergrundplatte:";
            BackdropRgbHintLabel.Text = "Rot / Grün / Blau / Deckkraft";
            OverlayColourButton.Text = "Wählen…";
            BackdropColourButton.Text = "Wählen…";
            GameOptGraphicsLabel.Text = "Grafik";
            TextureFilterLabel.Text = "Texturfilter:";
            AnisotropicLevelLabel.Text = "Anisotropie-Stufe:";
        }
    }
}
