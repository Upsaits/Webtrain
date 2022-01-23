using System.Windows.Forms;

namespace SoftObject.TrainConcept.Adapter
{
    class LearnmapAdapterImpl : SoftObject.TrainConcept.Libraries.ILearnmapAdapter
    {
        public void ShowMessageBox(string section, string key, string defKey)
        {
            string txt = Program.AppHandler.LanguageHandler.GetText(section, key, defKey);
            string cap = Program.AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
            MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public int GetKeywords(string path, ref SoftObject.TrainConcept.Libraries.KeywordCollection aKeywords)
        {
            return Program.AppHandler.LibManager.GetKeywords(path,ref aKeywords);
        }
    }
}
