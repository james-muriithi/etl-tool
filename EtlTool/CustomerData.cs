using Reader;

namespace EtlTool
{
    public class CustomerData
    {
        private readonly IFileReader _fileReader;

        public CustomerData(IFileReader fileReader)
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
