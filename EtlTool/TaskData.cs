using System;
using System.Collections.Generic;
using System.Linq;

namespace EtlTool
{
    public class TaskData
    {
        public static List<Task> MapToModel(List<List<string>> parsedData)
        {
            // tasks objects
            var tasks = new List<Task>();

            // columns list
            var columnTitles = new List<string>();
            int index = 0;
            foreach (var row in parsedData)
            {
                //if index is zero define the columns
                if (index == 0)
                {
                    foreach (var column in row)
                    {
                        columnTitles.Add(column);
                    }

                    index++;
                    continue;
                }

                var task = new Task();
                for (var i = 0; i < row.Count; i++)
                {
                    if (columnTitles[i].ToLower() == "id")
                        task.Id = row[i];
                    else if (columnTitles[i].ToLower() == "description")
                        task.Description = row[i];
                    else if (columnTitles[i].ToLower() == "customer_id")
                        task.CustomerId = row[i];
                }

                tasks.Add(task);
            }

            return tasks;
        }

        public static void SaveToDatabase(List<Task> tasks)
        {
            foreach (var task in tasks)
            {
                var context = new EtlToolDbContext();
                var existingTask = context.Tasks.FirstOrDefault(t => t.Id == task.Id);
                if (existingTask != null)
                {
                    context.Entry(existingTask).CurrentValues.SetValues(task);
                    Console.WriteLine("task {0} was updated.", task.Id);
                }
                else
                {
                    context.Tasks.Add(task);
                    Console.WriteLine("task {0} saved to database.", task.Id);
                }

                context.SaveChanges();
            }
        }
    }
}
