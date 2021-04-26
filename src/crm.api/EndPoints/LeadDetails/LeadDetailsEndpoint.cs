using Ardalis.ApiEndpoints;
using crm.common;
using crm.common.DTOs;
using Microsoft.AspNetCore.Mvc;
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
        .WithResponse<LeadDetailsDto>
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
        public override async Task<ActionResult<LeadDetailsDto>> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var leadDetails = await _leadQueryRepo.GetDetails(id);


            return leadDetails;
        }
    }
}
