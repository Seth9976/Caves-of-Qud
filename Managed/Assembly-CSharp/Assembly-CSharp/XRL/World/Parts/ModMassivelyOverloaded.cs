using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x0200118B RID: 4491
	[Serializable]
	public class ModMassivelyOverloaded : IModification
	{
		// Token: 0x0600B290 RID: 45712 RVA: 0x00414596 File Offset: 0x00412796
		public ModMassivelyOverloaded()
		{
		}

		// Token: 0x0600B291 RID: 45713 RVA: 0x0041459E File Offset: 0x0041279E
		public ModMassivelyOverloaded(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B292 RID: 45714 RVA: 0x004145A7 File Offset: 0x004127A7
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.IsEMPSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "AuxiliaryPowerRegulator";
		}

		// Token: 0x0600B293 RID: 45715 RVA: 0x004145C9 File Offset: 0x004127C9
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<ModOverloaded>();
		}

		// Token: 0x0600B294 RID: 45716 RVA: 0x004145D1 File Offset: 0x004127D1
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600B295 RID: 45717 RVA: 0x004145DC File Offset: 0x004127DC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetPowerLoadLevelEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B296 RID: 45718 RVA: 0x00414602 File Offset: 0x00412802
		public override bool HandleEvent(GetPowerLoadLevelEvent E)
		{
			E.Level += 800;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B297 RID: 45719 RVA: 0x0041461D File Offset: 0x0041281D
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{overloaded|massively overloaded}}", -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B298 RID: 45720 RVA: 0x00414648 File Offset: 0x00412848
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModMassivelyOverloaded.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B299 RID: 45721 RVA: 0x00414668 File Offset: 0x00412868
		public static string GetDescription(int Tier)
		{
			return "Massively overloaded: This item has significantly increased performance but consumes a great deal of extra charge, generates considerable heat when used, and has a chance to break relative to its charge draw.";
		}
	}
}
