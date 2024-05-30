using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VanThiel.Domain.Enums;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Domain.Entities;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(PhoneNumber), IsUnique = true)]
public class User : BaseEntity
{
    #region [ CTor ]
    public User() : base()
    {

    }
    #endregion

    #region [ Properties ]
    [Required]
    [StringLength(256)]
    [DataType(DataType.Text)]
    public string Fullname { get; set; }

    [Required]
    [StringLength(512)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [StringLength(512)]
    [DataType(DataType.Password)]
    public string PasswordHash { get; set; }

    [StringLength(15)]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Address { get; set; }

    [Required]
    public RoleEnum Role { get; set; }
    #endregion

    #region [ Virtual Properties ]
    [JsonIgnore]
    [InverseProperty("User")]
    public virtual ICollection<Order>? Orders { get; set; }
    #endregion
}
