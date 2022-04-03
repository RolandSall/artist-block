using account_service.Models;

namespace account_service.ValueObjects;

public class CurrentUser
{
    public RegisteredUser? RegisteredUser;
    public string? Role { get; set; }
}