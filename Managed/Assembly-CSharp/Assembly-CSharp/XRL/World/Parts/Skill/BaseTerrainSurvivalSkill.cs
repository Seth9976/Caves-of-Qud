using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001399 RID: 5017
	[Serializable]
	public abstract class BaseTerrainSurvivalSkill : BaseSkill
	{
		// Token: 0x0600C231 RID: 49713
		public abstract string GetTerrainName();

		// Token: 0x0600C232 RID: 49714 RVA: 0x0046EFC7 File Offset: 0x0046D1C7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EncounterChanceEvent.ID || ID == GetLostChanceEvent.ID || ID == TravelSpeedEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C233 RID: 49715 RVA: 0x0046EFF5 File Offset: 0x0046D1F5
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C234 RID: 49716 RVA: 0x0046F018 File Offset: 0x0046D218
		public override bool HandleEvent(EncounterChanceEvent E)
		{
			if (E.TravelClass == base.Name)
			{
				E.PercentageBonus += 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C235 RID: 49717 RVA: 0x0046F043 File Offset: 0x0046D243
		public override bool HandleEvent(GetLostChanceEvent E)
		{
			if (E.TravelClass == base.Name)
			{
				E.PercentageBonus += 95;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C236 RID: 49718 RVA: 0x0046F06E File Offset: 0x0046D26E
		public override bool HandleEvent(TravelSpeedEvent E)
		{
			if (E.TravelClass == base.Name)
			{
				E.PercentageBonus += 100;
			}
			return base.HandleEvent(E);
		}
	}
}
