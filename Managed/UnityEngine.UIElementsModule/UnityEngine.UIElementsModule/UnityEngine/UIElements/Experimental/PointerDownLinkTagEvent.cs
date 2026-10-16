using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000716 RID: 1814
	public sealed class PointerDownLinkTagEvent : PointerEventBase<PointerDownLinkTagEvent>
	{
		// Token: 0x0600345D RID: 13405 RVA: 0x000CC38A File Offset: 0x000CA58A
		static PointerDownLinkTagEvent()
		{
			EventBase<PointerDownLinkTagEvent>.SetCreateFunction(() => new PointerDownLinkTagEvent());
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x000CC3A3 File Offset: 0x000CA5A3
		// (set) Token: 0x0600345F RID: 13407 RVA: 0x000CC3AB File Offset: 0x000CA5AB
		public string linkID { get; private set; }

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x000CC3B4 File Offset: 0x000CA5B4
		// (set) Token: 0x06003461 RID: 13409 RVA: 0x000CC3BC File Offset: 0x000CA5BC
		public string linkText { get; private set; }

		// Token: 0x06003462 RID: 13410 RVA: 0x000CC3C5 File Offset: 0x000CA5C5
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x0004A573 File Offset: 0x00048773
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000CC3D8 File Offset: 0x000CA5D8
		public static PointerDownLinkTagEvent GetPooled(IPointerEvent evt, string linkID, string linkText)
		{
			PointerDownLinkTagEvent pooled = PointerEventBase<PointerDownLinkTagEvent>.GetPooled(evt);
			pooled.linkID = linkID;
			pooled.linkText = linkText;
			return pooled;
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000CC402 File Offset: 0x000CA602
		public PointerDownLinkTagEvent()
		{
			this.LocalInit();
		}
	}
}
