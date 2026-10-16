using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02001172 RID: 4466
	[Serializable]
	public class ModTerrifyingVisage : IModification
	{
		// Token: 0x0600B1AD RID: 45485 RVA: 0x00412403 File Offset: 0x00410603
		public ModTerrifyingVisage()
		{
		}

		// Token: 0x0600B1AE RID: 45486 RVA: 0x0041240B File Offset: 0x0041060B
		public ModTerrifyingVisage(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B1AF RID: 45487 RVA: 0x00412414 File Offset: 0x00410614
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "TerrifyingVisage";
		}

		// Token: 0x0600B1B0 RID: 45488 RVA: 0x00412428 File Offset: 0x00410628
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Head", "Face", null, null, true, true, false, false);
		}

		// Token: 0x0600B1B1 RID: 45489 RVA: 0x0041244C File Offset: 0x0041064C
		public override void ApplyModification(GameObject Object)
		{
			string text;
			if ((text = Object.UsesSlots) == null)
			{
				Armor part = Object.GetPart<Armor>();
				text = ((part != null) ? part.WornOn : null);
			}
			string text2 = text;
			if (text2.IsNullOrEmpty() || text2 == "Head" || text2 == "Face")
			{
				text2 = "Head,Face";
			}
			else if (text2.Contains(","))
			{
				if (!text2.HasDelimitedSubstring(',', "Head", StringComparison.Ordinal))
				{
					text2 += ",Head";
				}
				if (!text2.HasDelimitedSubstring(',', "Face", StringComparison.Ordinal))
				{
					text2 += ",Face";
				}
			}
			else
			{
				text2 += ",Head,Face";
			}
			Object.UsesSlots = text2;
			string propertyOrTag = Object.GetPropertyOrTag("Mods", null);
			if (!propertyOrTag.IsNullOrEmpty())
			{
				if (propertyOrTag != "None")
				{
					List<string> list = new List<string>(propertyOrTag.Split(',', StringSplitOptions.None));
					if (!list.Contains("HeadwearMods"))
					{
						list.Add("HeadwearMods");
					}
					if (!list.Contains("MaskMods"))
					{
						list.Add("MaskMods");
					}
					Object.SetStringProperty("Mods", string.Join(",", list.ToArray()), false);
				}
			}
			else
			{
				Object.SetStringProperty("Mods", "HeadwearMods,MaskMods", false);
			}
			base.IncreaseDifficultyIfComplex(2, null);
		}

		// Token: 0x0600B1B2 RID: 45490 RVA: 0x00412592 File Offset: 0x00410792
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600B1B3 RID: 45491 RVA: 0x004125C0 File Offset: 0x004107C0
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.ModIntProperty("Horrifying", 1, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1B4 RID: 45492 RVA: 0x004125DC File Offset: 0x004107DC
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.ModIntProperty("Horrifying", -1, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1B5 RID: 45493 RVA: 0x004125F8 File Offset: 0x004107F8
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddWithClause("{{K|terrifying}} visage");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1B6 RID: 45494 RVA: 0x00412621 File Offset: 0x00410821
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModTerrifyingVisage.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1B7 RID: 45495 RVA: 0x00412641 File Offset: 0x00410841
		public static string GetDescription(int Tier)
		{
			return "Terrifying visage: This item reduces the cooldowns of Berate, Intimidate, and Menacing Stare by 10 rounds. It requires both the head and face to wear.";
		}
	}
}
