using System;

namespace XRL.World.Parts
{
	// Token: 0x02001177 RID: 4471
	[Serializable]
	public class ModWeightless : IModification
	{
		// Token: 0x0600B1E9 RID: 45545 RVA: 0x00412F33 File Offset: 0x00411133
		public ModWeightless()
		{
		}

		// Token: 0x0600B1EA RID: 45546 RVA: 0x00412F3B File Offset: 0x0041113B
		public ModWeightless(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B1EB RID: 45547 RVA: 0x00412F44 File Offset: 0x00411144
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "Weightless";
		}

		// Token: 0x0600B1EC RID: 45548 RVA: 0x00412F58 File Offset: 0x00411158
		public override bool ModificationApplicable(GameObject Object)
		{
			return true;
		}

		// Token: 0x0600B1ED RID: 45549 RVA: 0x00412F5B File Offset: 0x0041115B
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600B1EE RID: 45550 RVA: 0x00412F66 File Offset: 0x00411166
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustWeightEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B1EF RID: 45551 RVA: 0x00412F8C File Offset: 0x0041118C
		public override bool HandleEvent(AdjustWeightEvent E)
		{
			E.Weight = 0.0;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1F0 RID: 45552 RVA: 0x00412FA4 File Offset: 0x004111A4
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{y-K sequence|weightless}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1F1 RID: 45553 RVA: 0x00412FCE File Offset: 0x004111CE
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModWeightless.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1F2 RID: 45554 RVA: 0x00412FEE File Offset: 0x004111EE
		public static string GetDescription(int Tier)
		{
			return "Weightless: This item weighs nothing.";
		}
	}
}
