using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000297 RID: 663
	public sealed class ClickEvent : PointerEventBase<ClickEvent>
	{
		// Token: 0x06001346 RID: 4934 RVA: 0x0004EB08 File Offset: 0x0004CD08
		static ClickEvent()
		{
			EventBase<ClickEvent>.SetCreateFunction(() => new ClickEvent());
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0004EB21 File Offset: 0x0004CD21
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0004B551 File Offset: 0x00049751
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0004EB32 File Offset: 0x0004CD32
		public ClickEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0004EB44 File Offset: 0x0004CD44
		internal static ClickEvent GetPooled(IPointerEvent pointerEvent, int clickCount)
		{
			ClickEvent pooled = PointerEventBase<ClickEvent>.GetPooled(pointerEvent);
			pooled.clickCount = clickCount;
			return pooled;
		}
	}
}
