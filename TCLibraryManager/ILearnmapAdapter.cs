using System;
using System.Collections.Generic;
using System.Text;

namespace SoftObject.TrainConcept.Libraries
{
    public interface ILearnmapAdapter
    {
        void ShowMessageBox(string section, string key, string defKey);
        int GetKeywords(string path, ref KeywordCollection aKeywords);
    }
}
