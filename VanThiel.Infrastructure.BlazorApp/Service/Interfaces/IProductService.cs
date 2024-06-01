using System.Threading.Tasks;
using System.Threading;
using VanThiel.Domain.Entities;
using VanThiel.SharedLibrary.Entity;
using System.Collections.Generic;

namespace VanThiel.Infrastructure.Blazor.Service.Interfaces;

public interface IProductService
{
    #region [ Method - Get ]
    ValueTask<PagingResult<Product>> GetMany_PagingAsync(int page, CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<Product>> GetMany_ActiveAsync(CancellationToken cancellationToken = default);
    #endregion
}
