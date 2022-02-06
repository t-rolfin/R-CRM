using Ardalis.ApiEndpoints;
using crm.common.DTOs;
using crm.infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace crm.api.EndPoints.GetUsers
{

    [Route("/management/getUsers")]
    public class GetUsersEndpoint
        : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<UsersListModel>
    {

        private readonly IIdentityService _identityService;

        public GetUsersEndpoint(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        [Authorize]
        public override async Task<ActionResult<UsersListModel>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var users = await _identityService.GetUsersAsync();
            return Ok(new UsersListModel(users));
        }
    }



}
