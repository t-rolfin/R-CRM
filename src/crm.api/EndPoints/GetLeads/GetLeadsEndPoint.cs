using System;
using crm.common;
using System.Linq;
using System.Threading;
using Ardalis.ApiEndpoints;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace crm.api.EndPoints.GetLeads
{
    public class GetLeadsEndPoint : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<List<LeadsDto>>
    {

        private readonly ILeadQueryRepository queryRepository;

        public GetLeadsEndPoint(ILeadQueryRepository queryRepository)
        {
            this.queryRepository = queryRepository;
        }

        [HttpGet("/leads")]
        [SwaggerOperation(
        Summary = "Get all the leads.",
        Description = "",
        OperationId = "Lead.List",
        Tags = new[] { "LeadEndpoint" })
        ]
        public override async Task<ActionResult<List<LeadsDto>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var response = await queryRepository.GetAll();

            if (response.Leads.Any())
            {
                var leadsDto = new LeadsDto();

                foreach (var item in response.Leads)
                {
                    leadsDto.Leads.Add(new LeadDto(
                            item.Id,
                            item.PhoneNumber,
                            item.LeadStage.ToString(),
                            item.CatchLead
                        ));
                }

                return Ok(leadsDto.Leads.ToArray());
            }
            else
                return NotFound();
        }
    }
}
