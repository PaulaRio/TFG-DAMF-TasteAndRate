using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TasteAndRateAPI.Data;
using TasteAndRateAPI.Models.Entity;
using TasteAndRateAPI.Repository.IRepository;

namespace TasteAndRateAPI.Repository
{
    public class GastroRepository : IGastroRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string GastroEntityCacheKey = "GastroEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;
        
        public GastroRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(GastroEntityCacheKey);
        }


        public async Task<bool> CreateAsync(GastroEntity GastroEntity)
        {
            _context.Gastro.Add(GastroEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var GastroEntity = await GetAsync(id);
            if (GastroEntity == null)
                return false;

            _context.Gastro.Remove(GastroEntity);
            return await Save();
        }
        

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Gastro.AnyAsync(c => c.Id == id);
        }

        public async Task<ICollection<GastroEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(GastroEntityCacheKey, out ICollection<GastroEntity> LibrosCached))
                return LibrosCached;

            var objetosFromDb = await _context.Gastro.OrderBy(c => c.Nombre).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(GastroEntityCacheKey, objetosFromDb, cacheEntryOptions);
            return objetosFromDb;
        }

        public async Task<GastroEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(GastroEntityCacheKey, out ICollection<GastroEntity> GastroCached))
            {
                var GastroEntity = GastroCached.FirstOrDefault(c => c.Id == id);
                if (GastroEntity != null)
                    return GastroEntity;
            }

            return await _context.Gastro.FirstOrDefaultAsync(c => c.Id == id);
        }

       
        public async Task<bool> UpdateAsync(GastroEntity GastroEntity)
        {
           // GastroEntity.CreatedDate = DateTime.Now;
            _context.Update(GastroEntity);
            return await Save();
        }
    }
}
