using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace TechStack.Infrastructure.Utilities
{
    /// <summary>
    /// MemoryCacheExtensions
    /// </summary>
    public static class MemoryCacheExtensions
    {
        /// <summary>
        /// GetOrCreate
        /// </summary>
        /// <param name="memoryCache">memoryCache</param>
        /// <param name="cacheKey">cacheKey</param>
        /// <param name="cacheEntry">cacheEntry</param>
        /// <param name="minutes">days</param>
        /// <returns>result</returns>
        public static object GetOrCreate(this IMemoryCache memoryCache, string cacheKey, object cacheEntry, int minutes = 0)
        {
            if (!memoryCache.TryGetValue(cacheKey, out cacheEntry))
            {
                // Key not in cache, so get data.
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(minutes > 0 ? CacheItemPriority.Normal : CacheItemPriority.NeverRemove);
                if (minutes > 0)
                {
                    //cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(minutes);
                    //cacheEntryOptions.SlidingExpiration = TimeSpan.FromDays(minutes);
                    cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes);
                }

                // Save data in cache.
                memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            cacheEntry = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cacheEntry));
            return cacheEntry;
        }

        /// <summary>
        /// Get / Set Cache
        /// </summary>
        /// <typeparam name="TResult">result for call back method</typeparam>
        /// <param name="memoryCache">cache</param>
        /// <param name="cacheKey">key</param>
        /// <param name="callBack">call back method</param>
        /// <param name="minutes">Expiry</param>
        /// <returns>Result</returns>
        public static object GetOrCreate<TResult>(this IMemoryCache memoryCache, string cacheKey, Func<TResult> callBack, int minutes = 0)
        {
            object cacheEntry = null;
            if (!memoryCache.TryGetValue(cacheKey, out cacheEntry))
            {
                // Key not in cache, so get data.
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(minutes > 0 ? CacheItemPriority.Normal : CacheItemPriority.NeverRemove);
                if (minutes > 0)
                {
                    //cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(minutes);
                    //cacheEntryOptions.SlidingExpiration = TimeSpan.FromDays(minutes);
                    cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes);
                }

                // Save data in cache.
                cacheEntry = callBack();
                memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            cacheEntry = JsonConvert.DeserializeObject<TResult>(JsonConvert.SerializeObject(cacheEntry));
            return cacheEntry;
        }

        /// <summary>
        /// Get / Set cache
        /// </summary>
        /// <typeparam name="T">Method params</typeparam>
        /// <typeparam name="TResult">Method Result</typeparam>
        /// <param name="memoryCache">cache</param>
        /// <param name="cacheKey">key</param>
        /// <param name="callBack">call back methos</param>
        /// <param name="arg1">argument</param>
        /// <param name="minutes">days</param>
        /// <returns>Result</returns>
        public static object GetOrCreate<T, TResult>(this IMemoryCache memoryCache, string cacheKey, Func<T, TResult> callBack, T arg1, int minutes = 0)
        {
            object cacheEntry = null;
            if (!memoryCache.TryGetValue(cacheKey, out cacheEntry))
            {
                // Key not in cache, so get data.
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(minutes > 0 ? CacheItemPriority.Normal : CacheItemPriority.NeverRemove);
                if (minutes > 0)
                {
                    //cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(days);
                    //cacheEntryOptions.SlidingExpiration = TimeSpan.FromDays(days);
                    cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes);
                }

                // Save data in cache.
                cacheEntry = callBack(arg1);
                memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            cacheEntry = JsonConvert.DeserializeObject<TResult>(JsonConvert.SerializeObject(cacheEntry));
            return cacheEntry;
        }

        /// <summary>
        /// Get or Set Cache details
        /// </summary>
        /// <typeparam name="T1">argumentType1</typeparam>
        /// <typeparam name="T2">argumentType2</typeparam>
        /// <typeparam name="TResult">Call back method</typeparam>
        /// <param name="memoryCache">cache</param>
        /// <param name="cacheKey">key</param>
        /// <param name="callBack">call back functions</param>
        /// <param name="arg1">argument1</param>
        /// <param name="arg2">argument2</param>
        /// <param name="minutes">days</param>
        /// <returns>Result</returns>
        public static object GetOrCreate<T1, T2, TResult>(this IMemoryCache memoryCache, string cacheKey, Func<T1, T2, TResult> callBack, T1 arg1, T2 arg2, int minutes = 0)
        {
            object cacheEntry = null;
            if (!memoryCache.TryGetValue(cacheKey, out cacheEntry))
            {
                // Key not in cache, so get data.
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(minutes > 0 ? CacheItemPriority.Normal : CacheItemPriority.NeverRemove);
                if (minutes > 0)
                {
                    //cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(days);
                    //cacheEntryOptions.SlidingExpiration = TimeSpan.FromDays(days);
                    //cacheEntryOptions.AbsoluteExpiration = new DateTimeOffset(DateTime.Today.AddDays(days));
                    cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes);
                }

                // Save data in cache.
                cacheEntry = callBack(arg1, arg2);
                memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            cacheEntry = JsonConvert.DeserializeObject<TResult>(JsonConvert.SerializeObject(cacheEntry));
            return cacheEntry;
        }

        /// <summary>
        /// Get / Set Cache
        /// </summary>
        /// <typeparam name="T1">argument1 type</typeparam>
        /// <typeparam name="T2">argument2 type</typeparam>
        /// <typeparam name="T3">argument3 type</typeparam>
        /// <typeparam name="TResult">method result</typeparam>
        /// <param name="memoryCache">cache</param>
        /// <param name="cacheKey">key</param>
        /// <param name="callBack">call back method</param>
        /// <param name="arg1">argument1</param>
        /// <param name="arg2">argument2</param>
        /// <param name="arg3">argument3</param>
        /// <param name="minutes">days</param>
        /// <returns>Result</returns>
        public static object GetOrCreate<T1, T2, T3, TResult>(this IMemoryCache memoryCache, string cacheKey, Func<T1, T2, T3, TResult> callBack, T1 arg1, T2 arg2, T3 arg3, int minutes = 0)
        {
            object cacheEntry = null;
            if (!memoryCache.TryGetValue(cacheKey, out cacheEntry))
            {
                // Key not in cache, so get data.
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(minutes > 0 ? CacheItemPriority.Normal : CacheItemPriority.NeverRemove);
                if (minutes > 0)
                {
                    //As per the discussion, following lines of code have been commented to check cache expiry in minutes.
                    //cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(days);
                    //cacheEntryOptions.SlidingExpiration = TimeSpan.FromDays(days);
                    cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes);
                }

                // Save data in cache.
                cacheEntry = callBack(arg1, arg2, arg3);
                memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            cacheEntry = JsonConvert.DeserializeObject<TResult>(JsonConvert.SerializeObject(cacheEntry));
            return cacheEntry;
        }

        /// <summary>
        /// Clear memory cache.
        /// </summary>
        /// <param name="memoryCache">memoryCache</param>
        /// <param name="keys">keys</param>
        /// <returns>bool</returns>
        public static bool ClearCache(this IMemoryCache memoryCache, params string[] keys)
        {
            bool result = false;
            dynamic cacheItems;
            foreach (var key in keys)
            {
                if (memoryCache.TryGetValue(key, out cacheItems))
                {
                    memoryCache.Remove(key);
                }
            }

            result = true;
            return result;
        }

        /// <summary>
        /// Clear memory cache.
        /// </summary>
        /// <param name="memoryCache">memoryCache</param>
        /// <returns>bool</returns>
        public static bool ClearCache(this IMemoryCache memoryCache)
        {
            bool result = false;
            dynamic cacheItems;
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field.GetValue(memoryCache) as ICollection;
            var items = new List<string>();
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo.GetValue(item);
                    items.Add(val.ToString());
                }
            }

            string[] keys = items.ToArray();
            foreach (var key in keys)
            {
                if (!key.Contains("Microsoft.EntityFrameworkCore"))
                {
                    if (memoryCache.TryGetValue(key, out cacheItems))
                    {
                        memoryCache.Remove(key);
                    }
                }
            }

            result = true;
            return result;
        }

        /// <summary>
        /// Clear memory cache.
        /// </summary>
        /// <param name="memoryCache">memoryCache</param>
        /// <returns>bool</returns>
        public static bool ClearCacheExclude(this IMemoryCache memoryCache, params string[] exclude)
        {
            bool result = false;
            dynamic cacheItems;
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field.GetValue(memoryCache) as ICollection;
            var items = new List<string>();
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo.GetValue(item);
                    items.Add(val.ToString());
                }
            }

            string[] keys = items.ToArray();
            foreach (var key in keys)
            {
                if (exclude != null && exclude.Any(key.Contains))
                {
                    continue;
                }
                if (!key.Contains("Microsoft.EntityFrameworkCore"))
                {
                    if (memoryCache.TryGetValue(key, out cacheItems))
                    {
                        memoryCache.Remove(key);
                    }
                }
            }

            result = true;
            return result;
        }
        /// <summary>
        /// Clear memory cache.
        /// </summary>
        /// <param name="memoryCache">memoryCache</param>
        /// <returns>bool</returns>
        public static bool ClearCacheByStartwithKey(this IMemoryCache memoryCache, params string[] contains)
        {
            bool result = false;
            dynamic cacheItems;
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field.GetValue(memoryCache) as ICollection;
            var items = new List<string>();
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo.GetValue(item);
                    items.Add(val.ToString());
                }
            }

            string[] keys = items.ToArray();
            foreach (var key in keys)
            {
                if (!key.Contains("Microsoft.EntityFrameworkCore"))
                {
                    foreach (var contain in contains)
                    {
                        if (key.Contains(contain))
                        {
                            if (memoryCache.TryGetValue(key, out cacheItems))
                            {
                                memoryCache.Remove(key);
                            }
                        }
                    }

                }
            }

            result = true;
            return result;
        }
    }
}
