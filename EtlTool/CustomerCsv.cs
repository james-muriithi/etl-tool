using System;
using System.IO;
using System.Text;

namespace EtlTool
{
    public class CustomerCsv : IFile
    {
        public string decode(string EncodedText)
        {
            byte[] data = Convert.FromBase64String(EncodedText);
            return Encoding.UTF8.GetString(data);
        }

        public void read(string path)
        {
            var EncodedText = File.ReadAllText(path);
            var DecodedText = decode(EncodedText);
            Console.WriteLine(DecodedText);
        }
    }
}
