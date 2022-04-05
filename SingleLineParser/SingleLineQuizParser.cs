using Bdeir.Quizzer.Core;
using System.Collections.Generic;

namespace Bdeir.Quizzer.Parsers
{
    public class SingleLineQuizParser : BaseParser
    {
        public SingleLineQuizParser(IParserConfig config): base(config)
        {

        }
        public override (string Title, IEnumerable<IQuestion> Questions) Parse(string content)
        {
            var questions = new List<IQuestion>();
            if (!base.VerifyParams(content)) return (null, questions);// throw new System.Exception("content is empty/null or config is invalid");
            string title = string.Empty;
            var lines = content.Split('\n');
            int start = 0;
            if (!lines[0].StartsWith(Config.QuestionPrefix))
            {
                title = lines[0];
                start = 1;
            }
            for (int i = start; i < lines.Length; i++)
            {
                string line = lines[i];

                if (line.StartsWith(Config.QuestionPrefix))
                {
                    var q = Question.CreateInstance(line.Remove(0, Config.QuestionPrefix.Length));
                    string answer = lines[++i];
                    do
                    {
                        bool correct = answer.StartsWith(Config.CorrectAnswerPrefix);
                        string answerPrompt = answer.Remove(0, Config.CorrectAnswerPrefix.Length);
                        q.AddAnswer(answerPrompt, correct);
                        if (i + 1 == lines.Length) break;
                        answer = lines[++i];
                    } while (answer.StartsWith(Config.WrongAnswerPrefix) || answer.StartsWith(Config.CorrectAnswerPrefix));
                    questions.Add(q);
                    --i;
                }
            }
            return (title, questions);
        }
    }
}