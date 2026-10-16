using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web
{
	// Token: 0x02000057 RID: 87
	internal sealed class TaskWrapperAsyncResult : IAsyncResult
	{
		// Token: 0x060003DC RID: 988 RVA: 0x0000730D File Offset: 0x0000550D
		internal TaskWrapperAsyncResult(Task task, object asyncState)
		{
			this.Task = task;
			this.AsyncState = asyncState;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00007323 File Offset: 0x00005523
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000732B File Offset: 0x0000552B
		public object AsyncState { get; private set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00007334 File Offset: 0x00005534
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return ((IAsyncResult)this.Task).AsyncWaitHandle;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00007341 File Offset: 0x00005541
		public bool CompletedSynchronously
		{
			get
			{
				return this._forceCompletedSynchronously || ((IAsyncResult)this.Task).CompletedSynchronously;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00007358 File Offset: 0x00005558
		public bool IsCompleted
		{
			get
			{
				return ((IAsyncResult)this.Task).IsCompleted;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00007365 File Offset: 0x00005565
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000736D File Offset: 0x0000556D
		internal Task Task { get; private set; }

		// Token: 0x060003E4 RID: 996 RVA: 0x00007376 File Offset: 0x00005576
		internal void ForceCompletedSynchronously()
		{
			this._forceCompletedSynchronously = true;
		}

		// Token: 0x04000E2B RID: 3627
		private bool _forceCompletedSynchronously;
	}
}
