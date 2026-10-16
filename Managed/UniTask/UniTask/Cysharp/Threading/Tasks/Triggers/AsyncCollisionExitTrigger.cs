using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000090 RID: 144
	[DisallowMultipleComponent]
	public sealed class AsyncCollisionExitTrigger : AsyncTriggerBase<Collision>
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x0000C0BA File Offset: 0x0000A2BA
		private void OnCollisionExit(Collision coll)
		{
			base.RaiseEvent(coll);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000C0C3 File Offset: 0x0000A2C3
		public IAsyncOnCollisionExitHandler GetOnCollisionExitAsyncHandler()
		{
			return new AsyncTriggerHandler<Collision>(this, false);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		public IAsyncOnCollisionExitHandler GetOnCollisionExitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collision>(this, cancellationToken, false);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000C0D6 File Offset: 0x0000A2D6
		public UniTask<Collision> OnCollisionExitAsync()
		{
			return ((IAsyncOnCollisionExitHandler)new AsyncTriggerHandler<Collision>(this, true)).OnCollisionExitAsync();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		public UniTask<Collision> OnCollisionExitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCollisionExitHandler)new AsyncTriggerHandler<Collision>(this, cancellationToken, true)).OnCollisionExitAsync();
		}
	}
}
