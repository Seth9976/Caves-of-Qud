using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000066 RID: 102
	[AddComponentMenu("Event/Event Trigger")]
	public class EventTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001916A File Offset: 0x0001736A
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x00019172 File Offset: 0x00017372
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use triggers instead (UnityUpgradable) -> triggers", true)]
		public List<EventTrigger.Entry> delegates
		{
			get
			{
				return this.triggers;
			}
			set
			{
				this.triggers = value;
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001917B File Offset: 0x0001737B
		protected EventTrigger()
		{
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00019183 File Offset: 0x00017383
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0001919E File Offset: 0x0001739E
		public List<EventTrigger.Entry> triggers
		{
			get
			{
				if (this.m_Delegates == null)
				{
					this.m_Delegates = new List<EventTrigger.Entry>();
				}
				return this.m_Delegates;
			}
			set
			{
				this.m_Delegates = value;
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000191A8 File Offset: 0x000173A8
		private void Execute(EventTriggerType id, BaseEventData eventData)
		{
			for (int i = 0; i < this.triggers.Count; i++)
			{
				EventTrigger.Entry entry = this.triggers[i];
				if (entry.eventID == id && entry.callback != null)
				{
					entry.callback.Invoke(eventData);
				}
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000191F5 File Offset: 0x000173F5
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerEnter, eventData);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000191FF File Offset: 0x000173FF
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerExit, eventData);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00019209 File Offset: 0x00017409
		public virtual void OnDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drag, eventData);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00019213 File Offset: 0x00017413
		public virtual void OnDrop(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drop, eventData);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001921D File Offset: 0x0001741D
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerDown, eventData);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00019227 File Offset: 0x00017427
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerUp, eventData);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00019231 File Offset: 0x00017431
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerClick, eventData);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001923B File Offset: 0x0001743B
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Select, eventData);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00019246 File Offset: 0x00017446
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Deselect, eventData);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00019251 File Offset: 0x00017451
		public virtual void OnScroll(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Scroll, eventData);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001925B File Offset: 0x0001745B
		public virtual void OnMove(AxisEventData eventData)
		{
			this.Execute(EventTriggerType.Move, eventData);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00019266 File Offset: 0x00017466
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.UpdateSelected, eventData);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00019270 File Offset: 0x00017470
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.InitializePotentialDrag, eventData);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001927B File Offset: 0x0001747B
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.BeginDrag, eventData);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00019286 File Offset: 0x00017486
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.EndDrag, eventData);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019291 File Offset: 0x00017491
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Submit, eventData);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001929C File Offset: 0x0001749C
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Cancel, eventData);
		}

		// Token: 0x040001E3 RID: 483
		[FormerlySerializedAs("delegates")]
		[SerializeField]
		private List<EventTrigger.Entry> m_Delegates;

		// Token: 0x020000C3 RID: 195
		[Serializable]
		public class TriggerEvent : UnityEvent<BaseEventData>
		{
		}

		// Token: 0x020000C4 RID: 196
		[Serializable]
		public class Entry
		{
			// Token: 0x04000353 RID: 851
			public EventTriggerType eventID = EventTriggerType.PointerClick;

			// Token: 0x04000354 RID: 852
			public EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
		}
	}
}
