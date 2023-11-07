using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Username")]
    public string? UserName { get; set; }

    [Required]
    [Display(Name = "Name Surname")]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string? Email { get; set; }
    
    [Required]
    [StringLength(16, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    [Display(Name = "Confirm Password")]
    public string? ConfirmPassword { get; set; }
}