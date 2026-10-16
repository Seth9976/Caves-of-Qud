using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200026B RID: 619
	[EventCategory(EventCategory.EnterLeaveWindow)]
	public class MouseEnterWindowEvent : MouseEventBase<MouseEnterWindowEvent>
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x0004B9E2 File Offset: 0x00049BE2
		static MouseEnterWindowEvent()
		{
			EventBase<MouseEnterWindowEvent>.SetCreateFunction(() => new MouseEnterWindowEvent());
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0004B9FB File Offset: 0x00049BFB
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0004BA0C File Offset: 0x00049C0C
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0004BA17 File Offset: 0x00049C17
		public MouseEnterWindowEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0004BA28 File Offset: 0x00049C28
		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase eventBase = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = eventBase == null;
			if (flag)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0004BA66 File Offset: 0x00049C66
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToElementUnderPointerOrPanelRoot(this, panel, PointerId.mousePointerId, base.mousePosition);
		}
	}
}
