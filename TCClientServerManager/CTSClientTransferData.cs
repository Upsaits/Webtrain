using System;
using System.IO;
using System.Threading;
using System.Diagnostics;


namespace SoftObject.TrainConcept.ClientServer
{
	public delegate void OnCTSClientHandler(object sender,CTSClientEventArgs ea);

	public class CTSClientTransferData
	{
	    private long m_fileSize=0;
		private long m_actualSize=0;
		Stream m_stream=null;
	    private int m_packageCnt=0;

	    public string TypeName { get; private set; }
	    public string Name { get; private set; }
	    public string FileName { get; private set; }
	    public double PercDone { get; private set; }
	    public bool IsActive { get; private set; }
	    public int PackageId { get; set; }

	    public CTSClientTransferData(string typeName,string name,string fileName,int fileSize)
		{
			TypeName = typeName;
			Name = name;
		    PercDone = 0;
		    IsActive = false;
			m_packageCnt=1;
			PackageId = 1;
			FileName = fileName;
			m_fileSize = fileSize;
		}

		public CTSClientTransferData(string typeName,string name,int packageCnt,int packageId,
									 string fileName,int fileSize)
		{
			TypeName = typeName;
			Name = name;
			m_packageCnt=packageCnt;
			PackageId =packageId;
			FileName = fileName;
			m_fileSize = fileSize;
		    PercDone = 0;                                                                                                                               
		    IsActive = false;
		}

		public void Start()
		{
			m_actualSize=0;
		    m_stream = File.Create(FileName);
		    Thread.Sleep(250);
			IsActive = true;
		}

		public void Stop()
		{
			if (m_stream!=null)
			{
				m_stream.Close();
				m_stream=null;
			}
			IsActive=false;
		}

		public void DoTransfer(string text)
		{
		}

        public void DoTransfer(byte[] aBytes,int length)
        {
            if (IsActive)
            {
                m_stream.Write(aBytes, 0, length);
                m_actualSize = m_stream.Position;
                PercDone = ((double)m_actualSize) * 100 / ((double)m_fileSize);
                Trace.WriteLine("DoTransfer(" + String.Format("act:{0},exp:{1}) = {2}", m_actualSize, m_fileSize, PercDone));
                if (m_actualSize >= m_fileSize)
                    Stop();
            }
        }

		public bool IsLastPackage()
		{
			return m_packageCnt == PackageId;
		}
	}
}