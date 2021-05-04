using Ardalis.ApiEndpoints;
using crm.common.Utils;
using crm.domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace crm.api.EndPoints.AddNote
{
    public class AddNoteEndpoint : BaseAsyncEndpoint
        .WithRequest<AddNoteModel>
        .WithoutResponse
    {
        private readonly ILeadRepository _leadRepo;
        private readonly ILogger<AddNoteEndpoint> _log;

        public AddNoteEndpoint(ILeadRepository leadRepo, ILogger<AddNoteEndpoint> log)
        {
            _leadRepo = leadRepo ?? throw new ArgumentNullException();
            _log = log ?? throw new ArgumentNullException();
        }


        [HttpPost("leads/{leadid}/notes/add")]
        [SwaggerOperation(
        Summary = "Add a note to an existing lead.",
        Description = "",
        OperationId = "",
        Tags = new[] { "LeadEndpoint" })
        ]
        public override async Task<ActionResult> HandleAsync([FromRoute] AddNoteModel request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lead = await _leadRepo.GetAsync(request.LeadId);

            var result = lead.AddNote(request.Note);

            var response = await _leadRepo.UpdateAsync(lead, cancellationToken);

            if (response && result.IsSuccess)
            {
                _log.LogInformation($"An note was added for the lead with id: { request.LeadId } ");

                return Ok();
            }
            else
            {
                _log.LogError("Something went wrong. Report this to the team.");

                return BadRequest();
            }

        }
    }
}
