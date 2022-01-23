using System.Windows.Forms;

namespace SoftObject.TrainConcept.Adapter
{
    class UserAdapterImpl : SoftObject.TrainConcept.Libraries.IUserAdapter
    {
        public void ShowMessageBox(string section, string key, string defKey)
        {
            string txt = Program.AppHandler.LanguageHandler.GetText(section, key, defKey);
            string cap = Program.AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
            MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
