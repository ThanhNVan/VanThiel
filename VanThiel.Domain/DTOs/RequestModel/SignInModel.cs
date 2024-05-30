using System.ComponentModel.DataAnnotations;

namespace VanThiel.Domain.DTOs.RequestModel;

public class SignInModel
{
    [Required]
    [StringLength(512)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
