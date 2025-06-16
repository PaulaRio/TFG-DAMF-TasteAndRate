using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TasteAndRateAPI.Data;
using TasteAndRateAPI.Models.Entity;
using TasteAndRateAPI.Repository.IRepository;

namespace TasteAndRateAPI.Repository
{
    public class ValoracionRepository : IValoracionRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string ValoracionEntityCacheKey = "ValoracionEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;
        
        public ValoracionRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(ValoracionEntityCacheKey);
        }


        public async Task<bool> CreateAsync(ValoracionEntity ValoracionEntity)
        {
            _context.Valoracion.Add(ValoracionEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ValoracionEntity = await GetAsync(id);
            if (ValoracionEntity == null)
                return false;

            _context.Valoracion.Remove(ValoracionEntity);
            return await Save();
        }
        

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Valoracion.AnyAsync(c => c.Id == id);
        }

        public async Task<ICollection<ValoracionEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(ValoracionEntityCacheKey, out ICollection<ValoracionEntity> ValoracionesCached))
                return ValoracionesCached;

            //var valoracionesFromDb = await _context.Valoracion.OrderBy(c => c.Id).ToListAsync();
            var valoracionesFromDb = await _context.Valoracion
        .Include(v => v.Gastro)  .OrderBy(c => c.Id).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(ValoracionEntityCacheKey, valoracionesFromDb, cacheEntryOptions);
            return valoracionesFromDb;
        }

        public async Task<ValoracionEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(ValoracionEntityCacheKey, out ICollection<ValoracionEntity> ValoracionCached))
            {
                var ValoracionEntity = ValoracionCached.FirstOrDefault(c => c.Id == id);
                if (ValoracionEntity != null)
                    return ValoracionEntity;
            }

            return await _context.Valoracion.FirstOrDefaultAsync(c => c.Id == id);
        }

       
        public async Task<bool> UpdateAsync(ValoracionEntity ValoracionEntity)
        {
            //ValoracionEntity.CreatedDate = DateTime.Now;
            _context.Update(ValoracionEntity);
            return await Save();
        }

        
    }
}
