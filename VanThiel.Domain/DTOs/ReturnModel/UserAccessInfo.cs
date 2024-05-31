using System.ComponentModel.DataAnnotations;

namespace VanThiel.Domain.DTOs.ReturnModel;

public class UserAccessInfo
{
    public string Id { get; set; }

    [Required]
    [StringLength(256)]
    [DataType(DataType.Text)]
    public string Fullname { get; set; }

    [Required]
    [StringLength(15)]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(512)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}
