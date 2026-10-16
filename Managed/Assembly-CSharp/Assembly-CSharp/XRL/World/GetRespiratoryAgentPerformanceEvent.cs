using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000783 RID: 1923
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetRespiratoryAgentPerformanceEvent : PooledEvent<GetRespiratoryAgentPerformanceEvent>
	{
		// Token: 0x06004ED2 RID: 20178 RVA: 0x00181847 File Offset: 0x0017FA47
		public override int GetCascadeLevel()
		{
			return GetRespiratoryAgentPerformanceEvent.CascadeLevel;
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x0018184E File Offset: 0x0017FA4E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x00181857 File Offset: 0x0017FA57
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.GasObject = null;
			this.Gas = null;
			this.BaseRating = 0;
			this.LinearAdjustment = 0;
			this.PercentageAdjustment = 0;
			this.WillAllowSave = false;
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x00181890 File Offset: 0x0017FA90
		public static int GetFor(GameObject Object, GameObject GasObject = null, Gas Gas = null, int? BaseRating = null, int LinearAdjustment = 0, int PercentageAdjustment = 0, bool WillAllowSave = false)
		{
			if (!GameObject.Validate(ref Object))
			{
				return 1;
			}
			if (Gas == null && GasObject != null)
			{
				Gas = GasObject.GetPart<Gas>();
			}
			int num = BaseRating ?? ((Gas != null) ? Gas.Density : 0);
			bool flag = true;
			if (flag)
			{
				bool flag2 = Object.HasRegisteredEvent("GetRespiratoryAgentPerformance");
				bool flag3 = GasObject != null && GasObject.HasRegisteredEvent("GetRespiratoryAgentPerformance");
				if (flag2 || flag3)
				{
					Event @event = Event.New("GetRespiratoryAgentPerformance", 0, 0, 0);
					@event.SetParameter("Object", Object);
					@event.SetParameter("GasObject", GasObject);
					@event.SetParameter("Gas", Gas);
					@event.SetParameter("BaseRating", num);
					@event.SetParameter("LinearAdjustment", LinearAdjustment);
					@event.SetParameter("PercentageAdjustment", PercentageAdjustment);
					@event.SetFlag("WillAllowSave", WillAllowSave);
					if (flag && flag2 && !Object.FireEvent(@event))
					{
						flag = false;
					}
					if (flag && flag3 && !GasObject.FireEvent(@event))
					{
						flag = false;
					}
					LinearAdjustment = @event.GetIntParameter("LinearAdjustment", 0);
					PercentageAdjustment = @event.GetIntParameter("PercentageAdjustment", 0);
				}
			}
			if (flag)
			{
				bool flag4 = Object.WantEvent(PooledEvent<GetRespiratoryAgentPerformanceEvent>.ID, GetRespiratoryAgentPerformanceEvent.CascadeLevel);
				bool flag5 = GasObject != null && GasObject.WantEvent(PooledEvent<GetRespiratoryAgentPerformanceEvent>.ID, GetRespiratoryAgentPerformanceEvent.CascadeLevel);
				if (flag4 || flag5)
				{
					GetRespiratoryAgentPerformanceEvent getRespiratoryAgentPerformanceEvent = PooledEvent<GetRespiratoryAgentPerformanceEvent>.FromPool();
					getRespiratoryAgentPerformanceEvent.Object = Object;
					getRespiratoryAgentPerformanceEvent.GasObject = GasObject;
					getRespiratoryAgentPerformanceEvent.Gas = Gas;
					getRespiratoryAgentPerformanceEvent.BaseRating = num;
					getRespiratoryAgentPerformanceEvent.LinearAdjustment = LinearAdjustment;
					getRespiratoryAgentPerformanceEvent.PercentageAdjustment = PercentageAdjustment;
					getRespiratoryAgentPerformanceEvent.WillAllowSave = WillAllowSave;
					if (flag && flag4 && !Object.HandleEvent(getRespiratoryAgentPerformanceEvent))
					{
						flag = false;
					}
					if (!flag || !flag5 || !GasObject.HandleEvent(getRespiratoryAgentPerformanceEvent))
					{
					}
					LinearAdjustment = getRespiratoryAgentPerformanceEvent.LinearAdjustment;
					PercentageAdjustment = getRespiratoryAgentPerformanceEvent.PercentageAdjustment;
				}
			}
			int num2 = num;
			if (LinearAdjustment != 0)
			{
				num2 += LinearAdjustment;
			}
			if (PercentageAdjustment != 0)
			{
				num2 = num2 * (100 + PercentageAdjustment) / 100;
			}
			return num2;
		}

		// Token: 0x04002688 RID: 9864
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002689 RID: 9865
		public GameObject Object;

		// Token: 0x0400268A RID: 9866
		public GameObject GasObject;

		// Token: 0x0400268B RID: 9867
		public Gas Gas;

		// Token: 0x0400268C RID: 9868
		public int BaseRating;

		// Token: 0x0400268D RID: 9869
		public int LinearAdjustment;

		// Token: 0x0400268E RID: 9870
		public int PercentageAdjustment;

		// Token: 0x0400268F RID: 9871
		public bool WillAllowSave;
	}
}
