using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000295 RID: 661
	public sealed class PointerCancelEvent : PointerEventBase<PointerCancelEvent>
	{
		// Token: 0x0600133D RID: 4925 RVA: 0x0004EA1C File Offset: 0x0004CC1C
		static PointerCancelEvent()
		{
			EventBase<PointerCancelEvent>.SetCreateFunction(() => new PointerCancelEvent());
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0004EA35 File Offset: 0x0004CC35
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0004E6B6 File Offset: 0x0004C8B6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			((IPointerEventInternal)this).triggeredByOS = true;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0004EA46 File Offset: 0x0004CC46
		public PointerCancelEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0004EA58 File Offset: 0x0004CC58
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag)
			{
				((IPointerEventInternal)this).compatibilityMouseEvent = MouseUpEvent.GetPooled(this);
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0004EA88 File Offset: 0x0004CC88
		protected internal override void PostDispatch(IPanel panel)
		{
			panel.dispatcher.m_ClickDetector.ProcessEvent<PointerCancelEvent>(this);
			bool flag = PointerType.IsDirectManipulationDevice(base.pointerType);
			if (flag)
			{
				panel.ReleasePointer(base.pointerId);
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.ClearCachedElementUnderPointer(base.pointerId, this);
				}
			}
			base.PostDispatch(panel);
			panel.ActivateCompatibilityMouseEvents(base.pointerId);
		}
	}
}
