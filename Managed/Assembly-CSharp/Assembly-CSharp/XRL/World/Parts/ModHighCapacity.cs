using System;

namespace XRL.World.Parts
{
	// Token: 0x02001146 RID: 4422
	[Serializable]
	public class ModHighCapacity : IModification
	{
		// Token: 0x0600AF80 RID: 44928 RVA: 0x0040B4C4 File Offset: 0x004096C4
		public ModHighCapacity()
		{
		}

		// Token: 0x0600AF81 RID: 44929 RVA: 0x0040B4CC File Offset: 0x004096CC
		public ModHighCapacity(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF82 RID: 44930 RVA: 0x0040B4D5 File Offset: 0x004096D5
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "HighCapacityStorage";
		}

		// Token: 0x0600AF83 RID: 44931 RVA: 0x0040B4E9 File Offset: 0x004096E9
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<EnergyCell>();
		}

		// Token: 0x0600AF84 RID: 44932 RVA: 0x0040B4F4 File Offset: 0x004096F4
		public override void ApplyModification(GameObject Object)
		{
			EnergyCell part = Object.GetPart<EnergyCell>();
			part.MaxCharge = part.MaxCharge * (14 + this.Tier) / 10;
			if (Object.GetCurrentCell() == null)
			{
				part.Charge = part.Charge * (14 + this.Tier) / 10;
			}
			base.IncreaseComplexity(1, null);
		}

		// Token: 0x0600AF85 RID: 44933 RVA: 0x0040B54A File Offset: 0x0040974A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AF86 RID: 44934 RVA: 0x0040B568 File Offset: 0x00409768
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{c|high-capacity}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF87 RID: 44935 RVA: 0x0040B592 File Offset: 0x00409792
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModHighCapacity.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF88 RID: 44936 RVA: 0x0040B5B2 File Offset: 0x004097B2
		public static string GetDescription(int Tier)
		{
			return "High-capacity: This item has increased charge capacity.";
		}
	}
}
