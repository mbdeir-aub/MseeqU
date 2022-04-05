using Bdeir.Quizzer.Core;
using System.IO;

namespace Bdeir.FileStorage
{
    public class LocalStorage : BaseStorage
    {
        public LocalStorage(LocalStorageConfig config) : base(config)
        {
        }

        public override void Delete(string fileName)
        {
            File.Delete(fileName);
        }

        public override bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }

        public override void Move(string fileName, string destination)
        {
            File.Move(fileName, destination);
        }

        public override string Read(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public override void Write(string fileName, string content)
        {
            File.WriteAllText(fileName, content);
        }
    }
}