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

namespace crm.api.EndPoints.ChangePrice
{
    public class ChangePriceEndpoint : BaseAsyncEndpoint
        .WithRequest<ChangePriceModel>
        .WithoutResponse
    {
        private readonly ILeadRepository _leadRepository;

        public ChangePriceEndpoint(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        [Authorize]
        [HttpPatch("leads/{leadid}/updatevalue")]
        [SwaggerOperation(
            Summary = "Update the value of a lead.",
            Tags = new[] { "LeadEndpoint" }
        )]
        public override async Task<ActionResult> HandleAsync([FromRoute] ChangePriceModel request, CancellationToken cancellationToken = default)
        {
            var lead = await _leadRepository.GetAsync(request.LeadId);

            if (lead is null)
                return NotFound();

            lead.SetValue(request.NewPrice);

            await _leadRepository.UpdateAsync(lead, cancellationToken);

            return Ok();
        }
    }
}
