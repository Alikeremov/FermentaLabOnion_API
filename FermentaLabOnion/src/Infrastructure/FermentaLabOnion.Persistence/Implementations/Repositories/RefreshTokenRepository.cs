using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Domain.Entities;
using FermentaLabOnion.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Persistence.Implementations.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _db;

        public RefreshTokenRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<RefreshToken?> GetByHashAsync(string tokenHash)
        {
            var token = await _db.RefreshTokens
                .FirstOrDefaultAsync(x => x.TokenHash == tokenHash);

            return token;
        }

        public async Task AddAsync(RefreshToken token)
        {
            await _db.RefreshTokens.AddAsync(token);
        }

        public void Revoke(RefreshToken token, DateTime revokedAt)
        {
            token.RevokedAt = revokedAt;
        }

        public async Task RevokeAllActiveAsync(string appUserId, DateTime now)
        {
            var actives = await _db.RefreshTokens
                .Where(x => x.AppUserId == appUserId && x.RevokedAt == null && x.ExpiresAt > now)
                .ToListAsync();

            foreach (var t in actives)
            {
                t.RevokedAt = now;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
