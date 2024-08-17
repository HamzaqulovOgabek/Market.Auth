using System.ComponentModel.DataAnnotations;

namespace Market.Auth.Application.Services.AuthenticationService;

public class PasswordValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var model = (UserRegistrationDto)validationContext.ObjectInstance;
        if(string.IsNullOrWhiteSpace(model.PasswordHash) || !model.IsValidPassword(model.PasswordHash))
        {
            return new ValidationResult("Password must be at least 8 characters.");
        }
        return ValidationResult.Success;
    }
}
