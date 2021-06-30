using System;

namespace EtlTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var path = @"C:\Users\James\Downloads\Compressed\Project 4 - James Muriithi\customers-encrypted.csv";
            var CC = new CustomerCsv();
            CC.read(path);
        }
    }
}
