using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000086 RID: 134
	[DisallowMultipleComponent]
	public sealed class AsyncBecameVisibleTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x0000BF69 File Offset: 0x0000A169
		private void OnBecameVisible()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000BF76 File Offset: 0x0000A176
		public IAsyncOnBecameVisibleHandler GetOnBecameVisibleAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000BF7F File Offset: 0x0000A17F
		public IAsyncOnBecameVisibleHandler GetOnBecameVisibleAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000BF89 File Offset: 0x0000A189
		public UniTask OnBecameVisibleAsync()
		{
			return ((IAsyncOnBecameVisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnBecameVisibleAsync();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000BF97 File Offset: 0x0000A197
		public UniTask OnBecameVisibleAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnBecameVisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnBecameVisibleAsync();
		}
	}
}
