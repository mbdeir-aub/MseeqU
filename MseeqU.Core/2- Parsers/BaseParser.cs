using System.Collections.Generic;

namespace Bdeir.Quizzer.Core
{
    public abstract class BaseParser : IParser
    {
        public BaseParser(IParserConfig config)
        {
            this.Config = config;
        }
        public IParserConfig Config { get; protected set; }
        public abstract (string Title, IEnumerable<IQuestion> Questions) Parse(string content);
        public bool VerifyParams(string content)
        {
            if (string.IsNullOrEmpty(content)) return false;
            if (string.IsNullOrEmpty(Config.QuestionPrefix)) return false;
            if (string.IsNullOrEmpty(Config.CorrectAnswerPrefix)) return false;
            if (string.IsNullOrEmpty(Config.WrongAnswerPrefix)) return false;
            return true;
        }
    }
}