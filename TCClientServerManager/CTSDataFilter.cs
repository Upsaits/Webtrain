using System;
using System.Text;

namespace SoftObject.TrainConcept.ClientServer
{
    /// <summary>
	/// 
	/// </summary>
	public class CTSDataFilter
	{
		public CTSDataFilter()
		{
		}

        public bool AnalyseData(string strToAnalyze, out string strAnalyzed)
        {
            strAnalyzed = strToAnalyze;
            return true;
        }

	    public bool AnalyseData(byte [] aBytes,out string outString)
		{
		    outString = Encoding.UTF8.GetString(aBytes);
            return true;
		}
	}
}
