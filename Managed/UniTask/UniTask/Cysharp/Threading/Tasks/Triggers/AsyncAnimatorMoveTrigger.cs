using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200007A RID: 122
	[DisallowMultipleComponent]
	public sealed class AsyncAnimatorMoveTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000BDD1 File Offset: 0x00009FD1
		private void OnAnimatorMove()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000BDDE File Offset: 0x00009FDE
		public IAsyncOnAnimatorMoveHandler GetOnAnimatorMoveAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000BDE7 File Offset: 0x00009FE7
		public IAsyncOnAnimatorMoveHandler GetOnAnimatorMoveAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000BDF1 File Offset: 0x00009FF1
		public UniTask OnAnimatorMoveAsync()
		{
			return ((IAsyncOnAnimatorMoveHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnAnimatorMoveAsync();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000BDFF File Offset: 0x00009FFF
		public UniTask OnAnimatorMoveAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnAnimatorMoveHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnAnimatorMoveAsync();
		}
	}
}
