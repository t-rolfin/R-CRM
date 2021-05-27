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

namespace crm.api.EndPoints.CloseLead
{
    public class CloseLeadEndPoint : BaseAsyncEndpoint
        .WithRequest<CloseLeadModel>
        .WithoutResponse
    {
        private readonly ILeadRepository _leadRepository;

        public CloseLeadEndPoint(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        [Authorize(Policy = "close:lead")]
        [HttpPut("leads/{leadid}/close")]
        [SwaggerOperation(
            Summary = "Close a lead.",
            Tags = new[] { "LeadEndpoint" }
        )]
        public override async Task<ActionResult> HandleAsync([FromRoute] CloseLeadModel request, CancellationToken cancellationToken = default)
        {
            var lead = await _leadRepository.GetAsync(request.LeadId);

            if (lead is null)
                return NotFound();

            lead.CloseLead(request.CloseStatus);

            await _leadRepository.UpdateAsync(lead, cancellationToken);

            return Ok();
        }
    }
}
