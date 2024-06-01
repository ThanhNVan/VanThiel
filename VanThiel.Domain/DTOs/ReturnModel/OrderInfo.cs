namespace VanThiel.Domain.DTOs.ReturnModel;

public class OrderInfo : MyOrderInfo
{
    #region [ Ctor ]
    public OrderInfo() : base()
    {

    }
    #endregion

    #region [ Properties ]
    public string UserName { get; set; }
    public string UserId { get; set; }
    #endregion
}
