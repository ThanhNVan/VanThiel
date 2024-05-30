using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VanThiel.Core.Services;
using VanThiel.Domain.Entities;
using VanThiel.Infrastructure.WebApi.Base;

namespace VanThiel.Infrastructure.WebApi;

[ApiController]
[Route("api/v1/order-detail")]
public class OrderDetailController : BaseVanThielController<OrderDetail, IOrderDetailService>
{
    #region [ Ctor ]
    public OrderDetailController(IOrderDetailService service, ILogger<OrderDetailController> logger) : base(service, logger)
    {
    }
    #endregion

    #region [ Public Method - Get ]
    #endregion
    
    #region [ Public Method - Post ]
    #endregion
    
    #region [ Public Method - Put ]
    #endregion
    
    #region [ Public Method - Delete ]
    #endregion
}
