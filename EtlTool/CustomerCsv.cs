using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace EtlTool
{
    public class CustomerCsv : IFile
    {
        private string _delimeter = ",";
        // - decode text from base64 to readable text;
        public string decode(string encodedText)
        {
            byte[] data = Convert.FromBase64String(encodedText);
            return Encoding.Unicode.GetString(data);
        }

        // read data from the csv
        public void read(string path)
        {
            var encodedText = File.ReadAllText(path);
            var decodedText = decode(encodedText);
            // Console.WriteLine(DecodedText);

            // - parse data so you have rows with column names;
            // - map data to an oobject;

            var rows = decodedText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            //customers list
            var customers = new List<Customer>();
            // columns list
            var columnTitles = new List<string>();
            int index = 0;
            foreach(var row in rows)
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
                    var customer = new Customer();
                    for (var i = 0; i < columns.Length; i++)
                    {
                        if (columnTitles[i].ToLower() == "id")
                            customer.Id = columns[i];
                        else if (columnTitles[i].ToLower() == "first_name")
                            customer.FirstName = columns[i];
                        else if (columnTitles[i].ToLower() == "last_name")
                            customer.LastName = columns[i];
                        else if (columnTitles[i].ToLower() == "phone_number")
                            customer.LastName = columns[i];
                    }

                    Console.WriteLine(customer.Id);
                }
                index++;
            }
        }
    }
}
