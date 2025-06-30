using JobBoard.Application.DTOs;
using JobBoard.Domain.Entity;
using MongoDB.Driver;

namespace JobBoard.Infrastructure.Repositories;

public abstract class MongoAbstractRepository<T> where T : BaseEntity 
{
    
    protected async Task<PageResponse<T>> ToPagedResultAsync(IMongoCollection<T> collection, FilterDefinition<T> filter, PageRequestDto request)
    {
        var total = await collection.Find(filter).CountDocumentsAsync();
        var items = await collection
            .Find(filter)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync();

        return new PageResponse<T>
        {
            TotalElements = total,
            CurrentPage = request.PageNumber ,
            PageSize = request.PageSize ,
            TotalPages = (int) total/request.PageSize,
            Items = items
        };
    }
    
}