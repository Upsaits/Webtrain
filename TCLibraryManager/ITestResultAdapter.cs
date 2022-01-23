using System;
using System.IO;

namespace SoftObject.TrainConcept.Libraries
{
	public interface ITestResultAdapter
	{
		Stream GetReadStream(string strFilePath);
		Stream GetWriteStream(string strFilePath);
	}
}

