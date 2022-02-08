using crm.common;
using crm.common.DTOs;
using crm.infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPermissionsQueryRepository _permissionsQueryRepository;

        public RoleController(RoleManager<IdentityRole> roleManager, IPermissionsQueryRepository permissionsQueryRepository)
        {
            _roleManager = roleManager;
            _permissionsQueryRepository = permissionsQueryRepository;
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var listOfRoles = await _roleManager.Roles
                .Select(x => new RoleModel(x.Id, x.Name))
                .ToListAsync();

            return Ok(listOfRoles);
        }

        [HttpGet("permissions")]
        public async Task<List<PermissionModel>> GetPermissions()
        {
            var listOfPermissions = await _permissionsQueryRepository.GetPermissionsAsync();
            return listOfPermissions.ToList();
        }
    }
}
