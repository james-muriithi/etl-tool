using Cryptography;
using System;
using System.IO;
using Parser;

namespace EtlTool
{
    partial class Program
    {
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

            // initialize a csv parser
            var csvParser = new CsvParser();

            var customerCsvPath = @args[IndexOfPathToCustomerCsvFile];
            if (File.Exists(customerCsvPath))
            {
                var encryptedCustomerData = FileReader.Read(customerCsvPath);
                var decryptedCustomerData = Decode("base64", encryptedCustomerData);
                var customerParsedData = csvParser.Parse(decryptedCustomerData);
                var customerModels = CustomerData.MapToModel(customerParsedData);
                CustomerData.SaveToDatabase(customerModels);
            }

            var tasksCsvPath = @args[IndexOfPathToTaskCsvFile];
            if (File.Exists(tasksCsvPath))
            {
                var encryptedTasksData = FileReader.Read(tasksCsvPath);
                var decryptedTasksData = Decode("base64", encryptedTasksData);
                var tasksParsedData = csvParser.Parse(decryptedTasksData);
                var tasksModels = TaskData.MapToModel(tasksParsedData);
                TaskData.SaveToDatabase(tasksModels);
            }
        }

        public static string Decode(string algorithm, string encodedData)
        {
            switch (algorithm.ToLower())
            {
                case "base64":
                    // initialize a Base64Decoder object
                    var base64Decoder = new Base64Decoder();
                    return base64Decoder.Decode(encodedData);

                default:
                    throw new ArgumentException(String.Format("{0} is not a supported algorithm", algorithm), "algorithm");
            }
        }
    }
}
