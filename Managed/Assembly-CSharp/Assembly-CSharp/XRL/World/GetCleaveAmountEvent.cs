using System;

namespace XRL.World
{
	// Token: 0x02000732 RID: 1842
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetCleaveAmountEvent : PooledEvent<GetCleaveAmountEvent>
	{
		// Token: 0x06004C5A RID: 19546 RVA: 0x00179880 File Offset: 0x00177A80
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x00179889 File Offset: 0x00177A89
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Target = null;
			this.BaseAmount = 0;
			this.Amount = 0;
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x001798B4 File Offset: 0x00177AB4
		public static int GetFor(GameObject Object, GameObject Actor, GameObject Target, int BaseAmount = 1)
		{
			bool flag = true;
			int num = BaseAmount;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetCleaveAmount"))
			{
				Event @event = Event.New("GetCleaveAmount", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("BaseAmount", BaseAmount);
				@event.SetParameter("Amount", num);
				flag = Object.FireEvent(@event);
				num = @event.GetIntParameter("Amount", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetCleaveAmountEvent>.ID, MinEvent.CascadeLevel))
			{
				GetCleaveAmountEvent getCleaveAmountEvent = PooledEvent<GetCleaveAmountEvent>.FromPool();
				getCleaveAmountEvent.Object = Object;
				getCleaveAmountEvent.Actor = Actor;
				getCleaveAmountEvent.Target = Target;
				getCleaveAmountEvent.BaseAmount = BaseAmount;
				getCleaveAmountEvent.Amount = num;
				flag = Object.HandleEvent(getCleaveAmountEvent);
				num = getCleaveAmountEvent.Amount;
			}
			return num;
		}

		// Token: 0x040024F0 RID: 9456
		public GameObject Object;

		// Token: 0x040024F1 RID: 9457
		public GameObject Actor;

		// Token: 0x040024F2 RID: 9458
		public GameObject Target;

		// Token: 0x040024F3 RID: 9459
		public int BaseAmount;

		// Token: 0x040024F4 RID: 9460
		public int Amount;
	}
}
