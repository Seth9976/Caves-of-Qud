using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025D RID: 605
	[EventCategory(EventCategory.PointerMove)]
	public class MouseMoveEvent : MouseEventBase<MouseMoveEvent>
	{
		// Token: 0x060011F0 RID: 4592 RVA: 0x0004B6AB File Offset: 0x000498AB
		static MouseMoveEvent()
		{
			EventBase<MouseMoveEvent>.SetCreateFunction(() => new MouseMoveEvent());
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0004B6C4 File Offset: 0x000498C4
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0004A573 File Offset: 0x00048773
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0004B6D5 File Offset: 0x000498D5
		public MouseMoveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0004B6E8 File Offset: 0x000498E8
		public new static MouseMoveEvent GetPooled(Event systemEvent)
		{
			MouseMoveEvent pooled = MouseEventBase<MouseMoveEvent>.GetPooled(systemEvent);
			pooled.button = 0;
			return pooled;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0004B70C File Offset: 0x0004990C
		internal static MouseMoveEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseEventBase<MouseMoveEvent>.GetPooled(pointerEvent);
		}
	}
}
