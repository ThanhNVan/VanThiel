using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Repositories.Base;
using VanThiel.Application.Repositories.DatabaseContext;
using VanThiel.Application.Settings;
using VanThiel.Core.Repositories;
using VanThiel.Domain.DTOs.ReturnModel;
using VanThiel.Domain.Entities;
using VanThiel.Domain.Enums;

namespace VanThiel.Application.Repositories;

public class OrderRepository : BaseVanThielRepository<Order>, IOrderRepository
{
    #region [ Ctor ]
    public OrderRepository(ILogger<OrderRepository> logger, IDbContextFactory<VanThielDbContext> dbContextFactory, PagingSettings pagingSettings)
        : base(logger, dbContextFactory, pagingSettings)
    {
    }
    #endregion

    #region [ Public Method - Get Many ]
    #endregion
    
    #region [ Public Method - Get Single ]
    #endregion
    
    #region [ Public Method - Create ]
    public async ValueTask<bool> Create_TransactionAsync(IEnumerable<CartInfo> cartInfos, string userId, CancellationToken cancellationToken = default)
    {
        using var dbContext = await this.GetDbContextAsync(cancellationToken);
        var order = new Order 
        { 
            UserId = userId,
            PaymentStatus = PaymentStatus.Pending,
            ShippingStatus = ShippingStatus.None,
        };

        var productList = new List<Product>();
        var cartList = new List<Cart>();
        var orderDetailList = new List<OrderDetail>();
        foreach (var cartInfo in cartInfos) {
            var orderDetail = new OrderDetail
            {
                OrderId = order.Id,
                ProductId = cartInfo.ProductId,
                Quantity = cartInfo.ProductInCart,
                TotalPrice = cartInfo.Price * (100 - cartInfo.Discount) * cartInfo.ProductInCart,
            };

            var product = await dbContext.Products.FindAsync(cartInfo.ProductId, cancellationToken);
            product.Instock -= cartInfo.ProductInCart;
            productList.Add(product);

            var cart = await dbContext.Carts.FindAsync(cartInfo.Id, cancellationToken);
            cart.IsActive = false; 
            cartList.Add(cart);
        }
        var totalPrice = orderDetailList.Sum(x => x.TotalPrice);
        order.TotalPrice = totalPrice.Value;

        await dbContext.Orders.AddAsync(order, cancellationToken);
        await dbContext.OrderDetails.AddRangeAsync(orderDetailList, cancellationToken);

        dbContext.UpdateRange(productList);
        dbContext.UpdateRange(cartList);

        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
    #endregion

    #region [ Public Method - Update ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
    
    #region [ Public Method - Validate ]
    #endregion
}
