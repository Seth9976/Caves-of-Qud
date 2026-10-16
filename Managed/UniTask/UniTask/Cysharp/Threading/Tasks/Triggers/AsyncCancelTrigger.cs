using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000E8 RID: 232
	[DisallowMultipleComponent]
	public sealed class AsyncCancelTrigger : AsyncTriggerBase<BaseEventData>, ICancelHandler, IEventSystemHandler
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x0000CC58 File Offset: 0x0000AE58
		void ICancelHandler.OnCancel(BaseEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000CC61 File Offset: 0x0000AE61
		public IAsyncOnCancelHandler GetOnCancelAsyncHandler()
		{
			return new AsyncTriggerHandler<BaseEventData>(this, false);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000CC6A File Offset: 0x0000AE6A
		public IAsyncOnCancelHandler GetOnCancelAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public UniTask<BaseEventData> OnCancelAsync()
		{
			return ((IAsyncOnCancelHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnCancelAsync();
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000CC82 File Offset: 0x0000AE82
		public UniTask<BaseEventData> OnCancelAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCancelHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnCancelAsync();
		}
	}
}
