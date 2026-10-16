using System;
using XRL.Core;

namespace XRL.World
{
	// Token: 0x02000927 RID: 2343
	[Serializable]
	public class DynamicQuestRewardElement_Quest : DynamicQuestRewardElement
	{
		// Token: 0x060059FF RID: 23039 RVA: 0x001B494B File Offset: 0x001B2B4B
		public DynamicQuestRewardElement_Quest()
		{
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x001B4953 File Offset: 0x001B2B53
		public DynamicQuestRewardElement_Quest(string questID)
		{
			this.questID = questID;
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x001B4962 File Offset: 0x001B2B62
		public override string getRewardConversationType()
		{
			return null;
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x001B4965 File Offset: 0x001B2B65
		public override string getRewardAcceptQuestText()
		{
			return null;
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x001B4968 File Offset: 0x001B2B68
		public override void award()
		{
			XRLCore.Core.Game.StartQuest(this.questID, null, null, null);
		}

		// Token: 0x04002C96 RID: 11414
		public string questID;
	}
}
