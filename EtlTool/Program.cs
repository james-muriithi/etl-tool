using Cryptography;

namespace EtlTool
{
    partial class Program
    {
        static void Main(string[] args)
        {
            // Suggestion here is to come up with the plan first, for example:

            // initialize a Base64Decoder object
            var base64Decoder = Base64Decoder();

            // Feel free to extand or rewrite this plan.
            var Path = @"C:\Users\James\Downloads\Compressed\Project 4 - James Muriithi\customers-encrypted.csv";
            var CustomerCsv = new CustomerCsv(base64Decoder);
            CustomerCsv.read(Path);

            var PathTasks = @"C:\Users\James\Downloads\Compressed\Project 4 - James Muriithi\tasks-encrypted.csv";
            var TaskCsv = new TaskCsv(base64Decoder);

            TaskCsv.read(PathTasks);
        }
    }
}
