using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000E2 RID: 226
	[DisallowMultipleComponent]
	public sealed class AsyncResetTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000570 RID: 1392 RVA: 0x0000CB8D File Offset: 0x0000AD8D
		private void Reset()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000CB9A File Offset: 0x0000AD9A
		public IAsyncResetHandler GetResetAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000CBA3 File Offset: 0x0000ADA3
		public IAsyncResetHandler GetResetAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000CBAD File Offset: 0x0000ADAD
		public UniTask ResetAsync()
		{
			return ((IAsyncResetHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).ResetAsync();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000CBBB File Offset: 0x0000ADBB
		public UniTask ResetAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncResetHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).ResetAsync();
		}
	}
}
