﻿using Store.Service.Services.UserService.Dtos;

namespace Store.Service.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> Login(LoginDto loginDto);
        Task<UserDto> Register(RegisterDto registerDto);
    }
}
