using Market.Auth.Domain.Enums;

namespace Market.Auth.Domain.Models.Base;

public interface IHaveState
{
    State State { get; set; }
}
