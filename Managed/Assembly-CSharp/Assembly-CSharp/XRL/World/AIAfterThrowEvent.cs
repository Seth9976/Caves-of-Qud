using System;

namespace XRL.World
{
	// Token: 0x02000640 RID: 1600
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class AIAfterThrowEvent : PooledEvent<AIAfterThrowEvent>
	{
		// Token: 0x06004586 RID: 17798 RVA: 0x00163E31 File Offset: 0x00162031
		public override int GetCascadeLevel()
		{
			return AIAfterThrowEvent.CascadeLevel;
		}

		// Token: 0x06004587 RID: 17799 RVA: 0x00163E38 File Offset: 0x00162038
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004588 RID: 17800 RVA: 0x00163E41 File Offset: 0x00162041
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Target = null;
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x00163E60 File Offset: 0x00162060
		public static void Send(GameObject Object, GameObject Actor, GameObject Target)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("AIAfterThrow"))
			{
				Event @event = Event.New("AIAfterThrow", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AIAfterThrow"))
			{
				Event event2 = Event.New("AIAfterThrow", 0, 0, 0);
				event2.SetParameter("Object", Object);
				event2.SetParameter("Actor", Actor);
				event2.SetParameter("Target", Target);
				flag = Actor.FireEvent(event2);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<AIAfterThrowEvent>.ID, AIAfterThrowEvent.CascadeLevel))
			{
				AIAfterThrowEvent aiafterThrowEvent = PooledEvent<AIAfterThrowEvent>.FromPool();
				aiafterThrowEvent.Object = Object;
				aiafterThrowEvent.Actor = Actor;
				aiafterThrowEvent.Target = Target;
				flag = Actor.HandleEvent(aiafterThrowEvent);
			}
		}

		// Token: 0x04002102 RID: 8450
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002103 RID: 8451
		public GameObject Object;

		// Token: 0x04002104 RID: 8452
		public GameObject Actor;

		// Token: 0x04002105 RID: 8453
		public GameObject Target;
	}
}
