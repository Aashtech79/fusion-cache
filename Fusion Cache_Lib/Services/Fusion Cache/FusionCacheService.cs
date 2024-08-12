using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiggyCreatures.Caching.Fusion;

namespace Fusion_Cache_Lib.Services.Fusion_Cache
{
	public interface IFusionCacheHelper
	{
		Task<T> SetCache<T>(string key, T value);
		Task<T> GetCache<T>(string key);
		Task<bool> RemoveCache(string key);

	}
	public class FusionCacheHelper : IFusionCacheHelper
	{
		private readonly IFusionCache _fusionCache;

		public FusionCacheHelper(IFusionCache fusionCache)
		{
			_fusionCache = fusionCache;
		}

		// Set a cache entry with the given key and value, including various options
		public async Task<T> SetCache<T>(string key, T value)
		{
			var options = new FusionCacheEntryOptions
			{
				Duration =  TimeSpan.FromMinutes(5), // Set Time-To-Live (TTL)
			};

			await _fusionCache.SetAsync(key, value, options);
			return value;
		}

		// Get a cache entry based on the key, with options for fail-safe and background refresh
		public async Task<T> GetCache<T>(string key)
		{
			var value = await _fusionCache.GetOrDefaultAsync<T>(key);
			return value;
		}

		// Remove a cache entry based on the key
		public async Task<bool> RemoveCache(string key)
		{
			await _fusionCache.RemoveAsync(key);
			return true; // Return true to indicate success
		}

	}
}
