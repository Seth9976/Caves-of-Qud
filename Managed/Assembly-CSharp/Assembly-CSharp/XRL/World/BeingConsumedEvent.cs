using System;

namespace XRL.World
{
	// Token: 0x020006AD RID: 1709
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeingConsumedEvent : PooledEvent<BeingConsumedEvent>
	{
		// Token: 0x060048B5 RID: 18613 RVA: 0x0016D27E File Offset: 0x0016B47E
		public override int GetCascadeLevel()
		{
			return BeingConsumedEvent.CascadeLevel;
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x0016D285 File Offset: 0x0016B485
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x0016D28E File Offset: 0x0016B48E
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x0016D2A4 File Offset: 0x0016B4A4
		public static void Send(GameObject Actor, GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("BeingConsumed"))
			{
				Event @event = Event.New("BeingConsumed", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<BeingConsumedEvent>.ID, BeingConsumedEvent.CascadeLevel))
			{
				BeingConsumedEvent beingConsumedEvent = PooledEvent<BeingConsumedEvent>.FromPool();
				beingConsumedEvent.Actor = Actor;
				beingConsumedEvent.Object = Object;
				flag = Object.HandleEvent(beingConsumedEvent);
			}
		}

		// Token: 0x040022D9 RID: 8921
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040022DA RID: 8922
		public GameObject Actor;

		// Token: 0x040022DB RID: 8923
		public GameObject Object;
	}
}
