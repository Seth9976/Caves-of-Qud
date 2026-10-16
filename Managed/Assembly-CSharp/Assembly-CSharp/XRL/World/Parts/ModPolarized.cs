using System;

namespace XRL.World.Parts
{
	// Token: 0x0200115C RID: 4444
	[Serializable]
	public class ModPolarized : IModification
	{
		// Token: 0x0600B0A7 RID: 45223 RVA: 0x0040FA3A File Offset: 0x0040DC3A
		public ModPolarized()
		{
		}

		// Token: 0x0600B0A8 RID: 45224 RVA: 0x0040FA42 File Offset: 0x0040DC42
		public ModPolarized(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B0A9 RID: 45225 RVA: 0x0040FA4B File Offset: 0x0040DC4B
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B0AA RID: 45226 RVA: 0x0040FA54 File Offset: 0x0040DC54
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Head", "Face", null, null, true, true, true, false);
		}

		// Token: 0x0600B0AB RID: 45227 RVA: 0x0040FA78 File Offset: 0x0040DC78
		public override void ApplyModification(GameObject Object)
		{
			FlareCompensation part = Object.GetPart<FlareCompensation>();
			int modificationLevel = ModPolarized.GetModificationLevel(this.Tier);
			if (part == null)
			{
				Object.AddPart<FlareCompensation>(new FlareCompensation
				{
					Level = modificationLevel,
					IsBootSensitive = false,
					IsEMPSensitive = false,
					ShowInShortDescription = false,
					DescribeStatusForProperty = null
				}, true, false);
			}
			else if (part.IsEMPSensitive)
			{
				part.LevelHardened += modificationLevel;
				part.ShowInShortDescription = false;
			}
			else
			{
				part.Level += modificationLevel;
				part.ShowInShortDescription = false;
			}
			base.IncreaseComplexityIfComplex(1, null);
		}

		// Token: 0x0600B0AC RID: 45228 RVA: 0x0040FB0C File Offset: 0x0040DD0C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B0AD RID: 45229 RVA: 0x0040FB2A File Offset: 0x0040DD2A
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{polarized|polarized}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0AE RID: 45230 RVA: 0x0040FB54 File Offset: 0x0040DD54
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModPolarized.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0AF RID: 45231 RVA: 0x0040FB74 File Offset: 0x0040DD74
		public static int GetModificationLevel(int Tier)
		{
			return Math.Max(1, Tier / 3);
		}

		// Token: 0x0600B0B0 RID: 45232 RVA: 0x0040FB7F File Offset: 0x0040DD7F
		public static string GetDescription(int Tier)
		{
			return "Polarized: The item offers protection against visual flash effects.";
		}
	}
}
