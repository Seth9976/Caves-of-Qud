using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HistoryKit;
using XRL;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace Qud.API
{
	// Token: 0x02000408 RID: 1032
	public static class HistoryAPI
	{
		// Token: 0x060026DB RID: 9947 RVA: 0x000C8B84 File Offset: 0x000C6D84
		public static HistoricEntitySnapshot GetEntityCurrentSnapshot(string id)
		{
			XRLCore core = XRLCore.Core;
			if (core == null)
			{
				return null;
			}
			XRLGame game = core.Game;
			if (game == null)
			{
				return null;
			}
			History sultanHistory = game.sultanHistory;
			if (sultanHistory == null)
			{
				return null;
			}
			HistoricEntity entity = sultanHistory.GetEntity(id);
			if (entity == null)
			{
				return null;
			}
			return entity.GetCurrentSnapshot();
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x000C8BB8 File Offset: 0x000C6DB8
		public static string GetEntityName(string id)
		{
			if (string.Equals(id, "Joppa"))
			{
				return "Joppa";
			}
			if (string.Equals(id, "The Yd Freehold"))
			{
				return "the Yd Freehold";
			}
			if (string.Equals(id, "Kyakukya"))
			{
				return "Kyakukya";
			}
			XRLCore core = XRLCore.Core;
			if (core == null)
			{
				return null;
			}
			XRLGame game = core.Game;
			if (game == null)
			{
				return null;
			}
			History sultanHistory = game.sultanHistory;
			if (sultanHistory == null)
			{
				return null;
			}
			HistoricEntity entity = sultanHistory.GetEntity(id);
			if (entity == null)
			{
				return null;
			}
			HistoricEntitySnapshot currentSnapshot = entity.GetCurrentSnapshot();
			if (currentSnapshot == null)
			{
				return null;
			}
			return currentSnapshot.GetProperty("name", "unknown");
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000C8C45 File Offset: 0x000C6E45
		public static HistoricEvent GetEvent(long id)
		{
			XRLCore core = XRLCore.Core;
			if (core == null)
			{
				return null;
			}
			XRLGame game = core.Game;
			if (game == null)
			{
				return null;
			}
			History sultanHistory = game.sultanHistory;
			if (sultanHistory == null)
			{
				return null;
			}
			return sultanHistory.GetEvent(id);
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000C8C70 File Offset: 0x000C6E70
		public static List<HistoricEntity> GetSultans()
		{
			XRLCore core = XRLCore.Core;
			History history;
			if (core == null)
			{
				history = null;
			}
			else
			{
				XRLGame game = core.Game;
				history = ((game != null) ? game.sultanHistory : null);
			}
			History history2 = history;
			if (history2 == null)
			{
				return new List<HistoricEntity>();
			}
			HistoricEntityList entitiesWithProperty = history2.GetEntitiesWithProperty("isCandidate");
			if (entitiesWithProperty == null)
			{
				return new List<HistoricEntity>();
			}
			return entitiesWithProperty.entities;
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000C8CBF File Offset: 0x000C6EBF
		public static List<string> GetSultanHatedFactions(HistoricEntity sultan)
		{
			return sultan.GetCurrentSnapshot().GetList("hatedFactions");
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x000C8CD1 File Offset: 0x000C6ED1
		public static List<string> GetSultanLikedFactions(HistoricEntity sultan)
		{
			return sultan.GetCurrentSnapshot().GetList("likedFactions");
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000C8CE3 File Offset: 0x000C6EE3
		public static List<HistoricEntity> GetVillages()
		{
			return new List<HistoricEntity>(XRLCore.Core.Game.sultanHistory.GetEntitiesWherePropertyEquals("type", "village").entities);
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000C8D10 File Offset: 0x000C6F10
		public static List<HistoricEntity> GetKnownVillages()
		{
			List<HistoricEntity> list = new List<HistoricEntity>();
			using (List<HistoricEntity>.Enumerator enumerator = HistoryAPI.GetVillages().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					HistoricEntity entity = enumerator.Current;
					if (JournalAPI.VillageNotes.Any<JournalVillageNote>((JournalVillageNote note) => note.Revealed && note.VillageID == entity.id))
					{
						list.Add(entity);
					}
				}
			}
			return list;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000C8D94 File Offset: 0x000C6F94
		public static HistoricEntitySnapshot GetVillageSnapshot(string Faction)
		{
			History sultanHistory = The.Game.sultanHistory;
			Faction faction;
			HistoricEntity historicEntity;
			if (Factions.TryGet(Faction, out faction) && !faction.EntityID.IsNullOrEmpty() && sultanHistory.TryGetEntity(faction.EntityID, out historicEntity))
			{
				return historicEntity.GetCurrentSnapshot();
			}
			return null;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000C8DDB File Offset: 0x000C6FDB
		public static string ExpandVillageText(string Text, string Faction = null, HistoricEntitySnapshot Snapshot = null)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(Text);
			HistoryAPI.ExpandVillageText(stringBuilder, Faction, Snapshot);
			return stringBuilder.ToString();
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000C8DF0 File Offset: 0x000C6FF0
		public static void ExpandVillageText(StringBuilder Text, string Faction = null, HistoricEntitySnapshot Snapshot = null)
		{
			if (Snapshot == null && !Faction.IsNullOrEmpty())
			{
				Snapshot = HistoryAPI.GetVillageSnapshot(Faction);
			}
			string text = "a village";
			string text2 = "the act of procreating";
			string text3 = "those who oppose arable land and the act of procreating";
			string text4 = "roaming around idly";
			if (Snapshot != null)
			{
				Random stableRandomGenerator = Stat.GetStableRandomGenerator(Snapshot.Name);
				GameObjectBlueprint randomElement = GameObjectFactory.Factory.GetFactionMembers(Snapshot.GetProperty("baseFaction", "unknown"), false, true).GetRandomElement<GameObjectBlueprint>(stableRandomGenerator);
				text = Snapshot.Name;
				text2 = Snapshot.GetRandomElementFromListProperty("sacredThings", null, stableRandomGenerator) ?? Snapshot.GetProperty("defaultSacredThing", "unknown");
				text3 = Snapshot.GetRandomElementFromListProperty("profaneThings", null, stableRandomGenerator) ?? Snapshot.GetProperty("defaultProfaneThing", "unknown");
				text4 = randomElement.GetxTag_CommaDelimited("TextFragments", "Activity", null, stableRandomGenerator);
			}
			Text.StartReplace().AddReplacer("village.name", text).AddReplacer("village.sacred", text2)
				.AddReplacer("village.profane", text3)
				.AddReplacer("village.activity", text4)
				.Execute();
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000C8EF8 File Offset: 0x000C70F8
		public static HistoricEntitySnapshot GetSultanForPeriod(int period)
		{
			List<HistoricEntity> sultans = HistoryAPI.GetSultans();
			if (sultans == null || sultans.Count == 0)
			{
				return null;
			}
			foreach (HistoricEntity historicEntity in sultans)
			{
				string entityProperty = historicEntity.GetEntityProperty("period", -1L);
				int num;
				if (!entityProperty.IsNullOrEmpty() && int.TryParse(entityProperty, out num) && period == num)
				{
					return historicEntity.GetCurrentSnapshot();
				}
			}
			return null;
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000C8F88 File Offset: 0x000C7188
		public static List<string> GetLikedFactionsForSultan(int period)
		{
			HistoricEntitySnapshot sultanForPeriod = HistoryAPI.GetSultanForPeriod(period);
			if (sultanForPeriod == null)
			{
				return new List<string>();
			}
			return new List<string>(sultanForPeriod.GetList("likedFactions"));
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x000C8FB8 File Offset: 0x000C71B8
		public static List<string> GetDislikedFactionsForSultan(int period)
		{
			HistoricEntitySnapshot sultanForPeriod = HistoryAPI.GetSultanForPeriod(period);
			if (sultanForPeriod == null)
			{
				return new List<string>();
			}
			return new List<string>(sultanForPeriod.GetList("hatedFactions"));
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000C8FE8 File Offset: 0x000C71E8
		public static List<string> GetDomainsForSultan(int period)
		{
			HistoricEntitySnapshot sultanForPeriod = HistoryAPI.GetSultanForPeriod(period);
			if (sultanForPeriod == null)
			{
				return new List<string>();
			}
			return new List<string>(sultanForPeriod.GetList("elements"));
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000C9015 File Offset: 0x000C7215
		public static void OnWaterRitualShuffleSecrets(WaterRitualRecord Record, BallBag<IBaseJournalEntry> Notes)
		{
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000C9017 File Offset: 0x000C7217
		public static void OnWaterRitualBuySecret(WaterRitualRecord Record, ref IBaseJournalEntry Note)
		{
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000C9019 File Offset: 0x000C7219
		public static void OnWaterRitualSellSecret(WaterRitualRecord Record, List<IBaseJournalEntry> Notes)
		{
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000C901C File Offset: 0x000C721C
		public static List<HistoricEntity> GetKnownSultans()
		{
			HistoricEntityList sultanHistory = XRLCore.Core.Game.sultanHistory;
			List<HistoricEntity> list = new List<HistoricEntity>();
			foreach (HistoricEntity historicEntity in sultanHistory.GetEntitiesWithProperty("isCandidate").entities)
			{
				if (HistoryAPI.AnyKnownSultanEventsWithGospelsOrTombPropaganda(historicEntity.id))
				{
					list.Add(historicEntity);
				}
			}
			return list;
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000C909C File Offset: 0x000C729C
		public static List<HistoricEvent> GetKnownSultanEventsWithGospels(string sultanId)
		{
			List<HistoricEvent> list = new List<HistoricEvent>();
			foreach (HistoricEvent historicEvent in XRLCore.Core.Game.sultanHistory.GetEntity(sultanId).events)
			{
				if (historicEvent.HasEventProperty("gospel") && JournalAPI.KnowsSultanEvent(historicEvent.id))
				{
					list.Add(historicEvent);
				}
			}
			return list;
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000C9124 File Offset: 0x000C7324
		public static bool AnyKnownSultanEventsWithGospels(string sultanId)
		{
			foreach (HistoricEvent historicEvent in XRLCore.Core.Game.sultanHistory.GetEntity(sultanId).events)
			{
				if (historicEvent.HasEventProperty("gospel") && JournalAPI.KnowsSultanEvent(historicEvent.id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x000C91A8 File Offset: 0x000C73A8
		public static bool AnyKnownSultanEventsWithGospelsOrTombPropaganda(string sultanId)
		{
			foreach (HistoricEvent historicEvent in XRLCore.Core.Game.sultanHistory.GetEntity(sultanId).events)
			{
				if ((historicEvent.HasEventProperty("gospel") || historicEvent.HasEventProperty("tombInscription")) && JournalAPI.KnowsSultanEvent(historicEvent.id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x000C9238 File Offset: 0x000C7438
		public static HistoricEntity GetResheph()
		{
			return XRLCore.Core.Game.sultanHistory.GetEntityWithProperty("Resheph");
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000C9253 File Offset: 0x000C7453
		public static int GetFlipYear()
		{
			return int.Parse(HistoryAPI.GetResheph().GetEntityProperty("flipYear", -1L));
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x000C926B File Offset: 0x000C746B
		public static string GetSultanTerm()
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("SultanTerm", null) : null) ?? "sultan";
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000C9290 File Offset: 0x000C7490
		public static List<HistoricEvent> GetSultanEventsWithGospels(string sultanId)
		{
			List<HistoricEvent> list = new List<HistoricEvent>();
			foreach (HistoricEvent historicEvent in XRLCore.Core.Game.sultanHistory.GetEntity(sultanId).events)
			{
				if (historicEvent.HasEventProperty("gospel"))
				{
					list.Add(historicEvent);
				}
			}
			return list;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x000C930C File Offset: 0x000C750C
		public static List<HistoricEvent> GetSultanEventsWithTombInscriptions(string sultanId)
		{
			List<HistoricEvent> list = new List<HistoricEvent>();
			foreach (HistoricEvent historicEvent in XRLCore.Core.Game.sultanHistory.GetEntity(sultanId).events)
			{
				if (historicEvent.HasEventProperty("tombInscription"))
				{
					list.Add(historicEvent);
				}
			}
			return list;
		}
	}
}
