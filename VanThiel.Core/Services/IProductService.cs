using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Core.Services.Base;
using VanThiel.Domain.Entities;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Core.Services;

public interface IProductService : IBaseVanThielService<Product>
{
    #region [ Method - Get ]
    ValueTask<PagingResult<Product>> GetMany_PagingAsync(int page, CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<Product>> GetMany_ActiveAsync(CancellationToken cancellationToken = default);
    #endregion

    ValueTask<bool> AddManyAsync(IEnumerable<Product> list);
}
