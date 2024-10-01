namespace Store.Service.Services.CashService
{
    public interface ICashService
    {
        Task SetCashResponseAsync(string Key , object Response, TimeSpan timeToLive);
        Task <string> GetCashResponseAsync(string Key);
    }
}
