using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000263 RID: 611
	[EventCategory(EventCategory.EnterLeave)]
	public class MouseEnterEvent : MouseEventBase<MouseEnterEvent>
	{
		// Token: 0x0600120B RID: 4619 RVA: 0x0004B88C File Offset: 0x00049A8C
		static MouseEnterEvent()
		{
			EventBase<MouseEnterEvent>.SetCreateFunction(() => new MouseEnterEvent());
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0004B8A5 File Offset: 0x00049AA5
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0004B8B6 File Offset: 0x00049AB6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0004B8C1 File Offset: 0x00049AC1
		public MouseEnterEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0004B8D2 File Offset: 0x00049AD2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}
	}
}
