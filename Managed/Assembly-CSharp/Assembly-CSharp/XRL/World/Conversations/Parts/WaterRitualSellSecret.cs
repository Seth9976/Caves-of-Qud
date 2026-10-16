using System;
using System.Collections.Generic;
using System.Text;
using Qud.API;
using XRL.UI;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C2B RID: 3115
	public class WaterRitualSellSecret : IWaterRitualSecretPart
	{
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06007C90 RID: 31888 RVA: 0x002ECB41 File Offset: 0x002EAD41
		// (set) Token: 0x06007C91 RID: 31889 RVA: 0x002ECB4C File Offset: 0x002EAD4C
		public bool Gossip
		{
			get
			{
				return !this.Secret;
			}
			set
			{
				this.Secret = !value;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06007C92 RID: 31890 RVA: 0x002ECB58 File Offset: 0x002EAD58
		public override bool Affordable
		{
			get
			{
				return WaterRitual.Record.totalFactionAvailable > 0;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06007C93 RID: 31891 RVA: 0x002ECB67 File Offset: 0x002EAD67
		public override bool IsSell
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007C94 RID: 31892 RVA: 0x002ECB6A File Offset: 0x002EAD6A
		public override bool Filter(IBaseJournalEntry Entry)
		{
			return Entry.CanSell();
		}

		// Token: 0x06007C95 RID: 31893 RVA: 0x002ECB74 File Offset: 0x002EAD74
		public override int GetWeight(IBaseJournalEntry Entry)
		{
			int buySecretWeight = WaterRitual.RecordFaction.GetBuySecretWeight(Entry, The.Speaker);
			if (buySecretWeight <= 0)
			{
				return 0;
			}
			JournalObservation journalObservation = Entry as JournalObservation;
			if (journalObservation != null)
			{
				if (journalObservation.Category != "Gossip" != this.Secret)
				{
					return 0;
				}
				return buySecretWeight;
			}
			else
			{
				if (!this.Secret)
				{
					return 0;
				}
				return buySecretWeight;
			}
		}

		// Token: 0x06007C96 RID: 31894 RVA: 0x002ECBCC File Offset: 0x002EADCC
		public virtual void Share()
		{
			List<IBaseJournalEntry> distinctNotes = this.GetDistinctNotes(3);
			HistoryAPI.OnWaterRitualSellSecret(WaterRitual.Record, distinctNotes);
			int num = Popup.PickOption(this.Secret ? "Choose a secret to share:" : "Choose some gossip to share:", null, "", "Sounds/UI/ui_notification", this.GetOptionsFor(distinctNotes), null, null, null, null, null, null, 1, 60, 0, -1, true, true, false, true, false, null, null);
			if (num >= 0)
			{
				this.SellEntry(distinctNotes[num]);
			}
		}

		// Token: 0x06007C97 RID: 31895 RVA: 0x002ECC3C File Offset: 0x002EAE3C
		public virtual void SellEntry(IBaseJournalEntry Entry)
		{
			Entry.Tradable = false;
			Entry.AppendHistory(" {{K|-shared with " + WaterRitual.RecordFaction.GetFormattedName() + "}}");
			Entry.Updated();
			Entry.Reveal(WaterRitual.RecordFaction.GetFormattedName(), false);
			base.AwardReputation(this.Bonus, "WaterRitualAward");
		}

		// Token: 0x06007C98 RID: 31896 RVA: 0x002ECC98 File Offset: 0x002EAE98
		public override void Awake()
		{
			base.Awake();
			this.Message = null;
			this.Reputation = (this.Secret ? 50 : 100);
			this.Bonus = 0;
			GetWaterRitualSellSecretBehaviorEvent.Send(The.Player, The.Speaker, ref this.Message, ref this.Reputation, ref this.Bonus, this.Secret, this.Gossip);
			this.Reputation = Math.Min(this.Reputation, WaterRitual.Record.totalFactionAvailable);
		}

		// Token: 0x06007C99 RID: 31897 RVA: 0x002ECD15 File Offset: 0x002EAF15
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID || ID == PrepareTextEvent.ID;
		}

		// Token: 0x06007C9A RID: 31898 RVA: 0x002ECD3B File Offset: 0x002EAF3B
		public override bool HandleEvent(PrepareTextEvent E)
		{
			if (!this.Message.IsNullOrEmpty())
			{
				E.Text.Clear();
				E.Text.Append(this.Message);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C9B RID: 31899 RVA: 0x002ECD70 File Offset: 0x002EAF70
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (WaterRitual.Record.totalFactionAvailable <= 0)
			{
				Popup.ShowFail(The.Speaker.T(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + " can't grant you any more reputation.", true, true, true);
			}
			else
			{
				this.Share();
				base.Awake();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C9C RID: 31900 RVA: 0x002ECDDC File Offset: 0x002EAFDC
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null).Append("{{").Append(this.Lowlight)
				.Append("|[{{")
				.Append(this.Numeric)
				.Append("|+")
				.Append(base.GetReputationAward("WaterRitualAward"));
			if (this.Bonus != 0)
			{
				if (this.Affordable)
				{
					stringBuilder.Append("{{c|");
				}
				stringBuilder.Append((this.Bonus > 0) ? '+' : '-').Append(this.Bonus);
				if (this.Affordable)
				{
					stringBuilder.Append("}}");
				}
			}
			E.Tag = stringBuilder.Append("}} reputation]}}").ToString();
			return false;
		}

		// Token: 0x04003899 RID: 14489
		public string Message;

		// Token: 0x0400389A RID: 14490
		public int Bonus;

		// Token: 0x0400389B RID: 14491
		public bool Secret = true;
	}
}
