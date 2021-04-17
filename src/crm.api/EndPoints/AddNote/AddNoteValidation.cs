﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.api.EndPoints.AddNote
{
    public class AddNoteValidation : AbstractValidator<AddNoteDto>
    {
        public AddNoteValidation()
        {
            RuleFor(x => x.Note)
                .NotEmpty()
                .WithMessage("Note can't be empty!");

            RuleFor(x => x.LeadId)
                .NotEqual(Guid.Empty).WithMessage("This is the default value. Please enter a valid ID.")
                .NotEmpty().WithMessage("Must provide an valid ID.");
        }

    }
}
