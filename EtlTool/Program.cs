using System;

namespace EtlTool
{
    class Program
    {
        static void Main(string[] args)
        {
            // Suggestion here is to come up with the plan first, for example:
            
            // Feel free to extand or rewrite this plan.
            var path = @"C:\Users\James\Downloads\Compressed\Project 4 - James Muriithi\customers-encrypted.csv";
            var customerCsv = new CustomerCsv();
            customerCsv.read(path);
        }
    }
}
