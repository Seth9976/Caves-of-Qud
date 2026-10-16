using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000041 RID: 65
	public struct SwitchToSynchronizationContextAwaitable
	{
		// Token: 0x0600017E RID: 382 RVA: 0x000067C2 File Offset: 0x000049C2
		public SwitchToSynchronizationContextAwaitable(SynchronizationContext synchronizationContext, CancellationToken cancellationToken)
		{
			this.synchronizationContext = synchronizationContext;
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000067D2 File Offset: 0x000049D2
		public SwitchToSynchronizationContextAwaitable.Awaiter GetAwaiter()
		{
			return new SwitchToSynchronizationContextAwaitable.Awaiter(this.synchronizationContext, this.cancellationToken);
		}

		// Token: 0x0400008E RID: 142
		private readonly SynchronizationContext synchronizationContext;

		// Token: 0x0400008F RID: 143
		private readonly CancellationToken cancellationToken;

		// Token: 0x020001C1 RID: 449
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000A6A RID: 2666 RVA: 0x00024D95 File Offset: 0x00022F95
			public Awaiter(SynchronizationContext synchronizationContext, CancellationToken cancellationToken)
			{
				this.synchronizationContext = synchronizationContext;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00024DA5 File Offset: 0x00022FA5
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000A6C RID: 2668 RVA: 0x00024DA8 File Offset: 0x00022FA8
			public void GetResult()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
			}

			// Token: 0x06000A6D RID: 2669 RVA: 0x00024DC3 File Offset: 0x00022FC3
			public void OnCompleted(Action continuation)
			{
				this.synchronizationContext.Post(SwitchToSynchronizationContextAwaitable.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x06000A6E RID: 2670 RVA: 0x00024DD6 File Offset: 0x00022FD6
			public void UnsafeOnCompleted(Action continuation)
			{
				this.synchronizationContext.Post(SwitchToSynchronizationContextAwaitable.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x06000A6F RID: 2671 RVA: 0x00024DE9 File Offset: 0x00022FE9
			private static void Callback(object state)
			{
				((Action)state)();
			}

			// Token: 0x040003A0 RID: 928
			private static readonly SendOrPostCallback switchToCallback = new SendOrPostCallback(SwitchToSynchronizationContextAwaitable.Awaiter.Callback);

			// Token: 0x040003A1 RID: 929
			private readonly SynchronizationContext synchronizationContext;

			// Token: 0x040003A2 RID: 930
			private readonly CancellationToken cancellationToken;
		}
	}
}
