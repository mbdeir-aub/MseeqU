namespace Bdeir.Quizzer.Core
{
    public class ParserConfig : IParserConfig
    {
        public ParserConfig(string question_prefix, string correct_answer_prefix, string wrong_answer_prefix)
        {
            QuestionPrefix = question_prefix;
            CorrectAnswerPrefix = correct_answer_prefix;
            WrongAnswerPrefix = wrong_answer_prefix;
        }

        public string QuestionPrefix {get; private set; }

        public string CorrectAnswerPrefix {get; private set; }

        public string WrongAnswerPrefix {get; private set; }
    }
}
