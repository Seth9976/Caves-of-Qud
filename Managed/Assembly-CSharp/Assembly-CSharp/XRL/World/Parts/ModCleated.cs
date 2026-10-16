using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x0200112C RID: 4396
	[Serializable]
	public class ModCleated : IModification
	{
		// Token: 0x0600ADF1 RID: 44529 RVA: 0x004054F5 File Offset: 0x004036F5
		public ModCleated()
		{
		}

		// Token: 0x0600ADF2 RID: 44530 RVA: 0x004054FD File Offset: 0x004036FD
		public ModCleated(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600ADF3 RID: 44531 RVA: 0x00405506 File Offset: 0x00403706
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "StabilityAssist";
		}

		// Token: 0x0600ADF4 RID: 44532 RVA: 0x0040551C File Offset: 0x0040371C
		public override bool ModificationApplicable(GameObject Object)
		{
			if (!IModification.CheckWornSlot(Object, "Feet", "Back", null, null, true, true, true, false))
			{
				return false;
			}
			SaveModifier part = Object.GetPart<SaveModifier>();
			return part == null || !(part.Vs != "Move,Knockdown,Restraint");
		}

		// Token: 0x0600ADF5 RID: 44533 RVA: 0x00405564 File Offset: 0x00403764
		public override void ApplyModification(GameObject Object)
		{
			SaveModifier saveModifier = Object.GetPart<SaveModifier>();
			if (saveModifier != null)
			{
				saveModifier.Amount += ModCleated.GetSaveModifierAmount(this.Tier);
			}
			else
			{
				saveModifier = Object.AddPart<SaveModifier>(true, false);
				saveModifier.Vs = "Move,Knockdown,Restraint";
				saveModifier.Amount = ModCleated.GetSaveModifierAmount(this.Tier);
				saveModifier.ShowInShortDescription = false;
			}
			base.IncreaseDifficultyIfComplex(1, null);
		}

		// Token: 0x0600ADF6 RID: 44534 RVA: 0x004055C9 File Offset: 0x004037C9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ADF7 RID: 44535 RVA: 0x004055E7 File Offset: 0x004037E7
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddWithClause("cleats");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADF8 RID: 44536 RVA: 0x00405610 File Offset: 0x00403810
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModCleated.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADF9 RID: 44537 RVA: 0x00405630 File Offset: 0x00403830
		public static string GetDescription(int Tier)
		{
			return "Fitted with cleats: " + (SavingThrows.GetSaveBonusDescription(ModCleated.GetSaveModifierAmount(Tier), "Move,Knockdown,Restraint", false) ?? "no effect");
		}

		// Token: 0x0600ADFA RID: 44538 RVA: 0x00405656 File Offset: 0x00403856
		public static int GetSaveModifierAmount(int Tier)
		{
			return 2 + Tier / 4;
		}

		// Token: 0x040047DE RID: 18398
		public const string SAVE_VS = "Move,Knockdown,Restraint";
	}
}
