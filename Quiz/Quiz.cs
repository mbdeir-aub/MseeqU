using Bdeir.Quizzer.Core;
using System.Collections.Generic;

namespace Bdeir.Quizzer
{
    public class Quiz : IQuiz
    {
        public Quiz(string title, IEnumerable<IQuestion> questions)
        {
            this.Title = title;
            this.Questions = questions;
            Question.SeqNum = 0; // reset it each time a Question is created, issue #7

        }
        public string Title { get; private set; }

        public IEnumerable<IQuestion> Questions { get; private set; }
    }
}
