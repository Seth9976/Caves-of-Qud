using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200003D RID: 61
	public struct SwitchToMainThreadAwaitable
	{
		// Token: 0x06000178 RID: 376 RVA: 0x0000674B File Offset: 0x0000494B
		public SwitchToMainThreadAwaitable(PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken)
		{
			this.playerLoopTiming = playerLoopTiming;
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000675B File Offset: 0x0000495B
		public SwitchToMainThreadAwaitable.Awaiter GetAwaiter()
		{
			return new SwitchToMainThreadAwaitable.Awaiter(this.playerLoopTiming, this.cancellationToken);
		}

		// Token: 0x0400008A RID: 138
		private readonly PlayerLoopTiming playerLoopTiming;

		// Token: 0x0400008B RID: 139
		private readonly CancellationToken cancellationToken;

		// Token: 0x020001BD RID: 445
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000A53 RID: 2643 RVA: 0x00024C24 File Offset: 0x00022E24
			public Awaiter(PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken)
			{
				this.playerLoopTiming = playerLoopTiming;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00024C34 File Offset: 0x00022E34
			public bool IsCompleted
			{
				get
				{
					int managedThreadId = Thread.CurrentThread.ManagedThreadId;
					return PlayerLoopHelper.MainThreadId == managedThreadId;
				}
			}

			// Token: 0x06000A55 RID: 2645 RVA: 0x00024C58 File Offset: 0x00022E58
			public void GetResult()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
			}

			// Token: 0x06000A56 RID: 2646 RVA: 0x00024C73 File Offset: 0x00022E73
			public void OnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.playerLoopTiming, continuation);
			}

			// Token: 0x06000A57 RID: 2647 RVA: 0x00024C81 File Offset: 0x00022E81
			public void UnsafeOnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.playerLoopTiming, continuation);
			}

			// Token: 0x0400039A RID: 922
			private readonly PlayerLoopTiming playerLoopTiming;

			// Token: 0x0400039B RID: 923
			private readonly CancellationToken cancellationToken;
		}
	}
}
