using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VanThiel.Application.Repositories.Base;
using VanThiel.Application.Repositories.DatabaseContext;
using VanThiel.Application.Settings;
using VanThiel.Core.Repositories;
using VanThiel.Domain.Entities;

namespace VanThiel.Application.Repositories;

public class OrderRepository : BaseVanThielRepository<Order>, IOrderRepository
{
    #region [ Ctor ]
    public OrderRepository(ILogger<OrderRepository> logger, IDbContextFactory<VanThielDbContext> dbContextFactory, PagingSettings pagingSettings)
        : base(logger, dbContextFactory, pagingSettings)
    {
    }
    #endregion
}
