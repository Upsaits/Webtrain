using System.Drawing;
using System.Windows.Forms;

namespace SoftObject.SOComponents.Forms
{
    public partial class FrmTransparentFrame : Form
    {
        public event TransparentFrameEventDelegate FrameChanged;

        private bool isDrag = false;
        private Point startPos = new Point();
        private object context = null;

        public FrmTransparentFrame()
        {
            InitializeComponent();
        }

        public FrmTransparentFrame(int left, int top, int width, int height, object context)
        {
            InitializeComponent();
            Location = new Point(left,top);
            Size = new Size(width, height);
            this.context = context;
        }

        public void MoveByDelta(int left, int top)
        {
            var newPos = new Point(Location.X+left,Location.Y+top);
            this.Location = newPos;
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, 
                                    Color.Black, 4, ButtonBorderStyle.Solid,
                                    Color.Black, 4, ButtonBorderStyle.Solid,
                                    Color.Black, 4, ButtonBorderStyle.Solid,
                                    Color.Black, 4, ButtonBorderStyle.Solid);
        }

        private void FrmTransparentFrame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrag = true;
                startPos = Location;
            }
        }

        private void FrmTransparentFrame_MouseUp(object sender, MouseEventArgs e)
        {
            // If the MouseUp event occurs, the user is not dragging.
            isDrag = false;
            var args=new TransparentFrameEventArgs(new Point(Location.X-startPos.X,Location.Y-startPos.Y),context);
            FrameChanged(this,ref args);
        }

        private void FrmTransparentFrame_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
                this.Location = new Point(Location.X + e.X, Location.Y + e.Y);
        }
    }
}
