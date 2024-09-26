using System.ComponentModel.DataAnnotations;

namespace Market.Auth.Application.Services.AuthenticationService;

public class PasswordValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dto = (UserLoginDto)validationContext.ObjectInstance;
        if (dto == null ||
            string.IsNullOrWhiteSpace(dto.Password) ||
            !dto.IsValidPassword(dto.Password)
        )
        {
            return new ValidationResult("Password must be at least 8 characters. Including Upper, lower, digit and special character");
        }
        return ValidationResult.Success;
    }
}
