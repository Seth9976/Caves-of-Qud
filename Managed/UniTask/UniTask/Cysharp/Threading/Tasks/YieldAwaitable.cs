using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200003B RID: 59
	public readonly struct YieldAwaitable
	{
		// Token: 0x06000174 RID: 372 RVA: 0x000066CF File Offset: 0x000048CF
		public YieldAwaitable(PlayerLoopTiming timing)
		{
			this.timing = timing;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000066D8 File Offset: 0x000048D8
		public YieldAwaitable.Awaiter GetAwaiter()
		{
			return new YieldAwaitable.Awaiter(this.timing);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000066E5 File Offset: 0x000048E5
		public UniTask ToUniTask()
		{
			return UniTask.Yield(this.timing, CancellationToken.None, false);
		}

		// Token: 0x04000083 RID: 131
		private readonly PlayerLoopTiming timing;

		// Token: 0x020001BC RID: 444
		public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000A4E RID: 2638 RVA: 0x00024BFA File Offset: 0x00022DFA
			public Awaiter(PlayerLoopTiming timing)
			{
				this.timing = timing;
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00024C03 File Offset: 0x00022E03
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000A50 RID: 2640 RVA: 0x00024C06 File Offset: 0x00022E06
			public void GetResult()
			{
			}

			// Token: 0x06000A51 RID: 2641 RVA: 0x00024C08 File Offset: 0x00022E08
			public void OnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.timing, continuation);
			}

			// Token: 0x06000A52 RID: 2642 RVA: 0x00024C16 File Offset: 0x00022E16
			public void UnsafeOnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.timing, continuation);
			}

			// Token: 0x04000399 RID: 921
			private readonly PlayerLoopTiming timing;
		}
	}
}
