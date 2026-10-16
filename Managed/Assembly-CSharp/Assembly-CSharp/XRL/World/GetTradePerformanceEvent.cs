using System;

namespace XRL.World
{
	// Token: 0x0200079E RID: 1950
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetTradePerformanceEvent : PooledEvent<GetTradePerformanceEvent>
	{
		// Token: 0x06004F82 RID: 20354 RVA: 0x001844B3 File Offset: 0x001826B3
		public override int GetCascadeLevel()
		{
			return GetTradePerformanceEvent.CascadeLevel;
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x001844BA File Offset: 0x001826BA
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x001844C3 File Offset: 0x001826C3
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Trader = null;
			this.BaseRating = 0;
			this.LinearAdjustment = 0.0;
			this.FactorAdjustment = 0.0;
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x001844FE File Offset: 0x001826FE
		public static GetTradePerformanceEvent FromPool(GameObject Actor, GameObject Trader, int BaseRating, double LinearAdjustment = 0.0, double FactorAdjustment = 1.0)
		{
			GetTradePerformanceEvent getTradePerformanceEvent = PooledEvent<GetTradePerformanceEvent>.FromPool();
			getTradePerformanceEvent.Actor = Actor;
			getTradePerformanceEvent.Trader = Trader;
			getTradePerformanceEvent.BaseRating = BaseRating;
			getTradePerformanceEvent.LinearAdjustment = LinearAdjustment;
			getTradePerformanceEvent.FactorAdjustment = FactorAdjustment;
			return getTradePerformanceEvent;
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x0018452C File Offset: 0x0018272C
		public static double GetFor(GameObject Actor, GameObject Trader)
		{
			if (Trader == null || Actor == null)
			{
				return 1.0;
			}
			if (!Actor.HasStat("Ego"))
			{
				return 0.25;
			}
			int num = Actor.StatMod("Ego", 0);
			double num2 = 0.0;
			double num3 = 1.0;
			bool flag = true;
			if (flag && Actor.HasRegisteredEvent("GetTradePerformance"))
			{
				Event @event = Event.New("GetTradePerformance", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Trader", Trader);
				@event.SetParameter("BaseRating", num);
				@event.SetParameter("LinearAdjustment", num2);
				@event.SetParameter("FactorAdjustment", num3);
				if (!Actor.FireEvent(@event))
				{
					flag = false;
				}
				num2 = (double)@event.GetParameter("LinearAdjustment");
				num3 = (double)@event.GetParameter("FactorAdjustment");
			}
			if (flag && Actor.WantEvent(PooledEvent<GetTradePerformanceEvent>.ID, GetTradePerformanceEvent.CascadeLevel))
			{
				GetTradePerformanceEvent getTradePerformanceEvent = GetTradePerformanceEvent.FromPool(Actor, Trader, num, num2, num3);
				if (!Actor.HandleEvent(getTradePerformanceEvent))
				{
				}
				num2 = getTradePerformanceEvent.LinearAdjustment;
				num3 = getTradePerformanceEvent.FactorAdjustment;
			}
			return Math.Min(Math.Max((0.35 + 0.07 * ((double)num + num2)) * num3, 0.05), 0.95);
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x0018469C File Offset: 0x0018289C
		public static int GetRatingFor(GameObject Actor, GameObject Trader)
		{
			if (Trader == null || Actor == null)
			{
				return 0;
			}
			if (!Actor.HasStat("Ego"))
			{
				return -2;
			}
			int num = Actor.StatMod("Ego", 0);
			double num2 = 0.0;
			double num3 = 1.0;
			bool flag = true;
			if (flag && Actor.HasRegisteredEvent("GetTradePerformance"))
			{
				Event @event = Event.New("GetTradePerformance", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Trader", Trader);
				@event.SetParameter("BaseRating", num);
				@event.SetParameter("LinearAdjustment", num2);
				@event.SetParameter("FactorAdjustment", num3);
				if (!Actor.FireEvent(@event))
				{
					flag = false;
				}
				num2 = (double)@event.GetParameter("LinearAdjustment");
				num3 = (double)@event.GetParameter("FactorAdjustment");
			}
			if (flag && Actor.WantEvent(PooledEvent<GetTradePerformanceEvent>.ID, GetTradePerformanceEvent.CascadeLevel))
			{
				GetTradePerformanceEvent getTradePerformanceEvent = GetTradePerformanceEvent.FromPool(Actor, Trader, num, num2, num3);
				if (!Actor.HandleEvent(getTradePerformanceEvent))
				{
				}
				num2 = getTradePerformanceEvent.LinearAdjustment;
				num3 = getTradePerformanceEvent.FactorAdjustment;
			}
			return (int)(((double)num + num2) * num3);
		}

		// Token: 0x04002741 RID: 10049
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002742 RID: 10050
		public GameObject Actor;

		// Token: 0x04002743 RID: 10051
		public GameObject Trader;

		// Token: 0x04002744 RID: 10052
		public int BaseRating;

		// Token: 0x04002745 RID: 10053
		public double LinearAdjustment;

		// Token: 0x04002746 RID: 10054
		public double FactorAdjustment;
	}
}
