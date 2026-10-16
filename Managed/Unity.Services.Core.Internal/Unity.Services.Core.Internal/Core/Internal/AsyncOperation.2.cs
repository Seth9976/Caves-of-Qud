using System;
using System.Collections;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000029 RID: 41
	internal class AsyncOperation<T> : IAsyncOperation<T>, IEnumerator
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002422 File Offset: 0x00000622
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000242A File Offset: 0x0000062A
		public bool IsDone { get; protected set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002433 File Offset: 0x00000633
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000243B File Offset: 0x0000063B
		public AsyncOperationStatus Status { get; protected set; }

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000081 RID: 129 RVA: 0x00002444 File Offset: 0x00000644
		// (remove) Token: 0x06000082 RID: 130 RVA: 0x0000246D File Offset: 0x0000066D
		public event Action<IAsyncOperation<T>> Completed
		{
			add
			{
				if (this.IsDone)
				{
					value(this);
					return;
				}
				this.m_CompletedCallback = (Action<IAsyncOperation<T>>)Delegate.Combine(this.m_CompletedCallback, value);
			}
			remove
			{
				this.m_CompletedCallback = (Action<IAsyncOperation<T>>)Delegate.Remove(this.m_CompletedCallback, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002486 File Offset: 0x00000686
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000248E File Offset: 0x0000068E
		public Exception Exception { get; protected set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002497 File Offset: 0x00000697
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000249F File Offset: 0x0000069F
		public T Result { get; protected set; }

		// Token: 0x06000087 RID: 135 RVA: 0x000024A8 File Offset: 0x000006A8
		public void SetInProgress()
		{
			this.Status = AsyncOperationStatus.InProgress;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000024B1 File Offset: 0x000006B1
		public void Succeed(T result)
		{
			if (this.IsDone)
			{
				return;
			}
			this.Result = result;
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Succeeded;
			Action<IAsyncOperation<T>> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000024EA File Offset: 0x000006EA
		public void Fail(Exception reason)
		{
			if (this.IsDone)
			{
				return;
			}
			this.Exception = reason;
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Failed;
			Action<IAsyncOperation<T>> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002523 File Offset: 0x00000723
		public void Cancel()
		{
			if (this.IsDone)
			{
				return;
			}
			this.Exception = new OperationCanceledException();
			this.IsDone = true;
			this.Status = AsyncOperationStatus.Cancelled;
			Action<IAsyncOperation<T>> completedCallback = this.m_CompletedCallback;
			if (completedCallback != null)
			{
				completedCallback(this);
			}
			this.m_CompletedCallback = null;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002560 File Offset: 0x00000760
		bool IEnumerator.MoveNext()
		{
			return !this.IsDone;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000256B File Offset: 0x0000076B
		void IEnumerator.Reset()
		{
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000256D File Offset: 0x0000076D
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000024 RID: 36
		protected Action<IAsyncOperation<T>> m_CompletedCallback;
	}
}
