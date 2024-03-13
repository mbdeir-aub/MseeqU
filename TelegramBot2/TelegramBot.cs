using Bdeir.Quizzer.Core;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bdeir.Quizzer.Bots
{
    public class TelegramBot
    {
        ITelegramBotClient botClient;
        string botChannelId;
        public TelegramBot(string botToken, string botChannelId)
        {
            botClient = new TelegramBotClient(botToken);
            this.botChannelId = botChannelId;
            
        }
        public Message SendQuestion(IQuestion q)
        {
            return botClient.SendPollAsync(
                 chatId         : this.botChannelId
                ,type           : Telegram.Bot.Types.Enums.PollType.Quiz
                ,question       : q.Prompt
                ,options        : q.ShuffledAnswers.Select(p => p.AnswerText)
                ,correctOptionId: q.ShuffledAnswers.FindIndex(p => p.Correct)
            ).Result;
        }
        public Message SendQuestion(IQuestion q, int questionNumber)
        {
            return botClient.SendPollAsync(
                 chatId: this.botChannelId
                , type: Telegram.Bot.Types.Enums.PollType.Quiz
                , question: $"({ToIndic(questionNumber)}). " + q.Prompt
                , options: q.ShuffledAnswers.Select(p => p.AnswerText)
                , correctOptionId: q.ShuffledAnswers.FindIndex(p => p.Correct)
            ).Result;
            
        }
        public Message SendText(string text)
        {
            return botClient.SendTextMessageAsync(chatId: this.botChannelId, text: text).Result;
        }
        public static string ToIndic(int num)
        {
            string s = string.Empty;
            num.ToString().ToList().ForEach(c => s += (char)(0x660 + (int)c - 48));
            return s;
        }
    }
}