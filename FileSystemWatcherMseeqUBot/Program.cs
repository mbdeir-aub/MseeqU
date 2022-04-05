#define LIVE
using Bdeir.Quizzer.Bots;
using Bdeir.Quizzer.Core;
using System;
using System.Diagnostics;
using System.IO;

namespace QuizzerBot
{
    class Program
    {
        static readonly string BOT_TOKEN = Environment.GetEnvironmentVariable("BotToken");
        //static readonly string MseeqU_BOT_CHANNEL_ID = "@mseequ";
        static readonly string TEST_BOT_CHANNEL_ID = "@test_890";

        const AutoRestartOptions AUTO_RESTART = AutoRestartOptions.AutoRestart;
        const RepeatQuestionOptions QUESTIONS_CAN_REPEAT = RepeatQuestionOptions.QuestionsCanRepeat;

        const string QUESTION_PREFIX = "*";
        const string CORRECT_ANSWER_PREFIX = "=";
        const string WRONG_ANSWER_PREFIX = "-";
        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory;
            if (args.Length > 0) path = args[0];
            if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                Console.WriteLine("Not a directory. Must pass a valid directory or blank for current directory.");
                return;
            }
            string watchedFolder = Path.Combine(path, "Send");
            Directory.CreateDirectory(watchedFolder);

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = watchedFolder;

            Console.WriteLine($"MseeqU is watching {watchedFolder}");
            Console.WriteLine("Type exit to exit");

            watcher.Filter = "*.txt";
            watcher.Created += Watcher_Created;
            watcher.EnableRaisingEvents = true;


            while (Console.ReadLine().ToLower() != "exit") ;
        }
        private static void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Created {e.FullPath}");

            var mcq = Bdeir.Quizzer.MseeqU.CreateLocalQuiz(e.FullPath, QUESTION_PREFIX, CORRECT_ANSWER_PREFIX, WRONG_ANSWER_PREFIX, AUTO_RESTART, QUESTIONS_CAN_REPEAT);
            IQuestion question = mcq.Next();
            string value = $"{question.SequenceNumber}: {question.Prompt}";
#if LIVE
            TelegramBot bot = new TelegramBot(BOT_TOKEN, TEST_BOT_CHANNEL_ID);
            if (bot.SendQuestion(question) != null)
            {
                mcq.Remove(question.SequenceNumber);
            } 
#else
            //Console.WriteLine(value);
            Debug.WriteLine(value);
#endif
        }
    }
}
