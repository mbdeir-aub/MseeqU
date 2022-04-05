using Bdeir.Quizzer.Core;

namespace Bdeir.FileStorage
{
    public class AzureStorageConfig : BaseStorageConfig
    {
        public AzureStorageConfig(string connectionString, string containerName, string blobName) : base(blobName)
        {
            ConnectionString = connectionString;
            ContainerName = containerName;
        }

        public string ConnectionString {get; private set;}
        public string ContainerName { get; private set; }
    }
}
