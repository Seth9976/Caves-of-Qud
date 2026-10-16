using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02001188 RID: 4488
	[Serializable]
	public class ModHypervelocity : IModification
	{
		// Token: 0x0600B271 RID: 45681 RVA: 0x00414140 File Offset: 0x00412340
		public ModHypervelocity()
		{
		}

		// Token: 0x0600B272 RID: 45682 RVA: 0x00414148 File Offset: 0x00412348
		public ModHypervelocity(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B273 RID: 45683 RVA: 0x00414151 File Offset: 0x00412351
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B274 RID: 45684 RVA: 0x0041415C File Offset: 0x0041235C
		public override bool ModificationApplicable(GameObject Object)
		{
			MagazineAmmoLoader magazineAmmoLoader;
			return Object.HasPart<MissileWeapon>() && Object.TryGetPart<MagazineAmmoLoader>(out magazineAmmoLoader) && !(magazineAmmoLoader.AmmoPart == "AmmoMissile") && !(magazineAmmoLoader.AmmoPart == "AmmoGrenade") && !MissileWeapon.IsVorpal(Object);
		}

		// Token: 0x0600B275 RID: 45685 RVA: 0x004141B0 File Offset: 0x004123B0
		public override void ApplyModification(GameObject Object)
		{
			PoweredMissilePerformance poweredMissilePerformance = Object.RequirePart<PoweredMissilePerformance>(false);
			poweredMissilePerformance.WantAddAttribute("Vorpal");
			poweredMissilePerformance.PenetrateCreatures = new bool?(true);
			if (poweredMissilePerformance.ChargeUse > 100)
			{
				poweredMissilePerformance.ChargeUse = 100;
			}
			Object.RequirePart<EnergyCellSocket>(false);
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600B276 RID: 45686 RVA: 0x00414200 File Offset: 0x00412400
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B277 RID: 45687 RVA: 0x0041421E File Offset: 0x0041241E
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{hypervelocity|hypervelocity}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B278 RID: 45688 RVA: 0x00414248 File Offset: 0x00412448
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Hypervelocity: When powered, this weapon matches its penetration to its target's armor and penetrates creatures.");
			return base.HandleEvent(E);
		}
	}
}
