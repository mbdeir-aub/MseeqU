namespace Bdeir.Quizzer.Core
{
    public abstract class BaseStorageConfig : IStorageConfig
    {
        public BaseStorageConfig(string fileName)
        {
            QuizFileEnvVar = fileName;
        }

        public string QuizFileEnvVar {get; protected set;}
    }
}
