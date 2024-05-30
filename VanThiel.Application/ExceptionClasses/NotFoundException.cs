using System;

namespace VanThiel.Application.ExceptionClasses;

public class NotFoundException : Exception
{
    #region [ Ctor ]
    public NotFoundException(string ex) : base(ex)
    {
            
    }
    #endregion
}
