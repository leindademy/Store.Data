
using StackExchange.Redis;
using System.Text.Json;

namespace Store.Service.Services.CashService
{
    public class CashService : ICashService
    {
        private readonly IDatabase _database;
        public CashService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
       
        public async Task<string> GetCashResponseAsync(string Key)
        {
            var cashedResponse = await _database.StringGetAsync(Key);
            if(cashedResponse.IsNullOrEmpty)
                return null;

            return cashedResponse.ToString(); 
        }

        public async Task SetCashResponseAsync(string Key, object response, TimeSpan timeToLive)
        {
            if (response == null)
                return;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; 

            var serializedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(Key, serializedResponse, timeToLive);

        }
    }
}
