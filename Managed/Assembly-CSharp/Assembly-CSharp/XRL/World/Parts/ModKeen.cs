using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02001189 RID: 4489
	[Serializable]
	public class ModKeen : IModification
	{
		// Token: 0x0600B279 RID: 45689 RVA: 0x00414262 File Offset: 0x00412462
		public ModKeen()
		{
		}

		// Token: 0x0600B27A RID: 45690 RVA: 0x0041426A File Offset: 0x0041246A
		public ModKeen(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B27B RID: 45691 RVA: 0x00414273 File Offset: 0x00412473
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "EdgeHyperEnhancement";
		}

		// Token: 0x0600B27C RID: 45692 RVA: 0x00414287 File Offset: 0x00412487
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<ModSharp>();
		}

		// Token: 0x0600B27D RID: 45693 RVA: 0x00414290 File Offset: 0x00412490
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

		// Token: 0x0600B27E RID: 45694 RVA: 0x00414352 File Offset: 0x00412552
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B27F RID: 45695 RVA: 0x00414378 File Offset: 0x00412578
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("keen", -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B280 RID: 45696 RVA: 0x004143A3 File Offset: 0x004125A3
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModKeen.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B281 RID: 45697 RVA: 0x004143C3 File Offset: 0x004125C3
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B282 RID: 45698 RVA: 0x004143E6 File Offset: 0x004125E6
		public static string GetDescription(int Tier)
		{
			return "Keen: +2 to penetration rolls";
		}
	}
}
