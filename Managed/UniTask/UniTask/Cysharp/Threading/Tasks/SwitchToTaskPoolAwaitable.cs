using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000040 RID: 64
	public struct SwitchToTaskPoolAwaitable
	{
		// Token: 0x0600017D RID: 381 RVA: 0x000067AC File Offset: 0x000049AC
		public SwitchToTaskPoolAwaitable.Awaiter GetAwaiter()
		{
			return default(SwitchToTaskPoolAwaitable.Awaiter);
		}

		// Token: 0x020001C0 RID: 448
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00024D34 File Offset: 0x00022F34
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000A65 RID: 2661 RVA: 0x00024D37 File Offset: 0x00022F37
			public void GetResult()
			{
			}

			// Token: 0x06000A66 RID: 2662 RVA: 0x00024D39 File Offset: 0x00022F39
			public void OnCompleted(Action continuation)
			{
				Task.Factory.StartNew(SwitchToTaskPoolAwaitable.Awaiter.switchToCallback, continuation, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}

			// Token: 0x06000A67 RID: 2663 RVA: 0x00024D57 File Offset: 0x00022F57
			public void UnsafeOnCompleted(Action continuation)
			{
				Task.Factory.StartNew(SwitchToTaskPoolAwaitable.Awaiter.switchToCallback, continuation, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}

			// Token: 0x06000A68 RID: 2664 RVA: 0x00024D75 File Offset: 0x00022F75
			private static void Callback(object state)
			{
				((Action)state)();
			}

			// Token: 0x0400039F RID: 927
			private static readonly Action<object> switchToCallback = new Action<object>(SwitchToTaskPoolAwaitable.Awaiter.Callback);
		}
	}
}
