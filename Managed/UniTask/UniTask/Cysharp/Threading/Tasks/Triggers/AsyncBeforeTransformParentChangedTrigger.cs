using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000088 RID: 136
	[DisallowMultipleComponent]
	public sealed class AsyncBeforeTransformParentChangedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x0000BFAE File Offset: 0x0000A1AE
		private void OnBeforeTransformParentChanged()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000BFBB File Offset: 0x0000A1BB
		public IAsyncOnBeforeTransformParentChangedHandler GetOnBeforeTransformParentChangedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000BFC4 File Offset: 0x0000A1C4
		public IAsyncOnBeforeTransformParentChangedHandler GetOnBeforeTransformParentChangedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000BFCE File Offset: 0x0000A1CE
		public UniTask OnBeforeTransformParentChangedAsync()
		{
			return ((IAsyncOnBeforeTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnBeforeTransformParentChangedAsync();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		public UniTask OnBeforeTransformParentChangedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnBeforeTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnBeforeTransformParentChangedAsync();
		}
	}
}
