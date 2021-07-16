namespace EtlTool
{
    public class Customer
    {
        // It's a bad practice to use public fields. Use public properties instead.
        // Reason: fields are visible to everyone and you're not in control of when
        // and how they're modified. But with properties you can control access. 
        
        // If some properties of the object are not intended to be changed after
        // initialization, but might be used as readonly value, then you can use
        // get-only property.
        
        // I changed a bit your class to show you an example:

        // Now the unique identifier of a customer has public getter and setter and 
        // can be safely accessed outside of the class.
        public string Id { get; set; }

        // And the first name of the customer now has the public getter and setter
        // so it can be modified outside of the class.
        public string FirstName { get; set; }

        // TODO I let you to decide what to do with these two field.
        public string LastName { get; set; }
            
        public string PhoneNumber { get; set; }


        /*public Customer(string id)
        {
            Id = id;
        }*/
    }
}
