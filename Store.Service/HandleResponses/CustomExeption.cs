namespace Store.Service.HandleResponses
{
    public class CustomExeption : Response
    {
        public CustomExeption(int statusCode, string? message = null , string? details = null) 
            : base(statusCode,  message)
        {
            Details = details;
        }
        public string? Details { get; set; }
    }
}
