using Microsoft.EntityFrameworkCore;
using VanThiel.Domain.Entities;

namespace VanThiel.Application.Repositories.DatabaseContext;

public class VanThielDbContext : DbContext
{
    #region [ CTor ]
    public VanThielDbContext(DbContextOptions<VanThielDbContext> options) : base(options)
    {

    }
    #endregion

    #region [ Properties - DbSet ]
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<User> Users { get; set; }
    #endregion
}
