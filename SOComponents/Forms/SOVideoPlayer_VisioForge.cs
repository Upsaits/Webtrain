using System;
using System.Drawing;
using System.Windows.Forms;
using SoftObject.SOComponents.UtilityLibrary;
using VisioForge.Controls.UI.WinForms;
using VisioForge.Types;

namespace SoftObject.SOComponents.Forms
{
    /// <summary>
    /// Zusammendfassende Beschreibung für Form1.
    /// </summary>
    public partial class SOVideoPlayer_VisioForge : DevExpress.XtraEditors.XtraForm
	{
		enum MediaStatus { None, Stopped, Paused, Running };
		private MediaStatus m_currentStatus = MediaStatus.None;
		private System.Windows.Forms.Timer timer1;
		private ResourceHandler m_rh=null;
	    private string m_fileName="";

		
		public SOVideoPlayer_VisioForge() 
		{
#if(SKIN_EMCO)
			m_rh = new ResourceHandler("res.trainconcept.resources_emco",GetType().Assembly);
#elif(SKIN_WEBTRAIN)
			m_rh = new ResourceHandler("res.webtrain.resources_softobject",GetType().Assembly);
#else
			m_rh = new ResourceHandler("res.trainconcept.resources_emco",GetType().Assembly);
#endif

			InitializeComponent();
            
			//Erstelle alle Buttons für Video Player
			//Play, Pause, Stop Buttons
			btnPlay.Image = new Bitmap(m_rh.GetBitmap("btnPlayNormal"));
			btnPause.Image = new Bitmap(m_rh.GetBitmap("btnPauseNormal"));
			btnStop.Image = new Bitmap(m_rh.GetBitmap("btnStopNormal"));

            mediaPlayer1.SetLicenseKey("1DD3-9064-6B16-7B30-8D79-6A1A", "Franz Mair", "fmair@softobject.at");
            mediaPlayer1.OnError += mediaPlayer1_OnError;
        }

        private void mediaPlayer1_OnError(object sender, ErrorsEventArgs e)
        {
            Console.WriteLine(e.Message+" ",e.AssemblyVersion);
        }

		public void Play(string fileName)
		{
			CleanUp();

            /*
            switch (cbSourceMode.SelectedIndex)
            {
                case 0:
                    mediaPlayer1.Source_Mode = VFMediaPlayerSource.LAV;
                    break;
                case 1:
                    mediaPlayer1.Source_Mode = VFMediaPlayerSource.File_DS;
                    break;
                case 2:
                    mediaPlayer1.Source_Mode = VFMediaPlayerSource.File_FFMPEG;
                    break;
                case 3:
                    mediaPlayer1.Source_Mode = VFMediaPlayerSource.File_VLC;
                    break;
            }*/

            if (fileName.IndexOf("http://", StringComparison.Ordinal) >= 0)
                fileName = fileName.Replace('\\', '/');

            mediaPlayer1.Source_Mode = VFMediaPlayerSource.LAV;
            mediaPlayer1.FilenamesOrURL.Add(fileName);
            //mediaPlayer1.FilenamesOrURL.Add("http://localhost/TrainconceptServer/Content/de/cnc_programmierung/videos/G0turn.avi");
            mediaPlayer1.Loop = true;
            mediaPlayer1.Audio_PlayAudio = true;

            mediaPlayer1.Audio_OutputDevice = "Default DirectSound Device";

            if (mediaPlayer1.Filter_Supported_EVR())
            {
                mediaPlayer1.Video_Renderer.Video_Renderer = VFVideoRenderer.EVR;
            }
            else if (mediaPlayer1.Filter_Supported_VMR9())
            {
                mediaPlayer1.Video_Renderer.Video_Renderer = VFVideoRenderer.VMR9;
            }
            else
            {
                mediaPlayer1.Video_Renderer.Video_Renderer = VFVideoRenderer.VideoRenderer;
            }

            mediaPlayer1.Debug_Mode = false;

		    Cursor.Current = Cursors.WaitCursor;

            mediaPlayer1.Play();
            mediaPlayer1.Pause();
            mediaPlayer1.Position_Set_Time(0);
            m_currentStatus = MediaStatus.Paused;
		    m_fileName = fileName;
            // set audio volume for each stream
            //mediaPlayer1.Audio_OutputDevice_Balance_Set(0, tbBalance1.Value);
            //mediaPlayer1.Audio_OutputDevice_Volume_Set(0, tbVolume1.Value);

			Show();

			slider1.Minimum = 0;
            slider1.Maximum = (int)(mediaPlayer1.Duration_Time() / 1000);

			timer1.Enabled = true;
			timer1.Start();
            Cursor.Current = Cursors.Default;
		}

		public void Stop()
		{
            mediaPlayer1.Stop();
            m_currentStatus = MediaStatus.Stopped;
            slider1.Value = 0;
        }

		private void CleanUp()
		{
			m_currentStatus = MediaStatus.Stopped;
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
            timer1.Tag = 1;
		    slider1.Maximum = (int)(mediaPlayer1.Duration_Time() / 1000);

            int value = (int)(mediaPlayer1.Position_Get_Time() / 1000);
            if ((value > 0) && (value < slider1.Maximum))
            {
                slider1.Value = value;
            }

            this.textBox1.Text= MediaPlayer.Helpful_SecondsToTimeFormatted(slider1.Value) + "/" + MediaPlayer.Helpful_SecondsToTimeFormatted(slider1.Maximum);

            timer1.Tag = 0;
		}

	    private void btnStop_Click_1(object sender, EventArgs e)
	    {
	        mediaPlayer1.Position_Set_Time(0);
            mediaPlayer1.Pause();
	        m_currentStatus = MediaStatus.Paused;
	        slider1.Value = 0;
	    }

        private void btnPlay_Click_1(object sender, EventArgs e)
        {
            if (m_currentStatus != MediaStatus.Running)
            {
                if (m_currentStatus == MediaStatus.Stopped)
                    mediaPlayer1.Play();
                else if (m_currentStatus == MediaStatus.Paused)
                    mediaPlayer1.Resume();

                m_currentStatus = MediaStatus.Running;
            }
        }

        private void btnPause_Click_1(object sender, EventArgs e)
        {
            if (m_currentStatus == MediaStatus.Running)
            {
                mediaPlayer1.Pause();
                m_currentStatus = MediaStatus.Paused;
            }
        }

        private void slider1_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(timer1.Tag) == 0)
            {
                mediaPlayer1.Position_Set_Time(slider1.Value * 1000);
            }
        }

        private void slider1_IncreaseButtonClick(object sender, EventArgs e)
        {

        }

        private void slider1_DecreaseButtonClick(object sender, EventArgs e)
        {

        }

        private void SOVideoPlayer_VisioForge_FormClosed(object sender, FormClosedEventArgs e)
        {
            mediaPlayer1.Stop();
        }

        private void mediaPlayer1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var frm = new FrmShowFilename(m_fileName);
            frm.ShowDialog();
        }

    }
}

