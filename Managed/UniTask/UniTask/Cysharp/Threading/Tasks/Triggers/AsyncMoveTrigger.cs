using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000F4 RID: 244
	[DisallowMultipleComponent]
	public sealed class AsyncMoveTrigger : AsyncTriggerBase<AxisEventData>, IMoveHandler, IEventSystemHandler
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x0000CDDE File Offset: 0x0000AFDE
		void IMoveHandler.OnMove(AxisEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000CDE7 File Offset: 0x0000AFE7
		public IAsyncOnMoveHandler GetOnMoveAsyncHandler()
		{
			return new AsyncTriggerHandler<AxisEventData>(this, false);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
		public IAsyncOnMoveHandler GetOnMoveAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AxisEventData>(this, cancellationToken, false);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000CDFA File Offset: 0x0000AFFA
		public UniTask<AxisEventData> OnMoveAsync()
		{
			return ((IAsyncOnMoveHandler)new AsyncTriggerHandler<AxisEventData>(this, true)).OnMoveAsync();
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000CE08 File Offset: 0x0000B008
		public UniTask<AxisEventData> OnMoveAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMoveHandler)new AsyncTriggerHandler<AxisEventData>(this, cancellationToken, true)).OnMoveAsync();
		}
	}
}
