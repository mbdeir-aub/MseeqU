namespace Bdeir.Quizzer.Core
{
    public class QuizFactory : IQuizFactory
    {
        public IQuiz CreateQuizFromFile(string file, IStorage storage, IParser parser)
        {
            string contents = storage.Read(file);
            return CreateQuizFromString(contents, storage, parser);
        }
        public IQuiz CreateQuizFromBytes(byte[] bytes, IStorage storage, IParser parser)
        {
            string contents = System.Text.Encoding.UTF8.GetString(bytes);
            return CreateQuizFromString(contents, storage, parser);
        }

        public IQuiz CreateQuizFromString(string contents, IStorage storage, IParser parser)
        {
            var (Title, Questions) = parser.Parse(contents);
            return new Quiz(Title, Questions);
        }
    }
}