using System;
using crm.common;
using System.Linq;
using System.Threading;
using Ardalis.ApiEndpoints;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using crm.common.DTOs.GetLeads;

namespace crm.api.EndPoints.GetLeads
{
    public class GetLeadsEndpoint : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<List<LeadModel>>
    {

        private readonly ILeadQueryRepository queryRepository;

        public GetLeadsEndpoint(ILeadQueryRepository queryRepository)
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
        public override async Task<ActionResult<List<LeadModel>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var response = await queryRepository.GetAll();

            if (response.Leads.Any())
            {
                var leadList = new List<LeadModel>();

                foreach (var item in response.Leads)
                {
                    leadList.Add(new LeadModel(
                            item.Id,
                            item.PhoneNumber,
                            item.LeadStage.ToString(),
                            item.CatchLead
                        ));
                }

                return Ok(leadList.ToArray());
            }
            else
                return NotFound();
        }
    }
}
