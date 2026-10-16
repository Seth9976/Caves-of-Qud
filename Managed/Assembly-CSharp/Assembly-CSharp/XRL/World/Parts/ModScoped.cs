using System;

namespace XRL.World.Parts
{
	// Token: 0x02001165 RID: 4453
	[Serializable]
	public class ModScoped : IModification
	{
		// Token: 0x0600B120 RID: 45344 RVA: 0x00410EA7 File Offset: 0x0040F0A7
		public ModScoped()
		{
		}

		// Token: 0x0600B121 RID: 45345 RVA: 0x00410EAF File Offset: 0x0040F0AF
		public ModScoped(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B122 RID: 45346 RVA: 0x00410EB8 File Offset: 0x0040F0B8
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B123 RID: 45347 RVA: 0x00410EC1 File Offset: 0x0040F0C1
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MissileWeapon>();
		}

		// Token: 0x0600B124 RID: 45348 RVA: 0x00410ECE File Offset: 0x0040F0CE
		public override void ApplyModification(GameObject Object)
		{
			Object.GetPart<MissileWeapon>().AimVarianceBonus = 4;
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600B125 RID: 45349 RVA: 0x00410EE5 File Offset: 0x0040F0E5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B126 RID: 45350 RVA: 0x00410F03 File Offset: 0x0040F103
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!this.ParentObject.HasPart<ModSmart>() && E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("scoped", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B127 RID: 45351 RVA: 0x00410F3A File Offset: 0x0040F13A
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.ParentObject.HasPart<ModSmart>())
			{
				E.Base.Compound(this.GetPhysicalDescription());
				E.Postfix.AppendRules(ModScoped.GetDescription(this.Tier));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B128 RID: 45352 RVA: 0x00410F79 File Offset: 0x0040F179
		public string GetPhysicalDescription()
		{
			return "";
		}

		// Token: 0x0600B129 RID: 45353 RVA: 0x00410F80 File Offset: 0x0040F180
		public static string GetDescription(int Tier)
		{
			return "Scoped: This weapon has increased accuracy.";
		}
	}
}
