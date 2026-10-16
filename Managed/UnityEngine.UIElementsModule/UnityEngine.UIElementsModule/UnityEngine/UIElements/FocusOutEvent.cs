using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000241 RID: 577
	public class FocusOutEvent : FocusEventBase<FocusOutEvent>
	{
		// Token: 0x0600113B RID: 4411 RVA: 0x0004A549 File Offset: 0x00048749
		static FocusOutEvent()
		{
			EventBase<FocusOutEvent>.SetCreateFunction(() => new FocusOutEvent());
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0004A562 File Offset: 0x00048762
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0004A573 File Offset: 0x00048773
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0004A57E File Offset: 0x0004877E
		public FocusOutEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0004A590 File Offset: 0x00048790
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = base.relatedTarget == null;
			if (flag)
			{
				base.focusController.ProcessPendingFocusChange(null);
			}
			base.PostDispatch(panel);
		}
	}
}
