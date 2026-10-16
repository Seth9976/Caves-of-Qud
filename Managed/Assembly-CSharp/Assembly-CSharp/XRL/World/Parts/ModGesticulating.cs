using System;

namespace XRL.World.Parts
{
	// Token: 0x0200113F RID: 4415
	[Serializable]
	public class ModGesticulating : IModification
	{
		// Token: 0x0600AF20 RID: 44832 RVA: 0x00409D73 File Offset: 0x00407F73
		public ModGesticulating()
		{
		}

		// Token: 0x0600AF21 RID: 44833 RVA: 0x00409D7B File Offset: 0x00407F7B
		public ModGesticulating(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF22 RID: 44834 RVA: 0x00409D84 File Offset: 0x00407F84
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AF23 RID: 44835 RVA: 0x00409D90 File Offset: 0x00407F90
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Floating Nearby", null, null, null, false, true, false, true);
		}

		// Token: 0x0600AF24 RID: 44836 RVA: 0x00409DB0 File Offset: 0x00407FB0
		public override void ApplyModification(GameObject Object)
		{
			Armor part = Object.GetPart<Armor>();
			if (Object.UsesSlots == null)
			{
				string text;
				if (part != null && part.WornOn != "*")
				{
					text = part.WornOn;
				}
				else
				{
					text = "Hand";
				}
				Object.UsesSlots = text + ",Floating Nearby";
			}
			else
			{
				Object.UsesSlots += ",Floating Nearby";
			}
			if (part != null)
			{
				part.Strength += ModGesticulating.GetStrengthBonus(this.Tier);
			}
			else
			{
				string text2 = ModGesticulating.GetStrengthBonus(this.Tier).ToString();
				EquipStatBoost.AppendBoostOnEquip(Object, "Strength:" + text2, null, false);
			}
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600AF25 RID: 44837 RVA: 0x00409E66 File Offset: 0x00408066
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AF26 RID: 44838 RVA: 0x00409E84 File Offset: 0x00408084
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{m|gesticulating}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF27 RID: 44839 RVA: 0x00409EAE File Offset: 0x004080AE
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModGesticulating.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF28 RID: 44840 RVA: 0x00409ECE File Offset: 0x004080CE
		public static int GetStrengthBonus(int Tier)
		{
			if (Tier <= 3)
			{
				return 2;
			}
			if (Tier <= 4)
			{
				return 3;
			}
			if (Tier <= 5)
			{
				return 4;
			}
			if (Tier <= 6)
			{
				return 5;
			}
			return 6;
		}

		// Token: 0x0600AF29 RID: 44841 RVA: 0x00409EF0 File Offset: 0x004080F0
		public static string GetDescription(int Tier)
		{
			return "Gesticulating: This item grants +" + ModGesticulating.GetStrengthBonus(Tier).ToString() + " Strength but disallows the use of the Floating Nearby equipment slot.";
		}

		// Token: 0x0600AF2A RID: 44842 RVA: 0x00409F1C File Offset: 0x0040811C
		public string GetInstanceDescription()
		{
			return "Gesticulating: This item grants +" + ModGesticulating.GetStrengthBonus(this.Tier).ToString() + " Strength but disallows the use of the Floating Nearby equipment slot.";
		}
	}
}
