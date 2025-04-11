﻿namespace Domain.Models;

public class Project
{
    public string Id { get; set; } = null!;
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public AppUser User { get; set; } = null!;
    public Status Status { get; set; } = null!;
    public decimal Budget { get; set; }
    public List<string> TeamMembers { get; set; } = [];

}
