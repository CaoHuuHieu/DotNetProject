using System;

namespace JobBoard.Application.Exceptions;

public class BusinessException : Exception
{
    public required int Code { get; set; }
    
    public BusinessException(int code, string message) : base(message)
    {
        Code = code;
    }
  
}
