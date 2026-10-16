using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000042 RID: 66
	public struct ReturnToSynchronizationContext
	{
		// Token: 0x06000180 RID: 384 RVA: 0x000067E5 File Offset: 0x000049E5
		public ReturnToSynchronizationContext(SynchronizationContext syncContext, bool dontPostWhenSameContext, CancellationToken cancellationToken)
		{
			this.syncContext = syncContext;
			this.dontPostWhenSameContext = dontPostWhenSameContext;
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000067FC File Offset: 0x000049FC
		public ReturnToSynchronizationContext.Awaiter DisposeAsync()
		{
			return new ReturnToSynchronizationContext.Awaiter(this.syncContext, this.dontPostWhenSameContext, this.cancellationToken);
		}

		// Token: 0x04000090 RID: 144
		private readonly SynchronizationContext syncContext;

		// Token: 0x04000091 RID: 145
		private readonly bool dontPostWhenSameContext;

		// Token: 0x04000092 RID: 146
		private readonly CancellationToken cancellationToken;

		// Token: 0x020001C2 RID: 450
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000A71 RID: 2673 RVA: 0x00024E09 File Offset: 0x00023009
			public Awaiter(SynchronizationContext synchronizationContext, bool dontPostWhenSameContext, CancellationToken cancellationToken)
			{
				this.synchronizationContext = synchronizationContext;
				this.dontPostWhenSameContext = dontPostWhenSameContext;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x06000A72 RID: 2674 RVA: 0x00024E20 File Offset: 0x00023020
			public ReturnToSynchronizationContext.Awaiter GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00024E28 File Offset: 0x00023028
			public bool IsCompleted
			{
				get
				{
					return this.dontPostWhenSameContext && SynchronizationContext.Current == this.synchronizationContext;
				}
			}

			// Token: 0x06000A74 RID: 2676 RVA: 0x00024E44 File Offset: 0x00023044
			public void GetResult()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
			}

			// Token: 0x06000A75 RID: 2677 RVA: 0x00024E5F File Offset: 0x0002305F
			public void OnCompleted(Action continuation)
			{
				this.synchronizationContext.Post(ReturnToSynchronizationContext.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x06000A76 RID: 2678 RVA: 0x00024E72 File Offset: 0x00023072
			public void UnsafeOnCompleted(Action continuation)
			{
				this.synchronizationContext.Post(ReturnToSynchronizationContext.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x00024E85 File Offset: 0x00023085
			private static void Callback(object state)
			{
				((Action)state)();
			}

			// Token: 0x040003A3 RID: 931
			private static readonly SendOrPostCallback switchToCallback = new SendOrPostCallback(ReturnToSynchronizationContext.Awaiter.Callback);

			// Token: 0x040003A4 RID: 932
			private readonly SynchronizationContext synchronizationContext;

			// Token: 0x040003A5 RID: 933
			private readonly bool dontPostWhenSameContext;

			// Token: 0x040003A6 RID: 934
			private readonly CancellationToken cancellationToken;
		}
	}
}
