using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x02000942 RID: 2370
	[HasGameBasedStaticCache]
	[HasModSensitiveStaticCache]
	public static class Factions
	{
		// Token: 0x06005B6E RID: 23406 RVA: 0x001B9F2C File Offset: 0x001B812C
		public static void CheckInit()
		{
			if (Factions.FactionTable == null)
			{
				Loading.LoadTask("Loading Factions.xml", new Action(Factions.Init), false);
			}
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x001B9F4C File Offset: 0x001B814C
		public static Faction GetZoneHolyFaction(string ZoneID)
		{
			Factions.CheckInit();
			foreach (Faction faction in Factions.FactionList)
			{
				if (faction.HolyPlaces != null && faction.HolyPlaces.Contains(ZoneID))
				{
					return faction;
				}
			}
			return null;
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x001B9FBC File Offset: 0x001B81BC
		public static bool Exists(string Name)
		{
			if (Name == null)
			{
				return false;
			}
			Factions.CheckInit();
			Faction faction;
			return Factions.FactionTable.TryGetValue(Name, out faction);
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x001B9FE8 File Offset: 0x001B81E8
		public static Faction Get(string Name)
		{
			Factions.CheckInit();
			Faction faction;
			if (!Factions.FactionTable.TryGetValue(Name, out faction))
			{
				foreach (Faction faction2 in Factions.FactionList)
				{
					if (faction2.DisplayName == Name)
					{
						return faction2;
					}
				}
				throw new Exception("unknown faction \"" + Name + "\"");
			}
			if (faction == null)
			{
				MetricsManager.LogError("faction table was out of sync for faction name" + Name);
				Factions.FactionTable[Name] = new Faction(Name);
				return Factions.FactionTable[Name];
			}
			return faction;
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x001BA0A4 File Offset: 0x001B82A4
		[Obsolete("use Get(), will be removed after Q2 2024")]
		public static Faction get(string Name)
		{
			return Factions.Get(Name);
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x001BA0AC File Offset: 0x001B82AC
		public static Faction GetIfExists(string Name)
		{
			if (Name == null)
			{
				return null;
			}
			Factions.CheckInit();
			Faction faction;
			if (Factions.FactionTable.TryGetValue(Name, out faction))
			{
				return faction;
			}
			return null;
		}

		// Token: 0x06005B74 RID: 23412 RVA: 0x001BA0D8 File Offset: 0x001B82D8
		public static Faction GetMostLiked(bool VisibleOnly = true)
		{
			Faction faction = Factions.GetIfExists("Beasts");
			float num = float.MinValue;
			foreach (KeyValuePair<string, float> keyValuePair in The.Game.PlayerReputation.ReputationValues)
			{
				Faction ifExists = Factions.GetIfExists(keyValuePair.Key);
				if (ifExists != null && (!VisibleOnly || ifExists.Visible) && keyValuePair.Value > num)
				{
					faction = ifExists;
					num = keyValuePair.Value;
				}
			}
			return faction;
		}

		// Token: 0x06005B75 RID: 23413 RVA: 0x001BA174 File Offset: 0x001B8374
		public static string GetMostLikedFormattedName(bool VisibleOnly = true)
		{
			return Factions.GetMostLiked(VisibleOnly).GetFormattedName();
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x001BA184 File Offset: 0x001B8384
		public static Faction GetMostHated(bool VisibleOnly = true)
		{
			Faction faction = Factions.GetIfExists("Pariahs");
			float num = float.MaxValue;
			foreach (KeyValuePair<string, float> keyValuePair in The.Game.PlayerReputation.ReputationValues)
			{
				Faction ifExists = Factions.GetIfExists(keyValuePair.Key);
				if (ifExists != null && (!VisibleOnly || ifExists.Visible) && keyValuePair.Value < num)
				{
					faction = ifExists;
					num = keyValuePair.Value;
				}
			}
			return faction;
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x001BA220 File Offset: 0x001B8420
		public static Faction GetSecondMostHated(bool VisibleOnly = true)
		{
			Faction faction = Factions.GetIfExists("Pariahs");
			float num = float.MaxValue;
			Faction faction2 = faction;
			float num2 = num;
			foreach (KeyValuePair<string, float> keyValuePair in The.Game.PlayerReputation.ReputationValues)
			{
				Faction ifExists = Factions.GetIfExists(keyValuePair.Key);
				if (ifExists != null && (!VisibleOnly || ifExists.Visible))
				{
					float value = keyValuePair.Value;
					if (value < num)
					{
						faction2 = faction;
						num2 = num;
						faction = ifExists;
						num = value;
					}
					else if (value < num2)
					{
						faction2 = ifExists;
						num2 = value;
					}
				}
			}
			return faction2;
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x001BA2D0 File Offset: 0x001B84D0
		public static string GetMostHatedFormattedName(bool VisibleOnly = true)
		{
			return Factions.GetMostHated(VisibleOnly).GetFormattedName();
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x001BA2DD File Offset: 0x001B84DD
		[Obsolete("use GetIfExists(), will be removed after Q2 2024")]
		public static Faction getIfExists(string Name)
		{
			return Factions.GetIfExists(Name);
		}

		// Token: 0x06005B7A RID: 23418 RVA: 0x001BA2E8 File Offset: 0x001B84E8
		public static Faction GetByDisplayName(string Name)
		{
			if (Name == null)
			{
				return null;
			}
			Factions.CheckInit();
			foreach (Faction faction in Factions.FactionList)
			{
				if (faction.DisplayName == Name)
				{
					return faction;
				}
			}
			return null;
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x001BA354 File Offset: 0x001B8554
		public static bool TryGet(string Name, out Faction Faction)
		{
			if (Name == null)
			{
				Faction = null;
				return false;
			}
			Factions.CheckInit();
			return Factions.FactionTable.TryGetValue(Name, out Faction);
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x001BA36F File Offset: 0x001B856F
		public static IEnumerable<Faction> Loop()
		{
			Factions.CheckInit();
			foreach (Faction faction in Factions.FactionList)
			{
				yield return faction;
			}
			List<Faction>.Enumerator enumerator = default(List<Faction>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x001BA378 File Offset: 0x001B8578
		[Obsolete("use Loop(), will be removed after Q2 2024")]
		public static IEnumerable<Faction> loop()
		{
			Factions.CheckInit();
			foreach (Faction faction in Factions.FactionList)
			{
				yield return faction;
			}
			List<Faction>.Enumerator enumerator = default(List<Faction>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x001BA381 File Offset: 0x001B8581
		public static IReadOnlyList<Faction> GetList()
		{
			Factions.CheckInit();
			return Factions.FactionList;
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x001BA38D File Offset: 0x001B858D
		public static List<string> GetFactionNames()
		{
			Factions.CheckInit();
			return new List<string>(Factions.FactionTable.Keys);
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x001BA3A3 File Offset: 0x001B85A3
		[Obsolete("use GetFactionNames(), will be removed after Q2 2024")]
		public static List<string> getFactionNames()
		{
			return Factions.GetFactionNames();
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x001BA3AC File Offset: 0x001B85AC
		public static List<string> GetVisibleFactionNames()
		{
			Factions.CheckInit();
			return (from kv in Factions.FactionTable
				where kv.Value.Visible
				select kv.Key).ToList<string>();
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x001BA410 File Offset: 0x001B8610
		[Obsolete("use GetVisibleFactionNames(), will be removed after Q2 2024")]
		public static List<string> getVisibleFactionNames()
		{
			return Factions.GetVisibleFactionNames();
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x001BA417 File Offset: 0x001B8617
		public static int GetFactionCount()
		{
			Factions.CheckInit();
			return Factions.FactionList.Count;
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x001BA428 File Offset: 0x001B8628
		[Obsolete("use GetFactionCount(), will be removed after Q2 2024")]
		public static int getFactionCount()
		{
			return Factions.GetFactionCount();
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x001BA42F File Offset: 0x001B862F
		public static void AddNewFaction(Faction NewFaction)
		{
			Factions.CheckInit();
			Factions.FactionTable.Add(NewFaction.Name, NewFaction);
			Factions.FactionList.Add(NewFaction);
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x001BA454 File Offset: 0x001B8654
		public static void Load(SerializationReader Reader)
		{
			int num = Reader.ReadInt32();
			if (Factions.FactionTable == null)
			{
				Factions.FactionTable = new Dictionary<string, Faction>(num);
			}
			if (Factions.FactionList == null)
			{
				Factions.FactionList = new List<Faction>();
			}
			Factions.FactionTable.Clear();
			Factions.FactionTable.EnsureCapacity(num);
			Factions.FactionList.Clear();
			Factions.FactionList.EnsureCapacity<Faction>(num);
			for (int i = 0; i < num; i++)
			{
				string text = Reader.ReadString();
				Faction faction = Reader.ReadComposite<Faction>();
				Factions.FactionTable[text] = faction;
				Factions.FactionList.Add(faction);
			}
			Loading.LoadTask("Loading Factions.xml", new Action(Factions.LoadXml), false);
			Factions.InitAttitudes();
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x001BA504 File Offset: 0x001B8704
		public static void Save(SerializationWriter writer)
		{
			Factions.CheckInit();
			writer.Write(Factions.FactionTable.Count);
			foreach (KeyValuePair<string, Faction> keyValuePair in Factions.FactionTable)
			{
				writer.Write(keyValuePair.Key);
				writer.WriteComposite<Faction>(keyValuePair.Value);
			}
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x001BA580 File Offset: 0x001B8780
		private static void Init()
		{
			Factions.Initializing = true;
			Factions.FactionTable = new Dictionary<string, Faction>(64);
			Factions.FactionList = new List<Faction>(64);
			try
			{
				Factions.LoadXml();
				Factions.InitAttitudes();
			}
			finally
			{
				Factions.Initializing = false;
			}
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x001BA5D0 File Offset: 0x001B87D0
		private static void InitAttitudes()
		{
			Faction faction = Factions.FactionTable["Inanimate"];
			foreach (Faction faction2 in Factions.FactionList)
			{
				if (faction2 != faction && faction2.GetFeelingTowardsFaction(faction.Name, null, 0) != 0)
				{
					faction2.FactionFeeling[faction.Name] = 0;
				}
			}
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x001BA65C File Offset: 0x001B885C
		public static void LoadXml()
		{
			foreach (DataFile dataFile in DataManager.GetXMLFilesWithRoot("Factions"))
			{
				try
				{
					Factions.ProcessFactionXmlFile(dataFile, dataFile.IsMod);
				}
				catch (Exception ex)
				{
					MetricsManager.LogPotentialModError(dataFile.Mod, ex);
				}
			}
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x001BA6DC File Offset: 0x001B88DC
		private static bool IsVisible(Faction Faction)
		{
			return Faction.Visible;
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x001BA6E4 File Offset: 0x001B88E4
		private static bool IsVisibleAndIsOld(Faction Faction)
		{
			return Faction.Visible && Faction.Old;
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x001BA6F6 File Offset: 0x001B88F6
		private static bool IsVisibleAndCanBeExtradimensional(Faction Faction)
		{
			return Faction.Visible && Faction.ExtradimensionalVersions;
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x001BA708 File Offset: 0x001B8908
		private static bool HasAtLeastOneMember(Faction Faction)
		{
			return GameObjectFactory.Factory.AnyFactionMembers(Faction.Name);
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x001BA71A File Offset: 0x001B891A
		public static Faction GetRandomFaction()
		{
			Factions.CheckInit();
			return Factions.FactionTable.Values.Where<Faction>(new Func<Faction, bool>(Factions.IsVisible)).GetRandomElement<Faction>(null);
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x001BA744 File Offset: 0x001B8944
		public static Faction GetRandomFaction(Predicate<Faction> Filter)
		{
			if (Filter == null)
			{
				return Factions.GetRandomFaction();
			}
			Factions.CheckInit();
			return Factions.FactionTable.Values.Where<Faction>((Faction f) => Factions.IsVisible(f) && Filter(f)).GetRandomElement<Faction>(null);
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x001BA794 File Offset: 0x001B8994
		public static Faction GetRandomFaction(string Exception)
		{
			Factions.CheckInit();
			return Factions.FactionTable.Values.Where<Faction>((Faction f) => Factions.IsVisible(f) && f.Name != Exception).GetRandomElement<Faction>(null);
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x001BA7D4 File Offset: 0x001B89D4
		public static Faction GetRandomFaction(string[] Exceptions)
		{
			Factions.CheckInit();
			return Factions.FactionTable.Values.Where<Faction>((Faction f) => Factions.IsVisible(f) && Array.IndexOf<string>(Exceptions, f.Name) == -1).GetRandomElement<Faction>(null);
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x001BA814 File Offset: 0x001B8A14
		public static Faction GetRandomPotentiallyExtradimensionalFaction()
		{
			Factions.CheckInit();
			return Factions.FactionTable.Values.Where<Faction>(new Func<Faction, bool>(Factions.IsVisibleAndCanBeExtradimensional)).GetRandomElement<Faction>(null);
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x001BA83C File Offset: 0x001B8A3C
		public static Faction GetRandomOldFaction()
		{
			Factions.CheckInit();
			return Factions.FactionTable.Values.Where<Faction>(new Func<Faction, bool>(Factions.IsVisibleAndIsOld)).GetRandomElement<Faction>(null);
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x001BA864 File Offset: 0x001B8A64
		public static Faction GetRandomOldFaction(string Exception)
		{
			Factions.CheckInit();
			return Factions.FactionTable.Values.Where<Faction>((Faction f) => Factions.IsVisibleAndIsOld(f) && f.Name != Exception).GetRandomElement<Faction>(null);
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x001BA8A4 File Offset: 0x001B8AA4
		public static Faction GetRandomFactionWithAtLeastOneMember()
		{
			return Factions.GetRandomFaction(new Predicate<Faction>(Factions.HasAtLeastOneMember));
		}

		// Token: 0x06005B97 RID: 23447 RVA: 0x001BA8B8 File Offset: 0x001B8AB8
		public static Faction GetRandomFactionWithAtLeastOneMember(Predicate<Faction> Filter)
		{
			if (Filter == null)
			{
				return Factions.GetRandomFactionWithAtLeastOneMember();
			}
			return Factions.GetRandomFaction((Faction f) => Factions.HasAtLeastOneMember(f) && Filter(f));
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x001BA8F4 File Offset: 0x001B8AF4
		public static int GetFeelingFactionToFaction(string Faction1, string Faction2)
		{
			if (Faction1 == Faction2)
			{
				return 100;
			}
			int num;
			try
			{
				num = Factions.Get(Faction1).GetFeelingTowardsFaction(Faction2, null, 0);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error with faction " + Faction1, ex, "game_exception");
				num = 0;
			}
			return num;
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x001BA954 File Offset: 0x001B8B54
		public static int GetFeelingFactionToObject(string Faction, GameObject Object, IDictionary<string, int> Override = null)
		{
			int num;
			try
			{
				num = Factions.Get(Faction).GetFeelingTowardsObject(Object, Override);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error with faction " + Faction, ex, "game_exception");
				num = 0;
			}
			return num;
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x001BA9A0 File Offset: 0x001B8BA0
		public static bool IsPettable(string Faction)
		{
			return Factions.Get(Faction).Pettable;
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x001BA9B0 File Offset: 0x001B8BB0
		private static void ProcessFactionXmlFile(string File, bool Mod)
		{
			using (XmlTextReader streamingAssetsXMLStream = DataManager.GetStreamingAssetsXMLStream(File))
			{
				streamingAssetsXMLStream.WhitespaceHandling = WhitespaceHandling.None;
				while (streamingAssetsXMLStream.Read())
				{
					if (streamingAssetsXMLStream.Name == "factions")
					{
						Factions.LoadFactionsNode(streamingAssetsXMLStream, Mod);
					}
					if ((streamingAssetsXMLStream.IsEmptyElement || streamingAssetsXMLStream.NodeType == XmlNodeType.EndElement) && streamingAssetsXMLStream.Name == "factions")
					{
						break;
					}
				}
				streamingAssetsXMLStream.Close();
			}
		}

		// Token: 0x06005B9C RID: 23452 RVA: 0x001BAA34 File Offset: 0x001B8C34
		private static void LoadFactionsNode(XmlTextReader Reader, bool Mod = false)
		{
			while (Reader.Read())
			{
				if (Reader.Name == "faction")
				{
					Factions.LoadFactionNode(Reader, Mod);
				}
				else if (Reader.Name == "removefaction")
				{
					string attribute = Reader.GetAttribute("Name");
					if (attribute.IsNullOrEmpty())
					{
						throw new Exception("removefaction tag had no Name attribute");
					}
					if (Factions.FactionTable.ContainsKey(attribute))
					{
						Factions.FactionTable.Remove(attribute);
					}
				}
			}
		}

		// Token: 0x06005B9D RID: 23453 RVA: 0x001BAAB0 File Offset: 0x001B8CB0
		private static void LoadFactionNode(XmlTextReader Reader, bool Mod = false)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute.IsNullOrEmpty())
			{
				throw new Exception("faction tag had no Name attribute");
			}
			Faction faction;
			if (Factions.FactionTable.ContainsKey(attribute))
			{
				if (!Mod && !Factions.Initializing)
				{
					Reader.SkipToEnd();
					return;
				}
				if (Reader.GetAttribute("Load") == "Replace" && Factions.Initializing)
				{
					faction = new Faction(attribute);
					Factions.FactionTable[attribute] = faction;
				}
				else
				{
					faction = Factions.Get(attribute);
				}
			}
			else
			{
				faction = new Faction(attribute);
				Factions.AddNewFaction(faction);
			}
			string text = Reader.GetAttribute("DisplayName");
			if (!text.IsNullOrEmpty())
			{
				faction.DisplayName = text;
			}
			text = Reader.GetAttribute("Parent");
			if (!text.IsNullOrEmpty())
			{
				faction.SetParent(text);
			}
			text = Reader.GetAttribute("ExtradimensionalVersions");
			if (!text.IsNullOrEmpty())
			{
				faction.ExtradimensionalVersions = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("FormatWithArticle");
			if (!text.IsNullOrEmpty())
			{
				faction.FormatWithArticle = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("HatesPlayer");
			if (!text.IsNullOrEmpty())
			{
				faction.HatesPlayer = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("Old");
			if (!text.IsNullOrEmpty())
			{
				faction.Old = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("InitialPlayerReputation");
			if (!text.IsNullOrEmpty())
			{
				faction.InitialPlayerReputation = Factions.TryInt(text, "player initial reputation");
			}
			text = Reader.GetAttribute("Pettable");
			if (!text.IsNullOrEmpty())
			{
				faction.Pettable = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("Plural");
			if (!text.IsNullOrEmpty())
			{
				faction.Plural = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("Visible");
			if (!text.IsNullOrEmpty())
			{
				faction.Visible = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("PositiveSound");
			if (!text.IsNullOrEmpty())
			{
				faction.PositiveSound = text;
			}
			text = Reader.GetAttribute("NegativeSound");
			if (!text.IsNullOrEmpty())
			{
				faction.NegativeSound = text;
			}
			text = Reader.GetAttribute("HistoricalSignificance");
			if (!text.IsNullOrEmpty())
			{
				faction.HistoricalSignificance = Factions.TryInt(text, "historical significance");
			}
			text = Reader.GetAttribute("DefaultAddress");
			if (!text.IsNullOrEmpty())
			{
				faction.DefaultAddress = text;
			}
			text = Reader.GetAttribute("RankTerm");
			if (!text.IsNullOrEmpty())
			{
				faction.RankTerm = text;
			}
			text = Reader.GetAttribute("EmblemTile");
			if (!text.IsNullOrEmpty())
			{
				faction.Emblem.Tile = text;
			}
			text = Reader.GetAttribute("EmblemTileColor");
			if (!text.IsNullOrEmpty())
			{
				faction.Emblem.TileColor = "&" + text;
			}
			text = Reader.GetAttribute("EmblemDetailColor");
			if (!text.IsNullOrEmpty())
			{
				faction.Emblem.DetailColor = text[0];
			}
			if (!Reader.IsEmptyElement)
			{
				Factions.LoadFactionChildNodes(faction, Reader, Mod);
			}
			faction.FactionFeeling.TryAdd("Player", Reputation.GetFeeling((float)faction.InitialPlayerReputation));
			faction.InheritParent();
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x001BADD8 File Offset: 0x001B8FD8
		private static void LoadFactionChildNodes(Faction Entry, XmlTextReader Reader, bool Mod = false)
		{
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (Reader.Name == "feeling")
					{
						string attribute = Reader.GetAttribute("About");
						int num;
						if (int.TryParse(Reader.GetAttribute("Value"), out num))
						{
							Entry.TryAddFactionFeeling(attribute, num);
						}
						else
						{
							MetricsManager.LogError("Invalid faction feeling: " + Entry.Name + " to " + attribute);
						}
					}
					else
					{
						if (Reader.Name == "partreputation")
						{
							string attribute2 = Reader.GetAttribute("About");
							string attribute3 = Reader.GetAttribute("Value");
							try
							{
								Entry.SetPartReputation(attribute2, Convert.ToInt32(attribute3));
								goto IL_0620;
							}
							catch (Exception ex)
							{
								MetricsManager.LogException("Error reading faction part reputation", ex, "game_exception");
								goto IL_0620;
							}
						}
						if (Reader.Name == "interests")
						{
							Factions.LoadInterestsNode(Entry, Reader, Mod);
						}
						else if (Reader.Name == "ranks")
						{
							Factions.LoadRanksNode(Entry, Reader, Mod);
						}
						else if (Reader.Name == "wellknownworshippables")
						{
							Factions.LoadWellKnownWorshippablesNode(Entry, Reader, Mod);
						}
						else if (Reader.Name == "factionworshipattitudes")
						{
							Factions.LoadFactionWorshipAttitudesNode(Entry, Reader, Mod);
						}
						else if (Reader.Name == "property")
						{
							Entry.SetProperty(Reader.GetAttribute("Name"), Reader.GetAttribute("Value"));
						}
						else if (Reader.Name == "intproperty")
						{
							Entry.SetProperty(Reader.GetAttribute("Name"), int.Parse(Reader.GetAttribute("Value")));
						}
						else if (Reader.Name == "floatproperty")
						{
							Entry.SetProperty(Reader.GetAttribute("Name"), float.Parse(Reader.GetAttribute("Value")));
						}
						else if (Reader.Name == "waterritual")
						{
							string text = Reader.GetAttribute("Liquid");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualLiquid = text;
							}
							text = Reader.GetAttribute("Skill");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualSkill = text;
							}
							text = Reader.GetAttribute("SkillCost");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualSkillCost = Factions.TryInt(text, "water ritual skill cost");
							}
							text = Reader.GetAttribute("BuyMostValuableItem");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualBuyMostValuableItem = text.EqualsNoCase("true");
							}
							text = Reader.GetAttribute("FungusInfect");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualFungusInfect = Factions.TryInt(text, "water ritual fungus infect");
							}
							text = Reader.GetAttribute("HermitOath");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualHermitOath = Factions.TryInt(text, "water ritual hermit oath");
							}
							text = Reader.GetAttribute("SkillPointAmount");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualSkillPointAmount = Factions.TryInt(text, "water ritual skill point amount");
							}
							text = Reader.GetAttribute("SkillPointCost");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualSkillPointCost = Factions.TryInt(text, "water ritual skill point cost");
							}
							text = Reader.GetAttribute("Mutation");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualMutation = text;
							}
							text = Reader.GetAttribute("MutationCost");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualMutationCost = Factions.TryInt(text, "water ritual mutation cost");
							}
							text = Reader.GetAttribute("Gifts");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualGifts = text;
							}
							text = Reader.GetAttribute("Items");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualItems = text;
							}
							text = Reader.GetAttribute("ItemBlueprint");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualItemBlueprint = text;
							}
							text = Reader.GetAttribute("ItemCost");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualItemCost = Factions.TryInt(text, "water ritual item cost");
							}
							text = Reader.GetAttribute("Blueprints");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualBlueprints = text;
							}
							text = Reader.GetAttribute("Recipe");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualRecipe = text;
							}
							text = Reader.GetAttribute("RecipeText");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualRecipeText = text;
							}
							text = Reader.GetAttribute("RecipeGenotype");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualRecipeGenotype = text;
							}
							text = Reader.GetAttribute("Join");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualJoin = text.EqualsNoCase("true");
							}
							text = Reader.GetAttribute("RandomMentalMutation");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualRandomMentalMutation = Factions.TryInt(text, "water ritual random mental mutation");
							}
							text = Reader.GetAttribute("AltBehaviorPart");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltBehaviorPart = text;
							}
							text = Reader.GetAttribute("AltBehaviorTag");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltBehaviorTag = text;
							}
							text = Reader.GetAttribute("AltLiquid");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltLiquid = text;
							}
							text = Reader.GetAttribute("AltSkill");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltSkill = text;
							}
							text = Reader.GetAttribute("AltSkillCost");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltSkillCost = Factions.TryInt(text, "water ritual alt skill cost");
							}
							text = Reader.GetAttribute("AltGifts");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltGifts = text;
							}
							text = Reader.GetAttribute("AltItems");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltItems = text;
							}
							text = Reader.GetAttribute("AltItemBlueprint");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltItemBlueprint = text;
							}
							text = Reader.GetAttribute("AltItemCost");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltItemCost = Factions.TryInt(text, "water ritual alt item cost");
							}
							text = Reader.GetAttribute("AltSkill");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltSkill = text;
							}
							text = Reader.GetAttribute("AltSkillCost");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltSkillCost = Factions.TryInt(text, "water ritual alt skill cost");
							}
							text = Reader.GetAttribute("AltBlueprints");
							if (!text.IsNullOrEmpty())
							{
								Entry.WaterRitualAltBlueprints = text;
							}
						}
						else if (Reader.Name == "holyplace")
						{
							string text = Reader.GetAttribute("ZoneID");
							if (!text.IsNullOrEmpty() && !Entry.HolyPlaces.Contains(text))
							{
								Entry.HolyPlaces.Add(text);
							}
						}
					}
				}
				IL_0620:
				if (Reader.NodeType == XmlNodeType.EndElement && Reader.Name == "faction")
				{
					break;
				}
			}
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x001BB43C File Offset: 0x001B963C
		private static void LoadInterestsNode(Faction Entry, XmlTextReader Reader, bool Mod = false)
		{
			string text = Reader.GetAttribute("BuyTargetedSecrets");
			if (!text.IsNullOrEmpty())
			{
				Entry.BuyTargetedSecrets = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("SellTargetedSecrets");
			if (!text.IsNullOrEmpty())
			{
				Entry.SellTargetedSecrets = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("BuyDescription");
			if (!text.IsNullOrEmpty())
			{
				Entry.BuyDescription = text;
			}
			text = Reader.GetAttribute("SellDescription");
			if (!text.IsNullOrEmpty())
			{
				Entry.SellDescription = text;
			}
			text = Reader.GetAttribute("BothDescription");
			if (!text.IsNullOrEmpty())
			{
				Entry.BothDescription = text;
			}
			text = Reader.GetAttribute("Blurb");
			if (!text.IsNullOrEmpty())
			{
				Entry.InterestsBlurb = text;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element && Reader.Name == "interest")
				{
					Factions.LoadInterestNode(Entry, Reader, Mod);
				}
				if ((Reader.IsEmptyElement || Reader.NodeType == XmlNodeType.EndElement) && Reader.Name == "interests")
				{
					break;
				}
			}
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x001BB550 File Offset: 0x001B9750
		private static void LoadInterestNode(Faction Entry, XmlTextReader Reader, bool Mod = false)
		{
			FactionInterest factionInterest = new FactionInterest();
			factionInterest.Tags = Reader.GetAttribute("Tags");
			string text = Reader.GetAttribute("WillBuy");
			if (!text.IsNullOrEmpty())
			{
				factionInterest.WillBuy = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("WillSell");
			if (!text.IsNullOrEmpty())
			{
				factionInterest.WillSell = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("MatchAny");
			if (!text.IsNullOrEmpty())
			{
				factionInterest.MatchAny = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("Inverse");
			if (!text.IsNullOrEmpty())
			{
				factionInterest.Inverse = text.EqualsNoCase("true");
			}
			text = Reader.GetAttribute("Description");
			if (!text.IsNullOrEmpty())
			{
				factionInterest.Description = text;
			}
			text = Reader.GetAttribute("Weight");
			int num;
			if (!text.IsNullOrEmpty() && int.TryParse(text, out num))
			{
				factionInterest.Weight = num;
			}
			factionInterest.SourceFileName = "Factions.xml";
			factionInterest.SourceLineNumber = Reader.LineNumber;
			factionInterest.SourceWasMod = Mod;
			Entry.AddInterestIfNew(factionInterest);
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x001BB670 File Offset: 0x001B9870
		private static void LoadRanksNode(Faction Entry, XmlTextReader Reader, bool Mod = false)
		{
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element && Reader.Name == "rank")
				{
					Factions.LoadRankNode(Entry, Reader, Mod);
				}
				if ((Reader.IsEmptyElement || Reader.NodeType == XmlNodeType.EndElement) && Reader.Name == "ranks")
				{
					break;
				}
			}
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x001BB6D0 File Offset: 0x001B98D0
		private static void LoadRankNode(Faction Entry, XmlTextReader Reader, bool Mod = false)
		{
			string attribute = Reader.GetAttribute("Name");
			if (!attribute.IsNullOrEmpty())
			{
				if (!Entry.Ranks.Contains(attribute))
				{
					Entry.Ranks.Add(attribute);
				}
				string attribute2 = Reader.GetAttribute("Standing");
				if (!attribute2.IsNullOrEmpty())
				{
					Entry.RankStandings[attribute] = Factions.TryInt(attribute2, "rank standing");
					return;
				}
				Entry.RankStandings[attribute] = Entry.GetHighestStanding() + 1;
			}
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x001BB74C File Offset: 0x001B994C
		private static void LoadWellKnownWorshippablesNode(Faction Entry, XmlTextReader Reader, bool Mod = false)
		{
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (Reader.Name == "wellknownworshippable")
					{
						Factions.LoadWellKnownWorshippableNode(Entry, Reader, Mod);
					}
					else if (Reader.Name == "removewellknownworshippable")
					{
						Factions.LoadRemoveWellKnownWorshippableNode(Entry, Reader, Mod);
					}
				}
				if ((Reader.IsEmptyElement || Reader.NodeType == XmlNodeType.EndElement) && Reader.Name == "wellknownworshippables")
				{
					break;
				}
			}
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x001BB7C8 File Offset: 0x001B99C8
		private static void LoadWellKnownWorshippableNode(Faction Entry, XmlTextReader Reader, bool Mod = false)
		{
			string attribute = Reader.GetAttribute("Name");
			if (!attribute.IsNullOrEmpty())
			{
				Worshippable worshippable = Entry.FindWorshippable(attribute);
				if (worshippable == null)
				{
					worshippable = new Worshippable();
					worshippable.Name = attribute;
					worshippable.Faction = Entry.Name;
					Entry.Worshippables.Add(worshippable);
				}
				string attribute2 = Reader.GetAttribute("Power");
				if (!attribute2.IsNullOrEmpty())
				{
					worshippable.Power = Factions.TryInt(attribute2, "worship power");
				}
			}
		}

		// Token: 0x06005BA5 RID: 23461 RVA: 0x001BB840 File Offset: 0x001B9A40
		private static void LoadRemoveWellKnownWorshippableNode(Faction Entry, XmlTextReader Reader, bool Mod = false)
		{
			string attribute = Reader.GetAttribute("Name");
			if (!attribute.IsNullOrEmpty())
			{
				Worshippable worshippable = Entry.FindWorshippable(attribute);
				if (worshippable != null)
				{
					Entry.Worshippables.Remove(worshippable);
				}
			}
		}

		// Token: 0x06005BA6 RID: 23462 RVA: 0x001BB87C File Offset: 0x001B9A7C
		private static void LoadFactionWorshipAttitudesNode(Faction Entry, XmlTextReader Reader, bool Mod = false)
		{
			string attribute = Reader.GetAttribute("Default");
			if (attribute != null)
			{
				if (attribute == "")
				{
					Entry.DefaultFactionWorshipAttitude = 0;
					Entry.DefaultFactionWorshipAttitudeSet = false;
				}
				else
				{
					Entry.DefaultFactionWorshipAttitude = Factions.TryInt(attribute, "default faction worship attitude");
					Entry.DefaultFactionWorshipAttitudeSet = true;
				}
			}
			string attribute2 = Reader.GetAttribute("ApplyDefaultAfterSpecificFeelings");
			if (!attribute2.IsNullOrEmpty())
			{
				Entry.ApplyDefaultFactionWorshipAttitudeAfterSpecificFeelings = attribute2.EqualsNoCase("true");
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (Reader.Name == "factionworshipattitude")
					{
						Factions.LoadFactionWorshipAttitudeNode(Entry, Reader, Mod);
					}
					else if (Reader.Name == "removefactionworshipattitude")
					{
						Factions.LoadRemoveFactionWorshipAttitudeNode(Entry, Reader, Mod);
					}
				}
				if ((Reader.IsEmptyElement || Reader.NodeType == XmlNodeType.EndElement) && Reader.Name == "factionworshipattitudes")
				{
					break;
				}
			}
		}

		// Token: 0x06005BA7 RID: 23463 RVA: 0x001BB960 File Offset: 0x001B9B60
		private static void LoadFactionWorshipAttitudeNode(Faction Entry, XmlTextReader Reader, bool Mod = false)
		{
			string attribute = Reader.GetAttribute("Name");
			if (!attribute.IsNullOrEmpty())
			{
				Entry.FactionWorshipAttitudes[attribute] = Factions.TryInt(Reader.GetAttribute("Attitude"), "faction worship attitude");
			}
		}

		// Token: 0x06005BA8 RID: 23464 RVA: 0x001BB9A4 File Offset: 0x001B9BA4
		private static void LoadRemoveFactionWorshipAttitudeNode(Faction Entry, XmlTextReader Reader, bool Mod = false)
		{
			string attribute = Reader.GetAttribute("Name");
			if (!attribute.IsNullOrEmpty())
			{
				Entry.FactionWorshipAttitudes.Remove(attribute);
			}
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x001BB9D4 File Offset: 0x001B9BD4
		private static int TryInt(string Spec, string What)
		{
			try
			{
				return Convert.ToInt32(Spec);
			}
			catch
			{
				MetricsManager.LogError("Error in " + What + ": " + Spec);
			}
			return -1;
		}

		// Token: 0x06005BAA RID: 23466 RVA: 0x001BBA18 File Offset: 0x001B9C18
		public static void RequireCachedHeirlooms()
		{
			foreach (Faction faction in Factions.Loop())
			{
				faction.RequireCachedHeirloom();
			}
		}

		// Token: 0x06005BAB RID: 23467 RVA: 0x001BBA64 File Offset: 0x001B9C64
		public static void RegisterWorshippable(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return;
			}
			if (!Object.HasPropertyOrTag("Worshippable"))
			{
				return;
			}
			if (Object.HasIntProperty("WorshipRegistered"))
			{
				return;
			}
			string text = Object.GetPropertyOrTag("WorshipFaction", null) ?? Object.GetPrimaryFaction(false);
			if (text.IsNullOrEmpty())
			{
				return;
			}
			Faction ifExists = Factions.GetIfExists(text);
			if (ifExists == null)
			{
				return;
			}
			ifExists.RegisterWorshippable(Object);
		}

		// Token: 0x06005BAC RID: 23468 RVA: 0x001BBAC9 File Offset: 0x001B9CC9
		public static Worshippable RegisterWorshippable(string Name, string Faction, int Power = 0)
		{
			Faction ifExists = Factions.GetIfExists(Faction);
			if (ifExists == null)
			{
				return null;
			}
			return ifExists.RegisterWorshippable(Name, Power);
		}

		// Token: 0x06005BAD RID: 23469 RVA: 0x001BBAE0 File Offset: 0x001B9CE0
		public static List<Worshippable> GetWorshippables()
		{
			List<Worshippable> list = new List<Worshippable>();
			foreach (Faction faction in Factions.Loop())
			{
				list.AddRange(faction.GetWorshippables());
			}
			list.Sort(new Comparison<Worshippable>(Worshippable.Sort));
			return list;
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x001BBB4C File Offset: 0x001B9D4C
		public static Worshippable FindWorshippable(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return null;
			}
			if (!Object.HasPropertyOrTag("Worshippable"))
			{
				return null;
			}
			string text = Object.GetPropertyOrTag("WorshipFaction", null) ?? Object.GetPrimaryFaction(false);
			if (text.IsNullOrEmpty())
			{
				return null;
			}
			Faction ifExists = Factions.GetIfExists(text);
			if (ifExists == null)
			{
				return null;
			}
			return ifExists.FindWorshippable(Object);
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x001BBBA7 File Offset: 0x001B9DA7
		public static Worshippable FindWorshippable(string Faction, string Name)
		{
			Faction ifExists = Factions.GetIfExists(Faction);
			if (ifExists == null)
			{
				return null;
			}
			return ifExists.FindWorshippable(Name);
		}

		// Token: 0x04002D37 RID: 11575
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		private static Dictionary<string, Faction> FactionTable;

		// Token: 0x04002D38 RID: 11576
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		private static List<Faction> FactionList;

		// Token: 0x04002D39 RID: 11577
		private static bool Initializing;
	}
}
