using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000299 RID: 665
	[EventCategory(EventCategory.EnterLeave)]
	public sealed class PointerEnterEvent : PointerEventBase<PointerEnterEvent>
	{
		// Token: 0x0600134E RID: 4942 RVA: 0x0004EB79 File Offset: 0x0004CD79
		static PointerEnterEvent()
		{
			EventBase<PointerEnterEvent>.SetCreateFunction(() => new PointerEnterEvent());
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0004EB92 File Offset: 0x0004CD92
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0004B8B6 File Offset: 0x00049AB6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0004EBA3 File Offset: 0x0004CDA3
		public PointerEnterEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0004B8D2 File Offset: 0x00049AD2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0004EBB4 File Offset: 0x0004CDB4
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.containedPointerIds |= 1 << base.pointerId;
			base.elementTarget.UpdateHoverPseudoState();
		}
	}
}
