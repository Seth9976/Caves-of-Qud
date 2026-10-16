using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000293 RID: 659
	public sealed class PointerUpEvent : PointerEventBase<PointerUpEvent>
	{
		// Token: 0x06001334 RID: 4916 RVA: 0x0004E92F File Offset: 0x0004CB2F
		static PointerUpEvent()
		{
			EventBase<PointerUpEvent>.SetCreateFunction(() => new PointerUpEvent());
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0004E948 File Offset: 0x0004CB48
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0004E6B6 File Offset: 0x0004C8B6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			((IPointerEventInternal)this).triggeredByOS = true;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0004E959 File Offset: 0x0004CB59
		public PointerUpEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0004E96C File Offset: 0x0004CB6C
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag)
			{
				((IPointerEventInternal)this).compatibilityMouseEvent = MouseUpEvent.GetPooled(this);
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0004E99C File Offset: 0x0004CB9C
		protected internal override void PostDispatch(IPanel panel)
		{
			panel.dispatcher.m_ClickDetector.ProcessEvent<PointerUpEvent>(this);
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
