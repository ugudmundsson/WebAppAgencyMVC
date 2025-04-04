using System.ComponentModel.DataAnnotations;

namespace WebAppAssignment.Models;

public class RegistrationModel
{

    [Display(Name = "First Name", Prompt = "Your First Name")]
    [Required(ErrorMessage = "You must enter your first name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Your Last Name")]
    [Required(ErrorMessage = "You must enter your last name")]
    public string LastName { get; set; } = null!;


    [Display(Name = "Email", Prompt = "Your email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "You must enter your email address")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a vaild email!")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "You must enter your password")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "You must enter a strong password!")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "You must Confirm your passowrd")]
    [Compare(nameof(Password), ErrorMessage = "Password must match!")]
    public string ConfirmPassword { get; set; } = null!;
}
