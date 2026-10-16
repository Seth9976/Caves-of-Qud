using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200026F RID: 623
	public class ContextualMenuPopulateEvent : MouseEventBase<ContextualMenuPopulateEvent>
	{
		// Token: 0x0600123C RID: 4668 RVA: 0x0004BB86 File Offset: 0x00049D86
		static ContextualMenuPopulateEvent()
		{
			EventBase<ContextualMenuPopulateEvent>.SetCreateFunction(() => new ContextualMenuPopulateEvent());
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0004BB9F File Offset: 0x00049D9F
		// (set) Token: 0x0600123E RID: 4670 RVA: 0x0004BBA7 File Offset: 0x00049DA7
		public DropdownMenu menu { get; private set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0004BBB0 File Offset: 0x00049DB0
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x0004BBB8 File Offset: 0x00049DB8
		public EventBase triggerEvent { get; private set; }

		// Token: 0x06001241 RID: 4673 RVA: 0x0004BBC4 File Offset: 0x00049DC4
		public static ContextualMenuPopulateEvent GetPooled(EventBase triggerEvent, DropdownMenu menu, IEventHandler target, ContextualMenuManager menuManager)
		{
			ContextualMenuPopulateEvent pooled = EventBase<ContextualMenuPopulateEvent>.GetPooled(triggerEvent);
			bool flag = triggerEvent != null;
			if (flag)
			{
				triggerEvent.Acquire();
				pooled.triggerEvent = triggerEvent;
				IMouseEvent mouseEvent = triggerEvent as IMouseEvent;
				bool flag2 = mouseEvent != null;
				if (flag2)
				{
					pooled.modifiers = mouseEvent.modifiers;
					pooled.mousePosition = mouseEvent.mousePosition;
					pooled.localMousePosition = mouseEvent.mousePosition;
					pooled.mouseDelta = mouseEvent.mouseDelta;
					pooled.button = mouseEvent.button;
					pooled.clickCount = mouseEvent.clickCount;
				}
				else
				{
					IPointerEvent pointerEvent = triggerEvent as IPointerEvent;
					bool flag3 = pointerEvent != null;
					if (flag3)
					{
						pooled.modifiers = pointerEvent.modifiers;
						pooled.mousePosition = pointerEvent.position;
						pooled.localMousePosition = pointerEvent.position;
						pooled.mouseDelta = pointerEvent.deltaPosition;
						pooled.button = pointerEvent.button;
						pooled.clickCount = pointerEvent.clickCount;
					}
				}
				IMouseEventInternal mouseEventInternal = triggerEvent as IMouseEventInternal;
				bool flag4 = mouseEventInternal != null;
				if (flag4)
				{
					((IMouseEventInternal)pooled).triggeredByOS = mouseEventInternal.triggeredByOS;
				}
				else
				{
					IPointerEventInternal pointerEventInternal = triggerEvent as IPointerEventInternal;
					bool flag5 = pointerEventInternal != null;
					if (flag5)
					{
						((IMouseEventInternal)pooled).triggeredByOS = pointerEventInternal.triggeredByOS;
					}
				}
			}
			pooled.elementTarget = (VisualElement)target;
			pooled.menu = menu;
			pooled.m_ContextualMenuManager = menuManager;
			return pooled;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0004BD38 File Offset: 0x00049F38
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0004BD4C File Offset: 0x00049F4C
		private void LocalInit()
		{
			this.menu = null;
			this.m_ContextualMenuManager = null;
			bool flag = this.triggerEvent != null;
			if (flag)
			{
				this.triggerEvent.Dispose();
				this.triggerEvent = null;
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0004BD8C File Offset: 0x00049F8C
		public ContextualMenuPopulateEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0004BDA0 File Offset: 0x00049FA0
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = this.menu.Count > 0 && this.m_ContextualMenuManager != null;
			if (flag)
			{
				this.menu.PrepareForDisplay(this.triggerEvent);
				this.m_ContextualMenuManager.DoDisplayMenu(this.menu, this.triggerEvent);
			}
			base.PostDispatch(panel);
		}

		// Token: 0x04000A54 RID: 2644
		private ContextualMenuManager m_ContextualMenuManager;
	}
}
