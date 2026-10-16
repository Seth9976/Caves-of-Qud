using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000C4 RID: 196
	[DisallowMultipleComponent]
	public sealed class AsyncRectTransformDimensionsChangeTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x0000C798 File Offset: 0x0000A998
		private void OnRectTransformDimensionsChange()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000C7A5 File Offset: 0x0000A9A5
		public IAsyncOnRectTransformDimensionsChangeHandler GetOnRectTransformDimensionsChangeAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000C7AE File Offset: 0x0000A9AE
		public IAsyncOnRectTransformDimensionsChangeHandler GetOnRectTransformDimensionsChangeAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000C7B8 File Offset: 0x0000A9B8
		public UniTask OnRectTransformDimensionsChangeAsync()
		{
			return ((IAsyncOnRectTransformDimensionsChangeHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnRectTransformDimensionsChangeAsync();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000C7C6 File Offset: 0x0000A9C6
		public UniTask OnRectTransformDimensionsChangeAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnRectTransformDimensionsChangeHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnRectTransformDimensionsChangeAsync();
		}
	}
}
