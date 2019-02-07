using System.Drawing;
using System.Windows.Forms;

namespace Contra
{
    public partial class Marquee : Label
    {

            public Timer MarqueeTimer { get; set; }
            public int Speed { get; set; }
            public int yOffset { get; set; }

            public void Start() { MarqueeTimer.Start(); }
            public void Stop() { MarqueeTimer.Stop(); }

            private int offset;
            SolidBrush backBrush;
            SolidBrush textBrush;

            public Marquee()
            {
                textBrush = new SolidBrush(Color.White);
                backBrush = new SolidBrush(Color.FromArgb(10,5,0));
                yOffset = 0;
                Speed = 1;
                MarqueeTimer = new Timer();
                MarqueeTimer.Interval = 25;
                MarqueeTimer.Enabled = true;
                MarqueeTimer.Tick += (aSender, eArgs) =>
                {
                    offset = (offset - Speed);
                    if (offset < -this.ClientSize.Width) offset = 0;
                    this.Invalidate();
                };
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.FillRectangle(backBrush, e.ClipRectangle);
                e.Graphics.DrawString(this.Text, this.Font, textBrush, offset, yOffset);
                e.Graphics.DrawString(this.Text, this.Font, textBrush,
                                      this.ClientSize.Width + offset, yOffset);
            }
    }
}
