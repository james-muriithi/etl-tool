using Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using Parser;

namespace EtlTool
{
    partial class Program
    {
        const string ConnectionString = "server=localhost;port=3306;database=etl_tool_1;uid=root;password=";
        private const int IndexOfPathToCustomerCsvFile = 0;
        private const int IndexOfPathToTaskCsvFile = 1;
        private const int NumberOfRequiredArguments = 2;

        static void Main(string[] args)
        {
            if (args.Length < NumberOfRequiredArguments)
            {
                Console.WriteLine("please provide file path arguments");
                return;
            }
            
            // WARNING! Argument indexes are on random positions. Consider implement a smart arguments picker.

            var path = args[1];
            var encryptedData = FileReader.Read(path);
            var decryptedData = Decode(args[3], encryptedData);
            var parsedData = Parse(args[4], decryptedData);
            SaveToDatabase(args[0], parsedData);
        }

        public static string Decode(string algorithm, string encodedData)
        {
            var decoder = (IDecoder)null;
            
            switch (algorithm.ToLower())
            {
                case "base64":
                    decoder = new Base64Decoder();
                    break;
                default:
                    throw new ArgumentException(String.Format("{0} is not a supported algorithm", algorithm), "algorithm");
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
                default:
                    throw new NotSupportedException();
            }

            return parser.Parse(decryptedData);
        }

        private static void SaveToDatabase(string entityType, List<List<string>> parsedData)
        {
            if (entityType == "--customer")
            {
                var customerModels = CustomerData.MapToModel(parsedData);
                CustomerData.SaveToDatabase(customerModels);
            }
            else if (entityType == "--task")
            {
                var tasksModels = TaskData.MapToModel(parsedData);
                TaskData.SaveToDatabase(tasksModels);
            }
            else if (entityType == "--company") 
                throw new NotImplementedException();
        }
    }
}
