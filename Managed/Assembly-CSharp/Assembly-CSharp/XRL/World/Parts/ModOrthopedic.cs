using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02001190 RID: 4496
	[Serializable]
	public class ModOrthopedic : IModification
	{
		// Token: 0x0600B2C9 RID: 45769 RVA: 0x00414C13 File Offset: 0x00412E13
		public ModOrthopedic()
		{
		}

		// Token: 0x0600B2CA RID: 45770 RVA: 0x00414C1B File Offset: 0x00412E1B
		public ModOrthopedic(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B2CB RID: 45771 RVA: 0x00414C24 File Offset: 0x00412E24
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "OrthopedicSystems";
		}

		// Token: 0x0600B2CC RID: 45772 RVA: 0x00414C38 File Offset: 0x00412E38
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Feet", "Back", null, null, true, true, true, false);
		}

		// Token: 0x0600B2CD RID: 45773 RVA: 0x00414C5C File Offset: 0x00412E5C
		public override void ApplyModification(GameObject Object)
		{
			int num = -this.Tier;
			MoveCostMultiplier part = this.ParentObject.GetPart<MoveCostMultiplier>();
			if (part != null)
			{
				part.Amount += num;
			}
			else
			{
				this.ParentObject.AddPart<MoveCostMultiplier>(new MoveCostMultiplier(num), true, false);
			}
			SaveModifier saveModifier = Object.GetPart<SaveModifier>();
			if (saveModifier != null)
			{
				saveModifier.Amount += ModOrthopedic.GetSaveModifierAmount(this.Tier);
			}
			else
			{
				saveModifier = Object.AddPart<SaveModifier>(true, false);
				saveModifier.Vs = "Move,Knockdown,Restraint";
				saveModifier.Amount = ModOrthopedic.GetSaveModifierAmount(this.Tier);
				saveModifier.ShowInShortDescription = false;
			}
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600B2CE RID: 45774 RVA: 0x00414CFD File Offset: 0x00412EFD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B2CF RID: 45775 RVA: 0x00414D1B File Offset: 0x00412F1B
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("orthopedic", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2D0 RID: 45776 RVA: 0x00414D45 File Offset: 0x00412F45
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModOrthopedic.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2D1 RID: 45777 RVA: 0x00414D65 File Offset: 0x00412F65
		public static string GetDescription(int Tier)
		{
			return "Orthopedic: This item grants bonus move speed and " + (SavingThrows.GetSaveBonusDescription(ModOrthopedic.GetSaveModifierAmount(Tier), "Move,Knockdown,Restraint", false) ?? "no effect");
		}

		// Token: 0x0600B2D2 RID: 45778 RVA: 0x00414D8B File Offset: 0x00412F8B
		public static int GetSaveModifierAmount(int Tier)
		{
			return 1 + Tier / 6;
		}

		// Token: 0x0400483B RID: 18491
		public const string SAVE_VS = "Move,Knockdown,Restraint";
	}
}
