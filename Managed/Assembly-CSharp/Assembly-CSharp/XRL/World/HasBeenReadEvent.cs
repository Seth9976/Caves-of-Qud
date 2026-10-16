using System;

namespace XRL.World
{
	// Token: 0x020007B1 RID: 1969
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class HasBeenReadEvent : PooledEvent<HasBeenReadEvent>
	{
		// Token: 0x0600500D RID: 20493 RVA: 0x00185D67 File Offset: 0x00183F67
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x00185D70 File Offset: 0x00183F70
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x00185D88 File Offset: 0x00183F88
		public static bool Check(GameObject Object, GameObject Actor)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("HasBeenRead"))
			{
				Event @event = Event.New("HasBeenRead", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<HasBeenReadEvent>.ID, MinEvent.CascadeLevel))
			{
				HasBeenReadEvent hasBeenReadEvent = PooledEvent<HasBeenReadEvent>.FromPool();
				hasBeenReadEvent.Object = Object;
				hasBeenReadEvent.Actor = Actor;
				flag = Object.HandleEvent(hasBeenReadEvent);
			}
			return !flag;
		}

		// Token: 0x040027A0 RID: 10144
		public GameObject Object;

		// Token: 0x040027A1 RID: 10145
		public GameObject Actor;
	}
}
