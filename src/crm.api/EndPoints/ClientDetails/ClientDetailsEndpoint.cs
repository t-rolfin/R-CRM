using Ardalis.ApiEndpoints;
using crm.common;
using crm.common.DTOs;
using crm.domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace crm.api.EndPoints.ClientDetails
{
    public class ClientDetailsEndpoint : BaseAsyncEndpoint
        .WithRequest<Guid>
        .WithResponse<ClientDetailsModel>
    {

        private readonly ILeadQueryRepository _leadQueryRepository;

        public ClientDetailsEndpoint(ILeadRepository leadRepository, ILeadQueryRepository leadQueryRepository)
        {
            _leadQueryRepository = leadQueryRepository;
        }


        [HttpGet("leads/{leadid}/clientdetails")]
        [SwaggerOperation(
            Summary = "Get client details for a specific lead.",
            Tags = new[] { "LeadEndpoint" }
        )]
        public override async Task<ActionResult<ClientDetailsModel>> HandleAsync([FromRoute(Name = "leadid")] Guid leadId, CancellationToken cancellationToken = default)
        {
            if (leadId == Guid.Empty)
                return BadRequest();

            var clientDetails = await _leadQueryRepository.GetLeadClientDetails(leadId);

            if (clientDetails is null)
                return NotFound();

            return Ok(clientDetails);
        }
    }
}
