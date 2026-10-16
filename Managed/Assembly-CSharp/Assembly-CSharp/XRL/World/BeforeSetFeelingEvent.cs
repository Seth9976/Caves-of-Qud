using System;
using XRL.World.AI;

namespace XRL.World
{
	// Token: 0x0200069F RID: 1695
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeSetFeelingEvent : PooledEvent<BeforeSetFeelingEvent>
	{
		// Token: 0x06004852 RID: 18514 RVA: 0x0016BF69 File Offset: 0x0016A169
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x0016BF72 File Offset: 0x0016A172
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.Opinion = null;
			this.Feeling = 0;
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x0016BF98 File Offset: 0x0016A198
		public static bool Check(GameObject Actor, GameObject Target, ObjectOpinion Opinion, ref int Feeling)
		{
			bool flag = true;
			if (Actor.WantEvent(PooledEvent<BeforeSetFeelingEvent>.ID, BeforeSetFeelingEvent.CascadeLevel))
			{
				BeforeSetFeelingEvent beforeSetFeelingEvent = PooledEvent<BeforeSetFeelingEvent>.FromPool();
				beforeSetFeelingEvent.Actor = Actor;
				beforeSetFeelingEvent.Target = Target;
				beforeSetFeelingEvent.Opinion = Opinion;
				beforeSetFeelingEvent.Feeling = Feeling;
				flag = Actor.HandleEvent(beforeSetFeelingEvent);
				Feeling = beforeSetFeelingEvent.Feeling;
				PooledEvent<BeforeSetFeelingEvent>.ResetTo(ref beforeSetFeelingEvent);
			}
			if (flag && Actor.HasRegisteredEvent("BeforeSetFeeling"))
			{
				Event @event = Event.New("BeforeSetFeeling", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("Opinion", Opinion);
				@event.SetParameter("Feeling", Feeling);
				flag = Actor.FireEvent(@event);
				Feeling = @event.GetIntParameter("Feeling", 0);
			}
			return flag;
		}

		// Token: 0x0400228F RID: 8847
		public GameObject Actor;

		// Token: 0x04002290 RID: 8848
		public GameObject Target;

		// Token: 0x04002291 RID: 8849
		public ObjectOpinion Opinion;

		// Token: 0x04002292 RID: 8850
		public int Feeling;

		// Token: 0x04002293 RID: 8851
		public new static readonly int CascadeLevel;
	}
}
