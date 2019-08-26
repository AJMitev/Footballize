namespace Footballize.Web.Middlewares
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Models;

    public class RemoveBannedUserMiddleware
    {
        private readonly RequestDelegate next;

        public RemoveBannedUserMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            if (!string.IsNullOrEmpty(httpContext.User.Identity.Name))
            {
                var user = await userManager.FindByNameAsync(httpContext.User.Identity.Name);

                if (user.IsBanned && user.BanUntil > DateTime.UtcNow)
                {
                    await signInManager.SignOutAsync();
                    //httpContext.Response.Redirect("/");
                }
            }

            await this.next(httpContext);
        }
    }
}