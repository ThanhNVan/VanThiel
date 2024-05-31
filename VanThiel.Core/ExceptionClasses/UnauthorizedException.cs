using System;

namespace VanThiel.Core.ExceptionClasses;

public class UnauthorizedException : Exception
{
    #region [ Ctor ]
    public UnauthorizedException(string ex) : base(ex)
    {

    }
    #endregion
}
