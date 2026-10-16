using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001338 RID: 4920
	[Serializable]
	public class Customs_Sharer : BaseSkill
	{
		// Token: 0x0600C035 RID: 49205 RVA: 0x00467238 File Offset: 0x00465438
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetNamingChanceEvent.ID || ID == GetNamingBestowalChanceEvent.ID || ID == PooledEvent<GetRitualSifrahSetupEvent>.ID || ID == PooledEvent<GetWaterRitualSellSecretBehaviorEvent>.ID;
		}

		// Token: 0x0600C036 RID: 49206 RVA: 0x00467266 File Offset: 0x00465466
		public override bool HandleEvent(GetNamingChanceEvent E)
		{
			E.PercentageBonus += 50.0;
			E.LinearBonus += 1.0;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C037 RID: 49207 RVA: 0x0046729B File Offset: 0x0046549B
		public override bool HandleEvent(GetNamingBestowalChanceEvent E)
		{
			E.PercentageBonus += 100;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C038 RID: 49208 RVA: 0x004672B3 File Offset: 0x004654B3
		public override bool HandleEvent(GetRitualSifrahSetupEvent E)
		{
			E.Turns++;
			E.Rating += 4;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C039 RID: 49209 RVA: 0x004672D8 File Offset: 0x004654D8
		public override bool HandleEvent(GetWaterRitualSellSecretBehaviorEvent E)
		{
			E.BonusReputationProvided += E.ReputationProvided / 2;
			if (E.IsGossip)
			{
				E.Message = "I sing to you of your people, =subject.waterRitualLiquid=-=pronouns.siblingTerm=.";
			}
			else
			{
				E.Message = "I sing to you of secrets, =subject.waterRitualLiquid=-=pronouns.siblingTerm=.";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C03A RID: 49210 RVA: 0x00467316 File Offset: 0x00465516
		public override bool AddSkill(GameObject GO)
		{
			if (GO.IsPlayer())
			{
				RitualSifrah.AwardInsight();
			}
			return base.AddSkill(GO);
		}
	}
}
