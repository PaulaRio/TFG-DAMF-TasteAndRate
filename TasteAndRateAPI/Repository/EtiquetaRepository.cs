using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TasteAndRateAPI.Data;
using TasteAndRateAPI.Models.Entity;
using TasteAndRateAPI.Repository.IRepository;

namespace TasteAndRateAPI.Repository
{
    public class EtiquetaRepository : IEtiquetaRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string EtiquetaEntityCacheKey = "EtiquetaEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;
        
        public EtiquetaRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(EtiquetaEntityCacheKey);
        }


        public async Task<bool> CreateAsync(EtiquetaEntity EtiquetaEntity)
        {
            _context.Etiqueta.Add(EtiquetaEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var EtiquetaEntity = await GetAsync(id);
            if (EtiquetaEntity == null)
                return false;

            _context.Etiqueta.Remove(EtiquetaEntity);
            return await Save();
        }
        

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Etiqueta.AnyAsync(c => c.Id == id);
        }

        public async Task<ICollection<EtiquetaEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(EtiquetaEntityCacheKey, out ICollection<EtiquetaEntity> EtiquetasCached))
                return EtiquetasCached;

            var gruposFromDb = await _context.Etiqueta.OrderBy(c => c.Nombre).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(EtiquetaEntityCacheKey, gruposFromDb, cacheEntryOptions);
            return gruposFromDb;
        }

        public async Task<EtiquetaEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(EtiquetaEntityCacheKey, out ICollection<EtiquetaEntity> EtiquetaCached))
            {
                var EtiquetaEntity = EtiquetaCached.FirstOrDefault(c => c.Id == id);
                if (EtiquetaEntity != null)
                    return EtiquetaEntity;
            }

            return await _context.Etiqueta.FirstOrDefaultAsync(c => c.Id == id);
        }

       
        public async Task<bool> UpdateAsync(EtiquetaEntity EtiquetaEntity)
        {
            //EtiquetaEntity.CreatedDate = DateTime.Now;
            _context.Update(EtiquetaEntity);
            return await Save();
        }
    }
}
