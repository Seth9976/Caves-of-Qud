using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000EC RID: 236
	[DisallowMultipleComponent]
	public sealed class AsyncDragTrigger : AsyncTriggerBase<PointerEventData>, IDragHandler, IEventSystemHandler
	{
		// Token: 0x06000593 RID: 1427 RVA: 0x0000CCDA File Offset: 0x0000AEDA
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000CCE3 File Offset: 0x0000AEE3
		public IAsyncOnDragHandler GetOnDragAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000CCEC File Offset: 0x0000AEEC
		public IAsyncOnDragHandler GetOnDragAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000CCF6 File Offset: 0x0000AEF6
		public UniTask<PointerEventData> OnDragAsync()
		{
			return ((IAsyncOnDragHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnDragAsync();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000CD04 File Offset: 0x0000AF04
		public UniTask<PointerEventData> OnDragAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDragHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnDragAsync();
		}
	}
}
