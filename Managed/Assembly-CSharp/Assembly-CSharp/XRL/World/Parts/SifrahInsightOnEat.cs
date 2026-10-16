using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02000F8D RID: 3981
	[Serializable]
	public class SifrahInsightOnEat : IPart
	{
		// Token: 0x06009E06 RID: 40454 RVA: 0x00393490 File Offset: 0x00391690
		public override bool SameAs(IPart Part)
		{
			SifrahInsightOnEat sifrahInsightOnEat = Part as SifrahInsightOnEat;
			return sifrahInsightOnEat.TinkeringChance == this.TinkeringChance && sifrahInsightOnEat.SocialChance == this.SocialChance && sifrahInsightOnEat.RitualChance == this.RitualChance && sifrahInsightOnEat.PsionicChance == this.PsionicChance && base.SameAs(Part);
		}

		// Token: 0x06009E07 RID: 40455 RVA: 0x003934EB File Offset: 0x003916EB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009E08 RID: 40456 RVA: 0x00393500 File Offset: 0x00391700
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				if (gameObjectParameter != null && gameObjectParameter.IsPlayer())
				{
					if (this.TinkeringChance.in100())
					{
						TinkeringSifrah.AwardInsight();
					}
					if (this.SocialChance.in100())
					{
						SocialSifrah.AwardInsight();
					}
					if (this.RitualChance.in100())
					{
						RitualSifrah.AwardInsight();
					}
					if (this.PsionicChance.in100())
					{
						PsionicSifrah.AwardInsight();
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004159 RID: 16729
		public int TinkeringChance;

		// Token: 0x0400415A RID: 16730
		public int SocialChance;

		// Token: 0x0400415B RID: 16731
		public int RitualChance;

		// Token: 0x0400415C RID: 16732
		public int PsionicChance;
	}
}
