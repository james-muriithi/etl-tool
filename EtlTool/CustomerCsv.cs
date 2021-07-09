﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace EtlTool
{
    public class CustomerCsv : IFile
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
