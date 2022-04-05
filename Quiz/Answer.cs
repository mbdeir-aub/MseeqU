using Bdeir.Quizzer.Core;

namespace Bdeir.Quizzer
{
    public class Answer : IAnswer
    {
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
        public override string ToString()
        {
            return AnswerText;
        }
    }
}
