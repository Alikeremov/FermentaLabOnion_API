using AutoMapper;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Application.DTOs.ReviewDTOs;
using FermentaLabOnion.Domain.Entities;
using FermentaLabOnion.Persistence.Utilites.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Implementations.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public ReviewService(
            IReviewRepo reviewRepo,
            IProductRepo productRepo,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            _accessor = accessor;
        }

        private string GetUserId()
        {
            var userId = _accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException("User not authenticated.");
            return userId;
        }

        public async Task CreateAsync(ReviewCreateDto dto)
        {
            var productExists = await _productRepo.GetByIdAsync(dto.ProductId);
            if (productExists == null) throw new NotFoundException("Product not found.");

            var userId = GetUserId();

            bool already = await _reviewRepo.GetAllWhere(
                    x => x.ProductId == dto.ProductId && x.AppUserId == userId,
                    includes: null)
                .AnyAsync();

            if (already)
                throw new BadRequestException("You already reviewed this product.");

            var review = _mapper.Map<Review>(dto);
            review.AppUserId = userId;
            review.IsApproved = false;

            await _reviewRepo.AddAsync(review);
            await _reviewRepo.SaveChangesAsync();
        }

        public async Task<ICollection<ReviewGetDto>> GetByProductAsync(int productId, int page = 1, int take = 10)
        {
            if (page < 1) page = 1;
            if (take < 1) take = 10;

            var query = _reviewRepo.GetAllWhere(
                x => x.ProductId == productId && x.IsApproved,
                skip: (page - 1) * take,
                take: take,
                includes: new[] { "AppUser" }
            );

            var list = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();
            return _mapper.Map<ICollection<ReviewGetDto>>(list);
        }
        public async Task<ICollection<ReviewGetDto>> GetAllAsync(int page, int take)
        {
            ICollection<Review> reviews = await _reviewRepo.GetAllWhere(
                skip: (page - 1) * take, take: take,includes: new[] { "AppUser" }).ToListAsync();
            return _mapper.Map<ICollection<ReviewGetDto>>(reviews);
        }
        public async Task<ReviewGetDto> GetAsync(int id)
        {
            Review review = await _reviewRepo.GetByIdAsync(id);
            if (review == null) throw new NotFoundException();
            return _mapper.Map<ReviewGetDto>(review);
        }
        public async Task<ICollection<ReviewGetDto>> GetAllbyApprovedandProduuct(int productId,bool IsApproved, int page = 1, int take = 10)
        {
            if (page < 1) page = 1;
            if (take < 1) take = 10;

            var query = _reviewRepo.GetAllWhere(
                x => x.ProductId == productId && x.IsApproved==IsApproved,
                skip: (page - 1) * take,
                take: take,
                includes: new[] { "AppUser" }
            );

            var list = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();
            return _mapper.Map<ICollection<ReviewGetDto>>(list);
        }
        public async Task ApproveAsync(int reviewId, bool isApproved)
        {
            var review = await _reviewRepo.GetByIdAsync(reviewId);
            if (review == null) throw new NotFoundException("Review not found.");

            review.IsApproved = isApproved;

            await _reviewRepo.UpdateAsync(review);
        }

        public async Task DeleteAsync(int reviewId)
        {
            var review = await _reviewRepo.GetByIdAsync(reviewId);
            if (review == null) throw new NotFoundException("Review not found.");

            await _reviewRepo.DeleteAsync(review);
        }
    }
}
