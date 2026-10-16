using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000F2 RID: 242
	[DisallowMultipleComponent]
	public sealed class AsyncInitializePotentialDragTrigger : AsyncTriggerBase<PointerEventData>, IInitializePotentialDragHandler, IEventSystemHandler
	{
		// Token: 0x060005A8 RID: 1448 RVA: 0x0000CD9D File Offset: 0x0000AF9D
		void IInitializePotentialDragHandler.OnInitializePotentialDrag(PointerEventData eventData)
		{
			base.RaiseEvent(eventData);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000CDA6 File Offset: 0x0000AFA6
		public IAsyncOnInitializePotentialDragHandler GetOnInitializePotentialDragAsyncHandler()
		{
			return new AsyncTriggerHandler<PointerEventData>(this, false);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000CDAF File Offset: 0x0000AFAF
		public IAsyncOnInitializePotentialDragHandler GetOnInitializePotentialDragAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, false);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000CDB9 File Offset: 0x0000AFB9
		public UniTask<PointerEventData> OnInitializePotentialDragAsync()
		{
			return ((IAsyncOnInitializePotentialDragHandler)new AsyncTriggerHandler<PointerEventData>(this, true)).OnInitializePotentialDragAsync();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000CDC7 File Offset: 0x0000AFC7
		public UniTask<PointerEventData> OnInitializePotentialDragAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnInitializePotentialDragHandler)new AsyncTriggerHandler<PointerEventData>(this, cancellationToken, true)).OnInitializePotentialDragAsync();
		}
	}
}
