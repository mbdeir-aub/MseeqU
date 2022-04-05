using Bdeir.Quizzer.Core;
using System;
using System.Linq;

namespace Bdeir.Quizzer.QuestionPickers
{
    public class RandomQuestionPicker : BaseQuestionPicker
    {
        Random rnd = new Random();

        public RandomQuestionPicker(IQuiz quiz, IStorage progressStorage, string progressFile, AutoRestartOptions autoRestartOptions, RepeatQuestionOptions repeatQuestionOptions) : base(quiz, progressStorage, progressFile, autoRestartOptions, repeatQuestionOptions)
        {
        }

        public override void Remove(int questionNumber)
        {
            UnsentNumbers.Remove(questionNumber);
            ProgressStorage.Write(ProgressFile, string.Join(',', UnsentNumbers));
        }

        public override IQuestion Next()
        {
            if (UnsentNumbers.Count == 0) return null;
            int QuestionIndexNumber = UnsentNumbers[rnd.Next(0, UnsentNumbers.Count)];
            var Question = base.Quiz.Questions.ElementAt(QuestionIndexNumber);
            return Question;
        }
    }
}