using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Market.Auth.Application.Services.AuthenticationService;

public class UserLoginDto
{
    [EmailOrUserNameValidation]
    public required string EmailOrUsername { get; set; } 
    [PasswordValidation]
    public required string Password { get; set; }

    public bool IsValidUserName(string userName)
    {
        var regex = new Regex(@"^(?=[a-zA-Z0-9._]{3,20}$)(?!.*[_.]{2})[^_.].*[^_.]$");
        return regex.IsMatch(userName);
    }
    public bool IsValidPassword(string password)
    {
        if (password.Length < 8) return false;
        bool hasUpperCase = password.Any(char.IsUpper);
        bool hasLowerCase = password.Any(char.IsLower);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

        return hasUpperCase && hasLowerCase;
    }
    public bool IsValidEmail(string email)
    {
        var regex = new Regex("^[\\w\\d]{5,20}@[\\w\\d]+.[\\w]{2,}$");
        return regex.IsMatch(email);
    }

}
