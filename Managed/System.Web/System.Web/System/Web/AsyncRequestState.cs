using System;
using System.Threading;

namespace System.Web
{
	// Token: 0x0200007B RID: 123
	internal class AsyncRequestState : IAsyncResult
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0000C9B1 File Offset: 0x0000ABB1
		internal AsyncRequestState(ManualResetEvent complete_event, AsyncCallback cb, object cb_data)
		{
			this.cb = cb;
			this.cb_data = cb_data;
			this.complete_event = complete_event;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		internal void Complete()
		{
			this.completed = true;
			try
			{
				if (this.cb != null)
				{
					this.cb(this);
				}
			}
			catch
			{
			}
			this.complete_event.Set();
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public object AsyncState
		{
			get
			{
				return this.cb_data;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000CA24 File Offset: 0x0000AC24
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000CA2C File Offset: 0x0000AC2C
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.complete_event;
			}
		}

		// Token: 0x04000ED9 RID: 3801
		private AsyncCallback cb;

		// Token: 0x04000EDA RID: 3802
		private object cb_data;

		// Token: 0x04000EDB RID: 3803
		private bool completed;

		// Token: 0x04000EDC RID: 3804
		private ManualResetEvent complete_event;
	}
}
