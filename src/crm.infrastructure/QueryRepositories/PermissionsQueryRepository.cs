using crm.common;
using crm.common.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure.QueryRepositories
{
    public class PermissionsQueryRepository : IPermissionsQueryRepository
    {
        private readonly LeadConnString _connectionString;

        public PermissionsQueryRepository(LeadConnString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<PermissionModel>> GetPermissionsAsync()
        {
            string query = "SELECT * FROM Permissions";

            using var conn = new SqlConnection(_connectionString.Value);
            conn.Open();

            var permissions = await conn.QueryAsync<PermissionModel>(query);

            return permissions;
        }
    }
}
