using Ardalis.ApiEndpoints;
using crm.domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace crm.api.EndPoints.DeleteNote
{
    public class DeleteNoteEndpoint : BaseAsyncEndpoint
        .WithRequest<DeleteNoteModel>
        .WithoutResponse
    {
        private readonly ILeadRepository _leadRepository;

        public DeleteNoteEndpoint(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        [Authorize(Policy = "delete:note")]
        [HttpDelete("/leads/{leadid}/notes/{noteid}")]
        [SwaggerOperation(
            Summary = "Delete a note for a specific lead.",
            Tags = new [] { "LeadEndpoint" }
            )]
        public override async Task<ActionResult> HandleAsync([FromRoute] DeleteNoteModel request, CancellationToken cancellationToken = default)
        {
            var lead = await _leadRepository.GetAsync(request.LeadId);

            if (lead == null)
                return NotFound($"A lead with id: { request.LeadId } does not exist!");

            var result = lead.DeleteNote(request.NoteId);

            if (!result.IsSuccess)
                return BadRequest(result.MetaResult.Message);
            else
            {
                await _leadRepository.UpdateAsync(lead, cancellationToken);
                return Ok();
            }
        }
    }
}
