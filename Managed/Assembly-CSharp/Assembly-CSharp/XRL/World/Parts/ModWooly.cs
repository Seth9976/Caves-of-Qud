using System;

namespace XRL.World.Parts
{
	// Token: 0x0200117A RID: 4474
	[Serializable]
	public class ModWooly : IModification
	{
		// Token: 0x0600B206 RID: 45574 RVA: 0x004131BC File Offset: 0x004113BC
		public ModWooly()
		{
		}

		// Token: 0x0600B207 RID: 45575 RVA: 0x004131C4 File Offset: 0x004113C4
		public ModWooly(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B208 RID: 45576 RVA: 0x004131CD File Offset: 0x004113CD
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B209 RID: 45577 RVA: 0x004131D6 File Offset: 0x004113D6
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<Armor>();
		}

		// Token: 0x0600B20A RID: 45578 RVA: 0x004131E0 File Offset: 0x004113E0
		public override void ApplyModification(GameObject Object)
		{
			Armor part = Object.GetPart<Armor>();
			if (part.WornOn == "Body")
			{
				part.Heat += 10;
				part.Cold += 10;
				return;
			}
			part.Heat += 5;
			part.Cold += 5;
		}

		// Token: 0x0600B20B RID: 45579 RVA: 0x00413241 File Offset: 0x00411441
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B20C RID: 45580 RVA: 0x00413267 File Offset: 0x00411467
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageIncrease += 50;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B20D RID: 45581 RVA: 0x0041327F File Offset: 0x0041147F
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{Y|wooly}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B20E RID: 45582 RVA: 0x004132A9 File Offset: 0x004114A9
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModWooly.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B20F RID: 45583 RVA: 0x004132C9 File Offset: 0x004114C9
		public static string GetDescription(int Tier)
		{
			return "Wooly: This item grants resistance to heat and cold.";
		}
	}
}
