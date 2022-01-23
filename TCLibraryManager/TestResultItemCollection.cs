using System;
using System.Collections;
using System.Globalization;


namespace SoftObject.TrainConcept.Libraries
{

	[Serializable]
	public class TestResultItemCollection : System.Collections.CollectionBase
	{
		public void Add(TestResultItem item)
		{
			List.Add(item);
		}

		public void Remove(int _id)
		{
			if (_id>=0 && _id < Count)
				List.RemoveAt(_id); 
		}	

		public TestResultItem Item(int _id)
		{
			return (TestResultItem) List[_id];
		}
	}
}
