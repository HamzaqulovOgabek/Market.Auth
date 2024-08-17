using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Market.Auth.Application.Services.AuthenticationService;

public class UserLoginDto
{
    //[UserNameValidation]
    [RegularExpression("^[a-zA-Z0-9]{5,20}$", ErrorMessage = "Invalid UserName")]
    public required string UserName { get; set; }
    [StringLength(20, MinimumLength = 4)]
    [PasswordPropertyText]
    public required string PasswordHash { get; set; }

    public bool IsValidUserName(string userName)
    {
        var regex = new Regex(@"^[a-zA-Z0-9._]{3,20}$");
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

}
