using System;

namespace XRL.World
{
	// Token: 0x02000926 RID: 2342
	[Serializable]
	public class DynamicQuestRewardElement_Reputation : DynamicQuestRewardElement
	{
		// Token: 0x060059FA RID: 23034 RVA: 0x001B4900 File Offset: 0x001B2B00
		public DynamicQuestRewardElement_Reputation()
		{
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x001B4908 File Offset: 0x001B2B08
		public DynamicQuestRewardElement_Reputation(string faction, int amount)
			: this()
		{
			this.faction = faction;
			this.amount = amount;
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x001B491E File Offset: 0x001B2B1E
		public override string getRewardConversationType()
		{
			return null;
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x001B4921 File Offset: 0x001B2B21
		public override string getRewardAcceptQuestText()
		{
			return null;
		}

		// Token: 0x060059FE RID: 23038 RVA: 0x001B4924 File Offset: 0x001B2B24
		public override void award()
		{
			The.Game.PlayerReputation.Modify(this.faction, this.amount, "Quest", null, null, false, false);
		}

		// Token: 0x04002C94 RID: 11412
		public string faction;

		// Token: 0x04002C95 RID: 11413
		public int amount;
	}
}
