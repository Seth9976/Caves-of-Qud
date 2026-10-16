using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Language;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008F9 RID: 2297
	[Serializable]
	public class SocialSifrahTokenSecret : SifrahPrioritizableToken
	{
		// Token: 0x06005812 RID: 22546 RVA: 0x001AB194 File Offset: 0x001A9394
		public SocialSifrahTokenSecret()
		{
			this.Description = "tell a secret";
			this.Tile = "Items/sw_book1.bmp";
			this.RenderString = "\u000e";
			this.ColorString = "&K";
			this.DetailColor = 'Y';
		}

		// Token: 0x06005813 RID: 22547 RVA: 0x001AB1D0 File Offset: 0x001A93D0
		public SocialSifrahTokenSecret(GameObject Representative)
			: this()
		{
			this.UseFaction = Representative.GetPrimaryFaction(false);
			this.RepresentativeID = Representative.ID;
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x001AB1F1 File Offset: 0x001A93F1
		public override int GetPriority()
		{
			return this.GetNumberAvailable(0);
		}

		// Token: 0x06005815 RID: 22549 RVA: 0x001AB1FA File Offset: 0x001A93FA
		public override int GetTiebreakerPriority()
		{
			return 0;
		}

		// Token: 0x06005816 RID: 22550 RVA: 0x001AB200 File Offset: 0x001A9400
		public List<IBaseJournalEntry> GetAvailable()
		{
			if (this.RepresentativeID.IsNullOrEmpty())
			{
				return null;
			}
			GameObject rep = GameObject.FindByID(this.RepresentativeID);
			if (rep == null)
			{
				return null;
			}
			List<JournalSultanNote> sultanNotes = JournalAPI.GetSultanNotes((JournalSultanNote note) => Faction.WantsToBuySecret(this.UseFaction, note, rep));
			List<JournalObservation> observations = JournalAPI.GetObservations((JournalObservation observation) => Faction.WantsToBuySecret(this.UseFaction, observation, rep));
			List<JournalMapNote> mapNotes = JournalAPI.GetMapNotes((JournalMapNote mapnote) => Faction.WantsToBuySecret(this.UseFaction, mapnote, rep));
			List<JournalRecipeNote> recipes = JournalAPI.GetRecipes((JournalRecipeNote recipenote) => Faction.WantsToBuySecret(this.UseFaction, recipenote, rep));
			List<IBaseJournalEntry> list = new List<IBaseJournalEntry>(sultanNotes.Count + observations.Count + mapNotes.Count + recipes.Count);
			foreach (IBaseJournalEntry baseJournalEntry in sultanNotes)
			{
				list.Add(baseJournalEntry);
			}
			foreach (IBaseJournalEntry baseJournalEntry2 in observations)
			{
				list.Add(baseJournalEntry2);
			}
			foreach (IBaseJournalEntry baseJournalEntry3 in mapNotes)
			{
				list.Add(baseJournalEntry3);
			}
			foreach (IBaseJournalEntry baseJournalEntry4 in recipes)
			{
				list.Add(baseJournalEntry4);
			}
			return list;
		}

		// Token: 0x06005817 RID: 22551 RVA: 0x001AB3BC File Offset: 0x001A95BC
		public int GetNumberAvailable(int Chosen = 0)
		{
			if (this.RepresentativeID.IsNullOrEmpty())
			{
				return 0;
			}
			GameObject rep = GameObject.FindByID(this.RepresentativeID);
			if (rep == null)
			{
				return 0;
			}
			List<JournalSultanNote> sultanNotes = JournalAPI.GetSultanNotes((JournalSultanNote note) => Faction.WantsToBuySecret(this.UseFaction, note, rep));
			List<JournalObservation> observations = JournalAPI.GetObservations((JournalObservation observation) => Faction.WantsToBuySecret(this.UseFaction, observation, rep));
			List<JournalMapNote> mapNotes = JournalAPI.GetMapNotes((JournalMapNote mapnote) => Faction.WantsToBuySecret(this.UseFaction, mapnote, rep));
			List<JournalRecipeNote> recipes = JournalAPI.GetRecipes((JournalRecipeNote recipenote) => Faction.WantsToBuySecret(this.UseFaction, recipenote, rep));
			return sultanNotes.Count + observations.Count + mapNotes.Count + recipes.Count - Chosen;
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x001AB468 File Offset: 0x001A9668
		public bool IsAvailable(int Chosen = 0)
		{
			if (this.RepresentativeID.IsNullOrEmpty())
			{
				return false;
			}
			GameObject rep = GameObject.FindByID(this.RepresentativeID);
			if (rep == null)
			{
				return false;
			}
			int num = 0;
			List<JournalSultanNote> sultanNotes = JournalAPI.GetSultanNotes((JournalSultanNote note) => Faction.WantsToBuySecret(this.UseFaction, note, rep));
			num += sultanNotes.Count;
			if (num > Chosen)
			{
				return true;
			}
			List<JournalObservation> observations = JournalAPI.GetObservations((JournalObservation observation) => Faction.WantsToBuySecret(this.UseFaction, observation, rep));
			num += observations.Count;
			if (num > Chosen)
			{
				return true;
			}
			List<JournalMapNote> mapNotes = JournalAPI.GetMapNotes((JournalMapNote mapnote) => Faction.WantsToBuySecret(this.UseFaction, mapnote, rep));
			num += mapNotes.Count;
			if (num > Chosen)
			{
				return true;
			}
			List<JournalRecipeNote> recipes = JournalAPI.GetRecipes((JournalRecipeNote recipenote) => Faction.WantsToBuySecret(this.UseFaction, recipenote, rep));
			num += recipes.Count;
			return num > Chosen;
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x001AB538 File Offset: 0x001A9738
		public override string GetDescription(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return this.Description + " [have {{C|" + this.GetNumberAvailable(Game.GetTimesChosen(this, Slot)).ToString() + "}}]";
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x001AB570 File Offset: 0x001A9770
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable(Game.GetTimesChosen(this, Slot)) || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x001AB590 File Offset: 0x001A9790
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			int timesChosen = Game.GetTimesChosen(this, null);
			if (!this.IsAvailable(timesChosen))
			{
				Popup.ShowFail(string.Concat(new string[]
				{
					"You do not have any ",
					(timesChosen > 0) ? "more " : "",
					"secrets ",
					ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					ContextObject.Is,
					" interested in."
				}), true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x001AB62C File Offset: 0x001A982C
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			List<string> list = new List<string>();
			List<IBaseJournalEntry> list2 = new List<IBaseJournalEntry>();
			int num = 3;
			List<IBaseJournalEntry> available = this.GetAvailable();
			int num2 = 0;
			while (num2 < num && num2 < available.Count)
			{
				int num3 = -1;
				int num4 = int.MaxValue;
				for (int i = 0; i < available.Count; i++)
				{
					if (!list2.Contains(available[i]))
					{
						int num5 = SocialSifrahTokenSecret.SharesNPropertiesWith(available[i], list2);
						if (num5 < num4)
						{
							num3 = i;
							num4 = num5;
						}
					}
				}
				if (available[num3] is JournalMapNote)
				{
					list.Add("The location of " + Grammar.LowerArticles(available[num3].GetShortText()));
				}
				else
				{
					list.Add(available[num3].GetShortText());
				}
				list2.Add(available[num3]);
				num2++;
			}
			int num6 = Popup.PickOption("Choose a secret to share:\n", null, "", "Sounds/UI/ui_notification", list.ToArray(), new char[] { 'a', 'b', 'c' }, null, null, null, null, null, 1, 60, 0, -1, false, true, false, true, false, null, null);
			list2[num6].Tradable = false;
			if (Factions.Get(this.UseFaction).Visible)
			{
				if (list2[num6].History.Length > 0)
				{
					IBaseJournalEntry baseJournalEntry = list2[num6];
					baseJournalEntry.History += "\n";
				}
				IBaseJournalEntry baseJournalEntry2 = list2[num6];
				baseJournalEntry2.History = baseJournalEntry2.History + " {{K|-shared with " + Faction.GetFormattedName(this.UseFaction) + "}}";
			}
			list2[num6].Updated();
			base.UseToken(Game, Slot, ContextObject);
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x001AB7E4 File Offset: 0x001A99E4
		public static int SharesNPropertiesWith(IBaseJournalEntry entry, List<IBaseJournalEntry> choices)
		{
			int num = 0;
			for (int i = 0; i < choices.Count; i++)
			{
				if (entry.Text == choices[i].Text)
				{
					num++;
				}
				foreach (string text in choices[i].Attributes)
				{
					if (entry.Has(text))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x04002C16 RID: 11286
		private string UseFaction;

		// Token: 0x04002C17 RID: 11287
		private string RepresentativeID;
	}
}
