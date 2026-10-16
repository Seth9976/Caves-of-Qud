using System;
using HistoryKit;
using Qud.API;
using Qud.UI;
using XRL.Language;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F6D RID: 3949
	[Serializable]
	public class OpeningStory : IPart
	{
		// Token: 0x06009D27 RID: 40231 RVA: 0x0038E751 File Offset: 0x0038C951
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009D28 RID: 40232 RVA: 0x0038E754 File Offset: 0x0038C954
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeTakeActionEvent>.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x06009D29 RID: 40233 RVA: 0x0038E774 File Offset: 0x0038C974
		public override bool HandleEvent(BeforeTakeActionEvent E)
		{
			if (!this.Triggered)
			{
				bool flag = The.Game.GetStringGameState("embark", "Joppa").EqualsNoCase("Joppa");
				this.Triggered = true;
				string text = null;
				if (The.Game.HasStringGameState("embarkIntroTextID"))
				{
					text = Data.GetText(The.Game.GetStringGameState("embarkIntroTextID", ""));
				}
				if (The.Game.HasStringGameState("embarkIntroText"))
				{
					text = The.Game.GetStringGameState("embarkIntroText", "");
				}
				if (text == null)
				{
					text = (flag ? Data.GetText("OpeningStoryJoppa") : Data.GetText("OpeningStoryAlternate"));
				}
				string displayName = this.ParentObject.GetCurrentCell().ParentZone.DisplayName;
				if (!flag)
				{
					The.Game.SetStringGameState("villageZeroName", displayName);
				}
				text = text.Replace("$day", Calendar.GetDay());
				text = text.Replace("$month", Calendar.GetMonth());
				text = text.Replace("$village", displayName);
				WorldGenerationScreen.HideWorldGenerationScreen();
				Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				TutorialManager.OnTrigger("OpeningStoryClosed");
				OpeningStory.AddAccomplishment(displayName);
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D2A RID: 40234 RVA: 0x0038E8B4 File Offset: 0x0038CAB4
		public static void AddAccomplishment(string VillageName)
		{
			string text = HistoricStringExpander.ExpandString("<spice.elements." + The.Player.GetMythicDomain(5, null) + ".nouns.!random>", null);
			string text2 = HistoricStringExpander.ExpandString("<spice.colors.!random>", null);
			JournalAPI.AddAccomplishment(string.Concat(new string[]
			{
				"On the ",
				Calendar.GetDay(),
				" of ",
				Calendar.GetMonth(),
				", you arrived at ",
				VillageName,
				"."
			}), string.Concat(new string[]
			{
				"On the auspicious ",
				Calendar.GetDay(),
				" of ",
				Calendar.GetMonth(),
				", =name= arrived in ",
				VillageName,
				" and began ",
				The.Player.GetPronounProvider(false).PossessiveAdjective,
				" prodigious odyssey through Qud."
			}), string.Concat(new string[]
			{
				"At <spice.time.partsOfDay.!random> under <spice.commonPhrases.strange.!random.article> and ",
				text2,
				" sky, the people of ",
				VillageName,
				" saw an image on the horizon that looked like a ",
				text,
				" bathed in ",
				text2,
				". It was =name=, and after ",
				The.Player.GetPronounProvider(false).Subjective,
				" came and left, the people of ",
				VillageName,
				" built a monument to =name= and thenceforth called ",
				The.Player.GetPronounProvider(false).Objective,
				" ",
				Grammar.MakeTitleCase(text),
				"-in-",
				Grammar.MakeTitleCase(text2),
				"."
			}), null, "general", MuralCategory.IsBorn, MuralWeight.Medium, null, -1L, true);
		}

		// Token: 0x06009D2B RID: 40235 RVA: 0x0038EA4D File Offset: 0x0038CC4D
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040040F1 RID: 16625
		public bool Triggered;
	}
}
