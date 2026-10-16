using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World;
using XRL.World.Parts.Mutation;

namespace XRL
{
	// Token: 0x02000530 RID: 1328
	[HasModSensitiveStaticCache]
	public static class MutationFactory
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x00117030 File Offset: 0x00115230
		[Obsolete("Use GetCategories() instead of Categories")]
		public static List<MutationCategory> Categories
		{
			get
			{
				MutationFactory.CheckInit();
				return MutationFactory._Categories;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060038D5 RID: 14549 RVA: 0x0011703C File Offset: 0x0011523C
		[Obsolete("Use GetCategories() instead of CategoriesByName")]
		public static Dictionary<string, MutationCategory> CategoriesByName
		{
			get
			{
				MutationFactory.CheckInit();
				return MutationFactory._CategoriesByName;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x00117048 File Offset: 0x00115248
		[Obsolete("Use HasMutation(string name) or GetMutationEntryByName(string name)")]
		public static Dictionary<string, MutationEntry> MutationsByName
		{
			get
			{
				MutationFactory.CheckInit();
				return MutationFactory._MutationsByName;
			}
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x00117054 File Offset: 0x00115254
		public static void CheckInit()
		{
			if (MutationFactory._Categories == null)
			{
				try
				{
					Loading.LoadTask("Loading Mutations.xml", new Action(MutationFactory.Init), true);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Error loading mutations.", ex, "game_exception");
				}
			}
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x001170A4 File Offset: 0x001152A4
		public static IEnumerable<MutationEntry> AllMutationEntries()
		{
			return MutationFactory._MutationsByName.Values;
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x001170B0 File Offset: 0x001152B0
		public static bool HasMutation(string name)
		{
			MutationFactory.CheckInit();
			return MutationFactory._MutationsByName.ContainsKey(name);
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x001170C4 File Offset: 0x001152C4
		public static MutationEntry GetMutationEntryByName(string name)
		{
			MutationFactory.CheckInit();
			MutationEntry mutationEntry;
			MutationFactory._MutationsByName.TryGetValue(name, out mutationEntry);
			return mutationEntry;
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x001170E5 File Offset: 0x001152E5
		public static List<MutationCategory> GetCategories()
		{
			MutationFactory.CheckInit();
			return MutationFactory._Categories;
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x001170F1 File Offset: 0x001152F1
		public static List<MutationEntry> GetMutationEntries(BaseMutation mutation)
		{
			return MutationFactory.GetMutationEntries(mutation.GetType().Name);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x00117104 File Offset: 0x00115304
		public static List<MutationEntry> GetMutationEntries(string Class)
		{
			MutationFactory.CheckInit();
			List<MutationEntry> list;
			if (!MutationFactory._MutationsByClass.TryGetValue(Class, out list))
			{
				return null;
			}
			return list;
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x00117128 File Offset: 0x00115328
		public static bool TryGetMutationEntry(BaseMutation Mutation, out MutationEntry Entry)
		{
			MutationEntry mutationEntry = Mutation.GetMutationEntry();
			return MutationFactory.TryGetMutationEntry((mutationEntry != null) ? mutationEntry.Name : null, Mutation.Name, out Entry);
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x00117148 File Offset: 0x00115348
		public static bool TryGetMutationEntry(string Spec, out MutationEntry Entry)
		{
			return MutationFactory.TryGetMutationEntry(Spec, Spec, out Entry);
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x00117154 File Offset: 0x00115354
		public static bool TryGetMutationEntry(string Name, string Class, out MutationEntry Entry)
		{
			MutationFactory.CheckInit();
			if (Name != null && MutationFactory._MutationsByName.TryGetValue(Name, out Entry))
			{
				return true;
			}
			List<MutationEntry> list;
			if (Class != null && MutationFactory._MutationsByClass.TryGetValue(Class, out list))
			{
				Entry = list[0];
				return true;
			}
			Entry = null;
			return false;
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x0011719C File Offset: 0x0011539C
		public static MutationEntry GetMorphotype(GameObject Object)
		{
			MutationEntry mutationEntry;
			MutationFactory.TryGetMorphotype(Object, out mutationEntry);
			return mutationEntry;
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x001171B4 File Offset: 0x001153B4
		public static bool TryGetMorphotype(GameObject Object, out MutationEntry Morphotype)
		{
			MutationFactory.CheckInit();
			if (MutationFactory.Morphotypes != null)
			{
				string tagOrStringProperty = Object.GetTagOrStringProperty("MutationLevel", null);
				if (!tagOrStringProperty.IsNullOrEmpty() && MutationFactory._MutationsByName.TryGetValue(tagOrStringProperty, out Morphotype) && Morphotype.Category == MutationFactory.Morphotypes)
				{
					return true;
				}
				foreach (MutationEntry mutationEntry in MutationFactory.Morphotypes.Entries)
				{
					if (!mutationEntry.Class.IsNullOrEmpty() && Object.HasPart(mutationEntry.Class))
					{
						Morphotype = mutationEntry;
						return true;
					}
				}
			}
			Morphotype = null;
			return false;
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x00117270 File Offset: 0x00115470
		private static void AddFromCategoryRespectingPrerelease(List<MutationEntry> list, MutationCategory category, bool IgnoreExclude = false)
		{
			MutationFactory.CheckInit();
			bool enablePrereleaseContent = Options.EnablePrereleaseContent;
			foreach (MutationEntry mutationEntry in category.Entries)
			{
				if ((!mutationEntry.Prerelease || enablePrereleaseContent) && (!mutationEntry.ExcludeFromPool || IgnoreExclude))
				{
					list.Add(mutationEntry);
				}
			}
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x001172E4 File Offset: 0x001154E4
		public static List<MutationEntry> GetMutationsOfCategory(string Categories, bool IgnoreExclude = false)
		{
			MutationFactory.CheckInit();
			List<MutationEntry> list = new List<MutationEntry>(128);
			if (Categories.Contains(","))
			{
				foreach (string text in Categories.Split(',', StringSplitOptions.None))
				{
					MutationFactory.AddFromCategoryRespectingPrerelease(list, MutationFactory._CategoriesByName[text], IgnoreExclude);
				}
			}
			else
			{
				MutationFactory.AddFromCategoryRespectingPrerelease(list, MutationFactory._CategoriesByName[Categories], IgnoreExclude);
			}
			return list;
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x00117354 File Offset: 0x00115554
		public static BaseMutation GetRandomMutation(string Categories)
		{
			MutationFactory.CheckInit();
			MutationEntry randomElement = MutationFactory.GetMutationsOfCategory(Categories, false).GetRandomElement<MutationEntry>(null);
			if (randomElement == null)
			{
				return null;
			}
			return randomElement.CreateInstance();
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x00117380 File Offset: 0x00115580
		private static void Init()
		{
			MutationFactory._Categories = new List<MutationCategory>(5);
			MutationFactory._CategoriesByName = new Dictionary<string, MutationCategory>(5);
			MutationFactory._MutationsByName = new Dictionary<string, MutationEntry>(128);
			MutationFactory._MutationsByClass = new Dictionary<string, List<MutationEntry>>(128);
			MutationFactory.StatsUsedByMutations = new List<string>(1);
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("Mutations", true, true))
			{
				xmlDataHelper.HandleNodes(MutationFactory.XMLNodes, null);
			}
			for (int i = 0; i < MutationFactory._Categories.Count; i++)
			{
				MutationCategory mutationCategory = MutationFactory._Categories[i];
				if (!string.IsNullOrEmpty(mutationCategory.Stat) && !MutationFactory.StatsUsedByMutations.Contains(mutationCategory.Stat))
				{
					MutationFactory.StatsUsedByMutations.Add(mutationCategory.Stat);
				}
				mutationCategory.Entries.Sort((MutationEntry a, MutationEntry b) => a.GetDisplayName(false).CompareTo(b.GetDisplayName(false)));
				for (int j = 0; j < mutationCategory.Entries.Count; j++)
				{
					MutationEntry mutationEntry = mutationCategory.Entries[j];
					MutationFactory._MutationsByName[mutationEntry.Name] = mutationEntry;
					if (!string.IsNullOrEmpty(mutationEntry.Class))
					{
						if (!MutationFactory._MutationsByClass.ContainsKey(mutationEntry.Class))
						{
							MutationFactory._MutationsByClass.Add(mutationEntry.Class, new List<MutationEntry>(1));
						}
						MutationFactory._MutationsByClass[mutationEntry.Class].Add(mutationEntry);
					}
					if (!string.IsNullOrEmpty(mutationEntry.Stat) && !MutationFactory.StatsUsedByMutations.Contains(mutationEntry.Stat))
					{
						MutationFactory.StatsUsedByMutations.Add(mutationEntry.Stat);
					}
				}
			}
			MutationFactory._CategoriesByName.TryGetValue("Morphotypes", out MutationFactory.Morphotypes);
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x0011756C File Offset: 0x0011576C
		public static MutationEntry CreateMutationEntryForMutation(BaseMutation Mutation)
		{
			MutationEntry mutationEntry = new MutationEntry
			{
				Name = (string.IsNullOrEmpty(Mutation._DisplayName) ? Mutation.GetType().Name : Mutation._DisplayName),
				Class = Mutation.GetType().Name,
				Hidden = true,
				ExcludeFromPool = true
			};
			string text = Mutation._Type ?? "Physical";
			MutationCategory mutationCategory;
			if (MutationFactory._CategoriesByName.TryGetValue(text, out mutationCategory))
			{
				mutationEntry.Category = mutationCategory;
				mutationCategory.Add(mutationEntry);
			}
			MutationFactory._MutationsByClass.TryAdd(mutationEntry.Class, new List<MutationEntry> { mutationEntry });
			MutationFactory._MutationsByName.TryAdd(mutationEntry.Name, mutationEntry);
			return mutationEntry;
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x00117624 File Offset: 0x00115824
		public static void HandleMutationsNode(XmlDataHelper xml)
		{
			MutationFactory.FilePoolExclude = !xml.ParseAttribute<bool>("IncludeInMutatePool", true, false, null);
			MutationFactory.FilePoolExclude = xml.ParseAttribute<bool>("ExcludeFromPool", MutationFactory.FilePoolExclude, false, null);
			MutationFactory.FileHidden = xml.ParseAttribute<bool>("Hidden", false, false, null);
			xml.HandleNodes(MutationFactory.XMLNodes, null);
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x00117680 File Offset: 0x00115880
		public static void HandleCategoryNode(XmlDataHelper xml)
		{
			string text = xml.ParseAttribute<string>("Name", null, true, null);
			if (text.StartsWith('-'))
			{
				text = text.TrimStart('-');
				MetricsManager.LogPotentialModError(xml.modInfo, DataManager.SanitizePathForDisplay(xml.BaseURI) + ":" + xml.LineNumber.ToString() + ": Entry removal discontinued, set Hidden attribute instead.");
			}
			MutationCategory mutationCategory;
			if (!MutationFactory._CategoriesByName.TryGetValue(text, out mutationCategory))
			{
				mutationCategory = new MutationCategory(text);
				MutationFactory._CategoriesByName.Add(text, mutationCategory);
				MutationFactory._Categories.Add(mutationCategory);
			}
			MutationFactory._currentParsingCategory = mutationCategory;
			MutationFactory.CategoryPoolExclude = !xml.ParseAttribute<bool>("IncludeInMutatePool", !MutationFactory.FilePoolExclude, false, null);
			MutationFactory.CategoryPoolExclude = xml.ParseAttribute<bool>("ExcludeFromPool", MutationFactory.CategoryPoolExclude, false, null);
			MutationFactory.CategoryHidden = xml.ParseAttribute<bool>("Hidden", MutationFactory.FileHidden, false, null);
			MutationFactory.CategoryDefect = xml.ParseAttribute<bool>("Defect", false, false, null);
			mutationCategory.DisplayName = xml.ParseAttribute<string>("DisplayName", mutationCategory.DisplayName, false, null);
			mutationCategory.Help = xml.ParseAttribute<string>("Help", mutationCategory.Help, false, null);
			mutationCategory.Stat = xml.ParseAttribute<string>("Stat", mutationCategory.Stat, false, null);
			mutationCategory.Property = xml.ParseAttribute<string>("Property", mutationCategory.Property, false, null);
			mutationCategory.ForceProperty = xml.ParseAttribute<string>("ForceProperty", mutationCategory.ForceProperty, false, null);
			mutationCategory.Foreground = xml.ParseAttribute<string>("Foreground", mutationCategory.Foreground, false, null);
			mutationCategory.Detail = xml.ParseAttribute<string>("Detail", mutationCategory.Detail, false, null);
			xml.HandleNodes(new Dictionary<string, Action<XmlDataHelper>> { 
			{
				"mutation",
				new Action<XmlDataHelper>(MutationFactory.HandleMutationNode)
			} }, null);
			MutationFactory._currentParsingCategory = null;
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x0011784C File Offset: 0x00115A4C
		public static void HandleMutationNode(XmlDataHelper xml)
		{
			string name = xml.ParseAttribute<string>("Name", null, true, null);
			MutationFactory.currentMutation = MutationFactory._currentParsingCategory.Entries.Find((MutationEntry x) => x.Name == name);
			bool flag = MutationFactory.currentMutation == null;
			if (MutationFactory.currentMutation == null)
			{
				MutationFactory.currentMutation = new MutationEntry
				{
					Name = name,
					Defect = MutationFactory.CategoryDefect,
					Hidden = MutationFactory.CategoryHidden,
					ExcludeFromPool = MutationFactory.CategoryPoolExclude,
					Foreground = MutationFactory._currentParsingCategory.Foreground,
					Detail = MutationFactory._currentParsingCategory.Detail
				};
			}
			MutationFactory.currentMutation.HandleXMLNode(xml);
			MutationFactory.currentMutation.Category = MutationFactory._currentParsingCategory;
			MutationEntry mutationEntry = MutationFactory.currentMutation;
			if (mutationEntry.Type == null)
			{
				MutationEntry mutationEntry2 = mutationEntry;
				MutationCategory currentParsingCategory = MutationFactory._currentParsingCategory;
				mutationEntry2.Type = ((currentParsingCategory != null) ? currentParsingCategory.Name : null);
			}
			if (flag)
			{
				if (MutationFactory.currentMutation.Prerelease && !Options.EnablePrereleaseContent)
				{
					MutationFactory.currentMutation.Hidden = true;
					MutationFactory.currentMutation.ExcludeFromPool = true;
				}
				MutationFactory._currentParsingCategory.Entries.Add(MutationFactory.currentMutation);
			}
			xml.HandleNodes(MutationFactory.MutationSubnodes, null);
			MutationFactory.currentMutation = null;
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x00117989 File Offset: 0x00115B89
		public static void HandleLevelTextNode(XmlDataHelper xml)
		{
			Templates.LoadTemplateFromExternal("Mutation." + MutationFactory.currentMutation.Class + ".LevelText", xml);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x001179AA File Offset: 0x00115BAA
		public static void HandleDescriptionNode(XmlDataHelper xml)
		{
			Templates.LoadTemplateFromExternal("Mutation." + MutationFactory.currentMutation.Class + ".Description", xml);
		}

		// Token: 0x04001CBD RID: 7357
		[ModSensitiveStaticCache(false)]
		private static List<MutationCategory> _Categories = null;

		// Token: 0x04001CBE RID: 7358
		private static Dictionary<string, MutationCategory> _CategoriesByName = null;

		// Token: 0x04001CBF RID: 7359
		private static Dictionary<string, MutationEntry> _MutationsByName = null;

		// Token: 0x04001CC0 RID: 7360
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, List<MutationEntry>> _MutationsByClass = null;

		// Token: 0x04001CC1 RID: 7361
		[ModSensitiveStaticCache(false)]
		public static List<string> StatsUsedByMutations = new List<string>(1);

		// Token: 0x04001CC2 RID: 7362
		[ModSensitiveStaticCache(false)]
		public static MutationCategory Morphotypes;

		// Token: 0x04001CC3 RID: 7363
		public static Dictionary<string, Action<XmlDataHelper>> XMLNodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"mutations",
				new Action<XmlDataHelper>(MutationFactory.HandleMutationsNode)
			},
			{
				"category",
				new Action<XmlDataHelper>(MutationFactory.HandleCategoryNode)
			}
		};

		// Token: 0x04001CC4 RID: 7364
		private static bool FilePoolExclude;

		// Token: 0x04001CC5 RID: 7365
		private static bool FileHidden;

		// Token: 0x04001CC6 RID: 7366
		public static MutationCategory _currentParsingCategory;

		// Token: 0x04001CC7 RID: 7367
		private static bool CategoryPoolExclude;

		// Token: 0x04001CC8 RID: 7368
		private static bool CategoryHidden;

		// Token: 0x04001CC9 RID: 7369
		private static bool CategoryDefect;

		// Token: 0x04001CCA RID: 7370
		public static Dictionary<string, Action<XmlDataHelper>> MutationSubnodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"leveltext",
				new Action<XmlDataHelper>(MutationFactory.HandleLevelTextNode)
			},
			{
				"description",
				new Action<XmlDataHelper>(MutationFactory.HandleDescriptionNode)
			}
		};

		// Token: 0x04001CCB RID: 7371
		private static MutationEntry currentMutation;
	}
}
