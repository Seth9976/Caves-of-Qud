using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028F RID: 655
	[EventCategory(EventCategory.PointerMove)]
	public sealed class PointerMoveEvent : PointerEventBase<PointerMoveEvent>
	{
		// Token: 0x0600131F RID: 4895 RVA: 0x0004E754 File Offset: 0x0004C954
		static PointerMoveEvent()
		{
			EventBase<PointerMoveEvent>.SetCreateFunction(() => new PointerMoveEvent());
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0004E76D File Offset: 0x0004C96D
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x0004E775 File Offset: 0x0004C975
		internal bool isHandledByDraggable { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0004E77E File Offset: 0x0004C97E
		internal bool isPointerUpDown
		{
			get
			{
				return base.button >= 0;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0004E78C File Offset: 0x0004C98C
		internal bool isPointerDown
		{
			get
			{
				return base.button >= 0 && (base.pressedButtons & (1 << base.button)) != 0;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0004E7AF File Offset: 0x0004C9AF
		internal bool isPointerUp
		{
			get
			{
				return base.button >= 0 && (base.pressedButtons & (1 << base.button)) == 0;
			}
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0004E7D2 File Offset: 0x0004C9D2
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0004E7E3 File Offset: 0x0004C9E3
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			((IPointerEventInternal)this).triggeredByOS = true;
			this.isHandledByDraggable = false;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0004E7FE File Offset: 0x0004C9FE
		public PointerMoveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0004E810 File Offset: 0x0004CA10
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag)
			{
				bool flag2 = base.imguiEvent != null && base.imguiEvent.rawType == EventType.MouseDown;
				if (flag2)
				{
					((IPointerEventInternal)this).compatibilityMouseEvent = MouseDownEvent.GetPooled(this);
				}
				else
				{
					bool flag3 = base.imguiEvent != null && base.imguiEvent.rawType == EventType.MouseUp;
					if (flag3)
					{
						((IPointerEventInternal)this).compatibilityMouseEvent = MouseUpEvent.GetPooled(this);
					}
					else
					{
						((IPointerEventInternal)this).compatibilityMouseEvent = MouseMoveEvent.GetPooled(this);
					}
				}
			}
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004E89E File Offset: 0x0004CA9E
		protected internal override void PostDispatch(IPanel panel)
		{
			panel.dispatcher.m_ClickDetector.ProcessEvent<PointerMoveEvent>(this);
			base.PostDispatch(panel);
		}
	}
}
