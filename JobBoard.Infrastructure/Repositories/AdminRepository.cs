using JobBoard.Application.DTOs;
using JobBoard.Application.Interfaces.Repositories;
using JobBoard.Domain.Entity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JobBoard.Infrastructure.Repositories;

public class AdminRepository : MongoAbstractRepository<Admin>,  IAdminRepository
{
    private readonly IMongoCollection<Admin> _adminCollection;
    
    public AdminRepository(IMongoDatabase mongoDatabase) 
    {
        _adminCollection = mongoDatabase.GetCollection<Admin>("admins");
    }

    public async Task<PageResponse<Admin>> GetAllAdminsAsync(AdminRequestDto request)
    {
        
        var filter = Builders<Admin>.Filter.Empty;
        if (!string.IsNullOrEmpty(request.SearchBy) && !string.IsNullOrEmpty(request.SearchValue))
        {
            filter = request.SearchBy.ToLower() switch
            {
                "name" => Builders<Admin>.Filter.Regex(a => a.Name,
                    new BsonRegularExpression(request.SearchValue, "i")),
                "email" => Builders<Admin>.Filter.Regex(a => a.Email,
                    new BsonRegularExpression(request.SearchValue, "i")),
                _ => filter
            };
        }
        
        if(request.Active.HasValue)
            filter = Builders<Admin>.Filter.And(
                filter,
                Builders<Admin>.Filter.Eq(a => a.Active, request.Active)
            );
        
        return await ToPagedResultAsync(_adminCollection, filter, request);
    }
    
    public async Task<Admin?> GetAdminByEmailAsync(string email)
    {
        return await _adminCollection.Find(a => a.Email == email).FirstOrDefaultAsync();
    }

    public async Task<Admin?> GetAdminByIdAsync(string id)
    {
        return await _adminCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAdminAsync(Admin admin)
    {
        await _adminCollection.InsertOneAsync(admin);
    }

    public async Task UpdateAdminAsync(string id, Admin admin)
    {
        await _adminCollection.ReplaceOneAsync(admin => admin.Id == id, admin);
    }

    public async Task DeleteAdminAsync(string id)
    {
        await _adminCollection.DeleteOneAsync(admin => admin.Id == id);
    }
}