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
        private readonly ILeadRepository _leadRepo;

        public AddNoteEndpoint(ILeadRepository leadRepo)
        {
            _leadRepo = leadRepo ?? throw new ArgumentNullException();
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

            var lead = await _leadRepo.GetAsync(request.LeadId);

            var result = lead.AddNote(request.Note);

            var response = await _leadRepo.UpdateAsync(lead, cancellationToken);

            if (response && result.IsSuccess)
                return Ok(result.MetaResult.Message);
            else
                return BadRequest(result.MetaResult.Message);

        }
    }
}
