using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000C6 RID: 198
	[DisallowMultipleComponent]
	public sealed class AsyncRectTransformRemovedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x0000C7DD File Offset: 0x0000A9DD
		private void OnRectTransformRemoved()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000C7EA File Offset: 0x0000A9EA
		public IAsyncOnRectTransformRemovedHandler GetOnRectTransformRemovedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000C7F3 File Offset: 0x0000A9F3
		public IAsyncOnRectTransformRemovedHandler GetOnRectTransformRemovedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000C7FD File Offset: 0x0000A9FD
		public UniTask OnRectTransformRemovedAsync()
		{
			return ((IAsyncOnRectTransformRemovedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnRectTransformRemovedAsync();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000C80B File Offset: 0x0000AA0B
		public UniTask OnRectTransformRemovedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnRectTransformRemovedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnRectTransformRemovedAsync();
		}
	}
}
