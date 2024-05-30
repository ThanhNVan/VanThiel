using Microsoft.Extensions.Logging;
using VanThiel.Application.Services.Base;
using VanThiel.Core.Repositories;
using VanThiel.Core.Services;
using VanThiel.Domain.Entities;

namespace VanThiel.Application.Services;

public class OrderService : BaseVanThielService<Order, IOrderRepository>, IOrderService
{
    #region [ Ctor ]
    public OrderService(ILogger<OrderService> logger, IOrderRepository repository) : base(logger, repository)
    {
    }
    #endregion
}
