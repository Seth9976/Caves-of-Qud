using System;

namespace XRL.World
{
	// Token: 0x02000929 RID: 2345
	[Serializable]
	public class DynamicQuestRewardElement_VillageZeroLoot : DynamicQuestRewardElement
	{
		// Token: 0x06005A09 RID: 23049 RVA: 0x001B49A3 File Offset: 0x001B2BA3
		public override string getRewardConversationType()
		{
			return null;
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x001B49A6 File Offset: 0x001B2BA6
		public override string getRewardAcceptQuestText()
		{
			return "Accept Quest - reward: random";
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x001B49AD File Offset: 0x001B2BAD
		public override void award()
		{
		}
	}
}
