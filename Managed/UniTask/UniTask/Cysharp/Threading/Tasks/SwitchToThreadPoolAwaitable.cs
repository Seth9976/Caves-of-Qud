using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200003F RID: 63
	public struct SwitchToThreadPoolAwaitable
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00006794 File Offset: 0x00004994
		public SwitchToThreadPoolAwaitable.Awaiter GetAwaiter()
		{
			return default(SwitchToThreadPoolAwaitable.Awaiter);
		}

		// Token: 0x020001BF RID: 447
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00024CF3 File Offset: 0x00022EF3
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000A5F RID: 2655 RVA: 0x00024CF6 File Offset: 0x00022EF6
			public void GetResult()
			{
			}

			// Token: 0x06000A60 RID: 2656 RVA: 0x00024CF8 File Offset: 0x00022EF8
			public void OnCompleted(Action continuation)
			{
				ThreadPool.QueueUserWorkItem(SwitchToThreadPoolAwaitable.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x06000A61 RID: 2657 RVA: 0x00024D06 File Offset: 0x00022F06
			public void UnsafeOnCompleted(Action continuation)
			{
				ThreadPool.UnsafeQueueUserWorkItem(SwitchToThreadPoolAwaitable.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x06000A62 RID: 2658 RVA: 0x00024D14 File Offset: 0x00022F14
			private static void Callback(object state)
			{
				((Action)state)();
			}

			// Token: 0x0400039E RID: 926
			private static readonly WaitCallback switchToCallback = new WaitCallback(SwitchToThreadPoolAwaitable.Awaiter.Callback);
		}
	}
}
