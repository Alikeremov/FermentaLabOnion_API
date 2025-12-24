using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Repositories.Generic;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.WishlistDTOs;
using FermentaLabOnion.Application.DTOs.WishlistitemDTOs;
using FermentaLabOnion.Domain.Entities;
using FermentaLabOnion.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Implementations.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepo _wishlistRepo;
        private readonly IWishlistItemRepo _itemRepo;
        private readonly IProductRepo _productRepo;
        private readonly IHttpContextAccessor _accessor;
        private readonly IWishlistCookieService _cookie;

        public WishlistService(
            IWishlistRepo wishlistRepo,
            IWishlistItemRepo itemRepo,
            IProductRepo productRepo,
            IHttpContextAccessor accessor,
            IWishlistCookieService cookie)
        {
            _wishlistRepo = wishlistRepo;
            _itemRepo = itemRepo;
            _productRepo = productRepo;
            _accessor = accessor;
            _cookie = cookie;
        }

        private (string? userId, string? cookieId) ResolveOwner()
        {
            var ctx = _accessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null.");
            var userId = ctx.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrWhiteSpace(userId))
                return (userId, null);

            return (null, _cookie.GetOrCreate());
        }

        public async Task AddAsync(int productId)
        {
            var productExists = await _productRepo.IsExistAsync(p => p.Id == productId);
            if (!productExists) throw new KeyNotFoundException("Product not found.");

            var (userId, cookieId) = ResolveOwner();

            Wishlist? wishlist = userId != null
                ? await _wishlistRepo.GetByExpressionAsync(x => x.AppUserId == userId, isTracking: true)
                : await _wishlistRepo.GetByExpressionAsync(x => x.CookieId == cookieId, isTracking: true);

            if (wishlist == null)
            {
                wishlist = userId != null
                    ? new Wishlist { AppUserId = userId }
                    : new Wishlist { CookieId = cookieId };

                await _wishlistRepo.AddAsync(wishlist); // səndə save edir
            }

            var already = await _itemRepo.IsExistAsync(i => i.WishlistId == wishlist.Id && i.ProductId == productId);
            if (already) return;

            await _itemRepo.AddAsync(new WishlistItem
            {
                WishlistId = wishlist.Id,
                ProductId = productId
            });
        }

        public async Task RemoveAsync(int productId)
        {
            var (userId, cookieId) = ResolveOwner();

            var wishlist = userId != null
                ? await _wishlistRepo.GetByExpressionAsync(x => x.AppUserId == userId, isTracking: true)
                : await _wishlistRepo.GetByExpressionAsync(x => x.CookieId == cookieId, isTracking: true);

            if (wishlist == null) return;

            var item = await _itemRepo.GetByExpressionAsync(
                x => x.WishlistId == wishlist.Id && x.ProductId == productId,
                isTracking: true
            );

            if (item == null) return;

            await _itemRepo.DeleteAsync(item); // səndə save edir

            var anyLeft = await _itemRepo.IsExistAsync(x => x.WishlistId == wishlist.Id);
            if (!anyLeft)
                await _wishlistRepo.DeleteAsync(wishlist); // save edir
        }

        public async Task<WishlistGetDto> GetAsync()
        {
            var (userId, cookieId) = ResolveOwner();

            var wishlist = userId != null
                ? await _wishlistRepo.GetByExpressionAsync(
                    x => x.AppUserId == userId,
                     isTracking: false,
                     includes: new[] {
                             "Items",
                             "Items.Product",
                              "Items.Product.ProductImages"})
                : await _wishlistRepo.GetByExpressionAsync(
                     x => x.CookieId == cookieId,
                     isTracking: false,
                     includes: new[]{
                     "Items",
                     "Items.Product",
                     "Items.Product.ProductImages"});

            if (wishlist == null)
                return new WishlistGetDto();

            return new WishlistGetDto
            {
                WishlistId = wishlist?.Id ?? 0,
                Items = wishlist?.Items.Select(i =>
                {
                    var imgs = i.Product?.ProductImages;

                    var main = imgs?
                        .FirstOrDefault(pi => pi.ImageType == ImageType.Main)
                        ?.Url;

                    var hover = imgs?
                        .FirstOrDefault(pi => pi.ImageType == ImageType.Hover)
                        ?.Url;

                    hover ??= main;

                    return new WishlistItemDto(
                        i.ProductId,
                        i.Product?.Name,
                        i.Product?.Price,
                        main,
                        hover
                    );
                }).ToList() ?? new List<WishlistItemDto>()
            };
        }
    }
}
