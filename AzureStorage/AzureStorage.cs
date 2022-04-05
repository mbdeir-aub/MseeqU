using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Bdeir.Quizzer.Core;
using System;
using System.IO;
using System.Text;

namespace Bdeir.FileStorage
{
    public class AzureStorage : BaseStorage
    {
        private readonly BlobContainerClient container;

        public AzureStorage(AzureStorageConfig config) : base(config)
        {
            container = new BlobContainerClient(config.ConnectionString, config.ContainerName);
            container.CreateIfNotExists();
        }
        /// <summary>
        /// Uploads a file to Azure Blob Storage
        /// </summary>
        /// <param name="blobName">The name of the blob on Azure Storage.</param>
        /// <param name="content">The content of the file.</param>
        public override void Write(string blobName, string content)
        {
            MemoryStream stream = new(Encoding.ASCII.GetBytes(content));
            stream.Position = 0;
            BlobClient blob = container.GetBlobClient(blobName);
            blob.DeleteIfExists();
            blob.Upload(stream);
        }
        public override string Read(string blobName)
        {
            Pageable<BlobItem> all = container.GetBlobs(BlobTraits.All);
            foreach (var item in all)
            {
                if (item.Name == blobName)
                {
                    MemoryStream stream = new();
                    BlobClient blob = container.GetBlobClient(blobName);
                    blob.DownloadTo(stream);
                    stream.Position = 0;
                    string str = new StreamReader(stream).ReadToEnd();
                    return str;
                }
            }
            Write(blobName, string.Empty);
            return string.Empty;

        }

        public override void Delete(string blobName)
        {
            BlobClient blob = container.GetBlobClient(blobName);
            blob.Delete();
        }
        public override void Move(string blobName, string destination)
        {
            var targetBlobClient = container.GetBlobClient(destination);
            var blobUri          = new Uri($"{container.Uri}/{blobName}");
            targetBlobClient.StartCopyFromUriAsync(blobUri);
        }

        public override bool Exists(string blobName)
        {
            Pageable<BlobItem> all = container.GetBlobs(BlobTraits.All);
            foreach (var item in all)
            {
                if (item.Name == blobName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
