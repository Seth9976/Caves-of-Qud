using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F46 RID: 3910
	[Serializable]
	public class CyberneticsSocialCoprocessor : IPoweredPart
	{
		// Token: 0x06009BF9 RID: 39929 RVA: 0x00388EB5 File Offset: 0x003870B5
		public CyberneticsSocialCoprocessor()
		{
			this.WorksOnImplantee = true;
			this.ChargeUse = 0;
			this.NameForStatus = "SocialCoprocessor";
		}

		// Token: 0x06009BFA RID: 39930 RVA: 0x00388ED6 File Offset: 0x003870D6
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009BFB RID: 39931 RVA: 0x00388ED9 File Offset: 0x003870D9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCompanionLimitEvent>.ID || ID == PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID || ID == PooledEvent<GetSocialSifrahSetupEvent>.ID || ID == PooledEvent<GetWaterRitualReputationAmountEvent>.ID || ID == PooledEvent<ReputationChangeEvent>.ID || ID == PooledEvent<WaterRitualStartEvent>.ID;
		}

		// Token: 0x06009BFC RID: 39932 RVA: 0x00388F18 File Offset: 0x00387118
		public override bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			if (this.ParentObject.Implantee != null)
			{
				int waterRitualReputationBonus = this.GetWaterRitualReputationBonus();
				int proselytizeLimitBonus = this.GetProselytizeLimitBonus();
				E.Description = string.Concat(new string[]
				{
					"Whenever you perform the water ritual with a new creature, you gain an extra ",
					waterRitualReputationBonus.ToString(),
					" reputation. If you install this implant after you treat with a creature for the first time, you gain ",
					waterRitualReputationBonus.ToString(),
					" reputation the next time you treat with them.\nReputation costs in the water ritual are reduced by ",
					this.GetWaterRitualReputationCostReductionPercentage().ToString(),
					"%.\nYou may Proselytize ",
					proselytizeLimitBonus.Things("additional creature", null),
					".\nCompute power on the local lattice increases this implant's effectiveness."
				});
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BFD RID: 39933 RVA: 0x00388FB8 File Offset: 0x003871B8
		public override bool HandleEvent(WaterRitualStartEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Actor) && E.Actor.IsPlayer())
			{
				int num = (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) ? this.GetWaterRitualReputationBonus() : 0);
				int num2 = num - E.Record.LastSocialCoprocessorBonus;
				if (num2 > 0)
				{
					E.Record.LastSocialCoprocessorBonus = num;
					The.Game.PlayerReputation.Modify(E.Record.faction, num2, "WaterRitualSocialCoprocessorAward", null, null, false, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BFE RID: 39934 RVA: 0x00389060 File Offset: 0x00387260
		public override bool HandleEvent(GetWaterRitualReputationAmountEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Actor) && E.Actor.IsPlayer() && E.Faction == E.Record.faction)
			{
				int num = (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) ? this.GetWaterRitualReputationBonus() : 0);
				int num2 = num - E.Record.LastSocialCoprocessorBonus;
				if (num2 > 0)
				{
					E.Record.LastSocialCoprocessorBonus = num;
					E.Amount += num2;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BFF RID: 39935 RVA: 0x00389108 File Offset: 0x00387308
		public override bool HandleEvent(ReputationChangeEvent E)
		{
			if (E.BaseAmount < 0 && !E.Transient && E.Type == "WaterRitualUse" && base.IsReady(!E.Prospective, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Amount = E.Amount * (100 - this.GetWaterRitualReputationCostReductionPercentage()) / 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C00 RID: 39936 RVA: 0x00389188 File Offset: 0x00387388
		public override bool HandleEvent(GetCompanionLimitEvent E)
		{
			if (E.Means == "Proselytize" && E.Actor == this.ParentObject.Implantee && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Limit += this.GetProselytizeLimitBonus();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C01 RID: 39937 RVA: 0x003891FC File Offset: 0x003873FC
		public override bool HandleEvent(GetSocialSifrahSetupEvent E)
		{
			E.Rating += 5;
			E.Turns++;
			return base.HandleEvent(E);
		}

		// Token: 0x06009C02 RID: 39938 RVA: 0x00389221 File Offset: 0x00387421
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009C03 RID: 39939 RVA: 0x00389224 File Offset: 0x00387424
		public int GetWaterRitualReputationBonus()
		{
			return GetAvailableComputePowerEvent.AdjustUp(this.ParentObject.Implantee, CyberneticsSocialCoprocessor.BASE_WATER_RITUAL_REPUTATION_BONUS);
		}

		// Token: 0x06009C04 RID: 39940 RVA: 0x0038923B File Offset: 0x0038743B
		public int GetWaterRitualReputationCostReductionPercentage()
		{
			return GetAvailableComputePowerEvent.AdjustUp(this.ParentObject.Implantee, CyberneticsSocialCoprocessor.BASE_REP_COST_REDUCTION_PERCENTAGE);
		}

		// Token: 0x06009C05 RID: 39941 RVA: 0x00389252 File Offset: 0x00387452
		public int GetProselytizeLimitBonus()
		{
			return GetAvailableComputePowerEvent.AdjustUp(this.ParentObject.Implantee, CyberneticsSocialCoprocessor.BASE_PROSELYTIZE_LIMIT_BONUS);
		}

		// Token: 0x04004099 RID: 16537
		public static readonly int BASE_WATER_RITUAL_REPUTATION_BONUS = RuleSettings.REPUTATION_BASE_UNIT / 2;

		// Token: 0x0400409A RID: 16538
		public static readonly int BASE_REP_COST_REDUCTION_PERCENTAGE = 20;

		// Token: 0x0400409B RID: 16539
		public static readonly int BASE_PROSELYTIZE_LIMIT_BONUS = 1;
	}
}
