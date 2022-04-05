#define SEND_BOT_MESSAGE
using Bdeir.Quizzer.Bots;
using Bdeir.Quizzer.Core;
using System;

namespace QuizzerBot
{
    class Program
    {
        static readonly string BOT_TOKEN      = Environment.GetEnvironmentVariable("BotToken");
        static readonly string BOT_CHANNEL_ID = Environment.GetEnvironmentVariable("BotChannelId");
        static readonly string MseeqU_BOT_CHANNEL_ID = Environment.GetEnvironmentVariable("MseeqU_BOT_CHANNEL_ID");

        const AutoRestartOptions AUTO_RESTART            = AutoRestartOptions.AutoRestart;
        const RepeatQuestionOptions QUESTIONS_CAN_REPEAT = RepeatQuestionOptions.QuestionsCanRepeat;

        const string FILENAME_AND_PATH = @"C:\Trash\mapfilter.txt"; //@"C:\Trash\sample.qa";// Environment.GetEnvironmentVariable("QuizFile");
        const string QUESTION_PREFIX       = "*";
        const string CORRECT_ANSWER_PREFIX = "=";
        const string WRONG_ANSWER_PREFIX   = "-";
        static void Main(string[] args)
        {
            var mcq = Bdeir.Quizzer.MseeqU.CreateLocalQuiz(FILENAME_AND_PATH, QUESTION_PREFIX, CORRECT_ANSWER_PREFIX, WRONG_ANSWER_PREFIX, AUTO_RESTART, QUESTIONS_CAN_REPEAT);
            IQuestion question = mcq.Next();
#if SEND_BOT_MESSAGE
            TelegramBot bot = new TelegramBot(BOT_TOKEN, MseeqU_BOT_CHANNEL_ID);
            if (bot.SendQuestion(question) != null)
            {
                mcq.Remove(question.SequenceNumber);
            } 
            #else
            Console.WriteLine($"{question.SequenceNumber}: {question.Prompt}");
#endif
        }
    }
}
