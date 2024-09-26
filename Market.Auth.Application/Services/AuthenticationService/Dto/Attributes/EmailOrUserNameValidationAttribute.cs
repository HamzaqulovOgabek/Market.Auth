using System.ComponentModel.DataAnnotations;

namespace Market.Auth.Application.Services.AuthenticationService
{ 
    public class EmailOrUserNameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var userLoginDto = validationContext.ObjectInstance as UserLoginDto;
            
            if(string.IsNullOrWhiteSpace(userLoginDto.EmailOrUsername))
                return new ValidationResult("Email or username is required");

            bool isEmail = userLoginDto.IsValidEmail(userLoginDto.EmailOrUsername);
            bool isUsername = userLoginDto.IsValidUserName(userLoginDto.EmailOrUsername);

            if(!isEmail && !isUsername)
                return new ValidationResult("Invalid email or username");

            return ValidationResult.Success;
        }
    }
}
