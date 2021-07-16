using Reader;
using System;
using System.Collections.Generic;

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

                    tasks.Add(task);

                    Console.WriteLine(task.Id);
                }
                index++;
            }
        }
    }
}
