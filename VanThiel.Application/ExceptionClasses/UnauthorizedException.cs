using System;

namespace VanThiel.Application.ExceptionClasses;

public class UnauthorizedException : Exception
{
    #region [ Ctor ]
    public UnauthorizedException(string ex) : base(ex)
    {

    }
    #endregion
}
