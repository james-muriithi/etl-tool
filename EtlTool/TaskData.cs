using Reader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EtlTool
{
    public class TaskData
    {
        private readonly IFileReader _fileReader;

        public TaskData(IFileReader fileReader)
        {
            this._fileReader = fileReader;
        }

        // read data from the csv
        public void Read(string path)
        {
            // task csv rows
            var rows = _fileReader.Read(path);

            //tasks list
            var tasks = new List<Task>();
            // columns list
            var columnTitles = new List<string>();

            int index = 0;
            foreach (var row in rows)
            {
                //if index is zero define the columns
                if (index == 0)
                {
                    foreach (var column in row)
                    {
                        columnTitles.Add(column);
                    }
                }
                else
                {
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

                    // save task to db
                    var context = new EtlToolDbContext();
                    var existingTask = context.Tasks.FirstOrDefault(t => t.Id == task.Id);
                    if(existingTask != null)
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
                index++;
            }
        }
    }
}
