using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Genkit;
using HistoryKit;
using XRL;
using XRL.Annals;
using XRL.Collections;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Skills.Cooking;

namespace Qud.API
{
	// Token: 0x02000409 RID: 1033
	[HasWishCommand]
	[HasGameBasedStaticCache]
	public static class JournalAPI
	{
		// Token: 0x060026F6 RID: 9974 RVA: 0x000C9388 File Offset: 0x000C7588
		public static bool GetCategoryMapNoteToggle(string category)
		{
			return XRLCore.Core.Game.GetIntGameState(category + "_mapnotetoggle", 1) == 1;
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000C93A8 File Offset: 0x000C75A8
		public static void SetCategoryMapNoteToggle(string category, bool val)
		{
			XRLCore.Core.Game.SetIntGameState(category + "_mapnotetoggle", val ? 1 : 0);
			Cell currentCell = The.Player.GetCurrentCell();
			if (currentCell != null && currentCell.ParentZone.IsWorldMap())
			{
				currentCell.ParentZone.Activated();
			}
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000C93FC File Offset: 0x000C75FC
		[GameBasedCacheInit]
		public static void Reset()
		{
			JournalAPI.NotesByID.Clear();
			JournalAPI.Accomplishments.Clear();
			JournalAPI.Observations.Clear();
			JournalAPI.MapNotes.Clear();
			JournalAPI.RecipeNotes.Clear();
			JournalAPI.GeneralNotes.Clear();
			JournalAPI.SultanNotes.Clear();
			JournalAPI.VillageNotes.Clear();
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x000C945C File Offset: 0x000C765C
		public static int Count
		{
			get
			{
				return JournalAPI.Accomplishments.Count<JournalAccomplishment>((JournalAccomplishment r) => r.Revealed) + JournalAPI.MapNotes.Count<JournalMapNote>((JournalMapNote r) => r.Revealed) + JournalAPI.Observations.Count<JournalObservation>((JournalObservation r) => r.Revealed) + JournalAPI.RecipeNotes.Count<JournalRecipeNote>((JournalRecipeNote r) => r.Revealed) + JournalAPI.SultanNotes.Count<JournalSultanNote>((JournalSultanNote r) => r.Revealed) + JournalAPI.GeneralNotes.Count<JournalGeneralNote>((JournalGeneralNote r) => r.Revealed);
			}
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000C9564 File Offset: 0x000C7764
		public static List<JournalSultanNote> GetSultanNotes(Func<JournalSultanNote, bool> condition = null)
		{
			List<JournalSultanNote> list = new List<JournalSultanNote>();
			foreach (JournalSultanNote journalSultanNote in JournalAPI.SultanNotes)
			{
				if (condition == null || condition(journalSultanNote))
				{
					list.Add(journalSultanNote);
				}
			}
			return list;
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000C95CC File Offset: 0x000C77CC
		public static void Save(SerializationWriter Writer)
		{
			JournalAPI.WriteList<JournalAccomplishment>(Writer, JournalAPI.Accomplishments);
			JournalAPI.WriteList<JournalObservation>(Writer, JournalAPI.Observations);
			JournalAPI.WriteList<JournalMapNote>(Writer, JournalAPI.MapNotes);
			JournalAPI.WriteList<JournalRecipeNote>(Writer, JournalAPI.RecipeNotes);
			JournalAPI.WriteList<JournalGeneralNote>(Writer, JournalAPI.GeneralNotes);
			JournalAPI.WriteList<JournalSultanNote>(Writer, JournalAPI.SultanNotes);
			JournalAPI.WriteList<JournalVillageNote>(Writer, JournalAPI.VillageNotes);
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000C9628 File Offset: 0x000C7828
		public static void Load(SerializationReader Reader)
		{
			JournalAPI.NotesByID.Clear();
			JournalAPI.ReadList<JournalAccomplishment>(Reader, ref JournalAPI.Accomplishments);
			JournalAPI.ReadList<JournalObservation>(Reader, ref JournalAPI.Observations);
			JournalAPI.ReadList<JournalMapNote>(Reader, ref JournalAPI.MapNotes);
			JournalAPI.ReadList<JournalRecipeNote>(Reader, ref JournalAPI.RecipeNotes);
			JournalAPI.ReadList<JournalGeneralNote>(Reader, ref JournalAPI.GeneralNotes);
			JournalAPI.ReadList<JournalSultanNote>(Reader, ref JournalAPI.SultanNotes);
			JournalAPI.ReadList<JournalVillageNote>(Reader, ref JournalAPI.VillageNotes);
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000C968C File Offset: 0x000C788C
		public static void WriteList<T>(SerializationWriter Writer, List<T> Value) where T : IBaseJournalEntry
		{
			if (Value == null)
			{
				Writer.WriteOptimized(0);
				return;
			}
			int count = Value.Count;
			Writer.WriteOptimized(count + 1);
			for (int i = 0; i < count; i++)
			{
				Writer.Write(Value[i]);
			}
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000C96D4 File Offset: 0x000C78D4
		public static void ReadList<T>(SerializationReader Reader, ref List<T> Value) where T : IBaseJournalEntry
		{
			Value.Clear();
			int num = Reader.ReadOptimizedInt32() - 1;
			if (num == -1)
			{
				return;
			}
			for (int i = 0; i < num; i++)
			{
				T t = (T)((object)Reader.ReadComposite());
				Value.Add(t);
				JournalAPI.AddedNote(t);
			}
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000C9721 File Offset: 0x000C7921
		public static void AddedNote(IBaseJournalEntry Entry)
		{
			if (!Entry.ID.IsNullOrEmpty() && !JournalAPI.NotesByID.TryAdd(Entry.ID, Entry))
			{
				MetricsManager.LogError("Duplicate entry ID: '" + Entry.ID + "'");
			}
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000C9760 File Offset: 0x000C7960
		public static bool TryRevealNote(string ID, string LearnedFrom = null, bool Silent = false)
		{
			IBaseJournalEntry baseJournalEntry;
			if (ID != null && JournalAPI.NotesByID.TryGetValue(ID, out baseJournalEntry))
			{
				baseJournalEntry.Reveal(LearnedFrom, Silent);
				return baseJournalEntry.Revealed;
			}
			return false;
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000C9790 File Offset: 0x000C7990
		public static bool HasNote(string ID)
		{
			IBaseJournalEntry baseJournalEntry;
			return JournalAPI.NotesByID.TryGetValue(ID, out baseJournalEntry) && baseJournalEntry.Revealed;
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000C97B4 File Offset: 0x000C79B4
		public static bool HasUnrevealedNote(string ID)
		{
			IBaseJournalEntry baseJournalEntry;
			return JournalAPI.NotesByID.TryGetValue(ID, out baseJournalEntry) && !baseJournalEntry.Revealed;
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000C97DB File Offset: 0x000C79DB
		public static IEnumerable<IBaseJournalEntry> GetAllNotes()
		{
			foreach (JournalAccomplishment journalAccomplishment in JournalAPI.Accomplishments)
			{
				yield return journalAccomplishment;
			}
			List<JournalAccomplishment>.Enumerator enumerator = default(List<JournalAccomplishment>.Enumerator);
			foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
			{
				yield return journalMapNote;
			}
			List<JournalMapNote>.Enumerator enumerator2 = default(List<JournalMapNote>.Enumerator);
			foreach (JournalObservation journalObservation in JournalAPI.Observations)
			{
				yield return journalObservation;
			}
			List<JournalObservation>.Enumerator enumerator3 = default(List<JournalObservation>.Enumerator);
			foreach (JournalRecipeNote journalRecipeNote in JournalAPI.RecipeNotes)
			{
				yield return journalRecipeNote;
			}
			List<JournalRecipeNote>.Enumerator enumerator4 = default(List<JournalRecipeNote>.Enumerator);
			foreach (JournalSultanNote journalSultanNote in JournalAPI.SultanNotes)
			{
				yield return journalSultanNote;
			}
			List<JournalSultanNote>.Enumerator enumerator5 = default(List<JournalSultanNote>.Enumerator);
			foreach (JournalGeneralNote journalGeneralNote in JournalAPI.GeneralNotes)
			{
				yield return journalGeneralNote;
			}
			List<JournalGeneralNote>.Enumerator enumerator6 = default(List<JournalGeneralNote>.Enumerator);
			foreach (JournalVillageNote journalVillageNote in JournalAPI.VillageNotes)
			{
				yield return journalVillageNote;
			}
			List<JournalVillageNote>.Enumerator enumerator7 = default(List<JournalVillageNote>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000C97E4 File Offset: 0x000C79E4
		public static IEnumerable<IBaseJournalEntry> GetKnownNotes(Predicate<IBaseJournalEntry> filter = null)
		{
			if (filter == null)
			{
				filter = (IBaseJournalEntry n) => true;
			}
			foreach (IBaseJournalEntry baseJournalEntry in JournalAPI.GetAllNotes())
			{
				if (baseJournalEntry.Revealed && filter(baseJournalEntry))
				{
					yield return baseJournalEntry;
				}
			}
			IEnumerator<IBaseJournalEntry> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000C97F4 File Offset: 0x000C79F4
		public static List<JournalSultanNote> GetKnownSultanNotes()
		{
			List<JournalSultanNote> list = new List<JournalSultanNote>();
			foreach (JournalSultanNote journalSultanNote in JournalAPI.SultanNotes)
			{
				if (journalSultanNote.Revealed)
				{
					list.Add(journalSultanNote);
				}
			}
			return list;
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000C9858 File Offset: 0x000C7A58
		public static List<JournalSultanNote> GetKnownNotesForSultan(string sultan, Predicate<JournalSultanNote> filter = null)
		{
			List<JournalSultanNote> list = new List<JournalSultanNote>();
			foreach (JournalSultanNote journalSultanNote in JournalAPI.SultanNotes)
			{
				if (journalSultanNote.SultanID == sultan && journalSultanNote.Revealed && (filter == null || filter(journalSultanNote)))
				{
					list.Add(journalSultanNote);
				}
			}
			return list;
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000C98D4 File Offset: 0x000C7AD4
		public static List<JournalSultanNote> GetKnownNotesForResheph(Predicate<JournalSultanNote> Filter = null)
		{
			return JournalAPI.GetKnownNotesForSultan(HistoryAPI.GetResheph().id, Filter);
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000C98E8 File Offset: 0x000C7AE8
		public static List<JournalSultanNote> GetNotesForSultan(string sultan)
		{
			List<JournalSultanNote> list = new List<JournalSultanNote>();
			foreach (JournalSultanNote journalSultanNote in JournalAPI.SultanNotes)
			{
				if (journalSultanNote.SultanID == sultan)
				{
					list.Add(journalSultanNote);
				}
			}
			return list;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000C9950 File Offset: 0x000C7B50
		public static List<JournalSultanNote> GetNotesForResheph()
		{
			return JournalAPI.GetNotesForSultan(HistoryAPI.GetResheph().id);
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000C9964 File Offset: 0x000C7B64
		public static JournalSultanNote RevealSultanEventBySecretID(string id, string LearnedFrom = null)
		{
			foreach (JournalSultanNote journalSultanNote in JournalAPI.SultanNotes)
			{
				if (journalSultanNote.ID == id)
				{
					journalSultanNote.Reveal(LearnedFrom, false);
					return journalSultanNote;
				}
			}
			return null;
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000C99CC File Offset: 0x000C7BCC
		public static JournalSultanNote RevealSultanEvent(long id, string LearnedFrom = null)
		{
			foreach (JournalSultanNote journalSultanNote in JournalAPI.SultanNotes)
			{
				if (journalSultanNote.EventID == id)
				{
					journalSultanNote.Reveal(LearnedFrom, false);
					return journalSultanNote;
				}
			}
			return null;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000C9A30 File Offset: 0x000C7C30
		public static bool KnowsSultanEvent(long id)
		{
			return JournalAPI.SultanNotes.Any<JournalSultanNote>((JournalSultanNote n) => n.EventID == id && n.Revealed);
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x000C9A60 File Offset: 0x000C7C60
		public static bool HasUnrevealedSultanEvent(long id)
		{
			int i = 0;
			int count = JournalAPI.SultanNotes.Count;
			while (i < count)
			{
				if (JournalAPI.SultanNotes[i].EventID == id)
				{
					return !JournalAPI.SultanNotes[i].Revealed;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000C9AAC File Offset: 0x000C7CAC
		public static bool HasUnrevealedSultanEvent(string id)
		{
			int i = 0;
			int count = JournalAPI.SultanNotes.Count;
			while (i < count)
			{
				if (JournalAPI.SultanNotes[i].ID == id)
				{
					return !JournalAPI.SultanNotes[i].Revealed;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x000C9B00 File Offset: 0x000C7D00
		public static List<JournalVillageNote> GetKnownNotesForVillage(string village)
		{
			List<JournalVillageNote> list = new List<JournalVillageNote>();
			foreach (JournalVillageNote journalVillageNote in JournalAPI.VillageNotes)
			{
				if (journalVillageNote.VillageID == village && journalVillageNote.Revealed)
				{
					list.Add(journalVillageNote);
				}
			}
			return list;
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000C9B70 File Offset: 0x000C7D70
		public static List<JournalVillageNote> GetNotesForVillage(string village)
		{
			List<JournalVillageNote> list = new List<JournalVillageNote>();
			foreach (JournalVillageNote journalVillageNote in JournalAPI.VillageNotes)
			{
				if (journalVillageNote.VillageID == village)
				{
					list.Add(journalVillageNote);
				}
			}
			return list;
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x000C9BD8 File Offset: 0x000C7DD8
		public static bool IsMapOrVillageNoteRevealed(string secretID)
		{
			JournalMapNote journalMapNote = JournalAPI.MapNotes.Find((JournalMapNote m) => m.ID == secretID);
			if (journalMapNote != null && journalMapNote.Revealed)
			{
				return true;
			}
			JournalVillageNote journalVillageNote = JournalAPI.VillageNotes.Find((JournalVillageNote m) => m.ID == secretID);
			return journalVillageNote != null && journalVillageNote.Revealed;
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000C9C3C File Offset: 0x000C7E3C
		public static bool RevealVillageNote(string secretID, string LearnedFrom = null)
		{
			JournalVillageNote journalVillageNote = JournalAPI.VillageNotes.Find((JournalVillageNote n) => secretID == n.ID);
			if (journalVillageNote != null)
			{
				journalVillageNote.Reveal(LearnedFrom, false);
			}
			return journalVillageNote != null && journalVillageNote.Revealed;
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000C9C88 File Offset: 0x000C7E88
		public static void InitializeGossip()
		{
			int num = 60;
			for (int i = 0; i < num; i++)
			{
				string text = Guid.NewGuid().ToString();
				if (Stat.Random(1, 100) <= 75)
				{
					Faction randomFaction = Factions.GetRandomFaction();
					JournalAPI.AddObservation(Grammar.InitCap(Gossip.GenerateGossip_OneFaction(randomFaction.Name)), text, "Gossip", text, new string[]
					{
						"gossip:" + randomFaction.Name,
						"gossip"
					}, false, -1L, null, false, true);
				}
				else
				{
					Faction randomFaction2 = Factions.GetRandomFaction();
					Faction randomFaction3;
					do
					{
						randomFaction3 = Factions.GetRandomFaction();
					}
					while (randomFaction3 == randomFaction2);
					JournalAPI.AddObservation(Grammar.InitCap(Gossip.GenerateGossip_TwoFactions(randomFaction2.Name, randomFaction3.Name)), text, "Gossip", text, new string[]
					{
						"gossip:" + randomFaction2.Name,
						"gossip:" + randomFaction3.Name,
						"gossip"
					}, false, -1L, null, false, true);
				}
			}
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x000C9D90 File Offset: 0x000C7F90
		public static void InitializeObservations()
		{
			JournalAPI.AddObservation("Qud was once called Salum.", "QudSalum", "general", null, new string[] { "old" }, false, -1L, null, true, true);
			JournalAPI.AddObservation("The shomer Rainwater claims that Brightsheol is the dream of a seraph who lives atop the Spindle.", "BrightsheolDream", "Gossip", null, new string[] { "gossip", "old" }, false, -1L, null, false, true);
			JournalAPI.AddObservation("The Palladium Reef was once called Maqqom Yd, the Place outside Itself.", "MaqqomYd", "general", null, new string[] { "old" }, false, -1L, null, false, true);
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000C9E20 File Offset: 0x000C8020
		public static void AddVillageGospels(HistoricEntity Village)
		{
			JournalAPI.AddVillageGospels(Village.GetCurrentSnapshot());
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000C9E30 File Offset: 0x000C8030
		public static void AddVillageGospels(HistoricEntitySnapshot Snapshot)
		{
			foreach (string text in Snapshot.GetList("Gospels"))
			{
				string text2;
				string text3;
				text.Split('|', out text2, out text3);
				long? num = null;
				long num2;
				if (!text3.IsNullOrEmpty() && long.TryParse(text3, out num2))
				{
					num = new long?(num2);
				}
				JournalAPI.AddVillageNote(text2, Snapshot.entity.id, num, "village");
			}
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000C9EC8 File Offset: 0x000C80C8
		public static void InitializeVillageEntries()
		{
			foreach (HistoricEntity historicEntity in HistoryAPI.GetVillages())
			{
				JournalAPI.AddVillageGospels(historicEntity);
			}
			JournalAPI.AddVillageNote("JoppaSecret1", "In the mystic air of Jeweled Dusk, a circle of farmers clasped hands and sheathed a vinereaper in the moistened soil. From then on, the villagers of Joppa were known as the people of the vine.", "Joppa", null, "village");
			JournalAPI.AddVillageNote("JoppaSecret2", "Since the first festival of Ut yara Ux, the villagers of Joppa have feasted on warm apple matz.", "Joppa", null, "village");
			JournalAPI.AddVillageNote("KyakukyaSecret1", "Under the Beetle Moon, folks settled here to gather goods of passage for Ape Saad Oboroqoru, and so Kyakukya was founded.", "Kyakukya", null, "village");
			JournalAPI.AddVillageNote("KyakukyaSecret2", "In 979, Nuntu, the legendary albino ape, grew tired of bludgeoning things to death and journeyed to a root-strangled place on the River Svy. There he came upon Kyakukya and its inhabitants. The villagers of Kyakukya asked Nuntu to employ his magisterial skills and lead the village.", "Kyakukya", null, "village");
			JournalAPI.AddVillageNote("YdFreeholdSecret1", "Centuries after the height of the Gyre, the svardym Goek, Mak, and Geeub met the galgal Many Eyes while languishing inside of a reef sponge. Together they founded the Yd Freehold to live out the rest of their lives.", "The Yd Freehold", null, "village");
			JournalAPI.AddVillageNote("YdFreeholdSecret2", "In 901, the villagers of the Yd Freehold and its founders abolished all forms of hierarchy from their settlement. The two pillars of civic life in the Freehold thus became anarchy and authenticity.", "The Yd Freehold", null, "village");
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000C9FEC File Offset: 0x000C81EC
		public static JournalSultanNote AddSultanNote(string Text, string SultanID, long? EventID = null, object Attributes = null)
		{
			return JournalAPI.AddSultanNote(Guid.NewGuid().ToString(), Text, SultanID, EventID, Attributes);
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000CA018 File Offset: 0x000C8218
		public static JournalSultanNote AddSultanNote(string ID, string Text, string SultanID, long? EventID = null, object Attributes = null)
		{
			JournalSultanNote journalSultanNote = new JournalSultanNote();
			journalSultanNote.ID = ID;
			journalSultanNote.Text = Text;
			journalSultanNote.SultanID = SultanID;
			if (EventID != null)
			{
				journalSultanNote.EventID = EventID.Value;
			}
			string text = Attributes as string;
			if (text != null)
			{
				journalSultanNote.Attributes.Add(text);
			}
			else
			{
				IEnumerable<string> enumerable = Attributes as IEnumerable<string>;
				if (enumerable != null)
				{
					journalSultanNote.Attributes.AddRange(enumerable);
				}
			}
			JournalAPI.SultanNotes.Add(journalSultanNote);
			JournalAPI.AddedNote(journalSultanNote);
			return journalSultanNote;
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000CA098 File Offset: 0x000C8298
		public static void InitializeSultanEntries()
		{
			List<string> list = new List<string>();
			foreach (HistoricEntity historicEntity in HistoryAPI.GetSultans())
			{
				List<string> sultanLikedFactions = HistoryAPI.GetSultanLikedFactions(historicEntity);
				List<string> sultanHatedFactions = HistoryAPI.GetSultanHatedFactions(historicEntity);
				foreach (HistoricEvent historicEvent in HistoryAPI.GetSultanEventsWithGospels(historicEntity.id))
				{
					list.Clear();
					foreach (string text in sultanLikedFactions)
					{
						list.Add("include:" + text);
					}
					foreach (string text2 in sultanHatedFactions)
					{
						list.Add("include:" + text2);
					}
					list.Add("sultan");
					if (historicEvent.GetEventProperty("rebekah", null) == "true")
					{
						list.Add("rebekah");
					}
					if (historicEvent.GetEventProperty("rebekahWasHealer", null) == "true")
					{
						list.Add("rebekahWasHealer");
					}
					if (historicEvent.GetEventProperty("gyreplagues", null) == "true")
					{
						list.Add("gyreplagues");
					}
					JournalAPI.AddSultanNote(historicEvent.GetEventProperty("gospel", null), historicEntity.id, new long?(historicEvent.id), list);
				}
				foreach (HistoricEvent historicEvent2 in HistoryAPI.GetSultanEventsWithTombInscriptions(historicEntity.id))
				{
					list.Clear();
					foreach (string text3 in sultanLikedFactions)
					{
						list.Add("include:" + text3);
					}
					foreach (string text4 in sultanHatedFactions)
					{
						list.Add("include:" + text4);
					}
					list.Add("sultanTombPropaganda");
					list.Add("onlySellIfTargetedAndInterested");
					JournalAPI.AddSultanNote(historicEvent2.GetEventProperty("tombInscription", null), historicEntity.id, new long?(historicEvent2.id), list);
				}
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000CA3F4 File Offset: 0x000C85F4
		public static void DeleteAccomplishment(JournalAccomplishment accomplishmentToDelete)
		{
			JournalAPI.Accomplishments.Remove(accomplishmentToDelete);
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x000CA402 File Offset: 0x000C8602
		public static void DeleteGeneralNote(JournalGeneralNote noteToDelete)
		{
			JournalAPI.GeneralNotes.Remove(noteToDelete);
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x000CA410 File Offset: 0x000C8610
		public static void AddAccomplishment(string text, string muralText = null, string gospelText = null, string aggregateWith = null, string category = "general", MuralCategory muralCategory = MuralCategory.Generic, MuralWeight muralWeight = MuralWeight.Medium, string secretId = null, long time = -1L, bool revealed = true)
		{
			if (text.IsNullOrEmpty())
			{
				MetricsManager.LogError("Attempting to add accomplishment without text.");
				return;
			}
			HistoricEntitySnapshot historicEntitySnapshot = QudHistoryFactory.RequirePlayerEntitySnapshot();
			JournalAccomplishment journalAccomplishment = new JournalAccomplishment();
			journalAccomplishment.Category = category;
			journalAccomplishment.MuralCategory = muralCategory;
			journalAccomplishment.MuralWeight = muralWeight;
			journalAccomplishment.MuralText = HistoricStringExpander.ExpandString(muralText ?? "", null).StartReplace().ForceThirdPerson()
				.ToString();
			journalAccomplishment.GospelText = HistoricStringExpander.ExpandString(gospelText ?? "", historicEntitySnapshot, historicEntitySnapshot.entity.history, null, null).StartReplace().ForceThirdPerson()
				.ToString();
			journalAccomplishment.AggregateWith = aggregateWith;
			if (muralText == null && muralWeight != MuralWeight.Nil)
			{
				string text2 = text.Replace("You ", The.Game.PlayerName + " ").Replace("you ", The.Game.PlayerName + " ").Replace("Your ", Grammar.MakePossessive(The.Game.PlayerName) + " ")
					.Replace("your ", Grammar.MakePossessive(The.Game.PlayerName) + " ");
				journalAccomplishment.MuralText = Grammar.InitCap(text2);
				journalAccomplishment.MuralCategory = MuralCategory.DoesSomethingRad;
				journalAccomplishment.MuralWeight = MuralWeight.Medium;
			}
			if (The.Player != null)
			{
				Lovesick lovesick;
				if (The.Player.TryGetEffect<Lovesick>(out lovesick))
				{
					text = HistoricStringExpander.ExpandString("<spice.lovesick.!random>", null, null, null, null).Replace("*var*", text).Replace("*varLower*", Grammar.InitLower(text))
						.Replace("*sob*", Grammar.Stutterize(text, "{{w|*sob*}}"))
						.Replace("*varReplacePeriod*", text.Replace(".", ""))
						.Replace("*obj*", lovesick.Beauty.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, true, null, false))
						.Replace("*objShort*", lovesick.Beauty.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, false, true, null, false));
				}
				if (The.Player.IsAflame())
				{
					text = "While on fire, " + (text.StartsWith(The.Game.PlayerName) ? text : Grammar.InitLower(text));
				}
				else if (The.Player.IsFrozen())
				{
					text = "While frozen solid, " + (text.StartsWith(The.Game.PlayerName) ? text : Grammar.InitLower(text));
				}
				journalAccomplishment.UpdateScreenshot();
			}
			journalAccomplishment.Text = text;
			journalAccomplishment.ID = secretId;
			journalAccomplishment.Tradable = false;
			if (time == -1L)
			{
				journalAccomplishment.Time = XRLCore.Core.Game.TimeTicks;
			}
			else
			{
				journalAccomplishment.Time = time;
			}
			JournalAPI.Accomplishments.Add(journalAccomplishment);
			JournalAPI.AddedNote(journalAccomplishment);
			if (JournalAPI.sorting)
			{
				JournalAPI.Accomplishments.Sort((JournalAccomplishment a, JournalAccomplishment b) => a.Time.CompareTo(b.Time));
			}
			try
			{
				Physics physics = The.Player.Physics;
				if (physics != null && physics.CurrentCell != null)
				{
					physics.CurrentCell.ParentZone.FireEvent(Event.New("AccomplishmentAdded", "Text", text, "Category", category, "SecretId", secretId));
				}
			}
			catch (Exception ex)
			{
				Logger.Exception(ex);
			}
			if (revealed)
			{
				journalAccomplishment.Reveal(null, false);
			}
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x000CA77C File Offset: 0x000C897C
		public static void Init()
		{
			JournalAPI._mapNotesByZone = null;
			JournalAPI._mapNoteCategories = null;
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x000CA78C File Offset: 0x000C898C
		public static void AddObservation(string text, string id, string category = "general", string secretId = null, string[] attributes = null, bool revealed = false, long time = -1L, string additionalRevealText = null, bool initCapAsFragment = false, bool Tradable = true)
		{
			if (JournalAPI.GetObservation(id) != null)
			{
				return;
			}
			JournalObservation journalObservation = new JournalObservation();
			journalObservation.ID = id.Coalesce(secretId);
			journalObservation.Text = text;
			if (revealed)
			{
				journalObservation.Reveal(null, false);
			}
			else
			{
				journalObservation.Forget(false);
			}
			journalObservation.RevealText = additionalRevealText;
			journalObservation.Rumor = initCapAsFragment;
			journalObservation.Tradable = Tradable;
			if (attributes != null)
			{
				journalObservation.Attributes.AddRange(attributes);
			}
			if (time == -1L)
			{
				journalObservation.Time = XRLCore.Core.Game.TimeTicks;
			}
			else
			{
				journalObservation.Time = time;
			}
			journalObservation.Category = category;
			JournalAPI.Observations.Add(journalObservation);
			JournalAPI.AddedNote(journalObservation);
			if (JournalAPI.sorting)
			{
				JournalAPI.Observations.Sort((JournalObservation a, JournalObservation b) => a.Time.CompareTo(b.Time));
			}
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000CA867 File Offset: 0x000C8A67
		public static void RevealObservation(JournalObservation note)
		{
			note.Reveal(null, false);
			note.Updated();
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000CA878 File Offset: 0x000C8A78
		public static bool IsObservationRevealed(string secretID)
		{
			JournalObservation journalObservation = JournalAPI.Observations.Find((JournalObservation m) => m.ID == secretID);
			return journalObservation != null && journalObservation.Revealed;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x000CA8B8 File Offset: 0x000C8AB8
		public static void RevealObservation(string id, bool onlyIfNotRevealed = false)
		{
			foreach (JournalObservation journalObservation in JournalAPI.Observations)
			{
				if (journalObservation.ID == id && (!onlyIfNotRevealed || !journalObservation.Revealed))
				{
					JournalAPI.RevealObservation(journalObservation);
				}
			}
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x000CA924 File Offset: 0x000C8B24
		public static JournalObservation GetObservation(string id)
		{
			foreach (JournalObservation journalObservation in JournalAPI.Observations)
			{
				if (journalObservation.ID == id)
				{
					return journalObservation;
				}
			}
			return null;
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000CA984 File Offset: 0x000C8B84
		public static List<JournalObservation> GetObservations(Func<JournalObservation, bool> compare = null)
		{
			List<JournalObservation> list = new List<JournalObservation>();
			foreach (JournalObservation journalObservation in JournalAPI.Observations)
			{
				if (compare == null || compare(journalObservation))
				{
					list.Add(journalObservation);
				}
			}
			return list;
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x000CA9EC File Offset: 0x000C8BEC
		public static List<JournalMapNote> GetRevealedMapNotesForWorldMapCell(int x, int y)
		{
			List<JournalMapNote> list = new List<JournalMapNote>();
			foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
			{
				if (journalMapNote.Revealed && journalMapNote.ParasangX == x && journalMapNote.ParasangY == y)
				{
					list.Add(journalMapNote);
				}
			}
			return list;
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000CAA60 File Offset: 0x000C8C60
		public static List<JournalMapNote> GetMapNotesForWorldMapCell(int x, int y)
		{
			List<JournalMapNote> list = new List<JournalMapNote>();
			foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
			{
				if (journalMapNote.ParasangX == x && journalMapNote.ParasangY == y)
				{
					list.Add(journalMapNote);
				}
			}
			return list;
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000CAACC File Offset: 0x000C8CCC
		public static List<string> GetMapNoteCategories()
		{
			if (JournalAPI._mapNoteCategories != null)
			{
				return JournalAPI._mapNoteCategories;
			}
			JournalAPI._mapNoteCategories = new List<string>();
			foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
			{
				if (journalMapNote.Revealed && !JournalAPI._mapNoteCategories.Contains(journalMapNote.Category))
				{
					JournalAPI._mapNoteCategories.Add(journalMapNote.Category);
				}
			}
			JournalAPI._mapNoteCategories.Sort();
			return JournalAPI._mapNoteCategories;
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x000CAB68 File Offset: 0x000C8D68
		public static List<JournalMapNote> GetMapNotesForZone(string zoneId)
		{
			List<JournalMapNote> list;
			if (JournalAPI.mapNotesByZone.TryGetValue(zoneId, out list))
			{
				return list;
			}
			return new List<JournalMapNote>();
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000CAB8C File Offset: 0x000C8D8C
		public static JournalMapNote GetMapNote(string secretId)
		{
			foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
			{
				if (journalMapNote.ID == secretId)
				{
					return journalMapNote;
				}
			}
			return null;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x000CABEC File Offset: 0x000C8DEC
		public static bool HasMapNote(string WorldID, int WX = -1, int WY = -1, int X = -1, int Y = -1, int Z = -1)
		{
			foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
			{
				if ((WX == -1 || journalMapNote.ParasangX == WX) && (WY == -1 || journalMapNote.ParasangY == WY) && (X == -1 || journalMapNote.ZoneX == X) && (Y == -1 || journalMapNote.ZoneY == Y) && (Z == -1 || journalMapNote.ZoneZ == Z) && journalMapNote.WorldID == WorldID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000CAC94 File Offset: 0x000C8E94
		public static List<JournalMapNote> GetMapNotes(Func<JournalMapNote, bool> compare = null)
		{
			List<JournalMapNote> list = new List<JournalMapNote>();
			foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
			{
				if (compare == null || compare(journalMapNote))
				{
					list.Add(journalMapNote);
				}
			}
			return list;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000CACFC File Offset: 0x000C8EFC
		public static List<JournalMapNote> GetMapNotesWithAllAttributes(string attributes)
		{
			return JournalAPI.GetMapNotes(delegate(JournalMapNote note)
			{
				bool flag = true;
				foreach (string text in attributes.Split(',', StringSplitOptions.None))
				{
					if (!note.Has(text))
					{
						flag = false;
						break;
					}
				}
				return flag;
			});
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000CAD1C File Offset: 0x000C8F1C
		public static List<JournalMapNote> GetMapNotesInCardinalDirections(string zoneid)
		{
			string world;
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			ZoneID.Parse(zoneid, out world, out num, out num2, out num3, out num4, out num5);
			int rx = num * 3 + num3;
			int ry = num2 * 3 + num4;
			Location2D location = Location2D.Get(rx, ry);
			List<JournalMapNote> mapNotes = JournalAPI.GetMapNotes((JournalMapNote note) => !string.Equals(note.Category, "Miscellaneous") && !(note.WorldID != world) && (rx != note.ResolvedX || ry != note.ResolvedY) && (rx == note.ResolvedX || ry == note.ResolvedY));
			if (mapNotes.Count == 0)
			{
				return null;
			}
			mapNotes.Sort((JournalMapNote a, JournalMapNote b) => location.Distance(a.ResolvedLocation).CompareTo(location.Distance(b.ResolvedLocation)));
			return mapNotes;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000CADA8 File Offset: 0x000C8FA8
		public static List<JournalMapNote> GetUnrevealedMapNotesWithinWorldRadiusN(string zoneid, int min, int max)
		{
			string world;
			int px;
			int py;
			int z;
			int num;
			int num2;
			ZoneID.Parse(zoneid, out world, out px, out py, out num, out num2, out z);
			return JournalAPI.GetMapNotes((JournalMapNote note) => !note.Revealed && !(note.WorldID != world) && Math.Abs(px - note.ParasangX) <= max && Math.Abs(py - note.ParasangY) <= max && Math.Abs(z - note.ZoneZ) <= max && Math.Abs(px - note.ParasangX) >= min && Math.Abs(py - note.ParasangY) >= min && Math.Abs(z - note.ZoneZ) >= min);
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000CAE00 File Offset: 0x000C9000
		public static List<JournalMapNote> GetUnrevealedMapNotesWithinZoneRadiusN(string zoneid, int min, int max, Predicate<Location2D> isValid = null)
		{
			string world;
			int z;
			int num;
			int num2;
			int num3;
			int num4;
			ZoneID.Parse(zoneid, out world, out num, out num2, out num3, out num4, out z);
			int rx = num * 3 + num3;
			int ry = num2 * 3 + num4;
			return JournalAPI.GetMapNotes((JournalMapNote note) => !note.Revealed && !(note.WorldID != world) && !(note.ZoneID == zoneid) && Math.Abs(rx - note.ResolvedX) <= max && Math.Abs(ry - note.ResolvedY) <= max && Math.Abs(z - note.ZoneZ) <= 0 && Math.Abs(rx - note.ResolvedX) >= min && Math.Abs(ry - note.ResolvedY) >= min && Math.Abs(z - note.ZoneZ) <= 0 && (isValid == null || isValid(note.ResolvedLocation)));
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x000CAE77 File Offset: 0x000C9077
		public static List<JournalMapNote> GetUnrevealedMapNotesWithinWorldRadiusN(Zone z, int min, int max, Predicate<Location2D> isValid = null)
		{
			return JournalAPI.GetMapNotes((JournalMapNote note) => !note.Revealed && Math.Abs(z.wX - note.ParasangX) <= max && Math.Abs(z.wY - note.ParasangY) <= max && Math.Abs(z.Z - note.ZoneZ) <= max && Math.Abs(z.wX - note.ParasangX) >= min && Math.Abs(z.wY - note.ParasangY) >= min && Math.Abs(z.Z - note.ZoneZ) >= min && (isValid == null || isValid(note.ResolvedLocation)));
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000CAEAA File Offset: 0x000C90AA
		public static List<JournalMapNote> GetUnrevealedMapNotesWithinZoneRadiusN(Zone z, int min, int max, Predicate<Location2D> isValid = null)
		{
			return JournalAPI.GetMapNotes((JournalMapNote note) => !note.Revealed && Math.Abs(z.ResolvedX - note.ResolvedX) <= max && Math.Abs(z.ResolvedY - note.ResolvedY) <= max && Math.Abs(z.ResolvedX - note.ResolvedX) >= min && Math.Abs(z.ResolvedY - note.ResolvedY) >= min && Math.Abs(z.Z - note.ZoneZ) <= 0 && (isValid == null || isValid(note.ResolvedLocation)));
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000CAEDD File Offset: 0x000C90DD
		public static List<JournalMapNote> GetMapNotesWithinRadiusN(Zone z, int radius)
		{
			return JournalAPI.GetMapNotes((JournalMapNote note) => !string.Equals(note.Category, "Miscellaneous") && Math.Abs(z.wX - note.ParasangX) <= radius && Math.Abs(z.wY - note.ParasangY) <= radius && Math.Abs(z.Z - note.ZoneZ) <= radius);
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000CAF02 File Offset: 0x000C9102
		public static JournalMapNote GetLandmarkNearestPlayer()
		{
			return JournalAPI.GetLandmarkNearest(The.Player.CurrentZone);
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x000CAF14 File Offset: 0x000C9114
		public static JournalMapNote GetLandmarkNearest(Zone Zone)
		{
			JournalMapNote journalMapNote = JournalAPI.MapNotes.GetRandomElement<JournalMapNote>(null);
			int num = int.MaxValue;
			foreach (JournalMapNote journalMapNote2 in JournalAPI.MapNotes)
			{
				if (journalMapNote2.Revealed && !(journalMapNote2.Category == "Miscellaneous"))
				{
					int num2 = Math.Abs(Zone.wX - journalMapNote2.ParasangX) * 3 + Math.Abs(Zone.X - journalMapNote2.ZoneX);
					int num3 = Math.Abs(Zone.wY - journalMapNote2.ParasangY) * 3 + Math.Abs(Zone.Y - journalMapNote2.ZoneY);
					int num4 = Math.Abs(Zone.Z - journalMapNote2.ZoneZ);
					int num5 = num2 + num3 + num4;
					if (num5 < num)
					{
						num = num5;
						journalMapNote = journalMapNote2;
					}
				}
			}
			return journalMapNote;
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000CB00C File Offset: 0x000C920C
		public static List<JournalRecipeNote> GetRecipes(Func<JournalRecipeNote, bool> compare = null)
		{
			List<JournalRecipeNote> list = new List<JournalRecipeNote>();
			foreach (JournalRecipeNote journalRecipeNote in JournalAPI.RecipeNotes)
			{
				if (compare == null || compare(journalRecipeNote))
				{
					list.Add(journalRecipeNote);
				}
			}
			return list;
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000CB074 File Offset: 0x000C9274
		public static bool HasObservation(string id)
		{
			foreach (JournalObservation journalObservation in JournalAPI.Observations)
			{
				if (journalObservation.ID == id && journalObservation.Revealed)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000CB0DC File Offset: 0x000C92DC
		public static bool HasObservationWithTag(string Tag)
		{
			return JournalAPI.GetObservations((JournalObservation note) => note.Revealed && note.Has(Tag)).Count > 0;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000CB102 File Offset: 0x000C9302
		public static bool HasSultanNoteWithTag(string Tag)
		{
			return JournalAPI.GetSultanNotes((JournalSultanNote note) => note.Revealed && note.Has(Tag)).Count > 0;
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000CB128 File Offset: 0x000C9328
		public static JournalSultanNote GetFirstSultanNoteWithTag(string Tag)
		{
			foreach (JournalSultanNote journalSultanNote in JournalAPI.SultanNotes)
			{
				if (journalSultanNote.Has(Tag))
				{
					return journalSultanNote;
				}
			}
			return null;
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000CB184 File Offset: 0x000C9384
		public static JournalVillageNote AddVillageNote(string Text, string VillageID, long? EventID = null, object Attributes = null)
		{
			return JournalAPI.AddVillageNote(Guid.NewGuid().ToString(), Text, VillageID, EventID, Attributes);
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000CB1B0 File Offset: 0x000C93B0
		public static JournalVillageNote AddVillageNote(string ID, string Text, string VillageID, long? EventID = null, object Attributes = null)
		{
			JournalVillageNote journalVillageNote = new JournalVillageNote();
			journalVillageNote.ID = ID;
			journalVillageNote.Text = Text;
			journalVillageNote.VillageID = VillageID;
			if (EventID != null)
			{
				journalVillageNote.EventID = EventID.Value;
			}
			string text = Attributes as string;
			if (text != null)
			{
				journalVillageNote.Attributes.Add(text);
			}
			else
			{
				IEnumerable<string> enumerable = Attributes as IEnumerable<string>;
				if (enumerable != null)
				{
					journalVillageNote.Attributes.AddRange(enumerable);
				}
			}
			JournalAPI.VillageNotes.Add(journalVillageNote);
			JournalAPI.AddedNote(journalVillageNote);
			return journalVillageNote;
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000CB230 File Offset: 0x000C9430
		public static bool HasVillageNote(string ID)
		{
			foreach (JournalVillageNote journalVillageNote in JournalAPI.VillageNotes)
			{
				if (journalVillageNote.ID == ID && journalVillageNote.Revealed)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000CB298 File Offset: 0x000C9498
		public static bool HasUnrevealedVillageNote(string ID)
		{
			foreach (JournalVillageNote journalVillageNote in JournalAPI.VillageNotes)
			{
				if (journalVillageNote.ID == ID && !journalVillageNote.Revealed)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x000CB300 File Offset: 0x000C9500
		private static Dictionary<string, List<JournalMapNote>> mapNotesByZone
		{
			get
			{
				if (JournalAPI._mapNotesByZone == null)
				{
					JournalAPI._mapNotesByZone = new Dictionary<string, List<JournalMapNote>>();
					foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
					{
						if (!JournalAPI._mapNotesByZone.ContainsKey(journalMapNote.ZoneID))
						{
							JournalAPI._mapNotesByZone.Add(journalMapNote.ZoneID, new List<JournalMapNote>());
						}
						JournalAPI._mapNotesByZone[journalMapNote.ZoneID].Add(journalMapNote);
					}
				}
				return JournalAPI._mapNotesByZone;
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000CB3A0 File Offset: 0x000C95A0
		public static void DeleteMapNote(JournalMapNote note)
		{
			JournalAPI._mapNoteCategories = null;
			JournalAPI.MapNotes.Remove(note);
			List<JournalMapNote> list;
			if (note.ZoneID != null && JournalAPI.mapNotesByZone.TryGetValue(note.ZoneID, out list))
			{
				list.Remove(note);
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000CB3E4 File Offset: 0x000C95E4
		public static IBaseJournalEntry GetRandomRevealedNote(Predicate<IBaseJournalEntry> filter = null)
		{
			List<IBaseJournalEntry> list = new List<IBaseJournalEntry>();
			list.AddRange((from c in JournalAPI.Accomplishments
				select (c) into c
				where c.Revealed && (filter == null || filter(c))
				select c).ToList<IBaseJournalEntry>());
			list.AddRange((from c in JournalAPI.Observations
				select (c) into c
				where c.Revealed && (filter == null || filter(c))
				select c).ToList<IBaseJournalEntry>());
			list.AddRange((from c in JournalAPI.GetKnownSultanNotes()
				select (c) into c
				where c.Revealed && (filter == null || filter(c))
				select c).ToList<IBaseJournalEntry>());
			list.AddRange((from c in JournalAPI.MapNotes
				select (c) into c
				where c.Revealed && (filter == null || filter(c))
				select c).ToList<IBaseJournalEntry>());
			list.AddRange((from c in JournalAPI.RecipeNotes
				select (c) into c
				where c.Revealed && (filter == null || filter(c))
				select c).ToList<IBaseJournalEntry>());
			return list.GetRandomElement<IBaseJournalEntry>(null);
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000CB562 File Offset: 0x000C9762
		public static IBaseJournalEntry GetRandomUnrevealedNote(Predicate<IBaseJournalEntry> Filter = null)
		{
			return JournalAPI.GetUnrevealedNotes(Filter).GetRandomElement<IBaseJournalEntry>(null);
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000CB570 File Offset: 0x000C9770
		public static List<IBaseJournalEntry> GetUnrevealedNotes(Predicate<IBaseJournalEntry> Filter = null)
		{
			List<IBaseJournalEntry> list = new List<IBaseJournalEntry>();
			foreach (JournalSultanNote journalSultanNote in JournalAPI.SultanNotes)
			{
				if (!journalSultanNote.Revealed && (Filter == null || Filter(journalSultanNote)))
				{
					list.Add(journalSultanNote);
				}
			}
			foreach (JournalObservation journalObservation in JournalAPI.Observations)
			{
				if (!journalObservation.Revealed && (Filter == null || Filter(journalObservation)))
				{
					list.Add(journalObservation);
				}
			}
			foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
			{
				if (!journalMapNote.Revealed && (Filter == null || Filter(journalMapNote)))
				{
					list.Add(journalMapNote);
				}
			}
			return list;
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000CB690 File Offset: 0x000C9890
		public static void RevealRandomSecret()
		{
			JournalAPI.GetRandomUnrevealedNote(null).Reveal(null, false);
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000CB69F File Offset: 0x000C989F
		public static void RevealMapNote(JournalMapNote note, bool silent = false, string LearnedFrom = null)
		{
			note.Reveal(LearnedFrom, silent);
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000CB6AC File Offset: 0x000C98AC
		public static void AddMapNote(JournalMapNote newNote)
		{
			if (JournalAPI.MapNotes.Contains(newNote))
			{
				return;
			}
			JournalAPI.MapNotes.Add(newNote);
			JournalAPI.AddedNote(newNote);
			List<JournalMapNote> list;
			if (!JournalAPI.mapNotesByZone.TryGetValue(newNote.ZoneID, out list))
			{
				list = (JournalAPI.mapNotesByZone[newNote.ZoneID] = new List<JournalMapNote>());
			}
			if (!list.Contains(newNote))
			{
				list.Add(newNote);
			}
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000CB714 File Offset: 0x000C9914
		public static void AddMapNote(string ZoneID, string text, string category = "general", string[] attributes = null, string secretId = null, bool revealed = false, bool sold = false, long time = -1L, bool silent = false)
		{
			if (!ZoneID.Contains("."))
			{
				int x = The.Player.Physics.CurrentCell.X;
				int y = The.Player.Physics.CurrentCell.Y;
				string stringGameState = XRLCore.Core.Game.GetStringGameState("LastLocationOnSurface", "");
				ZoneID = string.Concat(new string[]
				{
					ZoneID,
					".",
					x.ToString(),
					".",
					y.ToString(),
					".2.2.10"
				});
				if (stringGameState.Contains("."))
				{
					string[] array = stringGameState.Split('.', StringSplitOptions.None);
					if ((int)Convert.ToInt16(array[1]) == x && (int)Convert.ToInt16(array[2]) == y)
					{
						ZoneID = stringGameState.Split('@', StringSplitOptions.None)[0];
					}
				}
			}
			JournalAPI._mapNoteCategories = null;
			JournalMapNote journalMapNote = new JournalMapNote();
			if (time == -1L)
			{
				journalMapNote.Time = XRLCore.Core.Game.TimeTicks;
			}
			else
			{
				journalMapNote.Time = time;
			}
			journalMapNote.Tradable = !sold;
			journalMapNote.Text = text;
			journalMapNote.ZoneID = ZoneID;
			journalMapNote.Category = category;
			journalMapNote.ID = secretId;
			if (attributes != null)
			{
				journalMapNote.Attributes.AddRange(attributes);
			}
			List<JournalMapNote> list;
			if (!JournalAPI.mapNotesByZone.TryGetValue(journalMapNote.ZoneID, out list))
			{
				list = (JournalAPI.mapNotesByZone[journalMapNote.ZoneID] = new List<JournalMapNote>());
			}
			list.Add(journalMapNote);
			JournalAPI.MapNotes.Add(journalMapNote);
			JournalAPI.AddedNote(journalMapNote);
			if (JournalAPI.sorting)
			{
				journalMapNote.Forget(true);
				JournalAPI.MapNotes.Sort((JournalMapNote a, JournalMapNote b) => a.Text.CompareTo(b.Text));
				if (revealed)
				{
					JournalAPI.RevealMapNote(journalMapNote, silent, "Exploration");
					return;
				}
			}
			else
			{
				if (revealed)
				{
					journalMapNote.Reveal("Exploration", silent);
					return;
				}
				journalMapNote.Forget(true);
			}
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000CB900 File Offset: 0x000C9B00
		public static List<JournalMapNote> GetMapNotesForColumn(string world, int wx, int wy)
		{
			List<JournalMapNote> list = new List<JournalMapNote>();
			foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
			{
				if (journalMapNote.WorldID == world && journalMapNote.ParasangX == wx && journalMapNote.ParasangY == wy)
				{
					list.Add(journalMapNote);
				}
			}
			return list;
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000CB97C File Offset: 0x000C9B7C
		public static JournalRecipeNote AddRecipeNote(CookingRecipe recipe, GameObject Chef = null, bool revealed = true, bool silent = false, string id = null)
		{
			if (id == null)
			{
				id = Guid.NewGuid().ToString();
			}
			JournalRecipeNote journalRecipeNote = new JournalRecipeNote();
			journalRecipeNote.Recipe = recipe;
			journalRecipeNote.Text = string.Concat(new string[]
			{
				recipe.GetDisplayName(),
				"\n",
				recipe.GetIngredients(),
				"\n\n",
				recipe.GetDescription()
			});
			journalRecipeNote.ID = id;
			journalRecipeNote.Attributes.Add("recipe");
			journalRecipeNote.Attributes.Add(id);
			if (Chef != null)
			{
				if (Chef.IsPlayer())
				{
					journalRecipeNote.Attributes.Add("chef:player");
				}
				else
				{
					journalRecipeNote.Attributes.Add("chef:" + Chef.BaseID.ToString());
				}
			}
			JournalAPI.RecipeNotes.Add(journalRecipeNote);
			JournalAPI.AddedNote(journalRecipeNote);
			if (JournalAPI.sorting)
			{
				JournalAPI.RecipeNotes.Sort((JournalRecipeNote a, JournalRecipeNote b) => a.Text.CompareTo(b.Text));
			}
			if (revealed)
			{
				journalRecipeNote.Reveal(null, silent);
			}
			return journalRecipeNote;
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000CBA9D File Offset: 0x000C9C9D
		public static void DeleteRecipeNote(JournalRecipeNote note)
		{
			JournalAPI.RecipeNotes.Remove(note);
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000CBAAC File Offset: 0x000C9CAC
		public static JournalGeneralNote AddGeneralNote(string text, string secretId = null, long time = -1L, bool revealed = true)
		{
			JournalGeneralNote journalGeneralNote = new JournalGeneralNote();
			journalGeneralNote.Text = text;
			journalGeneralNote.ID = secretId;
			journalGeneralNote.Tradable = false;
			if (time == -1L)
			{
				journalGeneralNote.Time = XRLCore.Core.Game.TimeTicks;
			}
			else
			{
				journalGeneralNote.Time = time;
			}
			JournalAPI.GeneralNotes.Add(journalGeneralNote);
			JournalAPI.AddedNote(journalGeneralNote);
			if (JournalAPI.sorting)
			{
				JournalAPI.GeneralNotes.Sort((JournalGeneralNote a, JournalGeneralNote b) => a.Time.CompareTo(b.Time));
			}
			if (revealed)
			{
				journalGeneralNote.Reveal(null, false);
			}
			return journalGeneralNote;
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000CBB44 File Offset: 0x000C9D44
		public static void SuspendSorting()
		{
			JournalAPI.sorting = false;
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000CBB4C File Offset: 0x000C9D4C
		public static void ResumeSorting()
		{
			JournalAPI.sorting = true;
			JournalAPI.MapNotes.Sort((JournalMapNote a, JournalMapNote b) => a.Text.CompareTo(b.Text));
			JournalAPI.Accomplishments.Sort((JournalAccomplishment a, JournalAccomplishment b) => a.Time.CompareTo(b.Time));
			JournalAPI.Observations.Sort((JournalObservation a, JournalObservation b) => a.Time.CompareTo(b.Time));
			JournalAPI.GeneralNotes.Sort((JournalGeneralNote a, JournalGeneralNote b) => a.Time.CompareTo(b.Time));
			JournalAPI.RecipeNotes.Sort((JournalRecipeNote a, JournalRecipeNote b) => a.Text.CompareTo(b.Text));
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000CBC2C File Offset: 0x000C9E2C
		public unsafe static string FormatSecretID(string Text)
		{
			int length = Text.Length;
			int num = 1;
			int num2 = length + 1;
			Span<char> span;
			checked
			{
				span = new Span<char>(stackalloc byte[unchecked((UIntPtr)num2) * 2], num2);
				*span[0] = '$';
			}
			for (int i = 0; i < length; i++)
			{
				char c = Text[i];
				if (char.IsLetterOrDigit(c))
				{
					*span[num++] = char.ToLowerInvariant(c);
				}
			}
			return new string(span.Slice(0, num));
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000CBCA8 File Offset: 0x000C9EA8
		[WishCommand(null, null, Regex = "^reveal\\s*settlements?$")]
		public static bool HandleRevealSettlementsWish(Match match)
		{
			foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
			{
				if (!journalMapNote.Revealed && journalMapNote.Category == "Settlements" && journalMapNote.Has("villages"))
				{
					JournalAPI.RevealMapNote(journalMapNote, false, null);
				}
			}
			return true;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000CBD24 File Offset: 0x000C9F24
		[WishCommand("gospelme", null)]
		public static void WishGospelAccomplishments()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			foreach (JournalAccomplishment journalAccomplishment in JournalAPI.Accomplishments)
			{
				stringBuilder.Compound("{{K|", "\n\n {{K|---}}\n").Append(journalAccomplishment.Text).Append("}}\n");
				stringBuilder.Append(journalAccomplishment.GospelText.IsNullOrEmpty() ? "{{W|[MISSING GOSPEL]}}" : journalAccomplishment.GospelText);
			}
			Popup.Show(stringBuilder.StartReplace().AddObject(The.Player, null).ToString(), "Gospel", "Sounds/UI/ui_notification", true, true, true, false, null);
		}

		// Token: 0x04001356 RID: 4950
		public static StringMap<IBaseJournalEntry> NotesByID = new StringMap<IBaseJournalEntry>();

		// Token: 0x04001357 RID: 4951
		public static List<JournalAccomplishment> Accomplishments = new List<JournalAccomplishment>();

		// Token: 0x04001358 RID: 4952
		public static List<JournalObservation> Observations = new List<JournalObservation>();

		// Token: 0x04001359 RID: 4953
		public static List<JournalMapNote> MapNotes = new List<JournalMapNote>();

		// Token: 0x0400135A RID: 4954
		public static List<JournalRecipeNote> RecipeNotes = new List<JournalRecipeNote>();

		// Token: 0x0400135B RID: 4955
		public static List<JournalGeneralNote> GeneralNotes = new List<JournalGeneralNote>();

		// Token: 0x0400135C RID: 4956
		public static List<JournalSultanNote> SultanNotes = new List<JournalSultanNote>();

		// Token: 0x0400135D RID: 4957
		public static List<JournalVillageNote> VillageNotes = new List<JournalVillageNote>();

		// Token: 0x0400135E RID: 4958
		public static List<string> _mapNoteCategories = null;

		// Token: 0x0400135F RID: 4959
		private static Dictionary<string, List<JournalMapNote>> _mapNotesByZone;

		// Token: 0x04001360 RID: 4960
		public static bool sorting = true;
	}
}
