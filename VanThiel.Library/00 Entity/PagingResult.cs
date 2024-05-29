using System.Collections.Generic;

namespace VanThiel.SharedLibrary.Entity;

public class PagingResult<TEntity>
    where TEntity : class
{
    #region
    public PagingResult()
    {
        this.Data = new List<TEntity>();
    }
    #endregion
    #region 
    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int DataCount { get; set; }

    public IEnumerable<TEntity> Data { get; set; }
    #endregion

}
