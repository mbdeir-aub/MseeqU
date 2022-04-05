using Bdeir.Quizzer.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bdeir.Quizzer
{
    public class Question : IQuestion
    {
        static Random rnd = new Random();
        public string Prompt { get; protected set; }
        public IEnumerable<IAnswer> Answers { get; }
        private List<IAnswer> _ShuffledAnswers;
        public List<IAnswer> ShuffledAnswers
        {
            get
            {
                if (_ShuffledAnswers == null) _ShuffledAnswers = Answers.OrderBy(p => rnd.Next()).ToList();
                return _ShuffledAnswers;
            }
        }
        public void AddAnswer(string answer, bool correct) => ((List<Answer>)Answers).Add(new Answer() { AnswerText = answer, Correct = correct });
        //public Answer CorrectAnswer { get { Answers.First(p => p.Correct); } set {et; }

        public int SequenceNumber { get; protected set; }

        protected Answer _correctAnswer;
        public IAnswer CorrectAnswer => Answers.First(p => p.Correct);
        

        #region Constructors and Static Factory Methods
        internal static int SeqNum = 0; // allow Question to reset it each time a Question is created , issue #7
        protected Question(string prompt)
        {
            Prompt = prompt;
            Answers = new List<Answer>();
            SequenceNumber = SeqNum++;
        }
        public static Question CreateInstance(string prompt) => new(prompt);
        #endregion
        public override string ToString() => Prompt.Trim();
    }
}