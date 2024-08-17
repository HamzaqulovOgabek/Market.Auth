﻿using Market.Auth.Domain.Models;

namespace Market.Auth.Application.Services.AuthenticationService;

public class UserRegistrationDto : UserLoginDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }

    public static explicit operator User(UserRegistrationDto dto)
    {
        return new User
        {
            UserName = dto.UserName,
            PasswordHash = dto.PasswordHash,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
        };
    }
}
