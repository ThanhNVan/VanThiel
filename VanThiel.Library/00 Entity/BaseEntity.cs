using System;
using System.ComponentModel.DataAnnotations;

namespace VanThiel.SharedLibrary.Entity;

public abstract class BaseEntity
{
    #region [ Properties ]
    [Key]
    [Required]
    [StringLength(36)]
    [DataType(DataType.Text)]
    public string Id { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime LastUpdatedAt { get; set; }
    #endregion

    #region [ CTor ]
    public BaseEntity()
    {
        Id = Guid.NewGuid().ToString();
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = DateTime.UtcNow;
    }
    #endregion
}
