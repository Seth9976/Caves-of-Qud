using System;
using System.Collections.Generic;
using System.Text;
using XRL.Collections;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.World;

namespace HistoryKit
{
	// Token: 0x02000371 RID: 881
	[Serializable]
	public class History : HistoricEntityList
	{
		// Token: 0x060020D4 RID: 8404 RVA: 0x000A2594 File Offset: 0x000A0794
		public History(long startingYear)
		{
			this.currentYear = startingYear;
			this.startingYear = startingYear;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x000A25FC File Offset: 0x000A07FC
		public Random r
		{
			get
			{
				if (XRLCore.Core.Game != null)
				{
					return Stat.Rnd4;
				}
				if (this._r == null)
				{
					this._r = new Random();
				}
				return this._r;
			}
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000A2629 File Offset: 0x000A0829
		public int Random(int low, int high)
		{
			return this.r.Next(low, high + 1);
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000A263C File Offset: 0x000A083C
		public static History Load(SerializationReader reader)
		{
			History history = new History(0L);
			history.startingYear = reader.ReadInt64();
			history.currentYear = reader.ReadInt64();
			history.totalEvents = reader.ReadInt64();
			history.nextEventId = history.totalEvents + 1L;
			int num = reader.ReadInt32();
			history.entities = new List<HistoricEntity>();
			history.events = new List<HistoricEvent>();
			for (int i = 0; i < num; i++)
			{
				HistoricEntity historicEntity = HistoricEntity.Load(reader, history);
				history.entities.Add(historicEntity);
				history.events.AddRange(historicEntity.events);
				history.EntityByID[historicEntity.id] = historicEntity;
			}
			return history;
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000A26E8 File Offset: 0x000A08E8
		public void Save(SerializationWriter writer)
		{
			writer.Write(this.startingYear);
			writer.Write(this.currentYear);
			writer.Write(this.totalEvents);
			writer.Write(this.entities.Count);
			for (int i = 0; i < this.entities.Count; i++)
			{
				this.entities[i].Save(writer);
			}
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000A2754 File Offset: 0x000A0954
		public HistoricEvent GetEvent(long id)
		{
			foreach (HistoricEvent historicEvent in this.events)
			{
				if (historicEvent.id == id)
				{
					return historicEvent;
				}
			}
			return null;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000A27B0 File Offset: 0x000A09B0
		public void SetupEvent(HistoricEvent newEvent)
		{
			if (newEvent.id != 0L)
			{
				throw new Exception("event for setup already has ID");
			}
			long num = this.nextEventId;
			this.nextEventId = num + 1L;
			newEvent.id = num;
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000A27E8 File Offset: 0x000A09E8
		public void AddEvent(HistoricEvent newEvent)
		{
			if (newEvent.id == 0L)
			{
				throw new Exception("event to add had no ID");
			}
			this.events.Add(newEvent);
			this.totalEvents += 1L;
			if (newEvent.year + newEvent.duration > this.currentYear)
			{
				this.currentYear = newEvent.year + newEvent.duration;
			}
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000A284C File Offset: 0x000A0A4C
		public HistoricEntity CreateEntity()
		{
			return this.CreateEntity(Guid.NewGuid().ToString(), this.currentYear);
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000A2878 File Offset: 0x000A0A78
		public HistoricEntity CreateEntity(long Year)
		{
			return this.CreateEntity(Guid.NewGuid().ToString(), Year);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000A28A0 File Offset: 0x000A0AA0
		public HistoricEntity CreateEntity(string ID, long Year)
		{
			HistoricEntity historicEntity = new HistoricEntity(this);
			historicEntity.id = ID;
			this.entities.Add(historicEntity);
			this.EntityByID[ID] = historicEntity;
			historicEntity.ApplyEvent(new CreatedHistoricEvent(), Year);
			return historicEntity;
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000A28E1 File Offset: 0x000A0AE1
		[Obsolete("Use CreateEntity")]
		public HistoricEntity GetNewEntity(long createdYear)
		{
			return this.CreateEntity(createdYear);
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000A28EA File Offset: 0x000A0AEA
		public override HistoricEntity GetEntity(string ID)
		{
			return this.EntityByID.GetValue(ID);
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000A28F8 File Offset: 0x000A0AF8
		public override bool TryGetEntity(string ID, out HistoricEntity Entity)
		{
			return this.EntityByID.TryGetValue(ID, out Entity);
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000A2908 File Offset: 0x000A0B08
		public List<HistoricEntitySnapshot> GetEntitySnapshotsAtYear(long year)
		{
			List<HistoricEntitySnapshot> list = new List<HistoricEntitySnapshot>();
			for (int i = 0; i < this.entities.Count; i++)
			{
				if (year >= this.entities[i].firstYear && year <= this.entities[i].lastYear)
				{
					list.Add(this.entities[i].GetSnapshotAtYear(year));
				}
			}
			return list;
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000A2974 File Offset: 0x000A0B74
		public string Dump(bool bVerbose = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			for (int i = 0; i < this.entities.Count; i++)
			{
				if (bVerbose)
				{
					HistoricEntitySnapshot snapshotAtYear = this.entities[i].GetSnapshotAtYear(this.entities[i].lastYear);
					if (snapshotAtYear.GetProperty("type", "unknown").Equals("region"))
					{
						stringBuilder.AppendLine();
						stringBuilder.AppendLine("*** Region added to history: " + snapshotAtYear.properties["name"] + " ***");
					}
					else if (snapshotAtYear.GetProperty("type", "unknown").Equals("location"))
					{
						stringBuilder.AppendLine("------------New location: " + snapshotAtYear.properties["name"]);
					}
				}
				for (int j = 0; j < this.entities[i].events.Count; j++)
				{
					if (this.entities[i].events[j].HasEventProperty("gospel"))
					{
						stringBuilder.AppendLine("  @" + this.entities[i].events[j].year.ToString() + "  " + Grammar.ConvertAtoAn(this.entities[i].events[j].GetEventProperty("gospel", null)));
					}
				}
				if (this.entities[i].GetSnapshotAtYear(this.entities[i].lastYear).GetProperty("type", "unknown").Equals("sultan"))
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400109E RID: 4254
		public List<HistoricEvent> events = new List<HistoricEvent>();

		// Token: 0x0400109F RID: 4255
		public StringMap<HistoricEntity> EntityByID = new StringMap<HistoricEntity>();

		// Token: 0x040010A0 RID: 4256
		public long startingYear = long.MinValue;

		// Token: 0x040010A1 RID: 4257
		public long currentYear = long.MinValue;

		// Token: 0x040010A2 RID: 4258
		private Random _r = new Random();

		// Token: 0x040010A3 RID: 4259
		private long totalEvents;

		// Token: 0x040010A4 RID: 4260
		[NonSerialized]
		private long nextEventId = 1L;
	}
}
