#define SINGLE_LINE_PARSER
using Bdeir.FileStorage;
using Bdeir.Quizzer.Core;
using Bdeir.Quizzer.Parsers;
using Bdeir.Quizzer.QuestionPickers;
using System.Linq;

namespace Bdeir.Quizzer
{
    public class MseeqU
    {
        public static IQuestionPicker CreateAzureQuiz(IStorageConfig storageConfig, IParserConfig parserConfig, AutoRestartOptions autoRestartOptions, RepeatQuestionOptions repeatQuestionOptions, AnswersOrder randomizeAnswers = AnswersOrder.ShuffledAnswers)
        {
            AzureStorageConfig azureStorageConfig = (AzureStorageConfig)storageConfig;
            IStorage quizStorage = new AzureStorage(azureStorageConfig);
#if SINGLE_LINE_PARSER
            IParser parser = new SingleLineQuizParser(parserConfig);
#else
            IParser parser = new MultiLineQuizParser(parserConfig);
#endif
            IQuiz quiz = new QuizFactory().CreateQuizFromFile(azureStorageConfig.QuizFileEnvVar, quizStorage, parser);
            string ProgressFileName = null;
            IStorage progressStorage = null;
            if (quiz.Questions.Count() > 1)
            {
                ProgressFileName = azureStorageConfig.QuizFileEnvVar + ".progress";
                progressStorage = new AzureStorage((AzureStorageConfig)storageConfig);
            }
            IQuestionPicker picker = new RandomQuestionPicker(quiz, progressStorage, ProgressFileName, autoRestartOptions, repeatQuestionOptions);
            return picker;
        }

        public static IQuestionPicker CreateLocalQuiz(IStorageConfig storageConfig, IParserConfig parserConfig, AutoRestartOptions autoRestartOptions, RepeatQuestionOptions repeatQuestionOptions, AnswersOrder randomizeAnswers = AnswersOrder.ShuffledAnswers)
        {
            LocalStorageConfig localStorageConfig = (LocalStorageConfig)storageConfig;
            IStorage quizStorage = new LocalStorage(localStorageConfig);
#if SINGLE_LINE_PARSER
            IParser parser = new SingleLineQuizParser(parserConfig);
#else
            IParser parser = new MultiLineQuizParser(parserConfig);
#endif
            IQuiz quiz = new QuizFactory().CreateQuizFromFile(localStorageConfig.QuizFileEnvVar, quizStorage, parser);
            string ProgressFileName = null;
            IStorage progressStorage = null;
            if (quiz.Questions.Count() > 1)
            {
                ProgressFileName = localStorageConfig.QuizFileEnvVar + ".progress";
                progressStorage = new LocalStorage((LocalStorageConfig)storageConfig);
            }
            IQuestionPicker picker = new RandomQuestionPicker(quiz, progressStorage, ProgressFileName, autoRestartOptions, repeatQuestionOptions);
            return picker;
        }

        public static IQuestionPicker CreateAzureQuiz(string fileName, string azureStorageConnectionString, string azureStorageContainerName, string questionPrefix, string correctAnswerPrefix, string wrongAnswerPrefix, AutoRestartOptions autoRestartOptions, RepeatQuestionOptions repeatQuestionOptions)
        =>
            CreateAzureQuiz(
                new AzureStorageConfig(azureStorageConnectionString, azureStorageContainerName, fileName)
                , new ParserConfig(questionPrefix, correctAnswerPrefix, wrongAnswerPrefix)
                , autoRestartOptions
                , repeatQuestionOptions
            );

        public static IQuestionPicker CreateLocalQuiz(string fileName, string questionPrefix, string correctAnswerPrefix, string wrongAnswerPrefix, AutoRestartOptions autoRestartOptions, RepeatQuestionOptions repeatQuestionOptions)
        =>
            CreateLocalQuiz(
                new LocalStorageConfig(fileName)
                , new ParserConfig(questionPrefix, correctAnswerPrefix, wrongAnswerPrefix)
                , autoRestartOptions
                , repeatQuestionOptions
            );
    }
}