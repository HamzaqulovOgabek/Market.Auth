using Market.Auth.Domain.Models;

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
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
        };
    }
}
