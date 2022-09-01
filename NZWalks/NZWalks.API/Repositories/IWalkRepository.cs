using NZWalks.API.Models.Domain;
using System.Collections;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
    }
}
