using System;
using System.Drawing;
using System.Windows.Forms;

namespace SoftObject.SOComponents.Controls
{
    public partial class TransparentFrameControl : UserControl
    {
        public event TransparentFrameEventDelegate FrameChanged;

        private Point startPos = new Point();
        private object context = null;

        public bool isDrag = false;
        public bool enab = false;
        private int opacity = 100;

        private int alpha;
        public TransparentFrameControl()
        {
            InitializeComponent();
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //SetStyle(ControlStyles.Opaque, true);
        }

        public void Initialize(int left, int top, int width, int height, object context)
        {
            if (left>0 && Right>0)
                Location = new Point(left,top);
            if (width>0 && height>0)
                Size = new Size(width, height);
            this.context = context;
        }

        public int Opacity
        {
            get
            {
                if (opacity > 100)
                {
                    opacity = 100;
                }
                else if (opacity < 1)
                {
                    opacity = 1;
                }
                return this.opacity;
            }
            set
            {
                this.opacity = value;
                if (this.Parent != null)
                {
                    Parent.Invalidate(this.Bounds, true);
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }

        public void MoveByDelta(int left, int top)
        {
            var newPos = new Point(Location.X + left, Location.Y + top);
            this.Location = newPos;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            Graphics g = e.Graphics;
            Rectangle bounds = e.ClipRectangle;
            bounds.Inflate(-4,-4);

            try
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                        Color.Black, 4, ButtonBorderStyle.Solid,
                                        Color.Black, 4, ButtonBorderStyle.Solid,
                                        Color.Black, 4, ButtonBorderStyle.Solid,
                                        Color.Black, 4, ButtonBorderStyle.Solid);
            }
            catch (System.Exception /*ex*/)
            {

            }

            Color frmColor = this.Parent.BackColor;
            Brush bckColor = default(Brush);

            alpha = (opacity * 255) / 100;

            if (isDrag)
            {
                Color dragBckColor = default(Color);

                if (BackColor != Color.Transparent)
                {
                    int Rb = BackColor.R * alpha / 255 + frmColor.R * (255 - alpha) / 255;
                    int Gb = BackColor.G * alpha / 255 + frmColor.G * (255 - alpha) / 255;
                    int Bb = BackColor.B * alpha / 255 + frmColor.B * (255 - alpha) / 255;
                    dragBckColor = Color.FromArgb(Rb, Gb, Bb);
                }
                else
                {
                    dragBckColor = frmColor;
                }

                alpha = 255;
                bckColor = new SolidBrush(Color.FromArgb(alpha, dragBckColor));
            }
            else
            {
                bckColor = new SolidBrush(Color.FromArgb(alpha, this.BackColor));
            }

            if (this.BackColor != Color.Transparent | isDrag)
            {
                g.FillRectangle(bckColor, bounds);
            }

            bckColor.Dispose();
            g.Dispose();

            //base.OnPaint(e); 
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (this.Parent != null)
            {
                Parent.Invalidate(this.Bounds, true);
            }
            base.OnBackColorChanged(e);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnParentBackColorChanged(e);
        }

        private void TransparentFrameControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrag = true;
                startPos = Location;
            }
        }

        private void TransparentFrameControl_MouseUp(object sender, MouseEventArgs e)
        {
            // If the MouseUp event occurs, the user is not dragging.
            isDrag = false;
            var args = new TransparentFrameEventArgs(new Point(Location.X - startPos.X, Location.Y - startPos.Y), context);
            if (FrameChanged!=null)
                FrameChanged(this, ref args);
        }

        private void TransparentFrameControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
                this.Location = new Point(Location.X + e.X, Location.Y + e.Y);
        }
    }
}
