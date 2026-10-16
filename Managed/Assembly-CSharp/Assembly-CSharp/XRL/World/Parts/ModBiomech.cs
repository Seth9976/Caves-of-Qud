using System;

namespace XRL.World.Parts
{
	// Token: 0x0200112A RID: 4394
	[Serializable]
	public class ModBiomech : IModification
	{
		// Token: 0x0600ADDB RID: 44507 RVA: 0x00405170 File Offset: 0x00403370
		public ModBiomech()
		{
		}

		// Token: 0x0600ADDC RID: 44508 RVA: 0x00405178 File Offset: 0x00403378
		public ModBiomech(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600ADDD RID: 44509 RVA: 0x00405181 File Offset: 0x00403381
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "Biomachinery";
		}

		// Token: 0x0600ADDE RID: 44510 RVA: 0x00405195 File Offset: 0x00403395
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.IsWall();
		}

		// Token: 0x0600ADDF RID: 44511 RVA: 0x004051A0 File Offset: 0x004033A0
		public override void ApplyModification(GameObject Object)
		{
			int num = this.Tier * this.Tier * 50;
			BiomechanicalPowerTransmission biomechanicalPowerTransmission = Object.RequirePart<BiomechanicalPowerTransmission>(false);
			if (biomechanicalPowerTransmission.ChargeRate < num)
			{
				biomechanicalPowerTransmission.ChargeRate = num;
			}
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600ADE0 RID: 44512 RVA: 0x004051DF File Offset: 0x004033DF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GenericRatingEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ADE1 RID: 44513 RVA: 0x00405205 File Offset: 0x00403405
		public override bool HandleEvent(GenericRatingEvent E)
		{
			if (E.Type == "WallDigNavigationWeight" && E.Object == this.ParentObject)
			{
				E.Rating += 6;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADE2 RID: 44514 RVA: 0x0040523C File Offset: 0x0040343C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{biomech|biomech}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADE3 RID: 44515 RVA: 0x00405266 File Offset: 0x00403466
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModBiomech.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADE4 RID: 44516 RVA: 0x00405286 File Offset: 0x00403486
		public static string GetDescription(int Tier)
		{
			return "Biomech: Has biomechanical power transmission systems.";
		}
	}
}
