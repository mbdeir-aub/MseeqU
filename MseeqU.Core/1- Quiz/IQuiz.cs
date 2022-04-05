using System.Collections.Generic;

namespace Bdeir.Quizzer.Core
{
    public interface IQuiz
    {
        string Title { get; }
        IEnumerable<IQuestion> Questions { get; }
    }
}