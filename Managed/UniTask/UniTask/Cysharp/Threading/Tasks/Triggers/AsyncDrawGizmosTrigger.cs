using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200009C RID: 156
	[DisallowMultipleComponent]
	public sealed class AsyncDrawGizmosTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x0000C244 File Offset: 0x0000A444
		private void OnDrawGizmos()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000C251 File Offset: 0x0000A451
		public IAsyncOnDrawGizmosHandler GetOnDrawGizmosAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000C25A File Offset: 0x0000A45A
		public IAsyncOnDrawGizmosHandler GetOnDrawGizmosAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000C264 File Offset: 0x0000A464
		public UniTask OnDrawGizmosAsync()
		{
			return ((IAsyncOnDrawGizmosHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnDrawGizmosAsync();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000C272 File Offset: 0x0000A472
		public UniTask OnDrawGizmosAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDrawGizmosHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnDrawGizmosAsync();
		}
	}
}
