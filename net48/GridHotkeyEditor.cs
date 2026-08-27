using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Contra
{
    /// <summary>
    /// The grid hotkey layout, drawn as the command bar it describes: one cell per slot, arranged
    /// in the same rows and columns the game will use.
    ///
    /// The three settings behind this are one idea split across three Options.ini keys --
    /// GridHotkeyLayout is a flat string, GridHotkeyColumns says how to fold it, and NonGridHotkeys
    /// lists keys that keep their original meaning. Typed into three separate boxes, the whole
    /// premise of the feature (key position = button position) is something the player has to hold
    /// in their head. Here the layout is typed into the slots themselves, the column count reshapes
    /// the grid as it changes, and an excluded key is struck through in the slot it would occupy.
    ///
    /// Left click a cell and type to set its key. Right click a cell to exclude that key.
    /// </summary>
    internal sealed class GridHotkeyEditor : Panel
    {
        private const int CellSize = 24;
        private const int CellGap = 2;

        private readonly List<CellBox> cells = new List<CellBox>();
        private readonly HashSet<char> excluded = new HashSet<char>();

        private int columns = 9;
        private string layout = string.Empty;

        /// <summary>Raised when the layout or the exclusions change, so the form can react.</summary>
        public event EventHandler LayoutChanged;

        public GridHotkeyEditor()
        {
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        /// <summary>
        /// Keys excluded from the grid that are not on it -- NonGridHotkeys matches by character
        /// regardless of position, so a config can legitimately exclude a key that has no slot.
        /// Kept verbatim rather than dropped, so loading and saving does not quietly lose them.
        /// </summary>
        public string OverflowExclusions { get; set; }

        /// <summary>
        /// The layout string as Options.ini holds it: the cells in reading order. Named KeyLayout
        /// rather than Layout because Control already has a Layout event, and a property of the
        /// same name on a Panel subclass silently shadows it.
        /// </summary>
        public string KeyLayout
        {
            get { return CollectLayout(); }
            set
            {
                layout = value ?? string.Empty;
                Rebuild();
            }
        }

        public int Columns
        {
            get { return columns; }
            set
            {
                // 0 means "already in slot order" to the engine, which for drawing purposes is a
                // single row -- there is no folding to do.
                columns = value;
                Rebuild();
            }
        }

        /// <summary>
        /// The exclusion list as the engine reads it: every excluded key on the grid, plus any that
        /// were excluded without having a slot.
        /// </summary>
        public string Exclusions
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (CellBox cell in cells)
                {
                    if (cell.Excluded && cell.Key != '\0')
                    {
                        sb.Append(cell.Key);
                    }
                }

                sb.Append(OverflowExclusions ?? string.Empty);
                return sb.ToString();
            }

            set
            {
                excluded.Clear();
                StringBuilder overflow = new StringBuilder();

                foreach (char c in value ?? string.Empty)
                {
                    // The engine scans the list a character at a time and ignores anything that is
                    // not a key, so commas and spaces are separators rather than entries.
                    if (char.IsWhiteSpace(c) || c == ',')
                    {
                        continue;
                    }

                    excluded.Add(char.ToUpperInvariant(c));
                }

                ApplyExclusions(overflow);
                OverflowExclusions = overflow.ToString();
            }
        }

        /// <summary>
        /// Mark the cells whose key is excluded, and report back any exclusion that found no slot.
        /// </summary>
        private void ApplyExclusions(StringBuilder overflow)
        {
            HashSet<char> placed = new HashSet<char>();

            foreach (CellBox cell in cells)
            {
                bool on = cell.Key != '\0' && excluded.Contains(char.ToUpperInvariant(cell.Key));
                cell.Excluded = on;
                if (on)
                {
                    placed.Add(char.ToUpperInvariant(cell.Key));
                }
            }

            if (overflow != null)
            {
                foreach (char c in excluded)
                {
                    if (!placed.Contains(c))
                    {
                        overflow.Append(c);
                    }
                }
            }
        }

        private string CollectLayout()
        {
            StringBuilder sb = new StringBuilder();
            foreach (CellBox cell in cells)
            {
                if (cell.Key != '\0')
                {
                    sb.Append(cell.Key);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Lay the cells out again for the current layout and column count. Called on every change
        /// because the shape depends on both -- one more letter can add a whole row.
        /// </summary>
        private void Rebuild()
        {
            string text = layout ?? string.Empty;

            SuspendLayout();
            foreach (CellBox old in cells)
            {
                Controls.Remove(old);
                old.Dispose();
            }

            cells.Clear();

            int perRow = columns > 0 ? columns : Math.Max(text.Length, 1);

            for (int i = 0; i < text.Length; i++)
            {
                CellBox cell = new CellBox(text[i]);
                cell.Location = new Point(
                    (i % perRow) * (CellSize + CellGap),
                    (i / perRow) * (CellSize + CellGap));
                cell.KeyEdited += CellKeyEdited;
                cell.ExcludeToggled += CellExcludeToggled;
                cell.AdvanceRequested += CellAdvanceRequested;

                cells.Add(cell);
                Controls.Add(cell);
            }

            ApplyExclusions(null);

            int rows = perRow > 0 ? (text.Length + perRow - 1) / perRow : 1;
            Size = new Size(
                Math.Max(1, perRow * (CellSize + CellGap)),
                Math.Max(1, Math.Max(rows, 1) * (CellSize + CellGap)));

            ResumeLayout();
            Invalidate();
        }

        private void CellKeyEdited(object sender, EventArgs e)
        {
            layout = CollectLayout();
            RaiseChanged();
        }

        private void CellExcludeToggled(object sender, EventArgs e)
        {
            CellBox cell = (CellBox)sender;
            if (cell.Key == '\0')
            {
                return;
            }

            char key = char.ToUpperInvariant(cell.Key);
            if (cell.Excluded)
            {
                excluded.Add(key);
            }
            else
            {
                excluded.Remove(key);
            }

            // The same letter can sit in more than one slot; excluding it excludes the key, not the
            // slot, so every cell showing it has to agree.
            ApplyExclusions(null);
            RaiseChanged();
        }

        /// <summary>Typing a key fills the cell and moves on, so a row can be typed straight out.</summary>
        private void CellAdvanceRequested(object sender, EventArgs e)
        {
            int i = cells.IndexOf((CellBox)sender);
            if (i >= 0 && i + 1 < cells.Count)
            {
                cells[i + 1].Focus();
                cells[i + 1].SelectAll();
            }
        }

        private void RaiseChanged()
        {
            EventHandler handler = LayoutChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// One slot: a single character, which can also be struck through to show that the key is
        /// on the exclusion list and so keeps whatever it normally does.
        /// </summary>
        private sealed class CellBox : TextBox
        {
            private bool isExcluded;

            public event EventHandler KeyEdited;
            public event EventHandler ExcludeToggled;
            public event EventHandler AdvanceRequested;

            public CellBox(char key)
            {
                MaxLength = 1;
                TextAlign = HorizontalAlignment.Center;
                BorderStyle = BorderStyle.FixedSingle;
                Size = new Size(CellSize, CellSize);
                Font = new Font("Consolas", 11F, FontStyle.Bold);
                CharacterCasing = CharacterCasing.Upper;

                // Suppress the built in cut/copy/paste menu: right click is the exclude gesture
                // here, and the default menu would open on top of it. Null means "use the default",
                // so it takes an empty menu to actually get rid of it. ContextMenuStrip rather than
                // ContextMenu because the netcore build of this form has no ContextMenu.
                ContextMenuStrip = new ContextMenuStrip();
                Text = key == '\0' ? string.Empty : key.ToString();
                Repaint();
            }

            public char Key
            {
                get { return Text.Length > 0 ? Text[0] : '\0'; }
            }

            public bool Excluded
            {
                get { return isExcluded; }
                set
                {
                    isExcluded = value;
                    Repaint();
                }
            }

            private void Repaint()
            {
                // Struck through and greyed: the key is still shown, because which key it is remains
                // the point -- it just no longer belongs to the grid.
                Font = new Font(Font, isExcluded ? FontStyle.Bold | FontStyle.Strikeout : FontStyle.Bold);
                ForeColor = isExcluded ? Color.FromArgb(128, 128, 128) : Color.Black;
            }

            protected override void OnTextChanged(EventArgs e)
            {
                base.OnTextChanged(e);

                EventHandler edited = KeyEdited;
                if (edited != null)
                {
                    edited(this, EventArgs.Empty);
                }
            }

            /// <summary>
            /// A slot holds exactly one key, so a keypress replaces what is there rather than being
            /// appended. Left to itself a MaxLength of 1 refuses the keystroke on a cell that is
            /// already full and Windows answers with the error ding, which is what typing over a
            /// key sounded like.
            /// </summary>
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                if (char.IsControl(e.KeyChar))
                {
                    base.OnKeyPress(e);
                    return;
                }

                e.Handled = true;

                Text = char.ToUpperInvariant(e.KeyChar).ToString();
                SelectAll();

                EventHandler advance = AdvanceRequested;
                if (advance != null)
                {
                    advance(this, EventArgs.Empty);
                }
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                // Left click has to stay what it is everywhere else -- put the caret in the box --
                // because typing a key is the main thing done here. Excluding is the rarer action,
                // so it takes the right button and leaves editing alone.
                if (e.Button == MouseButtons.Right && Key != '\0')
                {
                    Excluded = !Excluded;

                    EventHandler toggled = ExcludeToggled;
                    if (toggled != null)
                    {
                        toggled(this, EventArgs.Empty);
                    }

                    return;
                }

                base.OnMouseDown(e);
            }
        }
    }
}
