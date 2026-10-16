using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000F0 RID: 240
	[DisallowMultipleComponent]
	public sealed class AsyncEndDragTrigger : AsyncTriggerBase<PointerEventData>, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000CD65 File Offset: 0x0000AF65
		public IAsyncOnEndDragHandler GetOnEndDragAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000CD6E File Offset: 0x0000AF6E
		public IAsyncOnEndDragHandler GetOnEndDragAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000CD78 File Offset: 0x0000AF78
		public UniTask<PointerEventData> OnEndDragAsync()
		{
			return ((IAsyncOnEndDragHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnEndDragAsync();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000CD86 File Offset: 0x0000AF86
		public UniTask<PointerEventData> OnEndDragAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnEndDragHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnEndDragAsync();
		}
	}
}
