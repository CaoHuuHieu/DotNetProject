namespace JobBoard.Application.DTOs;

using System.Collections.Generic;
using System.Linq;
using System;

public class PageResponseDto<T>
{
    public int TotalElements { get; set; }
    public int CurrentPage { get; set; }    
    public int Size { get; set; }
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
}