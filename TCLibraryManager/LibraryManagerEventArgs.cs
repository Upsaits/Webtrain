using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftObject.TrainConcept.Libraries
{
    public class LibraryManagerEventArgs : EventArgs
    {
        public enum CommandType { Open, Close, Save, Create, Destroy, ChangeManagement};
        private readonly CommandType m_command;
        private readonly string m_libName;

        public CommandType Command
        {
            get {return m_command;}
        }

        public string LibName
        {
            get { return m_libName; }
        }

        public LibraryManagerEventArgs(CommandType command, string libName)
        {
            m_command = command;
            m_libName = libName;
        }

    }
}
