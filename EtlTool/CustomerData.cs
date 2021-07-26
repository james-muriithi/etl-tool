using Reader;
using System.Collections.Generic;
using System;
using System.Linq;

namespace EtlTool
{
    public class CustomerData
    {
        private readonly IFileReader _fileReader;

        public CustomerData(IFileReader fileReader)
        {
            this._fileReader = fileReader;
        }

        // read data from the csv
        public void Read(string path)
        {
            // customer csv rows
            var rows = _fileReader.Read(path);

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
                    // I believe your program will become more adoptable to changes if you split 
                    // the part that parses the file and the part that maps it to some entity. 
                    // In current implementation you duplicate the process of parsing in CustomerCsvReader and TaskCsvReader.
                    // Consider create a unified CsvReader and put all logic related to data reading inside it.
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
                    for (var i = 0; i < row.Count; i++)
                    {
                        if (columnTitles[i].ToLower() == "id")
                            customer.Id = row[i];
                        else if (columnTitles[i].ToLower() == "first_name")
                            customer.FirstName = row[i];
                        else if (columnTitles[i].ToLower() == "last_name")
                            customer.LastName = row[i];
                        else if (columnTitles[i].ToLower() == "phone_number")
                            customer.PhoneNumber = row[i];
                    }

                    // save customer to db
                    var context = new EtlToolDbContext();

                    var existingCustomer = context.Customers.FirstOrDefault(cust => cust.Id == customer.Id);

                    if (existingCustomer != null)
                    {
                        //update customer
                        context.Entry(existingCustomer).CurrentValues.SetValues(customer);
                        Console.WriteLine("customer {0} was updated.", customer.FirstName);
                    }
                    else
                    {
                        context.Customers.Add(customer);
                        Console.WriteLine("customer {0} saved to database.", customer.FirstName);
                    }
                    
                    context.SaveChanges();

                }
                index++;
            }

        }
    }
}
