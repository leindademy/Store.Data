using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using Store.Service.Services.TokenService;
using Store.Service.Services.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<UserDto> Login(LoginDto input)
        {
            var user = await _userManager.FindByNameAsync(input.Email);

            if (user is null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);
            if (!result.Succeeded)
                throw new Exception("Login Failed");

            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateToken(user),

            };
        }

        public Task<UserDto> Register(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
