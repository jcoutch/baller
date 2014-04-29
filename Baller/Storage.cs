using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SlightlyOrganized.Baller
{
	class Storage
	{
		public string StorageKey {get; private set; }
		public TimeSpan CacheDuration { get; set; }

		public Storage(string storageKey)
		{
			StorageKey = storageKey;
			CacheDuration = new TimeSpan(1, 0, 0, 0);
		}

		public void Write(LocalizationMessage localizationMessage)
		{
			var storage = GetStorage();
			storage.Add(localizationMessage);
			WriteStorage(storage);
		}

		public List<LocalizationMessage> GetStorage()
		{
			var storage = (HttpRuntime.Cache.Get(StorageKey) as List<LocalizationMessage>);

			if (storage == null)
				WriteStorage(storage = new List<LocalizationMessage>());

			return storage;
		}

		public void Clear()
		{
			WriteStorage(new List<LocalizationMessage>());
		}

		private void WriteStorage(List<LocalizationMessage> localizationMessages)
		{
			HttpRuntime.Cache.Insert(
					key: StorageKey,
					value: localizationMessages,
					dependencies: null,
					absoluteExpiration: DateTime.Now.Add(CacheDuration),
					slidingExpiration: System.Web.Caching.Cache.NoSlidingExpiration,
					priority: System.Web.Caching.CacheItemPriority.Low,
					onRemoveCallback: null);
		}
	}
}
