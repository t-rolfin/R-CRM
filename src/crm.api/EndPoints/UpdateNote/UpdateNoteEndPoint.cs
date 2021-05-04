using Ardalis.ApiEndpoints;
using crm.domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace crm.api.EndPoints.UpdateNote
{
    public class UpdateNoteEndPoint : BaseAsyncEndpoint
        .WithRequest<UpdateNoteModel>
        .WithoutResponse
    {

        private readonly ILeadRepository _leadRepository;

        public UpdateNoteEndPoint(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        [HttpPatch("leads/{leadid}/notes/update/{noteid}")]
        [SwaggerOperation(
            Summary = "Update an existin' note for a specific lead.",
            Tags = new[] { "LeadEndpoint" }
        )]
        public override async Task<ActionResult> HandleAsync([FromRoute] UpdateNoteModel request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lead = await _leadRepository.GetAsync(request.LeadId);

            if (lead is null)
                return NotFound();

            if(lead.DeleteNote(request.NoteId))
            {
                lead.AddNote(request.NewContent);
                await _leadRepository.UpdateAsync(lead, cancellationToken);
                return Ok();
            }

            return BadRequest();
        }
    }
}
