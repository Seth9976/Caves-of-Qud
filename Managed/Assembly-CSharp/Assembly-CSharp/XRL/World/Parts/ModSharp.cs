using System;

namespace XRL.World.Parts
{
	// Token: 0x02001168 RID: 4456
	[Serializable]
	public class ModSharp : IModification
	{
		// Token: 0x0600B144 RID: 45380 RVA: 0x00411458 File Offset: 0x0040F658
		public ModSharp()
		{
		}

		// Token: 0x0600B145 RID: 45381 RVA: 0x00411460 File Offset: 0x0040F660
		public ModSharp(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B146 RID: 45382 RVA: 0x00411469 File Offset: 0x0040F669
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "EdgeEnhancement";
		}

		// Token: 0x0600B147 RID: 45383 RVA: 0x00411480 File Offset: 0x0040F680
		public override bool ModificationApplicable(GameObject Object)
		{
			MeleeWeapon meleeWeapon;
			ThrownWeapon thrownWeapon;
			return (Object.TryGetPart<MeleeWeapon>(out meleeWeapon) && (meleeWeapon.Skill == "LongBlades" || meleeWeapon.Skill == "ShortBlades" || meleeWeapon.Skill == "Axe")) || (Object.TryGetPart<ThrownWeapon>(out thrownWeapon) && thrownWeapon.Attributes != null && (thrownWeapon.Attributes.HasDelimitedSubstring(' ', "LongBlades", StringComparison.Ordinal) || thrownWeapon.Attributes.HasDelimitedSubstring(' ', "ShortBlades", StringComparison.Ordinal) || thrownWeapon.Attributes.HasDelimitedSubstring(' ', "Axe", StringComparison.Ordinal)));
		}

		// Token: 0x0600B148 RID: 45384 RVA: 0x00411524 File Offset: 0x0040F724
		public override void ApplyModification(GameObject Object)
		{
			MeleeWeapon part = Object.GetPart<MeleeWeapon>();
			if (part != null && (part.Skill == "LongBlades" || part.Skill == "ShortBlades" || part.Skill == "Axe"))
			{
				part.PenBonus++;
			}
			ThrownWeapon thrownWeapon;
			if (Object.TryGetPart<ThrownWeapon>(out thrownWeapon) && thrownWeapon.Attributes != null && (thrownWeapon.Attributes.HasDelimitedSubstring(' ', "LongBlades", StringComparison.Ordinal) || thrownWeapon.Attributes.HasDelimitedSubstring(' ', "ShortBlades", StringComparison.Ordinal) || thrownWeapon.Attributes.HasDelimitedSubstring(' ', "Axe", StringComparison.Ordinal)))
			{
				thrownWeapon.PenetrationBonus++;
			}
			base.IncreaseDifficultyIfComplex(1, null);
		}

		// Token: 0x0600B149 RID: 45385 RVA: 0x004115E6 File Offset: 0x0040F7E6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B14A RID: 45386 RVA: 0x0041160C File Offset: 0x0040F80C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName && !E.Object.HasPart<ModKeen>())
			{
				E.AddAdjective("sharp", -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B14B RID: 45387 RVA: 0x00411644 File Offset: 0x0040F844
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!E.Object.HasPart<ModKeen>())
			{
				E.Postfix.AppendRules(ModSharp.GetDescription(this.Tier));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B14C RID: 45388 RVA: 0x00411671 File Offset: 0x0040F871
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B14D RID: 45389 RVA: 0x00411694 File Offset: 0x0040F894
		public static string GetDescription(int Tier)
		{
			return "Sharp: +1 to penetration rolls";
		}
	}
}
