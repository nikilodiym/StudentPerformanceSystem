// namespace StudentPerformanceSystem.Middleware;
//
// public class LoginRequiredMiddleware
// {
//     private readonly RequestDelegate _next;
//
//     public LoginRequiredMiddleware(RequestDelegate next)
//     {
//         _next = next;
//     }
//
//     public async Task Invoke(HttpContext context)
//     {
//         var path = context.Request.Path;
//
//         if (!context.User.Identity.IsAuthenticated &&
//             !path.StartsWithSegments("/Index") &&
//             !path.StartsWithSegments("/Users") &&
//             !path.StartsWithSegments("/Welcome") &&
//             !path.StartsWithSegments("/css") &&
//             !path.StartsWithSegments("/js") &&
//             !path.StartsWithSegments("/lib"))
//         {
//             context.Response.Redirect("/Index");
//             return;
//         }
//
//         await _next(context);
//     }
// }
