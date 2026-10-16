using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000718 RID: 1816
	public class PointerUpLinkTagEvent : PointerEventBase<PointerUpLinkTagEvent>
	{
		// Token: 0x06003469 RID: 13417 RVA: 0x000CC426 File Offset: 0x000CA626
		static PointerUpLinkTagEvent()
		{
			EventBase<PointerUpLinkTagEvent>.SetCreateFunction(() => new PointerUpLinkTagEvent());
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000CC43F File Offset: 0x000CA63F
		// (set) Token: 0x0600346B RID: 13419 RVA: 0x000CC447 File Offset: 0x000CA647
		public string linkID { get; private set; }

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000CC450 File Offset: 0x000CA650
		// (set) Token: 0x0600346D RID: 13421 RVA: 0x000CC458 File Offset: 0x000CA658
		public string linkText { get; private set; }

		// Token: 0x0600346E RID: 13422 RVA: 0x000CC461 File Offset: 0x000CA661
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x0004A573 File Offset: 0x00048773
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000CC474 File Offset: 0x000CA674
		public static PointerUpLinkTagEvent GetPooled(IPointerEvent evt, string linkID, string linkText)
		{
			PointerUpLinkTagEvent pooled = PointerEventBase<PointerUpLinkTagEvent>.GetPooled(evt);
			pooled.linkID = linkID;
			pooled.linkText = linkText;
			return pooled;
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000CC49E File Offset: 0x000CA69E
		public PointerUpLinkTagEvent()
		{
			this.LocalInit();
		}
	}
}
