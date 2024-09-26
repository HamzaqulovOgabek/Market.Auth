using Market.Auth.Domain.Models;

namespace Market.Auth.Application.Services.AuthenticationService;

public class UserRegistrationDto : UserLoginDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }

    public static explicit operator User(UserRegistrationDto dto)
    {
        var userName = dto.IsValidUserName(dto.EmailOrUsername)
            ? dto.EmailOrUsername
            : null;

        var email = dto.IsValidEmail(dto.EmailOrUsername)
            ? dto.EmailOrUsername
            : null;
        return new User
        {
            UserName = userName,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
        };
    }
}
