using Bdeir.Quizzer.Core;
using System;

namespace QuizTester
{
    class Program
    {
        const AutoRestartOptions autoRestart                     = AutoRestartOptions.AutoRestart;
        const RepeatQuestionOptions questionsCanRepeat           = RepeatQuestionOptions.QuestionsCanRepeat;
        static readonly string QuestionPrefix                    = Environment.GetEnvironmentVariable("Quiz_QuestionPrefix");
        static readonly string CorrectAnswerPrefix               = Environment.GetEnvironmentVariable("Quiz_CorrectQuestionPrefix");
        static readonly string WrongAnswerPrefix                 = Environment.GetEnvironmentVariable("Quiz_WrongQuestionPrefix");
        static readonly string AzureStorageConnectionString      = Environment.GetEnvironmentVariable("Storage_ConnectionString");
        static readonly string AzureStorageContainerName         = Environment.GetEnvironmentVariable("Storage_Container");
        static string quizFile                            = Environment.GetEnvironmentVariable("Storage_QuizFile");
        
        static void Main(string[] args)
        {
            if (string.IsNullOrWhiteSpace(quizFile)) quizFile = "2022-04-12.txt";
            var mcq = Bdeir.Quizzer.MseeqU.CreateAzureQuiz(quizFile, AzureStorageConnectionString, AzureStorageContainerName, QuestionPrefix, CorrectAnswerPrefix, WrongAnswerPrefix, autoRestart, questionsCanRepeat);
            IQuestion question = mcq.Next();
            Console.WriteLine($"{question.SequenceNumber}:{question.Prompt}");
        }
    }
}