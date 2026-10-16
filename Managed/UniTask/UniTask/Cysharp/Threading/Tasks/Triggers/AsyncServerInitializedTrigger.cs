using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000CC RID: 204
	[DisallowMultipleComponent]
	public sealed class AsyncServerInitializedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0000C8AE File Offset: 0x0000AAAE
		private void OnServerInitialized()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000C8BB File Offset: 0x0000AABB
		public IAsyncOnServerInitializedHandler GetOnServerInitializedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		public IAsyncOnServerInitializedHandler GetOnServerInitializedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000C8CE File Offset: 0x0000AACE
		public UniTask OnServerInitializedAsync()
		{
			return ((IAsyncOnServerInitializedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnServerInitializedAsync();
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000C8DC File Offset: 0x0000AADC
		public UniTask OnServerInitializedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnServerInitializedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnServerInitializedAsync();
		}
	}
}
