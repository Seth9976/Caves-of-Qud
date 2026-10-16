using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000F8 RID: 248
	[DisallowMultipleComponent]
	public sealed class AsyncPointerDownTrigger : AsyncTriggerBase<PointerEventData>, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x0000CE60 File Offset: 0x0000B060
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000CE69 File Offset: 0x0000B069
		public IAsyncOnPointerDownHandler GetOnPointerDownAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000CE72 File Offset: 0x0000B072
		public IAsyncOnPointerDownHandler GetOnPointerDownAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000CE7C File Offset: 0x0000B07C
		public UniTask<PointerEventData> OnPointerDownAsync()
		{
			return ((IAsyncOnPointerDownHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerDownAsync();
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000CE8A File Offset: 0x0000B08A
		public UniTask<PointerEventData> OnPointerDownAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPointerDownHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerDownAsync();
		}
	}
}
