using System;

namespace SoftObject.TrainConcept.Libraries
{
	/// <summary>
	/// Summary description for QuestionPool.
	/// </summary>
	/// 

	public class QuestionPool
	{
		private bool[]	aIsChosen;
        private QuestionCollection aQuestions;
		private bool isExaming;
		private int cntChosen;

		public bool IsEmpty
		{
			get
			{
				return (cntChosen==aIsChosen.Length);
			}
		}

		public QuestionPool(QuestionCollection _aQuestions,bool _isExaming)
		{
			aQuestions = _aQuestions;
			isExaming  = _isExaming;

			cntChosen=0;
			aIsChosen = new bool[aQuestions.Count];
		}

		public void Choose(QuestionCollection _aQuestions,int maxCnt)
		{
            if (maxCnt > (aQuestions.Count - cntChosen))
                Reset();

            int chosen = 0;
            int l_cntChosen = cntChosen;
			while(chosen < Math.Min(maxCnt,aQuestions.Count-l_cntChosen))
			{
				int quId=ChooseQuestion();
				QuestionItem que=aQuestions.Item(quId);
				_aQuestions.Add(que,aQuestions.GetPath(que),aQuestions.GetId(que));
				++chosen;
			}
		}

		public void ChooseAll(QuestionCollection _aQuestions)
		{
			for(int i=0;i<aQuestions.Count;++i)
			{
				QuestionItem que=aQuestions.Item(i);
				_aQuestions.Add(que,aQuestions.GetPath(que),aQuestions.GetId(que));
				aIsChosen[i]=true;
            }
		}

		public int ChooseQuestion()
		{
			Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
			while(true)
			{
				int quId=rand.Next(0,aQuestions.Count);
				if (!aIsChosen[quId])
				{
					++cntChosen;
					aIsChosen[quId]=true;
                    return quId;
				}
			}
		}


		public void Reset()
		{
			aIsChosen = new bool[aQuestions.Count];
            cntChosen = 0;
		}
	}
}
