using System;

namespace XRL.World
{
	// Token: 0x02000784 RID: 1924
	[GameEvent(Cascade = 3, Cache = MinEvent.Cache.Pool)]
	public class GetRitualSifrahSetupEvent : PooledEvent<GetRitualSifrahSetupEvent>
	{
		// Token: 0x06004ED8 RID: 20184 RVA: 0x00181A9A File Offset: 0x0017FC9A
		public override int GetCascadeLevel()
		{
			return GetRitualSifrahSetupEvent.CascadeLevel;
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x00181AA1 File Offset: 0x0017FCA1
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x00181AAA File Offset: 0x0017FCAA
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Subject = null;
			this.Type = null;
			this.Interruptable = false;
			this.Difficulty = 0;
			this.Rating = 0;
			this.Turns = 0;
			this.PsychometryApplied = false;
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x00181AEC File Offset: 0x0017FCEC
		public static bool GetFor(GameObject Actor, GameObject Subject, string Type, bool Interruptable, ref int Difficulty, ref int Rating, ref int Turns, ref bool Interrupt, ref bool PsychometryApplied)
		{
			bool flag = true;
			if (Actor.WantEvent(PooledEvent<GetRitualSifrahSetupEvent>.ID, GetRitualSifrahSetupEvent.CascadeLevel))
			{
				GetRitualSifrahSetupEvent getRitualSifrahSetupEvent = PooledEvent<GetRitualSifrahSetupEvent>.FromPool();
				getRitualSifrahSetupEvent.Actor = Actor;
				getRitualSifrahSetupEvent.Subject = Subject;
				getRitualSifrahSetupEvent.Type = Type;
				getRitualSifrahSetupEvent.Difficulty = Difficulty;
				getRitualSifrahSetupEvent.Rating = Rating;
				getRitualSifrahSetupEvent.Turns = Turns;
				getRitualSifrahSetupEvent.Interruptable = Interruptable;
				getRitualSifrahSetupEvent.PsychometryApplied = PsychometryApplied;
				flag = Actor.HandleEvent(getRitualSifrahSetupEvent);
				if (Interruptable && !flag)
				{
					Interrupt = true;
				}
				Difficulty = getRitualSifrahSetupEvent.Difficulty;
				Rating = getRitualSifrahSetupEvent.Rating;
				Turns = getRitualSifrahSetupEvent.Turns;
				PsychometryApplied = getRitualSifrahSetupEvent.PsychometryApplied;
			}
			return flag;
		}

		// Token: 0x04002690 RID: 9872
		public new static readonly int CascadeLevel = 3;

		// Token: 0x04002691 RID: 9873
		public GameObject Actor;

		// Token: 0x04002692 RID: 9874
		public GameObject Subject;

		// Token: 0x04002693 RID: 9875
		public string Type;

		// Token: 0x04002694 RID: 9876
		public bool Interruptable;

		// Token: 0x04002695 RID: 9877
		public int Difficulty;

		// Token: 0x04002696 RID: 9878
		public int Rating;

		// Token: 0x04002697 RID: 9879
		public int Turns;

		// Token: 0x04002698 RID: 9880
		public bool PsychometryApplied;
	}
}
