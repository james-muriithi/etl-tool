using System;

namespace EtlTool
{
    class Program
    {
        static void Main(string[] args)
        {
            // Suggestion here is to come up with the plan first, for example:
            // - read file content;
            // - decode it from base64 to readable text;
            // - parse data so you have rows with column names;
            // - map data to an oobject;
            // Feel free to extand or rewrite this plan.
            
            Console.WriteLine("Hello World!");
            var path = @"C:\Users\James\Downloads\Compressed\Project 4 - James Muriithi\customers-encrypted.csv";
            var CC = new CustomerCsv();
            CC.read(path);
        }
    }
}
