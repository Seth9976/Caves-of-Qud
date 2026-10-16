using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000FC RID: 252
	[DisallowMultipleComponent]
	public sealed class AsyncPointerExitTrigger : AsyncTriggerBase<PointerEventData>, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x0000CEE2 File Offset: 0x0000B0E2
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000CEEB File Offset: 0x0000B0EB
		public IAsyncOnPointerExitHandler GetOnPointerExitAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		public IAsyncOnPointerExitHandler GetOnPointerExitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000CEFE File Offset: 0x0000B0FE
		public UniTask<PointerEventData> OnPointerExitAsync()
		{
			return ((IAsyncOnPointerExitHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnPointerExitAsync();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000CF0C File Offset: 0x0000B10C
		public UniTask<PointerEventData> OnPointerExitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPointerExitHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnPointerExitAsync();
		}
	}
}
