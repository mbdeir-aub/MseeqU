# MseeqU
A quiz maker created as a case study for CMPS 252 Software Construction during the Fall semester of 2021-2022 at the American University of Beirut.

## To get started creating a quiz from a local file:
1. Create a [Telegram Bot](https://core.telegram.org/bots#6-botfather)
   - Get the `Bot Token` and store it in an environment variable called `TelegramBot_Token`
2. Create a *public* Telegram Channel, set the unique `t.me/` link
   - Get the `Channel Id` and store it in an environment variabled called `TelegramBot_ChannelId`. The `Channel Id` is the string you added after `t.me/` preceded with @, example: `@mychannel`
3. Create a quiz file. [Example](/sampleQuiz.txt)

```c#
using Bdeir.Quizzer;
using Bdeir.Quizzer.Bots;
using Bdeir.Quizzer.Core;
using System;

namespace MseeqU_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IQuestionPicker quiz = MseeqU.CreateLocalQuiz(
                  "SampleQuiz.txt"
                , "*" // question prefix
                , "=" // correct answer prefix
                , "-" // incorrect answer prefix
                , AutoRestartOptions.StopAfterLastQuestion
                , RepeatQuestionOptions.UniqueQuestions
            );

            while (quiz.HasNext())
            {
                IQuestion question = quiz.Next();
                TelegramBot telegram = new TelegramBot(
                       Environment.GetEnvironmentVariable("TelegramBot_Token")
                     , Environment.GetEnvironmentVariable("TelegramBot_ChannelId")
                );
                if (telegram.SendQuestion(question) != null)
                {
                    quiz.Remove(question.SequenceNumber);
                }
            }
        }
    }
}
```

## To create an Azure Storage based quiz:
```c#
            IQuestionPicker quiz = MseeqU.CreateAzureQuiz(
                Environment.GetEnvironmentVariable("Storage_QuizFile")
                , Environment.GetEnvironmentVariable("Storage_ConnectionString")
                , Environment.GetEnvironmentVariable("Storage_Container")
                , "*"
                , "="
                , "-"
                , AutoRestartOptions.StopAfterLastQuestion
                , RepeatQuestionOptions.UniqueQuestions
            );
```
