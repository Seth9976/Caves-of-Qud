using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200003E RID: 62
	public struct ReturnToMainThread
	{
		// Token: 0x0600017A RID: 378 RVA: 0x0000676E File Offset: 0x0000496E
		public ReturnToMainThread(PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken)
		{
			this.playerLoopTiming = playerLoopTiming;
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000677E File Offset: 0x0000497E
		public ReturnToMainThread.Awaiter DisposeAsync()
		{
			return new ReturnToMainThread.Awaiter(this.playerLoopTiming, this.cancellationToken);
		}

		// Token: 0x0400008C RID: 140
		private readonly PlayerLoopTiming playerLoopTiming;

		// Token: 0x0400008D RID: 141
		private readonly CancellationToken cancellationToken;

		// Token: 0x020001BE RID: 446
		public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000A58 RID: 2648 RVA: 0x00024C8F File Offset: 0x00022E8F
			public Awaiter(PlayerLoopTiming timing, CancellationToken cancellationToken)
			{
				this.timing = timing;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x06000A59 RID: 2649 RVA: 0x00024C9F File Offset: 0x00022E9F
			public ReturnToMainThread.Awaiter GetAwaiter()
			{
				return this;
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00024CA7 File Offset: 0x00022EA7
			public bool IsCompleted
			{
				get
				{
					return PlayerLoopHelper.MainThreadId == Thread.CurrentThread.ManagedThreadId;
				}
			}

			// Token: 0x06000A5B RID: 2651 RVA: 0x00024CBC File Offset: 0x00022EBC
			public void GetResult()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
			}

			// Token: 0x06000A5C RID: 2652 RVA: 0x00024CD7 File Offset: 0x00022ED7
			public void OnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.timing, continuation);
			}

			// Token: 0x06000A5D RID: 2653 RVA: 0x00024CE5 File Offset: 0x00022EE5
			public void UnsafeOnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.timing, continuation);
			}

			// Token: 0x0400039C RID: 924
			private readonly PlayerLoopTiming timing;

			// Token: 0x0400039D RID: 925
			private readonly CancellationToken cancellationToken;
		}
	}
}
