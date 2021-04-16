using Ardalis.ApiEndpoints;
using crm.domain.Interfaces;
using crm.domain.LeadAggregate;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace crm.api.EndPoints.CreateLead
{
    public class CreateLeadEndpoint : BaseAsyncEndpoint
        .WithRequest<CreateLeadDto>
        .WithResponse<bool>
    {
        private readonly ILeadRepository _repo;

        public CreateLeadEndpoint(ILeadRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException();
        }

        [HttpPost("/leads/create")]
        [SwaggerOperation(
        Summary = "Create a lead",
        Description = "Will create a lead foryour needs.",
        OperationId = "Lead.Create",
        Tags = new[] { "LeadEndpoint" })
        ]
        public override async Task<ActionResult<bool>> HandleAsync(CreateLeadDto request, CancellationToken cancellationToken = default)
        {
            var lead = Lead.New(request.LeadProducts, request.PhoneNumber, request.DelivaryAddress, request.Email);

            var response = await _repo.Create(lead, cancellationToken);

            if (response.IsSuccess)
                return Ok(response.MetaResult.Message);
            else
                return BadRequest(response.MetaResult.Message);
        }
    }
}
