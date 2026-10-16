using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Caching
{
	/// <summary>Implements the cache for a Web application. This class cannot be inherited.</summary>
	// Token: 0x02000676 RID: 1654
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class Cache : IEnumerable
	{
		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x060046AB RID: 18091 RVA: 0x000C44E4 File Offset: 0x000C26E4
		private bool DisableExpiration
		{
			get
			{
				if (this.disableExpiration == null)
				{
					CacheSection cacheSection = WebConfigurationManager.GetWebApplicationSection("system.web/caching/cache") as CacheSection;
					if (cacheSection == null)
					{
						this.disableExpiration = new bool?(false);
					}
					else
					{
						this.disableExpiration = new bool?(cacheSection.DisableExpiration);
					}
				}
				return this.disableExpiration.Value;
			}
		}

		/// <summary>Gets the number of bytes available for the cache.</summary>
		/// <returns>The number of bytes available for the cache.</returns>
		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x060046AC RID: 18092 RVA: 0x000C453C File Offset: 0x000C273C
		public long EffectivePrivateBytesLimit
		{
			get
			{
				if (this.privateBytesLimit == -1L)
				{
					CacheSection cacheSection = WebConfigurationManager.GetWebApplicationSection("system.web/caching/cache") as CacheSection;
					if (cacheSection == null)
					{
						this.privateBytesLimit = 0L;
					}
					else
					{
						this.privateBytesLimit = cacheSection.PrivateBytesLimit;
					}
					if (this.privateBytesLimit == 0L)
					{
						this.privateBytesLimit = 734003200L;
					}
				}
				return this.privateBytesLimit;
			}
		}

		/// <summary>Gets the percentage of physical memory that can be consumed by an application before ASP.NET starts removing items from the cache.</summary>
		/// <returns>The percentage of physical memory available to the application.</returns>
		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x060046AD RID: 18093 RVA: 0x000C4598 File Offset: 0x000C2798
		public long EffectivePercentagePhysicalMemoryLimit
		{
			get
			{
				if (this.percentagePhysicalMemoryLimit == -1L)
				{
					CacheSection cacheSection = WebConfigurationManager.GetWebApplicationSection("system.web/caching/cache") as CacheSection;
					if (cacheSection == null)
					{
						this.percentagePhysicalMemoryLimit = 0L;
					}
					else
					{
						this.percentagePhysicalMemoryLimit = (long)cacheSection.PercentagePhysicalMemoryUsedLimit;
					}
					if (this.percentagePhysicalMemoryLimit == 0L)
					{
						this.percentagePhysicalMemoryLimit = 97L;
					}
				}
				return this.percentagePhysicalMemoryLimit;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.Cache" /> class.</summary>
		// Token: 0x060046AE RID: 18094 RVA: 0x000C45F0 File Offset: 0x000C27F0
		public Cache()
		{
			this.cacheLock = new ReaderWriterLockSlim();
			this.cache = new CacheItemLRU(this, 15000, 10000);
		}

		/// <summary>Gets the number of items stored in the cache.</summary>
		/// <returns>The number of items stored in the cache.</returns>
		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x060046AF RID: 18095 RVA: 0x000C4629 File Offset: 0x000C2829
		public int Count
		{
			get
			{
				return this.cache.Count;
			}
		}

		/// <summary>Gets or sets the cache item at the specified key.</summary>
		/// <returns>The specified cache item.</returns>
		/// <param name="key">A <see cref="T:System.String" /> object that represents the key for the cache item.</param>
		// Token: 0x170015F6 RID: 5622
		public object this[string key]
		{
			get
			{
				return this.Get(key);
			}
			set
			{
				this.Insert(key, value);
			}
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x000C464C File Offset: 0x000C284C
		private CacheItem RemoveCacheItem(string key)
		{
			if (key == null)
			{
				return null;
			}
			CacheItem cacheItem = this.cache[key];
			if (cacheItem == null)
			{
				return null;
			}
			CacheItemPriorityQueue cacheItemPriorityQueue = this.timedItems;
			cacheItem.Disabled = true;
			this.cache.Remove(key);
			return cacheItem;
		}

		/// <summary>Adds the specified item to the <see cref="T:System.Web.Caching.Cache" /> object with dependencies, expiration and priority policies, and a delegate you can use to notify your application when the inserted item is removed from the Cache.</summary>
		/// <returns>An object that represents the item that was added if the item was previously stored in the cache; otherwise, null.</returns>
		/// <param name="key">The cache key used to reference the item. </param>
		/// <param name="value">The item to be added to the cache. </param>
		/// <param name="dependencies">The file or cache key dependencies for the item. When any dependency changes, the object becomes invalid and is removed from the cache. If there are no dependencies, this parameter contains null.</param>
		/// <param name="absoluteExpiration">The time at which the added object expires and is removed from the cache. If you are using sliding expiration, the <paramref name="absoluteExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoAbsoluteExpiration" />.</param>
		/// <param name="slidingExpiration">The interval between the time the added object was last accessed and the time at which that object expires. If this value is the equivalent of 20 minutes, the object expires and is removed from the cache 20 minutes after it is last accessed. If you are using absolute expiration, the <paramref name="slidingExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoSlidingExpiration" />.</param>
		/// <param name="priority">The relative cost of the object, as expressed by the <see cref="T:System.Web.Caching.CacheItemPriority" /> enumeration. The cache uses this value when it evicts objects; objects with a lower cost are removed from the cache before objects with a higher cost. </param>
		/// <param name="onRemoveCallback">A delegate that, if provided, is called when an object is removed from the cache. You can use this to notify applications when their objects are deleted from the cache.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="value" /> parameter is set to null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="slidingExpiration" /> parameter is set to less than TimeSpan.Zero or more than one year.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="absoluteExpiration" /> and <paramref name="slidingExpiration" /> parameters are both set for the item you are trying to add to the Cache.</exception>
		// Token: 0x060046B3 RID: 18099 RVA: 0x000C468C File Offset: 0x000C288C
		public object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			try
			{
				this.cacheLock.EnterWriteLock();
				CacheItem cacheItem = this.cache[key];
				if (cacheItem != null)
				{
					return cacheItem.Value;
				}
				this.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback, null, false);
			}
			finally
			{
				this.cacheLock.ExitWriteLock();
			}
			return null;
		}

		/// <summary>Retrieves the specified item from the <see cref="T:System.Web.Caching.Cache" /> object.</summary>
		/// <returns>The retrieved cache item, or null if the key is not found.</returns>
		/// <param name="key">The identifier for the cache item to retrieve.</param>
		// Token: 0x060046B4 RID: 18100 RVA: 0x000C4700 File Offset: 0x000C2900
		public object Get(string key)
		{
			object obj;
			try
			{
				this.cacheLock.EnterUpgradeableReadLock();
				CacheItem cacheItem = this.cache[key];
				if (cacheItem == null)
				{
					obj = null;
				}
				else if (cacheItem.Dependency != null && cacheItem.Dependency.HasChanged)
				{
					try
					{
						this.cacheLock.EnterWriteLock();
						if (!this.NeedsUpdate(cacheItem, CacheItemUpdateReason.DependencyChanged, false))
						{
							this.Remove(cacheItem.Key, CacheItemRemovedReason.DependencyChanged, false, true);
						}
					}
					finally
					{
						this.cacheLock.ExitWriteLock();
					}
					obj = null;
				}
				else
				{
					if (!this.DisableExpiration)
					{
						if (cacheItem.SlidingExpiration != Cache.NoSlidingExpiration)
						{
							cacheItem.AbsoluteExpiration = DateTime.Now + cacheItem.SlidingExpiration;
							long num = (long)cacheItem.SlidingExpiration.TotalMilliseconds;
							cacheItem.ExpiresAt = cacheItem.AbsoluteExpiration.Ticks;
							if (this.expirationTimer != null && (this.expirationTimerPeriod == 0L || this.expirationTimerPeriod > num))
							{
								this.expirationTimerPeriod = num;
								this.expirationTimer.Change(this.expirationTimerPeriod, this.expirationTimerPeriod);
							}
						}
						else if (DateTime.Now >= cacheItem.AbsoluteExpiration)
						{
							try
							{
								this.cacheLock.EnterWriteLock();
								if (!this.NeedsUpdate(cacheItem, CacheItemUpdateReason.Expired, false))
								{
									this.Remove(key, CacheItemRemovedReason.Expired, false, true);
								}
							}
							finally
							{
								this.cacheLock.ExitWriteLock();
							}
							return null;
						}
					}
					obj = cacheItem.Value;
				}
			}
			finally
			{
				this.cacheLock.ExitUpgradeableReadLock();
			}
			return obj;
		}

		/// <summary>Inserts an item into the <see cref="T:System.Web.Caching.Cache" /> object with a cache key to reference its location, using default values provided by the <see cref="T:System.Web.Caching.CacheItemPriority" /> enumeration.</summary>
		/// <param name="key">The cache key used to reference the item. </param>
		/// <param name="value">The object to be inserted into the cache.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="value" /> parameter is null.</exception>
		// Token: 0x060046B5 RID: 18101 RVA: 0x000C48B4 File Offset: 0x000C2AB4
		public void Insert(string key, object value)
		{
			this.Insert(key, value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null, null, true);
		}

		/// <summary>Inserts an object into the <see cref="T:System.Web.Caching.Cache" /> that has file or key dependencies.</summary>
		/// <param name="key">The cache key used to identify the item.</param>
		/// <param name="value">The object to be inserted in the cache.</param>
		/// <param name="dependencies">The file or cache key dependencies for the inserted object. When any dependency changes, the object becomes invalid and is removed from the cache. If there are no dependencies, this parameter contains null.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="value" /> parameter is null.</exception>
		// Token: 0x060046B6 RID: 18102 RVA: 0x000C48D8 File Offset: 0x000C2AD8
		public void Insert(string key, object value, CacheDependency dependencies)
		{
			this.Insert(key, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null, null, true);
		}

		/// <summary>Inserts an object into the <see cref="T:System.Web.Caching.Cache" /> with dependencies and expiration policies.</summary>
		/// <param name="key">The cache key used to reference the object. </param>
		/// <param name="value">The object to be inserted in the cache. </param>
		/// <param name="dependencies">The file or cache key dependencies for the inserted object. When any dependency changes, the object becomes invalid and is removed from the cache. If there are no dependencies, this parameter contains null.</param>
		/// <param name="absoluteExpiration">The time at which the inserted object expires and is removed from the cache. To avoid possible issues with local time such as changes from standard time to daylight saving time, use <see cref="P:System.DateTime.UtcNow" /> rather than <see cref="P:System.DateTime.Now" /> for this parameter value. If you are using absolute expiration, the <paramref name="slidingExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoSlidingExpiration" />.</param>
		/// <param name="slidingExpiration">The interval between the time the inserted object is last accessed and the time at which that object expires. If this value is the equivalent of 20 minutes, the object will expire and be removed from the cache 20 minutes after it was last accessed. If you are using sliding expiration, the <paramref name="absoluteExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoAbsoluteExpiration" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">You set the <paramref name="slidingExpiration" /> parameter to less than TimeSpan.Zero or the equivalent of more than one year.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="absoluteExpiration" /> and <paramref name="slidingExpiration" /> parameters are both set for the item you are trying to add to the Cache.</exception>
		// Token: 0x060046B7 RID: 18103 RVA: 0x000C48FC File Offset: 0x000C2AFC
		public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
		{
			this.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, CacheItemPriority.Normal, null, null, true);
		}

		/// <summary>Inserts an object into the <see cref="T:System.Web.Caching.Cache" /> object together with dependencies, expiration policies, and a delegate that you can use to notify the application before the item is removed from the cache.</summary>
		/// <param name="key">The cache key that is used to reference the object.</param>
		/// <param name="value">The object to insert into the cache.</param>
		/// <param name="dependencies">The file or cache key dependencies for the item. When any dependency changes, the object becomes invalid and is removed from the cache. If there are no dependencies, this parameter contains null.</param>
		/// <param name="absoluteExpiration">The time at which the inserted object expires and is removed from the cache. To avoid possible issues with local time such as changes from standard time to daylight saving time, use <see cref="P:System.DateTime.UtcNow" /> instead of <see cref="P:System.DateTime.Now" /> for this parameter value. If you are using absolute expiration, the <paramref name="slidingExpiration" /> parameter must be set to <see cref="F:System.Web.Caching.Cache.NoSlidingExpiration" />.</param>
		/// <param name="slidingExpiration">The interval between the time that the cached object was last accessed and the time at which that object expires. If this value is the equivalent of 20 minutes, the object will expire and be removed from the cache 20 minutes after it was last accessed. If you are using sliding expiration, the <paramref name="absoluteExpiration" /> parameter must be set to <see cref="F:System.Web.Caching.Cache.NoAbsoluteExpiration" />.</param>
		/// <param name="onUpdateCallback">A delegate that will be called before the object is removed from the cache. You can use this to update the cached item and ensure that it is not removed from the cache.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" />, <paramref name="value" />, or <paramref name="onUpdateCallback" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">You set the <paramref name="slidingExpiration" /> parameter to less than TimeSpan.Zero or the equivalent of more than one year.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="absoluteExpiration" /> and <paramref name="slidingExpiration" /> parameters are both set for the item you are trying to add to the Cache.-or-The <paramref name="dependencies" /> parameter is null, and the <paramref name="absoluteExpiration" /> parameter is set to <see cref="F:System.Web.Caching.Cache.NoAbsoluteExpiration" />, and the <paramref name="slidingExpiration" /> parameter is set to <see cref="F:System.Web.Caching.Cache.NoSlidingExpiration" />.</exception>
		// Token: 0x060046B8 RID: 18104 RVA: 0x000C491C File Offset: 0x000C2B1C
		public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
		{
			this.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, CacheItemPriority.Normal, null, onUpdateCallback, true);
		}

		/// <summary>Inserts an object into the <see cref="T:System.Web.Caching.Cache" /> object with dependencies, expiration and priority policies, and a delegate you can use to notify your application when the inserted item is removed from the Cache.</summary>
		/// <param name="key">The cache key used to reference the object.</param>
		/// <param name="value">The object to be inserted in the cache.</param>
		/// <param name="dependencies">The file or cache key dependencies for the item. When any dependency changes, the object becomes invalid and is removed from the cache. If there are no dependencies, this parameter contains null.</param>
		/// <param name="absoluteExpiration">The time at which the inserted object expires and is removed from the cache. To avoid possible issues with local time such as changes from standard time to daylight saving time, use <see cref="P:System.DateTime.UtcNow" /> rather than <see cref="P:System.DateTime.Now" /> for this parameter value. If you are using absolute expiration, the <paramref name="slidingExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoSlidingExpiration" />.</param>
		/// <param name="slidingExpiration">The interval between the time the inserted object was last accessed and the time at which that object expires. If this value is the equivalent of 20 minutes, the object will expire and be removed from the cache 20 minutes after it was last accessed. If you are using sliding expiration, the <paramref name="absoluteExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoAbsoluteExpiration" />.</param>
		/// <param name="priority">The cost of the object relative to other items stored in the cache, as expressed by the <see cref="T:System.Web.Caching.CacheItemPriority" /> enumeration. This value is used by the cache when it evicts objects; objects with a lower cost are removed from the cache before objects with a higher cost.</param>
		/// <param name="onRemoveCallback">A delegate that, if provided, will be called when an object is removed from the cache. You can use this to notify applications when their objects are deleted from the cache.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">You set the <paramref name="slidingExpiration" /> parameter to less than TimeSpan.Zero or the equivalent of more than one year.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="absoluteExpiration" /> and <paramref name="slidingExpiration" /> parameters are both set for the item you are trying to add to the Cache.</exception>
		// Token: 0x060046B9 RID: 18105 RVA: 0x000C493C File Offset: 0x000C2B3C
		public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
		{
			this.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback, null, true);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x000C495C File Offset: 0x000C2B5C
		private void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback, CacheItemUpdateCallback onUpdateCallback, bool doLock)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (slidingExpiration < TimeSpan.Zero || slidingExpiration > TimeSpan.FromDays(365.0))
			{
				throw new ArgumentNullException("slidingExpiration");
			}
			if (absoluteExpiration != Cache.NoAbsoluteExpiration && slidingExpiration != Cache.NoSlidingExpiration)
			{
				throw new ArgumentException("Both absoluteExpiration and slidingExpiration are specified");
			}
			CacheItem cacheItem = new CacheItem();
			cacheItem.Value = value;
			cacheItem.Key = key;
			if (dependencies != null)
			{
				cacheItem.Dependency = dependencies;
				dependencies.DependencyChanged += this.OnDependencyChanged;
				dependencies.SetCache(this.DependencyCache);
			}
			cacheItem.Priority = priority;
			this.SetItemTimeout(cacheItem, absoluteExpiration, slidingExpiration, onRemoveCallback, onUpdateCallback, key, doLock);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x000C4A34 File Offset: 0x000C2C34
		internal void SetItemTimeout(string key, DateTime absoluteExpiration, TimeSpan slidingExpiration, bool doLock)
		{
			try
			{
				if (doLock)
				{
					this.cacheLock.EnterWriteLock();
				}
				CacheItem cacheItem = this.cache[key];
				if (cacheItem != null)
				{
					this.SetItemTimeout(cacheItem, absoluteExpiration, slidingExpiration, cacheItem.OnRemoveCallback, null, key, false);
				}
			}
			finally
			{
				if (doLock)
				{
					this.cacheLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x000C4A98 File Offset: 0x000C2C98
		private void SetItemTimeout(CacheItem ci, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemRemovedCallback onRemoveCallback, CacheItemUpdateCallback onUpdateCallback, string key, bool doLock)
		{
			bool flag = this.DisableExpiration;
			if (!flag)
			{
				ci.SlidingExpiration = slidingExpiration;
				if (slidingExpiration != Cache.NoSlidingExpiration)
				{
					ci.AbsoluteExpiration = DateTime.Now + slidingExpiration;
				}
				else
				{
					ci.AbsoluteExpiration = absoluteExpiration;
				}
			}
			ci.OnRemoveCallback = onRemoveCallback;
			ci.OnUpdateCallback = onUpdateCallback;
			try
			{
				if (doLock)
				{
					this.cacheLock.EnterWriteLock();
				}
				if (key != null)
				{
					this.cache[key] = ci;
					this.cache.EvictIfNecessary();
				}
				ci.LastChange = DateTime.Now;
				if (!flag && ci.AbsoluteExpiration != Cache.NoAbsoluteExpiration)
				{
					bool flag2;
					if (ci.IsTimedItem)
					{
						flag2 = this.UpdateTimedItem(ci);
						if (!flag2)
						{
							this.UpdateTimerPeriod(ci);
						}
					}
					else
					{
						flag2 = true;
					}
					if (flag2)
					{
						ci.IsTimedItem = true;
						this.EnqueueTimedItem(ci);
					}
				}
			}
			finally
			{
				if (doLock)
				{
					this.cacheLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x000C4B8C File Offset: 0x000C2D8C
		private bool UpdateTimedItem(CacheItem item)
		{
			if (this.timedItems == null)
			{
				return true;
			}
			item.ExpiresAt = item.AbsoluteExpiration.Ticks;
			return !this.timedItems.Update(item);
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x000C4BB8 File Offset: 0x000C2DB8
		private void UpdateTimerPeriod(CacheItem item)
		{
			if (this.timedItems == null)
			{
				this.timedItems = new CacheItemPriorityQueue();
			}
			long num = Math.Max(0L, (long)(item.AbsoluteExpiration - DateTime.Now).TotalMilliseconds);
			item.ExpiresAt = item.AbsoluteExpiration.Ticks;
			if (num > (long)((ulong)(-2)))
			{
				num = (long)((ulong)(-2));
			}
			if (this.expirationTimer != null && this.expirationTimerPeriod <= num)
			{
				return;
			}
			this.expirationTimerPeriod = num;
			if (this.expirationTimer == null)
			{
				this.expirationTimer = new Timer(new TimerCallback(this.ExpireItems), null, this.expirationTimerPeriod, this.expirationTimerPeriod);
				return;
			}
			this.expirationTimer.Change(this.expirationTimerPeriod, this.expirationTimerPeriod);
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x000C4C72 File Offset: 0x000C2E72
		private void EnqueueTimedItem(CacheItem item)
		{
			this.UpdateTimerPeriod(item);
			this.timedItems.Enqueue(item);
		}

		/// <summary>Removes the specified item from the application's <see cref="T:System.Web.Caching.Cache" /> object.</summary>
		/// <returns>The item removed from the Cache. If the value in the key parameter is not found, returns null.</returns>
		/// <param name="key">A <see cref="T:System.String" /> identifier for the cache item to remove.</param>
		// Token: 0x060046C0 RID: 18112 RVA: 0x000C4C87 File Offset: 0x000C2E87
		public object Remove(string key)
		{
			return this.Remove(key, CacheItemRemovedReason.Removed, true, true);
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x000C4C94 File Offset: 0x000C2E94
		internal object Remove(string key, CacheItemRemovedReason reason, bool doLock, bool invokeCallback)
		{
			CacheItem cacheItem = null;
			try
			{
				if (doLock)
				{
					this.cacheLock.EnterWriteLock();
				}
				cacheItem = this.RemoveCacheItem(key);
			}
			finally
			{
				if (doLock)
				{
					this.cacheLock.ExitWriteLock();
				}
			}
			object obj = null;
			if (cacheItem != null)
			{
				if (cacheItem.Dependency != null)
				{
					cacheItem.Dependency.SetCache(null);
					cacheItem.Dependency.DependencyChanged -= this.OnDependencyChanged;
					cacheItem.Dependency.Dispose();
				}
				if (invokeCallback && cacheItem.OnRemoveCallback != null)
				{
					try
					{
						cacheItem.OnRemoveCallback(key, cacheItem.Value, reason);
					}
					catch
					{
					}
				}
				obj = cacheItem.Value;
				cacheItem.Value = null;
				cacheItem.Key = null;
				cacheItem.Dependency = null;
				cacheItem.OnRemoveCallback = null;
				cacheItem.OnUpdateCallback = null;
				cacheItem = null;
			}
			return obj;
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x000C4D78 File Offset: 0x000C2F78
		internal void InvokePrivateCallbacks()
		{
			try
			{
				this.cacheLock.EnterReadLock();
				this.cache.InvokePrivateCallbacks();
			}
			finally
			{
				this.cacheLock.ExitReadLock();
			}
		}

		/// <summary>Retrieves a dictionary enumerator used to iterate through the key settings and their values contained in the cache.</summary>
		/// <returns>An enumerator to iterate through the <see cref="T:System.Web.Caching.Cache" /> object.</returns>
		// Token: 0x060046C3 RID: 18115 RVA: 0x000C4DBC File Offset: 0x000C2FBC
		public IDictionaryEnumerator GetEnumerator()
		{
			List<CacheItem> list = null;
			try
			{
				this.cacheLock.EnterReadLock();
				list = this.cache.ToList();
			}
			finally
			{
				this.cacheLock.ExitReadLock();
			}
			return new CacheItemEnumerator(list);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Web.Caching.Cache" /> object collection.</summary>
		/// <returns>An enumerator that can iterate through the <see cref="T:System.Web.Caching.Cache" /> object collection.</returns>
		// Token: 0x060046C4 RID: 18116 RVA: 0x000C4E08 File Offset: 0x000C3008
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x000C4E10 File Offset: 0x000C3010
		private void OnDependencyChanged(object o, EventArgs a)
		{
			this.CheckDependencies();
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x000C4E18 File Offset: 0x000C3018
		private bool NeedsUpdate(CacheItem item, CacheItemUpdateReason reason, bool needLock)
		{
			bool flag;
			try
			{
				if (needLock)
				{
					this.cacheLock.EnterWriteLock();
				}
				if (item == null || item.OnUpdateCallback == null)
				{
					flag = false;
				}
				else
				{
					string key = item.Key;
					CacheItemUpdateCallback onUpdateCallback = item.OnUpdateCallback;
					object obj;
					CacheDependency cacheDependency;
					DateTime dateTime;
					TimeSpan timeSpan;
					onUpdateCallback(key, reason, out obj, out cacheDependency, out dateTime, out timeSpan);
					if (obj == null)
					{
						flag = false;
					}
					else
					{
						CacheItemPriority priority = item.Priority;
						CacheItemRemovedCallback onRemoveCallback = item.OnRemoveCallback;
						CacheItemRemovedReason cacheItemRemovedReason;
						if (reason != CacheItemUpdateReason.Expired)
						{
							if (reason != CacheItemUpdateReason.DependencyChanged)
							{
								cacheItemRemovedReason = CacheItemRemovedReason.Removed;
							}
							else
							{
								cacheItemRemovedReason = CacheItemRemovedReason.DependencyChanged;
							}
						}
						else
						{
							cacheItemRemovedReason = CacheItemRemovedReason.Expired;
						}
						this.Remove(key, cacheItemRemovedReason, false, false);
						this.Insert(key, obj, cacheDependency, dateTime, timeSpan, priority, onRemoveCallback, onUpdateCallback, false);
						flag = true;
					}
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			finally
			{
				if (needLock)
				{
					this.cacheLock.ExitWriteLock();
				}
			}
			return flag;
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x000C4EF0 File Offset: 0x000C30F0
		private void ExpireItems(object data)
		{
			DateTime now = DateTime.Now;
			CacheItem cacheItem = null;
			this.expirationTimer.Change(-1, -1);
			try
			{
				this.cacheLock.EnterWriteLock();
				for (;;)
				{
					cacheItem = this.timedItems.Peek();
					if (cacheItem == null)
					{
						if (this.timedItems.Count == 0)
						{
							break;
						}
						this.timedItems.Dequeue();
					}
					else
					{
						if (!cacheItem.Disabled && cacheItem.ExpiresAt > now.Ticks)
						{
							break;
						}
						if (cacheItem.Disabled)
						{
							cacheItem = this.timedItems.Dequeue();
						}
						else
						{
							cacheItem = this.timedItems.Dequeue();
							if (cacheItem != null && !this.NeedsUpdate(cacheItem, CacheItemUpdateReason.Expired, false))
							{
								this.Remove(cacheItem.Key, CacheItemRemovedReason.Expired, false, true);
							}
						}
					}
				}
			}
			finally
			{
				this.cacheLock.ExitWriteLock();
			}
			if (cacheItem != null)
			{
				long num = Math.Max(0L, (long)(cacheItem.AbsoluteExpiration - now).TotalMilliseconds);
				if (num > 0L && (this.expirationTimerPeriod == 0L || this.expirationTimerPeriod > num))
				{
					this.expirationTimerPeriod = num;
					this.expirationTimer.Change(this.expirationTimerPeriod, this.expirationTimerPeriod);
					return;
				}
				if (this.expirationTimerPeriod > 0L)
				{
					return;
				}
			}
			this.expirationTimer.Change(-1, -1);
			this.expirationTimerPeriod = 0L;
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x000C503C File Offset: 0x000C323C
		internal void CheckDependencies()
		{
			try
			{
				this.cacheLock.EnterWriteLock();
				List<CacheItem> list = this.cache.SelectItems((CacheItem it) => it != null && (it.Dependency != null && it.Dependency.HasChanged && !this.NeedsUpdate(it, CacheItemUpdateReason.DependencyChanged, false)));
				foreach (CacheItem cacheItem in list)
				{
					this.Remove(cacheItem.Key, CacheItemRemovedReason.DependencyChanged, false, true);
				}
				list.Clear();
				list.TrimExcess();
			}
			finally
			{
				this.cacheLock.ExitWriteLock();
			}
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x000C50DC File Offset: 0x000C32DC
		internal DateTime GetKeyLastChange(string key)
		{
			DateTime dateTime;
			try
			{
				this.cacheLock.EnterWriteLock();
				CacheItem cacheItem = this.cache[key];
				if (cacheItem == null)
				{
					dateTime = DateTime.MaxValue;
				}
				else
				{
					dateTime = cacheItem.LastChange;
				}
			}
			finally
			{
				this.cacheLock.ExitWriteLock();
			}
			return dateTime;
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x060046CA RID: 18122 RVA: 0x000C5134 File Offset: 0x000C3334
		// (set) Token: 0x060046CB RID: 18123 RVA: 0x000C5146 File Offset: 0x000C3346
		internal Cache DependencyCache
		{
			get
			{
				if (this.dependencyCache == null)
				{
					return this;
				}
				return this.dependencyCache;
			}
			set
			{
				this.dependencyCache = value;
			}
		}

		// Token: 0x0400254D RID: 9549
		private const int LOW_WATER_MARK = 10000;

		// Token: 0x0400254E RID: 9550
		private const int HIGH_WATER_MARK = 15000;

		/// <summary>Used in the <paramref name="absoluteExpiration" /> parameter in an <see cref="M:System.Web.Caching.Cache.Insert(System.String,System.Object)" /> method call to indicate the item should never expire. This field is read-only.</summary>
		// Token: 0x0400254F RID: 9551
		public static readonly DateTime NoAbsoluteExpiration = DateTime.MaxValue;

		/// <summary>Used as the <paramref name="slidingExpiration" /> parameter in an <see cref="M:System.Web.Caching.Cache.Insert(System.String,System.Object)" /> or <see cref="M:System.Web.Caching.Cache.Add(System.String,System.Object,System.Web.Caching.CacheDependency,System.DateTime,System.TimeSpan,System.Web.Caching.CacheItemPriority,System.Web.Caching.CacheItemRemovedCallback)" /> method call to disable sliding expirations. This field is read-only.</summary>
		// Token: 0x04002550 RID: 9552
		public static readonly TimeSpan NoSlidingExpiration = TimeSpan.Zero;

		// Token: 0x04002551 RID: 9553
		private ReaderWriterLockSlim cacheLock;

		// Token: 0x04002552 RID: 9554
		private CacheItemLRU cache;

		// Token: 0x04002553 RID: 9555
		private CacheItemPriorityQueue timedItems;

		// Token: 0x04002554 RID: 9556
		private Timer expirationTimer;

		// Token: 0x04002555 RID: 9557
		private long expirationTimerPeriod;

		// Token: 0x04002556 RID: 9558
		private Cache dependencyCache;

		// Token: 0x04002557 RID: 9559
		private bool? disableExpiration;

		// Token: 0x04002558 RID: 9560
		private long privateBytesLimit = -1L;

		// Token: 0x04002559 RID: 9561
		private long percentagePhysicalMemoryLimit = -1L;
	}
}
