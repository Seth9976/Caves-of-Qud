using System;

namespace XRL.World
{
	// Token: 0x020006DD RID: 1757
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckOverburdenedOnStrengthUpdateEvent : PooledEvent<CheckOverburdenedOnStrengthUpdateEvent>
	{
		// Token: 0x06004A03 RID: 18947 RVA: 0x00173117 File Offset: 0x00171317
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x00173120 File Offset: 0x00171320
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x0017312F File Offset: 0x0017132F
		public static CheckOverburdenedOnStrengthUpdateEvent FromPool(GameObject Object)
		{
			CheckOverburdenedOnStrengthUpdateEvent checkOverburdenedOnStrengthUpdateEvent = PooledEvent<CheckOverburdenedOnStrengthUpdateEvent>.FromPool();
			checkOverburdenedOnStrengthUpdateEvent.Object = Object;
			return checkOverburdenedOnStrengthUpdateEvent;
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x00173140 File Offset: 0x00171340
		public static bool Check(GameObject Object)
		{
			if (GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CheckOverburdenedOnStrengthUpdate"))
			{
				Event @event = Event.New("CheckOverburdenedOnStrengthUpdate", 0, 0, 0);
				@event.SetParameter("Object", Object);
				if (!Object.FireEvent(@event))
				{
					return false;
				}
			}
			return !GameObject.Validate(ref Object) || !Object.WantEvent(PooledEvent<CheckOverburdenedOnStrengthUpdateEvent>.ID, MinEvent.CascadeLevel) || Object.HandleEvent(CheckOverburdenedOnStrengthUpdateEvent.FromPool(Object));
		}

		// Token: 0x04002390 RID: 9104
		public GameObject Object;
	}
}
