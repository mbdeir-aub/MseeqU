namespace Bdeir.Quizzer.Core
{
    public interface IQuizFactory
    {
        IQuiz CreateQuizFromFile(string file, IStorage storage, IParser parser);
        IQuiz CreateQuizFromString(string contents, IStorage storage, IParser parser);
        IQuiz CreateQuizFromBytes(byte[] bytes, IStorage storage, IParser parser);
    }
}