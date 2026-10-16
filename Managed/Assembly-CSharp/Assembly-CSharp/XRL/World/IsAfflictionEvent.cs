using System;

namespace XRL.World
{
	// Token: 0x020007ED RID: 2029
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class IsAfflictionEvent : PooledEvent<IsAfflictionEvent>
	{
		// Token: 0x0600517E RID: 20862 RVA: 0x00189D43 File Offset: 0x00187F43
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x00189D4C File Offset: 0x00187F4C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x00189D5C File Offset: 0x00187F5C
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("IsAffliction"))
			{
				Event @event = Event.New("IsAffliction", 0, 0, 0);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<IsAfflictionEvent>.ID, MinEvent.CascadeLevel))
			{
				IsAfflictionEvent isAfflictionEvent = PooledEvent<IsAfflictionEvent>.FromPool();
				isAfflictionEvent.Object = Object;
				flag = Object.HandleEvent(isAfflictionEvent);
			}
			return !flag;
		}

		// Token: 0x040028DD RID: 10461
		public GameObject Object;
	}
}
