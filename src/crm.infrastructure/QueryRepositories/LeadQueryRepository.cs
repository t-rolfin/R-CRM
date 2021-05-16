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
using crm.common.DTOs;

namespace crm.infrastructure.QueryRepositories
{
    public class LeadQueryRepository : ILeadQueryRepository
    {
        private readonly LeadConnString _connString;

        public LeadQueryRepository(LeadConnString connString)
        {
            _connString = connString;
        }

        public async Task<LeadsModel> GetAll()
        {
            string query = "select Leads.Id, Leads.LeadStage, Leads.CatchLead, Customer.PhoneNumber " +
                "as PhoneNumber from Leads LEFT JOIN Customer ON Customer.Id = Leads.ClientId";

            using var conn = new SqlConnection(_connString.Value);
            conn.Open();

            var leads = await conn.QueryAsync<LeadModel>(query);

            if (!leads.Any())
                return null;
            else
                return new LeadsModel(leads);

        }

        public async Task<LeadDetailsModel> GetDetails(Guid leadId)
        {
            string query = $"select * from Leads WHERE Leads.Id = '{ leadId }'";

            using var conn = new SqlConnection(_connString.Value);
            conn.Open();

            var lead = await conn.QueryAsync<LeadDetailsModel>(query);

            if (lead is null)
                return null;
            else
                return lead.First();
        }

        public async Task<ClientDetailsModel> GetLeadClientDetails(Guid leadId)
        {
            string query = "select Leads.Id, Customer.Name_FirstName as FirstName, " +
                "Customer.Name_LastName as LastName, Customer.PhoneNumber, Customer.EmailAddress, " +
                "BillingAddress_Street as BillingAddress from Leads INNER JOIN Customer ON Customer.Id = Leads.ClientId";

            using var conn = new SqlConnection(_connString.Value);
            conn.Open();

            var clientDetails = await conn.QueryAsync<ClientDetailsModel>(query);

            if (!clientDetails.Any())
                return null;
            else
                return clientDetails.First();
        }

        public async Task<List<NoteModel>> GetNotes(Guid leadId)
        {
            string query = $"SELECT * FROM Note WHERE Note.LeadId = '{ leadId }'";

            using var conn = new SqlConnection(_connString.Value);
            conn.Open();

            var leadNotes = await conn.QueryAsync<NoteModel>(query);

            if (!leadNotes.Any())
                return null;
            else
                return leadNotes.ToList();
        }
    }
}
