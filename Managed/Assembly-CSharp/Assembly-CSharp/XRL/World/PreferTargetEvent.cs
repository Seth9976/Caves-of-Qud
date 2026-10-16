using System;

namespace XRL.World
{
	// Token: 0x0200082A RID: 2090
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class PreferTargetEvent : PooledEvent<PreferTargetEvent>
	{
		// Token: 0x0600531F RID: 21279 RVA: 0x0018EA43 File Offset: 0x0018CC43
		public override int GetCascadeLevel()
		{
			return PreferTargetEvent.CascadeLevel;
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x0018EA4A File Offset: 0x0018CC4A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x0018EA53 File Offset: 0x0018CC53
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target1 = null;
			this.Target2 = null;
			this.Result = 0;
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x0018EA78 File Offset: 0x0018CC78
		public static int Check(GameObject Actor, GameObject Target1, GameObject Target2)
		{
			int num = 0;
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("PreferTarget"))
			{
				Event @event = Event.New("PreferTarget", 0, 0, 0);
				@event.SetParameter("Target1", Target1);
				@event.SetParameter("Target2", Target2);
				@event.SetParameter("Result", num);
				flag = Actor.FireEvent(@event);
				num = @event.GetIntParameter("Result", 0);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<PreferTargetEvent>.ID, PreferTargetEvent.CascadeLevel))
			{
				PreferTargetEvent preferTargetEvent = PooledEvent<PreferTargetEvent>.FromPool();
				preferTargetEvent.Target1 = Target1;
				preferTargetEvent.Target2 = Target2;
				preferTargetEvent.Result = num;
				flag = Actor.HandleEvent(preferTargetEvent);
				num = preferTargetEvent.Result;
			}
			return num;
		}

		// Token: 0x040029D3 RID: 10707
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040029D4 RID: 10708
		public GameObject Actor;

		// Token: 0x040029D5 RID: 10709
		public GameObject Target1;

		// Token: 0x040029D6 RID: 10710
		public GameObject Target2;

		// Token: 0x040029D7 RID: 10711
		public int Result;
	}
}
