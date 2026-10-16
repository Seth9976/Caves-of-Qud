using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000D6 RID: 214
	[DisallowMultipleComponent]
	public sealed class AsyncTriggerExitTrigger : AsyncTriggerBase<Collider>
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x0000C9FF File Offset: 0x0000ABFF
		private void OnTriggerExit(Collider other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000CA08 File Offset: 0x0000AC08
		public IAsyncOnTriggerExitHandler GetOnTriggerExitAsyncHandler()
		{
			return new AsyncTriggerHandler<Collider>(this, false);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000CA11 File Offset: 0x0000AC11
		public IAsyncOnTriggerExitHandler GetOnTriggerExitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collider>(this, cancellationToken, false);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000CA1B File Offset: 0x0000AC1B
		public UniTask<Collider> OnTriggerExitAsync()
		{
			return ((IAsyncOnTriggerExitHandler)new AsyncTriggerHandler<Collider>(this, true)).OnTriggerExitAsync();
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000CA29 File Offset: 0x0000AC29
		public UniTask<Collider> OnTriggerExitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTriggerExitHandler)new AsyncTriggerHandler<Collider>(this, cancellationToken, true)).OnTriggerExitAsync();
		}
	}
}
