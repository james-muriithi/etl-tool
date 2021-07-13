using Reader;
using Cryptography;

namespace EtlTool
{
    public class TaskData
    {
        private readonly IFileReader _fileReader;

        public TaskData(IFileReader fileReader)
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
