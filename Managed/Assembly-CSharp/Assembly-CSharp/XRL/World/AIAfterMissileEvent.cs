using System;

namespace XRL.World
{
	// Token: 0x0200063F RID: 1599
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class AIAfterMissileEvent : PooledEvent<AIAfterMissileEvent>
	{
		// Token: 0x06004580 RID: 17792 RVA: 0x00163CF6 File Offset: 0x00161EF6
		public override int GetCascadeLevel()
		{
			return AIAfterMissileEvent.CascadeLevel;
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x00163CFD File Offset: 0x00161EFD
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x00163D06 File Offset: 0x00161F06
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Target = null;
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x00163D24 File Offset: 0x00161F24
		public static void Send(GameObject Object, GameObject Actor, GameObject Target)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("AIAfterMissile"))
			{
				Event @event = Event.New("AIAfterMissile", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Actor", Actor);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIAfterMissile"))
			{
				Event event2 = Event.New("AIAfterMissile", 0, 0, 0);
				event2.SetParameter("Object", Object);
				event2.SetParameter("Actor", Actor);
				event2.SetParameter("Target", Target);
				flag = Actor.FireEvent(event2);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<AIAfterMissileEvent>.ID, AIAfterMissileEvent.CascadeLevel))
			{
				AIAfterMissileEvent aiafterMissileEvent = PooledEvent<AIAfterMissileEvent>.FromPool();
				aiafterMissileEvent.Object = Object;
				aiafterMissileEvent.Actor = Actor;
				aiafterMissileEvent.Target = Target;
				flag = Actor.HandleEvent(aiafterMissileEvent);
			}
		}

		// Token: 0x040020FE RID: 8446
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040020FF RID: 8447
		public GameObject Object;

		// Token: 0x04002100 RID: 8448
		public GameObject Actor;

		// Token: 0x04002101 RID: 8449
		public GameObject Target;
	}
}
