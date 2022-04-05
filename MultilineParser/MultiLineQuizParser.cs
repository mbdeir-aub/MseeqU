using Bdeir.Quizzer.Core;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bdeir.Quizzer.Parsers
{
    public class MultiLineQuizParser : BaseParser
    {
        public MultiLineQuizParser(IParserConfig config) : base(config)
        {

        }
        public override (string Title, IEnumerable<IQuestion> Questions) Parse(string content)
        {
            if (!base.VerifyParams(content)) throw new System.Exception("content is empty/null or config is invalid");
            var questions = new List<IQuestion>();

            foreach (string q in new Regex(@"^\*", RegexOptions.Multiline).Split(content))
            {
                if (string.IsNullOrWhiteSpace(q)) continue;
                int indexOfEqual = q.IndexOf("\r\f=");
                int indexOfMinus = q.IndexOf("\r\f-");
                int index = Math.Min(indexOfEqual, indexOfMinus);
                string prompt = q.Substring(0, index).Trim();
                var question = Question.CreateInstance(prompt);

                MatchCollection m=null;// = answerRegex.Matches(q);
                for (int i = 0; i < m.Count; i++)
                {
                    string answer = m[i].Value;
                    bool correct = answer.StartsWith(Config.CorrectAnswerPrefix);
                    string answerPrompt;
                    if (correct)
                    {
                        answerPrompt = answer.Remove(0, Config.CorrectAnswerPrefix.Length).Trim();
                    }
                    else
                    {
                        answerPrompt = answer.Remove(0, Config.WrongAnswerPrefix.Length).Trim();
                    }
                    question.AddAnswer(answerPrompt, correct);
                }
                questions.Add(question);
            }
            return (null, questions);
        }
    }
}