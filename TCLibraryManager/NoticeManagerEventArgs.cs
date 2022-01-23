using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftObject.TrainConcept.Libraries
{
    public class NoticeManagerEventArgs : EventArgs
    {
        public enum CommandType { Open, Close, Save, Create, Destroy};
        private CommandType m_command;

        public CommandType Command
        {
            get {return m_command;}
        }

        public NoticeManagerEventArgs(CommandType command)
        {
            m_command = command;
        }
    }
}
