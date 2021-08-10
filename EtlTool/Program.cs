using Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using Parser;

namespace EtlTool
{
    partial class Program
    {
        static void Main(string[] args)
        {            
            // WARNING! Argument indexes are on random positions. Consider implement a smart arguments picker.

            var path = GetArgument(args, "--path");

            if (String.IsNullOrEmpty(path))
            {
                Console.WriteLine("please provide a path to the file e.g --path C:\\customers.csv");
                return;
            }

            var entityType = GetArgument(args, "--entity");

            if (String.IsNullOrEmpty(entityType))
            {
                Console.WriteLine("please provide an entity type e.g --entity customer");
                return;
            }

            var encryptionAlgorithm = GetArgument(args, "--enc");
            var fileType = GetArgument(args, "--f");

            var encryptedData = FileReader.Read(path).Trim();
            var decryptedData = Decode(encryptionAlgorithm, encryptedData).Trim();
            var parsedData = Parse(fileType, decryptedData);
            SaveToDatabase(entityType, parsedData);
        }

        public static string Decode(string algorithm, string encodedData)
        {
            var decoder = new Base64Decoder();
            
            switch (algorithm)
            {
                case "base64":
                    decoder = new Base64Decoder();
                    break;
                default:
                    return encodedData;
            }
            
            return decoder.Decode(encodedData);
        }

        private static List<List<string>> Parse(string fileFormat, string decryptedData)
        {
            IFileParser parser = new CsvParser();
            switch (fileFormat)
            {
                case "csv":
                    parser = new CsvParser();
                    break;
                case "tsv":
                    parser = new TsvParser();
                    break;
            }

            return parser.Parse(decryptedData);
        }

        private static void SaveToDatabase(string entityType, List<List<string>> parsedData)
        {
            if (entityType == "customer")
            {
                var customerModels = CustomerData.MapToModel(parsedData);
                CustomerData.SaveToDatabase(customerModels);
            }
            else if (entityType == "task")
            {
                var tasksModels = TaskData.MapToModel(parsedData);
                TaskData.SaveToDatabase(tasksModels);
            }
            else if (entityType == "--company") 
                throw new NotImplementedException();
        }

        static string GetArgument(IEnumerable<string> args, string option)
        {
            return args.SkipWhile(i => i != option)
                .Skip(1)
                .Take(1)
                .FirstOrDefault();
        }
    }
}
