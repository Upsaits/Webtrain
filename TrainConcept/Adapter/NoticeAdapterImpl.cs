namespace SoftObject.TrainConcept.Adapter
{
    class NoticeAdapterImpl : SoftObject.TrainConcept.Libraries.INoticeAdapter
    {
        public string GetText(string section, string key, string defKey)
        {
            return Program.AppHandler.LanguageHandler.GetText("FORMS", "Notice", "Notiz");
        }

        public string GetWorkingFolder()
        {
            return Program.AppHandler.WorkingFolder;
        }
    }
}
