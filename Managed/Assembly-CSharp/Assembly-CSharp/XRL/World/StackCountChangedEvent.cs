using System;

namespace XRL.World
{
	// Token: 0x0200085B RID: 2139
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class StackCountChangedEvent : PooledEvent<StackCountChangedEvent>
	{
		// Token: 0x06005553 RID: 21843 RVA: 0x00198516 File Offset: 0x00196716
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x0019851F File Offset: 0x0019671F
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.OldValue = 0;
			this.NewValue = 0;
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x0019853C File Offset: 0x0019673C
		public static void Send(GameObject Object, int OldValue, int NewValue)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("StackCountChanged"))
			{
				Event @event = Event.New("StackCountChanged", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("OldValue", OldValue);
				@event.SetParameter("NewValue", NewValue);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<StackCountChangedEvent>.ID, MinEvent.CascadeLevel))
			{
				StackCountChangedEvent stackCountChangedEvent = PooledEvent<StackCountChangedEvent>.FromPool();
				stackCountChangedEvent.Object = Object;
				stackCountChangedEvent.OldValue = OldValue;
				stackCountChangedEvent.NewValue = NewValue;
				flag = Object.HandleEvent(stackCountChangedEvent);
			}
		}

		// Token: 0x04002AF5 RID: 10997
		public GameObject Object;

		// Token: 0x04002AF6 RID: 10998
		public int OldValue;

		// Token: 0x04002AF7 RID: 10999
		public int NewValue;
	}
}
