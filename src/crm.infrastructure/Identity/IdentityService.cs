using crm.common.DTOs;
using crm.infrastructure.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace crm.infrastructure.Identity
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenFactory _jwtFactory;

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtTokenFactory jwtFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtFactory = jwtFactory;
        }

        public async Task<bool> RegisterUserAsync(User user, List<string> roles, bool generatePassword, string password)
        {
            if (user is not null)
            {
                // TODO: create a servici to generate passwords
                password = generatePassword ? "Gp123!@#" : password;

                var response = await _userManager.CreateAsync(user, password);
                return response.Succeeded ? true : false;
            }
            else return false;
        }


        public async Task<(string token, string idToken, string refreshToken)> LogInAsync(string userName, string password)
        {
            var user = await _userManager.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            var sf =await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (sf.Succeeded)
            {
                var token = _jwtFactory.GenerateToken(user);
                var idToken = _jwtFactory.GenerateIdToken(user);
                return new (token, idToken, "");
            }

            return (null ,null , null);
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            var users = await _userManager.Users
                .Select(x => new UserModel(x.Id.ToString(), x.UserName, x.Email, x.PhoneNumber, "", ""))
                .ToListAsync();

            return users;
        }
    }
}
