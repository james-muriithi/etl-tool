using System.Collections.Generic;
using System;
using System.Linq;

namespace EtlTool
{
    public class CustomerData
    {
        public static List<Customer> MapToModel(List<List<string>> parsedData)
        {
            // customer objects
            var customers = new List<Customer>();

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

                customers.Add(customer);
            }

            return customers;
        }

        public static void SaveToDatabase(List<Customer> customers)
        {
            foreach(var customer in customers)
            {
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
        }
    }
}
