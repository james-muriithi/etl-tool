using Cryptography;
using System;
using System.IO;
using Reader;

namespace EtlTool
{
    partial class Program
    {
        static void Main(string[] args)
        {
            // Suggestion here is to come up with the plan first, for example:

            // check if arguments were provided

            if(args.Length > 0)
            {
                // initialize a Base64Decoder object
                var base64Decoder = new Base64Decoder();

                // Feel free to extand or rewrite this plan.
                var customerCsvPath = @args[0];
                if (File.Exists(customerCsvPath))
                {
                    var customerCsvReader = new CsvReader(base64Decoder);
                    var customerCsv = new CustomerData(customerCsvReader);
                    customerCsv.Read(customerCsvPath);
                }

                var tasksCsvPath = @args[1];
                if (File.Exists(tasksCsvPath))
                {
                    var tasksFileReader = new CsvReader(base64Decoder);
                    var tasksData = new TaskData(tasksFileReader);
                    tasksData.Read(tasksCsvPath);
                }
            }
            else
            {
                Console.WriteLine("please provide file path arguments");
            }
        }
    }
}
