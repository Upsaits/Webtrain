using System;
using System.Collections.Generic;
using System.Text;

namespace SoftObject.TrainConcept.Libraries
{
    public interface INoticeAdapter
    {
        string GetText(string section, string key, string defKey);
        string GetWorkingFolder();
    }
}
