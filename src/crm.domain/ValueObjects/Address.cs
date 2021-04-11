using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, int number, string city, string bloc, int apartment)
        {
            Street = street;
            Number = number;
            City = city;
            Bloc = bloc;
            Apartment = apartment;
        }

        public string Street { get; init; }
        public int Number { get; init; }
        public string City { get; init; }
        public string Bloc { get; init; }
        public int Apartment { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return Number;
            yield return City;
            yield return Bloc;
            yield return Apartment;
        }
    }
}
