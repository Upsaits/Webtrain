using System;

namespace SoftObject.TrainConcept.Libraries
{
    public class ClassroomManagerEventArgs : EventArgs
    {
        public enum CommandType { Open, Close, Save, Create, Destroy, AddLearnmap, RemoveLearnmap, AddUser, RemoveUser,ChangeUsage};
        private CommandType m_command;
        private string m_className;
        private int m_retValue;

        public CommandType Command
        {
            get
            {
                return m_command;
            }
        }

        public string ClassName
        {
            get { return m_className; }
        }

        public int ReturnValue
        {
            get
            {
                return m_retValue;
            }
        }


        public ClassroomManagerEventArgs(CommandType command, string className, int retValue)
        {
            m_command = command;
            m_className = className;
            m_retValue = retValue;
        }
    }
}
