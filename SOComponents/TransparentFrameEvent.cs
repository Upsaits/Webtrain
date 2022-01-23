using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftObject.SOComponents
{
    public class TransparentFrameEventArgs : EventArgs
    {
        public System.Drawing.Point DeltaPos { get; set; }
        public System.Drawing.Size DeltaSize { get; set; }
        public object Context { get; set; }

        public TransparentFrameEventArgs(Point newPos , object context)
        {
            Context = context;
            DeltaPos = newPos;
            DeltaSize = new Size(0, 0);
        }

        public TransparentFrameEventArgs(Size newSize, object context)
        {
            Context = context;
            DeltaSize = newSize;
            DeltaPos = new Point(0, 0);
        }
    };

    public delegate void TransparentFrameEventDelegate(object sender, ref TransparentFrameEventArgs ea);
}
