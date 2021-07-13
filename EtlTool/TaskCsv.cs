using System;
using System.IO;
using System.Collections.Generic;
using Cryptography;

namespace EtlTool
{
    public class TaskCsv : IFile
    {
        private readonly IDecoder _decoder;
        private string _delimeter = ",";

        public TaskCsv(IDecoder decoder)
        {
            this._decoder = decoder;
        }

        // read data from the csv
        public void read(string path)
        {
            var encodedText = File.ReadAllText(path);
            var decodedText = _decoder.Decode(encodedText);
            // Console.WriteLine(DecodedText);

            // - parse data so you have rows with column names;
            // - map data to an oobject;

            var rows = decodedText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            //tasks list
            var tasks = new List<Task>();
            // columns list
            var columnTitles = new List<string>();
            int index = 0;
            foreach (var row in rows)
            {
                var columns = row.Split(_delimeter);
                //if index is zero define the columns
                if (index == 0)
                {
                    foreach (var column in columns)
                    {
                        columnTitles.Add(column);
                    }
                }
                else
                {
                    var task = new Task();
                    for (var i = 0; i < columns.Length; i++)
                    {
                        if (columnTitles[i].ToLower() == "id")
                            task.Id = columns[i];
                        else if (columnTitles[i].ToLower() == "description")
                            task.Description = columns[i];
                        else if (columnTitles[i].ToLower() == "customer_id")
                            task.CustomerId = columns[i];
                    }

                    Console.WriteLine(task.Id);
                }
                index++;
            }
        }
    }
}
