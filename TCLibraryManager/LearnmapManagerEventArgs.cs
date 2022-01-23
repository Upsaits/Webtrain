using System;

namespace SoftObject.TrainConcept.Libraries
{
    public class LearnmapManagerEventArgs : EventArgs
    {
        public enum CommandType { Open, Close, Save, Create, Destroy, AddWorking, DeleteWorking, AddUser, DeleteUser, ChangeUsage };
        private CommandType m_command;
        private string m_mapName;
        private int m_retValue;

        public CommandType Command
        {
            get
            {
                return m_command;
            }
        }

        public string MapName
        {
            get { return m_mapName; }
        }

        public int ReturnValue
        {
            get
            {
                return m_retValue;
            }
        }

        public LearnmapManagerEventArgs(CommandType command, string mapName, int retValue)
        {
            m_command = command;
            m_mapName = mapName;
            m_retValue = retValue;
        }
    }
}
