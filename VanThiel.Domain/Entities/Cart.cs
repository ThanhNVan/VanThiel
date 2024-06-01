using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VanThiel.Domain.Enums;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Domain.Entities;

public class Cart : BaseEntity
{
    #region [ CTor ]
    public Cart() : base()
    {

    }
    #endregion

    #region [ Propeties ]
    [Required]
    [StringLength(36)]
    [DataType(DataType.Text)]
    public string ProductId { get; set; }
    
    [Required]
    [StringLength(36)]
    [DataType(DataType.Text)]
    public string UserId { get; set; }

    [Required]
    [Range(1, float.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
    public int Quantity { get; set; }

    [Required]
    public CartStatus Status { get; set; }
    #endregion

    #region [ Virtual Properties ]
    [JsonIgnore]
    [ForeignKey(nameof(ProductId))]
    [InverseProperty("Carts")]
    public virtual Product? Product { get; set; }
    
    [JsonIgnore]
    [ForeignKey(nameof(UserId))]
    [InverseProperty("Carts")]
    public virtual User? User { get; set; }
    #endregion
}
