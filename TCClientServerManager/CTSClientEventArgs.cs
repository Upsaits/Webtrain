using System;

namespace SoftObject.TrainConcept.ClientServer
{
    public class CTSClientEventArgs : EventArgs
    {
        public enum CommandType { ConnectionInfo, Login, Logout, TransferStart, TransferProgress, TransferEnd, ReceiveClassMesssage, TestPermission, MapProgress, WorkProgress, PasswordChanged };
        private readonly CommandType m_command;
        private readonly string m_userName;
        private readonly string m_mapName;
        private readonly string m_workName;
        private readonly string m_retValues;
        private readonly int m_retValue;
        private readonly CTSClientTransferData m_transferData;
        private readonly int m_roomId;
        private readonly string m_message;
        private readonly bool m_bIsOn;
        private readonly string m_strTransferTypeName;
        private readonly string m_strTransferName;

        public CommandType Command
        {
            get
            {
                return m_command;
            }
        }

        public string UserName
        {
            get
            {
                return m_userName;
            }
        }

        public string MapName
        {
            get
            {
                return m_mapName;
            }
        }

        public string WorkName
        {
            get
            {
                return m_workName;
            }
        }

        public int ReturnValue
        {
            get
            {
                return m_retValue;
            }
        }

        public string ReturnValues
        {
            get
            {
                return m_retValues;
            }
        }

        public int RoomId
        {
            get
            {
                return m_roomId;
            }
        }

        public string Message
        {
            get
            {
                return m_message;
            }
        }

        public bool IsOn
        {
            get 
            {
                return m_bIsOn;
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

        public CTSClientEventArgs(CommandType command, string userName, int retValue)
        {
            m_command = command;
            m_userName = userName;
            m_retValue = retValue;
            m_mapName = "";
            m_transferData = null;
            m_roomId=-1;
            m_message="";
            m_bIsOn=false;
            m_strTransferTypeName="";
            m_strTransferName="";
        }

        public CTSClientEventArgs(CommandType command, string userName, string mapName, int retValue)
        {
            m_command = command;
            m_userName = userName;
            m_mapName = mapName;
            m_retValue = retValue;
            m_transferData = null;
            m_roomId = -1;
            m_message = "";
            m_bIsOn = false;
            m_strTransferTypeName = "";
            m_strTransferName = "";
        }

        public CTSClientEventArgs(CommandType command, string userName, string mapName, string workName, int retValue)
        {
            m_command = command;
            m_userName = userName;
            m_mapName = mapName;
            m_workName = workName;
            m_retValue = retValue;
            m_transferData = null;
            m_roomId = -1;
            m_message = "";
            m_bIsOn = false;
            m_strTransferTypeName = "";
            m_strTransferName = "";
        }

        public CTSClientEventArgs(CommandType command, string userName, string mapName, string workName, string retValues)
        {
            m_command = command;
            m_userName = userName;
            m_mapName = mapName;
            m_workName = workName;
            m_retValues = retValues;
            m_transferData = null;
            m_roomId = -1;
            m_message = "";
            m_bIsOn = false;
            m_strTransferTypeName = "";
            m_strTransferName = "";
        }

        public CTSClientEventArgs(CommandType command,string strTransferTypeName,string strTransferName, CTSClientTransferData transferData)
        {
            m_command = command;
            m_transferData = transferData;
            m_mapName = "";
            m_retValue = -1;
            m_userName = "";
            m_roomId = -1;
            m_message = "";
            m_bIsOn = false;
            m_strTransferTypeName = strTransferTypeName;
            m_strTransferName = strTransferName;
        }

        public CTSClientEventArgs(CommandType command, string userName, int roomId, string message)
        {
            m_command = command;
            m_userName = userName;
            m_roomId = roomId;
            m_message = message;
            m_command = command;
            m_mapName = "";
            m_retValue = -1;
            m_transferData = null;
            m_bIsOn = false;
            m_strTransferTypeName = "";
            m_strTransferName = "";
        }

        public CTSClientEventArgs(CommandType command, bool bIsOn)
        {
            m_command = command;
            m_userName = "";
            m_roomId = -1;
            m_message = "";
            m_mapName = "";
            m_retValue = -1;
            m_transferData = null;
            m_bIsOn = bIsOn;
            m_strTransferTypeName = "";
            m_strTransferName = "";
        }

    }
}
