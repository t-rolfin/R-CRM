using Microsoft.AspNetCore.Mvc;
using System;

namespace crm.api.EndPoints.ChangePrice
{
    public class ChangePriceModel
    {
        [FromRoute(Name = "leadid")]
        public Guid LeadId { get; set; }

        [FromForm]
        public decimal NewPrice { get; set; }
    }
}