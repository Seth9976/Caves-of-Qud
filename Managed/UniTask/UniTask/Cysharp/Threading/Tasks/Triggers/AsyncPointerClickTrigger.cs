using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000F6 RID: 246
	[DisallowMultipleComponent]
	public sealed class AsyncPointerClickTrigger : AsyncTriggerBase<PointerEventData>, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x0000CE1F File Offset: 0x0000B01F
		void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000CE28 File Offset: 0x0000B028
		public IAsyncOnPointerClickHandler GetOnPointerClickAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000CE31 File Offset: 0x0000B031
		public IAsyncOnPointerClickHandler GetOnPointerClickAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000CE3B File Offset: 0x0000B03B
		public UniTask<PointerEventData> OnPointerClickAsync()
		{
			return ((IAsyncOnPointerClickHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerClickAsync();
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000CE49 File Offset: 0x0000B049
		public UniTask<PointerEventData> OnPointerClickAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPointerClickHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerClickAsync();
		}
	}
}
