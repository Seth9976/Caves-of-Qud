using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029B RID: 667
	[EventCategory(EventCategory.EnterLeave)]
	public sealed class PointerLeaveEvent : PointerEventBase<PointerLeaveEvent>
	{
		// Token: 0x06001357 RID: 4951 RVA: 0x0004EBFC File Offset: 0x0004CDFC
		static PointerLeaveEvent()
		{
			EventBase<PointerLeaveEvent>.SetCreateFunction(() => new PointerLeaveEvent());
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0004EC15 File Offset: 0x0004CE15
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0004B8B6 File Offset: 0x00049AB6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0004EC26 File Offset: 0x0004CE26
		public PointerLeaveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004B8D2 File Offset: 0x00049AD2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0004EC37 File Offset: 0x0004CE37
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.containedPointerIds &= ~(1 << base.pointerId);
			base.elementTarget.UpdateHoverPseudoState();
		}
	}
}
