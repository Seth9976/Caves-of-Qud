using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200009E RID: 158
	[DisallowMultipleComponent]
	public sealed class AsyncDrawGizmosSelectedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x0000C289 File Offset: 0x0000A489
		private void OnDrawGizmosSelected()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000C296 File Offset: 0x0000A496
		public IAsyncOnDrawGizmosSelectedHandler GetOnDrawGizmosSelectedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000C29F File Offset: 0x0000A49F
		public IAsyncOnDrawGizmosSelectedHandler GetOnDrawGizmosSelectedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000C2A9 File Offset: 0x0000A4A9
		public UniTask OnDrawGizmosSelectedAsync()
		{
			return ((IAsyncOnDrawGizmosSelectedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnDrawGizmosSelectedAsync();
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000C2B7 File Offset: 0x0000A4B7
		public UniTask OnDrawGizmosSelectedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDrawGizmosSelectedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnDrawGizmosSelectedAsync();
		}
	}
}
