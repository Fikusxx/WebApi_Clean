using System.ComponentModel.DataAnnotations;

namespace Notes.Identity.ViewModels;

public class LoginVM
{
    [Required]
    public string UserName { get; set; } 

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
}
