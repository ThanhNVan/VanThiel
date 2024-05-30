using VanThiel.SharedLibrary;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Core.Services.Base;

public interface IBaseVanThielService<TEntiry> : IBaseEntityService<TEntiry>
    where TEntiry : BaseEntity
{
}
