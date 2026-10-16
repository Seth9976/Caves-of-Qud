using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000FE RID: 254
	[DisallowMultipleComponent]
	public sealed class AsyncPointerUpTrigger : AsyncTriggerBase<PointerEventData>, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x0000CF23 File Offset: 0x0000B123
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000CF2C File Offset: 0x0000B12C
		public IAsyncOnPointerUpHandler GetOnPointerUpAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000CF35 File Offset: 0x0000B135
		public IAsyncOnPointerUpHandler GetOnPointerUpAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000CF3F File Offset: 0x0000B13F
		public UniTask<PointerEventData> OnPointerUpAsync()
		{
			return ((IAsyncOnPointerUpHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerUpAsync();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000CF4D File Offset: 0x0000B14D
		public UniTask<PointerEventData> OnPointerUpAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPointerUpHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerUpAsync();
		}
	}
}
