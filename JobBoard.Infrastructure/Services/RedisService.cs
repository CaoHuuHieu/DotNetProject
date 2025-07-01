using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace JobBoard.Infrastructure.Services;

public class RedisService
{
    private readonly IDatabase _db;
    private readonly ILogger<RedisService> _logger;
    
    public RedisService(IConfiguration config, ILogger<RedisService> logger)
    {
        // var redisConfig = new ConfigurationOptions
        // {
        //     EndPoints = { config["Redis:URL"] },
        //     User = config["Redis:Username"],      
        //     Password = config["Redis:Password"],
        //     Ssl = false, 
        //     AbortOnConnectFail = false
        // };
        
        var redis = ConnectionMultiplexer.Connect("localhost:6379"); 
        _db = redis.GetDatabase();
        _logger = logger;   
    }
    
    
    public async Task<string?> GetAsync(string key)
    {
        return await _db.StringGetAsync(key);
    }
    
    public async Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null)
    {
        try
        {
            return await _db.StringSetAsync(key, value, expiry);
        }
        catch (Exception ex)
        {
            _logger.LogError("[RedisService][SetAsync] Could not set key {Key}", key);
            return false;
        }
    }
    
    public async Task<bool> DeleteAsync(string key)
    {
        try
        {
            return await _db.KeyDeleteAsync(key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[RedisService][DeleteAsync] Could not delete key {Key}", key);
            return false;
        }
    }
    
        
    public async Task<bool> KeyExistsAsync(string key)
    {
        try
        {
            return await _db.KeyExistsAsync(key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[RedisService][KeyExistsAsync] Could not check key {Key} exists or not", key);
            return false;
        }
       
    }
    
    public async Task<long> IncrementAsync(string key, long value = 1)
    {
        return await _db.StringIncrementAsync(key, value);
    }
    
    public async Task<long> DecrementAsync(string key, long value = 1)
    {
        return await _db.StringDecrementAsync(key, value);
    }
    
}