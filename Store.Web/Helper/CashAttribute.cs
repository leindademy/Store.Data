using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.Services.CashService;
using System.Text;

namespace Store.Web.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timetoliveseconds;

        public CacheAttribute(int TimeTILiveSeconds) 
        {
            _timetoliveseconds = TimeTILiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) 
        {
            var _cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();  

            var cashkey = GetKeyCashFromRequest(context.HttpContext.Request);

            var cashedResponse = await _cashService.GetCashResponseAsync(cashkey);

            if(!string.IsNullOrEmpty(cashedResponse) )
            {

                var contentResult = new ContentResult
                {
                    Content = cashedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;

                return;
            }
            var executedContext = await next();
            if (executedContext.Result is ObjectResult response)
                await _cashService.SetCashResponseAsync(cashkey, response.Value, TimeSpan.FromSeconds(_timetoliveseconds));
        }
        private string GetKeyCashFromRequest(HttpRequest request)
        {
            StringBuilder cashKey = new StringBuilder();

            cashKey.Append($"{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key)) 
                cashKey.Append($"|{key}-{value}");

            return cashKey.ToString();

        }
    }
}
