#define LIVENOT
using Bdeir.FileStorage;
using Bdeir.Quizzer.Bots;
using Bdeir.Quizzer.Core;
using Bdeir.Quizzer.Parsers;
using System;
using System.Linq;

namespace QuizzerBot
{
    class Program
    {
        static  string QUIZ_FILE = Environment.GetEnvironmentVariable("QuizFile");
        static readonly string AZURE_STORAGE_CONNECTION_STRING = Environment.GetEnvironmentVariable("Storage_ConnectionString");
        static readonly string AZURE_CONTAINER_NAME = Environment.GetEnvironmentVariable("Storage_Container");

        static readonly string BOT_TOKEN = Environment.GetEnvironmentVariable("TelegramBot_ChannelId");
        static readonly string BOT_CHANNEL_ID = Environment.GetEnvironmentVariable("BotChannelId");

        const AutoRestartOptions AUTO_RESTART = AutoRestartOptions.AutoRestart;
        const RepeatQuestionOptions QUESTIONS_CAN_REPEAT = RepeatQuestionOptions.QuestionsCanRepeat;

        const string QUESTION_PREFIX = "*";
        const string CORRECT_ANSWER_PREFIX = "=";
        const string WRONG_ANSWER_PREFIX = "-";
        static void Main(string[] args)
        {

            #region Method 1
            QUIZ_FILE = "2022-04-12.txt";
            IQuestionPicker mcq = Bdeir.Quizzer.MseeqU.CreateAzureQuiz(QUIZ_FILE, AZURE_STORAGE_CONNECTION_STRING, AZURE_CONTAINER_NAME, QUESTION_PREFIX, CORRECT_ANSWER_PREFIX, WRONG_ANSWER_PREFIX, AUTO_RESTART, QUESTIONS_CAN_REPEAT);
            IQuestion question = mcq.Next(); 
            #endregion

            #region Method 2
            var storageConfig = new AzureStorageConfig(AZURE_STORAGE_CONNECTION_STRING, AZURE_CONTAINER_NAME, QUIZ_FILE);
            var parserConfig = new ParserConfig(QUESTION_PREFIX, CORRECT_ANSWER_PREFIX, WRONG_ANSWER_PREFIX);
            var storage = new AzureStorage(storageConfig);
            IQuiz quiz = new QuizFactory().CreateQuizFromFile(QUIZ_FILE, storage, new SingleLineQuizParser(parserConfig));
            string destination = $"completed/{37,3:D3}- {QUIZ_FILE}";
            storage.Move(QUIZ_FILE, destination);
            #endregion
#if LIVE
            TelegramBot bot = new TelegramBot(BOT_TOKEN, BOT_CHANNEL_ID);
            if (bot.SendQuestion(question) != null)
            {
                mcq.Remove(question.SequenceNumber);
            }
#else
            Console.WriteLine($"{question.SequenceNumber}: {question.Prompt}");
            Console.WriteLine(question.ShuffledAnswers.Where(p => p.Correct));
#endif
        }
    }
}
