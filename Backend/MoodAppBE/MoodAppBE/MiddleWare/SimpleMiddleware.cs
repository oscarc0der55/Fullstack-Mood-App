namespace MoodAppBE.MiddleWare
{
    public class SimpleMiddleware
    {
        private readonly RequestDelegate next;

        public SimpleMiddleware(RequestDelegate _next)
        {
            next = _next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Request");

            await next(context);

            Console.WriteLine("Response");
        }
    }
}
