using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000CE RID: 206
	[DisallowMultipleComponent]
	public sealed class AsyncTransformChildrenChangedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x0000C8F3 File Offset: 0x0000AAF3
		private void OnTransformChildrenChanged()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000C900 File Offset: 0x0000AB00
		public IAsyncOnTransformChildrenChangedHandler GetOnTransformChildrenChangedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000C909 File Offset: 0x0000AB09
		public IAsyncOnTransformChildrenChangedHandler GetOnTransformChildrenChangedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000C913 File Offset: 0x0000AB13
		public UniTask OnTransformChildrenChangedAsync()
		{
			return ((IAsyncOnTransformChildrenChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnTransformChildrenChangedAsync();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000C921 File Offset: 0x0000AB21
		public UniTask OnTransformChildrenChangedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTransformChildrenChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnTransformChildrenChangedAsync();
		}
	}
}
