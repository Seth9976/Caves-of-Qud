using System;
using System.Collections.Generic;
using System.Xml;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Tinkering;

namespace XRL.World
{
	// Token: 0x0200095D RID: 2397
	[HasModSensitiveStaticCache]
	public class ModificationFactory
	{
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060062F7 RID: 25335 RVA: 0x001E6519 File Offset: 0x001E4719
		public static List<ModEntry> ModList
		{
			get
			{
				ModificationFactory.CheckInit();
				return ModificationFactory._ModList;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060062F8 RID: 25336 RVA: 0x001E6525 File Offset: 0x001E4725
		public static Dictionary<string, List<ModEntry>> ModTable
		{
			get
			{
				ModificationFactory.CheckInit();
				return ModificationFactory._ModTable;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060062F9 RID: 25337 RVA: 0x001E6531 File Offset: 0x001E4731
		public static Dictionary<string, ModEntry> ModsByPart
		{
			get
			{
				ModificationFactory.CheckInit();
				return ModificationFactory._ModsByPart;
			}
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x001E6540 File Offset: 0x001E4740
		public static void CheckInit()
		{
			if (ModificationFactory._ModTable == null)
			{
				Loading.LoadTask("Loading Mods.xml", new Action(ModificationFactory.LoadMods), true);
			}
			if (ModificationFactory.rarityCodes == null)
			{
				ModificationFactory.rarityCodes = new List<int>();
				foreach (ModEntry modEntry in ModificationFactory.ModList)
				{
					if (!ModificationFactory.rarityCodes.Contains(modEntry.Rarity))
					{
						ModificationFactory.rarityCodes.Add(modEntry.Rarity);
					}
				}
			}
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x001E65DC File Offset: 0x001E47DC
		private static void LoadMods()
		{
			ModificationFactory._ModList = new List<ModEntry>(128);
			ModificationFactory._ModTable = new Dictionary<string, List<ModEntry>>(32);
			ModificationFactory._ModsByPart = new Dictionary<string, ModEntry>(128);
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("Mods", true, true))
			{
				try
				{
					xmlDataHelper.WhitespaceHandling = WhitespaceHandling.None;
					while (xmlDataHelper.Read())
					{
						if (xmlDataHelper.Name == "mods")
						{
							ModificationFactory.LoadModsNode(xmlDataHelper, false);
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogPotentialModError(xmlDataHelper.modInfo, ex);
				}
			}
			foreach (ModEntry modEntry in ModificationFactory._ModList)
			{
				if (!modEntry.Tables.IsNullOrEmpty())
				{
					foreach (string text in modEntry.Tables.Split(',', StringSplitOptions.None))
					{
						if (!ModificationFactory._ModTable.ContainsKey(text))
						{
							ModificationFactory._ModTable.Add(text, new List<ModEntry>());
						}
						ModificationFactory._ModTable[text].Add(modEntry);
					}
				}
			}
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x001E6740 File Offset: 0x001E4940
		public static void LoadModsNode(XmlTextReader Reader, bool isPrimary = true)
		{
			while (Reader.Read())
			{
				if (Reader.Name == "mod")
				{
					ModificationFactory.LoadModNode(Reader, isPrimary);
				}
			}
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x001E6768 File Offset: 0x001E4968
		public static void LoadModNode(XmlTextReader Reader, bool isPrimary = true)
		{
			string attribute = Reader.GetAttribute("Part");
			ModEntry modEntry;
			if (!ModificationFactory._ModsByPart.TryGetValue(attribute, out modEntry))
			{
				modEntry = new ModEntry
				{
					Part = attribute
				};
				ModificationFactory._ModsByPart.Add(attribute, modEntry);
				ModificationFactory._ModList.Add(modEntry);
			}
			string text = Reader.GetAttribute("MinTier");
			if (!text.IsNullOrEmpty())
			{
				modEntry.MinTier = Convert.ToInt32(text);
			}
			text = Reader.GetAttribute("MaxTier");
			if (!text.IsNullOrEmpty())
			{
				modEntry.MaxTier = Convert.ToInt32(text);
			}
			text = Reader.GetAttribute("NativeTier");
			if (!text.IsNullOrEmpty())
			{
				modEntry.NativeTier = Convert.ToInt32(text);
			}
			text = Reader.GetAttribute("TinkerTier");
			if (!text.IsNullOrEmpty())
			{
				modEntry.TinkerTier = Convert.ToInt32(text);
			}
			text = Reader.GetAttribute("Value");
			if (!text.IsNullOrEmpty())
			{
				modEntry.Value = double.Parse(text);
			}
			text = Reader.GetAttribute("Description");
			if (text != null)
			{
				modEntry.Description = text;
			}
			text = Reader.GetAttribute("TinkerDisplayName");
			if (text != null)
			{
				modEntry.TinkerDisplayName = text;
			}
			text = Reader.GetAttribute("TinkerIngredient");
			if (text != null)
			{
				modEntry.TinkerIngredient = text;
			}
			text = Reader.GetAttribute("Tables");
			if (text != null)
			{
				modEntry.Tables = text;
			}
			text = Reader.GetAttribute("TinkerAllowed");
			if (!text.IsNullOrEmpty())
			{
				modEntry.TinkerAllowed = !text.EqualsNoCase("false");
			}
			text = Reader.GetAttribute("BonusAllowed");
			if (!text.IsNullOrEmpty())
			{
				modEntry.BonusAllowed = !text.EqualsNoCase("false");
			}
			text = Reader.GetAttribute("CanAutoTinker");
			if (!text.IsNullOrEmpty())
			{
				modEntry.CanAutoTinker = !text.EqualsNoCase("false");
			}
			text = Reader.GetAttribute("NoSparkingQuest");
			if (!text.IsNullOrEmpty())
			{
				modEntry.NoSparkingQuest = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("Rarity");
			if (!text.IsNullOrEmpty())
			{
				modEntry.Rarity = ModificationFactory.getRarityCode(text);
			}
			text = Reader.GetAttribute("TinkerCategory");
			if (text != null)
			{
				modEntry.TinkerCategory = text;
			}
			if (Reader.NodeType == XmlNodeType.EndElement || Reader.IsEmptyElement)
			{
				return;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.EndElement && (Reader.Name == "" || Reader.Name == "mod"))
				{
					return;
				}
			}
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x001E69CC File Offset: 0x001E4BCC
		public static int getRarityCode(string rarity)
		{
			if (rarity == "C")
			{
				return 0;
			}
			if (rarity == "U")
			{
				return 1;
			}
			if (rarity == "R")
			{
				return 2;
			}
			if (rarity == "R2")
			{
				return 3;
			}
			if (rarity == "R3")
			{
				return 4;
			}
			throw new Exception("Unknown rarity " + rarity);
		}

		// Token: 0x060062FF RID: 25343 RVA: 0x001E6A34 File Offset: 0x001E4C34
		public static int getBaseRarityWeight(int rarityCode)
		{
			switch (rarityCode)
			{
			case 0:
				return 100000;
			case 1:
				return 40000;
			case 2:
				return 10500;
			case 3:
				return 1500;
			case 4:
				return 150;
			default:
				throw new Exception("unknown rarity code " + rarityCode.ToString());
			}
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x001E6A94 File Offset: 0x001E4C94
		public static int getTierRarityWeight(int modNativeTier, int itemTier)
		{
			int num = 100;
			if (itemTier < modNativeTier)
			{
				num /= (modNativeTier - itemTier) * 5;
			}
			return num;
		}

		// Token: 0x06006301 RID: 25345 RVA: 0x001E6AB4 File Offset: 0x001E4CB4
		private static int fuzzTier(int tier)
		{
			for (;;)
			{
				int num = Stat.Random(1, 100);
				if (num <= 10 && tier > 1)
				{
					tier--;
				}
				else
				{
					if (num > 20 || tier >= 8)
					{
						break;
					}
					tier++;
				}
			}
			return tier;
		}

		// Token: 0x06006302 RID: 25346 RVA: 0x001E6AEC File Offset: 0x001E4CEC
		public static int ApplyModifications(GameObject GO, GameObjectBlueprint Blueprint, int BonusModChance, int SetModNumber, string Context = null)
		{
			int num = 0;
			ModificationFactory.CheckInit();
			if (BonusModChance <= -999 && SetModNumber <= 0)
			{
				return num;
			}
			try
			{
				int num2 = 3;
				int num3;
				if (GO.HasIntProperty("BaseModChance"))
				{
					num2 = GO.GetIntProperty("BaseModChance", 0);
				}
				else if (int.TryParse(GO.GetTag("BaseModChance", null), out num3))
				{
					num2 = num3;
				}
				int num4 = num2 + BonusModChance;
				if (num4 <= 0 && (!The.Core.CheatMaxMod || BonusModChance <= -999) && SetModNumber <= 0)
				{
					return num;
				}
				string tag = Blueprint.GetTag("Mods", "");
				if (tag.IsNullOrEmpty())
				{
					return num;
				}
				List<string> list = tag.CachedCommaExpansion();
				int num5 = 0;
				ModificationFactory.modDist.Clear();
				ModificationFactory.modsEligible.Clear();
				ModificationFactory.rarityCodeWeights.Clear();
				if (XRLCore.Core.CheatMaxMod)
				{
					num4 = 100;
				}
				for (int i = 0; i < 3; i++)
				{
					bool flag = SetModNumber > 0;
					if (flag || num4.in100())
					{
						if (flag)
						{
							SetModNumber--;
						}
						if (num5 == 0)
						{
							num5 = 1;
							string tag2 = Blueprint.GetTag("TechTier", "");
							if (!tag2.IsNullOrEmpty())
							{
								num5 = Convert.ToInt32(tag2);
							}
							else
							{
								string tag3 = Blueprint.GetTag("Tier", "");
								if (!tag3.IsNullOrEmpty())
								{
									num5 = Convert.ToInt32(tag3);
								}
							}
							Tier.Constrain(ref num5);
						}
						ModificationFactory.modDist.Clear();
						ModificationFactory.modsEligible.Clear();
						ModificationFactory.rarityCodeWeights.Clear();
						int num6 = 0;
						foreach (string text in list)
						{
							List<ModEntry> list2;
							if (ModificationFactory._ModTable.TryGetValue(text, out list2))
							{
								foreach (ModEntry modEntry in list2)
								{
									if (num5 >= modEntry.MinTier && num5 <= modEntry.MaxTier && (modEntry.CanAutoTinker || Context != "Tinkering" || (flag && modEntry.BonusAllowed)) && !ModificationFactory.modsEligible.Contains(modEntry) && ItemModding.ModificationApplicable(modEntry.Part, GO, null, null))
									{
										ModificationFactory.modsEligible.Add(modEntry);
									}
								}
							}
						}
						foreach (ModEntry modEntry2 in ModificationFactory.modsEligible)
						{
							if (!ModificationFactory.rarityCodeWeights.ContainsKey(modEntry2.Rarity))
							{
								ModificationFactory.rarityCodeWeights.Add(modEntry2.Rarity, 0);
							}
							Dictionary<int, int> dictionary = ModificationFactory.rarityCodeWeights;
							int rarity = modEntry2.Rarity;
							dictionary[rarity] += ModificationFactory.getTierRarityWeight(modEntry2.NativeTier, num5);
						}
						foreach (ModEntry modEntry3 in ModificationFactory.modsEligible)
						{
							int num7 = ModificationFactory.getBaseRarityWeight(modEntry3.Rarity) * ModificationFactory.getTierRarityWeight(modEntry3.NativeTier, num5) / ModificationFactory.rarityCodeWeights[modEntry3.Rarity];
							num7 = GetModRarityWeightEvent.GetFor(GO, modEntry3, num7);
							if (num7 > 0)
							{
								ModificationFactory.modDist.Add(modEntry3, num7);
								num6 += num7;
							}
						}
						if (ModificationFactory.modDist.Count <= 0)
						{
							break;
						}
						int num8 = 0;
						foreach (int num9 in ModificationFactory.rarityCodes)
						{
							if (!ModificationFactory.rarityCodeWeights.ContainsKey(num9))
							{
								num8 += ModificationFactory.getBaseRarityWeight(num9);
							}
						}
						if (num8 == 0 || Stat.Random(1, num6 + num8) <= num6)
						{
							ModEntry randomElement = ModificationFactory.modDist.GetRandomElement<ModEntry>(null);
							if (randomElement != null && randomElement.Part != null && !GO.HasPart(randomElement.Part) && ItemModding.ApplyModification(GO, randomElement.Part, ModificationFactory.fuzzTier(num5), true, null, false))
							{
								num++;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				XRLCore.LogError(ex);
				MetricsManager.LogException("ApplyModification", ex, "game_exception");
			}
			finally
			{
				ModificationFactory.modDist.Clear();
				ModificationFactory.modsEligible.Clear();
				ModificationFactory.rarityCodeWeights.Clear();
			}
			return num;
		}

		// Token: 0x04002E8B RID: 11915
		[ModSensitiveStaticCache(false)]
		private static List<ModEntry> _ModList;

		// Token: 0x04002E8C RID: 11916
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, List<ModEntry>> _ModTable;

		// Token: 0x04002E8D RID: 11917
		[ModSensitiveStaticCache(false)]
		public static Dictionary<string, ModEntry> _ModsByPart;

		// Token: 0x04002E8E RID: 11918
		[NonSerialized]
		private static List<int> rarityCodes;

		// Token: 0x04002E8F RID: 11919
		[NonSerialized]
		private static List<ModEntry> modsEligible = new List<ModEntry>(32);

		// Token: 0x04002E90 RID: 11920
		[NonSerialized]
		private static Dictionary<ModEntry, int> modDist = new Dictionary<ModEntry, int>(32);

		// Token: 0x04002E91 RID: 11921
		[NonSerialized]
		private static Dictionary<int, int> rarityCodeWeights = new Dictionary<int, int>(4);
	}
}
