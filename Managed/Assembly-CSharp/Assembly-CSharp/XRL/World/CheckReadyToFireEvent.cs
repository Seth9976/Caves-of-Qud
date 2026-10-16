using System;

namespace XRL.World
{
	// Token: 0x020006DF RID: 1759
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckReadyToFireEvent : PooledEvent<CheckReadyToFireEvent>
	{
		// Token: 0x06004A0D RID: 18957 RVA: 0x00173219 File Offset: 0x00171419
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x00173222 File Offset: 0x00171422
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x00173234 File Offset: 0x00171434
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CheckReadyToFire"))
			{
				Event @event = Event.New("CheckReadyToFire", 0, 0, 0);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CheckReadyToFireEvent>.ID, MinEvent.CascadeLevel))
			{
				CheckReadyToFireEvent checkReadyToFireEvent = PooledEvent<CheckReadyToFireEvent>.FromPool();
				checkReadyToFireEvent.Object = Object;
				flag = Object.HandleEvent(checkReadyToFireEvent);
			}
			return flag;
		}

		// Token: 0x04002392 RID: 9106
		public GameObject Object;
	}
}
