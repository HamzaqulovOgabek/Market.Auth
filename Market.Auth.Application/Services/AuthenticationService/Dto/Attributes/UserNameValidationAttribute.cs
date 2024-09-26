using System.ComponentModel.DataAnnotations;

namespace Market.Auth.Application.Services.AuthenticationService;

public class UserNameValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        //var userName =  value as string;
        var model = (UserRegistrationDto)validationContext.ObjectInstance;
        if (string.IsNullOrWhiteSpace(model.EmailOrUsername) || !model.IsValidUserName(model.EmailOrUsername))
        {
            string errorMessage = "Your Username must be at least 3 characters long, and only contain letters, numbers, underscores, and dashes. It must start and end with alphanumeric characters. Spaces are not allowed. Username is not allowed. Please try again.";

            return new ValidationResult(errorMessage);
        }
        return ValidationResult.Success;
    }
}
