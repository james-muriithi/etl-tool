using System;
using System.IO;
using System.Text;

namespace EtlTool
{
    public class CustomerCsv : IFile
    {
        // - decode text from base64 to readable text;
        public string decode(string EncodedText)
        {
            byte[] data = Convert.FromBase64String(EncodedText);
            return Encoding.UTF8.GetString(data);
        }

        // read data from the csv
        public void read(string path)
        {
            var EncodedText = File.ReadAllText(path);
            var DecodedText = decode(EncodedText);
            Console.WriteLine(DecodedText);

            // - parse data so you have rows with column names;
            // - map data to an oobject;


        }
    }
}
