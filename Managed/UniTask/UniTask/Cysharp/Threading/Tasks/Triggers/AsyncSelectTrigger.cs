using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000102 RID: 258
	[DisallowMultipleComponent]
	public sealed class AsyncSelectTrigger : AsyncTriggerBase<BaseEventData>, ISelectHandler, IEventSystemHandler
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0000CFA5 File Offset: 0x0000B1A5
		void ISelectHandler.OnSelect(BaseEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000CFAE File Offset: 0x0000B1AE
		public IAsyncOnSelectHandler GetOnSelectAsyncHandler()
		{
			return new AsyncTriggerHandler<BaseEventData>(this, false);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000CFB7 File Offset: 0x0000B1B7
		public IAsyncOnSelectHandler GetOnSelectAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000CFC1 File Offset: 0x0000B1C1
		public UniTask<BaseEventData> OnSelectAsync()
		{
			return ((IAsyncOnSelectHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnSelectAsync();
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000CFCF File Offset: 0x0000B1CF
		public UniTask<BaseEventData> OnSelectAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnSelectHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnSelectAsync();
		}
	}
}
