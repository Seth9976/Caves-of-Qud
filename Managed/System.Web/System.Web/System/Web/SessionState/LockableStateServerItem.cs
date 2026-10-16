using System;
using System.Threading;

namespace System.Web.SessionState
{
	// Token: 0x02000494 RID: 1172
	internal class LockableStateServerItem
	{
		// Token: 0x0600353D RID: 13629 RVA: 0x00088F3C File Offset: 0x0008713C
		public LockableStateServerItem(StateServerItem item)
		{
			this.item = item;
			this.rwlock = new ReaderWriterLock();
		}

		// Token: 0x04001D4D RID: 7501
		public StateServerItem item;

		// Token: 0x04001D4E RID: 7502
		public ReaderWriterLock rwlock;
	}
}
