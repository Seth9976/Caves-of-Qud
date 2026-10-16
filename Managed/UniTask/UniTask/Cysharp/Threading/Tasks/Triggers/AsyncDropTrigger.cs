using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000EE RID: 238
	[DisallowMultipleComponent]
	public sealed class AsyncDropTrigger : AsyncTriggerBase<PointerEventData>, IDropHandler, IEventSystemHandler
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x0000CD1B File Offset: 0x0000AF1B
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000CD24 File Offset: 0x0000AF24
		public IAsyncOnDropHandler GetOnDropAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000CD2D File Offset: 0x0000AF2D
		public IAsyncOnDropHandler GetOnDropAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000CD37 File Offset: 0x0000AF37
		public UniTask<PointerEventData> OnDropAsync()
		{
			return ((IAsyncOnDropHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnDropAsync();
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000CD45 File Offset: 0x0000AF45
		public UniTask<PointerEventData> OnDropAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDropHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnDropAsync();
		}
	}
}
