using System.Collections;

namespace SoftObject.TrainConcept.Libraries
{
	/// <summary>
	/// Zusammendfassende Beschreibung für Workout.
	/// </summary>
	public class WorkoutInfoItem
	{
		private QuestionItem question;
		private string path;
		private int	 id;
		private bool isWorkedOut;
		private bool isRight;
		private BitArray actCorrMask;
		private bool wasEdited;
        public string quizAnswers;
        public bool QuizResult { get; set; }

        public QuestionItem Question
		{
			get
			{
				return question;
			}
		}

		public bool IsRight
		{
			get
			{
				return isRight;
			}
		}

		public bool IsWorkedOut
		{
			get
			{
				return isWorkedOut;
			}
		}

		public BitArray ActCorrMask
		{
			set
			{
				actCorrMask = new BitArray(value);
				wasEdited = true;
			}

			get
			{
				return actCorrMask;
			}
		}

        public string QuizAnswers
        {
            set
            {
                quizAnswers = value;
                wasEdited = true;
            }

            get
            {
                return quizAnswers;
            }
        }

		public string Path
		{
			get
			{
				return path;
			}
		}

		public int Id
		{
			get
			{
				return id;
			}
		}

        public bool WasEdited
        {
            get
            {
                return wasEdited;
            }
        }

		public WorkoutInfoItem(QuestionItem _question,string _path,int _id)
		{
			question=_question;
			path = _path;
			id = _id;
			isWorkedOut = false;
			isRight = false;
			wasEdited = false;
			actCorrMask = new BitArray(new int[] {0});
		}

		public bool Workout()
		{
            if (question.type == "MultipleChoice")
            {
                BitArray corrMask = new BitArray(new int[] { question.correctAnswerMask });
                for (int i = 0; i < question.Answers.Length; ++i)
                    if (corrMask[i] != actCorrMask[i])
                    {
                        isRight = false;
                        isWorkedOut = true;
                        return false;
                    }
                isRight = true;
                isWorkedOut = true;
                return true;
            }
            else if (question.type == "Completion")
            {
                isWorkedOut = true;
                isRight = QuizResult;
                return true;
            }

            return false;
        }

		public bool HasAnswer()
		{
            if (question.type == "MultipleChoice")
            {
                for (int i = 0; i < actCorrMask.Count; i++)
                    if (actCorrMask[i])
                        return true;
            }
            else if (question.type == "Completion")
            {
                return !string.IsNullOrEmpty(QuizAnswers);
            }
			return false;
		}
	}

	public class WorkoutInfoItemCollection : System.Collections.CollectionBase
	{
		public void Add(WorkoutInfoItem item)
		{
			List.Add(item);
		}

		public void Remove(int _id)
		{
			if (_id>=0 && _id < Count)
				List.RemoveAt(_id); 
		}	

		public WorkoutInfoItem Item(int _id)
		{
			if (_id<List.Count)
				return (WorkoutInfoItem) List[_id];
			return null;
		}

		public bool IsWorkedOut()
		{
			IEnumerator iter = GetEnumerator();
			while(iter.MoveNext())
			{
				WorkoutInfoItem item=(WorkoutInfoItem)iter.Current;
				if (!item.IsWorkedOut)
					return false;
			}
			return true;
		}

		public void WorkoutAll()
		{
			IEnumerator iter = GetEnumerator();
			while(iter.MoveNext())
			{
				WorkoutInfoItem item=(WorkoutInfoItem)iter.Current;
				item.Workout();
			}
		}

        public bool AreAllWorkedOut()
        {
            IEnumerator iter = GetEnumerator();
            while (iter.MoveNext())
            {
                WorkoutInfoItem item = (WorkoutInfoItem)iter.Current;
                if (!item.IsWorkedOut)
                    return false;
            }

            return true;
        }

		public void GetWorkoutResult(ref int _workedOut,ref int _right,ref int _wrong, string path="")
		{
			_workedOut=0;
			_right=0;
			_wrong=0;

			IEnumerator iter = GetEnumerator();
			while(iter.MoveNext())
			{
				WorkoutInfoItem item=(WorkoutInfoItem)iter.Current;
                if (item != null)
                {
                    if (item.IsWorkedOut && (path.Length == 0 || item.Path == path))
                        ++_workedOut;
                    if (item.IsWorkedOut)
                    {
                        if (item.IsRight)
                            ++_right;
                        else
                            ++_wrong;
                    }
                }
			}
		}
	}

}
