using System.ComponentModel.DataAnnotations;

namespace Market.Auth.Application.Services.AuthenticationService;

public class UserNameValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        //var userName =  value as string;
        var model = (UserRegistrationDto)validationContext.ObjectInstance;
        if (string.IsNullOrWhiteSpace(model.UserName) || !model.IsValidUserName(model.UserName))
        {
            return new ValidationResult("Invalid Username. It must be between 3 and 20 characters.");
        }
        return ValidationResult.Success;
    }
}
