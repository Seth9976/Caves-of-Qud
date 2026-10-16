using System;

namespace XRL.World
{
	// Token: 0x020006A1 RID: 1697
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeforeSlamEvent : PooledEvent<BeforeSlamEvent>
	{
		// Token: 0x0600485C RID: 18524 RVA: 0x0016C11C File Offset: 0x0016A31C
		public override int GetCascadeLevel()
		{
			return BeforeSlamEvent.CascadeLevel;
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x0016C123 File Offset: 0x0016A323
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x0016C12C File Offset: 0x0016A32C
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.TargetCell = null;
			this.SlamMultiplier = 0;
			this.SlamPower = 0;
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x0016C158 File Offset: 0x0016A358
		public static bool Check(GameObject Actor, GameObject Target, Cell TargetCell, ref int SlamMultiplier, ref int SlamPower)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("BeforeSlam"))
			{
				Event @event = Event.New("BeforeSlam", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("TargetCell", TargetCell);
				@event.SetParameter("SlamMultiplier", SlamMultiplier);
				@event.SetParameter("SlamPower", SlamPower);
				flag = Actor.FireEvent(@event);
				SlamMultiplier = @event.GetIntParameter("SlamMultiplier", 0);
				SlamPower = @event.GetIntParameter("SlamPower", 0);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<BeforeSlamEvent>.ID, BeforeSlamEvent.CascadeLevel))
			{
				BeforeSlamEvent beforeSlamEvent = PooledEvent<BeforeSlamEvent>.FromPool();
				beforeSlamEvent.Actor = Actor;
				beforeSlamEvent.Target = Target;
				beforeSlamEvent.TargetCell = TargetCell;
				beforeSlamEvent.SlamMultiplier = SlamMultiplier;
				beforeSlamEvent.SlamPower = SlamPower;
				flag = Actor.HandleEvent(beforeSlamEvent);
				SlamMultiplier = beforeSlamEvent.SlamMultiplier;
				SlamPower = beforeSlamEvent.SlamPower;
			}
			return flag;
		}

		// Token: 0x0400229B RID: 8859
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400229C RID: 8860
		public GameObject Actor;

		// Token: 0x0400229D RID: 8861
		public GameObject Target;

		// Token: 0x0400229E RID: 8862
		public Cell TargetCell;

		// Token: 0x0400229F RID: 8863
		public int SlamMultiplier;

		// Token: 0x040022A0 RID: 8864
		public int SlamPower;
	}
}
