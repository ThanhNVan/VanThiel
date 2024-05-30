using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VanThiel.Core.Services;
using VanThiel.Domain.Entities;
using VanThiel.Infrastructure.WebApi.Base;

namespace VanThiel.Infrastructure.WebApi;

[ApiController]
[Route("api/v1/order")]
public class OrderController : BaseVanThielController<Order, IOrderService>
{
    #region [ Ctor ]
    public OrderController(IOrderService service, ILogger<OrderController> logger) : base(service, logger)
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
