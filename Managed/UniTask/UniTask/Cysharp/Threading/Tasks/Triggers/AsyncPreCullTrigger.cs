using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000C0 RID: 192
	[DisallowMultipleComponent]
	public sealed class AsyncPreCullTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x0000C70E File Offset: 0x0000A90E
		private void OnPreCull()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000C71B File Offset: 0x0000A91B
		public IAsyncOnPreCullHandler GetOnPreCullAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000C724 File Offset: 0x0000A924
		public IAsyncOnPreCullHandler GetOnPreCullAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000C72E File Offset: 0x0000A92E
		public UniTask OnPreCullAsync()
		{
			return ((IAsyncOnPreCullHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnPreCullAsync();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000C73C File Offset: 0x0000A93C
		public UniTask OnPreCullAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPreCullHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnPreCullAsync();
		}
	}
}
