using System;

namespace XRL.World
{
	// Token: 0x0200077B RID: 1915
	[GameEvent(Cascade = 3, Cache = MinEvent.Cache.Pool)]
	public class GetPsionicSifrahSetupEvent : PooledEvent<GetPsionicSifrahSetupEvent>
	{
		// Token: 0x06004EA2 RID: 20130 RVA: 0x001810EB File Offset: 0x0017F2EB
		public override int GetCascadeLevel()
		{
			return GetPsionicSifrahSetupEvent.CascadeLevel;
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x001810F2 File Offset: 0x0017F2F2
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x001810FC File Offset: 0x0017F2FC
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Subject = null;
			this.Type = null;
			this.Subtype = null;
			this.Interruptable = false;
			this.Difficulty = 0;
			this.Rating = 0;
			this.Turns = 0;
			this.PsychometryApplied = false;
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x00181150 File Offset: 0x0017F350
		public static bool GetFor(GameObject Actor, GameObject Subject, string Type, string Subtype, bool Interruptable, ref int Difficulty, ref int Rating, ref int Turns, ref bool Interrupt, ref bool PsychometryApplied)
		{
			bool flag = true;
			if (Actor.WantEvent(PooledEvent<GetPsionicSifrahSetupEvent>.ID, GetPsionicSifrahSetupEvent.CascadeLevel))
			{
				GetPsionicSifrahSetupEvent getPsionicSifrahSetupEvent = PooledEvent<GetPsionicSifrahSetupEvent>.FromPool();
				getPsionicSifrahSetupEvent.Actor = Actor;
				getPsionicSifrahSetupEvent.Subject = Subject;
				getPsionicSifrahSetupEvent.Type = Type;
				getPsionicSifrahSetupEvent.Subtype = Subtype;
				getPsionicSifrahSetupEvent.Difficulty = Difficulty;
				getPsionicSifrahSetupEvent.Rating = Rating;
				getPsionicSifrahSetupEvent.Turns = Turns;
				getPsionicSifrahSetupEvent.Interruptable = Interruptable;
				getPsionicSifrahSetupEvent.PsychometryApplied = PsychometryApplied;
				flag = Actor.HandleEvent(getPsionicSifrahSetupEvent);
				if (Interruptable && !flag)
				{
					Interrupt = true;
				}
				Difficulty = getPsionicSifrahSetupEvent.Difficulty;
				Rating = getPsionicSifrahSetupEvent.Rating;
				Turns = getPsionicSifrahSetupEvent.Turns;
				PsychometryApplied = getPsionicSifrahSetupEvent.PsychometryApplied;
			}
			return flag;
		}

		// Token: 0x04002665 RID: 9829
		public new static readonly int CascadeLevel = 3;

		// Token: 0x04002666 RID: 9830
		public GameObject Actor;

		// Token: 0x04002667 RID: 9831
		public GameObject Subject;

		// Token: 0x04002668 RID: 9832
		public string Type;

		// Token: 0x04002669 RID: 9833
		public string Subtype;

		// Token: 0x0400266A RID: 9834
		public bool Interruptable;

		// Token: 0x0400266B RID: 9835
		public int Difficulty;

		// Token: 0x0400266C RID: 9836
		public int Rating;

		// Token: 0x0400266D RID: 9837
		public int Turns;

		// Token: 0x0400266E RID: 9838
		public bool PsychometryApplied;
	}
}
