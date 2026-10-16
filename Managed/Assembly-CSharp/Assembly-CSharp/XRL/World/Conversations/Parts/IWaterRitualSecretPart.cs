using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Collections;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C02 RID: 3074
	public abstract class IWaterRitualSecretPart : IWaterRitualPart
	{
		// Token: 0x06007B7F RID: 31615 RVA: 0x002E60A9 File Offset: 0x002E42A9
		public virtual bool Filter(IBaseJournalEntry Entry)
		{
			return true;
		}

		// Token: 0x06007B80 RID: 31616 RVA: 0x002E60AC File Offset: 0x002E42AC
		[Obsolete]
		public virtual bool SultanNote(JournalSultanNote Entry)
		{
			return true;
		}

		// Token: 0x06007B81 RID: 31617 RVA: 0x002E60AF File Offset: 0x002E42AF
		[Obsolete]
		public virtual bool Observation(JournalObservation Entry)
		{
			return true;
		}

		// Token: 0x06007B82 RID: 31618 RVA: 0x002E60B2 File Offset: 0x002E42B2
		[Obsolete]
		public virtual bool MapNote(JournalMapNote Entry)
		{
			return true;
		}

		// Token: 0x06007B83 RID: 31619 RVA: 0x002E60B5 File Offset: 0x002E42B5
		[Obsolete]
		public virtual bool RecipeNote(JournalRecipeNote Entry)
		{
			return true;
		}

		// Token: 0x06007B84 RID: 31620 RVA: 0x002E60B8 File Offset: 0x002E42B8
		public virtual int GetWeight(IBaseJournalEntry Entry)
		{
			return Entry.Weight;
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06007B85 RID: 31621 RVA: 0x002E60C0 File Offset: 0x002E42C0
		public virtual bool IsBuy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06007B86 RID: 31622 RVA: 0x002E60C3 File Offset: 0x002E42C3
		public virtual bool IsSell
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007B87 RID: 31623 RVA: 0x002E60C8 File Offset: 0x002E42C8
		public override void Awake()
		{
			if (!The.Speaker.GetxTag("WaterRitual", "NoSecrets", null).EqualsNoCase("true"))
			{
				this.ShuffleNotes(WaterRitual.Record.mySeed);
				this.Visible = !this.Notes.Items.IsNullOrEmpty<IBaseJournalEntry>();
			}
		}

		/// <remarks>Used for prioritizing notes that share little in common with previously selected notes.</remarks>
		// Token: 0x06007B88 RID: 31624 RVA: 0x002E6120 File Offset: 0x002E4320
		public virtual int CountShared(IBaseJournalEntry Entry, List<IBaseJournalEntry> Notes)
		{
			int num = 0;
			foreach (IBaseJournalEntry baseJournalEntry in Notes)
			{
				if (Entry.Text == baseJournalEntry.Text)
				{
					num++;
				}
				foreach (string text in baseJournalEntry.Attributes)
				{
					if (Entry.Attributes.Contains(text))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06007B89 RID: 31625 RVA: 0x002E61D0 File Offset: 0x002E43D0
		public virtual void InitializeTagWeights(GameObject Speaker)
		{
			if (this.IsBuy)
			{
				string text = Speaker.GetxTag("WaterRitual", "SellSecretWeight", null);
				if (!text.IsNullOrEmpty())
				{
					foreach (ReadOnlySpan<char> readOnlySpan in text.DelimitedBy(';'))
					{
						ReadOnlySpan<char> readOnlySpan2;
						ReadOnlySpan<char> readOnlySpan3;
						readOnlySpan.Split(':', out readOnlySpan2, out readOnlySpan3);
						int num;
						if (int.TryParse(readOnlySpan3, out num))
						{
							this.Tagged[readOnlySpan2] = num;
						}
					}
					return;
				}
			}
			else if (this.IsSell)
			{
				string text2 = Speaker.GetxTag("WaterRitual", "BuySecretWeight", null);
				if (!text2.IsNullOrEmpty())
				{
					foreach (ReadOnlySpan<char> readOnlySpan4 in text2.DelimitedBy(';'))
					{
						ReadOnlySpan<char> readOnlySpan5;
						ReadOnlySpan<char> readOnlySpan6;
						readOnlySpan4.Split(':', out readOnlySpan5, out readOnlySpan6);
						int num2;
						if (int.TryParse(readOnlySpan6, out num2))
						{
							this.Tagged[readOnlySpan5] = num2;
						}
					}
				}
			}
		}

		// Token: 0x06007B8A RID: 31626 RVA: 0x002E62B8 File Offset: 0x002E44B8
		public void Dispatch(GetWaterRitualSecretWeightEvent E, IBaseJournalEntry Secret)
		{
			int num = 0;
			if (this.Filter(Secret))
			{
				int num2;
				num = (this.Tagged.TryGetValue(Secret.ID, out num2) ? num2 : this.GetWeight(Secret));
			}
			E.Secret = Secret;
			E.BaseWeight = (E.Weight = num);
			int i = 0;
			int count = this.Handlers.Count;
			while (i < count)
			{
				this.Handlers[i].HandleEvent(E);
				i++;
			}
			if (E.Weight > 0)
			{
				this.Notes.Add(E.Secret, E.Weight);
			}
		}

		// Token: 0x06007B8B RID: 31627 RVA: 0x002E6354 File Offset: 0x002E4554
		public virtual void ShuffleNotes(int Seed)
		{
			this.Seed = Seed;
			if (this.Notes == null)
			{
				this.Notes = new BallBag<IBaseJournalEntry>(new Random(Seed), 16);
			}
			this.Notes.Clear();
			if (this.Handlers == null)
			{
				this.Handlers = new Rack<IEventHandler>();
			}
			this.Handlers.Clear();
			if (this.Tagged == null)
			{
				this.Tagged = new StringMap<int>();
			}
			this.Tagged.Clear();
			GetWaterRitualSecretWeightEvent getWaterRitualSecretWeightEvent = PooledEvent<GetWaterRitualSecretWeightEvent>.FromPool();
			getWaterRitualSecretWeightEvent.Actor = The.Player;
			getWaterRitualSecretWeightEvent.Speaker = The.Speaker;
			getWaterRitualSecretWeightEvent.Buy = this.IsBuy;
			getWaterRitualSecretWeightEvent.Sell = this.IsSell;
			getWaterRitualSecretWeightEvent.Source = this;
			getWaterRitualSecretWeightEvent.Context = this.Context;
			this.InitializeTagWeights(getWaterRitualSecretWeightEvent.Speaker);
			getWaterRitualSecretWeightEvent.Actor.GetWantEventHandlers(PooledEvent<GetWaterRitualSecretWeightEvent>.ID, MinEvent.CascadeLevel, this.Handlers);
			getWaterRitualSecretWeightEvent.Speaker.GetWantEventHandlers(PooledEvent<GetWaterRitualSecretWeightEvent>.ID, MinEvent.CascadeLevel, this.Handlers);
			foreach (JournalSultanNote journalSultanNote in JournalAPI.SultanNotes)
			{
				this.Dispatch(getWaterRitualSecretWeightEvent, journalSultanNote);
			}
			foreach (JournalObservation journalObservation in JournalAPI.Observations)
			{
				this.Dispatch(getWaterRitualSecretWeightEvent, journalObservation);
			}
			foreach (JournalMapNote journalMapNote in JournalAPI.MapNotes)
			{
				this.Dispatch(getWaterRitualSecretWeightEvent, journalMapNote);
			}
			foreach (JournalRecipeNote journalRecipeNote in JournalAPI.RecipeNotes)
			{
				this.Dispatch(getWaterRitualSecretWeightEvent, journalRecipeNote);
			}
			getWaterRitualSecretWeightEvent.Reset();
			this.Handlers.Clear();
			this.Tagged.Clear();
			HistoryAPI.OnWaterRitualShuffleSecrets(WaterRitual.Record, this.Notes);
		}

		// Token: 0x06007B8C RID: 31628 RVA: 0x002E6594 File Offset: 0x002E4794
		public virtual List<IBaseJournalEntry> GetDistinctNotes(int Amount = 3)
		{
			if (Amount > this.Notes.Count)
			{
				Amount = this.Notes.Count;
			}
			this.Notes.Random = new Random(this.Seed);
			List<IBaseJournalEntry> list = new List<IBaseJournalEntry>(Amount);
			for (int i = 0; i < Amount; i++)
			{
				IBaseJournalEntry distinctNote = this.GetDistinctNote(list);
				if (distinctNote != null)
				{
					list.Add(distinctNote);
				}
			}
			return list;
		}

		// Token: 0x06007B8D RID: 31629 RVA: 0x002E65F8 File Offset: 0x002E47F8
		public virtual IBaseJournalEntry GetDistinctNote(List<IBaseJournalEntry> Selected = null)
		{
			if (Selected == null)
			{
				this.Notes.Random = new Random(this.Seed);
				return this.Notes.PeekOne();
			}
			IBaseJournalEntry baseJournalEntry = null;
			int num = int.MaxValue;
			int i = 0;
			int num2 = this.Notes.Count * 5;
			while (i < num2)
			{
				IBaseJournalEntry baseJournalEntry2 = this.Notes.PeekOne();
				if (!Selected.Contains(baseJournalEntry2))
				{
					num2 = Math.Min(num2, 5);
					int num3 = this.CountShared(baseJournalEntry2, Selected);
					if (num3 < num)
					{
						baseJournalEntry = baseJournalEntry2;
						num = num3;
						if (num3 == 0)
						{
							break;
						}
					}
				}
				i++;
			}
			return baseJournalEntry;
		}

		// Token: 0x06007B8E RID: 31630 RVA: 0x002E6688 File Offset: 0x002E4888
		public virtual string[] GetOptionsFor(List<IBaseJournalEntry> Secrets)
		{
			int count = Secrets.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = Secrets[i].GetShareText();
			}
			return array;
		}

		// Token: 0x0400382A RID: 14378
		public const int REP_SECRET = 50;

		// Token: 0x0400382B RID: 14379
		public const int REP_GOSSIP = 100;

		// Token: 0x0400382C RID: 14380
		public BallBag<IBaseJournalEntry> Notes;

		// Token: 0x0400382D RID: 14381
		public Rack<IEventHandler> Handlers;

		// Token: 0x0400382E RID: 14382
		public StringMap<int> Tagged;

		// Token: 0x0400382F RID: 14383
		public int Seed;

		// Token: 0x04003830 RID: 14384
		public string Context;
	}
}
