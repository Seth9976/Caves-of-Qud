using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200114B RID: 4427
	[Serializable]
	public class ModLacquered : IModification
	{
		// Token: 0x0600AFAE RID: 44974 RVA: 0x0040BCE7 File Offset: 0x00409EE7
		public ModLacquered()
		{
		}

		// Token: 0x0600AFAF RID: 44975 RVA: 0x0040BCEF File Offset: 0x00409EEF
		public ModLacquered(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AFB0 RID: 44976 RVA: 0x0040BCF8 File Offset: 0x00409EF8
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AFB1 RID: 44977 RVA: 0x0040BD04 File Offset: 0x00409F04
		public override bool ModificationApplicable(GameObject Object)
		{
			if (Object.HasPart<Metal>())
			{
				return true;
			}
			Springy part = Object.GetPart<Springy>();
			if (part == null)
			{
				return true;
			}
			if ((double)part.Factor <= 0.5)
			{
				return true;
			}
			Armor part2 = Object.GetPart<Armor>();
			return part2 != null && part2.AV >= 2;
		}

		// Token: 0x0600AFB2 RID: 44978 RVA: 0x0040BD51 File Offset: 0x00409F51
		public override void ApplyModification(GameObject Object)
		{
			Object.RemoveEffect<Rusted>();
			Object.RemoveEffect<LiquidCovered>();
			Object.RemoveEffect<LiquidStained>();
		}

		// Token: 0x0600AFB3 RID: 44979 RVA: 0x0040BD68 File Offset: 0x00409F68
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AFB4 RID: 44980 RVA: 0x0040BD8E File Offset: 0x00409F8E
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600AFB5 RID: 44981 RVA: 0x0040BD99 File Offset: 0x00409F99
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Understood() || !E.Object.HasProperName)
			{
				E.AddAdjective("{{lacquered|lacquered}}", -5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFB6 RID: 44982 RVA: 0x0040BDC4 File Offset: 0x00409FC4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModLacquered.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFB7 RID: 44983 RVA: 0x0040BDE4 File Offset: 0x00409FE4
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AFB8 RID: 44984 RVA: 0x0040BDE7 File Offset: 0x00409FE7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyRusted");
			Registrar.Register("ApplyLiquidCovered");
			Registrar.Register("ApplyLiquidStained");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AFB9 RID: 44985 RVA: 0x0040BE14 File Offset: 0x0040A014
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "ApplyRusted") && !(E.ID == "ApplyLiquidCovered") && !(E.ID == "ApplyLiquidStained") && base.FireEvent(E);
		}

		// Token: 0x0600AFBA RID: 44986 RVA: 0x0040BE60 File Offset: 0x0040A060
		public static string GetDescription(int Tier)
		{
			return "Lacquered: This item repels liquids and cannot rust.";
		}
	}
}
