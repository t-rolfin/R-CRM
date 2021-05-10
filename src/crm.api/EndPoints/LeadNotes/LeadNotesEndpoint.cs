using Ardalis.ApiEndpoints;
using crm.common;
using crm.common.DTOs;
using crm.common.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace crm.api.EndPoints.LeadNotes
{
    [Route("/leads")]
    public class LeadNotesEndpoint : BaseAsyncEndpoint
        .WithRequest<Guid>
        .WithResponse<NoteCollectionModel>
    {
        private readonly ILeadQueryRepository _queryLeadRepository;
        public LeadNotesEndpoint(ILeadQueryRepository queryLeadRepository)
        {
            _queryLeadRepository = queryLeadRepository;
        }


        [HttpGet("{leadId}/notes")]
        [SwaggerOperation(
            Summary = "Return a list of notes for a specific lead.",
            Tags = new[] { "LeadEndpoint" }
        )]
        public override async Task<ActionResult<NoteCollectionModel>> HandleAsync([FromRoute] Guid leadId, CancellationToken cancellationToken = default)
        {
            if (leadId == Guid.Empty)
                return BadRequest();

            var notes = await _queryLeadRepository.GetNotes(leadId);

            var result = new NoteCollectionModel();

            if(notes is not null)
            {
                result.Notes = notes;
                GenerateLinksForNotes(result.Notes, leadId);
            }

            result.Links.Add(new Link(
                    $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/leads/{ leadId }/notes/add",
                    "create-note",
                    "POST"
                ));


            return Ok(result);
        }

        private void GenerateLinksForNotes(List<NoteModel> notes, Guid leadId)
        {
            if (notes.Any() /*&& notes.LeadStage != 2*/)
            {
                foreach (var note in notes)
                {
                    if (note is null)
                        return;

                    note!.Links = new()
                    {
                        new Link(
                            $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/leads/{leadId}/notes/delete/{note.Id}",
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
}
