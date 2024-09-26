namespace Market.Auth.Application.Services.UserServices;

public class UsernameUpdateDto
{
    public required int UserId { get; set; }
    public string? OldUsername { get; set; }
    public required string NewUsername { get; set; }

}
