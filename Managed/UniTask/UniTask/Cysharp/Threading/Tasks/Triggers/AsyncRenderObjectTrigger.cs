using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000CA RID: 202
	[DisallowMultipleComponent]
	public sealed class AsyncRenderObjectTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x0000C869 File Offset: 0x0000AA69
		private void OnRenderObject()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000C876 File Offset: 0x0000AA76
		public IAsyncOnRenderObjectHandler GetOnRenderObjectAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000C87F File Offset: 0x0000AA7F
		public IAsyncOnRenderObjectHandler GetOnRenderObjectAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000C889 File Offset: 0x0000AA89
		public UniTask OnRenderObjectAsync()
		{
			return ((IAsyncOnRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnRenderObjectAsync();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000C897 File Offset: 0x0000AA97
		public UniTask OnRenderObjectAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnRenderObjectHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnRenderObjectAsync();
		}
	}
}
