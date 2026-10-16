using System;

namespace XRL.World
{
	// Token: 0x020007EC RID: 2028
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class IsAdaptivePenetrationActiveEvent : PooledEvent<IsAdaptivePenetrationActiveEvent>
	{
		// Token: 0x06005179 RID: 20857 RVA: 0x00189BE4 File Offset: 0x00187DE4
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600517A RID: 20858 RVA: 0x00189BED File Offset: 0x00187DED
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Bonus = 0;
			this.Symbol = null;
			this.Active = false;
		}

		// Token: 0x0600517B RID: 20859 RVA: 0x00189C14 File Offset: 0x00187E14
		public static bool Check(GameObject Object, ref int Bonus)
		{
			string text = "";
			return IsAdaptivePenetrationActiveEvent.Check(Object, ref Bonus, ref text);
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x00189C30 File Offset: 0x00187E30
		public static bool Check(GameObject Object, ref int Bonus, ref string Symbol)
		{
			bool flag = true;
			bool flag2 = false;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("IsAdaptivePenetrationActive"))
			{
				Event @event = Event.New("IsAdaptivePenetrationActive", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Bonus", Bonus);
				@event.SetParameter("Symbol", Symbol);
				@event.SetParameter("Active", flag2);
				flag = Object.FireEvent(@event);
				Bonus = @event.GetIntParameter("Bonus", 0);
				Symbol = @event.GetStringParameter("Symbol", null);
				flag2 = @event.HasFlag("Active");
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<IsAdaptivePenetrationActiveEvent>.ID, MinEvent.CascadeLevel))
			{
				IsAdaptivePenetrationActiveEvent isAdaptivePenetrationActiveEvent = PooledEvent<IsAdaptivePenetrationActiveEvent>.FromPool();
				isAdaptivePenetrationActiveEvent.Object = Object;
				isAdaptivePenetrationActiveEvent.Bonus = Bonus;
				isAdaptivePenetrationActiveEvent.Symbol = Symbol;
				isAdaptivePenetrationActiveEvent.Active = flag2;
				flag = Object.HandleEvent(isAdaptivePenetrationActiveEvent);
				Bonus = isAdaptivePenetrationActiveEvent.Bonus;
				Symbol = isAdaptivePenetrationActiveEvent.Symbol;
				flag2 = isAdaptivePenetrationActiveEvent.Active;
			}
			return flag2;
		}

		// Token: 0x040028D9 RID: 10457
		public GameObject Object;

		// Token: 0x040028DA RID: 10458
		public int Bonus;

		// Token: 0x040028DB RID: 10459
		public string Symbol;

		// Token: 0x040028DC RID: 10460
		public bool Active;
	}
}
