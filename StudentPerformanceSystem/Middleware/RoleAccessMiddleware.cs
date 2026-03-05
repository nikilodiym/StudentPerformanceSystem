namespace StudentPerformanceSystem.Middleware;

public class RoleAccessMiddleware
{
    private readonly RequestDelegate _next;

    public RoleAccessMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.User.IsInRole("guest"))
        {
            if (context.Request.Path.StartsWithSegments("/Students"))
            {
                context.Response.Redirect("/");
                return;
            }
        }

        await _next(context);
    }
}