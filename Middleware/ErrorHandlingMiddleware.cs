using RecipeFinderAPI.Exceptions;

namespace RecipeFinderAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(BadRequestException badReqestEx)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badReqestEx.Message);
            }
            catch(NotFoundException notFoundEx)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundEx.Message);
            }
            catch(ForbidException forbidEx)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbidEx.Message);
            }
            catch
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
