using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TasteAndRateAPI.Data;
using TasteAndRateAPI.Models.Entity;
using TasteAndRateAPI.Repository.IRepository;

namespace TasteAndRateAPI.Repository
{
    public class CriterioRepository : ICriterioRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string CriterioEntityCacheKey = "CriterioEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;
        
        public CriterioRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync() >= 0;
            if (result)
            {
                ClearCache();
            }
            return result;
        }
        public void ClearCache()
        {
            _cache.Remove(CriterioEntityCacheKey);
        }


        public async Task<bool> CreateAsync(CriterioEntity CriterioEntity)
        {
            _context.Criterio.Add(CriterioEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var CriterioEntity = await GetAsync(id);
            if (CriterioEntity == null)
                return false;

            _context.Criterio.Remove(CriterioEntity);
            return await Save();
        }
        

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Criterio.AnyAsync(c => c.Id == id);
        }

        public async Task<ICollection<CriterioEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(CriterioEntityCacheKey, out ICollection<CriterioEntity> CriteriosCached))
                return CriteriosCached;

            var gruposFromDb = await _context.Criterio.OrderBy(c => c.Nombre).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(CriterioEntityCacheKey, gruposFromDb, cacheEntryOptions);
            return gruposFromDb;
        }

        public async Task<CriterioEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(CriterioEntityCacheKey, out ICollection<CriterioEntity> CriterioCached))
            {
                var CriterioEntity = CriterioCached.FirstOrDefault(c => c.Id == id);
                if (CriterioEntity != null)
                    return CriterioEntity;
            }

            return await _context.Criterio.FirstOrDefaultAsync(c => c.Id == id);
        }

       
        public async Task<bool> UpdateAsync(CriterioEntity CriterioEntity)
        {
            //CriterioEntity.CreatedDate = DateTime.Now;
            _context.Update(CriterioEntity);
            return await Save();
        }
    }
}
