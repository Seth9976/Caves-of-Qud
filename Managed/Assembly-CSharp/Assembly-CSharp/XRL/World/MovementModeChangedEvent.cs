using System;

namespace XRL.World
{
	// Token: 0x0200080B RID: 2059
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class MovementModeChangedEvent : PooledEvent<MovementModeChangedEvent>
	{
		// Token: 0x06005243 RID: 21059 RVA: 0x0018C32F File Offset: 0x0018A52F
		public override int GetCascadeLevel()
		{
			return MovementModeChangedEvent.CascadeLevel;
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x0018C336 File Offset: 0x0018A536
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x0018C33F File Offset: 0x0018A53F
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.To = null;
			this.Involuntary = false;
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x0018C35C File Offset: 0x0018A55C
		public static void Send(GameObject Object, string To = null, bool Involuntary = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("MovementModeChanged"))
			{
				Event @event = Event.New("MovementModeChanged", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("To", To);
				@event.SetFlag("Involuntary", Involuntary);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<MovementModeChangedEvent>.ID, MovementModeChangedEvent.CascadeLevel))
			{
				MovementModeChangedEvent movementModeChangedEvent = PooledEvent<MovementModeChangedEvent>.FromPool();
				movementModeChangedEvent.Object = Object;
				movementModeChangedEvent.To = To;
				movementModeChangedEvent.Involuntary = Involuntary;
				flag = Object.HandleEvent(movementModeChangedEvent);
			}
		}

		// Token: 0x04002955 RID: 10581
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002956 RID: 10582
		public GameObject Object;

		// Token: 0x04002957 RID: 10583
		public string To;

		// Token: 0x04002958 RID: 10584
		public bool Involuntary;
	}
}
