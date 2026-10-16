using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200026D RID: 621
	[EventCategory(EventCategory.EnterLeaveWindow)]
	public class MouseLeaveWindowEvent : MouseEventBase<MouseLeaveWindowEvent>
	{
		// Token: 0x06001232 RID: 4658 RVA: 0x0004BA8F File Offset: 0x00049C8F
		static MouseLeaveWindowEvent()
		{
			EventBase<MouseLeaveWindowEvent>.SetCreateFunction(() => new MouseLeaveWindowEvent());
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0004BAA8 File Offset: 0x00049CA8
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0004BA0C File Offset: 0x00049C0C
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0004BAB9 File Offset: 0x00049CB9
		public MouseLeaveWindowEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0004BACC File Offset: 0x00049CCC
		public new static MouseLeaveWindowEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.ReleaseAllButtons(PointerId.mousePointerId);
			}
			return MouseEventBase<MouseLeaveWindowEvent>.GetPooled(systemEvent);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0004BAF8 File Offset: 0x00049CF8
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = base.pressedButtons == 0;
			if (flag)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.ClearCachedElementUnderPointer(PointerId.mousePointerId, this);
				}
			}
			EventBase eventBase = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag2 = eventBase == null;
			if (flag2)
			{
				BaseVisualElementPanel baseVisualElementPanel2 = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel2 != null)
				{
					baseVisualElementPanel2.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0004BB5D File Offset: 0x00049D5D
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToCachedElementUnderPointerOrPanelRoot(this, panel, PointerId.mousePointerId, base.mousePosition);
		}
	}
}
