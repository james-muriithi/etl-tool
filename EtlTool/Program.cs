﻿using Cryptography;
using System;
using System.IO;
using Reader;

namespace EtlTool
{
    partial class Program
    {
        const string ConnectionString = "server=localhost;port=3306;database=etl_tool_1;uid=root;password=";

        // Ok, lets start with code structuring. Right now Main method is
        // a bit messy and has some duplications. Ok for small amount of code,
        // but could become a big problem over time.
        //
        // Take a look on the example below:
        //
        //      static void Main(string[] args)
        //      {
        //          var encryptedData = ReadFile(args.GetFilePath());
        //          var decryptedData = Decrypt(encryptedData);
        //          var data = Parse(decryptedData);
        //          var model = MapToModel(data);
        //          SaveToDatabase(model);
        //      }
        //
        // Do you agree that this method is much more easy to read and understand?
        // 
        // It's just an example, but it has very important idea: each line has only one
        // responsibility and data flows from one method to another transforming and changing
        // it's form on the way. That's called SRP - Single Responsibility Principle.
        // 
        // Another very important thing in the example is that we hide the implementation.
        // E.g. if we change anything inside method Parse() - the remaining code will stay
        // the same and that is extremely good!
        //
        // We can improve our code even more. Imagine our application supports different
        // encryption algorithms. In this case we can improve our Decrypt method like this:
        //
        //      static string Decrypt(string algorithm, string encryptedData) { ... }
        //
        // The output type is String and it will always be it which means that outside
        // world doesn't depend on algorithm used inside Decrypt method.
        // 
        // Same for other methods. If we provide single interface for input and output, the
        // things that depend on this input or output will not change even if we change the
        // implementation of the method they use with this input and ouput.


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

                // var context = new EtlToolDbContext(ConnectionString);
            }
            else
            {
                Console.WriteLine("please provide file path arguments");
            }
        }
    }
}
