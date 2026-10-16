using System;

namespace XRL.World
{
	// Token: 0x020007F3 RID: 2035
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class IsRootedInPlaceEvent : PooledEvent<IsRootedInPlaceEvent>
	{
		// Token: 0x0600519B RID: 20891 RVA: 0x0018A25D File Offset: 0x0018845D
		public override int GetCascadeLevel()
		{
			return IsRootedInPlaceEvent.CascadeLevel;
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x0018A264 File Offset: 0x00188464
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x0018A26D File Offset: 0x0018846D
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x0018A27C File Offset: 0x0018847C
		public static IsRootedInPlaceEvent FromPool(GameObject Object)
		{
			IsRootedInPlaceEvent isRootedInPlaceEvent = PooledEvent<IsRootedInPlaceEvent>.FromPool();
			isRootedInPlaceEvent.Object = Object;
			return isRootedInPlaceEvent;
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x0018A28C File Offset: 0x0018848C
		public static bool Check(GameObject Object)
		{
			if (GameObject.Validate(ref Object) && Object.HasRegisteredEvent("IsRootedInPlace"))
			{
				Event @event = Event.New("IsRootedInPlace", 0, 0, 0);
				@event.SetParameter("Object", Object);
				if (!Object.FireEvent(@event))
				{
					return true;
				}
			}
			return GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<IsRootedInPlaceEvent>.ID, IsRootedInPlaceEvent.CascadeLevel) && !Object.HandleEvent(IsRootedInPlaceEvent.FromPool(Object));
		}

		// Token: 0x040028EE RID: 10478
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040028EF RID: 10479
		public GameObject Object;
	}
}
