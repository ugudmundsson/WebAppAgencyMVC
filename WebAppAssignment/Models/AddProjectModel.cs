﻿using System.ComponentModel.DataAnnotations;

namespace WebAppAssignment.Models;

public class AddProjectModel
{
    
    [Display(Name = "Project Name", Prompt = "Project Name")]
    public string ProjectName { get; set; } = null!;

   
    [Display(Name = "Client Name", Prompt = "Client Name")]
    public string ClientName { get; set; } = null!;

   
    [Display(Name = "Description", Prompt = "Type something")]
    public string Description { get; set; } = null!;

   
    [Display(Name = "Start Date", Prompt = "Start Date")]
    [DataType(DataType.Date)]
    public string StartDate { get; set; } = null!;

    
    [Display(Name = "End Date", Prompt = "End Date")]
    [DataType(DataType.Date)]
    public string EndDate { get; set; } = null!;

    
    [Display(Name = "Members", Prompt = "Members")]
    public string Members { get; set; } = null!;

    
    [Display(Name = "Budget", Prompt = "$ 0")]
    public string Budget { get; set; } = null!;

    
    [Required(ErrorMessage = "Please select a file.")]
    [DataType(DataType.Upload)]
    public IFormFile? File { get; set; } = null!;


}
