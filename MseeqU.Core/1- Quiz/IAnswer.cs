namespace Bdeir.Quizzer.Core
{
    public interface IAnswer
    {
        string AnswerText { get; set; }
        bool Correct { get; set; }
    }
}