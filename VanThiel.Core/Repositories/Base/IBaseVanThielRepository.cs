using VanThiel.SharedLibrary;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Core.Repositories.Base;

public interface IBaseVanThielRepository<TEntity> : IBaseEntityRepository<TEntity>
    where TEntity : BaseEntity
{
}
