using System;

namespace System.Web.SessionState
{
	// Token: 0x020004A5 RID: 1189
	[Serializable]
	internal class StateServerItem
	{
		// Token: 0x060035DD RID: 13789 RVA: 0x0008BB3F File Offset: 0x00089D3F
		public StateServerItem(int timeout)
			: this(null, null, timeout)
		{
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x0008BB4C File Offset: 0x00089D4C
		public StateServerItem(byte[] collection_data, byte[] sobjs_data, int timeout)
		{
			this.CollectionData = collection_data;
			this.StaticObjectsData = sobjs_data;
			this.Timeout = timeout;
			this.last_access = DateTime.UtcNow;
			this.Locked = false;
			this.LockId = int.MinValue;
			this.LockedTime = DateTime.MinValue;
			this.Action = SessionStateActions.None;
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x0008BBA3 File Offset: 0x00089DA3
		public void Touch()
		{
			this.last_access = DateTime.UtcNow;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x0008BBB0 File Offset: 0x00089DB0
		public bool IsAbandoned()
		{
			return this.last_access.AddMinutes((double)this.Timeout) < DateTime.UtcNow;
		}

		// Token: 0x04001D8E RID: 7566
		public byte[] CollectionData;

		// Token: 0x04001D8F RID: 7567
		public byte[] StaticObjectsData;

		// Token: 0x04001D90 RID: 7568
		private DateTime last_access;

		// Token: 0x04001D91 RID: 7569
		public int Timeout;

		// Token: 0x04001D92 RID: 7570
		public int LockId;

		// Token: 0x04001D93 RID: 7571
		public bool Locked;

		// Token: 0x04001D94 RID: 7572
		public DateTime LockedTime;

		// Token: 0x04001D95 RID: 7573
		public SessionStateActions Action;
	}
}
