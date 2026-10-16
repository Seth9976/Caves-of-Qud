using System;

namespace XRL.World
{
	// Token: 0x02000928 RID: 2344
	[Serializable]
	public class DynamicQuestRewardElement_VillageZeroMainQuestHook : DynamicQuestRewardElement
	{
		// Token: 0x06005A05 RID: 23045 RVA: 0x001B498B File Offset: 0x001B2B8B
		public override string getRewardConversationType()
		{
			return "VillageZeroMainQuest";
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x001B4992 File Offset: 0x001B2B92
		public override string getRewardAcceptQuestText()
		{
			return "Accept Quest - reward: recoiler and disk";
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x001B4999 File Offset: 0x001B2B99
		public override void award()
		{
		}
	}
}
