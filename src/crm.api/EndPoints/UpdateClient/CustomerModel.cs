using Microsoft.AspNetCore.Mvc;
using System;

namespace crm.api.EndPoints.UpdateClient
{
    public class CustomerModel
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }

        [FromForm]
        public string FirstName { get; set; }

        [FromForm]
        public string LastName { get; set; }

        [FromForm]
        public string PhoneNumber { get; set; }

        [FromForm]
        public string EmailAddress   { get; set; }
    }
}