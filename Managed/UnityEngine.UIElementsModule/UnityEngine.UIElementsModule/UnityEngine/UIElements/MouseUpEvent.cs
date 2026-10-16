using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025B RID: 603
	public class MouseUpEvent : MouseEventBase<MouseUpEvent>
	{
		// Token: 0x060011E4 RID: 4580 RVA: 0x0004B5E3 File Offset: 0x000497E3
		static MouseUpEvent()
		{
			EventBase<MouseUpEvent>.SetCreateFunction(() => new MouseUpEvent());
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0004B5FC File Offset: 0x000497FC
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0004B551 File Offset: 0x00049751
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0004B60D File Offset: 0x0004980D
		public MouseUpEvent()
		{
			this.LocalInit();
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0004B620 File Offset: 0x00049820
		public new static MouseUpEvent GetPooled(Event systemEvent)
		{
			return MouseEventBase<MouseUpEvent>.GetPooled(systemEvent);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0004B638 File Offset: 0x00049838
		private static MouseUpEvent MakeFromPointerEvent(IPointerEvent pointerEvent)
		{
			return MouseEventBase<MouseUpEvent>.GetPooled(pointerEvent);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0004B650 File Offset: 0x00049850
		internal static MouseUpEvent GetPooled(PointerUpEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0004B668 File Offset: 0x00049868
		internal static MouseUpEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0004B680 File Offset: 0x00049880
		internal static MouseUpEvent GetPooled(PointerCancelEvent pointerEvent)
		{
			return MouseUpEvent.MakeFromPointerEvent(pointerEvent);
		}
	}
}
