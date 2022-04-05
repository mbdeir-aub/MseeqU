#define LIVE
using Bdeir.Quizzer.Bots;
using Bdeir.Quizzer.Core;
using System;
using System.Linq;

namespace QuizzerBot
{
    class Program
    {
        static readonly string QUIZ_FILE = Environment.GetEnvironmentVariable("QuizFile");
        static readonly string AZURE_STORAGE_CONNECTION_STRING = Environment.GetEnvironmentVariable("BotAzureStorageConnectionString");
        static readonly string AZURE_CONTAINER_NAME = Environment.GetEnvironmentVariable("BotAzureContainer");

        static readonly string BOT_TOKEN = Environment.GetEnvironmentVariable("BotToken");
        static readonly string BOT_CHANNEL_ID = Environment.GetEnvironmentVariable("BotChannelId");

        const AutoRestartOptions AUTO_RESTART = AutoRestartOptions.AutoRestart;
        const RepeatQuestionOptions QUESTIONS_CAN_REPEAT = RepeatQuestionOptions.QuestionsCanRepeat;

        const string QUESTION_PREFIX = "*";
        const string CORRECT_ANSWER_PREFIX = "=";
        const string WRONG_ANSWER_PREFIX = "-";
        static void Main(string[] args)
        {
            var mcq = Bdeir.Quizzer.MseeqU.CreateAzureQuiz(QUIZ_FILE, AZURE_STORAGE_CONNECTION_STRING, AZURE_CONTAINER_NAME, QUESTION_PREFIX, CORRECT_ANSWER_PREFIX, WRONG_ANSWER_PREFIX, AUTO_RESTART, QUESTIONS_CAN_REPEAT);
            IQuestion question = mcq.Next();
#if LIVE
            TelegramBot bot = new TelegramBot(BOT_TOKEN, BOT_CHANNEL_ID);
            if (bot.SendQuestion(question) != null)
            {
                mcq.Remove(question.SequenceNumber);
            }
#else
            Console.WriteLine($"{question.SequenceNumber}: {question.Prompt}");
            Console.WriteLine(question.ShuffledAnswers.Where(p=>p.Correct));
#endif
        }
    }
}
