using System;

namespace XRL.World.Parts
{
	// Token: 0x02001145 RID: 4421
	[Serializable]
	public class ModHeatSeeking : IModification
	{
		// Token: 0x0600AF74 RID: 44916 RVA: 0x0040B3A2 File Offset: 0x004095A2
		public ModHeatSeeking()
		{
		}

		// Token: 0x0600AF75 RID: 44917 RVA: 0x0040B3AA File Offset: 0x004095AA
		public ModHeatSeeking(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF76 RID: 44918 RVA: 0x0040B3B3 File Offset: 0x004095B3
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "Homing";
		}

		// Token: 0x0600AF77 RID: 44919 RVA: 0x0040B3C7 File Offset: 0x004095C7
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MeleeWeapon>();
		}

		// Token: 0x0600AF78 RID: 44920 RVA: 0x0040B3D4 File Offset: 0x004095D4
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600AF79 RID: 44921 RVA: 0x0040B3DF File Offset: 0x004095DF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AF7A RID: 44922 RVA: 0x0040B3FD File Offset: 0x004095FD
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("homing", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF7B RID: 44923 RVA: 0x0040B427 File Offset: 0x00409627
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModHeatSeeking.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF7C RID: 44924 RVA: 0x0040B447 File Offset: 0x00409647
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AF7D RID: 44925 RVA: 0x0040B44A File Offset: 0x0040964A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponGetDefenderDV");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AF7E RID: 44926 RVA: 0x0040B460 File Offset: 0x00409660
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponGetDefenderDV" && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.SetParameter("DV", 0);
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AF7F RID: 44927 RVA: 0x0040B4BD File Offset: 0x004096BD
		public static string GetDescription(int Tier)
		{
			return "Homing: This weapon ignores DV.";
		}
	}
}
