using DevExpress.Web;
using DevExpress.Web.ASPxThemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TCWebUpdate
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RichEdit.CreateDefaultRibbonTabs(true);
            RichEdit.RibbonTabs[0].Groups[0].Items.Clear();

            for (int i = 1; i < RichEdit.RibbonTabs.Count; i++)
                RichEdit.RibbonTabs[i].Visible = false;

            var saveItem = new DevExpress.Web.RibbonButtonItem("Save");
            saveItem.LargeImage.IconID = IconID.SaveSave32x32;
            saveItem.Size = RibbonItemSize.Large;
            RichEdit.RibbonTabs[0].Groups[0].Items.Add(saveItem);

            string filename = MapPath("~/docs") + "/sozialanamnese.doc";
            RichEdit.Open(filename);
        }
    }
}