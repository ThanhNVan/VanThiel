using System.Collections.Generic;
using VanThiel.Domain.Enums;

namespace VanThiel.Domain.DTOs.ReturnModel;

public class MyOrderInfo
{
    #region [ Ctor ]
    public MyOrderInfo()
    {
        this.Details = new List<OrderDetailInfo>();
    }
    #endregion

    #region [ Properties ]
    public string Id { get; set; }

    public float TotalPrice { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public ShippingStatus ShippingStatus { get; set; }

    public IEnumerable<OrderDetailInfo> Details { get; set; }
    #endregion
}
