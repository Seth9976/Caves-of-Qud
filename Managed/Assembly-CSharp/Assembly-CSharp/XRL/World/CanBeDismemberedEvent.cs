using System;

namespace XRL.World
{
	// Token: 0x020006B7 RID: 1719
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class CanBeDismemberedEvent : PooledEvent<CanBeDismemberedEvent>
	{
		// Token: 0x06004905 RID: 18693 RVA: 0x001707BB File Offset: 0x0016E9BB
		public override int GetCascadeLevel()
		{
			return CanBeDismemberedEvent.CascadeLevel;
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x001707C2 File Offset: 0x0016E9C2
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x001707CB File Offset: 0x0016E9CB
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Attributes = null;
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x001707E4 File Offset: 0x0016E9E4
		public static bool Check(GameObject Object, string Attributes = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CanBeDismembered"))
			{
				Event @event = Event.New("CanBeDismembered", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Attributes", Attributes);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CanBeDismemberedEvent>.ID, CanBeDismemberedEvent.CascadeLevel))
			{
				CanBeDismemberedEvent canBeDismemberedEvent = PooledEvent<CanBeDismemberedEvent>.FromPool();
				canBeDismemberedEvent.Object = Object;
				canBeDismemberedEvent.Attributes = Attributes;
				flag = Object.HandleEvent(canBeDismemberedEvent);
			}
			return flag;
		}

		// Token: 0x04002308 RID: 8968
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002309 RID: 8969
		public GameObject Object;

		// Token: 0x0400230A RID: 8970
		public string Attributes;
	}
}
