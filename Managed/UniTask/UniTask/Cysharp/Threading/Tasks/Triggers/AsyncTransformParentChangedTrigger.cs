using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000D0 RID: 208
	[DisallowMultipleComponent]
	public sealed class AsyncTransformParentChangedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0000C938 File Offset: 0x0000AB38
		private void OnTransformParentChanged()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000C945 File Offset: 0x0000AB45
		public IAsyncOnTransformParentChangedHandler GetOnTransformParentChangedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000C94E File Offset: 0x0000AB4E
		public IAsyncOnTransformParentChangedHandler GetOnTransformParentChangedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000C958 File Offset: 0x0000AB58
		public UniTask OnTransformParentChangedAsync()
		{
			return ((IAsyncOnTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnTransformParentChangedAsync();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000C966 File Offset: 0x0000AB66
		public UniTask OnTransformParentChangedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTransformParentChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnTransformParentChangedAsync();
		}
	}
}
