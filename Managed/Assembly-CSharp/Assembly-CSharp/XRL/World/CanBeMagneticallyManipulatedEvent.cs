using System;

namespace XRL.World
{
	// Token: 0x020006B9 RID: 1721
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanBeMagneticallyManipulatedEvent : PooledEvent<CanBeMagneticallyManipulatedEvent>
	{
		// Token: 0x06004912 RID: 18706 RVA: 0x00170940 File Offset: 0x0016EB40
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x00170949 File Offset: 0x0016EB49
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Allow = false;
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x00170960 File Offset: 0x0016EB60
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			bool flag2 = false;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CanBeMagneticallyManipulated"))
			{
				Event @event = Event.New("CanBeMagneticallyManipulated", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetFlag("Allow", flag2);
				flag = Object.FireEvent(@event);
				flag2 = @event.HasFlag("Allow");
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CanBeMagneticallyManipulatedEvent>.ID, MinEvent.CascadeLevel))
			{
				CanBeMagneticallyManipulatedEvent canBeMagneticallyManipulatedEvent = PooledEvent<CanBeMagneticallyManipulatedEvent>.FromPool();
				canBeMagneticallyManipulatedEvent.Object = Object;
				canBeMagneticallyManipulatedEvent.Allow = flag2;
				flag = Object.HandleEvent(canBeMagneticallyManipulatedEvent);
				flag2 = canBeMagneticallyManipulatedEvent.Allow;
			}
			return flag2;
		}

		// Token: 0x0400230D RID: 8973
		public GameObject Object;

		// Token: 0x0400230E RID: 8974
		public bool Allow;
	}
}
