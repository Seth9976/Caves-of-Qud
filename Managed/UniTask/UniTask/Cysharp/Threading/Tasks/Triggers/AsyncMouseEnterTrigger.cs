using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000AC RID: 172
	[DisallowMultipleComponent]
	public sealed class AsyncMouseEnterTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000C464 File Offset: 0x0000A664
		private void OnMouseEnter()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000C471 File Offset: 0x0000A671
		public IAsyncOnMouseEnterHandler GetOnMouseEnterAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000C47A File Offset: 0x0000A67A
		public IAsyncOnMouseEnterHandler GetOnMouseEnterAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000C484 File Offset: 0x0000A684
		public UniTask OnMouseEnterAsync()
		{
			return ((IAsyncOnMouseEnterHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseEnterAsync();
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000C492 File Offset: 0x0000A692
		public UniTask OnMouseEnterAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseEnterHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseEnterAsync();
		}
	}
}
