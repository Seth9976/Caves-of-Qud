using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000100 RID: 256
	[DisallowMultipleComponent]
	public sealed class AsyncScrollTrigger : AsyncTriggerBase<PointerEventData>, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x0000CF64 File Offset: 0x0000B164
		void IScrollHandler.OnScroll(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000CF6D File Offset: 0x0000B16D
		public IAsyncOnScrollHandler GetOnScrollAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000CF76 File Offset: 0x0000B176
		public IAsyncOnScrollHandler GetOnScrollAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000CF80 File Offset: 0x0000B180
		public UniTask<PointerEventData> OnScrollAsync()
		{
			return ((IAsyncOnScrollHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnScrollAsync();
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000CF8E File Offset: 0x0000B18E
		public UniTask<PointerEventData> OnScrollAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnScrollHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnScrollAsync();
		}
	}
}
