using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000174 RID: 372
	[StructLayout(LayoutKind.Sequential)]
	internal class IOSelectorJob : IThreadPoolWorkItem
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x0002B66C File Offset: 0x0002986C
		public IOSelectorJob(IOOperation operation, IOAsyncCallback callback, IOAsyncResult state)
		{
			this.operation = operation;
			this.callback = callback;
			this.state = state;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002B689 File Offset: 0x00029889
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.callback(this.state);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00003917 File Offset: 0x00001B17
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002B69C File Offset: 0x0002989C
		public void MarkDisposed()
		{
			this.state.CompleteDisposed();
		}

		// Token: 0x040006CB RID: 1739
		private IOOperation operation;

		// Token: 0x040006CC RID: 1740
		private IOAsyncCallback callback;

		// Token: 0x040006CD RID: 1741
		private IOAsyncResult state;
	}
}
