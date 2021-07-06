using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace EtlTool
{
    public class TaskCsv : IFile
    {
        private string Delimeter = ",";
        // - decode text from base64 to readable text;
        public string decode(string EncodedText)
        {
            byte[] data = Convert.FromBase64String(EncodedText);
            return Encoding.Unicode.GetString(data);
        }

        // read data from the csv
        public void read(string path)
        {
            var EncodedText = File.ReadAllText(path);
            var DecodedText = decode(EncodedText);
            // Console.WriteLine(DecodedText);

            // - parse data so you have rows with column names;
            // - map data to an oobject;

            var rows = DecodedText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            //tasks list
            var Tasks = new List<Task>();
            // columns list
            var ColumnTitles = new List<string>();
            int index = 0;
            foreach (var row in rows)
            {
                var columns = row.Split(Delimeter);
                //if index is zero define the columns
                if (index == 0)
                {
                    foreach (var column in columns)
                    {
                        ColumnTitles.Add(column);
                    }
                }
                else
                {
                    var Task = new Task();
                    for (var i = 0; i < columns.Length; i++)
                    {
                        if (ColumnTitles[i].ToLower() == "id")
                            Task.Id = columns[i];
                        else if (ColumnTitles[i].ToLower() == "description")
                            Task.Description = columns[i];
                        else if (ColumnTitles[i].ToLower() == "customer_id")
                            Task.CustomerId = columns[i];
                    }

                    Console.WriteLine(Task.Id);
                }
                index++;
            }
        }
    }
}
