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

namespace crm.api.EndPoints.CreateLead
{
    public class CreateLeadEndpoint : BaseAsyncEndpoint
        .WithRequest<CreateLeadDto>
        .WithResponse<bool>
    {
        private readonly ILeadRepository _leadRepo;
        private readonly ILeadService _leadService;
        private readonly ILogger<CreateLeadEndpoint> _logger;

        public CreateLeadEndpoint(ILeadRepository leadRepo, ILeadService leadService, ILogger<CreateLeadEndpoint> logger)
        {
            _leadRepo = leadRepo ?? throw new ArgumentNullException();
            _leadService = leadService ?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException();
        }

        [HttpPost("/leads/create")]
        [SwaggerOperation(
        Summary = "Create a lead",
        Description = "Will create a lead for your needs.",
        OperationId = "Lead.Create",
        Tags = new[] { "LeadEndpoint" })
        ]
        public override async Task<ActionResult<bool>> HandleAsync(CreateLeadDto request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lead = Lead.New(request.LeadProducts, request.PhoneNumber, request.DelivaryAddress, request.Email);

            var updatedLead = await _leadService.AssignExistingClient(lead, request.PhoneNumber, cancellationToken); 

            var isSaved = await _leadRepo.CreateAsync(lead, cancellationToken);

            if (isSaved)
                _logger.LogInformation($"An new lead with id:{updatedLead.Value.Id} was added.");
            else
                _logger.LogError("An error occure while processing the request.");

            if (updatedLead.IsSuccess && isSaved)
                return Ok();
            else
                return BadRequest();
        }
    }
}
