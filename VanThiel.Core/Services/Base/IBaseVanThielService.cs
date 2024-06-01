using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using VanThiel.Domain.Entities;
using VanThiel.SharedLibrary;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Core.Services.Base;

public interface IBaseVanThielService<TEntiry> : IBaseEntityService<TEntiry>
    where TEntiry : BaseEntity
{
}
