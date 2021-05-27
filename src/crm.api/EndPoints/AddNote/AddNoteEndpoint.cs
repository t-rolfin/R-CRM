using Ardalis.ApiEndpoints;
using crm.common.DTOs;
using crm.common.Utils;
using crm.domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        .WithResponse<NoteModel>
    {
        private readonly ILeadRepository _leadRepo;
        private readonly ILogger<AddNoteEndpoint> _log;

        public AddNoteEndpoint(ILeadRepository leadRepo, ILogger<AddNoteEndpoint> log)
        {
            _leadRepo = leadRepo ?? throw new ArgumentNullException();
            _log = log ?? throw new ArgumentNullException();
        }


        [Authorize(Policy = "write:note")]
        [HttpPost("leads/{leadid}/notes/add")]
        [SwaggerOperation(
        Summary = "Add a note to an existing lead.",
        Description = "",
        OperationId = "",
        Tags = new[] { "LeadEndpoint" })
        ]
        public override async Task<ActionResult<NoteModel>> HandleAsync([FromRoute] AddNoteModel request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lead = await _leadRepo.GetAsync(request.LeadId);

            var result = lead.AddNote(request.Note);

            var response = await _leadRepo.UpdateAsync(lead, cancellationToken);

            if (response && result.IsSuccess)
            {
                _log.LogInformation($"An note was added for the lead with id: { request.LeadId } ");

                var newNote = new NoteModel()
                {
                    Id = result.Value.Id,
                    Content = result.Value.Content,
                    Links = new()
                };

                GenerateLinksForNote(newNote, lead.Id);

                return Ok(newNote);
            }
            else
            {
                _log.LogError("Something went wrong. Report this to the team.");

                return BadRequest();
            }

        }
        private void GenerateLinksForNote(NoteModel note, Guid leadId)
        {
            if (note is not null)
            {
                note!.Links = new()
                {
                    new Link(
                        $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/leads/{leadId}/notes/{note.Id}",
                        "delete-note",
                        "DELETE"
                    ),
                    new Link(
                        $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/leads/{leadId}/notes/update/{note.Id}",
                        "update-note",
                        "PATCH"
                    )
                };
            }
        }
    }
}
