namespace Market.Auth.Application.Services.UserDeviceServices;

public class UserDeviceBaseDto
{
    public int UserId { get; set; }
    public string Agent { get; set; }
    public string MacAddress { get; set; }
}
