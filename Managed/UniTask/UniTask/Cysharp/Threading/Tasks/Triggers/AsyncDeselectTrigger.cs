using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000EA RID: 234
	[DisallowMultipleComponent]
	public sealed class AsyncDeselectTrigger : AsyncTriggerBase<BaseEventData>, IDeselectHandler, IEventSystemHandler
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0000CC99 File Offset: 0x0000AE99
		void IDeselectHandler.OnDeselect(BaseEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000CCA2 File Offset: 0x0000AEA2
		public IAsyncOnDeselectHandler GetOnDeselectAsyncHandler()
		{
			return new AsyncTriggerHandler<BaseEventData>(this, false);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000CCAB File Offset: 0x0000AEAB
		public IAsyncOnDeselectHandler GetOnDeselectAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, false);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000CCB5 File Offset: 0x0000AEB5
		public UniTask<BaseEventData> OnDeselectAsync()
		{
			return ((IAsyncOnDeselectHandler)new AsyncTriggerHandler<BaseEventData>(this, true)).OnDeselectAsync();
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000CCC3 File Offset: 0x0000AEC3
		public UniTask<BaseEventData> OnDeselectAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnDeselectHandler)new AsyncTriggerHandler<BaseEventData>(this, cancellationToken, true)).OnDeselectAsync();
		}
	}
}
