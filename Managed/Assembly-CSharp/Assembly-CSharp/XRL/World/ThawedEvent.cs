using System;

namespace XRL.World
{
	// Token: 0x02000868 RID: 2152
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ThawedEvent : PooledEvent<ThawedEvent>
	{
		// Token: 0x0600559E RID: 21918 RVA: 0x00198F10 File Offset: 0x00197110
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600559F RID: 21919 RVA: 0x00198F19 File Offset: 0x00197119
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.By = null;
		}

		// Token: 0x060055A0 RID: 21920 RVA: 0x00198F30 File Offset: 0x00197130
		public static void Send(GameObject Object, GameObject By)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("Thawed"))
			{
				Event @event = Event.New("Thawed", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("By", By);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<ThawedEvent>.ID, MinEvent.CascadeLevel))
			{
				ThawedEvent thawedEvent = PooledEvent<ThawedEvent>.FromPool();
				thawedEvent.Object = Object;
				thawedEvent.By = By;
				flag = Object.HandleEvent(thawedEvent);
			}
		}

		// Token: 0x04002B24 RID: 11044
		public GameObject Object;

		// Token: 0x04002B25 RID: 11045
		public GameObject By;
	}
}
