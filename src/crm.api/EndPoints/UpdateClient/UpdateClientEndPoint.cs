using Ardalis.ApiEndpoints;
using crm.common.DTOs;
using crm.domain.Interfaces;
using crm.domain.LeadAggregate;
using crm.domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace crm.api.EndPoints.UpdateClient
{
    public class UpdateClientEndPoint : BaseAsyncEndpoint
        .WithRequest<CustomerModel>
        .WithoutResponse
    {

        public readonly ILeadRepository _leadRepository;

        public UpdateClientEndPoint(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }


        [HttpPatch("/lead/{id}/update")]
        [SwaggerOperation(
            Summary = "Update client details.",
            Tags = new [] { "LeadEndpoint" }
        )]
        public override async Task<ActionResult> HandleAsync([FromRoute] CustomerModel request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lead = await _leadRepository.GetAsync(request.Id);

            if (lead is null)
                return NotFound();

            lead.UpdateClient(
                    new Customer(
                        new Name(request.FirstName, request.LastName),
                        request.PhoneNumber,
                        request.EmailAddress
                        )
                );

            await _leadRepository.UpdateAsync(lead, cancellationToken);

            return Ok();
        }
    }
}
