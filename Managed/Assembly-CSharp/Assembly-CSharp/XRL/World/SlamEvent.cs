using System;

namespace XRL.World
{
	// Token: 0x0200085A RID: 2138
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class SlamEvent : PooledEvent<SlamEvent>
	{
		// Token: 0x0600554D RID: 21837 RVA: 0x001983BF File Offset: 0x001965BF
		public override int GetCascadeLevel()
		{
			return SlamEvent.CascadeLevel;
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x001983C6 File Offset: 0x001965C6
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x001983CF File Offset: 0x001965CF
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.TargetCell = null;
			this.SlamMultiplier = 0;
			this.SlamPower = 0;
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x001983FC File Offset: 0x001965FC
		public static bool Check(GameObject Actor, GameObject Target, Cell TargetCell, ref int SlamMultiplier, ref int SlamPower)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("Slam"))
			{
				Event @event = Event.New("Slam", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("TargetCell", TargetCell);
				@event.SetParameter("SlamMultiplier", SlamMultiplier);
				@event.SetParameter("SlamPower", SlamPower);
				flag = Actor.FireEvent(@event);
				SlamMultiplier = @event.GetIntParameter("SlamMultiplier", 0);
				SlamPower = @event.GetIntParameter("SlamPower", 0);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<SlamEvent>.ID, SlamEvent.CascadeLevel))
			{
				SlamEvent slamEvent = PooledEvent<SlamEvent>.FromPool();
				slamEvent.Actor = Actor;
				slamEvent.Target = Target;
				slamEvent.TargetCell = TargetCell;
				slamEvent.SlamMultiplier = SlamMultiplier;
				slamEvent.SlamPower = SlamPower;
				flag = Actor.HandleEvent(slamEvent);
				SlamMultiplier = slamEvent.SlamMultiplier;
				SlamPower = slamEvent.SlamPower;
			}
			return flag;
		}

		// Token: 0x04002AEF RID: 10991
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002AF0 RID: 10992
		public GameObject Actor;

		// Token: 0x04002AF1 RID: 10993
		public GameObject Target;

		// Token: 0x04002AF2 RID: 10994
		public Cell TargetCell;

		// Token: 0x04002AF3 RID: 10995
		public int SlamMultiplier;

		// Token: 0x04002AF4 RID: 10996
		public int SlamPower;
	}
}
