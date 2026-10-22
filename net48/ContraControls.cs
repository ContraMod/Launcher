using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Contra
{
    /// <summary>
    /// The launcher's colours for the controls Windows would otherwise draw in its own grey and
    /// blue: the skin's gold frame for borders, its dark brown for fills, and the pale yellow the
    /// buttons already use on hover.
    /// </summary>
    internal static class ContraTheme
    {
        public static readonly Color Border = Color.FromArgb(171, 150, 57);
        public static readonly Color BorderHot = Globals.buttonHighlight;
        public static readonly Color Fill = Color.FromArgb(28, 19, 9);
        public static readonly Color Mark = Color.FromArgb(240, 190, 70);
        public static readonly Color Selection = Color.FromArgb(110, 80, 30);
        public static readonly Color DisabledBorder = Color.FromArgb(95, 85, 65);
        public static readonly Color DisabledMark = Color.FromArgb(120, 110, 90);
        public static readonly Color DisabledText = Globals.disabledTextColor;

        /// <summary>
        /// The glyph and the caption beside it, laid out where Windows puts them, so AutoSize --
        /// which still measures the standard 13px glyph -- leaves exactly the right room.
        /// </summary>
        public static void PaintToggle(ButtonBase control, PaintEventArgs e, bool hot, bool on, bool round)
        {
            const int glyph = 13;
            Graphics g = e.Graphics;
            Rectangle box = new Rectangle(0, (control.Height - glyph) / 2, glyph - 1, glyph - 1);

            Color border = !control.Enabled ? DisabledBorder : hot ? BorderHot : Border;
            Color mark = control.Enabled ? Mark : DisabledMark;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (SolidBrush fill = new SolidBrush(Fill))
            using (Pen pen = new Pen(border))
            {
                if (round)
                {
                    g.FillEllipse(fill, box);
                    g.DrawEllipse(pen, box);
                }
                else
                {
                    g.FillRectangle(fill, box);
                    g.DrawRectangle(pen, box);
                }
            }

            if (on)
            {
                if (round)
                {
                    using (SolidBrush dot = new SolidBrush(mark))
                    {
                        g.FillEllipse(dot, box.X + 3, box.Y + 3, box.Width - 6, box.Height - 6);
                    }
                }
                else
                {
                    using (Pen tick = new Pen(mark, 2f))
                    {
                        tick.StartCap = LineCap.Round;
                        tick.EndCap = LineCap.Round;
                        tick.LineJoin = LineJoin.Round;
                        g.DrawLines(tick, new[]
                        {
                            new Point(box.X + 3, box.Y + 6),
                            new Point(box.X + 5, box.Y + 9),
                            new Point(box.X + 9, box.Y + 3)
                        });
                    }
                }
            }

            g.SmoothingMode = SmoothingMode.Default;

            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
            if (!control.UseMnemonic)
            {
                flags |= TextFormatFlags.NoPrefix;
            }

            Rectangle text = new Rectangle(glyph + 3, 0, control.Width - glyph - 3, control.Height);
            TextRenderer.DrawText(g, control.Text, control.Font, text,
                control.Enabled ? control.ForeColor : DisabledText, flags);
        }
    }

    /// <summary>A check box in the launcher's colours. Behaves exactly like the standard one.</summary>
    internal class ContraCheckBox : CheckBox
    {
        private bool hot;

        public ContraCheckBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Transparent, like every control on the skin: the parent paints what is behind.
            OnPaintBackground(e);
            ContraTheme.PaintToggle(this, e, hot, CheckState != CheckState.Unchecked, false);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hot = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hot = false;
            Invalidate();
            base.OnMouseLeave(e);
        }
    }

    /// <summary>A radio button in the launcher's colours. Behaves exactly like the standard one.</summary>
    internal class ContraRadioButton : RadioButton
    {
        private bool hot;

        public ContraRadioButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            OnPaintBackground(e);
            ContraTheme.PaintToggle(this, e, hot, Checked, true);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hot = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hot = false;
            Invalidate();
            base.OnMouseLeave(e);
        }
    }

    /// <summary>
    /// A combo box in the launcher's colours. The items, and in a drop-down list the selected item
    /// shown in the box, are owner-drawn; the border and the arrow button are painted over the
    /// native ones after each paint, which works for both the drop-down list and the editable
    /// style -- a fully user-painted combo box loses its edit field.
    /// </summary>
    internal class ContraComboBox : ComboBox
    {
        private const int WM_PAINT = 0x000F;
        private const int WM_CTLCOLORSTATIC = 0x0138;

        [DllImport("gdi32.dll")]
        private static extern int SetTextColor(IntPtr hdc, int color);

        [DllImport("gdi32.dll")]
        private static extern int SetBkColor(IntPtr hdc, int color);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateSolidBrush(int color);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr handle);

        private IntPtr fillBrush;

        public ContraComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            FlatStyle = FlatStyle.Flat;
            BackColor = ContraTheme.Fill;
            ForeColor = Color.White;
            // Owner-drawn combo boxes are ItemHeight + 6 tall; 15 keeps the standard 21px.
            ItemHeight = 15;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            bool inBox = (e.State & DrawItemState.ComboBoxEdit) != 0;
            bool selected = !inBox && (e.State & DrawItemState.Selected) != 0;

            using (SolidBrush back = new SolidBrush(selected ? ContraTheme.Selection : ContraTheme.Fill))
            {
                e.Graphics.FillRectangle(back, e.Bounds);
            }

            if (e.Index >= 0)
            {
                Color text = !Enabled ? ContraTheme.DisabledText : selected ? ContraTheme.BorderHot : ForeColor;
                TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), Font, e.Bounds, text,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine
                    | TextFormatFlags.NoPrefix | TextFormatFlags.EndEllipsis);
            }
        }

        protected override void WndProc(ref Message m)
        {
            // The editable style's text field is a native edit, which asks its combo box for colours
            // with WM_CTLCOLORSTATIC once disabled -- and would otherwise get the system grey.
            if (m.Msg == WM_CTLCOLORSTATIC && DropDownStyle == ComboBoxStyle.DropDown)
            {
                if (fillBrush == IntPtr.Zero)
                {
                    fillBrush = CreateSolidBrush(ColorTranslator.ToWin32(ContraTheme.Fill));
                }

                SetTextColor(m.WParam, ColorTranslator.ToWin32(ContraTheme.DisabledText));
                SetBkColor(m.WParam, ColorTranslator.ToWin32(ContraTheme.Fill));
                m.Result = fillBrush;
                return;
            }

            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                PaintChrome();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (fillBrush != IntPtr.Zero)
            {
                DeleteObject(fillBrush);
                fillBrush = IntPtr.Zero;
            }

            base.Dispose(disposing);
        }

        private void PaintChrome()
        {
            int buttonWidth = SystemInformation.HorizontalScrollBarArrowWidth;
            Rectangle all = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle button = new Rectangle(Width - buttonWidth - 1, 1, buttonWidth, Height - 2);
            Color border = Enabled ? ContraTheme.Border : ContraTheme.DisabledBorder;

            using (Graphics g = Graphics.FromHwnd(Handle))
            using (SolidBrush fill = new SolidBrush(ContraTheme.Fill))
            using (Pen pen = new Pen(border))
            using (SolidBrush arrow = new SolidBrush(Enabled ? ContraTheme.Mark : ContraTheme.DisabledMark))
            {
                // A disabled flat combo box is painted white by Windows and never asks for its
                // item to be drawn, so in a drop-down list paint the whole box and its text here.
                if (!Enabled && DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    Rectangle inner = new Rectangle(1, 1, Width - 2, Height - 2);
                    g.FillRectangle(fill, inner);
                    TextRenderer.DrawText(g, Text, Font, new Rectangle(3, 1, Width - buttonWidth - 5, Height - 2),
                        ContraTheme.DisabledText, TextFormatFlags.Left | TextFormatFlags.VerticalCenter
                        | TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix | TextFormatFlags.EndEllipsis);
                }

                // The editable style keeps its own text field, but the white frame Windows draws
                // round it when disabled sits outside that field, so cover just the frame.
                if (!Enabled && DropDownStyle == ComboBoxStyle.DropDown)
                {
                    using (Pen frame = new Pen(ContraTheme.Fill, 2f))
                    {
                        g.DrawRectangle(frame, 2, 2, Width - 4, Height - 4);
                    }
                }

                // Two pixels left of the button too: the native button's edge lies there, white
                // when disabled. Enabled it is box colour already, so painting it costs nothing.
                g.FillRectangle(fill, button.Left - 2, button.Top, button.Width + 2, button.Height);
                g.DrawLine(pen, button.Left, button.Top, button.Left, button.Bottom - 1);
                g.DrawRectangle(pen, all);

                int cx = button.Left + button.Width / 2;
                int cy = Height / 2;
                g.FillPolygon(arrow, new[]
                {
                    new Point(cx - 4, cy - 2), new Point(cx + 5, cy - 2), new Point(cx, cy + 3)
                });
            }
        }
    }
}
