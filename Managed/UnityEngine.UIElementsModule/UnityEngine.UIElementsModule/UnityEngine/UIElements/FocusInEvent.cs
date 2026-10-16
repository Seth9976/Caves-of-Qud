using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000245 RID: 581
	public class FocusInEvent : FocusEventBase<FocusInEvent>
	{
		// Token: 0x06001148 RID: 4424 RVA: 0x0004A60A File Offset: 0x0004880A
		static FocusInEvent()
		{
			EventBase<FocusInEvent>.SetCreateFunction(() => new FocusInEvent());
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0004A623 File Offset: 0x00048823
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0004A573 File Offset: 0x00048773
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0004A634 File Offset: 0x00048834
		public FocusInEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0004A645 File Offset: 0x00048845
		protected internal override void PostDispatch(IPanel panel)
		{
			base.focusController.ProcessPendingFocusChange(base.elementTarget);
			base.PostDispatch(panel);
		}
	}
}
