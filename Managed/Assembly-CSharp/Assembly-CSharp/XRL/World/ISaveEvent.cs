using System;

namespace XRL.World
{
	// Token: 0x020007DA RID: 2010
	[GameEvent(Base = true, Cascade = 17)]
	public abstract class ISaveEvent : MinEvent
	{
		// Token: 0x06005103 RID: 20739 RVA: 0x00188210 File Offset: 0x00186410
		public override int GetCascadeLevel()
		{
			return ISaveEvent.CascadeLevel;
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x00188217 File Offset: 0x00186417
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x00188220 File Offset: 0x00186420
		public override void Reset()
		{
			base.Reset();
			this.Attacker = null;
			this.Defender = null;
			this.Source = null;
			this.Stat = null;
			this.AttackerStat = null;
			this.Vs = null;
			this.NaturalRoll = 0;
			this.Roll = 0;
			this.BaseDifficulty = 0;
			this.Difficulty = 0;
			this.IgnoreNatural1 = false;
			this.IgnoreNatural20 = false;
			this.Actual = false;
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x00188290 File Offset: 0x00186490
		public static bool Process(GameObject Target, string RegisteredEventID, int ID, int CascadeLevel, ISaveEvent.ISaveEventGenerator Generator, GameObject Attacker, GameObject Defender, GameObject Source, string Stat, string AttackerStat, string Vs, int NaturalRoll, ref int Roll, int BaseDifficulty, ref int Difficulty, ref bool IgnoreNatural1, ref bool IgnoreNatural20, bool Actual)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Target) && Target.HasRegisteredEvent(RegisteredEventID))
			{
				Event @event = Event.New(RegisteredEventID, 0, 0, 0);
				@event.SetParameter("Attacker", Attacker);
				@event.SetParameter("Defender", Defender);
				@event.SetParameter("Source", Source);
				@event.SetParameter("Stat", Stat);
				@event.SetParameter("AttackerStat", AttackerStat ?? Stat);
				@event.SetParameter("Vs", Vs ?? "");
				@event.SetParameter("NaturalRoll", NaturalRoll);
				@event.SetParameter("Roll", Roll);
				@event.SetParameter("BaseDifficulty", BaseDifficulty);
				@event.SetParameter("Difficulty", Difficulty);
				@event.SetFlag("IgnoreNatural1", IgnoreNatural1);
				@event.SetFlag("IgnoreNatural20", IgnoreNatural20);
				@event.SetFlag("Actual", Actual);
				flag = Target.FireEvent(@event);
				Roll = @event.GetIntParameter("Roll", 0);
				Difficulty = @event.GetIntParameter("Difficulty", 0);
				IgnoreNatural1 = @event.HasFlag("IgnoreNatural1");
				IgnoreNatural20 = @event.HasFlag("IgnoreNatural20");
			}
			if (flag && GameObject.Validate(ref Target) && Target.WantEvent(ID, CascadeLevel))
			{
				ISaveEvent saveEvent = Generator(Attacker, Defender, Source, Stat, AttackerStat ?? Stat, Vs ?? "", NaturalRoll, Roll, BaseDifficulty, Difficulty, IgnoreNatural1, IgnoreNatural20, Actual);
				flag = Target.HandleEvent(saveEvent);
				Roll = saveEvent.Roll;
				Difficulty = saveEvent.Difficulty;
				IgnoreNatural1 = saveEvent.IgnoreNatural1;
				IgnoreNatural20 = saveEvent.IgnoreNatural20;
			}
			return true;
		}

		// Token: 0x04002871 RID: 10353
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002872 RID: 10354
		public GameObject Attacker;

		// Token: 0x04002873 RID: 10355
		public GameObject Defender;

		// Token: 0x04002874 RID: 10356
		public GameObject Source;

		// Token: 0x04002875 RID: 10357
		public string Stat;

		// Token: 0x04002876 RID: 10358
		public string AttackerStat;

		// Token: 0x04002877 RID: 10359
		public string Vs;

		// Token: 0x04002878 RID: 10360
		public int NaturalRoll;

		// Token: 0x04002879 RID: 10361
		public int Roll;

		// Token: 0x0400287A RID: 10362
		public int BaseDifficulty;

		// Token: 0x0400287B RID: 10363
		public int Difficulty;

		// Token: 0x0400287C RID: 10364
		public bool IgnoreNatural1;

		// Token: 0x0400287D RID: 10365
		public bool IgnoreNatural20;

		// Token: 0x0400287E RID: 10366
		public bool Actual;

		// Token: 0x02001A22 RID: 6690
		// (Invoke) Token: 0x0600E63F RID: 58943
		public delegate ISaveEvent ISaveEventGenerator(GameObject Attacker, GameObject Defender, GameObject Source, string Stat, string AttackerStat, string Vs, int NaturalRoll, int Roll, int BaseDifficulty, int Difficulty, bool IgnoreNatural1, bool IgnoreNatural20, bool Actual);
	}
}
