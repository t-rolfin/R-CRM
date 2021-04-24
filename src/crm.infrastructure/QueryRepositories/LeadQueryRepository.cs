using crm.domain.LeadAggregate;
using Microsoft.Data.SqlClient;
using Rolfin.Result;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using crm.common.DTOs.GetLeads;
using crm.common;

namespace crm.infrastructure.QueryRepositories
{
    public class LeadQueryRepository : ILeadQueryRepository
    {
        private readonly LeadConnString _connString;

        public LeadQueryRepository(LeadConnString connString)
        {
            _connString = connString;
        }

        public async Task<LeadsDto> GetAll()
        {
            string query = "select Leads.Id, Leads.LeadStage, Leads.CatchLead, Customer.PhoneNumber " +
                "as PhoneNumber from Leads INNER JOIN Customer ON Customer.LeadId = Leads.Id";

            using var conn = new SqlConnection(_connString.Value);
            conn.Open();

            var leads = await conn.QueryAsync<LeadDto>(query);

            if (!leads.Any())
                return null;
            else
                return new LeadsDto(leads);

        }
    }
}
