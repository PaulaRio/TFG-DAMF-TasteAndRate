using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TasteAndRateAPI.Data;
using TasteAndRateAPI.Models.Entity;
using TasteAndRateAPI.Repository.IRepository;

namespace TasteAndRateAPI.Repository
{
    public class ValoracionCriterioRepository : IValoracionCriterioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string ValoracionCriterioEntityCacheKey = "ValoracionCriterioEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;

        public ValoracionCriterioRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(ValoracionCriterioEntityCacheKey);
        }


        public async Task<bool> CreateAsync(ValoracionCriterioEntity ValoracionCriterioEntity)
        {
            _context.ValoracionCriterio.Add(ValoracionCriterioEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ValoracionCriterioEntity = await GetAsync(id);
            if (ValoracionCriterioEntity == null)
                return false;

            _context.ValoracionCriterio.Remove(ValoracionCriterioEntity);
            return await Save();
        }


        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ValoracionCriterio.AnyAsync(c => c.Id == id);
        }

        public async Task<ICollection<ValoracionCriterioEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(ValoracionCriterioEntityCacheKey, out ICollection<ValoracionCriterioEntity> LibrosCached))
                return LibrosCached;

            var objetosFromDb = await _context.ValoracionCriterio.OrderBy(c => c.Id).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(ValoracionCriterioEntityCacheKey, objetosFromDb, cacheEntryOptions);
            return objetosFromDb;
        }

        public async Task<ValoracionCriterioEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(ValoracionCriterioEntityCacheKey, out ICollection<ValoracionCriterioEntity> ValoracionCriterioCached))
            {
                var ValoracionCriterioEntity = ValoracionCriterioCached.FirstOrDefault(c => c.Id == id);
                if (ValoracionCriterioEntity != null)
                    return ValoracionCriterioEntity;
            }

            return await _context.ValoracionCriterio.FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<bool> UpdateAsync(ValoracionCriterioEntity ValoracionCriterioEntity)
        {
            _context.Update(ValoracionCriterioEntity);
            return await Save();
        }
    }
}
