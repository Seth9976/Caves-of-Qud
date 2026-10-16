using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000710 RID: 1808
	[EventCategory(EventCategory.EnterLeave)]
	public class PointerOverLinkTagEvent : PointerEventBase<PointerOverLinkTagEvent>
	{
		// Token: 0x0600343D RID: 13373 RVA: 0x000CC1EC File Offset: 0x000CA3EC
		static PointerOverLinkTagEvent()
		{
			EventBase<PointerOverLinkTagEvent>.SetCreateFunction(() => new PointerOverLinkTagEvent());
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x000CC205 File Offset: 0x000CA405
		// (set) Token: 0x0600343F RID: 13375 RVA: 0x000CC20D File Offset: 0x000CA40D
		public string linkID { get; private set; }

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06003440 RID: 13376 RVA: 0x000CC216 File Offset: 0x000CA416
		// (set) Token: 0x06003441 RID: 13377 RVA: 0x000CC21E File Offset: 0x000CA41E
		public string linkText { get; private set; }

		// Token: 0x06003442 RID: 13378 RVA: 0x000CC227 File Offset: 0x000CA427
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x0004A573 File Offset: 0x00048773
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000CC238 File Offset: 0x000CA438
		public static PointerOverLinkTagEvent GetPooled(IPointerEvent evt, string linkID, string linkText)
		{
			PointerOverLinkTagEvent pooled = PointerEventBase<PointerOverLinkTagEvent>.GetPooled(evt);
			pooled.linkID = linkID;
			pooled.linkText = linkText;
			return pooled;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000CC262 File Offset: 0x000CA462
		public PointerOverLinkTagEvent()
		{
			this.LocalInit();
		}
	}
}
