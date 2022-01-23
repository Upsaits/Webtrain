using System;

namespace SoftObject.TrainConcept.ClientServer
{
    public class CTSServerEventArgs : EventArgs
    {
        public enum CommandType { ConnectionInfo, Login, Logout, Message, TransferStart, TransferProgress, TransferEnd };
        private CommandType m_command;
        private string m_target;
        private string m_userName;
        private string m_password;
        private int m_retValue;
        private bool m_bIson;
        private string m_language;
        private string m_strMessage;
        private readonly string m_strTransferTypeName;
        private readonly string m_strTransferName;

        private CTSClientTransferData m_transferData;

        public CommandType Command
        {
            get
            {
                return m_command;
            }
        }

        public string Target
        {
            get
            {
                return m_target;
            }
        }

        public string UserName
        {
            get
            {
                return m_userName;
            }
        }

        public string Password
        {
            get
            {
                return m_password;
            }
        }

        public int ReturnValue
        {
            get
            {
                return m_retValue;
            }
            set
            {
                m_retValue = value;
            }
        }

        public string Language
        {
            get
            {
                return m_language;
            }
        }

        public string Message
        {
            get
            {
                return m_strMessage;
            }
        }

        public bool IsOn
        {
            get
            {
                return m_bIson;
            }
        }

        public string TransferTypeName
        {
            get { return m_strTransferTypeName; }
        }

        public string TransferName
        {
            get { return m_strTransferName; }
        }

        public CTSClientTransferData TransferData
        {
            get 
            { 
                return m_transferData; 
            }
        }

        public CTSServerEventArgs(CommandType command, string target, string userName, string password="",string language="")
        {
            m_command = command;
            m_target = target;
            m_userName = userName;
            m_password = password;
            m_language = language;
            m_strMessage = "";
            m_strTransferTypeName = "";
            m_strTransferName = "";
        }

        public CTSServerEventArgs(CommandType command, string target,bool bIsOn)
        {
            m_command = command;
            m_target = target;
            m_userName = "";
            m_password = "";
            m_language = "";
            m_strMessage = "";
            m_strTransferTypeName = "";
            m_strTransferName = "";
        }

        public CTSServerEventArgs(CommandType command, string target, string  strMessage)
        {
            m_command = command;
            m_target = target;
            m_userName = "";
            m_password = "";
            m_language = "";
            m_bIson = false;
            m_strMessage = strMessage;
            m_strTransferTypeName = "";
            m_strTransferName = "";
        }

        public CTSServerEventArgs(CommandType command,string strTransferTypeName,string strTransferName,CTSClientTransferData transferData)
        {
            m_command = command;
            m_target = "";
            m_userName = "";
            m_password = "";
            m_language = "";
            m_bIson = false;
            m_strMessage = "";
            m_transferData = transferData;
            m_strTransferTypeName = strTransferTypeName;
            m_strTransferName = strTransferName;
        }
    }
}
