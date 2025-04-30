using System.ComponentModel.DataAnnotations;

namespace WebAppAssignment.Models;

public class RegistrationModel
{

    [Display(Name = "First Name", Prompt = "Your First Name")]
    [Required(ErrorMessage = "Required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Your Last Name")]
    [Required(ErrorMessage = "Required")]
    public string LastName { get; set; } = null!;


    [Display(Name = "Email", Prompt = "Your email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a vaild email!")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "8+ chars with upper, lower, number & symbol.")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [Compare(nameof(Password), ErrorMessage = "Password must match!")]
    public string ConfirmPassword { get; set; } = null!;
}
