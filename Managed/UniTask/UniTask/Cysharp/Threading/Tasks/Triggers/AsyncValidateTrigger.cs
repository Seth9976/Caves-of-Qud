using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000DE RID: 222
	[DisallowMultipleComponent]
	public sealed class AsyncValidateTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x0000CB03 File Offset: 0x0000AD03
		private void OnValidate()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000CB10 File Offset: 0x0000AD10
		public IAsyncOnValidateHandler GetOnValidateAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000CB19 File Offset: 0x0000AD19
		public IAsyncOnValidateHandler GetOnValidateAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000CB23 File Offset: 0x0000AD23
		public UniTask OnValidateAsync()
		{
			return ((IAsyncOnValidateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnValidateAsync();
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000CB31 File Offset: 0x0000AD31
		public UniTask OnValidateAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnValidateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnValidateAsync();
		}
	}
}
