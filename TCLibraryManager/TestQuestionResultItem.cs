using System;
using System.Collections;

namespace SoftObject.TrainConcept.Libraries
{
    /// <summary>
    /// Zusammendfassende Beschreibung für TestResult.
    /// </summary>
    /// 
    [Serializable]
    public class TestQuestionResultItem : ICloneable
    {
        public string path;
        public int quId;
        public BitArray answerMask;
        public string type;
        public string quizAnswers;
        public bool quizResult;

        public TestQuestionResultItem(string _path, int _quId, BitArray _answerMask,string _type,string _quizAnswers,bool _quizResult)
        {
            path = _path;
            quId = _quId;
            answerMask = _answerMask;
            type = _type;
            quizAnswers = _quizAnswers;
            quizResult = _quizResult;
        }

        public Object Clone()
        {
            return new TestQuestionResultItem(path, quId, answerMask,type,quizAnswers,quizResult);
        }

        public int GetAnswerAsInt()
        {
            int val = 0;
            for (int i = 0; i < answerMask.Count; ++i)
                if (answerMask[i])
                    val = val | (1 << i);
            return val;
        }

        public bool IsRight(int corrAnswer)
        {
            BitArray corrMask = new BitArray(new int[] { corrAnswer });
            for (int i = 0; i < answerMask.Length; ++i)
                if (corrMask[i] != answerMask[i])
                    return false;
            return true;
        }

        public override string ToString()
        {
            return String.Format("{0};{1};{2};{3};{4}", path, quId, GetAnswerAsInt().ToString(), quizAnswers,
                quizResult ? "1" : "0");
        }
    }
}
