using Cryptography;
using System.IO;
using System.Collections.Generic;
using EtlTool;
using System;

namespace Reader
{
    public class CustomerCsvReader : IFileReader
    {
        private readonly IDecoder _decoder;
        private string _delimeter = ",";

        public CustomerCsvReader(IDecoder decoder)
        {
            this._decoder = decoder;
        }


        public void Read(string filePath)
        {
            var encodedText = File.ReadAllText(filePath);
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
            foreach (var row in rows)
            {
                // What if value in some cell will contain the same symbol as delimiter? 
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
                    // I believe your program will become more adoptable to changes if you split 
                    // the part that parses the file and the part that maps it to some entity. 
                    // In current implementation you duplicate the process of parsing in CustomerCsv and TaskCsv.
                    // Consider create CsvParser or CsvReader and put all logic related to data reading inside it.
                    // And map data to particular model somewhere else.
                    
                    // This is change is also a part of SOLID principals.
                    // The Single Responsibility Principal.
                    
                    // Right now this method does a lot of things:
                    // - it reads file
                    // - then decodes it
                    // - then parses decoded data
                    // - and then maps it to an entity
                    // So every change of any of these parts will trigger a change of this method. Try to split logic
                    // to different classes and method by their responsibilities.
                    
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
