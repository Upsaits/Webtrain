using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;
using System.Diagnostics;

namespace SoftObject.TrainConcept
{
    public delegate void OnUserProgressInfoManagerHandler(object sender, EventArgs ea);

    public class UserProgressInfoManager
    {
        public event OnUserProgressInfoManagerHandler UserProgressInfoManagerEventHandler;
        public event OnUserProgressInfoManagerHandler UserProgressInfoManagerEvent
        {
            add { UserProgressInfoManagerEventHandler += value; }
            remove { UserProgressInfoManagerEventHandler -= value; }
        }

        public enum RegionType { Unknown, Session, Learning, Examing, Testing };
        private string m_strFilePath;
        private bool m_isOpen;
        SQLiteConnection m_dbConnection;

        public UserProgressInfoManager()
        {
            m_isOpen = false;
            m_strFilePath = "";
        }

        public bool Open(string strFilePath)
        {
            if (m_isOpen)
                Close();

            try
            {
                m_strFilePath=strFilePath;
                string strFilename = Path.GetFileName(m_strFilePath);
                if (!File.Exists(m_strFilePath))
                {
                    SQLiteConnection.CreateFile(m_strFilePath);
                    m_dbConnection = new SQLiteConnection(String.Format("Data Source={0};Version=3;new=False;datetimeformat=CurrentCulture", m_strFilePath));
                    m_dbConnection.Open();

                    string sql = "create table userprogressinfos (dateCreated datetime, userName varchar(255), workingName varchar(255), regionId int, regionValue int)";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
                else
                {
                    m_dbConnection = new SQLiteConnection(String.Format("Data Source={0};Version=3;", m_strFilePath));
                    m_dbConnection.Open();
                }

                m_isOpen = true;
                return true;
            }
            catch (Exception ex)
            {
                string strTemp = ex.Message;
                m_isOpen = false;
                return false;
            }
        }

        public void Close()
        {
            if (m_isOpen)
                m_dbConnection.Close();
        }

        private string DateTimeSQLite(DateTime datetime)
        {
            //string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            //return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
            return datetime.ToString("o");
        }

        public void AddUserProgressInfo(string userName, string workingTitle, RegionType tRegion, ushort iRegionVal)
        {
            if (UserProgressInfoManagerEventHandler != null)
                UserProgressInfoManagerEventHandler(this, new EventArgs());
            if (workingTitle.IndexOf("'") >= 0)
            {
                int test1 = 10;
                test1 += 20;
            }

            try
            {
	            string sql = String.Format("insert into userprogressinfos (dateCreated, userName, workingName, regionId, regionValue) values ('{0}','{1}','{2}','{3}', {4})", DateTimeSQLite(DateTime.Now), userName, workingTitle, (int)tRegion, iRegionVal);
	            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
	            command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public int GetUserProgressInfo(string userName,string workingTitle,UserProgressInfoManager.RegionType tRegion)
        {
            int iMaxValue = 0;
            string sql = "select * from userprogressinfos where userName=@user and workingName=@working";
            SQLiteCommand cmd = new SQLiteCommand(sql, m_dbConnection);
            cmd.Parameters.Add(new SQLiteParameter("@user", userName));
            cmd.Parameters.Add(new SQLiteParameter("@working", workingTitle));

            try
            {
	            SQLiteDataReader reader = cmd.ExecuteReader();
	            while (reader.Read())
	            {
                    int iRegId = (int)reader["regionId"];
	                int iRegVal = (int)reader["regionValue"];
	                byte iCmdId = HelperMacros.HIBYTE((ushort)iRegVal);

                    //Console.WriteLine("Name: {0}, region-Id: {1}, regionVal: {2}, CmdId: {3}, iPageId: {4}",userName,iRegId,iRegVal,iCmdId,iPageId);
                    //object dateTime = reader["dateCreated"];
                    //Console.WriteLine("dateCreated:" + dateTime.ToString()) ;

                    if (iRegId == (int)tRegion && iCmdId == 2)
                    {
                        if (tRegion == RegionType.Learning)
                        {
                            byte iPageId = HelperMacros.LOBYTE((ushort)iRegVal);
                            if ((iPageId + 1) > iMaxValue)
                                iMaxValue = (iPageId + 1);
                        }
                        else
                        {
                            byte iQuCnt = HelperMacros.LOBYTE((ushort)iRegVal);
                            if (iQuCnt > iMaxValue)
                                iMaxValue = iQuCnt;
                        }
                    }
	            }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return iMaxValue;
        }

        public TimeSpan GetUserProgressTime(string userName)
        {
            string sql = "select * from userprogressinfos where userName=@user and regionId=@regionId";
            SQLiteCommand cmd = new SQLiteCommand(sql, m_dbConnection);
            cmd.Parameters.Add(new SQLiteParameter("@user", userName));
            cmd.Parameters.Add(new SQLiteParameter("@regionId", 1));
            try
            {
                SQLiteDataReader reader = cmd.ExecuteReader();
                DateTime dtStart = new DateTime(1963,4,4);
                TimeSpan tsComplete = new TimeSpan(0);
                bool bWasEnded = false;
                while (reader.Read())
                {
                    int iRegVal = (int)reader["regionValue"];
                    object oDateTime = reader["dateCreated"];
                    byte iCmdId = HelperMacros.HIBYTE((ushort)iRegVal);
                    byte iPageId = HelperMacros.LOBYTE((ushort)iRegVal);
                    if (iCmdId == 1)
                    {
                        dtStart = DateTime.Parse(oDateTime.ToString());
                        bWasEnded = false;
                    }
                    else if (dtStart!=new DateTime(1963,4,4))
                    {
                        DateTime dtEnd = DateTime.Parse(oDateTime.ToString());
                        TimeSpan tsDiff = dtEnd - dtStart;
                        if (tsDiff.TotalMinutes > 0)
                            tsComplete += tsDiff;
                        bWasEnded = true;
                    }
                }

                if ((dtStart != new DateTime(1963,4,4)) && !bWasEnded)
                {
                    TimeSpan tsDiff = DateTime.Now - dtStart;
                    if (tsDiff.TotalMinutes > 0)
                        tsComplete += tsDiff;
                }
                return tsComplete;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new TimeSpan(0);
        }


        public void DeleteProgressInfoOfUser(string userName)
        {
            string sql = "delete from userprogressinfos where userName=@user";
            SQLiteCommand cmd = new SQLiteCommand(sql, m_dbConnection);
            cmd.Parameters.Add(new SQLiteParameter("@user", userName));
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void DeleteProgressInfoOfMap(string mapTitle)
        {
            string[] aWorkings=null;
            if (Program.AppHandler.MapManager.GetWorkings(mapTitle, ref aWorkings))
                foreach (var w in aWorkings)
                    DeleteProgressInfoOfWork(w);
        }

        public void DeleteProgressInfoOfWork(string work)
        {
            string sql = "delete from userprogressinfos where workingName=@working";
            SQLiteCommand cmd = new SQLiteCommand(sql, m_dbConnection);
            cmd.Parameters.Add(new SQLiteParameter("@working", work));
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}
