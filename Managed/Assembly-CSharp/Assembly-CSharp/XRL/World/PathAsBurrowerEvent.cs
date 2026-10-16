using System;

namespace XRL.World
{
	// Token: 0x02000823 RID: 2083
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class PathAsBurrowerEvent : PooledEvent<PathAsBurrowerEvent>
	{
		// Token: 0x060052F2 RID: 21234 RVA: 0x0018E44F File Offset: 0x0018C64F
		public override int GetCascadeLevel()
		{
			return PathAsBurrowerEvent.CascadeLevel;
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x0018E456 File Offset: 0x0018C656
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x0018E45F File Offset: 0x0018C65F
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x0018E470 File Offset: 0x0018C670
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("PathAsBurrower"))
			{
				Event @event = Event.New("PathAsBurrower", 0, 0, 0);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<PathAsBurrowerEvent>.ID, PathAsBurrowerEvent.CascadeLevel))
			{
				PathAsBurrowerEvent pathAsBurrowerEvent = PooledEvent<PathAsBurrowerEvent>.FromPool();
				pathAsBurrowerEvent.Object = Object;
				flag = Object.HandleEvent(pathAsBurrowerEvent);
			}
			return !flag;
		}

		// Token: 0x040029B8 RID: 10680
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040029B9 RID: 10681
		public GameObject Object;
	}
}
