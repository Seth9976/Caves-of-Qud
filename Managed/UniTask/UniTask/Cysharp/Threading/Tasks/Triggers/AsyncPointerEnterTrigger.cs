using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000FA RID: 250
	[DisallowMultipleComponent]
	public sealed class AsyncPointerEnterTrigger : AsyncTriggerBase<PointerEventData>, IPointerEnterHandler, IEventSystemHandler
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x0000CEA1 File Offset: 0x0000B0A1
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000CEAA File Offset: 0x0000B0AA
		public IAsyncOnPointerEnterHandler GetOnPointerEnterAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000CEB3 File Offset: 0x0000B0B3
		public IAsyncOnPointerEnterHandler GetOnPointerEnterAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000CEBD File Offset: 0x0000B0BD
		public UniTask<PointerEventData> OnPointerEnterAsync()
		{
			return ((IAsyncOnPointerEnterHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerEnterAsync();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000CECB File Offset: 0x0000B0CB
		public UniTask<PointerEventData> OnPointerEnterAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPointerEnterHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerEnterAsync();
		}
	}
}
