namespace RMS.Services
{
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Threading.Tasks;

    public class CacheService
    {
        /// <summary>
        /// Memory cache instance field.
        /// </summary>
        private IMemoryCache memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheService"/> class.
        /// </summary>
        public CacheService()
        {
            this.memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        /// <summary>
        /// Get or create memory cache record async.
        /// </summary>
        /// <typeparam name="TItem">Item to cache.</typeparam>
        /// <param name="key">Item key.</param>
        /// <param name="factory">Cache record factory.</param>
        /// <returns>Cached memory item.</returns>
        public async Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory)
        {
            return await this.memoryCache.GetOrCreateAsync<TItem>(key, factory);
        }

        /// <summary>
        /// Get or create memory cache record.
        /// </summary>
        /// <typeparam name="TItem">Item to cache.</typeparam>
        /// <param name="key">Item key.</param>
        /// <param name="factory">Cache record factory.</param>
        /// <returns>Cached memory item.</returns>
        public TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory)
        {
            return this.memoryCache.GetOrCreate<TItem>(key, factory);
        }

        /// <summary>
        /// Get or create memory cache record.
        /// </summary>
        /// <typeparam name="TItem">Item to cache.</typeparam>
        /// <param name="key">Item key.</param>
        /// <returns>Cached memory item.</returns>
        public TItem TryGetValue<TItem>(object key)
        {
            this.memoryCache.TryGetValue(key, out object result);

            if (result != null)
            {
                return (TItem)result;
            }

            return default;
        }

        /// <summary>
        /// Remove single item from cache.
        /// </summary>
        /// <param name="key">Key to remove.</param>
        public void RemoveSingle(object key)
        {
            this.memoryCache.Remove(key);
        }

        /// <summary>
        /// Clear all cached items.
        /// </summary>
        public void Clear()
        {
            this.memoryCache = new MemoryCache(new MemoryCacheOptions());
        }
    }
}
