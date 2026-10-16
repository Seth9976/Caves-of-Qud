using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000278 RID: 632
	[ExcludeFromDocs]
	public struct MainThreadAwaitable : INotifyCompletion
	{
		// Token: 0x06001FEF RID: 8175 RVA: 0x0003D8F6 File Offset: 0x0003BAF6
		internal MainThreadAwaitable(SynchronizationContext syncContext, int mainThreadId)
		{
			this._synchronizationContext = syncContext;
			this._mainThreadId = mainThreadId;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x0003D907 File Offset: 0x0003BB07
		public MainThreadAwaitable GetAwaiter()
		{
			return this;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0003D90F File Offset: 0x0003BB0F
		public bool IsCompleted
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId == this._mainThreadId;
			}
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x00002925 File Offset: 0x00000B25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetResult()
		{
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0003D923 File Offset: 0x0003BB23
		public void OnCompleted(Action continuation)
		{
			this._synchronizationContext.Post(new SendOrPostCallback(MainThreadAwaitable.DoOnCompleted), continuation);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0003C8D4 File Offset: 0x0003AAD4
		private static void DoOnCompleted(object continuation)
		{
			Action action = continuation as Action;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x04000975 RID: 2421
		private readonly SynchronizationContext _synchronizationContext;

		// Token: 0x04000976 RID: 2422
		private readonly int _mainThreadId;
	}
}
