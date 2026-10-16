using System;
using System.Web.Caching;

namespace System.Web.SessionState
{
	// Token: 0x02000495 RID: 1173
	internal class RemoteStateServer : MarshalByRefObject
	{
		// Token: 0x0600353E RID: 13630 RVA: 0x00088F56 File Offset: 0x00087156
		internal RemoteStateServer()
		{
			this.cache = new Cache();
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x00088F69 File Offset: 0x00087169
		private void Insert(string id, LockableStateServerItem item)
		{
			this.cache.Insert(id, item, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, item.item.Timeout, 0));
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x00088F90 File Offset: 0x00087190
		private LockableStateServerItem Retrieve(string id)
		{
			return this.cache[id] as LockableStateServerItem;
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x00088FA4 File Offset: 0x000871A4
		internal void CreateUninitializedItem(string id, int timeout)
		{
			LockableStateServerItem lockableStateServerItem = new LockableStateServerItem(new StateServerItem(timeout)
			{
				Action = SessionStateActions.InitializeItem
			});
			this.Insert(id, lockableStateServerItem);
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x00088FCC File Offset: 0x000871CC
		internal StateServerItem GetItem(string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions, bool exclusive)
		{
			locked = false;
			lockAge = TimeSpan.MinValue;
			lockId = int.MinValue;
			actions = SessionStateActions.None;
			LockableStateServerItem lockableStateServerItem = this.Retrieve(id);
			if (lockableStateServerItem == null || lockableStateServerItem.item.IsAbandoned())
			{
				return null;
			}
			try
			{
				lockableStateServerItem.rwlock.AcquireReaderLock(30000);
				if (lockableStateServerItem.item.Locked)
				{
					locked = true;
					lockAge = DateTime.UtcNow.Subtract(lockableStateServerItem.item.LockedTime);
					lockId = lockableStateServerItem.item.LockId;
					return null;
				}
				lockableStateServerItem.rwlock.ReleaseReaderLock();
				if (exclusive)
				{
					lockableStateServerItem.rwlock.AcquireWriterLock(30000);
					lockableStateServerItem.item.Locked = true;
					lockableStateServerItem.item.LockedTime = DateTime.UtcNow;
					lockableStateServerItem.item.LockId++;
					lockId = lockableStateServerItem.item.LockId;
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (lockableStateServerItem.rwlock.IsReaderLockHeld)
				{
					lockableStateServerItem.rwlock.ReleaseReaderLock();
				}
				if (lockableStateServerItem.rwlock.IsWriterLockHeld)
				{
					lockableStateServerItem.rwlock.ReleaseWriterLock();
				}
			}
			actions = lockableStateServerItem.item.Action;
			return lockableStateServerItem.item;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x00089134 File Offset: 0x00087334
		internal void Remove(string id, object lockid)
		{
			this.cache.Remove(id);
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x00089144 File Offset: 0x00087344
		internal void ResetItemTimeout(string id)
		{
			LockableStateServerItem lockableStateServerItem = this.Retrieve(id);
			if (lockableStateServerItem == null)
			{
				return;
			}
			lockableStateServerItem.item.Touch();
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x00089168 File Offset: 0x00087368
		internal void ReleaseItemExclusive(string id, object lockId)
		{
			LockableStateServerItem lockableStateServerItem = this.Retrieve(id);
			if (lockableStateServerItem == null || lockableStateServerItem.item.LockId != (int)lockId)
			{
				return;
			}
			try
			{
				lockableStateServerItem.rwlock.AcquireWriterLock(30000);
				lockableStateServerItem.item.Locked = false;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (lockableStateServerItem.rwlock.IsWriterLockHeld)
				{
					lockableStateServerItem.rwlock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000891EC File Offset: 0x000873EC
		internal void SetAndReleaseItemExclusive(string id, byte[] collection_data, byte[] sobjs_data, object lockId, int timeout, bool newItem)
		{
			LockableStateServerItem lockableStateServerItem = this.Retrieve(id);
			bool flag = false;
			if (newItem || lockableStateServerItem == null)
			{
				lockableStateServerItem = new LockableStateServerItem(new StateServerItem(collection_data, sobjs_data, timeout));
				lockableStateServerItem.item.LockId = (int)lockId;
				flag = true;
			}
			else
			{
				if (lockableStateServerItem.item.LockId != (int)lockId)
				{
					return;
				}
				this.Remove(id, lockId);
			}
			try
			{
				lockableStateServerItem.rwlock.AcquireWriterLock(30000);
				lockableStateServerItem.item.Locked = false;
				if (!flag)
				{
					lockableStateServerItem.item.CollectionData = collection_data;
					lockableStateServerItem.item.StaticObjectsData = sobjs_data;
				}
				this.Insert(id, lockableStateServerItem);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (lockableStateServerItem.rwlock.IsWriterLockHeld)
				{
					lockableStateServerItem.rwlock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x04001D4F RID: 7503
		private const int lockAcquireTimeout = 30000;

		// Token: 0x04001D50 RID: 7504
		private Cache cache;
	}
}
