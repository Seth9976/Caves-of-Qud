using System;

namespace XRL.World
{
	// Token: 0x020006B5 RID: 1717
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanAcceptObjectEvent : PooledEvent<CanAcceptObjectEvent>
	{
		// Token: 0x060048F6 RID: 18678 RVA: 0x00170581 File Offset: 0x0016E781
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x0017058A File Offset: 0x0016E78A
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Holder = null;
			this.Container = null;
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x001705A7 File Offset: 0x0016E7A7
		public static bool Relevant(GameObject Container)
		{
			if (GameObject.Validate(ref Container))
			{
				if (Container.HasRegisteredEvent("CanAcceptObject"))
				{
					return true;
				}
				if (Container.WantEvent(PooledEvent<CanAcceptObjectEvent>.ID, MinEvent.CascadeLevel))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x001705D8 File Offset: 0x0016E7D8
		public static bool Check(GameObject Object, GameObject Holder, GameObject Container)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Container) && Container.HasRegisteredEvent("CanAcceptObject"))
			{
				Event @event = Event.New("CanAcceptObject", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Holder", Holder);
				@event.SetParameter("Container", Container);
				flag = Container.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Container) && Container.WantEvent(PooledEvent<CanAcceptObjectEvent>.ID, MinEvent.CascadeLevel))
			{
				CanAcceptObjectEvent canAcceptObjectEvent = PooledEvent<CanAcceptObjectEvent>.FromPool();
				canAcceptObjectEvent.Object = Object;
				canAcceptObjectEvent.Holder = Holder;
				canAcceptObjectEvent.Container = Container;
				flag = Container.HandleEvent(canAcceptObjectEvent);
			}
			return flag;
		}

		// Token: 0x04002301 RID: 8961
		public GameObject Object;

		// Token: 0x04002302 RID: 8962
		public GameObject Holder;

		// Token: 0x04002303 RID: 8963
		public GameObject Container;
	}
}
