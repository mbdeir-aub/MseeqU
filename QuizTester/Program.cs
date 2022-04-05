using Bdeir.Quizzer.Core;
using System;

namespace QuizTester
{
    class Program
    {
        const AutoRestartOptions autoRestart = AutoRestartOptions.AutoRestart;
        const RepeatQuestionOptions questionsCanRepeat = RepeatQuestionOptions.QuestionsCanRepeat;

        static readonly string FileNameAndPath = Environment.GetEnvironmentVariable("QuizFile");
        const string QuestionPrefix = "*";
        const string CorrectAnswerPrefix = "=";
        const string WrongAnswerPrefix = "-";
        static void Main(string[] args)
        {
            var mcq = Bdeir.Quizzer.MseeqU.CreateLocalQuiz(FileNameAndPath, QuestionPrefix, CorrectAnswerPrefix, WrongAnswerPrefix, autoRestart, questionsCanRepeat);
            IQuestion question = mcq.Next();
            Console.WriteLine($"{question.SequenceNumber}:{question.Prompt}");
        }
    }
}

