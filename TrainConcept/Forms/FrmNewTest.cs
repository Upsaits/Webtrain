using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Libraries;


namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmNewLearnmap.
	/// </summary>
    public partial class FrmNewTest: XtraForm
	{
		private string title;
		private ResourceHandler rh=null;
	    private string strMapTitle="";
        private AppHandler AppHandler = Program.AppHandler;
		public string Title
		{
			get
			{
				return title;
			}
		}

        public FrmNewTest()
        {
            //
            // Erforderlich für die Windows Form-Designerunterstützung
            //
            InitializeComponent();
        }

        public FrmNewTest(string strMapTitle)
        {
            this.strMapTitle = strMapTitle;
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);

			InitializeComponent();

			this.Icon = rh.GetIcon("main");

			this.btnOk.Text = AppHandler.LanguageHandler.GetText("FORMS","Ok","OK");
			this.btnCancel.Text = AppHandler.LanguageHandler.GetText("FORMS","Cancel","Abbrechen");
			this.label1.Text = AppHandler.LanguageHandler.GetText("FORMS","Title","Titel")+':';
            this.Text = AppHandler.LanguageHandler.GetText("FORMS", "New_Testtitle", "Neuer Test");

			edtTitle.Select();
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			title = edtTitle.Text;
			if (title.Length==0)
			{
				string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Please_enter_title","Bitte Titel angeben!");
				string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
				MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Error);
				DialogResult = DialogResult.None;
			}

            TestItem ti;
            int i = 0;
            do
            {
                ti = AppHandler.MapManager.GetTest(strMapTitle, i);
                if (ti != null && ti.title == title)
                    break;
                ++i;
            } while (ti != null);

			if (ti!=null || title=="Endtest")
			{
				string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Test_already_exists","Test existiert bereits!");
				string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
				MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Error);
				DialogResult = DialogResult.None;
			}

			//DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
