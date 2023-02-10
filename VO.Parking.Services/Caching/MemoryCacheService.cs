using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using VO.Parking.Services.Caching;
using System.Runtime.Caching;

namespace VO.Parking.Services
{
    public class MemoryCacheService : ICache
    {
        private readonly int _cacheTime = 60;
        /// <param name="cacheTime">The life time in minutes.</param>
        public MemoryCacheService(int cacheTime = 60)
        {
            _cacheTime = cacheTime;
        }
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
                // o colectie de KeyValuePair<string,object>
            }
        }
        /// Gets the value associated with the specified key.
        public T Get<T>(string key)
        {
            BinaryFormatter deserializer = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream((byte[])Cache[key]))
            {
                return (T)deserializer.Deserialize(memStream);
            }
        }
        /// Adds the specified key and object to the cache.
        public void Set(string key, object objectData, int? cacheTime = null)
        {
            if (objectData == null)
            {
                return;
            }
            var policy = new CacheItemPolicy();
            if (!cacheTime.HasValue)
            {
                cacheTime = _cacheTime;
            }
            policy.AbsoluteExpiration = DateTime.Now
            + TimeSpan.FromMinutes(cacheTime.Value);
            BinaryFormatter serializer = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                serializer.Serialize(memStream, objectData);
                Cache.Add(new CacheItem(key, memStream.ToArray()), policy);
            }
        }
        /// Gets a value indicating whether the value associated
        /// with the specified key is cached
        public bool IsSet(string key)
        {
            return Cache.Contains(key);
        }
        /// Removes the value with the specified key from the cache
        public void Remove(string key)
        {
            Cache.Remove(key);
        }
        public void RemoveByPattern(string pattern)
        {
            foreach (var item in Cache)
            {
                if (item.Key.StartsWith(pattern))
                {
                    Remove(item.Key);
                }
            }
        }
        /// Clear all cache data
        public void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }
    }
}
