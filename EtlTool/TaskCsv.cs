using Reader;
using Cryptography;

namespace EtlTool
{
    public class TaskCsv
    {
        private readonly IFileReader _fileReader;

        public TaskCsv(IFileReader fileReader)
        {
            this._fileReader = fileReader;
        }

        // read data from the csv
        public void Read(string path)
        {
            _fileReader.Read(path);
        }
    }
}
