using FermentaLabOnion.Application.Abstraction.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Infrastructure.Implementations.Services
{
    public class WishlistCookieService : IWishlistCookieService
    {
        private const string CookieName = "wishlist_id";
        private readonly IHttpContextAccessor _accessor;

        public WishlistCookieService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string GetOrCreate()
        {
            var ctx = _accessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null.");

            if (ctx.Request.Cookies.TryGetValue(CookieName, out var id) && !string.IsNullOrWhiteSpace(id))
                return id;

            id = Guid.NewGuid().ToString("N");

            ctx.Response.Cookies.Append(CookieName, id, new CookieOptions
            {
                HttpOnly = true,
                Secure = ctx.Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            return id;
        }
    }
}
