using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VanThiel.Domain.Enums;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Domain.Entities;

public class Order : BaseEntity
{
    #region [ CTor ]
    public Order() : base()
    {

    }
    #endregion

    #region [ Properties ]
    [Required]
    [Range(1, float.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
    public float TotalPrice { get; set; }

    [Required]
    [StringLength(36)]
    [DataType(DataType.Text)]
    public string UserId { get; set; }

    public PaymentStatus PaymentStatus { get; set; }
    
    public ShippingStatus ShippingStatus { get; set; }
    #endregion

    #region [ Virtual Properties ]
    [JsonIgnore]
    [ForeignKey(nameof(UserId))]
    [InverseProperty("Orders")]
    public virtual User? User { get; set; }

    [JsonIgnore]
    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    #endregion
}
