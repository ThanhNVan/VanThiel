namespace VanThiel.Domain.DTOs.ReturnModel;

public class OrderDetailInfo
{
    #region [ Properties ]
    public string Id { get; set; }

    public string ProductId { get; set; }

    public string ProductName { get; set; }

    public string Category { get; set; }

    public float TotalPrice { get; set; }

    public int Quantity { get; set; }

    public int Discount { get; set; }
    #endregion
}
