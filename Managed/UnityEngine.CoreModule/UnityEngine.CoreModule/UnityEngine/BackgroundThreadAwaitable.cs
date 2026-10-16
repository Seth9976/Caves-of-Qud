using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000279 RID: 633
	[ExcludeFromDocs]
	public struct BackgroundThreadAwaitable : INotifyCompletion
	{
		// Token: 0x06001FF5 RID: 8181 RVA: 0x0003D93F File Offset: 0x0003BB3F
		internal BackgroundThreadAwaitable(SynchronizationContext syncContext, int mainThreadId)
		{
			this._synchronizationContext = syncContext;
			this._mainThreadId = mainThreadId;
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0003D950 File Offset: 0x0003BB50
		public BackgroundThreadAwaitable GetAwaiter()
		{
			return this;
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x0003D958 File Offset: 0x0003BB58
		public bool IsCompleted
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId != this._mainThreadId;
			}
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00002925 File Offset: 0x00000B25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetResult()
		{
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x0003D96F File Offset: 0x0003BB6F
		public void OnCompleted(Action continuation)
		{
			Task.Run(continuation);
		}

		// Token: 0x04000977 RID: 2423
		private readonly SynchronizationContext _synchronizationContext;

		// Token: 0x04000978 RID: 2424
		private readonly int _mainThreadId;
	}
}
