using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000259 RID: 601
	[EventCategory(EventCategory.PointerDown)]
	public class MouseDownEvent : MouseEventBase<MouseDownEvent>
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x0004B527 File Offset: 0x00049727
		static MouseDownEvent()
		{
			EventBase<MouseDownEvent>.SetCreateFunction(() => new MouseDownEvent());
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0004B540 File Offset: 0x00049740
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0004B551 File Offset: 0x00049751
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0004B55C File Offset: 0x0004975C
		public MouseDownEvent()
		{
			this.LocalInit();
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0004B570 File Offset: 0x00049770
		public new static MouseDownEvent GetPooled(Event systemEvent)
		{
			return MouseEventBase<MouseDownEvent>.GetPooled(systemEvent);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0004B588 File Offset: 0x00049788
		private static MouseDownEvent MakeFromPointerEvent(IPointerEvent pointerEvent)
		{
			return MouseEventBase<MouseDownEvent>.GetPooled(pointerEvent);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0004B5A0 File Offset: 0x000497A0
		internal static MouseDownEvent GetPooled(PointerDownEvent pointerEvent)
		{
			return MouseDownEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0004B5B8 File Offset: 0x000497B8
		internal static MouseDownEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseDownEvent.MakeFromPointerEvent(pointerEvent);
		}
	}
}
