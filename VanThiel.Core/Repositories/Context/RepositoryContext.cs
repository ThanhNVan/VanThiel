namespace VanThiel.Core.Repositories.Context;

public class RepositoryContext
{
    #region [ Ctor ]
    public RepositoryContext(IOrderDetailRepository orderDetail,
                                IOrderRepository order,
                                IProductRepository product,
                                IUserRepository user,
                                ICartRepository cart)
    {
        OrderDetail = orderDetail;
        Order = order;
        Product = product;
        User = user;
        Cart = cart;
    }

    #endregion

    #region [ Properties ]
    public IOrderDetailRepository OrderDetail { get; }
    public IOrderRepository Order { get; }
    public IProductRepository Product { get; }
    public IUserRepository User { get; }
    public ICartRepository Cart { get; }
    #endregion
}
