using System;
using System.Collections.Specialized;
using System.Threading;
using System.Web.Caching;

namespace System.Web.SessionState
{
	// Token: 0x02000499 RID: 1177
	internal class SessionInProcHandler : SessionStateStoreProviderBase
	{
		// Token: 0x06003559 RID: 13657 RVA: 0x000895B0 File Offset: 0x000877B0
		public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
		{
			return new SessionStateStoreData(new SessionStateItemCollection(), this.staticObjects, timeout);
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000895C3 File Offset: 0x000877C3
		private void InsertSessionItem(InProcSessionItem item, int timeout, string id)
		{
			if (item == null || string.IsNullOrEmpty(id))
			{
				return;
			}
			HttpRuntime.InternalCache.Insert(id, item, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes((double)timeout), CacheItemPriority.AboveNormal, this.removedCB);
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000895F1 File Offset: 0x000877F1
		private void UpdateSessionItemTimeout(int timeout, string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return;
			}
			HttpRuntime.InternalCache.SetItemTimeout(id, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes((double)timeout), true);
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x00089614 File Offset: 0x00087814
		public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
		{
			this.EnsureGoodId(id, true);
			this.InsertSessionItem(new InProcSessionItem
			{
				expiresAt = DateTime.UtcNow.AddMinutes((double)timeout),
				timeout = timeout
			}, timeout, "@@@InProc@" + id);
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x00003946 File Offset: 0x00001B46
		public override void Dispose()
		{
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x0008965E File Offset: 0x0008785E
		public override void EndRequest(HttpContext context)
		{
			if (this.staticObjects != null)
			{
				this.staticObjects.GetObjects().Clear();
				this.staticObjects = null;
			}
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x00089680 File Offset: 0x00087880
		private SessionStateStoreData GetItemInternal(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions, bool exclusive)
		{
			locked = false;
			lockAge = TimeSpan.MinValue;
			lockId = int.MinValue;
			actions = SessionStateActions.None;
			if (id == null)
			{
				return null;
			}
			Cache internalCache = HttpRuntime.InternalCache;
			string text = "@@@InProc@" + id;
			InProcSessionItem inProcSessionItem = internalCache[text] as InProcSessionItem;
			if (inProcSessionItem == null)
			{
				return null;
			}
			bool flag = false;
			bool flag2 = false;
			SessionStateStoreData sessionStateStoreData;
			try
			{
				if (!inProcSessionItem.rwlock.TryEnterUpgradeableReadLock(30000))
				{
					throw new ApplicationException("Failed to acquire lock");
				}
				flag = true;
				if (inProcSessionItem.locked)
				{
					locked = true;
					lockAge = DateTime.UtcNow.Subtract(inProcSessionItem.lockedTime);
					lockId = inProcSessionItem.lockId;
					sessionStateStoreData = null;
				}
				else
				{
					if (exclusive)
					{
						if (!inProcSessionItem.rwlock.TryEnterWriteLock(30000))
						{
							throw new ApplicationException("Failed to acquire lock");
						}
						flag2 = true;
						inProcSessionItem.locked = true;
						inProcSessionItem.lockedTime = DateTime.UtcNow;
						inProcSessionItem.lockId++;
						lockId = inProcSessionItem.lockId;
					}
					if (inProcSessionItem.items == null)
					{
						actions = SessionStateActions.InitializeItem;
						inProcSessionItem.items = new SessionStateItemCollection();
					}
					if (inProcSessionItem.staticItems == null)
					{
						inProcSessionItem.staticItems = this.staticObjects;
					}
					sessionStateStoreData = new SessionStateStoreData(inProcSessionItem.items, inProcSessionItem.staticItems, inProcSessionItem.timeout);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag2)
				{
					inProcSessionItem.rwlock.ExitWriteLock();
				}
				if (flag)
				{
					inProcSessionItem.rwlock.ExitUpgradeableReadLock();
				}
			}
			return sessionStateStoreData;
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x0008980C File Offset: 0x00087A0C
		public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
		{
			this.EnsureGoodId(id, false);
			return this.GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, false);
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x00089826 File Offset: 0x00087A26
		public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
		{
			this.EnsureGoodId(id, false);
			return this.GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, true);
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x00089840 File Offset: 0x00087A40
		public override void Initialize(string name, NameValueCollection config)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "Session InProc handler";
			}
			this.removedCB = new CacheItemRemovedCallback(this.OnSessionRemoved);
			base.Initialize(name, config);
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x0008986B File Offset: 0x00087A6B
		public override void InitializeRequest(HttpContext context)
		{
			this.staticObjects = HttpApplicationFactory.ApplicationState.SessionObjects.Clone();
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x00089884 File Offset: 0x00087A84
		public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
		{
			this.EnsureGoodId(id, true);
			string text = "@@@InProc@" + id;
			InProcSessionItem inProcSessionItem = HttpRuntime.InternalCache[text] as InProcSessionItem;
			if (inProcSessionItem == null || lockId == null || lockId.GetType() != typeof(int) || inProcSessionItem.lockId != (int)lockId)
			{
				return;
			}
			bool flag = false;
			ReaderWriterLockSlim readerWriterLockSlim = null;
			try
			{
				readerWriterLockSlim = inProcSessionItem.rwlock;
				if (readerWriterLockSlim == null || !readerWriterLockSlim.TryEnterWriteLock(30000))
				{
					throw new ApplicationException("Failed to acquire lock");
				}
				flag = true;
				inProcSessionItem.locked = false;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag && readerWriterLockSlim != null)
				{
					readerWriterLockSlim.ExitWriteLock();
				}
			}
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x00089944 File Offset: 0x00087B44
		public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
		{
			this.EnsureGoodId(id, true);
			string text = "@@@InProc@" + id;
			Cache internalCache = HttpRuntime.InternalCache;
			InProcSessionItem inProcSessionItem = internalCache[text] as InProcSessionItem;
			if (inProcSessionItem == null || lockId == null || lockId.GetType() != typeof(int) || inProcSessionItem.lockId != (int)lockId)
			{
				return;
			}
			bool flag = false;
			ReaderWriterLockSlim readerWriterLockSlim = null;
			try
			{
				readerWriterLockSlim = inProcSessionItem.rwlock;
				if (readerWriterLockSlim == null || !readerWriterLockSlim.TryEnterWriteLock(30000))
				{
					throw new ApplicationException("Failed to acquire lock after");
				}
				flag = true;
				internalCache.Remove(text);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag)
				{
					readerWriterLockSlim.ExitWriteLock();
				}
			}
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x00089A08 File Offset: 0x00087C08
		public override void ResetItemTimeout(HttpContext context, string id)
		{
			this.EnsureGoodId(id, true);
			string text = "@@@InProc@" + id;
			InProcSessionItem inProcSessionItem = HttpRuntime.InternalCache[text] as InProcSessionItem;
			if (inProcSessionItem == null)
			{
				return;
			}
			bool flag = false;
			ReaderWriterLockSlim readerWriterLockSlim = null;
			try
			{
				readerWriterLockSlim = inProcSessionItem.rwlock;
				if (readerWriterLockSlim == null || !readerWriterLockSlim.TryEnterWriteLock(30000))
				{
					throw new ApplicationException("Failed to acquire lock after");
				}
				flag = true;
				inProcSessionItem.resettingTimeout = true;
				this.UpdateSessionItemTimeout(inProcSessionItem.timeout, text);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag && readerWriterLockSlim != null)
				{
					readerWriterLockSlim.ExitWriteLock();
				}
			}
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x00089AAC File Offset: 0x00087CAC
		public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
		{
			this.EnsureGoodId(id, true);
			string text = "@@@InProc@" + id;
			Cache internalCache = HttpRuntime.InternalCache;
			InProcSessionItem inProcSessionItem = internalCache[text] as InProcSessionItem;
			ISessionStateItemCollection sessionStateItemCollection = null;
			int num = 20;
			HttpStaticObjectsCollection httpStaticObjectsCollection = null;
			if (item != null)
			{
				sessionStateItemCollection = item.Items;
				num = item.Timeout;
				httpStaticObjectsCollection = item.StaticObjects;
			}
			if (newItem || inProcSessionItem == null)
			{
				inProcSessionItem = new InProcSessionItem();
				inProcSessionItem.timeout = num;
				inProcSessionItem.expiresAt = DateTime.UtcNow.AddMinutes((double)num);
				if (lockId.GetType() == typeof(int))
				{
					inProcSessionItem.lockId = (int)lockId;
				}
			}
			else
			{
				if (lockId == null || lockId.GetType() != typeof(int) || inProcSessionItem.lockId != (int)lockId)
				{
					return;
				}
				inProcSessionItem.resettingTimeout = true;
				internalCache.Remove(text);
			}
			bool flag = false;
			ReaderWriterLockSlim readerWriterLockSlim = null;
			try
			{
				readerWriterLockSlim = inProcSessionItem.rwlock;
				if (readerWriterLockSlim != null && readerWriterLockSlim.TryEnterWriteLock(30000))
				{
					flag = true;
				}
				else if (readerWriterLockSlim != null)
				{
					throw new ApplicationException("Failed to acquire lock");
				}
				if (inProcSessionItem.resettingTimeout)
				{
					this.UpdateSessionItemTimeout(num, text);
				}
				else
				{
					inProcSessionItem.locked = false;
					inProcSessionItem.items = sessionStateItemCollection;
					inProcSessionItem.staticItems = httpStaticObjectsCollection;
					this.InsertSessionItem(inProcSessionItem, num, text);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag && readerWriterLockSlim != null)
				{
					readerWriterLockSlim.ExitWriteLock();
				}
			}
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x00089C2C File Offset: 0x00087E2C
		public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
		{
			this.expireCallback = expireCallback;
			return true;
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x00089C36 File Offset: 0x00087E36
		private void EnsureGoodId(string id, bool throwOnNull)
		{
			if (id == null)
			{
				if (throwOnNull)
				{
					throw new HttpException("Session ID is invalid");
				}
				return;
			}
			else
			{
				if (id.Length > SessionIDManager.SessionIDMaxLength)
				{
					throw new HttpException("Session ID too long");
				}
				return;
			}
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x00089C64 File Offset: 0x00087E64
		private void OnSessionRemoved(string key, object value, CacheItemRemovedReason reason)
		{
			if (this.expireCallback == null)
			{
				if (value is InProcSessionItem)
				{
					InProcSessionItem inProcSessionItem = (InProcSessionItem)value;
					if (inProcSessionItem.resettingTimeout)
					{
						inProcSessionItem.resettingTimeout = false;
						return;
					}
					inProcSessionItem.Dispose();
				}
				return;
			}
			if (key.StartsWith("@@@InProc@", StringComparison.OrdinalIgnoreCase))
			{
				key = key.Substring(10);
			}
			if (value is SessionStateStoreData)
			{
				this.expireCallback(key, (SessionStateStoreData)value);
				return;
			}
			if (!(value is InProcSessionItem))
			{
				this.expireCallback(key, null);
				return;
			}
			InProcSessionItem inProcSessionItem2 = (InProcSessionItem)value;
			if (inProcSessionItem2.resettingTimeout)
			{
				inProcSessionItem2.resettingTimeout = false;
				return;
			}
			this.expireCallback(key, new SessionStateStoreData(inProcSessionItem2.items, inProcSessionItem2.staticItems, inProcSessionItem2.timeout));
			inProcSessionItem2.Dispose();
		}

		// Token: 0x04001D5F RID: 7519
		private const string CachePrefix = "@@@InProc@";

		// Token: 0x04001D60 RID: 7520
		private const int CachePrefixLength = 10;

		// Token: 0x04001D61 RID: 7521
		private const int lockAcquireTimeout = 30000;

		// Token: 0x04001D62 RID: 7522
		private CacheItemRemovedCallback removedCB;

		// Token: 0x04001D63 RID: 7523
		private SessionStateItemExpireCallback expireCallback;

		// Token: 0x04001D64 RID: 7524
		private HttpStaticObjectsCollection staticObjects;
	}
}
