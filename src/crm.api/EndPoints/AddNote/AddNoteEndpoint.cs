using Ardalis.ApiEndpoints;
using crm.domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace crm.api.EndPoints.AddNote
{
    public class AddNoteEndpoint : BaseAsyncEndpoint
        .WithRequest<AddNoteDto>
        .WithoutResponse
    {
        private readonly ILeadRepository _repo;

        public AddNoteEndpoint(ILeadRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException();
        }


        [HttpPost("leads/{leadid}/notes/add")]
        [SwaggerOperation(
        Summary = "Add a note to an existing lead.",
        Description = "",
        OperationId = "",
        Tags = new[] { "LeadEndpoint" })
        ]
        public override async Task<ActionResult> HandleAsync([FromRoute] AddNoteDto request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var leadResponse = await _repo.GetLead(request.LeadId);

            if (leadResponse.IsSuccess)
                leadResponse.Value.AddNote(request.Note);

            var response = await _repo.Update(leadResponse.Value, cancellationToken);

            if (response.IsSuccess)
                return Ok(response.MetaResult.Message);
            else
                return BadRequest(response.MetaResult.Message);

        }
    }
}
