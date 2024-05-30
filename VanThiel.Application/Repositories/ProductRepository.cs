using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VanThiel.Application.Repositories.Base;
using VanThiel.Application.Repositories.DatabaseContext;
using VanThiel.Core.Repositories;
using VanThiel.Domain.Entities;
using VanThiel.Domain.Settings;

namespace VanThiel.Application.Repositories;

public class ProductRepository : BaseVanThielRepository<Product>, IProductRepository
{
    #region [ Ctor ]
    public ProductRepository(ILogger<ProductRepository> logger, IDbContextFactory<VanThielDbContext> dbContextFactory, PagingSettings pagingSettings)
        : base(logger, dbContextFactory, pagingSettings)
    {
    }
    #endregion
}