using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.api.EndPoints.UpdateNote
{
    public class UpdateNoteValidator : AbstractValidator<UpdateNoteModel>
    {
        public UpdateNoteValidator()
        {
            RuleFor(x => x.LeadId)
                .NotEqual(Guid.Empty)
                .NotNull()
                .WithMessage("Must provide an valid lead id.");

            RuleFor(x => x.NoteId)
                .NotEqual(Guid.Empty)
                .NotNull()
                .WithMessage("Must provide an valid note id.");

            RuleFor(x => x.NewContent)
                .NotEmpty()
                .WithMessage("New note content can not be null or empty!");
        }
    }
}
