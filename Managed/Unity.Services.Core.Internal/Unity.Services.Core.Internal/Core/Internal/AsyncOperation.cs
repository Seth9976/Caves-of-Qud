using System;
using System.Collections;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000028 RID: 40
	internal class AsyncOperation : IAsyncOperation, IEnumerator
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000022E4 File Offset: 0x000004E4
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000022EC File Offset: 0x000004EC
		public bool IsDone { get; protected set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000022F5 File Offset: 0x000004F5
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000022FD File Offset: 0x000004FD
		public AsyncOperationStatus Status { get; protected set; }

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000071 RID: 113 RVA: 0x00002306 File Offset: 0x00000506
		// (remove) Token: 0x06000072 RID: 114 RVA: 0x0000232F File Offset: 0x0000052F
		public event Action<IAsyncOperation> Completed
		{
			add
			{
				if (this.IsDone)
				{
					value(this);
					return;
				}
				this.m_CompletedCallback = (Action<IAsyncOperation>)Delegate.Combine(this.m_CompletedCallback, value);
			}
			remove
			{
				this.m_CompletedCallback = (Action<IAsyncOperation>)Delegate.Remove(this.m_CompletedCallback, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002348 File Offset: 0x00000548
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002350 File Offset: 0x00000550
		public Exception Exception { get; protected set; }

		// Token: 0x06000075 RID: 117 RVA: 0x00002359 File Offset: 0x00000559
		public void SetInProgress()
		{
			this.Status = AsyncOperationStatus.InProgress;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002362 File Offset: 0x00000562
		public void Succeed()
		{
			if (this.IsDone)
			{
				return;
			}
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Succeeded;
			Action<IAsyncOperation> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002394 File Offset: 0x00000594
		public void Fail(Exception reason)
		{
			if (this.IsDone)
			{
				return;
			}
			this.Exception = reason;
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Failed;
			Action<IAsyncOperation> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000023CD File Offset: 0x000005CD
		public void Cancel()
		{
			if (this.IsDone)
			{
				return;
			}
			this.Exception = new OperationCanceledException();
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Cancelled;
			Action<IAsyncOperation> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000240A File Offset: 0x0000060A
		bool IEnumerator.MoveNext()
		{
			return !this.IsDone;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002415 File Offset: 0x00000615
		void IEnumerator.Reset()
		{
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002417 File Offset: 0x00000617
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400001F RID: 31
		protected Action<IAsyncOperation> m_CompletedCallback;
	}
}
