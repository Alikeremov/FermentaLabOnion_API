using FermentaLabOnion.Application.DTOs.ReviewDTOs;
using FermentaLabOnion.Application.DTOs.ReviewDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Services
{
    public interface IReviewService
    {
        Task CreateAsync(ReviewCreateDto dto);
        Task<ICollection<ReviewGetDto>> GetAllAsync(int page, int take);
        Task<ReviewGetDto> GetAsync(int id);
        Task<ICollection<ReviewGetDto>> GetAllbyApprovedandProduuct(int productId, bool IsApproved, int page = 1, int take = 10);
        Task ApproveAsync(int reviewId, bool isApproved);
        Task DeleteAsync(int reviewId);
    }
}
