using System;
using System.Threading;

namespace System.Web.SessionState
{
	// Token: 0x02000498 RID: 1176
	internal sealed class InProcSessionItem
	{
		// Token: 0x06003556 RID: 13654 RVA: 0x000894DC File Offset: 0x000876DC
		internal InProcSessionItem()
		{
			this.locked = false;
			this.cookieless = false;
			this.items = null;
			this.staticItems = null;
			this.lockedTime = DateTime.MinValue;
			this.expiresAt = DateTime.MinValue;
			this.rwlock = new ReaderWriterLockSlim();
			this.lockId = int.MinValue;
			this.timeout = 0;
			this.resettingTimeout = false;
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x00089545 File Offset: 0x00087745
		public void Dispose()
		{
			if (this.rwlock != null)
			{
				this.rwlock.Dispose();
				this.rwlock = null;
			}
			this.staticItems = null;
			if (this.items != null)
			{
				this.items.Clear();
			}
			this.items = null;
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x00089584 File Offset: 0x00087784
		~InProcSessionItem()
		{
			this.Dispose();
		}

		// Token: 0x04001D55 RID: 7509
		public bool locked;

		// Token: 0x04001D56 RID: 7510
		public bool cookieless;

		// Token: 0x04001D57 RID: 7511
		public ISessionStateItemCollection items;

		// Token: 0x04001D58 RID: 7512
		public DateTime lockedTime;

		// Token: 0x04001D59 RID: 7513
		public DateTime expiresAt;

		// Token: 0x04001D5A RID: 7514
		public ReaderWriterLockSlim rwlock;

		// Token: 0x04001D5B RID: 7515
		public int lockId;

		// Token: 0x04001D5C RID: 7516
		public int timeout;

		// Token: 0x04001D5D RID: 7517
		public bool resettingTimeout;

		// Token: 0x04001D5E RID: 7518
		public HttpStaticObjectsCollection staticItems;
	}
}
