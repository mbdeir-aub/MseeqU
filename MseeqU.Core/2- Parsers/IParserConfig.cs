namespace Bdeir.Quizzer.Core
{
    public interface IParserConfig
    {
        string QuestionPrefix { get; }
        string CorrectAnswerPrefix { get; }
        string WrongAnswerPrefix { get; }
    }
}
