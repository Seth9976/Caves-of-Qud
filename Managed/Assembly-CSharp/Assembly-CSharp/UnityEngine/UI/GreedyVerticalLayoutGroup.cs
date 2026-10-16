using System;

namespace UnityEngine.UI
{
	// Token: 0x020003EA RID: 1002
	public class GreedyVerticalLayoutGroup : VerticalLayoutGroup
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x000BF19A File Offset: 0x000BD39A
		public override float minHeight
		{
			get
			{
				return this.preferredHeight;
			}
		}
	}
}
