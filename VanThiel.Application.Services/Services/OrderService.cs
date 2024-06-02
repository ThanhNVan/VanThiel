using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Services.Base;
using VanThiel.Core.ExceptionClasses;
using VanThiel.Core.Repositories;
using VanThiel.Core.Services;
using VanThiel.Domain.DTOs.ReturnModel;
using VanThiel.Domain.Entities;

namespace VanThiel.Application.Services;

public class OrderService : BaseVanThielService<Order, IOrderRepository>, IOrderService
{
    #region [ Fields ]
    private readonly ICartRepository _cartRepository;
    #endregion

    #region [ Ctor ]
    public OrderService(ILogger<OrderService> logger, IOrderRepository repository, ICartRepository cartRepository) 
        : base(logger, repository)
    {
        this._cartRepository = cartRepository;
    }
    #endregion

    #region [ Public Method - Get ]
    public ValueTask<IEnumerable<OrderInfo>> GetMany_ActiveAsync(CancellationToken cancellationToken = default)
    {
        return this._repository.GetMany_ActiveAsync(cancellationToken);
    }

    public ValueTask<IEnumerable<MyOrderInfo>> GetMany_ByUserAsync(ClaimsIdentity identity, CancellationToken cancellationToken = default)
    {
        GuardParametter.IsValidIdentity(identity);

        var userId = identity.FindFirst("UserId").Value;
        GuardParametter.IsValidJwtClaim(userId);

        return this._repository.GetMany_ByUserAsync(userId, cancellationToken);
    }
    #endregion

    #region [ Public Method - Post ]
    public async ValueTask<bool> Post_CheckOutAsync(ClaimsIdentity identity, IEnumerable<string> cartIdList, CancellationToken cancellationToken = default )
    {
        GuardParametter.IsValidIdentity(identity);

        var userId = identity.FindFirst("UserId").Value;
        GuardParametter.IsValidJwtClaim(userId);
        GuardParametter.IEnumerableIsNullOrEmpty(cartIdList);
        var cartInforList = await this._cartRepository.Validate_ProductInCartAsync(userId, cartIdList, cancellationToken);

        return await this._repository.Create_TransactionAsync(cartInforList, userId, cancellationToken);
    }
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
