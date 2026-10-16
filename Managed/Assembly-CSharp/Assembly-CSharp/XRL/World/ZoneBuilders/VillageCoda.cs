using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using Genkit;
using HistoryKit;
using Qud.API;
using UnityEngine;
using XRL.Annals;
using XRL.Collections;
using XRL.Language;
using XRL.Names;
using XRL.Rules;
using XRL.World.AI;
using XRL.World.Capabilities;
using XRL.World.Conversations.Parts;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Skill;
using XRL.World.Quests;
using XRL.World.Quests.GolemQuest;
using XRL.World.WorldBuilders;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B08 RID: 2824
	public class VillageCoda : VillageCodaBase
	{
		// Token: 0x06007107 RID: 28935 RVA: 0x00295190 File Offset: 0x00293390
		public static HistoricEntity GenerateSultanEntity(GameObject Object)
		{
			int flipYear = HistoryAPI.GetFlipYear();
			HistoricEntity historicEntity = The.Game.sultanHistory.CreateEntity("CodaSultan", (long)(flipYear + 1001));
			HistoricEvent historicEvent = historicEntity.events[0];
			historicEvent.SetEntityProperty("name", Object.Render.DisplayName.Strip());
			historicEvent.SetEntityProperty("statueTile", Object.Render.Tile);
			historicEvent.SetEntityProperty("statueBase", "true");
			historicEvent.SetEntityProperty("isCandidate", "true");
			historicEvent.SetEntityProperty("type", "sultan");
			historicEvent.SetEntityProperty("period", 7.ToStringCached());
			Tattoos tattoos;
			if (Object.TryGetPart<Tattoos>(out tattoos))
			{
				historicEvent.AddListProperty("palette", new List<string>(3)
				{
					ConsoleLib.Console.ColorUtility.FindLastForeground(tattoos.ColorString).ToString(),
					tattoos.DetailColor,
					"K"
				});
			}
			else
			{
				historicEvent.AddListProperty("palette", Crayons.GetRandomDistinctColorsAll(3));
			}
			Faction faction = Factions.Get("PlayerCult");
			faction.DisplayName = "Cult of " + Object.Render.DisplayName.Strip();
			faction.Emblem.Copy(Object.Render);
			int num = 1;
			HashSet<string> hashSet = new HashSet<string>();
			foreach (JournalAccomplishment journalAccomplishment in (from x in PlayerMuralGameState.Instance.GetAccomplishments()
				where x.MuralCategory != MuralCategory.Dies
				select x).Take<JournalAccomplishment>(10))
			{
				if (journalAccomplishment.GospelText.IsNullOrEmpty())
				{
					MetricsManager.LogError(string.Concat(new string[]
					{
						"Mural accomplishment (",
						journalAccomplishment.ID.Coalesce("NO ID"),
						") does not have gospel text: \"",
						journalAccomplishment.Text.Coalesce(journalAccomplishment.MuralText),
						"\""
					}));
				}
				else if (hashSet.Add(journalAccomplishment.GospelText))
				{
					GospelEvent gospelEvent = new GospelEvent(journalAccomplishment.GospelText);
					string text = string.Format("CodaSultanGospel{0}", num++);
					historicEntity.ApplyEvent(gospelEvent, (long)(flipYear + Calendar.GetYear(journalAccomplishment.Time)));
					gospelEvent.SetEventProperty("Mural", "1");
					gospelEvent.SetEventProperty("NoteID", text);
					JournalAPI.AddSultanNote(text, journalAccomplishment.GospelText ?? "[NO GOSPEL]", historicEntity.id, new long?(gospelEvent.id), null);
				}
			}
			return historicEntity;
		}

		// Token: 0x06007108 RID: 28936 RVA: 0x00295470 File Offset: 0x00293670
		public void PlaceSultanStories()
		{
			bool flag = Stat.Chance(50);
			BallBag<GameObject> ballBag = new BallBag<GameObject>(0);
			for (int i = 0; i < this.zone.Height; i++)
			{
				for (int j = 0; j < this.zone.Width; j++)
				{
					GameObject highestRenderLayerObject = this.zone.GetCell(j, i).GetHighestRenderLayerObject();
					if (GameObject.Validate(highestRenderLayerObject) && !highestRenderLayerObject.IsCombatObject() && !highestRenderLayerObject.HasPart<ModPainted>() && !highestRenderLayerObject.HasPart<ModEngraved>() && !highestRenderLayerObject.HasPart<RevealNoteOnLook>() && !highestRenderLayerObject.HasPart<VillageStoryReveal>() && !highestRenderLayerObject.HasPart<AnimatedMaterialFire>())
					{
						int num = 0;
						if (highestRenderLayerObject.HasPropertyOrTag("Vessel"))
						{
							num = 1000;
						}
						else if (highestRenderLayerObject.HasPropertyOrTag("Furniture"))
						{
							num = 750;
						}
						else if (highestRenderLayerObject.HasPropertyOrTag("Door"))
						{
							num = 250;
						}
						ballBag.Add(highestRenderLayerObject, num);
					}
				}
			}
			foreach (PopulationLayout populationLayout in this.buildings)
			{
				foreach (Location2D location2D in populationLayout.originalRegion.Cells)
				{
					Cell cell = this.zone.GetCell(location2D);
					GameObject firstWall = cell.GetFirstWall();
					if (firstWall != null)
					{
						int num2 = 0;
						Cell.SpiralEnumerator enumerator3 = cell.IterateAdjacent(1, false, false, true).GetEnumerator();
						while (enumerator3.MoveNext())
						{
							if (enumerator3.Current.IsPassable(null, true))
							{
								num2++;
							}
						}
						if (num2 > 2)
						{
							ballBag.Add(firstWall, num2 * 2);
						}
					}
				}
			}
			HistoricEntity entity = The.Game.sultanHistory.GetEntity("CodaSultan");
			HistoricEntitySnapshot currentSnapshot = entity.GetCurrentSnapshot();
			string entityProperty = entity.GetEntityProperty("name", -1L);
			foreach (HistoricEvent historicEvent in entity.events)
			{
				if (historicEvent.HasEventProperty("Mural"))
				{
					string eventProperty = historicEvent.GetEventProperty("NoteID", null);
					IBaseJournalEntry baseJournalEntry = JournalAPI.NotesByID[eventProperty];
					GameObject gameObject = ballBag.PickOne();
					if (gameObject == null)
					{
						MetricsManager.LogError("Insufficient objects to place coda sultan stories.");
						break;
					}
					if (flag)
					{
						gameObject.AddPart<ModPainted>(new ModPainted
						{
							PaintedEvent = historicEvent,
							Sultan = entityProperty,
							Painting = baseJournalEntry.Text
						}, true, false);
					}
					else
					{
						gameObject.AddPart<ModEngraved>(new ModEngraved
						{
							EngravedEvent = historicEvent,
							Sultan = entityProperty,
							Engraving = baseJournalEntry.Text
						}, true, false);
					}
					string text = "Y";
					string text2 = "C";
					if (flag)
					{
						HistoricPerspective historicPerspective = historicEvent.RequirePerspective(currentSnapshot, null);
						text = historicPerspective.mainColor.Coalesce(text);
						text2 = historicPerspective.supportColor.Coalesce(text2);
					}
					gameObject.Render.ColorString = "&" + text;
					gameObject.Render.DetailColor = text2;
					if (!gameObject.Render.TileColor.IsNullOrEmpty())
					{
						gameObject.Render.TileColor = gameObject.Render.ColorString;
					}
				}
			}
		}

		// Token: 0x06007109 RID: 28937 RVA: 0x0029581C File Offset: 0x00293A1C
		public HistoricEvent GenerateEndEvent()
		{
			HistoricEntity entity = The.Game.sultanHistory.GetEntity("CodaSultan");
			string sultanTerm = HistoryAPI.GetSultanTerm();
			string text;
			if (EndGame.IsMarooned)
			{
				text = "In =event.year=, a triad of plagues afflicted the land. Tongues rotted away in the mouths of kith and kin, their legs annealed to iron, and darkness bloomed from the earth. The warden physickers voiced a prayer to =subject.name= the Above and walked beneath the chrome arches to heal the sick.";
			}
			else if (EndGame.IsCovenant)
			{
				text = "In =event.year=, =subject.name= cleansed the =village.region= of the plagues of the Gyre and, through the tutelage of the tinker monks at Grit Gate, taught =mayor.name= to sow =village.plant= along their fertile tracks.";
			}
			else if (EndGame.IsAccede)
			{
				text = "In =event.year=, =subject.name=, the Above, forsook the people of Qud in favor of its sludges and microorganisms, and then disappeared. =pronouns.Subjective= =verb:were:afterpronoun= 216 years old.";
			}
			else if (EndGame.IsLaunch)
			{
				text = "At twilight in the shadow of the Spindle, the people of =village.name= saw an image on the horizon that looked like a =subject.personTerm= bathed in starfire. It was =subject.name=, and after =pronouns.subjective= came and left =village.name=, the people built a monument to =pronouns.objective=, and thenceforth called =pronouns.objective= =subject.PersonTerm=-in-Starfire.";
			}
			else
			{
				text = "In =event.year=, a triad of plagues afflicted the land. Tongues rotted away in the mouths of kith and kin, their legs annealed to iron, and darkness bloomed from the earth. =subject.name= and their warden physickers walked beneath the chrome arches and healed the sick.";
			}
			string text2 = text.StartReplace().AddObject(this.System.Sultan, null).AddReplacer("sultan.term", sultanTerm)
				.AddReplacer("mayor.name", NameMaker.MakeName(null, null, null, null, "Qudish", null, null, null, null, null, null, null, null, false, false, null, null))
				.AddReplacer("village.name", this.villageName)
				.AddReplacer("village.region", HistoricStringExpander.ExpandString("<spice.history.regions.terrain." + this.region + ".pluralRegionName.!random>", null))
				.AddReplacer("village.plant", HistoricStringExpander.ExpandString("<spice.history.regions.terrain." + this.region + ".regionCrop.!random>", null))
				.AddReplacer("event.year", this.System.EndYear.ToString() + " AR")
				.ToString();
			GospelEvent gospelEvent = new GospelEvent(text2);
			entity.ApplyEvent(gospelEvent, (long)(HistoryAPI.GetFlipYear() + this.System.EndYear));
			gospelEvent.SetEventProperty("PlayerStatue", "1");
			gospelEvent.SetEventProperty("NoteID", "CodaSultanEndEvent");
			JournalAPI.AddSultanNote("CodaSultanEndEvent", text2, "CodaSultan", new long?(gospelEvent.id), null);
			return gospelEvent;
		}

		// Token: 0x0600710A RID: 28938 RVA: 0x002959C8 File Offset: 0x00293BC8
		public static HistoricEntity GenerateVillageEntity(string Region, string BaseFaction = null, int YearOffsetLow = 400, int YearOffsetHigh = 900)
		{
			History history = The.Game.sultanHistory;
			long currentYear = history.currentYear;
			history.currentYear = (long)(HistoryAPI.GetFlipYear() + Calendar.GetYear() + Stat.Random(YearOffsetLow, YearOffsetHigh));
			HistoricEntity historicEntity = history.CreateEntity("CodaVillage", history.currentYear);
			HistoricEntity entity = history.GetEntity("CodaSultan");
			historicEntity.ApplyEvent(new InitializeVillage(Region, BaseFaction, VillageCodaBase.GetEligibleVillagers(BaseFaction, true).GetRandomElement<GameObjectBlueprint>(null).Name, "Chiliad Qudish", entity.GetListProperties("palette", -1L, false), false), long.MinValue);
			if (!VillageCoda.IsRuined)
			{
				historicEntity.ApplyEvent(QudHistoryFactory.GenerateVillageEvent(false, false), historicEntity.lastYear + (long)Stat.Random(10, 20));
			}
			if (VillageCoda.IsPlagued)
			{
				historicEntity.events[0].AddEntityListItem("sharedDiseases", 50.in100() ? "Glotrot" : "Ironshank", false);
			}
			HistoricEvent historicEvent;
			if (VillageCoda.IsDespised)
			{
				historicEntity.ApplyEvent(historicEvent = new CodaDespises(), historicEntity.lastYear + (long)Stat.Random(10, 20));
			}
			else
			{
				historicEntity.ApplyEvent(historicEvent = new CodaWorships(), historicEntity.lastYear + (long)Stat.Random(10, 20));
			}
			if (VillageCoda.IsRuined)
			{
				Abandoned abandoned = new Abandoned();
				historicEntity.ApplyEvent(abandoned, historicEntity.lastYear + (long)Stat.Random(10, 20));
				abandoned.SetEntityProperty("ruinScale", "4");
			}
			Quest quest;
			if (The.Game.FinishedQuests.TryGetValue("Landing Pads", out quest))
			{
				VillageCoda.SlynthPopulationInflux(historicEntity, quest.GetProperty("Faction", null));
			}
			HistoricEntity historicEntity2 = historicEntity;
			string text = "proverbsCoda";
			List<string> listProperties = historicEvent.GetListProperties("sacredThings");
			string text2 = ((listProperties != null) ? listProperties.GetRandomElement<string>(null) : null);
			List<string> listProperties2 = historicEvent.GetListProperties("profaneThings");
			historicEntity2.ApplyEvent(new VillageProverb(text, text2, (listProperties2 != null) ? listProperties2.GetRandomElement<string>(null) : null, 100), historicEntity.lastYear);
			historicEntity.MutateListPropertyAtCurrentYear("Gospels", (string s) => QudHistoryHelpers.ConvertGospelToSultanateCalendarEra(s, history.currentYear));
			if (historicEntity.HasEntityProperty("worships_creature", -1L))
			{
				GameObject gameObject = JoppaWorldBuilder.GenerateLairOwner("Terrain" + Region, Stat.Random(1, 8), 10);
				The.ZoneManager.CacheObject(gameObject, false, false);
				Worships.PostProcessEvent(historicEntity, gameObject.GetReferenceDisplayName(int.MaxValue, null, "Worship", false, true, false, false, false, false, false, false, null, false), gameObject.ID);
			}
			if (historicEntity.HasEntityProperty("despises_creature", -1L))
			{
				GameObject gameObject2 = JoppaWorldBuilder.GenerateLairOwner("Terrain" + Region, Stat.Random(1, 8), 10);
				The.ZoneManager.CacheObject(gameObject2, false, false);
				Despises.PostProcessEvent(historicEntity, gameObject2.GetReferenceDisplayName(int.MaxValue, null, "Despise", false, true, false, false, false, false, false, false, null, false), gameObject2.ID);
			}
			history.currentYear = currentYear;
			return historicEntity;
		}

		// Token: 0x0600710B RID: 28939 RVA: 0x00295CB4 File Offset: 0x00293EB4
		private static void SlynthPopulationInflux(HistoricEntity Village, string FactionName)
		{
			HistoricEvent historicEvent = Village.events[0];
			string slynthType = VillageCoda.GetSlynthType(FactionName);
			string slynthRole = VillageCoda.GetSlynthRole(Village, FactionName);
			if (slynthType.IsNullOrEmpty() || slynthRole.IsNullOrEmpty())
			{
				return;
			}
			GameObject gameObject = GameObject.CreateSample(slynthType);
			historicEvent.AddEntityListItem("immigrant_name", NameMaker.MakeName(gameObject, null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null), true);
			historicEvent.AddEntityListItem("immigrant_gender", gameObject.GetGender(false).Name, false);
			historicEvent.AddEntityListItem("immigrant_role", slynthRole, true);
			historicEvent.AddEntityListItem("immigrant_dialogWhy_Q", "", true);
			historicEvent.AddEntityListItem("immigrant_dialogWhy_A", "", true);
			historicEvent.AddEntityListItem("immigrant_type", slynthType, true);
			gameObject.Pool();
		}

		// Token: 0x0600710C RID: 28940 RVA: 0x00295D84 File Offset: 0x00293F84
		private static string GetSlynthType(string FactionName)
		{
			Faction ifExists = Factions.GetIfExists(FactionName);
			if (ifExists == null)
			{
				return null;
			}
			if (FactionName == "Joppa")
			{
				return "Chiliad Slynth Joppa";
			}
			if (FactionName == "Barathrumites")
			{
				return "Chiliad Slynth Grit Gate";
			}
			if (FactionName == "Hindren")
			{
				return "Chiliad Slynth Bey Lah";
			}
			if (FactionName == "Kyakukya")
			{
				return "Chiliad Slynth Kyakukya";
			}
			if (FactionName == "Ezra")
			{
				return "Chiliad Slynth Ezra";
			}
			if (FactionName == "YdFreehold")
			{
				return "Chiliad Slynth Yd Freehold";
			}
			if (FactionName == "Mechanimists")
			{
				return "Chiliad Slynth Mechanimists";
			}
			if (FactionName == "Mopango")
			{
				return "Chiliad Slynth Mopango";
			}
			if (FactionName == "Chavvah")
			{
				return "Chiliad Slynth Chavvah";
			}
			if (ifExists.GetIntProperty("Village", 0) >= 1)
			{
				return "Chiliad Slynth Village";
			}
			return null;
		}

		// Token: 0x0600710D RID: 28941 RVA: 0x00295E60 File Offset: 0x00294060
		private static string GetSlynthRole(HistoricEntity Village, string FactionName)
		{
			BallBag<string> roleBag = PopulationInflux.GetRoleBag();
			List<string> listProperties = Village.GetListProperties("immigrant_role", -1L, true);
			string text;
			if (FactionName == "Barathrumites")
			{
				text = "tinker";
			}
			else if (FactionName == "Joppa" || FactionName == "Kyakukya")
			{
				text = "apothecary";
			}
			else
			{
				text = roleBag.PickOne();
			}
			while (!text.IsNullOrEmpty())
			{
				if (text == "villager" || !listProperties.Contains(text))
				{
					return text;
				}
				text = roleBag.PickOne();
			}
			return "villager";
		}

		// Token: 0x0600710E RID: 28942 RVA: 0x00295EF4 File Offset: 0x002940F4
		public void SetStatueVisuals(GameObject Object)
		{
			string stringGameState = The.Game.GetStringGameState("CodaStatueMaterial", "Stone");
			Render render = Object.Render;
			render.DisplayName = "shrine to " + this.System.Sultan.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, true, false, true, false, false, null, false);
			render.Tile = this.System.Sultan.Render.Tile;
			if (EndGame.IsAccede)
			{
				Object.AddPart<ModDesecrated>(new ModDesecrated(), true, false);
			}
			if (stringGameState == "Gold")
			{
				render.ColorString = "&W";
				render.DetailColor = "w";
				return;
			}
			render.ColorString = "&y";
			render.DetailColor = "Y";
			if (!Object.HasPart<ModDesecrated>())
			{
				Object.AddPart<MossyUnityMaterial2>(new MossyUnityMaterial2(), true, false);
			}
		}

		// Token: 0x0600710F RID: 28943 RVA: 0x00295FCC File Offset: 0x002941CC
		public void SetStatueDescription(GameObject Object)
		{
			this.GenerateEndEvent();
			GameObject sultan = this.System.Sultan;
			string sultanTerm = HistoryAPI.GetSultanTerm();
			Description part = Object.GetPart<Description>();
			string text = "This shrine depicts a significant event from the life of the ancient =sultan.term= =subject.name=:\n\n".StartReplace().AddObject(sultan, null).AddReplacer("sultan.term", sultanTerm)
				.ToString();
			IBaseJournalEntry value = JournalAPI.NotesByID.GetValue("CodaSultanEndEvent");
			Object.AddPart<RevealNoteOnLook>(new RevealNoteOnLook(value.ID), true, false);
			part.Short = text + value.Text;
		}

		// Token: 0x06007110 RID: 28944 RVA: 0x00296050 File Offset: 0x00294250
		public void FabricatePlayerStatue()
		{
			int num = 1;
			int num2 = 1;
			int num3 = Stat.Random(1, 4);
			int num4 = 0;
			if (num3 == 2)
			{
				num = 65;
			}
			else if (num3 == 3)
			{
				num = 65;
				num2 = 15;
				num4 = 2;
			}
			else if (num3 == 4)
			{
				num2 = 15;
				num4 = 2;
			}
			MapChunkPlacement.PlaceFromFile(this.zone.GetCell(num, num2), "preset_tile_chunks/CodaPlayerStatue.rpm", 13, 8, 0, num4, null, null, new Action<Cell>(this.PlacePostAction));
		}

		// Token: 0x06007111 RID: 28945 RVA: 0x002960B8 File Offset: 0x002942B8
		private void PlacePostAction(Cell Cell)
		{
			base.AvoidPoint(Cell.Location);
			GameObject gameObject = Cell.FindObject("Coda Player Statue");
			if (gameObject != null)
			{
				this.SetStatueVisuals(gameObject);
				this.SetStatueDescription(gameObject);
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06007112 RID: 28946 RVA: 0x002960F0 File Offset: 0x002942F0
		public CodaSystem System
		{
			get
			{
				CodaSystem codaSystem;
				if ((codaSystem = this._System) == null)
				{
					codaSystem = (this._System = The.Game.GetSystem<CodaSystem>());
				}
				return codaSystem;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06007113 RID: 28947 RVA: 0x0029611A File Offset: 0x0029431A
		public static bool IsRuined
		{
			get
			{
				return The.Game.HasGameState("CodaRuined");
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06007114 RID: 28948 RVA: 0x0029612B File Offset: 0x0029432B
		public static bool IsPlagued
		{
			get
			{
				return The.Game.HasGameState("CodaPlagued");
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06007115 RID: 28949 RVA: 0x0029613C File Offset: 0x0029433C
		public static bool IsDespised
		{
			get
			{
				return The.Game.HasGameState("CodaDespised");
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06007116 RID: 28950 RVA: 0x0029614D File Offset: 0x0029434D
		public static bool HasAmaranthinePrism
		{
			get
			{
				return The.Game.HasGameState("CodaAmaranthinePrism");
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06007117 RID: 28951 RVA: 0x0029615E File Offset: 0x0029435E
		public static bool HasAmaranthineDust
		{
			get
			{
				return The.Game.HasGameState("CodaAmaranthineDust");
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06007118 RID: 28952 RVA: 0x0029616F File Offset: 0x0029436F
		public static bool HasTauNoLonger
		{
			get
			{
				return The.Game.HasGameState("CodaTauNoLonger");
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06007119 RID: 28953 RVA: 0x00296180 File Offset: 0x00294380
		public static bool HasWanderingTau
		{
			get
			{
				return The.Game.HasGameState("CodaWanderingTau");
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x0600711A RID: 28954 RVA: 0x00296191 File Offset: 0x00294391
		public static bool HasDeadTau
		{
			get
			{
				return The.Game.HasGameState("CodaDeadTau");
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600711B RID: 28955 RVA: 0x002961A2 File Offset: 0x002943A2
		public static bool HasStarshiibLocket
		{
			get
			{
				return The.Game.HasGameState("CodaStarshiibLocket");
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600711C RID: 28956 RVA: 0x002961B3 File Offset: 0x002943B3
		public static bool HasFoundSonnet
		{
			get
			{
				return The.Game.HasGameState("CodaFoundSonnet");
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600711D RID: 28957 RVA: 0x002961C4 File Offset: 0x002943C4
		public static bool HasReturnedSonnet
		{
			get
			{
				return The.Game.HasGameState("CodaReturnedSonnet");
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600711E RID: 28958 RVA: 0x002961D5 File Offset: 0x002943D5
		public static bool ChoseNacham
		{
			get
			{
				return The.Game.HasGameState("ChoseNacham");
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600711F RID: 28959 RVA: 0x002961E6 File Offset: 0x002943E6
		public static bool ChoseVaam
		{
			get
			{
				return The.Game.HasGameState("ChoseVaam");
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06007120 RID: 28960 RVA: 0x002961F7 File Offset: 0x002943F7
		public static bool ChoseDagasha
		{
			get
			{
				return The.Game.HasGameState("ChoseDagasha");
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06007121 RID: 28961 RVA: 0x00296208 File Offset: 0x00294408
		public static bool ChoseKah
		{
			get
			{
				return The.Game.HasGameState("ChoseKah");
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06007122 RID: 28962 RVA: 0x0029621C File Offset: 0x0029441C
		public string mayorTemplate
		{
			get
			{
				if (this.villageSnapshot == null)
				{
					return null;
				}
				if (this.villageSnapshot.GetProperty("mayorTemplate", "unknown") != "unknown")
				{
					return this.villageSnapshot.GetProperty("mayorTemplate", "unknown");
				}
				return "Mayor";
			}
		}

		// Token: 0x06007123 RID: 28963 RVA: 0x00296270 File Offset: 0x00294470
		public GameObject generateWarden(GameObject baseObject, bool GivesRep = false)
		{
			GameObject gameObject;
			if (baseObject != null)
			{
				gameObject = baseObject;
			}
			else
			{
				Func<GameObject> func = FuzzyFunctions.DoThisButRarelyDoThat<GameObject>(delegate
				{
					GameObject anonLegendaryCreature = EncountersAPI.GetANonLegendaryCreature((GameObjectBlueprint ob) => ob.HasTag(this.dynamicCreatureTableName, false) && (ob.HasPart("Body") || ob.HasTagOrProperty("BodySubstitute")) && (ob.HasPart("Combat") || ob.HasTagOrProperty("BodySubstitute")) && !ob.HasTag("Merchant", false) && !ob.HasTag("ExcludeFromVillagePopulations", false));
					if (anonLegendaryCreature == null)
					{
						return null;
					}
					return anonLegendaryCreature;
				}, () => EncountersAPI.GetANonLegendaryCreature((GameObjectBlueprint ob) => ob.HasPart("Body") && ob.HasPart("Combat") && !ob.HasTag("Merchant", false) && !ob.HasTag("ExcludeFromVillagePopulations", false)), "33");
				try
				{
					gameObject = func();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					gameObject = null;
				}
				if (gameObject == null)
				{
					gameObject = base.getBaseVillager(true);
					base.preprocessVillager(gameObject, false);
				}
				else
				{
					base.preprocessVillager(gameObject, true);
				}
			}
			gameObject.Brain.Mobile = true;
			gameObject.Brain.Factions = "";
			gameObject.Brain.Allegiance.Clear();
			gameObject.Brain.Allegiance.Add("Wardens", 100);
			gameObject.Brain.Allegiance.Hostile = false;
			gameObject.Brain.Allegiance.Calm = true;
			gameObject = HeroMaker.MakeHero(gameObject, "SpecialVillagerHeroTemplate_Warden", -1, "Warden");
			gameObject.RequirePart<Interesting>(false);
			gameObject.SetIntProperty("VillageWarden", 1, false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			if (this.isVillageZero)
			{
				gameObject.SetIntProperty("WaterRitualNoSellSkill", 1, false);
			}
			GivesRep givesRep = gameObject.GetPart<GivesRep>();
			if (givesRep != null)
			{
				givesRep.ResetRelatedFactions();
			}
			if (GivesRep)
			{
				gameObject.SetStringProperty("staticFaction1", this.villageFaction + ",friend,defending their village", false);
				string text = gameObject.GetPropertyOrTag("NoHateFactions", null);
				if (text.IsNullOrEmpty())
				{
					text = "Wardens";
				}
				else
				{
					text += ",Wardens";
				}
				gameObject.SetStringProperty("NoHateFactions", text, false);
				if (givesRep == null)
				{
					givesRep = gameObject.AddPart<GivesRep>(true, false);
				}
				givesRep.FillInRelatedFactions(true);
			}
			else if (givesRep != null)
			{
				gameObject.RemovePart(givesRep);
			}
			string text2 = HistoricStringExpander.ExpandString("<spice.villages.warden.introDialog.!random>", null);
			gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			ConversationsAPI.addSimpleConversationToObject(gameObject, text2, "Live and drink.", null, null, null, true, true);
			TakeOnRoleEvent.Send(gameObject, "Warden");
			return gameObject;
		}

		// Token: 0x06007124 RID: 28964 RVA: 0x00296470 File Offset: 0x00294670
		public GameObject generateMayor(GameObject baseObject, string specialTemplate = "SpecialVillagerHeroTemplate_Mayor", bool GivesRep = true)
		{
			GameObject gameObject;
			if (baseObject != null)
			{
				gameObject = baseObject;
			}
			else
			{
				gameObject = base.getBaseVillager(false);
				base.preprocessVillager(gameObject, false);
				base.setVillagerProperties(gameObject, true);
			}
			if (gameObject.Brain != null)
			{
				gameObject.Brain.SetFactionFeeling(this.villageFaction, RuleSettings.REPUTATION_LOVED);
				gameObject.Brain.SetFactionFeeling(this.villagerBaseFaction, RuleSettings.REPUTATION_LOVED);
				gameObject.Brain.SetFactionMembership(this.villageFaction, 100);
				if (!this.isVillageZero)
				{
					gameObject.Brain.SetFactionMembership(this.villagerBaseFaction, 25);
				}
			}
			GivesRep givesRep = gameObject.GetPart<GivesRep>();
			if (givesRep != null)
			{
				givesRep.ResetRelatedFactions();
			}
			if (GivesRep)
			{
				string text = gameObject.GetPropertyOrTag("NoHateFactions", null);
				if (text.IsNullOrEmpty())
				{
					text = "Wardens";
				}
				else
				{
					text += ",Wardens";
				}
				gameObject.SetStringProperty("NoHateFactions", text, false);
				if (givesRep == null)
				{
					givesRep = gameObject.AddPart<GivesRep>(true, false);
				}
				givesRep.FillInRelatedFactions(true);
			}
			else if (givesRep != null)
			{
				gameObject.RemovePart(givesRep);
			}
			gameObject = HeroMaker.MakeHero(gameObject, specialTemplate, -1, this.mayorTemplate);
			gameObject.RequirePart<Interesting>(false);
			gameObject.SetStringProperty("Mayor", this.villageFaction, false);
			gameObject.SetIntProperty("VillageMayor", 1, false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			gameObject.SetIntProperty("ParticipantVillager", 1, false);
			gameObject.SetStringProperty("WaterRitual_Skill", this.signatureSkill ?? base.RollOneFrom("Village_RandomTaughtSkill"), false);
			if (this.signatureDish != null)
			{
				gameObject.AddPart<TeachesDish>(new TeachesDish(this.signatureDish, "What a savory smell! Teach me to cook the favorite dish of " + this.villageName + ".\n"), true, false);
			}
			string text2 = ((this.villageSnapshot.GetList("sacredThings").Count > 0) ? this.villageSnapshot.GetList("sacredThings").GetRandomElement<string>(null) : this.villageSnapshot.GetProperty("defaultSacredThing", "unknown"));
			string text3 = ((this.villageSnapshot.GetList("profaneThings").Count > 0) ? this.villageSnapshot.GetList("profaneThings").GetRandomElement<string>(null) : this.villageSnapshot.GetProperty("defaultProfaneThing", "unknown"));
			string text4 = HistoricStringExpander.ExpandString("<spice.villages.mayor.introDialog.!random>", null).Replace("*villageName*", this.villageName).Replace("*sacredThing*", text2)
				.Replace("*profaneThing*", text3);
			gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			base.AddVillagerConversation(gameObject, text4, "Live and drink, =pronouns.formalAddressTerm=.", null, null, false, true);
			LandingPadsSystem.AddDynamicVillageConversation(gameObject);
			TakeOnRoleEvent.Send(gameObject, "Mayor");
			return gameObject;
		}

		// Token: 0x06007125 RID: 28965 RVA: 0x00296708 File Offset: 0x00294908
		public GameObject generateMerchant(GameObject baseObject)
		{
			GameObject gameObject;
			if (!this.isVillageZero && baseObject == null && If.d100(20, null, null))
			{
				gameObject = base.getBaseVillager(true);
				base.preprocessVillager(gameObject, false);
				base.setVillagerProperties(gameObject, true);
				string text = (gameObject.GetBlueprint(true).DescendsFrom("Dromad") ? "SpecialVillagerHeroTemplate_DromadMerchant" : "SpecialVillagerHeroTemplate_Merchant");
				gameObject = HeroMaker.MakeHero(gameObject, text, -1, "Merchant");
			}
			else
			{
				if (baseObject != null)
				{
					gameObject = baseObject;
				}
				else
				{
					gameObject = GameObjectFactory.Factory.Blueprints["Chiliad Creature Dromads"].createOne();
					base.preprocessVillager(gameObject, true);
				}
				gameObject.RemovePart<DromadCaravan>();
				gameObject.RemovePart<ConversationScript>();
				string text2 = NameMaker.MakeTitle(gameObject, null, null, null, null, null, null, null, null, null, "Merchant", null, false, null, null);
				gameObject.GiveProperName(null, false, null, false, null, null, null, null);
				if (!text2.IsNullOrEmpty())
				{
					gameObject.RequirePart<Titles>(false).AddTitle(text2, -5);
				}
				if (gameObject.GetSpecies() != "dromad")
				{
					gameObject.RequirePart<DisplayNameColor>(false).SetColorByPriority("Y", 30);
					gameObject.RequirePart<MerchantIconColor>(false);
				}
			}
			gameObject.RequirePart<Interesting>(false);
			gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			if (gameObject.GetBlueprint(true).DescendsFrom("Dromad"))
			{
				base.AddVillagerConversation(gameObject, "Welcome, =player.species=. What do you desire?", "Live and drink.", null, null, false, true);
			}
			else
			{
				base.AddVillagerConversation(gameObject, "Come. Browse my wares, =player.formalAddressTerm=.", "Live and drink, =pronouns.formalAddressTerm=.", null, null, false, true);
			}
			if (gameObject.Brain.Allegiance.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				gameObject.Brain.Factions = this.villageFaction + "-100";
			}
			else if (!gameObject.Brain.Allegiance.ContainsKey(this.villageFaction))
			{
				gameObject.Brain.Allegiance[this.villageFaction] = 25;
			}
			GenericInventoryRestocker genericInventoryRestocker = gameObject.RequirePart<GenericInventoryRestocker>(false);
			genericInventoryRestocker.Clear();
			int num = 0;
			while (num <= 2 && this.villageTier > num)
			{
				genericInventoryRestocker.AddTable("Tier" + (this.villageTier - num).ToStringCached() + "Wares");
				num++;
			}
			genericInventoryRestocker.PerformRestock(true);
			gameObject.SetIntProperty("VillageMerchant", 1, false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			if (this.villageSnapshot.hasProperty("worships_faction") && GameObjectFactory.Factory.GetFactionMembers(this.villageSnapshot.GetProperty("worships_faction", "unknown"), false, true).Count > 0)
			{
				GameObject gameObject2 = GameObjectFactory.Factory.CreateObject(PopulationManager.RollOneFrom("Figurines " + this.villageTier.ToString(), null, null).Blueprint, 0, 0, null, null, delegate(GameObject o)
				{
					RandomFigurine randomFigurine;
					if (o.TryGetPart<RandomFigurine>(out randomFigurine))
					{
						randomFigurine.Creature = GameObjectFactory.Factory.GetFactionMembers(this.villageSnapshot.GetProperty("worships_faction", "unknown"), false, true).GetRandomElement<GameObjectBlueprint>(null).Name;
					}
				}, null, null);
				gameObject.ReceiveObject(gameObject2, false, null);
			}
			TakeOnRoleEvent.Send(gameObject, "Merchant");
			return gameObject;
		}

		// Token: 0x06007126 RID: 28966 RVA: 0x002969F0 File Offset: 0x00294BF0
		public void PopulateCodaMerchant(PopulationLayout Building)
		{
			GameObject gameObject = this.zone.FindObject((GameObject x) => x.IsCombatObject() && x.HasIntProperty("VillageMerchant"));
			if (VillageCoda.HasAmaranthinePrism)
			{
				GameObject gameObject2 = GameObject.Create("Cyclopean Prism");
				CyclopeanPrism cyclopeanPrism;
				if (gameObject2.TryGetPart<CyclopeanPrism>(out cyclopeanPrism))
				{
					cyclopeanPrism.ResetPrism();
				}
				gameObject2.SetIntProperty("NoAIEquip", 1, false);
				if (GameObject.Validate(gameObject))
				{
					gameObject.ReceiveObject(gameObject2, false, null);
				}
				else
				{
					base.PlaceObjectInBuilding(gameObject2, Building, null, null);
				}
			}
			if (VillageCoda.HasDeadTau)
			{
				GameObject gameObject3 = GameObject.Create("DeadTauFigurine");
				if (GameObject.Validate(gameObject))
				{
					gameObject.ReceiveObject(gameObject3, false, null);
				}
				else
				{
					base.PlaceObjectInBuilding(gameObject3, Building, null, null);
				}
			}
			if (VillageCoda.HasWanderingTau)
			{
				GameObject gameObject4 = GameObject.Create("WanderingTauFigurine");
				if (GameObject.Validate(gameObject))
				{
					gameObject.ReceiveObject(gameObject4, false, null);
				}
				else
				{
					base.PlaceObjectInBuilding(gameObject4, Building, null, null);
				}
			}
			if (VillageCoda.HasTauNoLonger)
			{
				GameObject gameObject5 = GameObject.Create("TauNoLongerFigurine");
				if (GameObject.Validate(gameObject))
				{
					gameObject.ReceiveObject(gameObject5, false, null);
					return;
				}
				base.PlaceObjectInBuilding(gameObject5, Building, null, null);
			}
		}

		// Token: 0x06007127 RID: 28967 RVA: 0x00296B10 File Offset: 0x00294D10
		public GameObject generateApothecary(GameObject immigrant = null)
		{
			int num = Math.Min(Math.Max(this.villageTier, 1), 8);
			string text = "SpecialVillagerHeroTemplate_Apothecary";
			GameObject gameObject;
			if (immigrant != null || If.d100(50, null, null))
			{
				if (immigrant != null)
				{
					gameObject = immigrant;
				}
				else
				{
					gameObject = base.getBaseVillager(true);
					base.preprocessVillager(gameObject, false);
					base.setVillagerProperties(gameObject, true);
				}
				GameObject gameObject2;
				if (this.isVillageZero)
				{
					gameObject2 = GameObjectFactory.Factory.Blueprints["HumanApothecary_Village0"].createOne();
				}
				else
				{
					gameObject2 = GameObjectFactory.Factory.Blueprints["HumanApothecary" + num.ToString()].createOne();
				}
				foreach (BaseSkill baseSkill in gameObject2.GetPart<Skills>().SkillList)
				{
					gameObject.AddSkill(baseSkill.Name, null, null);
				}
				GenericInventoryRestocker genericInventoryRestocker = gameObject.RequirePart<GenericInventoryRestocker>(false);
				GenericInventoryRestocker part = gameObject2.GetPart<GenericInventoryRestocker>();
				genericInventoryRestocker.Table = ((part != null) ? part.Table : null) ?? "Village Apothecary 1";
				genericInventoryRestocker.Chance = 100;
				gameObject.Statistics["XP"].BaseValue = Math.Max(gameObject.Stat("XP", 0), gameObject2.Stat("XP", 0));
				gameObject.Statistics["Hitpoints"].BaseValue = Math.Max(gameObject.Stat("Hitpoints", 0), gameObject2.Stat("Hitpoints", 0));
				gameObject.Statistics["Intelligence"].BaseValue = Math.Max(gameObject.Stat("Intelligence", 0), 15);
				gameObject.Statistics["Intelligence"].BaseValue = Math.Max(gameObject.Stat("Toughness", 0), 15);
			}
			else if (this.isVillageZero)
			{
				gameObject = GameObjectFactory.Factory.Blueprints["HumanApothecary_Village0"].createOne();
			}
			else
			{
				gameObject = GameObjectFactory.Factory.Blueprints["HumanApothecary" + num.ToString()].createOne();
			}
			gameObject = HeroMaker.MakeHero(gameObject, text, -1, "Apothecary");
			gameObject.RequirePart<Interesting>(false);
			gameObject.RemovePart<ConversationScript>();
			gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			if (this.villageTier <= 3)
			{
				ConversationsAPI.addSimpleConversationToObject(gameObject, "I've the cure for what ails you.~You don't look so good. You need more yuckwheat and honey in your diet.~Cook your meals with yuckwheat if you feel sick. Catch a disease early enough and you can kill it.~\"Ease the pain, addle the brain.\" Be careful when you chew witchwood bark.", "Live and drink.", null, null, null, true, true);
			}
			else
			{
				ConversationsAPI.addSimpleConversationToObject(gameObject, "I've the cure for what ails you.~You don't look so good. You need more yuckwheat and honey in your diet.~Cook your meals with yuckwheat if you feel sick. Catch a disease early enough and you can kill it.~\"Ease the pain, addle the brain.\" Be careful when you chew witchwood bark.~In the market for a tonic, =player.formalAddressTerm=? Spend water now or blood later, your choice.~Prickly-boons and yuckwheat for trade.~If you came for the humble pie, you had best not have led any mind-hunters here.~Have you got enough tonics?", "Live and drink.", null, null, null, true, true);
			}
			if (gameObject.Brain.Allegiance.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				gameObject.Brain.Factions = this.villageFaction + "-100";
			}
			else if (!gameObject.Brain.Allegiance.ContainsKey(this.villageFaction))
			{
				gameObject.Brain.Allegiance[this.villageFaction] = 50;
			}
			gameObject.SetIntProperty("VillageApothecary", 1, false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			TakeOnRoleEvent.Send(gameObject, "Apothecary");
			return gameObject;
		}

		// Token: 0x06007128 RID: 28968 RVA: 0x00296E20 File Offset: 0x00295020
		public void PopulateCodaApothecary(PopulationLayout Building)
		{
			GameObject gameObject = this.zone.FindObject((GameObject x) => x.IsCombatObject() && x.HasIntProperty("VillageApothecary"));
			if (VillageCoda.HasAmaranthineDust)
			{
				GameObject gameObject2 = GameObject.Create("Amaranthine Dust");
				gameObject2.Count = Stat.Random(3, 4);
				if (GameObject.Validate(gameObject))
				{
					gameObject.ReceiveObject(gameObject2, false, null);
					return;
				}
				base.PlaceObjectInBuilding(gameObject2, Building, null, null);
			}
		}

		// Token: 0x06007129 RID: 28969 RVA: 0x00296E98 File Offset: 0x00295098
		public GameObject generateTinker(GameObject immigrant = null)
		{
			string text = "SpecialVillagerHeroTemplate_Tinker";
			GameObject gameObject;
			if (immigrant != null || If.d100(50, null, null))
			{
				if (immigrant != null)
				{
					gameObject = immigrant;
				}
				else
				{
					gameObject = base.getBaseVillager(true);
					base.preprocessVillager(gameObject, false);
					base.setVillagerProperties(gameObject, true);
				}
				GameObject gameObject2;
				if (this.isVillageZero)
				{
					gameObject2 = GameObjectFactory.Factory.Blueprints["HumanTinker_Village0"].createOne();
				}
				else
				{
					gameObject2 = GameObjectFactory.Factory.Blueprints["HumanTinker" + this.villageTier.ToString()].createOne();
				}
				foreach (BaseSkill baseSkill in gameObject2.GetPart<Skills>().SkillList)
				{
					gameObject.AddSkill(baseSkill.Name, null, null);
				}
				GenericInventoryRestocker genericInventoryRestocker = gameObject.RequirePart<GenericInventoryRestocker>(false);
				GenericInventoryRestocker part = gameObject2.GetPart<GenericInventoryRestocker>();
				genericInventoryRestocker.Table = ((part != null) ? part.Table : null) ?? "Village Tinker 1";
				genericInventoryRestocker.Chance = 100;
				gameObject.GetStat("XP").BaseValue = Math.Max(gameObject.GetStatValue("XP", 0), gameObject2.GetStatValue("XP", 0));
				gameObject.GetStat("Hitpoints").BaseValue = Math.Max(gameObject.GetStatValue("Hitpoints", 0), gameObject2.GetStatValue("Hitpoints", 0));
				gameObject.GetStat("Intelligence").BaseValue = Math.Max(gameObject.GetStatValue("Intelligence", 0), 16);
			}
			else if (this.isVillageZero)
			{
				gameObject = GameObjectFactory.Factory.Blueprints["HumanTinker_Village0"].createOne();
			}
			else
			{
				gameObject = GameObjectFactory.Factory.Blueprints["HumanTinker" + this.villageTier.ToString()].createOne();
			}
			gameObject = HeroMaker.MakeHero(gameObject, text, -1, "Tinker");
			gameObject.RequirePart<Interesting>(false);
			gameObject.RemovePart<ConversationScript>();
			gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			ConversationsAPI.addSimpleConversationToObject(gameObject, "Need a gadget repaired or identified, =player.formalAddressTerm=? Or if you're a tinker =player.reflexive=, perhaps you'd like to peruse my schematics?", "Live and drink, tinker.", null, null, null, true, true);
			if (gameObject.Brain.Allegiance.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				gameObject.Brain.Factions = this.villageFaction + "-100";
			}
			else if (!gameObject.Brain.Allegiance.ContainsKey(this.villageFaction))
			{
				gameObject.Brain.Allegiance[this.villageFaction] = 50;
			}
			gameObject.SetIntProperty("VillageTinker", 1, false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			TakeOnRoleEvent.Send(gameObject, "Tinker");
			return gameObject;
		}

		// Token: 0x0600712A RID: 28970 RVA: 0x00297144 File Offset: 0x00295344
		public void PlaceTinker(GameObject Object, PopulationLayout Building)
		{
			base.PlaceObjectInBuilding(Object, Building, "AlongInsideWall", null);
			int i = 0;
			int num = Stat.Random(2, 3);
			while (i < num)
			{
				base.PlaceObjectInBuilding(GameObject.Create("Workbench"), Building, "AlongInsideWall", null);
				i++;
			}
			int j = 0;
			int num2 = Stat.Random(0, 2);
			while (j < num2)
			{
				base.PlaceObjectInBuilding(GameObject.Create("Table"), Building, "AlongInsideWall", null);
				j++;
			}
		}

		// Token: 0x0600712B RID: 28971 RVA: 0x002971B8 File Offset: 0x002953B8
		public void PopulateCodaTinker(PopulationLayout Tinker)
		{
			base.PlaceObjectInBuilding(this.GenerateMechanicalGolem(), Tinker, "InsideCorner", null);
			base.PlaceObjectInBuilding(GameObject.Create("Electric Generator"), Tinker, "AlongInsideWall", null);
			if (VillageCoda.ChoseNacham)
			{
				base.PlaceObjectInBuilding(GameObject.Create("Nacham's Loom"), Tinker, "AlongInsideWall", null);
			}
			if (VillageCoda.ChoseVaam)
			{
				GameObject gameObject = GameObject.Create("Va'am's Blower");
				base.PlaceObjectInBuilding(gameObject, Tinker, "InsideCorner", null);
				Fan fan;
				if (gameObject.TryGetPart<Fan>(out fan))
				{
					string directionFrom = gameObject.CurrentCell.GetDirectionFrom(Tinker.position);
					fan.Direction = directionFrom.GetRandomElement(null).ToString();
				}
			}
			if (VillageCoda.ChoseDagasha)
			{
				base.PlaceObjectInBuilding(GameObject.Create("Dagasha's Crucifix"), Tinker, "AlongInsideWall", null);
			}
			if (VillageCoda.ChoseKah)
			{
				GameObject gameObject2 = GameObject.Create("Kah's Conveyor");
				base.PlaceObjectInBuilding(gameObject2, Tinker, "InsideCorner", null);
				if (gameObject2.CurrentCell.Y > Tinker.position.Y)
				{
					gameObject2.CurrentCell.GetCellFromDirection("N", true).AddObject("ConveyorPadN", null, null, null, null);
					return;
				}
				gameObject2.CurrentCell.GetCellFromDirection("S", true).AddObject("ConveyorPadS", null, null, null, null);
			}
		}

		// Token: 0x0600712C RID: 28972 RVA: 0x00297300 File Offset: 0x00295500
		public GameObject GenerateMechanicalGolem()
		{
			GameObject gameObject = GameObject.Create("Mechanical Golem");
			List<GameObject> list = VehicleRecord.ResolveRecordsFor(null, null, "Golem");
			GameObject gameObject2 = ((list != null) ? list.GetRandomElement<GameObject>(null) : null);
			bool flag = false;
			if (gameObject2 == null)
			{
				gameObject2 = GameObject.CreateSample(GolemBodySelection.GetBodyBySpecies().GetRandomElement<KeyValuePair<string, string>>(null).Value);
				flag = true;
			}
			GolemQuestSelection.ProcessDescription(gameObject, gameObject2);
			gameObject.Render.DisplayName = "mechanical " + gameObject2.GetBlueprint(true).DisplayName();
			gameObject.Render.Tile = gameObject2.Render.Tile;
			gameObject.ForceApplyEffect(new Broken(), null);
			if (flag)
			{
				gameObject2.Pool();
			}
			return gameObject;
		}

		// Token: 0x0600712D RID: 28973 RVA: 0x002973A8 File Offset: 0x002955A8
		public void PlaceEtaEarthling(PopulationLayout Building)
		{
			GameObject gameObject = GameObject.Create(VillageCoda.HasReturnedSonnet ? "LoveAndFearCompleted" : "LoveAndFearUncompleted");
			int num = int.MaxValue;
			Location2D position = this.buildings[0].position;
			GameObject gameObject2 = null;
			foreach (GameObject gameObject3 in this.zone.IterateObjects())
			{
				if (gameObject3.HasPart<Container>() && gameObject3.GetBlueprint(true).DescendsFrom("BaseBookshelf"))
				{
					Location2D location = gameObject3.CurrentCell.Location;
					int num2 = position.Distance(location);
					if (num2 < num)
					{
						gameObject2 = gameObject3;
						num = num2;
					}
				}
			}
			if (gameObject2 == null)
			{
				gameObject2 = GameObject.Create("Bookshelf");
				base.PlaceObjectInBuilding(gameObject2, Building, "AlongInsideWall", null);
			}
			gameObject2.ReceiveObject(gameObject, false, null);
		}

		// Token: 0x0600712E RID: 28974 RVA: 0x0029747C File Offset: 0x0029567C
		public GameObject generateImmigrant(string type, string name, string gender, string role, string whyQ, string whyA)
		{
			GameObject gameObject;
			if (type == null)
			{
				gameObject = GameObject.Create(PopulationManager.RollOneFrom("DynamicInheritsTable:Creature:Tier" + this.villageTier.ToString(), null, null).Blueprint);
			}
			else
			{
				gameObject = GameObject.Create(type);
			}
			base.preprocessVillager(gameObject, true);
			gameObject.SetStringProperty("HeroNameColor", "&Y", false);
			base.setVillagerProperties(gameObject, true);
			if (role == "mayor")
			{
				gameObject = this.generateMayor(gameObject, "SpecialVillagerHeroTemplate_" + this.mayorTemplate, true);
			}
			else if (role == "warden")
			{
				gameObject = this.generateWarden(gameObject, this.isVillageZero);
			}
			else if (role == "merchant")
			{
				gameObject = this.generateMerchant(gameObject);
			}
			else if (role == "tinker")
			{
				gameObject = this.generateTinker(gameObject);
			}
			else if (role == "apothecary")
			{
				gameObject = this.generateApothecary(gameObject);
			}
			else
			{
				gameObject = HeroMaker.MakeHero(gameObject, null, null, -1, "Hero");
			}
			gameObject.RequirePart<Interesting>(false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			gameObject.SetIntProperty("ParticipantVillager", 1, false);
			gameObject.Render.DisplayName = name;
			if (!gender.IsNullOrEmpty())
			{
				gameObject.SetGender(gender);
			}
			if (role != "mayor" && role != "warden")
			{
				gameObject.RemovePart<GivesRep>();
			}
			if (role == "villager")
			{
				gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			}
			base.AddVillagerConversation(gameObject, gameObject.GetTag("SimpleConversation", "Moon and Sun. Wisdom and will.~May the earth yield for us this season.~Peace, =player.formalAddressTerm=."), "Live and drink.", whyQ, whyA, role != "villager", true);
			return gameObject;
		}

		// Token: 0x0600712F RID: 28975 RVA: 0x00297628 File Offset: 0x00295828
		public GameObject generatePet(string species, out string name)
		{
			GameObject gameObject;
			if (species == null)
			{
				gameObject = GameObject.Create(PopulationManager.RollOneFrom("DynamicInheritsTable:BaseAnimal:Tier" + this.villageTier.ToString(), null, null).Blueprint);
			}
			else
			{
				gameObject = GameObject.Create(species);
			}
			string text = NameMaker.MakeTitle(gameObject, null, null, null, null, null, null, null, null, null, null, null, false, null, null);
			name = gameObject.GiveProperName(null, false, null, false, null, null, null, null);
			if (!text.IsNullOrEmpty())
			{
				gameObject.RequirePart<Titles>(false).AddTitle(text, -5);
			}
			base.setVillagerProperties(gameObject, true);
			gameObject.RequirePart<SmartuseForceTwiddles>(false);
			gameObject.RemovePart<Pettable>();
			Pettable pettable = new Pettable();
			gameObject.AddPart<Pettable>(pettable, true, false);
			pettable.PettableIfPositiveFeeling = true;
			pettable.UseFactionForFeelingFloor = this.villageFaction;
			gameObject.SetIntProperty("VillagePet", 1, false);
			gameObject.RequirePart<Interesting>(false).Key = "VillagePet";
			ConversationsAPI.addSimpleConversationToObject(gameObject, gameObject.GetTag("SimpleConversation", "*does not react*"), "Live and drink.", null, null, null, false, true);
			return gameObject;
		}

		// Token: 0x06007130 RID: 28976 RVA: 0x00297740 File Offset: 0x00295940
		public List<PopulationResult> ResolveBuildingContents(List<PopulationResult> templateResults)
		{
			List<PopulationResult> list = new List<PopulationResult>(templateResults.Count);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<PopulationResult> list2;
			using (ScopeDisposedList<string> fromPool = ScopeDisposedList<string>.GetFromPool())
			{
				foreach (PopulationResult populationResult in templateResults)
				{
					for (int i = 0; i < populationResult.Number; i++)
					{
						if (!populationResult.Blueprint.StartsWith("*"))
						{
							list.Add(new PopulationResult(populationResult.Blueprint, 1, null, null));
						}
						else if (this.staticVillageResults.ContainsKey(populationResult.Blueprint) && !Stat.Chance(20))
						{
							list.Add(new PopulationResult(this.staticVillageResults[populationResult.Blueprint], 1, null, null));
						}
						else if (dictionary.ContainsKey(populationResult.Blueprint) && !Stat.Chance(20))
						{
							list.Add(new PopulationResult(dictionary[populationResult.Blueprint], 1, null, null));
						}
						else
						{
							string text = populationResult.Blueprint.Replace("*", "");
							PopulationInfo populationInfo;
							if (!this.ChiliadTables.TryGetValue(populationResult.Blueprint, out populationInfo))
							{
								fromPool.Clear();
								fromPool.Add("SemanticChiliad");
								foreach (ReadOnlySpan<char> readOnlySpan in text.DelimitedBy(','))
								{
									fromPool.Add("Semantic" + readOnlySpan.ToString());
								}
								populationInfo = (this.ChiliadTables[populationResult.Blueprint] = GameObjectFactory.Factory.FabricateTagTable(populationResult.Blueprint, fromPool, -1, this.villageTechTier, false, true, false));
							}
							PopulationResult populationResult2 = populationInfo.GenerateOne(null, null);
							if (populationResult2 == null)
							{
								populationResult2 = PopulationManager.RollOneFrom("DynamicSemanticTable:" + text + "::" + this.villageTechTier.ToString(), null, null);
							}
							if (populationResult2 == null || populationResult2.Blueprint.IsNullOrEmpty())
							{
								Debug.LogError("Couldn't resolve object for " + populationResult.Blueprint);
							}
							else
							{
								populationResult2.Hint = populationResult.Hint;
								list.Add(populationResult2);
								if (this.staticPerBuilding.Contains(populationResult.Blueprint) && !dictionary.ContainsKey(populationResult.Blueprint))
								{
									dictionary.Add(populationResult.Blueprint, populationResult2.Blueprint);
								}
								if (this.staticPerVillage.Contains(populationResult.Blueprint) && !this.staticVillageResults.ContainsKey(populationResult.Blueprint))
								{
									this.staticVillageResults.Add(populationResult.Blueprint, populationResult2.Blueprint);
								}
							}
						}
					}
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x06007131 RID: 28977 RVA: 0x00297A48 File Offset: 0x00295C48
		public override void addInitialStructures()
		{
			List<ISultanDungeonSegment> list = new List<ISultanDungeonSegment>();
			int num = 7;
			int num2 = 72;
			int num3 = 7;
			int num4 = 17;
			string blueprint = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_InitialStructureSegmentation"), null, "Full").Blueprint;
			if (blueprint == "None")
			{
				return;
			}
			foreach (string text in blueprint.Split(';', StringSplitOptions.None))
			{
				if (text == "FullHMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "HMirror"
					});
				}
				else if (text == "FullVMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "VMirror"
					});
				}
				else if (text == "FullHVMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "HVMirror"
					});
				}
				else if (text == "Full")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22)));
				}
				else if (text.StartsWith("BSP:"))
				{
					int num5 = Convert.ToInt32(text.Split(':', StringSplitOptions.None)[1]);
					base.partition(new Rect2D(2, 2, 78, 24), ref num5, list);
				}
				else if (text.StartsWith("Ring:"))
				{
					int num6 = Convert.ToInt32(text.Split(':', StringSplitOptions.None)[1]);
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22)));
					if (num6 == 2)
					{
						list.Add(new SultanRectDungeonSegment(new Rect2D(20, 8, 60, 16)));
					}
					if (num6 == 3)
					{
						list.Add(new SultanRectDungeonSegment(new Rect2D(15, 8, 65, 16)));
						list.Add(new SultanRectDungeonSegment(new Rect2D(25, 10, 55, 14)));
					}
				}
				else if (text.StartsWith("Blocks"))
				{
					string[] array2 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					int num7 = array2[0].RollCached();
					for (int j = 0; j < num7; j++)
					{
						int num8 = array2[1].RollCached();
						int num9 = array2[2].RollCached();
						int num10 = Stat.Random(2, 78 - num8);
						int num11 = Stat.Random(2, 23 - num9);
						int num12 = num10 + num8;
						int num13 = num11 + num9;
						if (num < num10)
						{
							num = num10;
						}
						if (num2 > num12)
						{
							num2 = num12;
						}
						if (num3 < num11)
						{
							num3 = num11;
						}
						if (num4 > num13)
						{
							num4 = num13;
						}
						SultanRectDungeonSegment sultanRectDungeonSegment = new SultanRectDungeonSegment(new Rect2D(num10, num11, num10 + num8, num11 + num9));
						if (text.Contains("[HMirror]"))
						{
							sultanRectDungeonSegment.mutator = "HMirror";
						}
						if (text.Contains("[VMirror]"))
						{
							sultanRectDungeonSegment.mutator = "VMirror";
						}
						if (text.Contains("[HVMirror]"))
						{
							sultanRectDungeonSegment.mutator = "HVMirror";
						}
						list.Add(sultanRectDungeonSegment);
					}
				}
				else if (text.StartsWith("Circle"))
				{
					string[] array3 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanCircleDungeonSegment(Location2D.Get(array3[0].RollCached(), array3[1].RollCached()), array3[2].RollCached()));
				}
				else if (text.StartsWith("Tower"))
				{
					string[] array4 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanTowerDungeonSegment(Location2D.Get(array4[0].RollCached(), array4[1].RollCached()), array4[2].RollCached(), array4[3].RollCached()));
				}
			}
			ColorOutputMap colorOutputMap = new ColorOutputMap(80, 25);
			for (int k = 0; k < list.Count; k++)
			{
				string text2 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_StructureTemplate"), null, null).Blueprint;
				int num14 = 3;
				string text3 = "";
				string blueprint2 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_StructureTemplate"), null, null).Blueprint;
				int num15 = 3;
				if (text2.Contains(","))
				{
					string[] array5 = text2.Split(',', StringSplitOptions.None);
					text2 = array5[0];
					text3 = array5[1];
				}
				WaveCollapseFastModel waveCollapseFastModel = new WaveCollapseFastModel(text2, num14, list[k].width(), list[k].height(), true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				if (!text3.IsNullOrEmpty())
				{
					waveCollapseFastModel.ClearColors(text3, "border1");
				}
				waveCollapseFastModel.UpdateSample(blueprint2.Split(',', StringSplitOptions.None)[0], num15, true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				ColorOutputMap colorOutputMap2 = new ColorOutputMap(waveCollapseFastModel);
				colorOutputMap2.ReplaceBorders(new Color32(byte.MaxValue, 0, 0, byte.MaxValue), new Color32(0, 0, 0, byte.MaxValue));
				if (list[k].mutator == "HMirror")
				{
					colorOutputMap2.HMirror();
				}
				if (list[k].mutator == "VMirror")
				{
					colorOutputMap2.VMirror();
				}
				if (list[k].mutator == "HVMirror")
				{
					colorOutputMap2.HMirror();
					colorOutputMap2.VMirror();
				}
				colorOutputMap.Paste(colorOutputMap2, list[k].x1, list[k].y1);
				MemoryHelper.GCCollect();
			}
			string text4 = base.RollOneFrom("Village_InitialStructureSegmentationFullscreenMutation");
			int num16 = 0;
			int num17 = 0;
			for (int l = 0; l < list.Count; l++)
			{
				string text5 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_StructureWall"), null, null).Blueprint;
				if (text5 == "*auto")
				{
					text5 = base.GetDefaultWall(this.zone);
				}
				for (int m = list[l].y1; m < list[l].y2; m++)
				{
					for (int n = list[l].x1; n < list[l].x2; n++)
					{
						if (list[l].contains(n, m))
						{
							for (int num18 = l + 1; num18 < list.Count; num18++)
							{
								if (list[num18].contains(n, m))
								{
									goto IL_0847;
								}
							}
							Color32 color = colorOutputMap.getPixel(n, m);
							if (list[l].HasCustomColor(n, m))
							{
								color = list[l].GetCustomColor(n, m);
							}
							if (WaveCollapseTools.equals(color, ColorOutputMap.BLACK))
							{
								this.zone.GetCell(n + num16, m + num17).ClearWalls();
								this.zone.GetCell(n + num16, m + num17).AddObject(text5, null, null, null, null);
								if (text4 == "VMirror" || text4 == "HVMirror")
								{
									this.zone.GetCell(n + num16, this.zone.Height - (m + num17) - 1).ClearWalls();
									this.zone.GetCell(n + num16, this.zone.Height - (m + num17) - 1).AddObject(text5, null, null, null, null);
								}
								if (text4 == "HMirror" || text4 == "HVMirror")
								{
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, m + num17).ClearWalls();
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, m + num17).AddObject(text5, null, null, null, null);
								}
								if (text4 == "HVMirror")
								{
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, this.zone.Height - (m + num17) - 1).ClearWalls();
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, this.zone.Height - (m + num17) - 1).AddObject(text5, null, null, null, null);
								}
							}
						}
						IL_0847:;
					}
				}
			}
		}

		// Token: 0x06007132 RID: 28978 RVA: 0x002982E4 File Offset: 0x002964E4
		public static void villageClear(Zone Z)
		{
			string tag = Z.GetTerrainObject().GetTag("VillageClearBehavior", null);
			if (!tag.IsNullOrEmpty())
			{
				string[] array = tag.Split(':', StringSplitOptions.None);
				if (array[0] == "circles")
				{
					int num = int.Parse(array[1]);
					for (int i = 0; i < num; i++)
					{
						foreach (Cell cell in Z.GetRandomCell(0).GetCellsInACosmeticCircle(Stat.Random(6, 10)))
						{
							Debug.Log("clearing " + cell.X.ToString() + "," + cell.Y.ToString());
							cell.Clear(null, false, false, (GameObject o) => o.GetBlueprint(true).DescendsFrom("Widget"));
						}
					}
				}
			}
		}

		// Token: 0x06007133 RID: 28979 RVA: 0x002983E8 File Offset: 0x002965E8
		public bool BuildZone(Zone Z)
		{
			bool flag = false;
			this.zone = Z;
			this.zone.SetZoneProperty("relaxedbiomes", "true");
			this.zone.SetZoneProperty("faction", this.villageFaction);
			this.villageSnapshot = base.villageEntity.GetCurrentSnapshot();
			this.region = this.villageSnapshot.GetProperty("region", "unknown");
			this.villagerBaseFaction = this.villageSnapshot.GetProperty("baseFaction", "unknown");
			this.villageName = this.villageSnapshot.GetProperty("name", "unknown");
			this.dynamicCreatureTableName = "DynamicObjectsTable:" + this.region + "_Creatures";
			Z.SetZoneProperty("villageEntityId", base.villageEntity.id);
			this.isVillageZero = this.villageSnapshot.GetProperty("isVillageZero", "false").EqualsNoCase("true");
			Tier.Constrain(ref this.villageTier);
			Tier.Constrain(ref this.villageTechTier);
			base.generateVillageTheme();
			base.generateSignatureItems();
			base.generateSignatureDish("Focal");
			base.generateSignatureLiquid();
			base.generateSignatureSkill();
			this.generateStoryType();
			base.getVillageDoorStyle();
			base.makeSureThereIsEnoughSpace();
			foreach (Cell cell in Z.GetCells())
			{
				for (int i = cell.Objects.Count - 1; i >= 0; i--)
				{
					GameObject gameObject = cell.Objects[i];
					if (!gameObject.IsPlayer() && !gameObject.HasTagOrProperty("NoVillageStrip"))
					{
						if (gameObject.HasTagOrProperty("RequireVillagePlacement"))
						{
							gameObject.Physics.CurrentCell = null;
							this.requiredPlacementObjects.Add(gameObject);
						}
						else if (gameObject.IsCombat || gameObject.HasTagOrProperty("BodySubstitute"))
						{
							gameObject.Physics.CurrentCell = null;
							this.originalCreatures.Add(gameObject);
						}
						else if (gameObject.IsWall() && gameObject.HasTag("Category_Settlement"))
						{
							gameObject.Physics.CurrentCell = null;
							this.originalWalls.Add(gameObject);
						}
						else if (gameObject.GetBlueprint(true).InheritsFrom("Plant") || gameObject.GetBlueprint(true).InheritsFrom("BasePlant") || gameObject.GetBlueprint(true).HasTag("PlantLike", false))
						{
							gameObject.Physics.CurrentCell = null;
							if (gameObject != null)
							{
								this.originalPlants.Add(gameObject);
							}
						}
						else if (gameObject.HasPart<LiquidVolume>())
						{
							gameObject.Physics.CurrentCell = null;
							if (gameObject.IsOpenLiquidVolume())
							{
								this.originalLiquids.Add(gameObject);
							}
						}
						else if (gameObject.GetBlueprint(true).InheritsFrom("Furniture"))
						{
							gameObject.Physics.CurrentCell = null;
							this.originalFurniture.Add(gameObject);
						}
						else if (gameObject.GetBlueprint(true).InheritsFrom("Item"))
						{
							gameObject.Physics.CurrentCell = null;
							this.originalItems.Add(gameObject);
						}
					}
				}
			}
			VillageCoda.villageClear(Z);
			this.addInitialStructures();
			this.FabricatePlayerStatue();
			Rect2D rect2D = Rect2D.zero;
			if (EndGame.IsUltimate)
			{
				rect2D = base.ReserveUltraPlot(VillageCoda.IsRuined);
			}
			if (VillageCoda.IsPlagued)
			{
				base.FabricatePlagueyard();
				for (int j = Stat.Random(3, 5); j >= 0; j--)
				{
					base.FabricateVantabudCluster();
				}
			}
			InfluenceMap regionMap = new InfluenceMap(Z.Width, Z.Height);
			for (int k = 0; k < Z.Width; k++)
			{
				for (int l2 = 0; l2 < Z.Height; l2++)
				{
					regionMap.Walls[k, l2] = (Z.GetCell(k, l2).HasObjectWithTagOrProperty("Wall") ? 1 : 0);
				}
			}
			base.AddAvoidedToMap(regionMap);
			try
			{
				regionMap.SeedAllUnseeded(false, true);
				goto IL_0453;
			}
			catch (Exception)
			{
				base.makeSureThereIsEnoughSpace();
				regionMap.SeedAllUnseeded(false, true);
				goto IL_0453;
			}
			IL_0447:
			regionMap.AddSeedAtMaximaInLargestSeed(false);
			IL_0453:
			if (regionMap.LargestSize() <= 150)
			{
				regionMap.SeedGrowthProbability = new List<int>();
				for (int m = 0; m < regionMap.Seeds.Count; m++)
				{
					regionMap.SeedGrowthProbability.Add(Stat.Random(10, 1000));
				}
				regionMap.Recalculate(flag);
				int num = Stat.Random(4, 9);
				int num2 = 0;
				int num3 = regionMap.FindClosestSeedTo(Location2D.Get(40, 13), (InfluenceMapRegion r) => r.maxRect.ReduceBy(1, 1).Width >= 6 && r.maxRect.ReduceBy(1, 1).Height >= 6 && r.AdjacentRegions.Count > 1);
				Location2D location2D = regionMap.Seeds[num3];
				this.townSquare = regionMap.Regions[num3];
				this.townSquareLayout = null;
				foreach (InfluenceMapRegion influenceMapRegion in regionMap.Regions)
				{
					Rect2D Rect = GridTools.MaxRectByArea(influenceMapRegion.GetGrid(), false, 0, 0).Translate(influenceMapRegion.BoundingBox.UpperLeft).ReduceBy(1, 1);
					PopulationLayout populationLayout = new PopulationLayout(Z, influenceMapRegion, Rect);
					if (influenceMapRegion.AdjacentRegions.Count <= 1 && influenceMapRegion.Size >= 9 && !influenceMapRegion.IsEdgeRegion() && influenceMapRegion != this.townSquare)
					{
						this.buildings.Add(populationLayout);
					}
					else if ((Rect.Width >= 5 && Rect.Height >= 5 && num > 0) || influenceMapRegion == this.townSquare)
					{
						List<GameObject> originalLiquids = this.originalLiquids;
						string text;
						if (originalLiquids == null)
						{
							text = null;
						}
						else
						{
							GameObject randomElement = originalLiquids.GetRandomElement<GameObject>(null);
							text = ((randomElement != null) ? randomElement.Blueprint : null);
						}
						string text2 = text ?? base.getZoneDefaultLiquid(this.zone);
						if (influenceMapRegion == this.townSquare)
						{
							this.townSquareLayout = populationLayout;
							if (base.fabricateStoryBuilding())
							{
								this.buildings.Add(populationLayout);
							}
						}
						else
						{
							string text3 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_BuildingStyle"), null, null).Blueprint;
							if (text3.StartsWith("wfc,") && !base.getWfcBuildingTemplate(text3.Split(',', StringSplitOptions.None)[1]).Any<ColorOutputMap>((ColorOutputMap t) => t.extrawidth <= Rect.Width && t.extraheight <= Rect.Height))
							{
								text3 = "squarehut";
							}
							this.buildings.Add(populationLayout);
							if (text3 == "burrow")
							{
								base.FabricateBurrow(populationLayout);
								populationLayout.hasStructure = true;
							}
							if (text3 == "aerie")
							{
								base.FabricateAerie(populationLayout);
							}
							if (text3 == "pond")
							{
								base.FabricatePond(populationLayout, text2);
							}
							if (text3 == "islandpond")
							{
								base.FabricateIslandPond(populationLayout, text2);
								populationLayout.hasStructure = true;
							}
							if (text3 == "walledpond")
							{
								base.FabricateWalledPond(populationLayout, text2);
								populationLayout.hasStructure = true;
							}
							if (text3 == "walledislandpond")
							{
								base.FabricateWalledIslandPond(populationLayout, text2);
								populationLayout.hasStructure = true;
							}
							if (text3 == "tent")
							{
								base.FabricateTent(populationLayout);
								populationLayout.hasStructure = true;
							}
							if (text3 == "roundhut")
							{
								base.FabricateHut(populationLayout, true);
								populationLayout.hasStructure = true;
							}
							if (text3 == "squarehut")
							{
								base.FabricateHut(populationLayout, false);
								populationLayout.hasStructure = true;
							}
							if (text3.StartsWith("wfc,"))
							{
								base.getWfcBuildingTemplate(text3.Split(',', StringSplitOptions.None)[1]).ShuffleInPlace<ColorOutputMap>(null);
								bool flag2 = false;
								foreach (ColorOutputMap colorOutputMap in base.getWfcBuildingTemplate(text3.Split(',', StringSplitOptions.None)[1]))
								{
									int num4 = colorOutputMap.width / 2;
									int num5 = colorOutputMap.height / 2;
									if (colorOutputMap.extrawidth <= populationLayout.innerRect.Width && colorOutputMap.extraheight <= populationLayout.innerRect.Height)
									{
										for (int n = 0; n < colorOutputMap.width; n++)
										{
											for (int num6 = 0; num6 < colorOutputMap.height; num6++)
											{
												Cell cell2 = Z.GetCell(populationLayout.position.X - num4 + n, populationLayout.position.Y - num5 + num6);
												if (cell2 != null)
												{
													if (colorOutputMap.getPixel(n, num6).Equals(ColorOutputMap.BLACK))
													{
														cell2.AddObject(base.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
													}
													else
													{
														colorOutputMap.getPixel(n, num6).Equals(ColorOutputMap.RED);
													}
												}
											}
										}
										populationLayout.hasStructure = true;
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									base.FabricateHut(populationLayout, false);
									populationLayout.hasStructure = true;
								}
							}
							num--;
							num2++;
						}
					}
					else if (influenceMapRegion.AdjacentRegions.Count == 1 && !influenceMapRegion.IsEdgeRegion() && this.townSquare != influenceMapRegion)
					{
						VillageCodaBase.MakeCaveBuilding(Z, regionMap, influenceMapRegion, "Shale");
						this.buildings.Add(populationLayout);
						populationLayout.hasStructure = true;
					}
				}
				base.placeStatues();
				regionMap.SeedAllUnseeded(flag, true);
				base.CarvePathwaysFromLocations(Z, true, regionMap, location2D);
				this.zone.ClearReachableMap(false);
				this.zone.BuildReachableMap(location2D.X, location2D.Y);
				base.SnakeToConnections(Location2D.Get(location2D.X, location2D.Y));
				base.clearDegenerateDoors();
				base.applyDoorFilters();
				for (int num7 = 0; num7 < Z.Width; num7++)
				{
					for (int num8 = 0; num8 < Z.Height; num8++)
					{
						regionMap.Walls[num7, num8] = (Z.GetCell(num7, num8).HasObjectWithTag("Wall") ? 1 : 0);
					}
				}
				base.AddAvoidedToMap(regionMap);
				List<Location2D> list = new List<Location2D>();
				foreach (PopulationLayout populationLayout2 in this.buildings)
				{
					Location2D position = populationLayout2.position;
					if (position != null)
					{
						list.Add(position);
					}
				}
				regionMap.Recalculate(flag);
				InfluenceMap influenceMap = regionMap.copy();
				using (Pathfinder pathfinder = this.zone.getPathfinder(null))
				{
					NoiseMap noiseMap = new NoiseMap(80, 25, 10, 3, 3, 4, 80, 80, 6, 3, -3, 1, new List<NoiseMapNode>());
					for (int num9 = 0; num9 < this.zone.Width; num9++)
					{
						for (int num10 = 0; num10 < this.zone.Height; num10++)
						{
							if (this.zone.GetCell(num9, num10).HasWall() || base.IsPointAvoided(num9, num10))
							{
								pathfinder.CurrentNavigationMap[num9, num10] = 4999;
							}
							else
							{
								pathfinder.CurrentNavigationMap[num9, num10] = noiseMap.Noise[num9, num10];
							}
						}
					}
					foreach (PopulationLayout populationLayout3 in this.buildings)
					{
						foreach (Location2D location2D2 in populationLayout3.originalRegion.Cells)
						{
							int x3 = location2D2.X;
							int y = location2D2.Y;
							if (x3 != 0 && x3 != 79 && y != 0 && y != 24 && Z.GetCell(x3, y).IsEmpty())
							{
								int num11 = 0;
								int num12 = 0;
								if (Z.GetCell(x3 - 1, y).HasWall() || Z.GetCell(x3 - 1, y).HasObjectWithTag("Door"))
								{
									num12++;
								}
								if (Z.GetCell(x3 + 1, y).HasWall() || Z.GetCell(x3 + 1, y).HasObjectWithTag("Door"))
								{
									num12++;
								}
								if (Z.GetCell(x3, y - 1).HasWall() || Z.GetCell(x3, y - 1).HasObjectWithTag("Door"))
								{
									num11++;
								}
								if (Z.GetCell(x3, y + 1).HasWall() || Z.GetCell(x3, y + 1).HasObjectWithTag("Door"))
								{
									num11++;
								}
								if ((num11 == 2 && num12 == 0) || (num11 == 0 && num12 == 2))
								{
									influenceMap.Walls[x3, y] = 1;
								}
							}
						}
					}
					for (int num13 = 0; num13 < 80; num13++)
					{
						for (int num14 = 0; num14 < 25; num14++)
						{
							if (this.burrowedDoors.Contains(Location2D.Get(num13, num14)))
							{
								influenceMap.Walls[num13, num14] = 1;
							}
						}
					}
					influenceMap.Recalculate(flag);
					string blueprint = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_BuildingFloor"), null, null).Blueprint;
					string text4 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_BuildingPath"), null, null).Blueprint;
					if (text4 == "Pond")
					{
						text4 = base.getZoneDefaultLiquid(this.zone);
					}
					foreach (GameObject gameObject2 in Z.FindObjects("PathConnection"))
					{
						if (pathfinder.FindPath(gameObject2.CurrentCell.Location, location2D, false, true, 9999, false))
						{
							foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
							{
								if (!text4.IsNullOrEmpty())
								{
									this.zone.GetCell(pathfinderNode.pos).RequireObject(text4, null, null, null, null);
								}
								if (!this.buildingPaths.Contains(pathfinderNode.pos))
								{
									this.buildingPaths.Add(pathfinderNode.pos);
								}
							}
						}
					}
					foreach (PopulationLayout populationLayout4 in this.buildings)
					{
						if (pathfinder.FindPath(populationLayout4.position, location2D, false, true, 9999, false))
						{
							foreach (PathfinderNode pathfinderNode2 in pathfinder.Steps)
							{
								if (!text4.IsNullOrEmpty())
								{
									this.zone.GetCell(pathfinderNode2.pos).AddObject(text4, null, null, null, null);
								}
								if (!this.buildingPaths.Contains(pathfinderNode2.pos))
								{
									this.buildingPaths.Add(pathfinderNode2.pos);
								}
							}
						}
						foreach (Location2D location2D3 in populationLayout4.originalRegion.Cells)
						{
							if (!Z.GetCell(location2D3).HasWall() && !this.buildingPaths.Contains(location2D3))
							{
								if (influenceMap.Regions.Count<InfluenceMapRegion>() <= populationLayout4.region.Seed)
								{
									populationLayout4.outside.Add(location2D3);
									int num15 = Z.GetCell(location2D3).CountObjectWithTagCardinalAdjacent("Wall");
									if (num15 > 0)
									{
										populationLayout4.outsideWall.Add(location2D3);
									}
									if (num15 >= 2)
									{
										populationLayout4.outsideCorner.Add(location2D3);
									}
								}
								else if (!influenceMap.Regions[populationLayout4.region.Seed].Cells.Contains(location2D3))
								{
									populationLayout4.outside.Add(location2D3);
									int num16 = Z.GetCell(location2D3).CountObjectWithTagCardinalAdjacent("Wall");
									if (num16 > 0)
									{
										populationLayout4.outsideWall.Add(location2D3);
									}
									if (num16 >= 2)
									{
										populationLayout4.outsideCorner.Add(location2D3);
									}
								}
								else
								{
									populationLayout4.inside.Add(location2D3);
									if (!blueprint.IsNullOrEmpty())
									{
										Z.GetCell(location2D3).AddObject(blueprint, null, null, null, null);
									}
									int num17 = Z.GetCell(location2D3).CountObjectWithTagCardinalAdjacent("Wall");
									if (num17 > 0)
									{
										populationLayout4.insideWall.Add(location2D3);
									}
									if (num17 >= 2)
									{
										populationLayout4.insideCorner.Add(location2D3);
									}
								}
							}
						}
					}
				}
				Dictionary<InfluenceMapRegion, Rect2D> dictionary = new Dictionary<InfluenceMapRegion, Rect2D>();
				Dictionary<InfluenceMapRegion, string> dictionary2 = new Dictionary<InfluenceMapRegion, string>();
				InfluenceMap influenceMap2 = new InfluenceMap(Z.Width, Z.Height);
				influenceMap2.Seeds = new List<Location2D>(regionMap.Seeds);
				Z.SetInfluenceMapWalls(influenceMap2.Walls);
				base.AddAvoidedToMap(influenceMap2);
				influenceMap2.Recalculate(flag);
				int num18 = 0;
				for (int num19 = 0; num19 < influenceMap2.Regions.Count; num19++)
				{
					InfluenceMapRegion R = influenceMap2.Regions[num19];
					Rect2D rect2D2;
					if (!dictionary.ContainsKey(R))
					{
						rect2D2 = GridTools.MaxRectByArea(R.GetGrid(), false, 0, 0).Translate(R.BoundingBox.UpperLeft);
						dictionary.Add(R, rect2D2);
					}
					else
					{
						rect2D2 = dictionary[R];
					}
					if (num19 != num3)
					{
						if (list.Contains(regionMap.Seeds[R.Seed]))
						{
							dictionary2.Add(R, "building");
							PopulationLayout populationLayout5 = this.buildings.First<PopulationLayout>((PopulationLayout b) => b.position == regionMap.Seeds[R.Seed]);
							string text5 = base.RollOneFrom("Villages_BuildingTheme_" + this.villageTheme);
							using (List<PopulationResult>.Enumerator enumerator8 = this.ResolveBuildingContents(PopulationManager.Generate(base.ResolvePopulationTableName("Villages_BuildingContents_Dwelling_" + text5), null, null, null, null, null)).GetEnumerator())
							{
								while (enumerator8.MoveNext())
								{
									PopulationResult populationResult = enumerator8.Current;
									base.PlaceObjectInBuilding(populationResult, populationLayout5, null);
								}
								goto IL_1E44;
							}
						}
						if (rect2D2.Area >= 4)
						{
							dictionary2.Add(R, "greenspace");
							if (num18 == 0 && this.signatureHistoricObjectInstance != null)
							{
								string text6 = "IronFence";
								string text7 = "Iron Gate";
								Z.GetCell(rect2D2.Center).AddObject(this.signatureHistoricObjectInstance, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								ZoneBuilderSandbox.encloseRectWithWall(this.zone, new Rect2D(rect2D2.Center.x - 1, rect2D2.Center.y - 1, rect2D2.Center.x + 1, rect2D2.Center.y + 1), text6);
								Z.GetCell(rect2D2.Center).GetCellFromDirection(Directions.GetRandomCardinalDirection(), true).Clear(null, false, false, null)
									.AddObject(text7, null, null, null, null);
							}
							else
							{
								string blueprint2 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_GreenspaceContents"), null, null).Blueprint;
								int num20 = 20;
								if (blueprint2 == "aquaculture")
								{
									List<GameObject> originalLiquids2 = this.originalLiquids;
									string text8;
									if (originalLiquids2 == null)
									{
										text8 = null;
									}
									else
									{
										GameObject randomElement2 = originalLiquids2.GetRandomElement<GameObject>(null);
										text8 = ((randomElement2 != null) ? randomElement2.Blueprint : null);
									}
									string text9 = text8 ?? base.getZoneDefaultLiquid(this.zone);
									GameObject afarmablePlant = base.getAFarmablePlant();
									Maze maze = RecursiveBacktrackerMaze.Generate(Math.Max(1, R.BoundingBox.Width / 3 + 1), Math.Max(1, R.BoundingBox.Height / 3 + 1), false, ZoneBuilderSandbox.GetOracleIntFromString("aquaculture" + num18.ToString(), 0, 2147483646));
									for (int num21 = R.BoundingBox.x1; num21 <= R.BoundingBox.x2; num21++)
									{
										for (int num22 = R.BoundingBox.y1; num22 <= R.BoundingBox.y2; num22++)
										{
											int num23 = (num21 - R.BoundingBox.x1) / 3;
											int num24 = (num22 - R.BoundingBox.y1) / 3;
											int num25 = (num21 - R.BoundingBox.x1) % 3;
											int num26 = (num22 - R.BoundingBox.y1) % 3;
											bool flag3 = false;
											if (num25 == 1 && num26 == 1)
											{
												flag3 = maze.Cell[num23, num24].AnyOpen();
											}
											if (num25 == 1 && num26 == 0)
											{
												flag3 = maze.Cell[num23, num24].N;
											}
											if (num25 == 1 && num26 == 2)
											{
												flag3 = maze.Cell[num23, num24].S;
											}
											if (num25 == 2 && num26 == 1)
											{
												flag3 = maze.Cell[num23, num24].E;
											}
											if (num25 == 0 && num26 == 1)
											{
												flag3 = maze.Cell[num23, num24].W;
											}
											if (flag3)
											{
												if (R.Cells.Contains(Location2D.Get(num21, num22)) && !this.buildingPaths.Contains(Location2D.Get(num21, num22)))
												{
													Cell cell3 = Z.GetCell(num21, num22);
													if (cell3 != null)
													{
														cell3.AddObject(afarmablePlant.Blueprint, new Action<GameObject>(base.setVillageDomesticatedProperties), null);
													}
												}
											}
											else if (R.Cells.Contains(Location2D.Get(num21, num22)) && Z.GetCell(num21, num22) != null)
											{
												Z.GetCell(num21, num22).AddObject(text9, null, null, null, null);
											}
										}
									}
								}
								else if (blueprint2 == "farm" && rect2D2.Area >= num20 && rect2D2.Width >= 7 && rect2D2.Height <= 7)
								{
									rect2D2 = rect2D2.ReduceBy(1, 1).Clamp(1, 1, 78, 23);
									if (rect2D2.Width <= 6 || rect2D2.Height <= 6)
									{
										goto IL_1E44;
									}
									Location2D location = rect2D2.GetRandomDoorCell("?", 2).location;
									ZoneBuilderSandbox.PlaceObjectOnRect(Z, "BrinestalkFence", rect2D2, false);
									base.GetCell(Z, location).Clear(null, false, false, null);
									base.GetCell(Z, location).AddObject("Brinestalk Gate", null, null, null, null);
									string cellSide = rect2D2.GetCellSide(location.Point);
									Rect2D rect2D3 = rect2D2.ReduceBy(0, 0);
									int num27 = 0;
									if (cellSide == "N")
									{
										num27 = ((Stat.Random(0, 1) == 0) ? 2 : 3);
									}
									if (cellSide == "S")
									{
										num27 = ((Stat.Random(0, 1) == 0) ? 0 : 1);
									}
									if (cellSide == "E")
									{
										num27 = ((Stat.Random(0, 1) == 0) ? 0 : 3);
									}
									if (cellSide == "W")
									{
										num27 = ((Stat.Random(0, 1) == 0) ? 1 : 2);
									}
									if (num27 == 0 || num27 == 1)
									{
										rect2D3.y2 = rect2D3.y1 + 3;
									}
									else
									{
										rect2D3.y1 = rect2D3.y2 - 3;
									}
									if (num27 == 0 || num27 == 3)
									{
										rect2D3.x2 = rect2D3.x1 + 3;
									}
									else
									{
										rect2D3.x1 = rect2D3.x2 - 3;
									}
									base.ClearRect(Z, rect2D3);
									ZoneBuilderSandbox.PlaceObjectOnRect(Z, base.getAVillageWall(), rect2D3, false);
									Location2D location2 = rect2D3.GetRandomDoorCell(cellSide, 1).location;
									Z.GetCell(location2).Clear(null, false, false, null);
									Z.GetCell(location2).AddObject(base.getAVillageDoor(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
									this.burrowedDoors.Add(Location2D.Get(location2.X, location2.Y));
									ZoneBuilderSandbox.PlacePopulationInRect(Z, rect2D2.ReduceBy(1, 1), base.ResolvePopulationTableName("Villages_FarmAnimals"), new Action<GameObject>(base.setVillageDomesticatedProperties));
									ZoneBuilderSandbox.PlacePopulationInRect(Z, rect2D3.ReduceBy(1, 1), base.ResolvePopulationTableName("Villages_FarmHutContents"));
								}
								else if (blueprint2 == "garden" || blueprint2 == "farm")
								{
									int num28 = Stat.Random(1, 4);
									GameObject afarmablePlant2 = base.getAFarmablePlant();
									List<GameObject> originalLiquids3 = this.originalLiquids;
									string text10;
									if (originalLiquids3 == null)
									{
										text10 = null;
									}
									else
									{
										GameObject randomElement3 = originalLiquids3.GetRandomElement<GameObject>(null);
										text10 = ((randomElement3 != null) ? randomElement3.Blueprint : null);
									}
									string text11 = text10 ?? base.getZoneDefaultLiquid(this.zone);
									if (num28 == 1)
									{
										bool flag4 = Stat.Random(1, 100) <= 33;
										for (int num29 = R.BoundingBox.x1; num29 <= R.BoundingBox.x2; num29++)
										{
											for (int num30 = R.BoundingBox.y1; num30 <= R.BoundingBox.y2; num30++)
											{
												if (num29 % 2 == 0)
												{
													if (R.Cells.Contains(Location2D.Get(num29, num30)) && !this.buildingPaths.Contains(Location2D.Get(num29, num30)))
													{
														Cell cell4 = Z.GetCell(num29, num30);
														if (cell4 != null)
														{
															cell4.AddObject(afarmablePlant2.Blueprint, new Action<GameObject>(base.setVillageDomesticatedProperties), null);
														}
													}
												}
												else if (flag4 && R.Cells.Contains(Location2D.Get(num29, num30)) && Z.GetCell(num29, num30) != null)
												{
													Z.GetCell(num29, num30).AddObject(text11, null, null, null, null);
												}
											}
										}
									}
									if (num28 == 2)
									{
										List<GameObject> originalLiquids4 = this.originalLiquids;
										string text12;
										if (originalLiquids4 == null)
										{
											text12 = null;
										}
										else
										{
											GameObject randomElement4 = originalLiquids4.GetRandomElement<GameObject>(null);
											text12 = ((randomElement4 != null) ? randomElement4.Blueprint : null);
										}
										string text13 = text12 ?? base.getZoneDefaultLiquid(this.zone);
										bool flag5 = Stat.Random(1, 100) <= 33;
										for (int num31 = R.BoundingBox.x1; num31 <= R.BoundingBox.x2; num31++)
										{
											for (int num32 = R.BoundingBox.y1; num32 <= R.BoundingBox.y2; num32++)
											{
												if (num32 % 2 == 0)
												{
													if (R.Cells.Contains(Location2D.Get(num31, num32)) && !this.buildingPaths.Contains(Location2D.Get(num31, num32)))
													{
														Cell cell5 = Z.GetCell(num31, num32);
														if (cell5 != null)
														{
															cell5.AddObject(afarmablePlant2.Blueprint, new Action<GameObject>(base.setVillageDomesticatedProperties), null);
														}
													}
												}
												else if (flag5 && R.Cells.Contains(Location2D.Get(num31, num32)) && Z.GetCell(num31, num32) != null)
												{
													Z.GetCell(num31, num32).AddObject(text13, null, null, null, null);
												}
											}
										}
									}
									if (num28 == 3)
									{
										int num33 = Stat.Random(20, 98);
										for (int num34 = R.BoundingBox.x1; num34 <= R.BoundingBox.x2; num34++)
										{
											for (int num35 = R.BoundingBox.y1; num35 <= R.BoundingBox.y2; num35++)
											{
												if (R.Cells.Contains(Location2D.Get(num34, num35)) && !this.buildingPaths.Contains(Location2D.Get(num34, num35)) && Stat.Random(1, 100) <= num33)
												{
													Cell cell6 = Z.GetCell(num34, num35);
													if (cell6 != null)
													{
														cell6.AddObject(afarmablePlant2.Blueprint, new Action<GameObject>(base.setVillageDomesticatedProperties), null);
													}
												}
											}
										}
									}
									if (num28 == 4)
									{
										int num36 = Stat.Random(20, 98);
										for (int num37 = R.BoundingBox.x1; num37 <= R.BoundingBox.x2; num37++)
										{
											for (int num38 = R.BoundingBox.y1; num38 <= R.BoundingBox.y2; num38++)
											{
												if (R.Cells.Contains(Location2D.Get(num37, num38)) && !this.buildingPaths.Contains(Location2D.Get(num37, num38)) && Stat.Random(1, 100) <= num36)
												{
													Cell cell7 = Z.GetCell(num37, num38);
													if (cell7 != null)
													{
														cell7.AddObject(base.getAFarmablePlant(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
													}
												}
											}
										}
									}
								}
							}
							num18++;
						}
						else if (influenceMap2.SeedToRegionMap[R.Seed].AdjacentRegions.Count == 1)
						{
							dictionary2.Add(R, "cubby");
						}
						else
						{
							dictionary2.Add(R, "hall");
						}
					}
					IL_1E44:;
				}
				base.placeNonTakeableSignatureItems();
				this.buildings.RemoveAll((PopulationLayout b) => b.inside.Count == 0 && b.outside.Count == 0);
				GameObject gameObject3 = base.generateVillageOven();
				base.PlaceObjectInBuilding(gameObject3, this.buildings.GetRandomElement<PopulationLayout>(null), If.OneIn(10, null, null) ? "Outside" : "Inside", (Location2D l) => !this.zone.GetCell(l).HasOpenLiquidVolume() && !this.zone.GetCell(l).MightBlockPaths());
				if (gameObject3 != null && gameObject3.CurrentCell != null)
				{
					gameObject3.CurrentCell.RemoveObjects((GameObject o) => o.IsOpenLiquidVolume());
				}
				PopulationLayout populationLayout6 = base.PickBuilding(false);
				PopulationLayout populationLayout7 = base.PickBuilding(false);
				PopulationLayout populationLayout8 = base.PickBuilding(false);
				PopulationLayout populationLayout9 = base.PickBuilding(false);
				if (this.villageSnapshot.GetProperty("abandoned", "unknown") != "true")
				{
					GameObject gameObject4 = null;
					GameObject gameObject5 = null;
					GameObject gameObject6 = null;
					GameObject gameObject7 = null;
					GameObject gameObject8 = null;
					if (this.villageSnapshot.listProperties.ContainsKey("immigrant_type"))
					{
						List<string> list2 = this.villageSnapshot.listProperties["immigrant_type"];
						List<string> list3 = this.villageSnapshot.listProperties["immigrant_name"];
						List<string> list4 = this.villageSnapshot.listProperties["immigrant_gender"];
						List<string> list5 = this.villageSnapshot.listProperties["immigrant_role"];
						List<string> list6 = this.villageSnapshot.listProperties["immigrant_dialogWhy_Q"];
						List<string> list7 = this.villageSnapshot.listProperties["immigrant_dialogWhy_A"];
						for (int num39 = 0; num39 < list2.Count; num39++)
						{
							string text14 = list2[num39];
							string text15;
							if (num39 >= list3.Count)
							{
								Debug.LogWarning("missing immigrant name for " + text14 + " in position " + num39.ToString());
								text15 = "MISSING_NAME";
							}
							else
							{
								text15 = list3[num39];
							}
							string text16;
							if (num39 >= list4.Count)
							{
								Debug.LogWarning("missing immigrant gender for " + text14 + " in position " + num39.ToString());
								text16 = null;
							}
							else
							{
								text16 = list4[num39];
							}
							string text17;
							if (num39 >= list5.Count)
							{
								Debug.LogWarning("missing immigrant role for " + text14 + " in position " + num39.ToString());
								text17 = "villager";
							}
							else
							{
								text17 = list5[num39];
							}
							string text18;
							if (num39 >= list6.Count)
							{
								Debug.LogWarning("missing immigrant dialog why Q for " + text14 + " in position " + num39.ToString());
								text18 = "MISSING_QUESTION";
							}
							else
							{
								text18 = list6[num39];
							}
							string text19;
							if (num39 >= list7.Count)
							{
								Debug.LogWarning("missing immigrant dialog why A for " + text14 + " in position " + num39.ToString());
								text19 = "MISSING_ANSWER";
							}
							else
							{
								text19 = list7[num39];
							}
							try
							{
								GameObject gameObject9 = this.generateImmigrant(text14, text15, text16, text17, text18, text19);
								if (text17 == "mayor")
								{
									gameObject4 = gameObject9;
								}
								else if (text17 == "merchant")
								{
									gameObject5 = gameObject9;
								}
								else if (text17 == "tinker")
								{
									gameObject6 = gameObject9;
								}
								else if (text17 == "apothecary")
								{
									gameObject7 = gameObject9;
								}
								else if (text17 == "warden")
								{
									gameObject8 = gameObject9;
								}
								else
								{
									ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), gameObject9, null, null, false);
								}
							}
							catch (Exception ex)
							{
								MetricsManager.LogException("Failed to generate immigrant.", ex, "game_exception");
							}
						}
					}
					if (this.villageSnapshot.GetProperty("government", "unknown") != "anarchism")
					{
						GameObject gameObject10 = null;
						if (this.villageSnapshot.GetProperty("government", "unknown") == "colonialism")
						{
							gameObject10 = GameObject.Create(this.villageSnapshot.GetProperty("colonistType", "unknown"));
						}
						if (gameObject8 != null)
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, gameObject8, null, null, false);
						}
						else
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, this.generateWarden(gameObject10, this.isVillageZero), null, null, false);
						}
					}
					GameObject gameObject11 = null;
					if (this.villageSnapshot.GetProperty("government", "unknown") == "colonialism")
					{
						gameObject11 = GameObject.Create(this.villageSnapshot.GetProperty("colonistType", "unknown"));
						base.setVillagerProperties(gameObject11, true);
					}
					if (gameObject4 != null)
					{
						base.PlaceObjectInBuilding(gameObject4, populationLayout6, If.OneIn(100, null, null) ? "Outside" : "Inside", null);
					}
					else
					{
						base.PlaceObjectInBuilding(this.generateMayor(gameObject11, "SpecialVillagerHeroTemplate_" + this.mayorTemplate, true), populationLayout6, If.OneIn(100, null, null) ? "Outside" : "Inside", null);
					}
					if (gameObject5 != null)
					{
						base.PlaceObjectInBuilding(gameObject5, populationLayout9, If.OneIn(100, null, null) ? "Outside" : "Inside", null);
					}
					else if (this.isVillageZero || If.Chance(100, null, null))
					{
						base.PlaceObjectInBuilding(this.generateMerchant(null), populationLayout9, If.OneIn(100, null, null) ? "Outside" : "Inside", null);
					}
					if (this.isVillageZero)
					{
						if (this.region == "Saltmarsh")
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, GameObject.Create("WatervineFarmerConvert"), null, null, false);
						}
						else if (this.region == "DesertCanyon")
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, GameObject.Create("PigFarmerConvert"), null, null, false);
						}
						else if (this.region == "Hills")
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, GameObject.Create("CannibalConvert"), null, null, false);
						}
						else if (this.region == "Saltdunes")
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, GameObject.Create("IssachariConvert"), null, null, false);
						}
						else
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, GameObject.Create("WatervineFarmerConvert"), null, null, false);
						}
					}
					GameObject gameObject12 = null;
					bool flag6 = false;
					if (gameObject6 != null)
					{
						gameObject12 = gameObject6;
					}
					else if (this.isVillageZero || If.Chance(100, null, null))
					{
						gameObject12 = this.generateTinker(null);
						flag6 = true;
					}
					if (gameObject12 != null)
					{
						this.PlaceTinker(gameObject12, populationLayout7);
					}
					GameObject gameObject13 = null;
					bool flag7 = false;
					if (gameObject7 != null)
					{
						gameObject13 = gameObject7;
					}
					else if (this.isVillageZero || If.Chance(100, null, null))
					{
						gameObject13 = this.generateApothecary(null);
						flag7 = true;
					}
					if (gameObject13 != null)
					{
						string text20 = (If.OneIn(50, null, null) ? "Outside" : "AlongInsideWall");
						base.PlaceObjectInBuilding(gameObject13, populationLayout8, text20, null);
						int num40 = 0;
						int num41 = Stat.Random(1, 2);
						while (num40 < num41)
						{
							base.PlaceObjectInBuilding(GameObject.Create("Table"), populationLayout8, text20, null);
							num40++;
						}
						int num42 = 0;
						int num43 = Stat.Random(0, 1);
						while (num42 < num43)
						{
							base.PlaceObjectInBuilding(GameObject.Create("Alchemist Table"), populationLayout8, text20, null);
							num42++;
						}
						int num44 = 0;
						int num45 = Stat.Random(2, 3);
						while (num44 < num45)
						{
							base.PlaceObjectInBuilding(GameObject.Create("Woven Basket"), populationLayout8, text20, null);
							num44++;
						}
					}
					int num46 = Stat.Random(4, 10);
					if (!this.isVillageZero)
					{
						if (!flag6)
						{
							num46++;
						}
						if (!flag7)
						{
							num46++;
						}
					}
					if (this.villageSnapshot.listProperties.ContainsKey("populationMultiplier"))
					{
						foreach (string text21 in this.villageSnapshot.listProperties["populationMultiplier"])
						{
							num46 *= int.Parse(text21);
						}
					}
					if (VillageCoda.IsPlagued)
					{
						num46 -= 2;
					}
					for (int num47 = 0; num47 < num46; num47++)
					{
						ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), base.generateVillager(false), null, null, false);
					}
					if (this.villageSnapshot.GetProperty("government", "unknown") == "colonialism")
					{
						int num48 = 0;
						int num49 = Stat.Random(2, 3);
						while (num48 < num49)
						{
							GameObject gameObject14 = GameObject.Create(this.villageSnapshot.GetProperty("colonistType", "unknown"));
							base.setVillagerProperties(gameObject14, true);
							gameObject14.SetIntProperty("SuppressSimpleConversation", 1, false);
							base.AddVillagerConversation(gameObject14, gameObject14.GetTag("SimpleConversation", "Moon and Sun. Wisdom and will.~May the earth yield for us this season.~Peace, =player.formalAddressTerm=."), "Live and drink.", null, null, false, true);
							ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), gameObject14, null, null, false);
							num48++;
						}
					}
					if (this.villageSnapshot.GetProperty("government", "unknown") == "representative democracy")
					{
						int num50 = 0;
						int num51 = Stat.Random(2, 4);
						while (num50 < num51)
						{
							ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), this.generateMayor(null, "SpecialVillagerHeroTemplate_" + this.mayorTemplate, false), null, null, false);
							num50++;
						}
					}
					ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), base.generateVillager(true), null, null, false);
				}
				if (this.villageSnapshot.listProperties.ContainsKey("pet_petSpecies"))
				{
					int x2;
					int x;
					for (x = 0; x < this.villageSnapshot.listProperties["pet_petSpecies"].Count; x = x2 + 1)
					{
						try
						{
							List<string> petNames = new List<string>();
							int num52 = int.Parse(this.villageSnapshot.listProperties["pet_number"][x]);
							for (int num53 = 0; num53 < num52; num53++)
							{
								string text22;
								GameObject gameObject15 = this.generatePet(this.villageSnapshot.listProperties["pet_petSpecies"][x], out text22);
								ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), gameObject15, null, null, false);
								petNames.Add(text22);
							}
							GameObject petSample = GameObject.Create(this.villageSnapshot.listProperties["pet_petSpecies"][x]);
							this.zone.ForeachObjectWithTagOrProperty("Villager", delegate(GameObject o)
							{
								string text25 = HistoricStringExpander.ExpandString("<spice.villages.pet.originStory.!random>", null, null, QudHistoryHelpers.BuildContextFromObjectTextFragments(this.villageSnapshot.listProperties["pet_petSpecies"][x]), null);
								if (int.Parse(this.villageSnapshot.listProperties["pet_number"][x]) == 1)
								{
									text25 = text25.Replace("@them@", petSample.them).Replace("@they@", petSample.it).Replace("@they're@", petSample.itis)
										.Replace("@they've@", petSample.ithas)
										.Replace("@their@", petSample.its)
										.Replace("@Them@", petSample.Them)
										.Replace("@They@", petSample.It)
										.Replace("@They're@", petSample.Itis)
										.Replace("@They've@", petSample.Ithas)
										.Replace("@Their@", petSample.Its)
										.Replace("@has@", "has")
										.Replace("@Name@", petNames[0]);
								}
								else
								{
									text25 = text25.Replace("@them@", "them").Replace("@they@", "they").Replace("@they're@", "they're")
										.Replace("@they've@", "they've")
										.Replace("@their@", "their")
										.Replace("@Them@", "Them")
										.Replace("@They@", "They")
										.Replace("@They're@", "They're")
										.Replace("@They've@", "They've")
										.Replace("@Their@", "Their")
										.Replace("@has@", "have")
										.Replace("@Name@", Grammar.MakeAndList(petNames, true));
								}
								if (!o.HasTagOrProperty("VillagePet"))
								{
									this.AddVillagerConversation(o, o.GetTag("SimpleConversation", "Moon and Sun. Wisdom and will.~May the earth yield for us this season.~Peace, =player.formalAddressTerm=."), "Live and drink.", this.villageSnapshot.listProperties["pet_dialogWhy_Q"][x], text25, true, true);
								}
							});
						}
						catch (Exception ex2)
						{
							MetricsManager.LogException("Failed to generate pet.", ex2, "game_exception");
						}
						x2 = x;
					}
				}
				Z.ForeachObjectWithPart("Brain", delegate(GameObject obj)
				{
					GameObject partyLeader = obj.PartyLeader;
					AllegianceSet allegianceSet = ((partyLeader != null) ? partyLeader.Brain.Allegiance : null);
					int num57;
					if (allegianceSet != null && allegianceSet.TryGetValue(this.villageFaction, out num57))
					{
						obj.Brain.Allegiance.TryAdd(this.villageFaction, num57);
					}
				});
				Z.ForeachObjectWithPart("SecretObject", delegate(GameObject obj)
				{
					obj.RemovePart<SecretObject>();
				});
				base.placeStories();
				Z.ForeachObject(delegate(GameObject o)
				{
					if ((o.GetBlueprint(true).HasTag("Furniture", false) || o.GetBlueprint(true).HasTag("Vessel", false)) && o.Physics != null)
					{
						o.Physics.Owner = this.villageFaction;
					}
					if (this.villageSnapshot.listProperties.ContainsKey("signatureLiquids") && o.GetBlueprint(true).HasTag("Vessel", false) && If.Chance(80, null, null))
					{
						LiquidVolume liquidVolume = o.LiquidVolume;
						if (liquidVolume != null)
						{
							liquidVolume.InitialLiquid = this.villageSnapshot.GetList("signatureLiquids").GetRandomElement<string>(null);
						}
					}
					if (o.HasStringProperty("GivesDynamicQuest") && o.Brain != null)
					{
						o.Brain.Wanders = false;
						o.Brain.WandersRandomly = false;
					}
				});
				if (this.villageSnapshot.GetProperty("abandoned", "unknown") == "true")
				{
					int num54 = 1;
					try
					{
						num54 = Convert.ToInt32(this.villageSnapshot.GetProperty("ruinScale", "unknown"));
						if (num54 < 1)
						{
							num54 = 1;
						}
						if (num54 > 4)
						{
							num54 = 4;
						}
					}
					catch (Exception ex3)
					{
						global::Logger.Exception(ex3);
					}
					if (num54 > 1)
					{
						int num55 = 10;
						if (num54 == 3)
						{
							num55 = 50;
						}
						if (num54 == 4)
						{
							num55 = 100;
						}
						new Ruiner().RuinZone(Z, num55, false, 100000, 500000);
						for (int num56 = 0; num56 < num55 * 2; num56++)
						{
							ZoneBuilderSandbox.PlaceObject(base.getARegionalPlant(), Z, null);
						}
					}
					if (If.Chance(0, null, null))
					{
						foreach (GameObject gameObject16 in this.originalCreatures)
						{
							ZoneBuilderSandbox.PlaceObject(gameObject16, Z, null);
						}
					}
					Z.ReplaceAll("Torchpost", "Chiliad Unlit Torchpost");
					Z.ReplaceAll("Chiliad Torchpost", "Chiliad Unlit Torchpost");
					Z.ReplaceAll("Sconce", "Chiliad Unlit Torchpost");
				}
				this.PopulateCodaApothecary(populationLayout8);
				this.PopulateCodaMerchant(populationLayout9);
				this.PopulateCodaTinker(populationLayout7);
				if (VillageCoda.HasFoundSonnet || VillageCoda.HasReturnedSonnet)
				{
					this.PlaceEtaEarthling(populationLayout6);
				}
				if (VillageCoda.HasStarshiibLocket)
				{
					PopulationLayout randomElement5 = this.buildings.GetRandomElement<PopulationLayout>(null);
					base.PlaceObjectInBuilding(GameObject.Create("Chiliad Hologram Locket"), randomElement5, "InsideCorner", null);
				}
				if (EndGame.IsUltimate)
				{
					base.FabricateUltraHut(rect2D, VillageCoda.IsRuined);
				}
				ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, false, 0.2f, null);
				if (Z.HasBuilder("RiverBuilder"))
				{
					bool flag8 = false;
					List<GameObject> originalLiquids5 = this.originalLiquids;
					string text23;
					if (originalLiquids5 == null)
					{
						text23 = null;
					}
					else
					{
						GameObject randomElement6 = originalLiquids5.GetRandomElement<GameObject>(null);
						text23 = ((randomElement6 != null) ? randomElement6.Blueprint : null);
					}
					new RiverBuilder(flag8, text23 ?? base.getZoneDefaultLiquid(Z), true).BuildZone(Z);
				}
				if (Z.HasBuilder("RoadBuilder"))
				{
					new RoadBuilder(false).BuildZone(Z);
				}
				foreach (GameObject gameObject17 in this.requiredPlacementObjects)
				{
					if (gameObject17.IsCombat)
					{
						base.setVillagerProperties(gameObject17, true);
					}
					ZoneBuilderSandbox.PlaceObject(gameObject17, this.zone, null);
				}
				string text24 = ((this.villageSnapshot.GetProperty("abandoned", "unknown") == "true") ? Stat.Random(5, 25).ToString() : (10 - this.villageTechTier).ToString());
				PowerGrid powerGrid = new PowerGrid();
				powerGrid.PreferWalls = false;
				powerGrid.DamageChance = text24;
				if ((10 + this.villageTechTier * 3).in100())
				{
					powerGrid.MissingConsumers = "1d6";
					powerGrid.MissingProducers = "1d3";
				}
				powerGrid.BuildZone(Z);
				Hydraulics hydraulics = new Hydraulics();
				hydraulics.DamageChance = text24;
				if ((10 + this.villageTechTier * 3).in100())
				{
					hydraulics.MissingConsumers = "1d6";
					hydraulics.MissingProducers = "1d3";
				}
				hydraulics.BuildZone(Z);
				MechanicalPower mechanicalPower = new MechanicalPower();
				mechanicalPower.DamageChance = text24;
				if ((20 - this.villageTechTier).in100())
				{
					mechanicalPower.MissingConsumers = "1d6";
					mechanicalPower.MissingProducers = "1d3";
				}
				mechanicalPower.BuildZone(Z);
				this.PlaceSultanStories();
				Z.SetMusic("Music/Mehmets Book on Strings");
				Z.FireEvent("VillageInit");
				this.cleanup();
				new IsCheckpoint().BuildZoneWithKey(Z, this.villageName);
				Cell cell8 = Z.GetCell(0, 0);
				if (this.SurfaceRevealer && !cell8.HasObject("VillageSurface") && base.villageEntity != null)
				{
					GameObject gameObject18 = GameObject.Create("VillageSurface");
					VillageSurface villageSurface;
					if (gameObject18.TryGetPart<VillageSurface>(out villageSurface))
					{
						Cell worldCell = Z.GetWorldCell();
						villageSurface.VillageName = this.villageName;
						villageSurface.RevealKey = "villageReveal_" + this.villageName;
						villageSurface.RevealLocation = new Vector2i(worldCell.X, worldCell.Y);
						villageSurface.RevealSecret = base.villageEntity.id;
						villageSurface.IsVillageZero = base.villageEntity.GetEntityProperty("isVillageZero", -1L).EqualsNoCase("true");
						if (base.villageEntity.GetEntityProperty("abandoned", -1L).EqualsNoCase("true"))
						{
							villageSurface.RevealString = "You discover the abandoned village of " + this.villageName + ".";
						}
						else
						{
							villageSurface.RevealString = "You discover the village of " + this.villageName + ".";
						}
						cell8.AddObject(gameObject18, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
				return true;
			}
			goto IL_0447;
		}

		// Token: 0x04003465 RID: 13413
		public const string SULTAN_ID = "CodaSultan";

		// Token: 0x04003466 RID: 13414
		public const string VILLAGE_ID = "CodaVillage";

		// Token: 0x04003467 RID: 13415
		public const string END_EVENT_ID = "CodaSultanEndEvent";

		// Token: 0x04003468 RID: 13416
		public const string EVENT_END_COVENANT = "In =event.year=, =subject.name= cleansed the =village.region= of the plagues of the Gyre and, through the tutelage of the tinker monks at Grit Gate, taught =mayor.name= to sow =village.plant= along their fertile tracks.";

		// Token: 0x04003469 RID: 13417
		public const string EVENT_END_RETURN = "In =event.year=, a triad of plagues afflicted the land. Tongues rotted away in the mouths of kith and kin, their legs annealed to iron, and darkness bloomed from the earth. =subject.name= and their warden physickers walked beneath the chrome arches and healed the sick.";

		// Token: 0x0400346A RID: 13418
		public const string EVENT_END_MAROON = "In =event.year=, a triad of plagues afflicted the land. Tongues rotted away in the mouths of kith and kin, their legs annealed to iron, and darkness bloomed from the earth. The warden physickers voiced a prayer to =subject.name= the Above and walked beneath the chrome arches to heal the sick.";

		// Token: 0x0400346B RID: 13419
		public const string EVENT_END_ACCEDE = "In =event.year=, =subject.name=, the Above, forsook the people of Qud in favor of its sludges and microorganisms, and then disappeared. =pronouns.Subjective= =verb:were:afterpronoun= 216 years old.";

		// Token: 0x0400346C RID: 13420
		public const string EVENT_END_LAUNCH = "At twilight in the shadow of the Spindle, the people of =village.name= saw an image on the horizon that looked like a =subject.personTerm= bathed in starfire. It was =subject.name=, and after =pronouns.subjective= came and left =village.name=, the people built a monument to =pronouns.objective=, and thenceforth called =pronouns.objective= =subject.PersonTerm=-in-Starfire.";

		// Token: 0x0400346D RID: 13421
		public const int STATUE_CHUNK_WIDTH = 13;

		// Token: 0x0400346E RID: 13422
		public const int STATUE_CHUNK_HEIGHT = 8;

		// Token: 0x0400346F RID: 13423
		public const int STATUE_CHUNK_PADDING = 1;

		// Token: 0x04003470 RID: 13424
		public const string STATUE_PREFIX = "This shrine depicts a significant event from the life of the ancient =sultan.term= =subject.name=:\n\n";

		// Token: 0x04003471 RID: 13425
		public string dynamicCreatureTableName;

		// Token: 0x04003472 RID: 13426
		public bool SurfaceRevealer = true;

		// Token: 0x04003473 RID: 13427
		private CodaSystem _System;

		// Token: 0x04003474 RID: 13428
		private string[] staticPerVillage = new string[] { "*Storage,*Furniture", "*LiquidStorage,*Furniture", "*Seating,*Furniture", "*Sleep*,*Furniture" };

		// Token: 0x04003475 RID: 13429
		private string[] staticPerBuilding = new string[] { "*LightSource,*Furniture" };

		// Token: 0x04003476 RID: 13430
		private Dictionary<string, string> staticVillageResults = new Dictionary<string, string>();

		// Token: 0x04003477 RID: 13431
		private Dictionary<string, PopulationInfo> ChiliadTables = new Dictionary<string, PopulationInfo>();
	}
}
