using System.Collections.Generic;

namespace Bdeir.Quizzer.Core
{
    public enum AutoRestartOptions
    {
        /// <summary>
        /// Restart the quiz after the last question is picked.
        /// </summary>
        AutoRestart,
        /// <summary>
        /// Stop picking questions after the last question is picked.
        /// </summary>
        StopAfterLastQuestion
    }
    public enum RepeatQuestionOptions
    {
        /// <summary>
        /// Send each unique question once only.
        /// </summary>
        UniqueQuestions,
        /// <summary>
        /// The same question can be sent more than one time.
        /// </summary>
        QuestionsCanRepeat

    }
    public enum AnswersOrder
    {
        ShuffledAnswers,
        KeepAnswersInOrder
    }
    public interface IQuestionPicker
    {
        IQuestion Next();
        bool HasNext();
        void Remove(int questionNumber);
        List<int> UnsentNumbers { get; }
        IStorage ProgressStorage { get; }
        string ProgressFile { get; }
        IQuiz Quiz { get; }
        AutoRestartOptions AutoRestartOptions { get; }
        RepeatQuestionOptions RepeatQuestionOptions { get; }
    }
}