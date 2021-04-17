using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.api.EndPoints.CreateLead
{
    public class CreateLeadValidation : AbstractValidator<CreateLeadDto>
    {
        public CreateLeadValidation()
        {
            RuleFor(x => x.DelivaryAddress)
                .NotEmpty()
                .WithMessage("The delivary address is required!");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Enter a valid email address!");

            RuleFor(x => x.LeadProducts)
                .NotEmpty()
                .WithMessage("You need to provide some products!");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Provide a phone number.");
        }

    }
}
