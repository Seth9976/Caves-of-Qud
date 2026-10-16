using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001360 RID: 4960
	[Serializable]
	public class Persuasion_SnakeOiler : BaseSkill
	{
		// Token: 0x0600C128 RID: 49448 RVA: 0x0046AABC File Offset: 0x00468CBC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetTradePerformanceEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C129 RID: 49449 RVA: 0x0046AADA File Offset: 0x00468CDA
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("jewels", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C12A RID: 49450 RVA: 0x0046AAFD File Offset: 0x00468CFD
		public override bool HandleEvent(GetTradePerformanceEvent E)
		{
			E.LinearAdjustment += 2.0;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C12B RID: 49451 RVA: 0x0046AB1C File Offset: 0x00468D1C
		public override bool AddSkill(GameObject GO)
		{
			if (GO.IsPlayer())
			{
				SocialSifrah.AwardInsight();
			}
			return base.AddSkill(GO);
		}
	}
}
