using crm.domain.ValueObjects;
using System;

namespace crm.domain.LeadAggregate
{
    public class Customer : Entity<Guid>
    {
        public Customer(Name name, string phoneNumber, string emailAddress)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }

        public Name Name { get; init; }
        public string PhoneNumber { get; init; }
        public string EmailAddress { get; init; }
        public Address BillingAddress { get; protected set; }

        public void AsignBillingAddress(string street, int number,string city, string bloc, int apartment)
        {
            this.BillingAddress = new Address(street, number, city, bloc, apartment);
        }
    }
}