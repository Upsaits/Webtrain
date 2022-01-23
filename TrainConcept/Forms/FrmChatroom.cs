using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.ClientServer;

namespace SoftObject.TrainConcept
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmChatroom.
	/// </summary>
	public partial class FrmChatroom : XtraForm
	{
		private int roomId=0;
		private Queue qMessages=null;
		private ResourceHandler rh=null;
        private AppHandler AppHandler = Program.AppHandler;
		public int RoomId
		{
			get
			{
				return roomId;
			}
		}


        public FrmChatroom(int _roomId, Queue qMsgs)
		{
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);

			InitializeComponent();

			this.Icon = rh.GetIcon("main");

			roomId=_roomId;
			qMessages=qMsgs;

			UpdateUserList();
			UpdateMsgs();

			AppHandler.CtsClientManager.OnCTSClientEvent += new OnCTSClientHandler(FrmChatroom_CTSClient);
			AppHandler.CtsServerManager.OnCTSServerEvent += new OnCTSServerHandler(FrmChatroom_CTSServer);
            AppHandler.CtsVPNServerManager.OnCTSServerEvent += new OnCTSServerHandler(FrmChatroom_CTSServer);
            
		}

        public new void Activate()
        {
            base.Activate();
            AfterActivation(true);
        }

        public void AfterActivation(bool bIsOn)
        {
            if (!bIsOn)
            {
                AppHandler.MainForm.CloseBar.Visible = false;
            }
            else
            {
                AppHandler.MainForm.CloseBar.Visible = true;
            }
        }

		public void UpdateUserList()
		{
			lstUsers.Items.Clear();

            if (AppHandler.UserManager.GetUserCount() > 0)
            {
                string[] aUserNames;
                AppHandler.UserManager.GetUserNames(out aUserNames);
                lstUsers.Items.AddRange(aUserNames);
            }
		}
	
		private void SetReceiver(string name)
		{
			txtTo.Text = name;
			if (name.Length>0)
				btnSend.Enabled=(txtMessage.Text.Length>0);
			else
				btnSend.Enabled=false;
		}

		public void UpdateMsgs()
		{
			lstTalking.Items.Clear();
			lstTalking.Items.AddRange(qMessages.ToArray());
			lstTalking.Update();
		}

		public void ReceiveMessage(string fromUserName,string message)
		{
			string msg=String.Format("{0}: {1}",fromUserName,message);
			lstTalking.Items.Add(msg);
			lstTalking.Update();
		}
		private void FrmChatroom_Closed(object sender, System.EventArgs e)
		{
			AppHandler.CtsClientManager.OnCTSClientEvent -= new OnCTSClientHandler(FrmChatroom_CTSClient);
			AppHandler.CtsServerManager.OnCTSServerEvent -= new OnCTSServerHandler(FrmChatroom_CTSServer);
            AppHandler.CtsVPNServerManager.OnCTSServerEvent -= new OnCTSServerHandler(FrmChatroom_CTSServer);

            AfterActivation(false);
            AppHandler.ContentManager.ChatroomClosed(roomId);
		}

		private void lstUsers_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			if (e.Index>=0)
			{
				e.DrawBackground();
				e.DrawFocusRectangle();

                string strUser=lstUsers.Items[e.Index].ToString();
                if ((strUser=="admin" && AppHandler.MainForm.IsAdminUserConnected()) || AppHandler.MainForm.IsUserConnected(strUser))
					e.Graphics.DrawString((string) lstUsers.Items[e.Index],new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular),new SolidBrush(Color.Black),e.Bounds);
				else
					e.Graphics.DrawString((string) lstUsers.Items[e.Index],new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular),new SolidBrush(Color.DarkGray),e.Bounds);
			}
		}

		private void lstUsers_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
		{
			e.ItemHeight= 22;
		}

		private void FrmChatroom_CTSClient(object sender,CTSClientEventArgs ea)
		{
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnCTSClientHandler(FrmChatroom_CTSClient), new object[] { sender, ea });
                return;
            }
			if (ea.Command == CTSClientEventArgs.CommandType.Login ||
				ea.Command == CTSClientEventArgs.CommandType.Logout)
			{
				lstUsers.Refresh();
			}
		}

		public void FrmChatroom_CTSServer(object sender,CTSServerEventArgs ea)
		{
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnCTSServerHandler(FrmChatroom_CTSServer), new object[] { sender, ea });
                return;
            }

            if (ea.Command==CTSServerEventArgs.CommandType.Login ||
				ea.Command == CTSServerEventArgs.CommandType.Logout)
			{
                lstUsers.Refresh();
            }
		}

		private void lstUsers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lstUsers.SelectedIndex>=0)
			{
				string userName=(string) lstUsers.Items[lstUsers.SelectedIndex];
				if (userName!=AppHandler.MainForm.ActualUserName)
					SetReceiver(userName);
			}
			else
				SetReceiver("");
		}

		private void txtMessage_TextChanged(object sender, System.EventArgs e)
		{
			if (txtMessage.Text.Length>0 && txtTo.Text.Length>0)
				btnSend.Enabled=true;
			else
				btnSend.Enabled=false;
		}

		private void btnSend_Click(object sender, System.EventArgs e)
		{
			string txt=String.Format("{0}: {1}",AppHandler.MainForm.ActualUserName,txtMessage.Text);
			qMessages.Enqueue(txt);
			UpdateMsgs();

            if (AppHandler.IsServer)
            {
                AppHandler.CtsServerManager.SendClassMessage(roomId, txtTo.Text, txtMessage.Text);
                AppHandler.CtsVPNServerManager.SendClassMessage(roomId, txtTo.Text, txtMessage.Text);
            }
            else 
				AppHandler.CtsClientManager.SendClassMessage(roomId,txtTo.Text,txtMessage.Text);

			txtMessage.Text = "";
			txtMessage.Select();
		}
	}
}
