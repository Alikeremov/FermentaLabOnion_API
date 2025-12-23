using FermentaLabOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Abstraction.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByHashAsync(string tokenHash);
        Task AddAsync(RefreshToken token);

        void Revoke(RefreshToken token, DateTime revokedAt);
        Task RevokeAllActiveAsync(string appUserId, DateTime now);
        Task SaveChangesAsync();
        
    }
}
