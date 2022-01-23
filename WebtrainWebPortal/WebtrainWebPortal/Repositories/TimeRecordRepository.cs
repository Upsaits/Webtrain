using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DevExpress.Web;
using DevExpress.XtraSpreadsheet.Model;
using MySqlConnector;
using Wisej.Web;

namespace WebtrainWebPortal.Repositories
{
    public class TimeRecordRepository
    {
        private static readonly Lazy<TimeRecordRepository> lazy =
            new Lazy<TimeRecordRepository>(() => new TimeRecordRepository());
        public static TimeRecordRepository Instance => lazy.Value;

        private static UserRepository userRepo = UserRepository.Instance;

        public MySqlConnection Connection
        {
            get => Application.Session.workflowMediator.Connection;
        }

        private TimeRecordRepository()
        {

        }

        public DataTable GetAllTimeRecords()
        {
            var mySqlDataAdapter = new MySqlDataAdapter("select * from wttimerecord", Connection);
            var ds = new DataSet();
            mySqlDataAdapter.Fill(ds);
            return ds.Tables[0];
        }



        public DataTable GetAllTimeRecordsByUserEmail(string strEmail)
        {
            userRepo.GetUserIdByEmail(strEmail, out var iUserId);

            Connection.Open();
            string strSQL =
                "select CAST(begin AS DATE) as 'Datum', CAST(begin AS TIME) as 'Start', CAST(end AS TIME) as 'Ende', absenceReason as 'Abwesenheit', absenceConfirmation, absenceCompleted, absenceFlag, id " +
                "from wttimerecord " +
                "where userId=@UserId";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.Parameters.AddWithValue("@UserId", iUserId);

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            Connection.Close();
                            return dt;
                        }
                    }
                }
            }
            catch (MySqlException sqlEx)
            {
#if USE_LOCALHOST
                MessageBox.Show(sqlEx.Message);
#endif
            }

            Connection.Close();
            return null;
        }



        public DataTable GetAllSicknessTimeRecordsByUserEmail(string strEmail)
        {
            int iUserId = -1;
            userRepo.GetUserIdByEmail(strEmail, out iUserId);
            if (iUserId >= 0)
            {
                Connection.Open();
                string strSQL = "select * " +
                                "from wttimerecord " +
                                "where userId=@UserId " +
                                "and absenceReason like '%Sickness%' and absenceCompleted=0";

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(strSQL))
                    {
                        cmd.Connection = Connection;
                        cmd.Parameters.AddWithValue("@UserId", iUserId);

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            using (var dt = new DataTable())
                            {
                                sda.Fill(dt);
                                Connection.Close();
                                return dt;
                            }

                        }
                    }
                }
                catch (MySqlException sqlEx)
                {
#if USE_LOCALHOST
                    MessageBox.Show(sqlEx.Message);
#endif
                }

            }

            Connection.Close();
            return null;
        }

        public DataTable GetAllPresentStudents(DateTime dtDate)
        {
            Connection.Open();
            //string strSQL = "select CAST(t.begin AS DATE) as 'Datum', s.surname as 'Vorname',s.middlename as 'Mittelname',s.lastname as 'Nachname'," +
            //                 "s.username as 'Benutzername', s.className as 'Klasse' from wtuser " +
            //                 "s join wttimerecord t on t.userId = s.id and CAST(begin AS DATE)=CAST(@Date AS DATE)";


            string strSQL = "UPDATE wtuser SET state=0" + ';' + "DROP TABLE IF EXISTS temp" + ';' +
                            "create temporary table temp as (select s.id, CAST(t.begin AS DATE) as 'Datum', t.begin as 'Start', t.end as 'Ende', s.surname as 'Vorname'," +
                            "s.middlename as 'Mittelname',s.lastname as 'Nachname', s.username as 'Benutzername',s.className as 'Klasse',s.state, t.absenceReason as 'Abwesenheit', t.absenceConfirmation, t.absenceCompleted as 'Gesundmeldung' " +
                            "from wtuser s join wttimerecord t on t.userId = s.id and CAST(begin AS DATE)=CAST(@Date AS DATE))" +
                            ';' +
                            "UPDATE wtuser SET state = 1 WHERE id in (SELECT id from temp  where Ende is null)" + ';' +
                            "UPDATE wtuser SET state = 2 WHERE id in (SELECT id from temp where (Abwesenheit='SicknessRange' or Abwesenheit='SicknessDaily') and Gesundmeldung=0)" + ';' +
                            "select surname as 'Vorname', middlename as '2.Vorname',lastname as 'Nachname', username as 'Benutzername', className as 'Klasse', state as 'Status' " +
                            "from wtuser where locationId=1 and activated=1 and not className='LAPTrainer'";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.Parameters.AddWithValue("@Date", dtDate);

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            Connection.Close();
                            return dt;
                        }
                    }
                }
            }
            catch (MySqlException sqlEx)
            {
#if USE_LOCALHOST
                MessageBox.Show(sqlEx.Message);
#endif
            }

            Connection.Close();
            return null;
        }



        public DataTable GetAllPresentTrainer(DateTime dtDate)
        {
            Connection.Open();


            string strSQL = "UPDATE wtuser SET state=0" + ';' + "DROP TABLE IF EXISTS temp" + ';' +
                            "create temporary table temp as (select s.id, CAST(t.begin AS DATE) as 'Datum', t.begin as 'Start', t.end as 'Ende', s.surname as 'Vorname'," +
                            "s.middlename as 'Mittelname',s.lastname as 'Nachname', s.username as 'Benutzername',s.className as 'Klasse',s.state, t.absenceReason as 'Abwesenheit', t.absenceConfirmation, t.absenceCompleted as 'Gesundmeldung' " +
                            "from wtuser s join wttimerecord t on t.userId = s.id and CAST(begin AS DATE)=CAST(@Date AS DATE))" +
                            ';' +
                            "UPDATE wtuser SET state = 1 WHERE id in (SELECT id from temp where Ende is null)" + ';' +
                            "UPDATE wtuser SET state = 2 WHERE id in (SELECT id from temp where (Abwesenheit='SicknessRange' or Abwesenheit='SicknessDaily') and Gesundmeldung=0)" + ';' +
                            "select surname as 'Vorname', middlename as '2.Vorname',lastname as 'Nachname', username as 'Benutzername', state as 'Status' " +
                            "from wtuser where locationId=1 and activated=1 and className='LAPTrainer'";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.Parameters.AddWithValue("@Date", dtDate);

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            Connection.Close();
                            return dt;
                        }
                    }
                }
            }
            catch (MySqlException sqlEx)
            {
#if USE_LOCALHOST
                MessageBox.Show(sqlEx.Message);
#endif
            }

            Connection.Close();
            return null;
        }


        public DataTable GetAllSicknessTimeRecords()
        {

            Connection.Open();
            string strSQL = "select s.surname as 'Vorname', s.middlename as '2.Vorname',s.lastname as 'Nachname', s.username as 'Benutzername', " +
                            "t.id, t.begin as 'Von', t.end as 'Bis', t.absenceConfirmation, t.absenceCompleted " +
                            "from wtuser s join wttimerecord t on t.userId = s.id " +
                            "and absenceReason like '%Sickness%'";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (var dt = new DataTable())
                        {
                            sda.Fill(dt);
                            Connection.Close();
                            return dt;
                        }

                    }
                }
            }
            catch (MySqlException sqlEx)
            {
#if USE_LOCALHOST
                MessageBox.Show(sqlEx.Message);
#endif
            }

            Connection.Close();
            return null;
        }


        public bool AddTimeRecordStart(string strEmail, DateTime dtStart, string strAbsence = "",
            int iAbsenceConfirmation = 0, int iAbsenceCompleted = 0, int iAbsenceFlag=0)
        {
            int iUserId = -1;
            bool bRes = false;
            userRepo.GetUserIdByEmail(strEmail, out iUserId);
            if (iUserId >= 0)
            {
                string strSQLQuery =
                    "insert into wttimerecord values (0, @UserId , @TimeBegin, NULL, @strAbsence, @iAbsConfirm, @iAbsCompleted, NULL, @iAbsFlag )";

                Connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserId", iUserId);
                    cmd.Parameters.AddWithValue("@TimeBegin", dtStart);
                    cmd.Parameters.AddWithValue("@strAbsence", strAbsence);
                    cmd.Parameters.AddWithValue("@iAbsConfirm", iAbsenceConfirmation);
                    cmd.Parameters.AddWithValue("@iAbsCompleted", iAbsenceCompleted);
                    cmd.Parameters.AddWithValue("@iAbsFlag", iAbsenceFlag);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        bRes = true;
                    }
                    catch (MySqlException sqlEx)
                    {
                        Connection.Close();
#if USE_LOCALHOST
                        MessageBox.Show(sqlEx.Message);
#endif
                    }
                }

                Connection.Close();
            }

            return bRes;
        }

        public bool AddTimeRecordEnd(string strEmail, DateTime dtEnd, int iAbsenceConfirmation = 0,
                                     int iAbsenceCompleted = 0)
        {
            int iUserId = -1;
            bool bRes = false;
            userRepo.GetUserIdByEmail(strEmail, out iUserId);
            if (iUserId >= 0)
            {
                if (FindLastOpenTimeRecord(iUserId, out int iTimeRecId, out _))
                {
                    string strSQL = "update wttimerecord " +
                                    "set end=@DateEnd " +
                                    "where id=@TimeRecId";
                    Connection.Open();
                    try
                    {
                        using (MySqlCommand cmd2 = new MySqlCommand(strSQL))
                        {
                            cmd2.Connection = Connection;
                            cmd2.CommandType = CommandType.Text;
                            cmd2.Parameters.AddWithValue("@DateEnd", dtEnd);
                            cmd2.Parameters.AddWithValue("@TimeRecId", iTimeRecId);
                            cmd2.Parameters.AddWithValue("@iAbsConfirm", iAbsenceConfirmation);
                            cmd2.Parameters.AddWithValue("@iAbsCompleted", iAbsenceCompleted);
                            cmd2.ExecuteNonQuery();
                            bRes = true;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Connection.Close();
                }
            }

            return bRes;
        }



        public bool FindLastOpenTimeRecord(int iUserId, out int iTimeRecId, out DateTime dateTime)
        {

            Connection.Open();
            string strSQL = "select * " +
                            "from wttimerecord " +
                            "where userId=@UserId " +
                            "and end is NULL";
            iTimeRecId = -1;
            dateTime = DateTime.MinValue;
            bool bRes = false;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.Parameters.AddWithValue("@UserId", iUserId);

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            iTimeRecId = (int) dt.Rows[dt.Rows.Count - 1]["id"];
                            var begin = dt.Rows[dt.Rows.Count - 1]["begin"];
                            if (!(begin is System.DBNull))
                            {
                                dateTime = (DateTime) begin;
                            }
                            bRes = true;
                        }
                    }
                }
            }
            catch (MySqlException sqlEx)
            {
#if USE_LOCALHOST
                MessageBox.Show(sqlEx.Message);
#endif
            }

            Connection.Close();
            return bRes;
        }


        private bool SetSicknessCompleted(int iId)
        {
            bool bRet = false;
            Connection.Open();
            string strSQL = "update wttimerecord " +
                            "set absenceCompleted=1 " +
                            "where id=@Id";
            try
            {
                using (MySqlCommand cmd2 = new MySqlCommand(strSQL))
                {
                    cmd2.Connection = Connection;
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Parameters.AddWithValue("@Id", iId);
                    cmd2.ExecuteNonQuery();
                }
                bRet = true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Connection.Close();

            return bRet;
        }

        public bool StartSickness(string strEmail, DateTime dtStart, DateTime dtEnd)
        {
            if (dtStart == dtEnd)
            {
                if (!AddTimeRecordStart(strEmail, new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, 6, 0, 0),
                    "SicknessDaily"))
                    return false;
                if (!AddTimeRecordEnd(strEmail, new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, 16, 0, 0)))
                    return false;
            }
            else
            {
                if (!AddTimeRecordStart(strEmail, new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, 6, 0, 0),
                    "SicknessRange",0,0,1))
                    return false;
                if (!AddTimeRecordEnd(strEmail, new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, 16, 0, 0)))
                    return false;

                DateTime dtIter = dtStart.AddDays(1);
                while (dtIter <= dtEnd)
                {
                    if (!AddTimeRecordStart(strEmail, new DateTime(dtIter.Year, dtIter.Month, dtIter.Day, 6, 0, 0),
                        "SicknessRange"))
                        return false;
                    if (!AddTimeRecordEnd(strEmail, new DateTime(dtIter.Year, dtIter.Month, dtIter.Day, 16, 0, 0)))
                        return false;
                    dtIter = dtIter.AddDays(1);
                }
            }

            return true;
        }

        public bool EndSickness(string strEmail, DateTime dtEnd)
        {
            int iUserId = -1;
            bool bRes = false;
            DataTable dtRes = GetAllSicknessTimeRecordsByUserEmail(strEmail);

            if (dtRes != null && dtRes.Rows.Count > 0)
            {
                int iLastRecId = (int) dtRes.Rows[dtRes.Rows.Count - 1]["id"];
                string strLastAbsReason = (string) dtRes.Rows[dtRes.Rows.Count - 1]["absenceReason"];
                DateTime dtLastBegin = (DateTime) dtRes.Rows[dtRes.Rows.Count - 1]["begin"];

                if (strLastAbsReason == "SicknessDaily")
                {
                    if (!SetSicknessCompleted(iLastRecId))
                        return false;
                    
                    DateTime dtIter = dtLastBegin.AddDays(1);
                    while (dtIter.DayOfYear < dtEnd.DayOfYear)
                    {
                        if (!AddTimeRecordStart(strEmail, new DateTime(dtIter.Year, dtIter.Month, dtIter.Day, 6, 0, 0), "SicknessDaily",0,1))
                            return false;
                        if (!AddTimeRecordEnd(strEmail, new DateTime(dtIter.Year, dtIter.Month, dtIter.Day, 16, 0, 0)))
                            return false;
                        dtIter = dtIter.AddDays(1);
                    }

                    bRes = true;
                }
                else
                {
                    int i = dtRes.Rows.Count - 1;
                    while (i >= 0)
                    {
                        int iRecId = (int)dtRes.Rows[i]["id"];
                        DateTime dtBegin = (DateTime)dtRes.Rows[i]["begin"];
                        if (dtBegin.DayOfYear > dtEnd.DayOfYear)
                        {
                            if (!DeleteTimeRecord(iRecId))
                                return false; 
                        }
                        else
                        {
                            SetSicknessCompleted(iRecId);
                        }
                        --i;
                    }

                    bRes = true;
                }
            }

            return bRes;
        }

        public bool EndDay(string strEmail, DateTime dtDate)
        {
            int iUserId = -1;
            bool bRes = false;
            userRepo.GetUserIdByEmail(strEmail, out iUserId);
            if (iUserId >= 0)
            {
                if (FindLastOpenTimeRecord(iUserId, out _, out DateTime dtEndDate))
                {
                    if (dtEndDate.DayOfYear < dtDate.DayOfYear)
                    {
                        bRes = AddTimeRecordEnd(strEmail,
                            new DateTime(dtEndDate.Year, dtEndDate.Month, dtEndDate.Day, 23, 59, 59));

                    }
                    else
                    {
                        bRes = AddTimeRecordEnd(strEmail, dtDate);
                    }
                }
            }

            return bRes;
        }


        private bool DeleteTimeRecord(int iRecId)
        {
            string strSQLQuery = "delete from wttimerecord where id=@Id";
            bool bRes = false;
            Connection.Open();
            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", iRecId);

                try
                {
                    cmd.ExecuteNonQuery();
                    bRes = true;
                }
                catch (MySqlException sqlEx)
                {
#if USE_LOCALHOST
                    MessageBox.Show(sqlEx.Message);
#endif
                }
            }

            Connection.Close();
            return bRes;
        }

        public bool AddSicknessConfirmation(string strEmail, int iRecId, string strFilename)
        {
            bool bRes = false;
            int iUserId;
            userRepo.GetUserIdByEmail(strEmail, out iUserId);
            if (iUserId >= 0)
            {
                string strSQL = "update wttimerecord " +
                                "set absenceReport=@AbsReport,absenceConfirmation=1 " +
                                "where id=@TimeRecId";
                Connection.Open();
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(strSQL))
                    {
                        cmd.Connection = Connection;
                        cmd.CommandType = CommandType.Text; 
                        cmd.Parameters.AddWithValue("@TimeRecId", iRecId);
                        cmd.Parameters.AddWithValue("@AbsReport", strFilename);
                        cmd.ExecuteNonQuery();
                        bRes = true;
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Connection.Close();
                
            }

            return bRes;
        }

        public bool GetSicknessConfirmation(string strEmail, int iRecId, out string strFilename)
        {
            bool bRes = false;
            strFilename = "";
            userRepo.GetUserIdByEmail(strEmail, out var iUserId);
            if (iUserId >= 0)
            {
                string strSQL = "select absenceReport from wttimerecord " +
                                "where id=@TimeRecId";
                Connection.Open();
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(strSQL))
                    {
                        cmd.Connection = Connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@TimeRecId", iRecId);
                        
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                strFilename= (string) dt.Rows[0]["absenceReport"];
                                bRes = true;
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Connection.Close();

            }

            return bRes;
        }

        public bool SetAbsenceReason(int iId, string strAbsenceReason)
        {
            bool bRet = false;
            Connection.Open();
            string strSQL = "update wttimerecord " +
                            "set absenceReason=@AbsenceReason " +
                            "where id=@Id";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", iId);
                    cmd.Parameters.AddWithValue("@AbsenceReason", strAbsenceReason);
                    cmd.ExecuteNonQuery();
                }
                bRet = true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Connection.Close();

            return bRet;
        }

    }
}