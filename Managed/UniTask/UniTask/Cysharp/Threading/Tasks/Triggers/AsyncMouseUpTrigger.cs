using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000B2 RID: 178
	[DisallowMultipleComponent]
	public sealed class AsyncMouseUpTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x0000C533 File Offset: 0x0000A733
		private void OnMouseUp()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000C540 File Offset: 0x0000A740
		public IAsyncOnMouseUpHandler GetOnMouseUpAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000C549 File Offset: 0x0000A749
		public IAsyncOnMouseUpHandler GetOnMouseUpAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000C553 File Offset: 0x0000A753
		public UniTask OnMouseUpAsync()
		{
			return ((IAsyncOnMouseUpHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseUpAsync();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000C561 File Offset: 0x0000A761
		public UniTask OnMouseUpAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseUpHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseUpAsync();
		}
	}
}
