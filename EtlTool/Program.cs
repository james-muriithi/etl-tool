using Cryptography;
using System;
using System.IO;
using Reader;

namespace EtlTool
{
    partial class Program
    {
        const string ConnectionString = "server=localhost;port=3306;database=etl_tool_1;uid=root;password=";
        private const int IndexOfPathToCustomerCsvFile = 0;
        private const int NumberOfRequiredArguments = 2;

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
            // Now the interesting part:
            // To avoid big parts of code be nested inside curly brackets of some expression,
            // you can invert the 'if' statement:  
            
            // Same here. Constant will make another developer to understand what you mean by this line.
            if (args.Length < NumberOfRequiredArguments) // And you see? Now this constant makes even more sense! 
            {
                Console.WriteLine("please provide file path arguments");
                return;
            }

            // initialize a Base64Decoder object
            var base64Decoder = new Base64Decoder();

            // To make code more readable it is good to avoid "magic numbers". If you have to use
            // some hardcoded value in your code - best practice would be to put inside constant.
            var customerCsvPath = @args[IndexOfPathToCustomerCsvFile];
            if (File.Exists(customerCsvPath))
            {
                // Another important thing: I see that you used the Strategy Pattern by providing
                // decoder to CsvReader and CsvReader to CustomerData. Very well but has some issues.
                // In current situation it violates the Single Responsibility Principal because now
                // the CsvReader not only reads the CSV files, but also decodes them first using provided decoder.
                // 
                // Same for Read method of CustomerData - it actually does everything :) It decodes read files,
                // then extracts rows from CSV and only then reads customer data... AND then saves it to database!
                // Well at least four different and not related to each other things are going on in one place...
                //
                // Going back to Strategy. Fix is simple: Strategy should be used on the level of Program class. 
                // You can pick different implementations hidden in some methods and classes based on user input.
                // E.g. use different decryption or parsing algorithms based on provided arguments.
                
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
    }
}
