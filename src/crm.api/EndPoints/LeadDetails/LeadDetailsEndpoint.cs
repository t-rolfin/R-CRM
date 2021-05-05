using Ardalis.ApiEndpoints;
using crm.common;
using crm.common.DTOs;
using crm.common.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
            var leadDetails = await _leadQueryRepo.GetDetails(id);

            if (leadDetails != null)
            {
                leadDetails.Actions.Add(
                    "addnote", 
                    $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/leads/{ leadDetails.Id }/notes/add"
                    );

                leadDetails.Actions.Add(
                    "closelead",
                    "");

                leadDetails.Actions.Add(
                    "updateprice",
                    "");

                leadDetails.Actions.Add(
                    "updateclientinfo",
                    "");

                return Ok(leadDetails);
            }

            return NotFound();
        }
    }
}
