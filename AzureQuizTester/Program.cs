using Bdeir.Quizzer.Core;
using System;

namespace QuizTester
{
    class Program
    {
        const AutoRestartOptions autoRestart           = AutoRestartOptions.AutoRestart;
        const RepeatQuestionOptions questionsCanRepeat = RepeatQuestionOptions.QuestionsCanRepeat;
        static readonly string FileNameAndPath                   = Environment.GetEnvironmentVariable("QuizFile");
        const string QuestionPrefix                    = "*";
        const string CorrectAnswerPrefix               = "=";
        const string WrongAnswerPrefix                 = "-";

        static readonly string AzureStorageConnectionString = Environment.GetEnvironmentVariable("BotAzureStorageConnectionString");
        static readonly string AzureStorageContainerName = Environment.GetEnvironmentVariable("BotAzureContainer");
        static void Main(string[] args)
        {
            var mcq = Bdeir.Quizzer.MseeqU.CreateAzureQuiz(FileNameAndPath, AzureStorageConnectionString, AzureStorageContainerName, QuestionPrefix, CorrectAnswerPrefix, WrongAnswerPrefix, autoRestart, questionsCanRepeat);
            IQuestion question = mcq.Next();
            Console.WriteLine($"{question.SequenceNumber}:{question.Prompt}");
        }
    }
}