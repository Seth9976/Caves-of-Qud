using System;

namespace XRL.World
{
	// Token: 0x0200074D RID: 1869
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetFirefightingPerformanceEvent : PooledEvent<GetFirefightingPerformanceEvent>
	{
		// Token: 0x06004D51 RID: 19793 RVA: 0x0017C978 File Offset: 0x0017AB78
		public override int GetCascadeLevel()
		{
			return GetFirefightingPerformanceEvent.CascadeLevel;
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x0017C97F File Offset: 0x0017AB7F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x0017C988 File Offset: 0x0017AB88
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Result = 0;
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x0017C9A8 File Offset: 0x0017ABA8
		public static int GetFor(GameObject Actor, GameObject Object = null, bool Patting = false, bool Rolling = false)
		{
			int num = -100;
			if (Rolling)
			{
				num *= 2;
			}
			if (Object == null)
			{
				Object = Actor;
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetFirefightingPerformance"))
			{
				Event @event = Event.New("GetFirefightingPerformance", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Result", num);
				flag = Actor.FireEvent(@event);
				num = @event.GetIntParameter("Result", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object != Actor && Object.HasRegisteredEvent("GetFirefightingPerformance"))
			{
				Event event2 = Event.New("GetFirefightingPerformance", 0, 0, 0);
				event2.SetParameter("Actor", Actor);
				event2.SetParameter("Object", Object);
				event2.SetParameter("Result", num);
				flag = Object.FireEvent(event2);
				num = event2.GetIntParameter("Result", 0);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetFirefightingPerformanceEvent>.ID, GetFirefightingPerformanceEvent.CascadeLevel))
			{
				GetFirefightingPerformanceEvent getFirefightingPerformanceEvent = PooledEvent<GetFirefightingPerformanceEvent>.FromPool();
				getFirefightingPerformanceEvent.Actor = Actor;
				getFirefightingPerformanceEvent.Object = Object;
				getFirefightingPerformanceEvent.Result = num;
				flag = Actor.HandleEvent(getFirefightingPerformanceEvent);
				num = getFirefightingPerformanceEvent.Result;
			}
			if (flag && GameObject.Validate(ref Object) && Object != Actor && Object.WantEvent(PooledEvent<GetFirefightingPerformanceEvent>.ID, GetFirefightingPerformanceEvent.CascadeLevel))
			{
				GetFirefightingPerformanceEvent getFirefightingPerformanceEvent2 = PooledEvent<GetFirefightingPerformanceEvent>.FromPool();
				getFirefightingPerformanceEvent2.Actor = Actor;
				getFirefightingPerformanceEvent2.Object = Object;
				getFirefightingPerformanceEvent2.Result = num;
				flag = Object.HandleEvent(getFirefightingPerformanceEvent2);
				num = getFirefightingPerformanceEvent2.Result;
			}
			return num;
		}

		// Token: 0x04002584 RID: 9604
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002585 RID: 9605
		public const int FIREFIGHTING_BASE_PERFORMANCE = -100;

		// Token: 0x04002586 RID: 9606
		public const int FIREFIGHTING_ROLLING_FACTOR = 2;

		// Token: 0x04002587 RID: 9607
		public GameObject Actor;

		// Token: 0x04002588 RID: 9608
		public GameObject Object;

		// Token: 0x04002589 RID: 9609
		public int Result;
	}
}
