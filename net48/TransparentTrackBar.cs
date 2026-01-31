using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System;

namespace Contra
{
    [DefaultEvent("Scroll")]
    [DefaultProperty("Value")]
    [ToolboxBitmap(typeof(System.Windows.Forms.TrackBar))]
    public class TrackBar : Control
    {
        public TrackBar()
            : base()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint | ControlStyles.Selectable | ControlStyles.UserMouse, true);
            Thumb = new Rectangle();
            LayoutTrackBarParts();
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 60);
            }
        }

        #region Event Delarations

        public event EventHandler Scroll;
        public event EventHandler ValueChanged;

        #endregion

        #region Private Instance Variables

        private Orientation orientation = Orientation.Horizontal;

        private int minimum = 392;
        private int maximum = 1000;
        private int smallChange = 1;
        private int largeChange = 5;
        private int _value = 0;

        private TickStyle tickStyle = TickStyle.BottomRight;
        private int tickFrequency = 1;

        private bool thumbDragging = false;
        private bool scrollUp = false;

        private Rectangle Thumb;
        private bool ThumbFocused;

        private Timer scrollTimer;

        #endregion

        #region Public properties

        [DefaultValue(typeof(Orientation), "Horizontal")]
        public Orientation Orientation
        {
            get { return orientation; }
            set
            {
                if (orientation != value)
                {
                    orientation = value;
                    int w, h;
                    w = Height;
                    h = Width;
                    Size = new Size(w, h);
                    LayoutTrackBarParts();
                    Invalidate();
                }
            }
        }

        [DefaultValue(0)]
        [RefreshProperties(RefreshProperties.All)]
        public int Minimum
        {
            get { return minimum; }
            set
            {
                if (minimum != value)
                {
                    minimum = value;
                    if (maximum <= value)
                        Maximum = value;
                    LayoutTrackBarParts();
                    Invalidate();
                }
            }
        }

        [DefaultValue(10)]
        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (maximum != value)
                {
                    maximum = value;
                    if (minimum >= value)
                        Minimum = value;
                    LayoutTrackBarParts();
                    Invalidate();
                }
            }
        }

        [DefaultValue(1)]
        public int SmallChange
        {
            get { return smallChange; }
            set
            {
                if (smallChange != value)
                    smallChange = value;
            }
        }

        [DefaultValue(5)]
        public int LargeChange
        {
            get { return largeChange; }
            set
            {
                if (largeChange != value)
                    largeChange = value;
            }
        }

        public int Value
        {
            get
            {
                if (_value < minimum)
                    return minimum;
                return _value;
            }
            set
            {
                if (value < minimum)
                    value = minimum;
                if (value > maximum)
                    value = maximum;
                if (value != _value)
                {
                    _value = value;
                    LayoutTrackBarParts();
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeValue()
        {
            return _value != minimum;
        }
        private void ResetValue()
        {
            _value = minimum;
        }

        [DefaultValue(false)]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = value; }
        }

        [DefaultValue("")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text
        {
            get { return ""; }
            set { base.Text = ""; }
        }

        [DefaultValue(typeof(TickStyle), "BottomRight")]
        public TickStyle TickStyle
        {
            get { return tickStyle; }
            set
            {
                if (tickStyle != value)
                {
                    tickStyle = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(1)]
        public int TickFrequency
        {
            get { return tickFrequency; }
            set
            {
                if (tickFrequency != value)
                {
                    tickFrequency = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region private Properties

        private bool Horizontal
        {
            get { return orientation == Orientation.Horizontal; }
        }

        private bool ThumbDragging
        {
            get { return thumbDragging; }
            set
            {
                if (thumbDragging != value)
                {
                    thumbDragging = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region Overridden Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            //Do default painting
            base.OnPaint(e);

            //Draw Tick Marks
            DrawTicks(e.Graphics);

            //Draw Channel
            Rectangle channelBounds = Horizontal ?
                new Rectangle(6, Height / 2 - 2, Width - 16, 4) :
                new Rectangle(Width / 2 - 2, 6, 4, Height - 16);
            ControlPaint.DrawBorder3D(e.Graphics, channelBounds, Border3DStyle.Sunken);

            // Draw the Thumb Object
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(125, 90, 70)))
            {
                if (ThumbDragging)
                {
                    brush.Color = Color.FromArgb(255, 210, 100);
                    Cursor = Cursors.Hand;
                }
                else Cursor = Cursors.Default;
                e.Graphics.FillRectangle(brush, Thumb);
            }

            //Draw Focus
            if (Focused && ShowFocusCues)
                ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            ThumbFocused = Focused && ShowFocusCues;
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            ThumbFocused = Focused && ShowFocusCues;
            Invalidate();
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Left:
                    Value += Horizontal ? -smallChange : smallChange;
                    break;
                case Keys.Down:
                case Keys.Right:
                    Value += Horizontal ? smallChange : -smallChange;
                    break;
                case Keys.PageDown:
                    Value += Horizontal ? largeChange : -largeChange;
                    break;
                case Keys.PageUp:
                    Value += Horizontal ? -largeChange : largeChange;
                    break;
                case Keys.Home:
                    Value = Horizontal ? minimum : maximum;
                    break;
                case Keys.End:
                    Value = Horizontal ? maximum : minimum;
                    break;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            Value += (e.Delta / WHEEL_DELTA) * WHEEL_LINES;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                ThumbDragging = Thumb.Contains(e.Location);

                if (!ThumbDragging)
                {
                    scrollUp = Horizontal ? e.X > Thumb.Right : e.Y < Thumb.Top;
                    if (scrollTimer == null)
                        InitTimer();
                    scrollTimer.Start();
                }

            }

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            ThumbDragging = false;
            if (scrollTimer != null)
                scrollTimer.Stop();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ThumbDragging)
            {
                Value = ValueFromPoint(e.Location);
            }

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
                LayoutTrackBarParts();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            LayoutTrackBarParts();
        }

        #endregion

        #region Protected Methods

        protected virtual void OnScroll(EventArgs e)
        {
            if (Scroll != null)
                Scroll(this, e);
        }

        protected virtual void OnValueChanged(EventArgs eventArgs)
        {
            if (ValueChanged != null)
                ValueChanged(this, eventArgs);
            LayoutTrackBarParts();
            OnScroll(eventArgs);
            Invalidate();
        }

        #endregion

        #region Internal Methods

        private void LayoutTrackBarParts()
        {
            if (Thumb == null)
                return;

            Thumb.Size = Horizontal ?
                new Size(14, 28) :
                new Size(28, 14);

            float channelLength = Horizontal ?
                Width - 26 : // Channel Left margin + Channel Right margin + Thumb.Width
                Height - 26; // Channel Top margin + Channel Bottom margin + Thumb.Height

            float stepCount = (Maximum - Minimum);

            float stepSize = stepCount > 0 ? channelLength / stepCount : 0;

            float thumbOffset = (stepSize) * (Value - minimum);

            Thumb.Location = Horizontal ?
                Point.Round(new PointF(6 + thumbOffset, Height / 2 - 14)) :
                Point.Round(new PointF(Width / 2 - 14, channelLength - thumbOffset + 6));

        }

        private void InitTimer()
        {
            scrollTimer = new Timer();
            scrollTimer.Interval = 500;
            scrollTimer.Tick += new EventHandler(scrollTimer_Tick);
        }

        private void scrollTimer_Tick(object sender, EventArgs e)
        {
            Value += scrollUp ? largeChange : -largeChange;

            if (_value == minimum || _value == maximum)
                scrollTimer.Stop();

            int val = ValueFromPoint(PointToClient(Cursor.Position));

            if (scrollUp && _value > val)
                scrollTimer.Stop();

            if (!scrollUp && _value < val)
                scrollTimer.Stop();
        }

        private int ValueFromPoint(Point point)
        {
            float channelLength = Horizontal ?
                Width - 26 : // Channel Left margin + Channel Right margin + Thumb.Width
                Height - 26; // Channel Top margin + Channel Bottom margin + Thumb.Height

            float stepCount = (maximum - minimum);

            float stepSize = stepCount > 0 ? channelLength / stepCount : 0;

            if (Horizontal)
            {
                point.Offset(-7, 0);
                return (int)(point.X / stepSize) + minimum;
            }
            point.Offset(0, -7);
            return maximum - (int)(point.Y / stepSize) + minimum;
        }

        private void DrawTicks(Graphics graphics)
        {
            if (tickStyle == TickStyle.None)
                return;

            //TODO: Implement Tick Drawing

        }


        #endregion

        private int WHEEL_DELTA = SystemInformation.MouseWheelScrollDelta;
        private int WHEEL_LINES = SystemInformation.MouseWheelScrollLines;

    }
}
