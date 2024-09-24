using Market.Auth.Domain.Enums;
using Market.Auth.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Market.Auth.Domain.Models;

public class User : Auditable<int>, IHaveState
{
    public string? UserName { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string? Salt { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public State State { get; set; } = State.Active;
}
