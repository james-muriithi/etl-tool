namespace EtlTool
{
    partial class Program
    {
        static void Main(string[] args)
        {
            // Suggestion here is to come up with the plan first, for example:

            // Feel free to extand or rewrite this plan.
            var Path = @"C:\Users\James\Downloads\Compressed\Project 4 - James Muriithi\customers-encrypted.csv";
            var CustomerCsv = new CustomerCsv();
            CustomerCsv.read(Path);

            var PathTasks = @"C:\Users\James\Downloads\Compressed\Project 4 - James Muriithi\tasks-encrypted.csv";
            var TaskCsv = new TaskCsv();

            TaskCsv.read(PathTasks);
        }
    }
}
