using System.Collections.Generic;

namespace Bdeir.Quizzer.Core
{
    public interface IQuestion
    {
        IEnumerable<IAnswer> Answers { get; }
        List<IAnswer> ShuffledAnswers { get; }
        IAnswer CorrectAnswer { get; }
        string Prompt { get; }
        int SequenceNumber { get; }
        void AddAnswer(string answer, bool correct);
    }
}