using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Annals;
using XRL.Core;
using XRL.Rules;
using XRL.World;

namespace HistoryKit
{
	// Token: 0x0200036A RID: 874
	[Serializable]
	public class HistoricEntity : IComposite
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x0009F237 File Offset: 0x0009D437
		public string Name
		{
			get
			{
				return this.GetEntityProperty("name", -1L);
			}
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x0009F246 File Offset: 0x0009D446
		private HistoricEntity()
		{
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x0009F259 File Offset: 0x0009D459
		public HistoricEntity(History history)
		{
			this.history = history;
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x0009F274 File Offset: 0x0009D474
		public static HistoricEntity Load(SerializationReader reader, History history)
		{
			HistoricEntity historicEntity = new HistoricEntity(history);
			historicEntity.id = reader.ReadString();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				HistoricEvent historicEvent = HistoricEvent.Load(reader);
				historicEvent.entity = historicEntity;
				historicEvent.history = history;
				historicEntity.events.Add(historicEvent);
			}
			return historicEntity;
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0009F2CC File Offset: 0x0009D4CC
		public void Save(SerializationWriter writer)
		{
			writer.Write(this.id);
			writer.Write(this.events.Count);
			foreach (HistoricEvent historicEvent in this.events)
			{
				historicEvent.Save(writer);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x0009F33C File Offset: 0x0009D53C
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x0009F35C File Offset: 0x0009D55C
		public History history
		{
			get
			{
				if (this._history == null)
				{
					return XRLCore.Core.Game.sultanHistory;
				}
				return this._history;
			}
			set
			{
				this._history = value;
			}
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0009F368 File Offset: 0x0009D568
		public HistoricEvent GetRandomEventWhereDelegate(HistoricEntity.eventMatchingDelegate matcher, Random R = null)
		{
			if (R == null)
			{
				R = Stat.Rand;
			}
			List<HistoricEvent> list = new List<HistoricEvent>();
			for (int i = 0; i < this.events.Count; i++)
			{
				if (matcher(this.events[i]))
				{
					list.Add(this.events[i]);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list[R.Next(0, list.Count)];
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x0009F3E0 File Offset: 0x0009D5E0
		public bool HasEntityProperty(string Key, long Year = -1L)
		{
			foreach (HistoricEvent historicEvent in this.events)
			{
				if ((Year == -1L || historicEvent.year <= Year) && historicEvent.entityProperties != null && historicEvent.entityProperties.ContainsKey(Key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x0009F458 File Offset: 0x0009D658
		public bool HasEventProperty(string Key, long Year = -1L)
		{
			foreach (HistoricEvent historicEvent in this.events)
			{
				if ((Year == -1L || historicEvent.year <= Year) && historicEvent.eventProperties != null && historicEvent.eventProperties.ContainsKey(Key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0009F4D0 File Offset: 0x0009D6D0
		public string GetEntityProperty(string Key, long Year = -1L)
		{
			for (int i = this.events.Count - 1; i >= 0; i--)
			{
				HistoricEvent historicEvent = this.events[i];
				string text;
				if ((Year == -1L || historicEvent.year <= Year) && historicEvent.entityProperties != null && historicEvent.entityProperties.TryGetValue(Key, out text))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0009F52C File Offset: 0x0009D72C
		public string GetEventProperty(string Key, long Year = -1L)
		{
			for (int i = this.events.Count - 1; i >= 0; i--)
			{
				HistoricEvent historicEvent = this.events[i];
				string text;
				if ((Year == -1L || historicEvent.year <= Year) && historicEvent.eventProperties != null && historicEvent.eventProperties.TryGetValue(Key, out text))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x0009F588 File Offset: 0x0009D788
		public HistoricEvent GetEvent(Predicate<HistoricEvent> Predicate, long Year = -1L)
		{
			for (int i = this.events.Count - 1; i >= 0; i--)
			{
				HistoricEvent historicEvent = this.events[i];
				if ((Year == -1L || historicEvent.year <= Year) && Predicate(historicEvent))
				{
					return historicEvent;
				}
			}
			return null;
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x0009F5D4 File Offset: 0x0009D7D4
		public HistoricEvent GetEventWithEventProperty(string Key, long Year = -1L)
		{
			for (int i = this.events.Count - 1; i >= 0; i--)
			{
				HistoricEvent historicEvent = this.events[i];
				if ((Year == -1L || historicEvent.year <= Year) && historicEvent.eventProperties != null && historicEvent.eventProperties.ContainsKey(Key))
				{
					return historicEvent;
				}
			}
			return null;
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x0009F630 File Offset: 0x0009D830
		public HistoricEvent GetEventWithEntityProperty(string Key, long Year = -1L)
		{
			for (int i = this.events.Count - 1; i >= 0; i--)
			{
				HistoricEvent historicEvent = this.events[i];
				if ((Year == -1L || historicEvent.year <= Year) && historicEvent.entityProperties != null && historicEvent.entityProperties.ContainsKey(Key))
				{
					return historicEvent;
				}
			}
			return null;
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0009F68C File Offset: 0x0009D88C
		public bool HasListProperties(string Key, long Year = -1L)
		{
			for (int i = this.events.Count - 1; i >= 0; i--)
			{
				HistoricEvent historicEvent = this.events[i];
				if ((Year == -1L || historicEvent.year <= Year) && historicEvent.addedListProperties != null && historicEvent.addedListProperties.ContainsKey(Key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0009F6E8 File Offset: 0x0009D8E8
		public List<string> GetListProperties(string Key, long Year = -1L, bool ReadOnly = true)
		{
			List<string> list = (ReadOnly ? HistoricEntity.ListProperties : new List<string>());
			list.Clear();
			foreach (HistoricEvent historicEvent in this.events)
			{
				if (Year == -1L || historicEvent.year <= Year)
				{
					List<string> list2;
					if (historicEvent.addedListProperties != null && historicEvent.addedListProperties.TryGetValue(Key, out list2))
					{
						using (List<string>.Enumerator enumerator2 = list2.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string text = enumerator2.Current;
								if (!list.Contains(text))
								{
									list.Add(text);
								}
							}
							continue;
						}
					}
					List<string> list3;
					if (historicEvent.removedListProperties != null && historicEvent.removedListProperties.TryGetValue(Key, out list3))
					{
						foreach (string text2 in list3)
						{
							list.Remove(text2);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x0009F820 File Offset: 0x0009DA20
		public void SetEntityPropertyAtCurrentYear(string name, string value)
		{
			this.ApplyEvent(new SetEntityProperty(name, value), long.MinValue);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x0009F838 File Offset: 0x0009DA38
		public void MutateListPropertyAtCurrentYear(string name, Func<string, string> mutation)
		{
			this.ApplyEvent(new MutateListProperty(name, mutation, this.GetCurrentSnapshot()), long.MinValue);
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x0009F858 File Offset: 0x0009DA58
		public void ApplyEvent(HistoricEvent newEvent, long year = -9223372036854775808L)
		{
			if (year == -9223372036854775808L)
			{
				newEvent.year = this.lastYear;
			}
			else
			{
				newEvent.year = year;
			}
			newEvent.SetEntityHistory(this, this.history);
			this.history.SetupEvent(newEvent);
			newEvent.Generate();
			this.AddEvent(newEvent);
			this.history.AddEvent(newEvent);
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x0009F8B8 File Offset: 0x0009DAB8
		public void AddEvent(HistoricEvent Event)
		{
			int num = this.events.Count;
			int num2 = num - 1;
			while (num2 >= 0 && Event.year < this.events[num2].year)
			{
				num = num2;
				num2--;
			}
			this.events.Insert(num, Event);
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x0009F907 File Offset: 0x0009DB07
		public HistoricEntitySnapshot GetCurrentSnapshot()
		{
			return this.GetSnapshotAtYear(this.history.currentYear);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0009F91C File Offset: 0x0009DB1C
		public HistoricEntitySnapshot GetSnapshotAtYear(long year)
		{
			HistoricEntitySnapshot historicEntitySnapshot = new HistoricEntitySnapshot(this);
			if (this.events == null || this.events.Count == 0)
			{
				return historicEntitySnapshot;
			}
			foreach (HistoricEvent historicEvent in this.events.OrderBy<HistoricEvent, long>((HistoricEvent ev) => ev.year))
			{
				if (historicEvent.year <= year)
				{
					historicEvent.ApplyToSnapshot(historicEntitySnapshot);
				}
			}
			return historicEntitySnapshot;
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x0009F9B8 File Offset: 0x0009DBB8
		public long firstYear
		{
			get
			{
				if (this.events == null || this.events.Count == 0)
				{
					return long.MinValue;
				}
				return this.events[0].year;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x0009F9EC File Offset: 0x0009DBEC
		public long lastYear
		{
			get
			{
				if (this.events == null || this.events.Count == 0)
				{
					return long.MinValue;
				}
				return this.events[this.events.Count - 1].year + this.events[this.events.Count - 1].duration;
			}
		}

		// Token: 0x0400107E RID: 4222
		public const long INVALID_DATE = -9223372036854775808L;

		// Token: 0x0400107F RID: 4223
		public const long LAST_ACTIVE_YEAR = -9223372036854775808L;

		// Token: 0x04001080 RID: 4224
		public string id;

		// Token: 0x04001081 RID: 4225
		[NonSerialized]
		public History _history;

		// Token: 0x04001082 RID: 4226
		public List<HistoricEvent> events = new List<HistoricEvent>();

		// Token: 0x04001083 RID: 4227
		[NonSerialized]
		private static List<string> ListProperties = new List<string>();

		// Token: 0x02001769 RID: 5993
		// (Invoke) Token: 0x0600DD59 RID: 56665
		public delegate bool eventMatchingDelegate(HistoricEvent ev);
	}
}
