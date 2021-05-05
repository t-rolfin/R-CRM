using Ardalis.ApiEndpoints;
using crm.domain.Interfaces;
using crm.domain.LeadAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Rolfin.Result;
using crm.common.Utils;
using Microsoft.AspNetCore.Routing;

namespace crm.api.EndPoints.CreateLead
{
    public class CreateLeadEndpoint : BaseAsyncEndpoint
        .WithRequest<CreateLeadModel>
        .WithResponse<bool>
    {
        private readonly ILeadRepository _leadRepo;
        private readonly ILeadService _leadService;
        private readonly ILogger<CreateLeadEndpoint> _logger;
        private readonly LinkGenerator _linkGenerator;

        public CreateLeadEndpoint(
            ILeadRepository leadRepo, 
            ILeadService leadService, 
            ILogger<CreateLeadEndpoint> logger,
            LinkGenerator linkGenerator)
        {
            _leadRepo = leadRepo ?? throw new ArgumentNullException();
            _leadService = leadService ?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException();
            _linkGenerator = linkGenerator ?? throw new ArgumentNullException();
        }

        [HttpPost("/leads/create")]
        [SwaggerOperation(
        Summary = "Create a lead",
        Description = "Will create a lead for your needs.",
        OperationId = "Lead.Create",
        Tags = new[] { "LeadEndpoint" })
        ]
        public override async Task<ActionResult<bool>> HandleAsync(CreateLeadModel request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lead = Lead.New(request.LeadProducts, request.PhoneNumber, request.DelivaryAddress, request.Email);

            if (lead is null)
                return NotFound();

            var updatedLead = await _leadService.AssignExistingClient(lead, request.PhoneNumber, cancellationToken);

            if (!updatedLead.IsSuccess)
            {
                _logger.LogError("An error ocure when lead was updated!");

                return BadRequest();
            }

            await _leadRepo.CreateAsync(lead, cancellationToken);

            _logger.LogInformation($"An new lead with id:{ lead.Id } was added.");

            return Created($"leads/{ lead.Id }", null);
        }
    }
}
