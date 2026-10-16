using System;

namespace XRL.World
{
	// Token: 0x0200080D RID: 2061
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class NeedPartSupportEvent : PooledEvent<NeedPartSupportEvent>
	{
		// Token: 0x0600524D RID: 21069 RVA: 0x0018C4B6 File Offset: 0x0018A6B6
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x0018C4BF File Offset: 0x0018A6BF
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Type = null;
			this.Skip = null;
		}

		// Token: 0x0600524F RID: 21071 RVA: 0x0018C4DC File Offset: 0x0018A6DC
		public static NeedPartSupportEvent FromPool(GameObject Actor, string Type, IPart Skip = null)
		{
			NeedPartSupportEvent needPartSupportEvent = PooledEvent<NeedPartSupportEvent>.FromPool();
			needPartSupportEvent.Actor = Actor;
			needPartSupportEvent.Type = Type;
			needPartSupportEvent.Skip = Skip;
			return needPartSupportEvent;
		}

		// Token: 0x06005250 RID: 21072 RVA: 0x0018C4F8 File Offset: 0x0018A6F8
		public static void Send(GameObject Actor, string Type, IPart Skip = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(Actor) && Actor.HasRegisteredEvent("NeedPartSupport"))
			{
				Event @event = Event.New("NeedPartSupport", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Type", Type);
				@event.SetParameter("Skip", Skip);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<NeedPartSupportEvent>.ID, MinEvent.CascadeLevel))
			{
				flag = Actor.HandleEvent(NeedPartSupportEvent.FromPool(Actor, Type, Skip));
			}
		}

		// Token: 0x0400295A RID: 10586
		public GameObject Actor;

		// Token: 0x0400295B RID: 10587
		public string Type;

		// Token: 0x0400295C RID: 10588
		public IPart Skip;
	}
}
