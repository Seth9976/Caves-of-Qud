using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000106 RID: 262
	[DisallowMultipleComponent]
	public sealed class AsyncUpdateSelectedTrigger : AsyncTriggerBase<BaseEventData>, IUpdateSelectedHandler, IEventSystemHandler
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x0000D027 File Offset: 0x0000B227
		void IUpdateSelectedHandler.OnUpdateSelected(BaseEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000D030 File Offset: 0x0000B230
		public IAsyncOnUpdateSelectedHandler GetOnUpdateSelectedAsyncHandler()
		{
			return new AsyncTriggerHandler<BaseEventData>(this, false);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0000D039 File Offset: 0x0000B239
		public IAsyncOnUpdateSelectedHandler GetOnUpdateSelectedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000D043 File Offset: 0x0000B243
		public UniTask<BaseEventData> OnUpdateSelectedAsync()
		{
			return ((IAsyncOnUpdateSelectedHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnUpdateSelectedAsync();
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000D051 File Offset: 0x0000B251
		public UniTask<BaseEventData> OnUpdateSelectedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnUpdateSelectedHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnUpdateSelectedAsync();
		}
	}
}
