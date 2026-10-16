using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200000E RID: 14
	public struct CancellationTokenAwaitable
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002A0B File Offset: 0x00000C0B
		public CancellationTokenAwaitable(CancellationToken cancellationToken)
		{
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A14 File Offset: 0x00000C14
		public CancellationTokenAwaitable.Awaiter GetAwaiter()
		{
			return new CancellationTokenAwaitable.Awaiter(this.cancellationToken);
		}

		// Token: 0x04000019 RID: 25
		private CancellationToken cancellationToken;

		// Token: 0x02000138 RID: 312
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600073B RID: 1851 RVA: 0x00010B6A File Offset: 0x0000ED6A
			public Awaiter(CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x0600073C RID: 1852 RVA: 0x00010B73 File Offset: 0x0000ED73
			public bool IsCompleted
			{
				get
				{
					return !this.cancellationToken.CanBeCanceled || this.cancellationToken.IsCancellationRequested;
				}
			}

			// Token: 0x0600073D RID: 1853 RVA: 0x00010B8F File Offset: 0x0000ED8F
			public void GetResult()
			{
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x00010B91 File Offset: 0x0000ED91
			public void OnCompleted(Action continuation)
			{
				this.UnsafeOnCompleted(continuation);
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x00010B9A File Offset: 0x0000ED9A
			public void UnsafeOnCompleted(Action continuation)
			{
				this.cancellationToken.RegisterWithoutCaptureExecutionContext(continuation);
			}

			// Token: 0x040001AA RID: 426
			private CancellationToken cancellationToken;
		}
	}
}
