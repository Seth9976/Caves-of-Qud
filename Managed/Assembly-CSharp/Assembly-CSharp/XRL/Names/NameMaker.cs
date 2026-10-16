using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.World;

namespace XRL.Names
{
	// Token: 0x02000584 RID: 1412
	public class NameMaker
	{
		// Token: 0x06003C9C RID: 15516 RVA: 0x00129F90 File Offset: 0x00128190
		public static string MakeName(GameObject For = null, string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null, string Region = null, string Gender = null, List<string> Mutations = null, string Tag = null, string Special = null, string Type = null, Dictionary<string, string> NamingContext = null, bool FailureOkay = false, bool SpecialFaildown = false, bool? HasHonorific = null, bool? HasEpithet = null)
		{
			return NameStyles.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, Type, NamingContext, FailureOkay, SpecialFaildown, null, null, null, null, HasHonorific, HasEpithet, false);
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x00129FDC File Offset: 0x001281DC
		public static void MakeName(ref string Into, GameObject For = null, string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null, string Region = null, string Gender = null, List<string> Mutations = null, string Tag = null, string Special = null, string Type = null, Dictionary<string, string> NamingContext = null, bool SpecialFaildown = false, bool? HasHonorific = null, bool? HasEpithet = null)
		{
			string text = NameStyles.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, Type, NamingContext, true, SpecialFaildown, null, null, null, null, HasHonorific, HasEpithet, false);
			if (!text.IsNullOrEmpty())
			{
				Into = text;
			}
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x0012A034 File Offset: 0x00128234
		public static string MakeHonorific(GameObject For = null, string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null, string Region = null, string Gender = null, List<string> Mutations = null, string Tag = null, string Special = null, Dictionary<string, string> NamingContext = null, bool SpecialFaildown = false, bool? HasHonorific = null, bool? HasEpithet = null)
		{
			return NameStyles.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, "Honorific", NamingContext, true, SpecialFaildown, null, null, null, null, HasHonorific, HasEpithet, false);
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x0012A080 File Offset: 0x00128280
		public static string MakeEpithet(GameObject For = null, string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null, string Region = null, string Gender = null, List<string> Mutations = null, string Tag = null, string Special = null, Dictionary<string, string> NamingContext = null, bool SpecialFaildown = false, bool? HasHonorific = null, bool? HasEpithet = null)
		{
			return NameStyles.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, "Epithet", NamingContext, true, SpecialFaildown, null, null, null, null, HasHonorific, HasEpithet, false);
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x0012A0CC File Offset: 0x001282CC
		public static string MakeTitle(GameObject For = null, string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null, string Region = null, string Gender = null, List<string> Mutations = null, string Tag = null, string Special = null, Dictionary<string, string> NamingContext = null, bool SpecialFaildown = false, bool? HasHonorific = null, bool? HasEpithet = null)
		{
			return NameStyles.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, "Title", NamingContext, true, SpecialFaildown, null, null, null, null, HasHonorific, HasEpithet, false);
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x0012A118 File Offset: 0x00128318
		public static string MakeExtraTitle(GameObject For = null, string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null, string Region = null, string Gender = null, List<string> Mutations = null, string Tag = null, string Special = null, Dictionary<string, string> NamingContext = null, bool SpecialFaildown = false, bool? HasHonorific = null, bool? HasEpithet = null)
		{
			return NameStyles.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, "ExtraTitle", NamingContext, true, SpecialFaildown, null, null, null, null, HasHonorific, HasEpithet, false);
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x0012A164 File Offset: 0x00128364
		public static string Eater()
		{
			if (If.Chance(50, null, null))
			{
				return NameMaker.MakeName(null, null, null, null, "Eater", null, null, null, null, null, null, null, null, false, false, null, null);
			}
			return NameMaker.MakeName(EncountersAPI.GetASampleCreature(null), null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x0012A1D4 File Offset: 0x001283D4
		public static string YdFreeholder()
		{
			return NameMaker.MakeName(EncountersAPI.GetASampleCreature(null), null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
		}
	}
}
