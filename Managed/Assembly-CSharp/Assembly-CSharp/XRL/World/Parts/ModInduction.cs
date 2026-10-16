using System;

namespace XRL.World.Parts
{
	// Token: 0x02001148 RID: 4424
	[Serializable]
	public class ModInduction : IModification
	{
		// Token: 0x0600AF91 RID: 44945 RVA: 0x0040B642 File Offset: 0x00409842
		public ModInduction()
		{
		}

		// Token: 0x0600AF92 RID: 44946 RVA: 0x0040B64A File Offset: 0x0040984A
		public ModInduction(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF93 RID: 44947 RVA: 0x0040B653 File Offset: 0x00409853
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AF94 RID: 44948 RVA: 0x0040B65C File Offset: 0x0040985C
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<EnergyCell>();
		}

		// Token: 0x0600AF95 RID: 44949 RVA: 0x0040B664 File Offset: 0x00409864
		public override void ApplyModification(GameObject Object)
		{
			InductionChargeReceiver inductionChargeReceiver = Object.GetPart<InductionChargeReceiver>();
			if (inductionChargeReceiver == null)
			{
				inductionChargeReceiver = new InductionChargeReceiver();
				if (this.Tier > 1)
				{
					inductionChargeReceiver.ChargeRate *= this.Tier;
				}
				Object.AddPart<InductionChargeReceiver>(inductionChargeReceiver, true, false);
			}
			else if (this.Tier > 0 && inductionChargeReceiver.ChargeRate < this.Tier * 10)
			{
				inductionChargeReceiver.ChargeRate = this.Tier * 10;
			}
			IntegralRecharger part = Object.GetPart<IntegralRecharger>();
			if (part == null)
			{
				Object.AddPart<IntegralRecharger>(new IntegralRecharger
				{
					ChargeRate = inductionChargeReceiver.ChargeRate
				}, true, false);
			}
			else if (part.ChargeRate != 0 && part.ChargeRate < inductionChargeReceiver.ChargeRate)
			{
				part.ChargeRate = inductionChargeReceiver.ChargeRate;
			}
			base.IncreaseDifficultyAndComplexity(4, 1, null);
		}

		// Token: 0x0600AF96 RID: 44950 RVA: 0x0040B725 File Offset: 0x00409925
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AF97 RID: 44951 RVA: 0x0040B743 File Offset: 0x00409943
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{Y|induction}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF98 RID: 44952 RVA: 0x0040B76D File Offset: 0x0040996D
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModInduction.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF99 RID: 44953 RVA: 0x0040B78D File Offset: 0x0040998D
		public static string GetDescription(int Tier)
		{
			return "Induction: This item may be charged at induction charging stations.";
		}
	}
}
