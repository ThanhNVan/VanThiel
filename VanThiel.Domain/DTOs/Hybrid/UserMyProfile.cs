using System.ComponentModel.DataAnnotations;
using VanThiel.Domain.DTOs.ReturnModel;

namespace VanThiel.Domain.DTOs;

public class UserMyProfile : UserAccessInfo
{
    [StringLength(512)]
    [DataType(DataType.EmailAddress)]
    public string? Address { get; set; }
}
