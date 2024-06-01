using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Domain.Entities;

public class Product : BaseEntity
{
    #region [ CTor ]
    public Product() : base()
    {

    }
    #endregion

    #region [ Properties ]
    [Required]
    [StringLength(256)]
    [DataType(DataType.Text)]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }

    [StringLength(256)]
    [DataType(DataType.Text)]
    public string Category { get; set; }    

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
    public int Price { get; set; }

    [Range(0, 99, ErrorMessage = "Value should be greater than or equal to 0 and less than 99")]
    public int Discount { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
    public int Instock { get; set; }

    [Required]
    [DataType(DataType.ImageUrl)]
    public string ImageUrl { get; set; }
    #endregion

    #region [ Virtual Properties ]
    [JsonIgnore]
    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    
    [JsonIgnore]
    [InverseProperty("Product")]
    public virtual ICollection<Cart>? Carts { get; set; }
    #endregion
}

