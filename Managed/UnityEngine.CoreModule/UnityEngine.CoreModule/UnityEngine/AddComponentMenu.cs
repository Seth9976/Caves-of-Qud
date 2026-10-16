using System;

namespace UnityEngine
{
	// Token: 0x0200025C RID: 604
	public sealed class AddComponentMenu : Attribute
	{
		// Token: 0x06001F57 RID: 8023 RVA: 0x0003C3F2 File Offset: 0x0003A5F2
		public AddComponentMenu(string menuName)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = 0;
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0003C40A File Offset: 0x0003A60A
		public AddComponentMenu(string menuName, int order)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = order;
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x0003C424 File Offset: 0x0003A624
		public string componentMenu
		{
			get
			{
				return this.m_AddComponentMenu;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x0003C43C File Offset: 0x0003A63C
		public int componentOrder
		{
			get
			{
				return this.m_Ordering;
			}
		}

		// Token: 0x0400093A RID: 2362
		private string m_AddComponentMenu;

		// Token: 0x0400093B RID: 2363
		private int m_Ordering;
	}
}
