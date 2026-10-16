using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200009A RID: 154
	[DisallowMultipleComponent]
	public sealed class AsyncDisableTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000C1FF File Offset: 0x0000A3FF
		private void OnDisable()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000C20C File Offset: 0x0000A40C
		public IAsyncOnDisableHandler GetOnDisableAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000C215 File Offset: 0x0000A415
		public IAsyncOnDisableHandler GetOnDisableAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000C21F File Offset: 0x0000A41F
		public UniTask OnDisableAsync()
		{
			return ((IAsyncOnDisableHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnDisableAsync();
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000C22D File Offset: 0x0000A42D
		public UniTask OnDisableAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDisableHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnDisableAsync();
		}
	}
}
