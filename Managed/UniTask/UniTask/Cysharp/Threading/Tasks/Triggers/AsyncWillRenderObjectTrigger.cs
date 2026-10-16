using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000E0 RID: 224
	[DisallowMultipleComponent]
	public sealed class AsyncWillRenderObjectTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x0000CB48 File Offset: 0x0000AD48
		private void OnWillRenderObject()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000CB55 File Offset: 0x0000AD55
		public IAsyncOnWillRenderObjectHandler GetOnWillRenderObjectAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000CB5E File Offset: 0x0000AD5E
		public IAsyncOnWillRenderObjectHandler GetOnWillRenderObjectAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000CB68 File Offset: 0x0000AD68
		public UniTask OnWillRenderObjectAsync()
		{
			return ((IAsyncOnWillRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnWillRenderObjectAsync();
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000CB76 File Offset: 0x0000AD76
		public UniTask OnWillRenderObjectAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnWillRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnWillRenderObjectAsync();
		}
	}
}
