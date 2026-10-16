using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013A2 RID: 5026
	[Serializable]
	public class Survival_Trailblazer : BaseSkill
	{
		// Token: 0x0600C248 RID: 49736 RVA: 0x0046F119 File Offset: 0x0046D319
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetLostChanceEvent.ID || ID == TravelSpeedEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C249 RID: 49737 RVA: 0x0046F13F File Offset: 0x0046D33F
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C24A RID: 49738 RVA: 0x0046F162 File Offset: 0x0046D362
		public override bool HandleEvent(GetLostChanceEvent E)
		{
			E.PercentageBonus += 17;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C24B RID: 49739 RVA: 0x0046F17A File Offset: 0x0046D37A
		public override bool HandleEvent(TravelSpeedEvent E)
		{
			E.PercentageBonus += 100;
			return base.HandleEvent(E);
		}
	}
}
