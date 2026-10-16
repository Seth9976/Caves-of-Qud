using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000AA RID: 170
	[DisallowMultipleComponent]
	public sealed class AsyncMouseDragTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x0000C41F File Offset: 0x0000A61F
		private void OnMouseDrag()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000C42C File Offset: 0x0000A62C
		public IAsyncOnMouseDragHandler GetOnMouseDragAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000C435 File Offset: 0x0000A635
		public IAsyncOnMouseDragHandler GetOnMouseDragAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000C43F File Offset: 0x0000A63F
		public UniTask OnMouseDragAsync()
		{
			return ((IAsyncOnMouseDragHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseDragAsync();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000C44D File Offset: 0x0000A64D
		public UniTask OnMouseDragAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseDragHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseDragAsync();
		}
	}
}
