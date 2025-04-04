using System.ComponentModel.DataAnnotations;

namespace WebAppAssignment.Models;

public class LoginModel
{
    [Display(Name = "Email", Prompt = "Enter your email")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "You must enter your email address")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a vaild email!")]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Enter your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "You must enter your password")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "You must enter a strong password!")]
    public string Password { get; set; } = null!;

    public bool IsPersistent { get; set; }

}
