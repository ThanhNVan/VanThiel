using Microsoft.Extensions.Logging;
using VanThiel.Application.Services.Base;
using VanThiel.Core.Repositories;
using VanThiel.Core.Services;
using VanThiel.Domain.Entities;

namespace VanThiel.Application.Services;

public class OrderDetailService : BaseVanThielService<OrderDetail, IOrderDetailRepository>, IOrderDetailService
{
    #region [ Ctor ]
    public OrderDetailService(ILogger<OrderDetailService> logger, IOrderDetailRepository repository) : base(logger, repository)
    {
    }
    #endregion
}
