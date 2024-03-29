﻿namespace MakeProfits.Backend.Models
{
    public class AbstractUser
    {

        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public decimal funds { get; set; }

    }
}
