using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ButtonPanel;
using DevExpress.XtraTab;
using DevExpress.XtraTab.Drawing;
using DevExpress.XtraTab.Registrator;
using DevExpress.XtraTab.ViewInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftObject.TrainConcept
{
    public class CustomFlatViewInfoRegistrator : FlatViewInfoRegistrator
    {
        public override string ViewName { get { return "MyFlat"; } }

        public override BaseTabPainter CreatePainter(IXtraTab tabControl)
        {
            return new CustomFlatTabPainter(tabControl);
        }
    }

    class CustomFlatTabPainter : FlatTabPainter
    {
        public CustomFlatTabPainter(IXtraTab tabControl) : base(tabControl)
        {

        }

        protected virtual Rectangle CalcNewBounds(TabDrawArgs e)
        {
            BaseTabHeaderViewInfo headerInfo = e.ViewInfo.HeaderInfo;
            int newWidth = 0;
            foreach (BaseTabPageViewInfo page in headerInfo.VisiblePages)
                newWidth += page.Bounds.Width;
            var newBounds = new Rectangle(headerInfo.Client.Location, new Size(newWidth, headerInfo.Client.Height));
            return newBounds;
        }

        protected override void DrawHeaderBackground(TabDrawArgs e)
        {
            e.ViewInfo.HeaderBorderPainter.DrawObject(new TabBorderObjectInfoArgs(e.ViewInfo, e.Cache, e.ViewInfo.HeaderInfo.PaintAppearance, e.ViewInfo.HeaderInfo.Bounds));
            BaseTabHeaderViewInfo headerInfo = e.ViewInfo.HeaderInfo;
            var newBounds = CalcNewBounds(e);
            headerInfo.PaintAppearance.FillRectangle(e.Cache, newBounds);
        }

        protected override void DrawHeaderRowBackground(TabDrawArgs e, BaseTabRowViewInfo rowInfo)
        {
            var newBounds = CalcNewBounds(e);
            e.ViewInfo.HeaderRowBorderPainter.DrawObject(new TabBorderObjectInfoArgs(e.ViewInfo, e.Cache, e.ViewInfo.HeaderInfo.PaintAppearance, newBounds));
        }
    }
}
