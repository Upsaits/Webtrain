using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftObject.TrainConcept.Libraries
{
    public interface IUserAdapter
    {
        void ShowMessageBox(string section, string key, string defKey);
    }
}
