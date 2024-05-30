using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Linq;

namespace VanThiel.Application.ExceptionClasses;

public static class GuardParametter
{
    public static void TakeParametter(int take)
    {
        if (take <= 0)
        {
            throw new ArgumentException($"Not Valid Take Parametter: {take}");
        }
    }
    public static void DoubleIsNullOrZero(double? param)
    {
        if (!param.HasValue || param == 0)
        {
            throw new ArgumentException($"IsNullOrZero: {param}");
        }
    }

    public static void StringIsNullOrEmpty(string param, string message = "")
    {
        if (string.IsNullOrEmpty(param) && string.IsNullOrEmpty(message))
        {
            throw new ArgumentException($"IsNullOrEmpty: {param}");
        }

        if (string.IsNullOrEmpty(param) && !string.IsNullOrEmpty(message))
        {
            throw new ArgumentException($"{message}");
        }
    }

    public static void IEnumerableIsNullOrEmpty(IEnumerable<object> param)
    {
        if (param is null || param.Count() <= 0)
        {
            throw new ArgumentException($"IsNull: {param}");
        }
    }

    public static void ParamIsNull(object param, string message = "")
    {
        if (param is null && string.IsNullOrEmpty(message))
        {
            throw new ArgumentException($"IsNull: {param}");
        } else if (param is null && !string.IsNullOrEmpty(message))
        {
            throw new ArgumentException($"{message}");
        }
    }
    public static void DateTimeIsValid(DateTime dateTime)
    {
        if (dateTime == default(DateTime))
        {
            throw new ArgumentException($"DateTime is not valid: {dateTime}");
        }
    }

    public static void IsValidJwtClaim(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new AccessViolationException("You are not allowed to process this Api, Please sign in to continue");
        }
    }

    public static void IsValidIdentity(ClaimsIdentity value)
    {
        if (value is null || value.IsAuthenticated == false)
        {
            throw new AccessViolationException("You are not allowed to process this Api, Please sign in to continue");
        }
    }
}
