﻿namespace Domain.Models;

public class AppUser
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? JobTitle { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }

    public string FullName => $"{FirstName} {LastName}";

}
