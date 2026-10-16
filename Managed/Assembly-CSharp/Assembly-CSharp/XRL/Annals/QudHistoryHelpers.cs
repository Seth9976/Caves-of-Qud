using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleLib.Console;
using HistoryKit;
using XRL.Language;
using XRL.Rules;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x020005EA RID: 1514
	public static class QudHistoryHelpers
	{
		// Token: 0x06004126 RID: 16678 RVA: 0x001474E4 File Offset: 0x001456E4
		public static HistoricEntitySnapshot GetRegionalizationParametersSnapshot(History history)
		{
			return history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("name", "unknown").Equals("regionalizationParameters")).GetRandomElement(null).GetSnapshotAtYear(history.currentYear);
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x0014751C File Offset: 0x0014571C
		public static string GetNewRegion(History history, string currentRegion)
		{
			return history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("type", "unknown").Equals("region") && !entity.GetSnapshotAtYear(entity.lastYear).GetProperty("name", "unknown").Equals(currentRegion)).GetRandomElement(null).GetSnapshotAtYear(history.currentYear)
				.GetProperty("name", "unknown");
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x00147568 File Offset: 0x00145768
		public static List<string> GetLocationsInRegion(History history, string region)
		{
			return history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("name", "unknown").Equals(region)).GetRandomElement(null).GetSnapshotAtYear(history.currentYear)
				.GetList("locations");
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x001475B0 File Offset: 0x001457B0
		public static List<string> GetLocationsInRegionByPeriod(History history, string region, int period)
		{
			return history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("name", "unknown").Equals(region) && int.Parse(entity.GetSnapshotAtYear(entity.lastYear).GetProperty("period", "unknown")) <= period).GetRandomElement(null).GetSnapshotAtYear(history.currentYear)
				.GetList("locations");
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x00147600 File Offset: 0x00145800
		public static string GetRandomLocationInRegion(History history, string region)
		{
			return history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("name", "unknown").Equals(region)).GetRandomElement(null).GetSnapshotAtYear(history.currentYear)
				.GetList("locations")
				.GetRandomElement<string>(null);
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x00147650 File Offset: 0x00145850
		public static string GetRandomLocationInRegionByPeriod(History history, string region, int period)
		{
			return history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("name", "unknown").Equals(region) && int.Parse(entity.GetSnapshotAtYear(entity.lastYear).GetProperty("period", "unknown")) <= period).GetRandomElement(null).GetSnapshotAtYear(history.currentYear)
				.GetList("locations")
				.GetRandomElement<string>(null);
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x001476A4 File Offset: 0x001458A4
		public static string GetNewLocationInRegion(History history, string region, string currentLocation)
		{
			HistoricEntityList entitiesByDelegate = history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("type", "unknown").Equals("location") && entity.GetSnapshotAtYear(entity.lastYear).GetProperty("region", "unknown").Equals(region) && !entity.GetSnapshotAtYear(entity.lastYear).GetProperty("name", "unknown").Equals(currentLocation));
			if (entitiesByDelegate.Count > 0)
			{
				return entitiesByDelegate.GetRandomElement(null).GetSnapshotAtYear(history.currentYear).GetProperty("name", "unknown");
			}
			HistoricEntity historicEntity = history.CreateEntity(history.currentYear);
			historicEntity.ApplyEvent(new InitializeLocation(region, 0), long.MinValue);
			return historicEntity.GetCurrentSnapshot().GetProperty("name", "unknown");
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x00147740 File Offset: 0x00145940
		public static string GetNewLocationInRegionByPeriod(History history, string region, string currentLocation, int period)
		{
			return history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("type", "unknown").Equals("location") && entity.GetSnapshotAtYear(entity.lastYear).GetProperty("region", "unknown").Equals(region) && !entity.GetSnapshotAtYear(entity.lastYear).GetProperty("name", "unknown").Equals(currentLocation) && int.Parse(entity.GetSnapshotAtYear(entity.lastYear).GetProperty("period", "unknown")) <= period).GetRandomElement(null).GetSnapshotAtYear(history.currentYear)
				.GetProperty("name", "unknown");
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x0014779C File Offset: 0x0014599C
		public static string GetRegionNewName(History history, string region)
		{
			return history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("name", "unknown").Equals(region)).GetRandomElement(null).GetSnapshotAtYear(history.currentYear)
				.GetProperty("newName", "unknown");
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x001477E8 File Offset: 0x001459E8
		public static string GetRegionNameRoot(History history, string region)
		{
			return history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("name", "unknown").Equals(region)).GetRandomElement(null).GetSnapshotAtYear(history.currentYear)
				.GetProperty("nameRoot", "unknown");
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x00147834 File Offset: 0x00145A34
		public static string GetNewFaction(HistoricEntity entity)
		{
			HistoricEntitySnapshot snapshotAtYear = entity.GetSnapshotAtYear(entity.lastYear);
			string text = Factions.GetRandomOldFaction().Name;
			while (snapshotAtYear.GetList("likedFactions").Contains(text) || snapshotAtYear.GetList("hatedFactions").Contains(text))
			{
				text = Factions.GetRandomOldFaction().Name;
			}
			return text;
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x00147890 File Offset: 0x00145A90
		private static void DoBrightnessCycle(List<char> pattern, SolidColor color1, SolidColor color2)
		{
			int num = 2 * ((color1.DarkTone != null) ? 2 : 1) * ((color1.LightTone != null) ? 2 : 1);
			int num2 = 2 * ((color2.DarkTone != null) ? 2 : 1) * ((color2.LightTone != null) ? 2 : 1);
			int num3 = num + num2;
			int num4 = Math.Max(num, num2);
			int num5 = Stat.Random(0, num4 - 1);
			int num6 = ((num4 >= 6) ? 3 : 2);
			int num7 = ((num4 >= 6) ? 1 : 2);
			for (int i = 0; i < num3; i++)
			{
				SolidColor solidColor = ((i % 2 == 0) ? color1 : color2);
				if (num5 <= num7 && solidColor.DarkTone != null)
				{
					pattern.Add(solidColor.DarkTone.Value);
				}
				else if (num5 >= num6 && solidColor.LightTone != null)
				{
					pattern.Add(solidColor.LightTone.Value);
				}
				else
				{
					pattern.Add(solidColor.Foreground.Value);
				}
				num5 = (num5 + 1) % num4;
			}
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x00147994 File Offset: 0x00145B94
		private static void DoHighlighting(string text, List<char> pattern, SolidColor color, char basic, int highlightChance = 25, int toneChance = 50, int darkToneChance = 50, int maxPatternLength = 10)
		{
			if (text != null && text.Length < maxPatternLength)
			{
				maxPatternLength = text.Length;
			}
			bool flag = false;
			int i = 0;
			int num = maxPatternLength - 1;
			while (i < num)
			{
				if (highlightChance.in100())
				{
					char c = '\0';
					if ((color.DarkTone != null || color.LightTone != null) && toneChance.in100())
					{
						if (color.DarkTone != null && darkToneChance.in100())
						{
							c = color.DarkTone.Value;
						}
						else if (color.LightTone != null)
						{
							c = color.LightTone.Value;
						}
					}
					if (c == '\0')
					{
						c = color.Foreground ?? 'y';
					}
					pattern.Add(c);
					i++;
					flag = true;
				}
				else
				{
					pattern.Add(basic);
				}
				i++;
			}
			if (!flag)
			{
				pattern.Clear();
			}
			pattern.Add(basic);
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x00147A83 File Offset: 0x00145C83
		private static void DoHighlighting(string text, List<char> pattern, SolidColor color, SolidColor basic, int highlightChance = 25, int toneChance = 50, int darkToneChance = 50, int maxPatternLength = 10)
		{
			QudHistoryHelpers.DoHighlighting(text, pattern, color, basic.Foreground.Value, highlightChance, toneChance, darkToneChance, maxPatternLength);
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x00147AA0 File Offset: 0x00145CA0
		private static string ComposePattern(string text, List<char> pattern, string patternType)
		{
			int count = pattern.Count;
			if (count == 0)
			{
				return text;
			}
			if (count != 1)
			{
				string[] array = new string[7];
				array[0] = "{{";
				array[1] = string.Join("-", pattern.Select<char, string>((char c) => char.ToString(c)).ToArray<string>());
				array[2] = " ";
				array[3] = patternType;
				array[4] = "|";
				array[5] = text;
				array[6] = "}}";
				return string.Concat(array);
			}
			return string.Concat(new string[]
			{
				"{{",
				pattern[0].ToString(),
				"|",
				text,
				"}}"
			});
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x00147B64 File Offset: 0x00145D64
		public static string Ansify(string Name, string Article = null)
		{
			MarkupShaders.CheckInit();
			List<SolidColor> list = new List<SolidColor>(16);
			foreach (SolidColor solidColor in MarkupShaders.PickerColors)
			{
				if (solidColor.Background != null)
				{
					char? c = solidColor.Background;
					int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
					int num2 = 107;
					if (!((num.GetValueOrDefault() == num2) & (num != null)))
					{
						continue;
					}
				}
				if (solidColor.Foreground != null)
				{
					char? c = solidColor.Foreground;
					int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
					int num2 = 111;
					if (!((num.GetValueOrDefault() == num2) & (num != null)))
					{
						c = solidColor.Foreground;
						num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
						num2 = 79;
						if (!((num.GetValueOrDefault() == num2) & (num != null)))
						{
							list.Add(solidColor);
						}
					}
				}
			}
			List<SolidColor> list2 = new List<SolidColor>();
			list2.Add(list.GetRandomElement<SolidColor>(null));
			int num3 = 0;
			SolidColor solidColor2;
			do
			{
				solidColor2 = list.GetRandomElement<SolidColor>(null);
			}
			while (list2.Contains(solidColor2) && ++num3 < 100);
			list2.Add(solidColor2);
			num3 = 0;
			do
			{
				solidColor2 = list.GetRandomElement<SolidColor>(null);
			}
			while (list2.Contains(solidColor2) && ++num3 < 100);
			list2.Add(solidColor2);
			string text = Name;
			int num4 = Stat.Random(0, 100);
			List<char> list3 = new List<char>();
			string text2 = "sequence";
			if (num4 < 20)
			{
				QudHistoryHelpers.DoBrightnessCycle(list3, list2[0], list2[1]);
			}
			else if (num4 < 40)
			{
				QudHistoryHelpers.DoHighlighting(Name, list3, list2[0], 'Y', 25, 50, 50, 10);
			}
			else if (num4 < 600)
			{
				QudHistoryHelpers.DoHighlighting(Name, list3, list2[0], list2[1], 25, 50, 50, 10);
			}
			else
			{
				text = "";
				int num5 = Stat.Random(0, 100);
				string[] array = Name.Split(' ', StringSplitOptions.None);
				int i = 0;
				int num6 = array.Length;
				while (i < num6)
				{
					string text3 = array[i];
					if (text3 == "the" || text3 == "The" || text3 == "of")
					{
						text = text + "{{Y|" + text3 + "}}";
					}
					else
					{
						num5 = (50.in100() ? (num5 - 5) : (num5 + 5));
						if (num5 < 20)
						{
							QudHistoryHelpers.DoBrightnessCycle(list3, list2[0], list2[1]);
							text += QudHistoryHelpers.ComposePattern(text3, list3, text2);
							list3.Clear();
						}
						else if (num5 < 40)
						{
							QudHistoryHelpers.DoHighlighting(text3, list3, list2[1], 'Y', 25, 50, 50, 10);
							text += QudHistoryHelpers.ComposePattern(text3, list3, text2);
							list3.Clear();
						}
						else if (num5 < 55)
						{
							QudHistoryHelpers.DoHighlighting(text3, list3, list2[1], list2[0], 25, 50, 50, 10);
							text += QudHistoryHelpers.ComposePattern(text3, list3, text2);
							list3.Clear();
						}
						else if (num5 < 70)
						{
							QudHistoryHelpers.DoHighlighting(text3, list3, list2[2], list2[0], 25, 50, 50, 10);
							text += QudHistoryHelpers.ComposePattern(text3, list3, text2);
							list3.Clear();
						}
						else
						{
							SolidColor solidColor3 = (50.in100() ? list2[0] : list2[1]);
							char c2 = '\0';
							if ((solidColor3.DarkTone != null || solidColor3.LightTone != null) && 50.in100())
							{
								if (solidColor3.DarkTone != null && 50.in100())
								{
									c2 = solidColor3.DarkTone.Value;
								}
								else if (solidColor3.LightTone != null)
								{
									c2 = solidColor3.LightTone.Value;
								}
							}
							if (c2 == '\0')
							{
								c2 = solidColor3.Foreground ?? 'y';
							}
							text = string.Concat(new string[]
							{
								text,
								"{{",
								c2.ToString(),
								"|",
								text3,
								"}}"
							});
						}
					}
					if (i < num6 - 1)
					{
						text += " ";
					}
					i++;
				}
			}
			string text4 = QudHistoryHelpers.ComposePattern(text, list3, text2);
			if (!Article.IsNullOrEmpty())
			{
				text4 = Article + " " + text4;
			}
			return text4;
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x0014806C File Offset: 0x0014626C
		public static string NameItem(string obj, History history, HistoricEntity entity)
		{
			string text;
			do
			{
				text = Grammar.GetWordRoot(obj);
				text += HistoricStringExpander.ExpandString("<spice.items.suffixes.!random>", null, history, null, null);
			}
			while (history.GetEntitiesWherePropertyEquals("name", text).Count > 0);
			return Grammar.InitCap(text);
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x001480B0 File Offset: 0x001462B0
		public static string NameItemNounRoot(string obj, History history, HistoricEntity entity)
		{
			string text;
			do
			{
				if (50.in100())
				{
					text = Grammar.GetWordRoot(obj);
					text += HistoricStringExpander.ExpandString("<spice.items.suffixes.!random>", null, history, null, null);
				}
				else
				{
					text = Grammar.GetRandomMeaningfulWord(obj);
					if (50.in100())
					{
						text = HistoricStringExpander.ExpandString("<spice.items.blessing.!random>", null, history, null, null) + " of " + text;
						if (50.in100())
						{
							text = "the " + text;
						}
					}
					else
					{
						text = Grammar.MakePossessive(text) + " " + HistoricStringExpander.ExpandString("<spice.items.blessing.!random>", null, history, null, null);
					}
					if (text == null)
					{
						text = obj;
					}
				}
			}
			while (history.GetEntitiesWherePropertyEquals("name", text).Count > 0);
			return Grammar.InitCap(text);
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x00148168 File Offset: 0x00146368
		public static string NameItemAdjRoot(string obj, History history, HistoricEntity entity)
		{
			string text;
			do
			{
				if (50.in100())
				{
					text = Grammar.GetWordRoot(obj);
					text += HistoricStringExpander.ExpandString("<spice.items.suffixes.!random>", null, history, null, null);
				}
				else
				{
					text = Grammar.GetRandomMeaningfulWord(obj);
					if (50.in100())
					{
						text = text + " " + HistoricStringExpander.ExpandString("<spice.items.blessing.!random>", null, history, null, null);
						if (50.in100())
						{
							text = "the " + text;
						}
					}
					if (text == null)
					{
						text = obj;
					}
				}
			}
			while (history.GetEntitiesWherePropertyEquals("name", text).Count > 0);
			return Grammar.InitCap(text);
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x001481FC File Offset: 0x001463FC
		public static string GenerateLocationName(string seed, History history)
		{
			string text = seed;
			int i = 50;
			while (i > 0)
			{
				int num = Stat.Random(0, 3);
				if (num == 0)
				{
					text = Grammar.InitCap(Grammar.GetWordRoot(seed)) + "abad";
				}
				else if (num == 1)
				{
					text = Grammar.InitCap(Grammar.GetWordRoot(seed)) + "grad";
				}
				else if (num == 2)
				{
					text = Grammar.InitCap(Grammar.GetWordRoot(seed)) + "platz";
				}
				else
				{
					text = Grammar.GetRandomMeaningfulWord(seed) + " City";
				}
				i--;
				if (history.GetEntitiesWherePropertyEquals("name", text).Count <= 0)
				{
					break;
				}
			}
			return text;
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x001482A0 File Offset: 0x001464A0
		public static string ListAllCognomen(HistoricEntitySnapshot snapSultan)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<string> list = new List<string>(snapSultan.GetList("cognomen"));
			if (list.Count == 0)
			{
				list.Add("the Untitled");
			}
			for (int i = 0; i < list.Count; i++)
			{
				stringBuilder.Append(", " + list[i]);
			}
			stringBuilder.Append(",");
			return stringBuilder.ToString();
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x00148312 File Offset: 0x00146512
		public static HistoricEntityList GetSultans(History History)
		{
			return History.GetEntitiesWherePropertyEquals("type", "sultan");
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x00148324 File Offset: 0x00146524
		public static HistoricEntity GetPeriodSultan(History History, string Period)
		{
			return History.GetEntitiesWherePropertyEquals("type", "sultan").GetEntitiesWherePropertyEquals("period", Period).First<HistoricEntity>();
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x00148346 File Offset: 0x00146546
		public static HistoricEntity GetPeriodSultan(History History, int Period)
		{
			return QudHistoryHelpers.GetPeriodSultan(History, Period.ToString());
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x00148355 File Offset: 0x00146555
		public static HistoricEntity GetRandomSultan(History History)
		{
			return QudHistoryHelpers.GetSultans(History).GetRandomElement(null);
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x00148363 File Offset: 0x00146563
		public static string GetRandomCognomen(HistoricEntitySnapshot snapSultan)
		{
			return snapSultan.GetList("cognomen").GetRandomElement<string>(null) ?? "the Untitled";
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x0014837F File Offset: 0x0014657F
		public static string GetRandomCognomen(History History)
		{
			return QudHistoryHelpers.GetRandomCognomen(QudHistoryHelpers.GetRandomSultan(History).GetCurrentSnapshot());
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x00148394 File Offset: 0x00146594
		public static string GetSultanateEra(HistoricEntitySnapshot snapSultan)
		{
			int num = int.Parse(snapSultan.GetProperty("period", "unknown"));
			if (num == 1 || num == 2)
			{
				return "EarlySultanate";
			}
			if (num == 4 || num == 5)
			{
				return "LateSultanate";
			}
			if (num != 3)
			{
				return "LateSultanate";
			}
			if (If.CoinFlip(null, null))
			{
				return "EarlySultanate";
			}
			return "LateSultanate";
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x001483F4 File Offset: 0x001465F4
		public static string GetMaskName(HistoricEntitySnapshot snapSultan)
		{
			int num = int.Parse(snapSultan.GetProperty("period", "unknown"));
			if (num == 1)
			{
				return "Kesil Face";
			}
			if (num == 2)
			{
				return "Shemesh Face";
			}
			if (num == 3)
			{
				return "Earth Face";
			}
			if (num == 4)
			{
				return "Levant Face";
			}
			if (num == 5)
			{
				return "Olive Face";
			}
			return "Nil Face";
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x00148450 File Offset: 0x00146650
		public static void RenameLocation(string oldLocation, string newLocation, History history)
		{
			history.GetEntitiesWherePropertyEquals("name", oldLocation).ForEach(delegate(HistoricEntity entity)
			{
				entity.ApplyEvent(new SetEntityProperty("name", newLocation), long.MinValue);
			});
			history.GetEntitiesWithListPropertyThatContains("locations", oldLocation).ForEach(delegate(HistoricEntity entity)
			{
				entity.ApplyEvent(new RemoveEntityListItem("locations", oldLocation), long.MinValue);
				entity.ApplyEvent(new AddEntityListItem("locations", newLocation), long.MinValue);
			});
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x001484B7 File Offset: 0x001466B7
		public static string GenerateSultanateYearName()
		{
			return HistoricStringExpander.ExpandString("Year of the <spice.adjectives.!random.capitalize> <spice.nouns.!random.capitalize>", null);
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x001484C4 File Offset: 0x001466C4
		public static Dictionary<string, string> BuildContextFromObjectTextFragments(string blueprintName)
		{
			if (!GameObjectFactory.Factory.Blueprints.ContainsKey(blueprintName))
			{
				return null;
			}
			if (GameObjectFactory.Factory.Blueprints[blueprintName].xTags == null)
			{
				return null;
			}
			if (!GameObjectFactory.Factory.Blueprints[blueprintName].xTags.ContainsKey("TextFragments"))
			{
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in GameObjectFactory.Factory.Blueprints[blueprintName].xTags["TextFragments"].Keys)
			{
				dictionary.Add("*" + text + "*", GameObjectFactory.Factory.Blueprints[blueprintName].xTags["TextFragments"][text].Split(',', StringSplitOptions.None).GetRandomElement<string>(null));
			}
			return dictionary;
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x001485D0 File Offset: 0x001467D0
		public static Dictionary<string, string> BuildContextFromMemberTextFragments(HistoricEntity Village, bool Throw = false)
		{
			string entityProperty = Village.GetEntityProperty("baseFactionMember", -1L);
			if (entityProperty.IsNullOrEmpty())
			{
				if (Throw)
				{
					throw new Exception("Village '" + Village.Name + "' is missing baseFactionMember property.");
				}
				return null;
			}
			else
			{
				Dictionary<string, string> dictionary = QudHistoryHelpers.BuildContextFromObjectTextFragments(entityProperty);
				if (Throw && dictionary.IsNullOrEmpty<KeyValuePair<string, string>>())
				{
					throw new Exception(string.Concat(new string[] { "Village '", Village.Name, "' member '", entityProperty, "' is missing text fragments." }));
				}
				return dictionary;
			}
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x0014865C File Offset: 0x0014685C
		public static string GetRandomProperty(HistoricEntitySnapshot entitySnapshot, string defaultValue, params string[] args)
		{
			if (args.Length == 0)
			{
				return defaultValue;
			}
			List<string> list = new List<string>();
			for (int i = 0; i < args.Length; i++)
			{
				if (entitySnapshot.hasProperty(args[i]))
				{
					list.Add(entitySnapshot.GetProperty(args[i], "unknown"));
				}
				else if (entitySnapshot.listProperties.ContainsKey(args[i]))
				{
					foreach (string text in entitySnapshot.GetList(args[i]))
					{
						list.Add(text);
					}
				}
			}
			if (list.Count == 0)
			{
				return defaultValue;
			}
			return list.GetRandomElement<string>(null);
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x00148710 File Offset: 0x00146910
		public static List<int> GetSpreadOfSultanYears(int yearsInSultanate, int numSultans)
		{
			List<int> list = new List<int>();
			yearsInSultanate = (int)((float)yearsInSultanate * Stat.Random(0.8f, 1.2f));
			int num = yearsInSultanate - numSultans * 200 + numSultans - 1;
			List<int> list2 = new List<int>();
			while (list2.Count < numSultans - 1)
			{
				int num2 = Stat.Random(1, num);
				if (!list2.Contains(num2))
				{
					list2.Add(num2);
				}
			}
			list2.Sort();
			for (int i = 0; i < numSultans; i++)
			{
				if (i == 0)
				{
					list.Add(list2[i] - 1);
				}
				else if (i == numSultans - 1)
				{
					list.Add(num - list2[i - 1]);
				}
				else
				{
					list.Add(list2[i] - list2[i - 1] - 1);
				}
			}
			list.Sort();
			return list;
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x001487DC File Offset: 0x001469DC
		public static string ConvertGospelToSultanateCalendarEra(string gospel, long endOfSultanateDate)
		{
			if (!gospel.Contains("%"))
			{
				return gospel;
			}
			Match match = Regex.Match(gospel, "\\%(.*?)\\%");
			if (match.Success)
			{
				return Regex.Replace(gospel, "\\%.*?\\%", QudHistoryHelpers.ConvertYearToSultanateCalendarEra(long.Parse(match.Groups[1].Value), endOfSultanateDate));
			}
			return gospel;
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x00148838 File Offset: 0x00146A38
		public static string ConvertYearToSultanateCalendarEra(long year, long endOfSultanateDate)
		{
			if (endOfSultanateDate > year)
			{
				return (endOfSultanateDate - year).ToString() + " BR";
			}
			return (year - endOfSultanateDate + 1L).ToString() + " AR";
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x00148878 File Offset: 0x00146A78
		public static int GetReshephGospelXP(int numGospels)
		{
			if (numGospels == 1)
			{
				return 250;
			}
			if (numGospels == 2)
			{
				return 500;
			}
			if (numGospels == 3)
			{
				return 1000;
			}
			if (numGospels == 4)
			{
				return 2000;
			}
			if (numGospels == 5)
			{
				return 3000;
			}
			if (numGospels == 6)
			{
				return 4000;
			}
			if (numGospels == 7)
			{
				return 5000;
			}
			if (numGospels == 8)
			{
				return 6000;
			}
			if (numGospels == 9)
			{
				return 7500;
			}
			if (numGospels == 10)
			{
				return 10000;
			}
			if (numGospels == 11)
			{
				return 12500;
			}
			if (numGospels == 12)
			{
				return 15000;
			}
			if (numGospels == 13)
			{
				return 20000;
			}
			if (numGospels == 14)
			{
				return 25000;
			}
			if (numGospels == 15)
			{
				return 30000;
			}
			return 40000;
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x00148927 File Offset: 0x00146B27
		public static void ExtractArticle(ref string Name, out string Article)
		{
			Article = null;
			if (Name.StartsWith("the ") || Name.StartsWith("The "))
			{
				Name = Name.Substring(4);
				Article = "the";
			}
		}
	}
}
