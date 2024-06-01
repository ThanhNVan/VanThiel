namespace VanThiel.Domain.DTOs.ReturnModel;

public class CartInfo
{
    public string Id { get; set; }

    public string ProductId { get; set; }

    public string ProductName { get; set; }

    public string Category { get; set; }

    public int Price { get; set; }

    public int Discount { get; set; }

    public int ProductInCart { get; set; }

    public bool IsSelected { get; set; }
}
