using System.Reflection;
using JobBoard.Application.DTOs;
using JobBoard.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories;

public abstract class AbstractRepository<T> where T : BaseEntity
{
    protected async Task<PageResponse<T>> ToPagedResultAsync(IQueryable<T> query, PageRequestDto request)
    {
        var total = await query.CountAsync();
        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var sortDirection = request.SortDirection?.Trim().ToLower();
            var prop = typeof(T).GetProperty(request.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (prop != null)
            {
                query = sortDirection == "desc"
                    ? query.OrderByDescending(c => EF.Property<object>(c, prop.Name))
                    : query.OrderBy(c => EF.Property<object>(c, prop.Name));
            }
        }
        else
        {
            query = query.OrderByDescending(c => c.Id);
        }

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PageResponse<T>
        {
            TotalElements = total,
            CurrentPage = request.PageNumber ,
            PageSize = request.PageSize ,
            TotalPages = total/request.PageSize,
            Items = items
        };
    }
}