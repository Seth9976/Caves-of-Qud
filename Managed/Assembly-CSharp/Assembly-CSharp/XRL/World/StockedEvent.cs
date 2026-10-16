using System;

namespace XRL.World
{
	// Token: 0x0200085E RID: 2142
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class StockedEvent : PooledEvent<StockedEvent>
	{
		// Token: 0x06005561 RID: 21857 RVA: 0x0019877A File Offset: 0x0019697A
		public override int GetCascadeLevel()
		{
			return StockedEvent.CascadeLevel;
		}

		// Token: 0x06005562 RID: 21858 RVA: 0x00198781 File Offset: 0x00196981
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x0019878A File Offset: 0x0019698A
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Context = null;
		}

		// Token: 0x06005564 RID: 21860 RVA: 0x001987A0 File Offset: 0x001969A0
		public static void Send(GameObject Object, string Context = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("Stocked"))
			{
				Event @event = Event.New("Stocked", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Context", Context);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<StockedEvent>.ID, StockedEvent.CascadeLevel))
			{
				StockedEvent stockedEvent = PooledEvent<StockedEvent>.FromPool();
				stockedEvent.Object = Object;
				stockedEvent.Context = Context;
				flag = Object.HandleEvent(stockedEvent);
			}
		}

		// Token: 0x04002B09 RID: 11017
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002B0A RID: 11018
		public GameObject Object;

		// Token: 0x04002B0B RID: 11019
		public string Context;
	}
}
