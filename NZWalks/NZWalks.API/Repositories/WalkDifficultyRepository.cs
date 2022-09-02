using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        private readonly IMapper mapper;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext, IMapper mapper)
        {
            this.nZWalksDbContext = nZWalksDbContext;
            this.mapper = mapper;
        }

        public Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            throw new NotImplementedException();
        }

        public Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDbContext.WalkDifficulty.ToListAsync();
        }

        public Task<WalkDifficulty> GetAsync(Guid id)
        {
            return nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<WalkDifficulty> UpdateAsycn(Guid id, WalkDifficulty walkDifficulty)
        {
            throw new NotImplementedException();
        }
    }
}
