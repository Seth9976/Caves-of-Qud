using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000712 RID: 1810
	[EventCategory(EventCategory.PointerMove)]
	public class PointerMoveLinkTagEvent : PointerEventBase<PointerMoveLinkTagEvent>
	{
		// Token: 0x06003449 RID: 13385 RVA: 0x000CC286 File Offset: 0x000CA486
		static PointerMoveLinkTagEvent()
		{
			EventBase<PointerMoveLinkTagEvent>.SetCreateFunction(() => new PointerMoveLinkTagEvent());
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x000CC29F File Offset: 0x000CA49F
		// (set) Token: 0x0600344B RID: 13387 RVA: 0x000CC2A7 File Offset: 0x000CA4A7
		public string linkID { get; private set; }

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x000CC2B0 File Offset: 0x000CA4B0
		// (set) Token: 0x0600344D RID: 13389 RVA: 0x000CC2B8 File Offset: 0x000CA4B8
		public string linkText { get; private set; }

		// Token: 0x0600344E RID: 13390 RVA: 0x000CC2C1 File Offset: 0x000CA4C1
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x0004A573 File Offset: 0x00048773
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000CC2D4 File Offset: 0x000CA4D4
		public static PointerMoveLinkTagEvent GetPooled(IPointerEvent evt, string linkID, string linkText)
		{
			PointerMoveLinkTagEvent pooled = PointerEventBase<PointerMoveLinkTagEvent>.GetPooled(evt);
			pooled.linkID = linkID;
			pooled.linkText = linkText;
			return pooled;
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000CC2FE File Offset: 0x000CA4FE
		public PointerMoveLinkTagEvent()
		{
			this.LocalInit();
		}
	}
}
