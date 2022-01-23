using System;
using System.IO;

namespace SoftObject.TrainConcept.Libraries
{
	public interface ILibraryAdapter
	{
		Stream GetReadStream(string strFilePath);
		Stream GetWriteStream(string strFilePath);
	}
}

