using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028D RID: 653
	[EventCategory(EventCategory.PointerDown)]
	public sealed class PointerDownEvent : PointerEventBase<PointerDownEvent>
	{
		// Token: 0x06001316 RID: 4886 RVA: 0x0004E68C File Offset: 0x0004C88C
		static PointerDownEvent()
		{
			EventBase<PointerDownEvent>.SetCreateFunction(() => new PointerDownEvent());
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0004E6A5 File Offset: 0x0004C8A5
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0004E6B6 File Offset: 0x0004C8B6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			((IPointerEventInternal)this).triggeredByOS = true;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0004E6C9 File Offset: 0x0004C8C9
		public PointerDownEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0004E6DC File Offset: 0x0004C8DC
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag)
			{
				((IPointerEventInternal)this).compatibilityMouseEvent = MouseDownEvent.GetPooled(this);
			}
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0004E70C File Offset: 0x0004C90C
		protected internal override void PostDispatch(IPanel panel)
		{
			panel.focusController.SwitchFocusOnEvent(panel.focusController.GetLeafFocusedElement(), this);
			panel.dispatcher.m_ClickDetector.ProcessEvent<PointerDownEvent>(this);
			base.PostDispatch(panel);
		}
	}
}
