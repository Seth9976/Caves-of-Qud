using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02001166 RID: 4454
	[Serializable]
	public class ModSereneVisage : IModification
	{
		// Token: 0x0600B12A RID: 45354 RVA: 0x00410F87 File Offset: 0x0040F187
		public ModSereneVisage()
		{
		}

		// Token: 0x0600B12B RID: 45355 RVA: 0x00410F8F File Offset: 0x0040F18F
		public ModSereneVisage(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B12C RID: 45356 RVA: 0x00410F98 File Offset: 0x0040F198
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "SereneVisage";
		}

		// Token: 0x0600B12D RID: 45357 RVA: 0x00410FAC File Offset: 0x0040F1AC
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Head", "Face", null, null, true, true, false, false);
		}

		// Token: 0x0600B12E RID: 45358 RVA: 0x00410FD0 File Offset: 0x0040F1D0
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
			Object.GetPart<Armor>().Willpower++;
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
			base.IncreaseDifficultyIfComplex(1, null);
		}

		// Token: 0x0600B12F RID: 45359 RVA: 0x00411129 File Offset: 0x0040F329
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600B130 RID: 45360 RVA: 0x00411157 File Offset: 0x0040F357
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.ModIntProperty("Serene", 1, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B131 RID: 45361 RVA: 0x00411173 File Offset: 0x0040F373
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.ModIntProperty("Serene", -1, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B132 RID: 45362 RVA: 0x0041118F File Offset: 0x0040F38F
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddWithClause("{{Y|serene}} visage");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B133 RID: 45363 RVA: 0x004111B8 File Offset: 0x0040F3B8
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModSereneVisage.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B134 RID: 45364 RVA: 0x004111D8 File Offset: 0x0040F3D8
		public static string GetDescription(int Tier)
		{
			return "Serene visage: This item grants bonus Willpower, reduces the cooldown of Meditate by 40 rounds, and is worn on both the head and face. It requires both the head and face to wear.";
		}
	}
}
