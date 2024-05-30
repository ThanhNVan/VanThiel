using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Domain.Entities;

public class OrderDetail : BaseEntity
{
    #region [ CTor ]
    public OrderDetail() : base()
    {

    }
    #endregion

    #region [ Properties ]
    [Required]
    [StringLength(36)]
    [DataType(DataType.Text)]
    public string OrderId { get; set; }

    [Required]
    [StringLength(36)]
    [DataType(DataType.Text)]
    public string ProductId { get; set; }

    [Required]
    [Range(1, 9999, ErrorMessage = "Value should be greater than or equal to 1")]
    public int Quantity { get; set; }

    [Range(1, float.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
    public float? TotalPrice { get; set; }
    #endregion

    #region [ Virtual Properties ]
    [JsonIgnore]
    [ForeignKey(nameof(OrderId))]
    [InverseProperty("OrderDetails")]
    public virtual Order? Order { get; set; }
    
    [JsonIgnore]
    [ForeignKey(nameof(ProductId))]
    [InverseProperty("OrderDetails")]
    public virtual Product? Product { get; set; }
    #endregion
}
