using crm.domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace crm.domain.LeadAggregate
{
    public class Customer : Entity<Guid>
    {
        protected Customer() 
            : base()  { }

        public Customer(Name name, string phoneNumber, string emailAddress)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }
        
        public Name Name { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public string EmailAddress { get; protected set; }
        public Address BillingAddress { get; protected set; }


        public void AsignBillingAddress(string street, int number,string city, string bloc, int apartment)
        {
            this.BillingAddress = new Address(street, number, city, bloc, apartment);
        }

        public void Update(Name name, string phoneNumbar, string emailAddress)
        {
            this.Name = name;
            this.PhoneNumber = phoneNumbar;
            this.EmailAddress = emailAddress;
        }
    }
}