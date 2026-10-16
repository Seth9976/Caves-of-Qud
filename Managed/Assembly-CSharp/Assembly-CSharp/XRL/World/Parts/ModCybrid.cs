using System;

namespace XRL.World.Parts
{
	// Token: 0x0200112F RID: 4399
	[Serializable]
	public class ModCybrid : IModification
	{
		// Token: 0x0600AE27 RID: 44583 RVA: 0x00405DDE File Offset: 0x00403FDE
		public ModCybrid()
		{
		}

		// Token: 0x0600AE28 RID: 44584 RVA: 0x00405DE6 File Offset: 0x00403FE6
		public ModCybrid(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AE29 RID: 44585 RVA: 0x00405DEF File Offset: 0x00403FEF
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "BiomechanicalAdapter";
		}

		// Token: 0x0600AE2A RID: 44586 RVA: 0x00405E04 File Offset: 0x00404004
		public override bool ModificationApplicable(GameObject Object)
		{
			ElectricalPowerTransmission part = Object.GetPart<ElectricalPowerTransmission>();
			return part != null && part.IsConsumer;
		}

		// Token: 0x0600AE2B RID: 44587 RVA: 0x00405E28 File Offset: 0x00404028
		public override void ApplyModification(GameObject Object)
		{
			ElectricalPowerTransmission part = Object.GetPart<ElectricalPowerTransmission>();
			int num = ((part != null) ? (part.ChargeRate * 2 / 5) : (this.Tier * this.Tier * 50));
			BiomechanicalPowerTransmission biomechanicalPowerTransmission = Object.RequirePart<BiomechanicalPowerTransmission>(false);
			if (biomechanicalPowerTransmission.ChargeRate < num)
			{
				biomechanicalPowerTransmission.ChargeRate = num;
			}
			if (part == null || part.IsConsumer)
			{
				biomechanicalPowerTransmission.IsConsumer = true;
			}
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600AE2C RID: 44588 RVA: 0x00405E8F File Offset: 0x0040408F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AE2D RID: 44589 RVA: 0x00405EAD File Offset: 0x004040AD
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{biomech|cybrid}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE2E RID: 44590 RVA: 0x00405ED7 File Offset: 0x004040D7
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModCybrid.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE2F RID: 44591 RVA: 0x00405EF7 File Offset: 0x004040F7
		public static string GetDescription(int Tier)
		{
			return "Cybrid: Can draw power from biomechanical power transmission systems in addition to an electrical power grid.";
		}
	}
}
