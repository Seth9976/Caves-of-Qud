using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x0200118E RID: 4494
	[Serializable]
	public class ModMighty : IModification
	{
		// Token: 0x0600B2B4 RID: 45748 RVA: 0x004149D0 File Offset: 0x00412BD0
		public ModMighty()
		{
		}

		// Token: 0x0600B2B5 RID: 45749 RVA: 0x004149D8 File Offset: 0x00412BD8
		public ModMighty(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B2B6 RID: 45750 RVA: 0x004149E1 File Offset: 0x00412BE1
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B2B7 RID: 45751 RVA: 0x004149EA File Offset: 0x00412BEA
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MeleeWeapon>();
		}

		// Token: 0x0600B2B8 RID: 45752 RVA: 0x004149F2 File Offset: 0x00412BF2
		public override void ApplyModification(GameObject Object)
		{
			Object.GetPart<MeleeWeapon>().MaxStrengthBonus = 999;
		}

		// Token: 0x0600B2B9 RID: 45753 RVA: 0x00414A04 File Offset: 0x00412C04
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B2BA RID: 45754 RVA: 0x00414A07 File Offset: 0x00412C07
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B2BB RID: 45755 RVA: 0x00414A25 File Offset: 0x00412C25
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("mighty", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2BC RID: 45756 RVA: 0x00414A4F File Offset: 0x00412C4F
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModMighty.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2BD RID: 45757 RVA: 0x00414A6F File Offset: 0x00412C6F
		public static string GetDescription(int Tier)
		{
			return "Mighty: This weapon has no strength bonus penetration cap.";
		}
	}
}
