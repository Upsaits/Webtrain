using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using DevExpress.XtraRichEdit;
using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Asn1.Cms;
using WebtrainWebPortal.Repositories;
using WebtrainWebPortal.Views;
using Wisej.Web;
using MySqlConnector;

namespace WebtrainWebPortal
{
    public enum eViewState
    {
        eStart,
        eLogin,
        eUser,
        eUserAdminStudents,
        eUserAdminTrainer,
        eTimeRecording,
        eReportAbsenceStudents,
        eReportAbsenceTrainer,
        eReportSickness,
        eLearnContent
    }

    public class UserInfo
    {
        public string EMail { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsLoggedIn { get; set; }
        public eViewState ViewState { get; set; } = eViewState.eTimeRecording;
    };

    public class WorkflowMediator
    {
        private static readonly Lazy<WorkflowMediator> lazy =
            new Lazy<WorkflowMediator>(() => new WorkflowMediator());
        public static WorkflowMediator Instance => lazy.Value;

        public static AdminUserRepository AdminUserRepo { get; } = AdminUserRepository.Instance;
        public static UserRepository UserRepo { get; } = UserRepository.Instance;
        public static TimeRecordRepository TimeRecordRepo { get; } = TimeRecordRepository.Instance;

        public string ConnectionString { get; set; }
        public MySqlConnection Connection { get; set; }

        public Dictionary<string, MainPage> MainPages => dMainPages;

        private readonly Dictionary<string, MainPage> dMainPages = new Dictionary<string, MainPage>();

        private WorkflowMediator()
        {

#if USE_LOCALHOST
            ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#else
            ConnectionString = ConfigurationManager.ConnectionStrings["ionosConnection"].ConnectionString;
#endif
#endif
            Connection = new MySqlConnection(ConnectionString);

            try
            {
                Connection.Open();
                Connection.Close();
            }
            catch (Exception ex)
            {
                AlertBox.Show(ex.Message,MessageBoxIcon.Error,true,ContentAlignment.MiddleCenter);
            }
            
        }


        public void SetActiveUser(UserInfo ui)
        {
            var pg = FindUserMainPage(ui.Username);
            if (pg != null)
            {
                Application.MainPage = pg;
                Application.MainPage.Show();
                SetViewState(ui,ui.ViewState);
            }
        }

        public void SetViewState(UserInfo ui,eViewState state=eViewState.eStart,int iStateInfo=-1)
        {
            if (ui == null)
            {
                Application.MainPage = new LoginPage();
                Application.MainPage.Show();
            }
            else
            {
                ui.ViewState = state;

                switch (state)
                {
                    case eViewState.eStart:
                        break;
                    case eViewState.eLogin:
                        Application.MainPage = new LoginPage();
                        Application.MainPage.Show();
                        break;
                    case eViewState.eUser:
                    case eViewState.eUserAdminStudents:
                    case eViewState.eUserAdminTrainer:
                    case eViewState.eTimeRecording:
                    case eViewState.eReportAbsenceStudents:
                    case eViewState.eReportAbsenceTrainer:
                    case eViewState.eReportSickness:
                    case eViewState.eLearnContent:
                        ((MainPage)Application.MainPage).SetViewState(ui.ViewState,iStateInfo);
                        break;
                }

            }
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public UserInfo TryToLogin(string strUserEmail,string strPassword)
        {
            string l_strUserName = "";

            if (!IsValidEmail(strUserEmail))
            {
                AlertBox.Show("Keine gültige Email-Adresse!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
            }
            else
            {
                bool isAdmin = true;
                string l_strPassword;
                string l_strSurname;
                string l_strMiddlename="";
                string l_strLastname;
                string l_strClassname;
                bool bUserExists = AdminUserRepo.FindUser(strUserEmail, out l_strPassword,out l_strSurname,out l_strLastname,out l_strUserName);

                if (!bUserExists)
                {
                    l_strPassword = "";
                    l_strUserName = "";
                    bUserExists = UserRepo.FindUser(1, strUserEmail, out l_strPassword, out l_strSurname, out l_strMiddlename, out l_strLastname, out l_strUserName, out l_strClassname);
                    isAdmin = (l_strClassname == "LAPTrainer");
                }

                if (bUserExists)
                {
                    if (l_strPassword != strPassword)
                    {
                        AlertBox.Show("Passwort ungültig!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
                    }
                    else
                    {
                        var pg = FindUserMainPage(l_strUserName);
                        if (pg != null)
                        {
                            //AlertBox.Show($"Der Benutzer {l_strUserName} ist bereits in einem Browserfenster aktiv!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
                            //return null;
                            //SetViewState(pg.UserInfo, eViewState.eLogin);
                            RemoveUser(pg.UserInfo);
                            //return pg.UserInfo;
                        }

                        var ui = new UserInfo();
                        ui.IsLoggedIn = false;
                        ui.EMail = strUserEmail;
                        ui.Username = l_strUserName;
                        ui.Fullname = l_strSurname + ' ' + l_strMiddlename + l_strLastname;
                        ui.ViewState = isAdmin ? eViewState.eUserAdminStudents: eViewState.eUser;
                        ui.IsAdmin = isAdmin;

                        AddUser(ui);
                        
                        return ui;
                    }
                }
                else
                {
                    AlertBox.Show("Kein Benutzer mit dieser Email-Adresse vorhanden!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
                }
            }

            return null;
        }

        private void AddUser(UserInfo ui)
        {
            dMainPages.Add(ui.Username,new MainPage(ui));
        }

        public void RemoveUser(UserInfo ui)
        {
            dMainPages.Remove(ui.Username);
        }

        private MainPage FindUserMainPage(string lStrUserName)
        {
            var res = dMainPages.Where(c => c.Value.UserInfo.Username == lStrUserName);
            if (res.Any())
                return res.First().Value;
            return null;
        }

        public bool TryToRegister(int locationId, string strFirstName,string strMiddleName,string strLastName,string strEmail,string strPassword,string strConfPassword)
        {
            if (!IsValidEmail(strEmail))
            {
                AlertBox.Show("Keine gültige Email-Adresse!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
            }
            else if (strPassword != strConfPassword)
            {
                AlertBox.Show("Passwort stimmt nicht überein!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
            }
            else
            {
                bool bUserExists = AdminUserRepo.FindUser(strFirstName, strMiddleName, strLastName);
                if (!bUserExists)
                {
                    bUserExists = UserRepo.FindUser(locationId, strEmail, out _, out _, out _, out _, out _,out _);
                }

                if (bUserExists)
                {
                    AlertBox.Show("Benutzer ist bereits registriert!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
                }
                else
                {
                    var dtNow = DateTime.Now;
                    string strClassname = $"LAPAV{dtNow.Month.ToString("D2")}{dtNow.Day.ToString("D2")}";
                    bool bRes = UserRepo.CreateUser(locationId, strFirstName, strMiddleName, strLastName, "", true, strClassname, strEmail, strPassword);
                    if (bRes)
                    {
                        AlertBox.Show("Benutzer erfolgreich angelegt, sie können sich jetzt anmelden!", MessageBoxIcon.Information, true, ContentAlignment.MiddleCenter);
                        return true;
                    }
                    else
                    {
                        AlertBox.Show("Benutzer konnte nicht erstellt werden!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
                    }
                }
            }

            return false;
        }

        public void vSendEmail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Joey Tribbiani", "joey@friends.com"));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
            message.Subject = "How you doin'?";
            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,
I just wanted to let you know that Monica and I were going to go play some paintball, you in?
-- Joey"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.friends.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("joey", "password");

                client.Send(message);
                client.Disconnect(true);
            }
        }

        public bool TryToSetNewPassword(string strEmail, string strNewPassword, string strNewPasswordConfirm)
        {
            if (!IsValidEmail(strEmail))
            {
                AlertBox.Show("Keine gültige Email-Adresse!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
            }
            else if (strNewPassword.Length<6 || strNewPasswordConfirm.Length < 6)
            {
                AlertBox.Show("Passwort muß mindestens 6 Zeichen enthalten!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
            }
            else if (strNewPassword != strNewPasswordConfirm)
            {
                AlertBox.Show("Passwort stimmt nicht überein!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
            }
            else
            {
                bool bUserExists = UserRepo.FindUser(1, strEmail, out _, out _, out _, out _, out _, out _);

                if (!bUserExists)
                {
                    AlertBox.Show("Benutzer ist nicht registriert!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
                }
                else
                {
                    bool bRes = UserRepo.SetNewPassword(1,strEmail,strNewPassword);
                    if (bRes)
                    {
                        AlertBox.Show("Passwort neu gesetzt, sie können sich jetzt anmelden!", MessageBoxIcon.Information, true, ContentAlignment.MiddleCenter);
                        return true;
                    }
                    else
                    {
                        AlertBox.Show("Passwort konnte nicht geändert werden!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
                    }
                }
            }

            return false;
        }

        public void LoadWordFile(string strFile)
        {
            /*
            string filePath = Application.MapPath($"~/Data/{strFile}");
            var server= new RichEditDocumentServer();
            server.LoadDocument(filePath);
            Process.Start(filePath);*/
        }

        public bool StartSickness(string strEMail,bool bHasRange, DateTime dtStart, DateTime dtEnd)
        {
            if (bHasRange)
                return TimeRecordRepo.StartSickness(strEMail, dtStart, dtEnd);
            return TimeRecordRepo.StartSickness(strEMail, dtStart,dtStart);
        }

        public bool EndSickness(string strEMail, DateTime dtEnd)
        {
            return TimeRecordRepo.EndSickness(strEMail, dtEnd);
        }

        public bool EndDay(UserInfo userInfo, DateTime dtDate)
        {
            return TimeRecordRepo.EndDay(userInfo.EMail, dtDate);
        }

        public bool StartDay(UserInfo userInfo, DateTime dtDate)
        {
            TimeRecordRepo.AddTimeRecordStart(userInfo.EMail, dtDate);
            return true;
        }

        public bool AddSicknessConfirmation(string strEmail, int iRecId, string strFilename)
        {
            return TimeRecordRepo.AddSicknessConfirmation(strEmail, iRecId, strFilename);
        }

        public bool GetSicknessConfirmation(string strEmail, int iRecId, out string strFilepath)
        {
            string strFilename = "";
            if (TimeRecordRepo.GetSicknessConfirmation(strEmail, iRecId, out strFilename))
            {
                string l_strFilename = $"~/Data/{strFilename}";
                string l_strFilePath = Application.MapPath(l_strFilename);
                if (System.IO.File.Exists(l_strFilePath))
                {
                    strFilepath = l_strFilePath;
                    return true;
                }
            }

            strFilepath = "";
            return false;
        }
    }
}