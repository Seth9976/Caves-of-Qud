using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x02000053 RID: 83
	internal class ClampedDragger<T> : Clickable where T : IComparable<T>
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002A7 RID: 679 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		// (remove) Token: 0x060002A8 RID: 680 RVA: 0x0000CB64 File Offset: 0x0000AD64
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action dragging;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002A9 RID: 681 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		// (remove) Token: 0x060002AA RID: 682 RVA: 0x0000CBD4 File Offset: 0x0000ADD4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action draggingEnded;

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000CC09 File Offset: 0x0000AE09
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000CC11 File Offset: 0x0000AE11
		public ClampedDragger<T>.DragDirection dragDirection { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000CC1A File Offset: 0x0000AE1A
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000CC22 File Offset: 0x0000AE22
		private BaseSlider<T> slider { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000CC2B File Offset: 0x0000AE2B
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000CC33 File Offset: 0x0000AE33
		public Vector2 startMousePosition { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000CC3C File Offset: 0x0000AE3C
		public Vector2 delta
		{
			get
			{
				return base.lastMousePosition - this.startMousePosition;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000CC4F File Offset: 0x0000AE4F
		public ClampedDragger(BaseSlider<T> slider, Action clickHandler, Action dragHandler)
			: base(clickHandler, 250L, 30L)
		{
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			this.slider = slider;
			this.dragging += dragHandler;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000CC7B File Offset: 0x0000AE7B
		protected override void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.startMousePosition = localPosition;
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			base.ProcessDownEvent(evt, localPosition, pointerId);
			Action action = this.dragging;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000CCAA File Offset: 0x0000AEAA
		protected override void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			base.ProcessUpEvent(evt, localPosition, pointerId);
			Action action = this.draggingEnded;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000CCCC File Offset: 0x0000AECC
		protected override void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			base.ProcessMoveEvent(evt, localPosition);
			bool flag = this.dragDirection == ClampedDragger<T>.DragDirection.None;
			if (flag)
			{
				this.dragDirection = ClampedDragger<T>.DragDirection.Free;
			}
			bool flag2 = this.dragDirection == ClampedDragger<T>.DragDirection.Free;
			if (flag2)
			{
				bool flag3 = evt.eventTypeId == EventBase<PointerMoveEvent>.TypeId();
				if (flag3)
				{
					PointerMoveEvent pointerMoveEvent = (PointerMoveEvent)evt;
					bool flag4 = pointerMoveEvent.pointerId != PointerId.mousePointerId;
					if (flag4)
					{
						pointerMoveEvent.isHandledByDraggable = true;
					}
				}
				Action action = this.dragging;
				if (action != null)
				{
					action();
				}
			}
		}

		// Token: 0x02000054 RID: 84
		[Flags]
		public enum DragDirection
		{
			// Token: 0x0400019B RID: 411
			None = 0,
			// Token: 0x0400019C RID: 412
			LowToHigh = 1,
			// Token: 0x0400019D RID: 413
			HighToLow = 2,
			// Token: 0x0400019E RID: 414
			Free = 4
		}
	}
}
