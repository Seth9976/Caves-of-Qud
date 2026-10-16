using System;

namespace XRL.World
{
	// Token: 0x02000924 RID: 2340
	[Serializable]
	public abstract class DynamicQuestRewardElement : IComposite
	{
		// Token: 0x060059F1 RID: 23025
		public abstract void award();

		// Token: 0x060059F2 RID: 23026
		public abstract string getRewardConversationType();

		// Token: 0x060059F3 RID: 23027
		public abstract string getRewardAcceptQuestText();
	}
}
