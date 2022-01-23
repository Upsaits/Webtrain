using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;


namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmNewLearnmap.
	/// </summary>
    public partial class FrmNewLearnmap : XtraForm
	{
		private string title;
		private ResourceHandler rh=null;
        private AppHandler AppHandler = Program.AppHandler;

		public string Title
		{
			get
			{
				return title;
			}
		}

		public FrmNewLearnmap()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			InitializeComponent();

			this.Icon = rh.GetIcon("main");

			this.btnOk.Text = AppHandler.LanguageHandler.GetText("FORMS","Ok","OK");
			this.btnCancel.Text = AppHandler.LanguageHandler.GetText("FORMS","Cancel","Abbrechen");
			this.label1.Text = AppHandler.LanguageHandler.GetText("FORMS","Title","Titel")+':';
			this.Text = AppHandler.LanguageHandler.GetText("FORMS","New_map","Neue Lernmappe");

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

			if (AppHandler.MapManager.GetId(title)>=0)
			{
				string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Map_already_exists","Mappe existiert bereits!");
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
