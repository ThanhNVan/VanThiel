using System;

namespace VanThiel.Core.ExceptionClasses;

public class NotFoundException : Exception
{
    #region [ Ctor ]
    public NotFoundException(string ex) : base(ex)
    {
            
    }
    #endregion
}
