using System.ComponentModel.DataAnnotations;

namespace VanThiel.Domain.DTOs.RequestModel;

public class SignUpModel
{
    [Required]
    [StringLength(512)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [StringLength(15)]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }
}
