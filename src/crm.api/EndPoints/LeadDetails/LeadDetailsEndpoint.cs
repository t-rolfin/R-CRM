using Ardalis.ApiEndpoints;
using crm.common;
using crm.common.DTOs;
using crm.common.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace crm.api.EndPoints.LeadDetails
{
    public class LeadDetailsEndpoint : BaseAsyncEndpoint
        .WithRequest<Guid>
        .WithResponse<LeadDetailsModel>
    {
        private readonly ILeadQueryRepository _leadQueryRepo;

        public LeadDetailsEndpoint(ILeadQueryRepository leadQueryRepo)
        {
            _leadQueryRepo = leadQueryRepo;
        }

        [HttpGet("leads/{id}")]
        [SwaggerOperation(
            Summary = "Get information for a specific lead.",
            Tags = new[] { "LeadEndpoint" }
        )]
        public override async Task<ActionResult<LeadDetailsModel>> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            this.Response.ContentType = "application/hal+json";

            var leadDetails = await _leadQueryRepo.GetDetails(id);

            if (leadDetails != null)
            {
                leadDetails.Links = leadDetails.LeadStage == 2 
                    ? null 
                    : new List<Link>()
            {
                new Link(
                    $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/leads/{ id }",
                    "self",
                    "GET"
                ),
                new Link(
                    $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/leads/{ id }/notes/add",
                    "add-note",
                    "POST"
                ),
                new Link(
                    $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/leads/{ id }/update",
                    "update-client",
                    "PATCH"
                ),
                new Link(
                    $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/leads/{ id }/updatevalue",
                    "update-value",
                    "PATCH"
                ),
                new Link(
                    $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/leads/{ id }/close",
                    "close-lead",
                    "PUT"
                )
            };

                if (leadDetails.Notes.Any() && leadDetails.LeadStage != 2)
                {
                    foreach (var note in leadDetails.Notes)
                    {
                        note.Links = new()
                        {
                            new Link(
                                $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}leads/{leadDetails.Id}/notes/delete/{note.Id}",
                                "delete-note",
                                "DELETE"
                            ),
                            new Link(
                                $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}leads/{leadDetails.Id}/notes/update/{note.Id}",
                                "update-note",
                                "PATCH"
                            )
                        };
                    }
                }

                return Ok(leadDetails);
            }

            return NotFound();
        }
    }
}
