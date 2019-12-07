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
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint | ControlStyles.Selectable | ControlStyles.UserMouse, true);
            this.Thumb = new Rectangle();
            this.LayoutTrackBarParts();
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
            get { return this.orientation; }
            set
            {
                if (this.orientation != value)
                {
                    this.orientation = value;
                    int w, h;
                    w = this.Height;
                    h = this.Width;
                    this.Size = new Size(w, h);
                    this.LayoutTrackBarParts();
                    this.Invalidate();
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
                    this.LayoutTrackBarParts();
                    this.Invalidate();
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
                    this.LayoutTrackBarParts();
                    this.Invalidate();
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
                if (_value < this.minimum)
                    return this.minimum;
                return _value;
            }
            set
            {
                if (value < this.minimum)
                    value = this.minimum;
                if (value > this.maximum)
                    value = this.maximum;
                if (value != _value)
                {
                    _value = value;
                    this.LayoutTrackBarParts();
                    this.OnValueChanged(EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeValue()
        {
            return this._value != this.minimum;
        }
        private void ResetValue()
        {
            this._value = this.minimum;
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
            get { return this.tickStyle; }
            set
            {
                if (this.tickStyle != value)
                {
                    this.tickStyle = value;
                    this.Invalidate();
                }
            }
        }

        [DefaultValue(1)]
        public int TickFrequency
        {
            get { return this.tickFrequency; }
            set
            {
                if (this.tickFrequency != value)
                {
                    this.tickFrequency = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region private Properties

        private bool Horizontal
        {
            get { return this.orientation == Orientation.Horizontal; }
        }

        private bool ThumbDragging
        {
            get { return thumbDragging; }
            set
            {
                if (thumbDragging != value)
                {
                    thumbDragging = value;
                    this.Invalidate();
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
            Rectangle channelBounds = this.Horizontal ?
                new Rectangle(6, this.Height / 2 - 2, this.Width - 16, 4) :
                new Rectangle(this.Width / 2 - 2, 6, 4, this.Height - 16);
            ControlPaint.DrawBorder3D(e.Graphics, channelBounds, Border3DStyle.Sunken);

            // Draw the Thumb Object
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(125, 90, 70)))
            {
                //if (ThumbFocused)
                //    brush.Color = Color.Green;
                if (ThumbDragging)
                    brush.Color = (Color.FromArgb(255, 210, 100));
                e.Graphics.FillRectangle(brush, this.Thumb);
            }

            //Draw Focus
            if (this.Focused && this.ShowFocusCues)
                ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);

        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            ThumbFocused = (this.Focused && this.ShowFocusCues);
            this.Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            ThumbFocused = (this.Focused && this.ShowFocusCues);
            this.Invalidate();
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
                    this.Value += this.Horizontal ? -this.smallChange : this.smallChange;
                    break;
                case Keys.Down:
                case Keys.Right:
                    this.Value += this.Horizontal ? this.smallChange : -this.smallChange;
                    break;
                case Keys.PageDown:
                    this.Value += this.Horizontal ? this.largeChange : -this.largeChange;
                    break;
                case Keys.PageUp:
                    this.Value += this.Horizontal ? -this.largeChange : this.largeChange;
                    break;
                case Keys.Home:
                    this.Value = this.Horizontal ? this.minimum : this.maximum;
                    break;
                case Keys.End:
                    this.Value = this.Horizontal ? this.maximum : this.minimum;
                    break;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            this.Value += (e.Delta / WHEEL_DELTA) * WHEEL_LINES;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                ThumbDragging = Thumb.Contains(e.Location);

                if (!ThumbDragging)
                {
                    scrollUp = this.Horizontal ? e.X > Thumb.Right : e.Y < Thumb.Top;
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
            if (this.scrollTimer != null)
                this.scrollTimer.Stop();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ThumbDragging)
            {
                this.Value = ValueFromPoint(e.Location);
            }

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
                this.LayoutTrackBarParts();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.LayoutTrackBarParts();
        }

        #endregion

        #region Protected Methods

        protected virtual void OnScroll(EventArgs e)
        {
            if (this.Scroll != null)
                this.Scroll(this, e);
        }

        protected virtual void OnValueChanged(EventArgs eventArgs)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, eventArgs);
            this.LayoutTrackBarParts();
            this.OnScroll(eventArgs);
            this.Invalidate();
        }

        #endregion

        #region Internal Methods

        private void LayoutTrackBarParts()
        {
            if (Thumb == null)
                return;

            Thumb.Size = this.Horizontal ?
                new Size(14, 28) :
                new Size(28, 14);

            float channelLength = this.Horizontal ?
                this.Width - 26 : // Channel Left margin + Channel Right margin + Thumb.Width
                this.Height - 26; // Channel Top margin + Channel Bottom margin + Thumb.Height

            float stepCount = (this.Maximum - this.Minimum);

            float stepSize = stepCount > 0 ? channelLength / stepCount : 0;

            float thumbOffset = (stepSize) * (this.Value - this.minimum);

            Thumb.Location = this.Horizontal ?
                Point.Round(new PointF(6 + thumbOffset, this.Height / 2 - 14)) :
                Point.Round(new PointF(this.Width / 2 - 14, channelLength - thumbOffset + 6));

        }

        private void InitTimer()
        {
            this.scrollTimer = new Timer();
            this.scrollTimer.Interval = 500;
            this.scrollTimer.Tick += new EventHandler(scrollTimer_Tick);
        }

        private void scrollTimer_Tick(object sender, EventArgs e)
        {
            this.Value += this.scrollUp ? this.largeChange : -this.largeChange;

            if (this._value == this.minimum || this._value == this.maximum)
                this.scrollTimer.Stop();

            int val = this.ValueFromPoint(this.PointToClient(Cursor.Position));

            if (this.scrollUp && this._value > val)
                this.scrollTimer.Stop();

            if (!this.scrollUp && this._value < val)
                this.scrollTimer.Stop();
        }

        private int ValueFromPoint(Point point)
        {
            float channelLength = this.Horizontal ?
                this.Width - 26 : // Channel Left margin + Channel Right margin + Thumb.Width
                this.Height - 26; // Channel Top margin + Channel Bottom margin + Thumb.Height

            float stepCount = (this.maximum - this.minimum);

            float stepSize = stepCount > 0 ? channelLength / stepCount : 0;

            if (this.Horizontal)
            {
                point.Offset(-7, 0);
                return (int)(point.X / stepSize) + this.minimum;
            }
            point.Offset(0, -7);
            return this.maximum - (int)(point.Y / stepSize) + this.minimum;
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
