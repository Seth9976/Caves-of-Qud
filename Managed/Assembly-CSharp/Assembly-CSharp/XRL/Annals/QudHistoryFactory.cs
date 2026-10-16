using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.Core;
using XRL.Language;
using XRL.Names;
using XRL.Rules;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x0200060B RID: 1547
	public static class QudHistoryFactory
	{
		// Token: 0x06004196 RID: 16790 RVA: 0x0014E7CE File Offset: 0x0014C9CE
		public static void AddSultanCultNames(History history)
		{
			history.GetEntitiesWherePropertyEquals("type", "sultan").ForEach(delegate(HistoricEntity sultan)
			{
				HistoricEntitySnapshot currentSnapshot = sultan.GetCurrentSnapshot();
				XRLCore.Core.Game.SetStringGameState("CultDisplayName_SultanCult" + currentSnapshot.GetProperty("period", "unknown"), currentSnapshot.properties["cultName"]);
				string text = "include:SultanCult" + currentSnapshot.GetProperty("period", "unknown");
				Func<JournalSultanNote, bool> <>9__1;
				Func<JournalSultanNote, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (JournalSultanNote note) => note.SultanID == sultan.id);
				}
				foreach (JournalSultanNote journalSultanNote in JournalAPI.GetSultanNotes(func))
				{
					journalSultanNote.Attributes.Add(text);
				}
			});
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x0014E808 File Offset: 0x0014CA08
		public static History GenerateNewSultanHistory()
		{
			History history = new History(1L);
			QudHistoryFactory.InitializeHistory(history);
			List<int> spreadOfSultanYears = QudHistoryHelpers.GetSpreadOfSultanYears(6000, 5);
			for (int i = 1; i <= 5; i++)
			{
				QudHistoryFactory.GenerateNewRegions(history, Stat.Random(2, 3), i);
				QudHistoryFactory.GenerateNewSultan(history, i);
				history.currentYear += (long)spreadOfSultanYears[i - 1];
			}
			QudHistoryFactory.AddSultanCultNames(history);
			QudHistoryFactory.AddResheph(history);
			return history;
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x0014E874 File Offset: 0x0014CA74
		public static History GenerateVillageEraHistory(History history)
		{
			int num = int.Parse(history.GetEntitiesWithProperty("Resheph").GetRandomElement(null).GetCurrentSnapshot()
				.GetProperty("flipYear", "unknown"));
			for (int i = 0; i < history.events.Count; i++)
			{
				if (history.events[i].HasEventProperty("gospel"))
				{
					string text = QudHistoryHelpers.ConvertGospelToSultanateCalendarEra(history.events[i].GetEventProperty("gospel", null), (long)num);
					history.events[i].SetEventProperty("gospel", Grammar.ConvertAtoAn(text));
				}
				if (history.events[i].HasEventProperty("tombInscription"))
				{
					history.events[i].SetEventProperty("tombInscription", Grammar.ConvertAtoAn(history.events[i].GetEventProperty("tombInscription", null)));
				}
			}
			QudHistoryFactory.GenerateNewVillage(history, num, "DesertCanyon", null, 400, 900, 2, true, true);
			QudHistoryFactory.GenerateNewVillage(history, num, "Saltdunes", null, 400, 900, 2, true, true);
			QudHistoryFactory.GenerateNewVillage(history, num, "Saltmarsh", null, 400, 900, 2, true, true);
			QudHistoryFactory.GenerateNewVillage(history, num, "Hills", null, 400, 900, 2, true, true);
			int num2 = (int)Math.Round(1.9600000000000002 * (double)Stat.Random(80, 120) / 100.0 * 1.2000000476837158);
			int num3 = (int)Math.Round(3.36 * (double)Stat.Random(80, 120) / 100.0 * 0.800000011920929);
			int num4 = (int)Math.Round(0.56 * (double)Stat.Random(80, 120) / 100.0 * 1.399999976158142);
			int num5 = (int)Math.Round(2.52 * (double)Stat.Random(80, 120) / 100.0 * 1.0);
			int num6 = (int)Math.Round(2.24 * (double)Stat.Random(80, 120) / 100.0 * 0.800000011920929);
			int num7 = (int)Math.Round(0.28 * (double)Stat.Random(80, 120) / 100.0 * 1.0);
			int num8 = (int)Math.Round(0.56 * (double)Stat.Random(80, 120) / 100.0 * 1.2000000476837158);
			int num9 = (int)Math.Round(0.84 * (double)Stat.Random(80, 120) / 100.0 * 1.2000000476837158);
			int num10 = (int)Math.Round(0.56 * (double)Stat.Random(80, 120) / 100.0 * 1.2000000476837158);
			num7 += Stat.Random(-2, 2);
			int num11 = (int)Math.Round(2.8000000000000003 * (double)Stat.Random(80, 120) / 100.0 * 0.800000011920929);
			int num12 = (int)Math.Round(0.84 * (double)Stat.Random(80, 120) / 100.0 * 1.2000000476837158);
			int num13 = (int)Math.Round(6.72 * (double)Stat.Random(80, 120) / 100.0 * 1.0);
			int num14 = (int)Math.Round(2.8000000000000003 * (double)Stat.Random(80, 120) / 100.0 * 1.0);
			int num15 = (int)Math.Round(0.84 * (double)Stat.Random(80, 120) / 100.0 * 1.0);
			int num16 = (int)Math.Round(0.84 * (double)Stat.Random(80, 120) / 100.0 * 1.0);
			int num17 = (int)Math.Round(1.12 * (double)Stat.Random(80, 120) / 100.0 * 1.0);
			for (int j = 1; j <= num2; j++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "DesertCanyon", null, 400, 900, 2, false, true);
			}
			for (int k = 1; k <= num3; k++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "Saltdunes", null, 400, 900, 2, false, true);
			}
			for (int l = 1; l <= num4; l++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "Saltmarsh", null, 400, 900, 2, false, true);
			}
			for (int m = 1; m <= num5; m++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "Hills", null, 400, 900, 2, false, true);
			}
			for (int n = 1; n <= num6; n++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "Water", null, 400, 900, 2, false, true);
			}
			for (int num18 = 1; num18 <= num7; num18++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "BananaGrove", null, 400, 900, 2, false, true);
			}
			for (int num19 = 1; num19 <= num8; num19++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "Fungal", null, 400, 900, 2, false, true);
			}
			for (int num20 = 1; num20 <= num9; num20++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "LakeHinnom", null, 400, 900, 2, false, true);
			}
			for (int num21 = 1; num21 <= num10; num21++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "PalladiumReef", null, 400, 900, 2, false, true);
			}
			for (int num22 = 1; num22 <= num11; num22++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "Mountains", null, 400, 900, 2, false, true);
			}
			for (int num23 = 1; num23 <= num12; num23++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "Flowerfields", null, 400, 900, 2, false, true);
			}
			for (int num24 = 1; num24 <= num13; num24++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "Jungle", null, 400, 900, 2, false, true);
			}
			for (int num25 = 1; num25 <= num14; num25++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "DeepJungle", null, 400, 900, 2, false, true);
			}
			for (int num26 = 1; num26 <= num15; num26++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "Ruins", null, 400, 900, 2, false, true);
			}
			for (int num27 = 1; num27 <= num16; num27++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "BaroqueRuins", null, 400, 900, 2, false, true);
			}
			for (int num28 = 1; num28 <= num17; num28++)
			{
				QudHistoryFactory.GenerateNewVillage(history, num, "MoonStair", null, 400, 900, 2, false, true);
			}
			history.currentYear = (long)(num + 1000);
			return history;
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x0014EFB3 File Offset: 0x0014D1B3
		public static void InitializeHistory(History history)
		{
			history.CreateEntity(history.currentYear).ApplyEvent(new Regionalize(), long.MinValue);
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x0014EFD4 File Offset: 0x0014D1D4
		public static HistoricEvent GenerateVillageEvent(bool VillageZero = false, bool AllowAbandoned = true)
		{
			if (!VillageZero && AllowAbandoned && If.OneIn(20, null, null))
			{
				return new Abandoned(VillageZero);
			}
			int num = Stat.Random(0, 6);
			if (num == 0)
			{
				return new BecomesKnownFor(VillageZero);
			}
			if (num == 1)
			{
				return new PopulationInflux(VillageZero);
			}
			if (num == 2)
			{
				return new Worships(VillageZero);
			}
			if (num == 3)
			{
				return new Despises(VillageZero);
			}
			if (num == 4)
			{
				return new SharedMutation(VillageZero);
			}
			if (num == 5)
			{
				return new NewGovernment(VillageZero);
			}
			return new ImportedFoodorDrink(VillageZero);
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x0014F04C File Offset: 0x0014D24C
		public static HistoricEntity GenerateNewVillage(History History, int Year, string Region, string BaseFaction = null, int YearOffsetLow = 400, int YearOffsetHigh = 900, int EventAmount = 2, bool VillageZero = false, bool AllowAbandoned = true)
		{
			long currentYear = History.currentYear;
			History.currentYear = (long)(Year + Stat.Random(YearOffsetLow, YearOffsetHigh));
			HistoricEntity historicEntity = History.CreateEntity(History.currentYear);
			historicEntity.ApplyEvent(new InitializeVillage(Region, BaseFaction, null, null, null, VillageZero), long.MinValue);
			for (int i = 0; i < EventAmount; i++)
			{
				HistoricEvent historicEvent = QudHistoryFactory.GenerateVillageEvent(VillageZero, AllowAbandoned);
				historicEntity.ApplyEvent(historicEvent, historicEntity.lastYear + (long)Stat.Random(10, 20));
			}
			historicEntity.ApplyEvent(new VillageProverb("proverbs", null, null, 50), historicEntity.lastYear);
			historicEntity.MutateListPropertyAtCurrentYear("Gospels", (string s) => QudHistoryHelpers.ConvertGospelToSultanateCalendarEra(s, (long)Year));
			History.currentYear = currentYear;
			return historicEntity;
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x0014F11C File Offset: 0x0014D31C
		public static void GenerateNewSultan(History history, int period)
		{
			HistoricEntity historicEntity = history.CreateEntity(history.currentYear);
			historicEntity.ApplyEvent(new InitializeSultan(period), long.MinValue);
			historicEntity.ApplyEvent(new SetEntityProperty("isCandidate", "true"), long.MinValue);
			if (Stat.Random(0, 4) == 0)
			{
				historicEntity.ApplyEvent(new BornAsHeir(), historicEntity.lastYear + (long)Stat.Random(6, 8));
			}
			else
			{
				historicEntity.ApplyEvent(new FoundAsBabe(), historicEntity.lastYear + (long)Stat.Random(6, 8));
			}
			for (int i = 0; i < 8; i++)
			{
				int num = Stat.Random(0, 16);
				if (num == 0)
				{
					historicEntity.ApplyEvent(new CorruptAdministrator(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 1)
				{
					historicEntity.ApplyEvent(new CapturedByBandits(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 2)
				{
					historicEntity.ApplyEvent(new InspiringExperience(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 3)
				{
					historicEntity.ApplyEvent(new MeetFaction(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 4)
				{
					historicEntity.ApplyEvent(new SecretRitual(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 5)
				{
					historicEntity.ApplyEvent(new ChallengeSultan(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 6)
				{
					historicEntity.ApplyEvent(new ForgeItem(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 7)
				{
					historicEntity.ApplyEvent(new UnderWeirdSky(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 8)
				{
					historicEntity.ApplyEvent(new LiberateCity(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 9)
				{
					historicEntity.ApplyEvent(new RampageRegion(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 10)
				{
					historicEntity.ApplyEvent(new FoundGuild(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 11)
				{
					historicEntity.ApplyEvent(new BattleItem(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 12)
				{
					historicEntity.ApplyEvent(new LoseItemAtTavern(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 13)
				{
					historicEntity.ApplyEvent(new BloodyBattle(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 14)
				{
					historicEntity.ApplyEvent(new ChariotDrivesOffCliff(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 15)
				{
					historicEntity.ApplyEvent(new Abdicate(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num == 16)
				{
					historicEntity.ApplyEvent(new Marry(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (historicEntity.GetCurrentSnapshot().GetProperty("isAlive", "unknown") != "true")
				{
					historicEntity.ApplyEvent(new FakedDeath(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
			}
			if (!historicEntity.GetCurrentSnapshot().GetProperty("isSultan", "unknown").EqualsNoCase("true"))
			{
				int num2 = Stat.Random(0, 1);
				if (num2 == 0)
				{
					historicEntity.ApplyEvent(new ChallengeSultan(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
				if (num2 == 1)
				{
					historicEntity.ApplyEvent(new Abdicate(), historicEntity.lastYear + (long)Stat.Random(1, 16));
				}
			}
			HistoricEntityList entitiesByDelegate = history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("type", "unknown").Equals("region") && int.Parse(entity.GetSnapshotAtYear(entity.lastYear).GetProperty("period", "unknown")) == period);
			for (int j = 0; j < entitiesByDelegate.Count; j++)
			{
				string property = entitiesByDelegate.entities[j].GetCurrentSnapshot().GetProperty("newName", "unknown");
				bool flag = false;
				for (int k = 0; k < historicEntity.events.Count; k++)
				{
					if (historicEntity.events[k].HasEventProperty("revealsRegion") && historicEntity.events[k].GetEventProperty("revealsRegion", null) == property)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					string property2 = entitiesByDelegate.entities[j].GetCurrentSnapshot().GetProperty("name", "unknown");
					int num3 = Stat.Random(0, 5);
					if (num3 == 0)
					{
						historicEntity.ApplyEvent(new CorruptAdministrator(property2), historicEntity.lastYear + (long)Stat.Random(1, 16));
					}
					if (num3 == 1)
					{
						historicEntity.ApplyEvent(new BloodyBattle(property2), historicEntity.lastYear + (long)Stat.Random(1, 16));
					}
					if (num3 == 2)
					{
						historicEntity.ApplyEvent(new LoseItemAtTavern(property2), historicEntity.lastYear + (long)Stat.Random(1, 16));
					}
					if (num3 == 3)
					{
						historicEntity.ApplyEvent(new MeetFaction(property2), historicEntity.lastYear + (long)Stat.Random(1, 16));
					}
					if (num3 == 4)
					{
						historicEntity.ApplyEvent(new RampageRegion(property2), historicEntity.lastYear + (long)Stat.Random(1, 16));
					}
					if (num3 == 5)
					{
						historicEntity.ApplyEvent(new SecretRitual(property2), historicEntity.lastYear + (long)Stat.Random(1, 16));
					}
				}
			}
			int num4 = 0;
			while (num4 < 3 && !historicEntity.GetCurrentSnapshot().GetProperty("isAlive", "unknown").EqualsNoCase("true"))
			{
				num4++;
			}
			if (historicEntity.GetCurrentSnapshot().GetProperty("isAlive", "unknown").EqualsNoCase("true"))
			{
				historicEntity.ApplyEvent(new GenericDeath(), long.MinValue);
			}
			QudHistoryFactory.FillOutLikedFactions(historicEntity);
			QudHistoryFactory.GenerateCultName(historicEntity, history);
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x0014F6A0 File Offset: 0x0014D8A0
		public static void AddResheph(History history)
		{
			HistoricEntity historicEntity = history.CreateEntity(history.currentYear);
			historicEntity.ApplyEvent(new InitializeResheph(6), historicEntity.lastYear);
			historicEntity.ApplyEvent(new SetEntityProperty("isCandidate", "true"), long.MinValue);
			historicEntity.ApplyEvent(new ReshephIsBorn(), historicEntity.lastYear);
			historicEntity.ApplyEvent(new ReshephHasStarExperience(), historicEntity.lastYear + 33L);
			historicEntity.ApplyEvent(new ReshephMeetsRebekah(), historicEntity.lastYear + 10L);
			historicEntity.ApplyEvent(new ReshephBecomesSultan(), historicEntity.lastYear + 29L);
			historicEntity.ApplyEvent(new ReshephAppointsRebekah(), historicEntity.lastYear + 41L);
			historicEntity.ApplyEvent(new ReshephFoundsHarborage(), historicEntity.lastYear + 91L);
			historicEntity.ApplyEvent(new ReshephHealsGyre1(), historicEntity.lastYear + 5L);
			historicEntity.ApplyEvent(new ReshephBetrayed(), historicEntity.lastYear);
			historicEntity.ApplyEvent(new ReshephHealsGyre2(), historicEntity.lastYear + 1L);
			historicEntity.ApplyEvent(new ReshephLearnsCurse(), historicEntity.lastYear + 1L);
			historicEntity.ApplyEvent(new ReshephRebuffsCurse(), historicEntity.lastYear);
			historicEntity.ApplyEvent(new ReshephClosesTomb(), historicEntity.lastYear + 1L);
			historicEntity.ApplyEvent(new ReshephAbsolvesRebekah(), historicEntity.lastYear + 1L);
			historicEntity.ApplyEvent(new ReshephCleansesGyre(), historicEntity.lastYear + 1L);
			historicEntity.ApplyEvent(new ReshephWeirdSky(), historicEntity.lastYear + 3L);
			historicEntity.ApplyEvent(new ReshephDies(), historicEntity.lastYear);
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x0014F824 File Offset: 0x0014DA24
		public static void GenerateNewRegions(History history, int numRegions, int period)
		{
			for (int i = 0; i < numRegions; i++)
			{
				history.CreateEntity(history.currentYear).ApplyEvent(new InitializeRegion(period), long.MinValue);
			}
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x0014F860 File Offset: 0x0014DA60
		public static string NameRuinsSite(History history, out bool Proper, out string nameRoot)
		{
			HistoricEntitySnapshot currentSnapshot = history.GetEntitiesWherePropertyEquals("name", "regionalizationParameters").GetRandomElement(null).GetCurrentSnapshot();
			string text = HistoricStringExpander.ExpandString("<spice." + currentSnapshot.GetProperty("siteTopology1", "unknown") + ".!random>", null, history, null, null);
			string text2 = HistoricStringExpander.ExpandString("<spice." + currentSnapshot.GetProperty("siteTopology2", "unknown") + ".!random>", null, history, null, null);
			nameRoot = NameMaker.MakeName(null, null, null, null, "Qudish", null, null, null, null, null, null, "Site", null, false, false, null, null);
			string text3 = (int.Parse(currentSnapshot.GetProperty("siteTopologyTheChance", "unknown")).in100() ? "the " : "");
			int num = Stat.Random(0, 80);
			if (num < 15)
			{
				Proper = true;
				return nameRoot;
			}
			if (80.in100())
			{
				nameRoot = currentSnapshot.GetProperty("siteName" + Stat.Random(1, 3).ToString(), "unknown");
			}
			if (1.in100())
			{
				nameRoot = "";
			}
			string text4;
			if (num < 30)
			{
				text4 = text3 + nameRoot;
				if (text4 != "")
				{
					text4 += " ";
				}
				text4 += text;
				Proper = true;
			}
			else if (num < 45)
			{
				text4 = text3 + text2 + " " + nameRoot;
				Proper = true;
			}
			else
			{
				if (num >= 60)
				{
					Proper = false;
					return "some forgotten ruins";
				}
				text4 = string.Concat(new string[] { text2, " ", text, " ", nameRoot });
				Proper = true;
			}
			return Grammar.MakeTitleCase(text4.Replace("  ", " "));
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x0014FA38 File Offset: 0x0014DC38
		public static string NameRuinsSite(History history)
		{
			bool flag;
			string text;
			return QudHistoryFactory.NameRuinsSite(history, out flag, out text);
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x0014FA50 File Offset: 0x0014DC50
		public static void GenerateCultName(HistoricEntity sultan, History history)
		{
			int num = Stat.Random(0, 100);
			HistoricEntitySnapshot currentSnapshot = sultan.GetCurrentSnapshot();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (currentSnapshot.GetList("cognomen").Count == 0)
			{
				num = 100;
			}
			string text = HistoricStringExpander.ExpandString("<spice.commonPhrases.cult.!random.capitalize>", null, history, null, null);
			if (num < 70)
			{
				string randomElement = currentSnapshot.GetList("cognomen").GetRandomElement<string>(null);
				if (Stat.Random(0, 1) == 0)
				{
					dictionary.Add("cultName", text + " of the " + Grammar.TrimLeadingThe(randomElement));
				}
				else
				{
					dictionary.Add("cultName", Grammar.TrimLeadingThe(randomElement + " " + text));
				}
			}
			else if (Stat.Random(0, 1) == 0)
			{
				dictionary.Add("cultName", "Cult of " + currentSnapshot.GetProperty("name", "unknown"));
			}
			else
			{
				int num2 = int.Parse(currentSnapshot.GetProperty("suffix", "unknown"));
				if (num2 > 0)
				{
					dictionary.Add("cultName", Grammar.MakeTitleCase(string.Concat(new string[]
					{
						Grammar.Ordinal(num2),
						" ",
						Grammar.GetWordRoot(currentSnapshot.GetProperty("nameRoot", "unknown")),
						"ian ",
						text
					})));
				}
				else
				{
					dictionary.Add("cultName", Grammar.GetWordRoot(currentSnapshot.GetProperty("nameRoot", "unknown")) + "ian " + text);
				}
			}
			sultan.ApplyEvent(new SetEntityProperties(dictionary, null), long.MinValue);
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x0014FBE0 File Offset: 0x0014DDE0
		public static void FillOutLikedFactions(HistoricEntity sultan)
		{
			HistoricEntitySnapshot currentSnapshot = sultan.GetCurrentSnapshot();
			int num = int.Parse(currentSnapshot.GetProperty("period", "unknown"));
			if (num == 5 || num == 4)
			{
				for (int i = currentSnapshot.GetList("likedFactions").Count; i < 3; i++)
				{
					HistoricEntitySnapshot currentSnapshot2 = sultan.GetCurrentSnapshot();
					string blueprint;
					do
					{
						blueprint = PopulationManager.RollOneFrom("RandomFaction_Period" + num.ToString(), null, null).Blueprint;
					}
					while (currentSnapshot2.GetList("likedFactions").Contains(blueprint));
					sultan.ApplyEvent(new SetEntityProperties(null, new Dictionary<string, string> { { "likedFactions", blueprint } }), long.MinValue);
				}
				return;
			}
			for (int j = currentSnapshot.GetList("likedFactions").Count; j < 3; j++)
			{
				HistoricEntitySnapshot currentSnapshot3 = sultan.GetCurrentSnapshot();
				string name;
				do
				{
					name = Factions.GetRandomFactionWithAtLeastOneMember().Name;
				}
				while (currentSnapshot3.GetList("likedFactions").Contains(name));
				sultan.ApplyEvent(new SetEntityProperties(null, new Dictionary<string, string> { { "likedFactions", name } }), long.MinValue);
			}
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x0014FD0C File Offset: 0x0014DF0C
		public static HistoricEntity RequirePlayerEntity()
		{
			XRLGame game = The.Game;
			History history = ((game != null) ? game.sultanHistory : null);
			if (history == null)
			{
				MetricsManager.LogError("Cannot get player entity, sultan history not yet generated.");
				return null;
			}
			GameObject body = game.Player.Body;
			HistoricEntity historicEntity = history.GetEntity("Player") ?? history.CreateEntity("Player", 1001L);
			HistoricEvent historicEvent2;
			HistoricEvent historicEvent = (historicEvent2 = historicEntity.events[0]);
			Dictionary<string, string> dictionary;
			if ((dictionary = historicEvent2.entityProperties) == null)
			{
				dictionary = (historicEvent2.entityProperties = new Dictionary<string, string>());
			}
			Dictionary<string, string> dictionary2 = dictionary;
			IPronounProvider pronounProvider = body.GetPronounProvider(false);
			dictionary2["name"] = (dictionary2["nameRoot"] = body.Render.DisplayName.Strip());
			dictionary2["subjectPronoun"] = pronounProvider.Subjective;
			dictionary2["possessivePronoun"] = pronounProvider.PossessiveAdjective;
			dictionary2["objectPronoun"] = pronounProvider.Objective;
			historicEvent2 = historicEvent;
			Dictionary<string, List<string>> dictionary3;
			if ((dictionary3 = historicEvent2.addedListProperties) == null)
			{
				dictionary3 = (historicEvent2.addedListProperties = new Dictionary<string, List<string>>());
			}
			Dictionary<string, List<string>> dictionary4 = dictionary3;
			List<string> list;
			if (!dictionary4.TryGetValue("elements", out list))
			{
				list = (dictionary4["elements"] = new List<string>());
			}
			list.Clear();
			GetItemElementsEvent mythicFor = GetItemElementsEvent.GetMythicFor(body, 5, null);
			if (mythicFor.Bag.IsNullOrEmpty<string>())
			{
				list.Add("might");
			}
			else
			{
				list.AddRange(mythicFor.Bag.Items);
			}
			PooledEvent<GetItemElementsEvent>.ResetTo(ref mythicFor);
			return historicEntity;
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x0014FE88 File Offset: 0x0014E088
		public static HistoricEntitySnapshot RequirePlayerEntitySnapshot()
		{
			HistoricEntity historicEntity = QudHistoryFactory.RequirePlayerEntity();
			HistoricEntitySnapshot snapshotInstance = QudHistoryFactory.SnapshotInstance;
			snapshotInstance.entity = historicEntity;
			snapshotInstance.properties = historicEntity.events[0].entityProperties ?? snapshotInstance.properties;
			snapshotInstance.listProperties = historicEntity.events[0].addedListProperties ?? snapshotInstance.listProperties;
			return snapshotInstance;
		}

		// Token: 0x04002020 RID: 8224
		public const string PLAYER_ENTITY_ID = "Player";

		// Token: 0x04002021 RID: 8225
		public const int numSultans = 5;

		// Token: 0x04002022 RID: 8226
		public const int avgYearsInSultanate = 6000;

		// Token: 0x04002023 RID: 8227
		public const float avgNumVillages = 28f;

		// Token: 0x04002024 RID: 8228
		public const float percentOfWorldmap_Saltdunes = 12f;

		// Token: 0x04002025 RID: 8229
		public const float percentOfWorldmap_Saltmarsh = 2f;

		// Token: 0x04002026 RID: 8230
		public const float percentOfWorldmap_DesertCanyon = 7f;

		// Token: 0x04002027 RID: 8231
		public const float percentOfWorldmap_Jungle = 24f;

		// Token: 0x04002028 RID: 8232
		public const float percentOfWorldmap_DeepJungle = 10f;

		// Token: 0x04002029 RID: 8233
		public const float percentOfWorldmap_Hills = 9f;

		// Token: 0x0400202A RID: 8234
		public const float percentOfWorldmap_Water = 8f;

		// Token: 0x0400202B RID: 8235
		public const float percentOfWorldmap_BananaGrove = 1f;

		// Token: 0x0400202C RID: 8236
		public const float percentOfWorldmap_Fungal = 2f;

		// Token: 0x0400202D RID: 8237
		public const float percentOfWorldmap_LakeHinnom = 3f;

		// Token: 0x0400202E RID: 8238
		public const float percentOfWorldmap_PalladiumReef = 2f;

		// Token: 0x0400202F RID: 8239
		public const float percentOfWorldmap_Mountains = 10f;

		// Token: 0x04002030 RID: 8240
		public const float percentOfWorldmap_Flowerfields = 3f;

		// Token: 0x04002031 RID: 8241
		public const float percentOfWorldmap_Ruins = 3f;

		// Token: 0x04002032 RID: 8242
		public const float percentOfWorldmap_BaroqueRuins = 2f;

		// Token: 0x04002033 RID: 8243
		public const float percentOfWorldmap_MoonStair = 4f;

		// Token: 0x04002034 RID: 8244
		public const float villageModifier_Saltdunes = 0.8f;

		// Token: 0x04002035 RID: 8245
		public const float villageModifier_Saltmarsh = 1.4f;

		// Token: 0x04002036 RID: 8246
		public const float villageModifier_DesertCanyon = 1.2f;

		// Token: 0x04002037 RID: 8247
		public const float villageModifier_Jungle = 1f;

		// Token: 0x04002038 RID: 8248
		public const float villageModifier_DeepJungle = 1f;

		// Token: 0x04002039 RID: 8249
		public const float villageModifier_Hills = 1f;

		// Token: 0x0400203A RID: 8250
		public const float villageModifier_Water = 0.8f;

		// Token: 0x0400203B RID: 8251
		public const float villageModifier_BananaGrove = 1f;

		// Token: 0x0400203C RID: 8252
		public const float villageModifier_Fungal = 1.2f;

		// Token: 0x0400203D RID: 8253
		public const float villageModifier_LakeHinnom = 1.2f;

		// Token: 0x0400203E RID: 8254
		public const float villageModifier_PalladiumReef = 1.2f;

		// Token: 0x0400203F RID: 8255
		public const float villageModifier_Mountains = 0.8f;

		// Token: 0x04002040 RID: 8256
		public const float villageModifier_Flowerfields = 1.2f;

		// Token: 0x04002041 RID: 8257
		public const float villageModifier_Ruins = 1f;

		// Token: 0x04002042 RID: 8258
		public const float villageModifier_BaroqueRuins = 1f;

		// Token: 0x04002043 RID: 8259
		public const float villageModifier_MoonStair = 1f;

		// Token: 0x04002044 RID: 8260
		public const int numVillageEvents = 2;

		// Token: 0x04002045 RID: 8261
		public const int ruinedVillagesOneIn = 20;

		// Token: 0x04002046 RID: 8262
		private static HistoricEntitySnapshot SnapshotInstance = new HistoricEntitySnapshot();
	}
}
