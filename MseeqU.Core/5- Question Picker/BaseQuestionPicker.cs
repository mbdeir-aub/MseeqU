using System.Collections.Generic;
using System.Linq;

namespace Bdeir.Quizzer.Core
{
    public abstract class BaseQuestionPicker : IQuestionPicker
    {
        public BaseQuestionPicker(IQuiz quiz, IStorage progressStorage, string progressFile, AutoRestartOptions autoRestartOptions, RepeatQuestionOptions repeatQuestionOptions)
        {
            this.Quiz                  = quiz;
            this.ProgressStorage       = progressStorage;
            this.ProgressFile          = progressFile;
            this.AutoRestartOptions    = autoRestartOptions;
            this.RepeatQuestionOptions = repeatQuestionOptions;
            
            if (!progressStorage.Exists(progressFile) || (this.AutoRestartOptions== AutoRestartOptions.AutoRestart && string.IsNullOrWhiteSpace(progressStorage.Read(progressFile))))
            {
                progressStorage.Write(progressFile, string.Join(",", Enumerable.Range(0, quiz.Questions.Count())));
            }
            UnsentNumbers = new();
            progressStorage.Read(progressFile).Split(',').Where(p => !string.IsNullOrWhiteSpace(p)).ToList().ForEach(p => UnsentNumbers.Add(int.Parse(p)));
        }

        protected AnswersOrder RandomizeAnswers { get; }
        public List<int> UnsentNumbers { get; protected set; }

        public IStorage ProgressStorage { get; protected set; }

        public IQuiz Quiz { get; protected set; }

        public string ProgressFile { get; protected set; }
        public AutoRestartOptions AutoRestartOptions { get; protected set; }
        public RepeatQuestionOptions RepeatQuestionOptions { get; protected set; }
        public abstract IQuestion Next();

        public abstract void Remove(int questionNumber);
        public bool HasNext() => UnsentNumbers.Count > 0;

    }
}
