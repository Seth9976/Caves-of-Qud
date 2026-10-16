using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000714 RID: 1812
	[EventCategory(EventCategory.EnterLeave)]
	public class PointerOutLinkTagEvent : PointerEventBase<PointerOutLinkTagEvent>
	{
		// Token: 0x06003455 RID: 13397 RVA: 0x000CC322 File Offset: 0x000CA522
		static PointerOutLinkTagEvent()
		{
			EventBase<PointerOutLinkTagEvent>.SetCreateFunction(() => new PointerOutLinkTagEvent());
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000CC33B File Offset: 0x000CA53B
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x0004A573 File Offset: 0x00048773
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000CC34C File Offset: 0x000CA54C
		public static PointerOutLinkTagEvent GetPooled(IPointerEvent evt, string linkID)
		{
			return PointerEventBase<PointerOutLinkTagEvent>.GetPooled(evt);
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000CC366 File Offset: 0x000CA566
		public PointerOutLinkTagEvent()
		{
			this.LocalInit();
		}
	}
}
