using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftObject.TrainConcept.Libraries
{
    public class UserManagerEventArgs : EventArgs
    {
        public enum CommandType { Open, Close, Save, Create, Destroy, ChangeManagement, Update};
        private CommandType m_command;
        private string m_userName;

        public CommandType Command
        {
            get {return m_command;}
        }

        public string UserName
        {
            get { return m_userName; }
        }

        public UserManagerEventArgs(CommandType command, string userName)
        {
            m_command = command;
            m_userName = userName;
        }

    }
}
