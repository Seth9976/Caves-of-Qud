using System;
using ConsoleLib.Console;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.UI;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C22 RID: 3106
	public class WaterRitualBuySecret : IWaterRitualSecretPart
	{
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06007C4A RID: 31818 RVA: 0x002EAF7B File Offset: 0x002E917B
		public override bool Available
		{
			get
			{
				return WaterRitual.Record.secretsRemaining > 0;
			}
		}

		// Token: 0x06007C4B RID: 31819 RVA: 0x002EAF8A File Offset: 0x002E918A
		public override bool Filter(IBaseJournalEntry Entry)
		{
			return Entry.CanBuy();
		}

		// Token: 0x06007C4C RID: 31820 RVA: 0x002EAF92 File Offset: 0x002E9192
		public override int GetWeight(IBaseJournalEntry Entry)
		{
			return WaterRitual.RecordFaction.GetSellSecretWeight(Entry);
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06007C4D RID: 31821 RVA: 0x002EAF9F File Offset: 0x002E919F
		public override bool IsBuy
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007C4E RID: 31822 RVA: 0x002EAFA2 File Offset: 0x002E91A2
		public override void Awake()
		{
			base.Awake();
			this.Reputation = GetWaterRitualCostEvent.GetFor(The.Player, The.Speaker, "Secret", 50);
		}

		// Token: 0x06007C4F RID: 31823 RVA: 0x002EAFC8 File Offset: 0x002E91C8
		public virtual void Share()
		{
			IBaseJournalEntry distinctNote = this.GetDistinctNote(null);
			HistoryAPI.OnWaterRitualBuySecret(WaterRitual.Record, ref distinctNote);
			this.RevealEntry(distinctNote);
		}

		// Token: 0x06007C50 RID: 31824 RVA: 0x002EAFF0 File Offset: 0x002E91F0
		public virtual void RevealEntry(IBaseJournalEntry Entry)
		{
			bool flag = true;
			Entry.Attributes.Add(WaterRitual.RecordFaction.NoBuySecretString);
			WaterRitual.Record.secretsRemaining--;
			JournalSultanNote journalSultanNote = Entry as JournalSultanNote;
			if (journalSultanNote == null)
			{
				JournalMapNote journalMapNote = Entry as JournalMapNote;
				if (journalMapNote == null)
				{
					JournalObservation journalObservation = Entry as JournalObservation;
					if (journalObservation == null)
					{
						if (Entry is JournalRecipeNote)
						{
							Popup.Show(The.Speaker.Does("share", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " a recipe with you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
					}
					else
					{
						string text = The.Speaker.Does("share", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " some gossip with you.";
						if (Entry.Has("gossip"))
						{
							text += "\n\n\"";
							string text2 = HistoricStringExpander.ExpandString("<spice.gossip.leadIns.!random>", null, The.Game.sultanHistory, null, null);
							if (text2.Contains('?') || text2.Contains('.') || journalObservation.Rumor)
							{
								text = text + text2 + " " + journalObservation.Text;
							}
							else
							{
								text = text + text2 + " " + Grammar.InitLower(journalObservation.Text);
							}
							text += "\"";
						}
						Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				else
				{
					Popup.Show(The.Speaker.Does("share", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " the location of " + Markup.Wrap(Grammar.LowerArticles(journalMapNote.Text)) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			else
			{
				HistoricEvent @event = HistoryAPI.GetEvent(journalSultanNote.EventID);
				Popup.Show(The.Speaker.Does("share", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " an event from the life of a sultan with you.\n\n\"" + @event.GetEventProperty("gospel", null) + "\"", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				@event.Reveal(null);
				flag = false;
			}
			if (WaterRitual.RecordFaction.Visible)
			{
				Entry.AppendHistory(" {{K|-learned from " + WaterRitual.RecordFaction.GetFormattedName() + "}}");
			}
			if (flag || !Entry.Revealed)
			{
				Entry.Reveal(WaterRitual.RecordFaction.GetFormattedName(), false);
			}
			Entry.Updated();
		}

		// Token: 0x06007C51 RID: 31825 RVA: 0x002EB2BA File Offset: 0x002E94BA
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007C52 RID: 31826 RVA: 0x002EB2D8 File Offset: 0x002E94D8
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (WaterRitual.Record.secretsRemaining <= 0)
			{
				Popup.ShowFail(The.Speaker.T(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + " has no more secrets to share.", true, true, true);
			}
			else if (base.UseReputation("WaterRitualUse"))
			{
				this.Share();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C53 RID: 31827 RVA: 0x002EB34C File Offset: 0x002E954C
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = string.Concat(new string[]
			{
				"{{",
				this.Lowlight,
				"|[{{",
				this.Numeric,
				"|",
				base.GetReputationCost("WaterRitualUse").ToString(),
				"}} reputation]}}"
			});
			return false;
		}
	}
}
