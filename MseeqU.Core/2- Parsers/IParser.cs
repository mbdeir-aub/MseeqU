using System.Collections.Generic;

namespace Bdeir.Quizzer.Core
{
    public interface IParser
    {
        (string Title, IEnumerable<IQuestion> Questions) Parse(string content);
        bool VerifyParams(string content);
        IParserConfig Config { get; }
    }
}