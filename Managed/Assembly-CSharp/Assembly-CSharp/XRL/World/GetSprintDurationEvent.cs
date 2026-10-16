using System;

namespace XRL.World
{
	// Token: 0x02000792 RID: 1938
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetSprintDurationEvent : PooledEvent<GetSprintDurationEvent>
	{
		// Token: 0x06004F37 RID: 20279 RVA: 0x00182E5C File Offset: 0x0018105C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x00182E65 File Offset: 0x00181065
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Base = 0;
			this.PercentageIncrease = 0;
			this.LinearIncrease = 0;
			this.PercentageReduction = 0;
			this.LinearReduction = 0;
			this.Stats = null;
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x00182E9E File Offset: 0x0018109E
		public static GetSprintDurationEvent FromPool(GameObject Object, int Base, int PercentageIncrease = 0, int LinearIncrease = 0, int PercentageReduction = 0, int LinearReduction = 0, Templates.StatCollector Stats = null)
		{
			GetSprintDurationEvent getSprintDurationEvent = PooledEvent<GetSprintDurationEvent>.FromPool();
			getSprintDurationEvent.Object = Object;
			getSprintDurationEvent.Base = Base;
			getSprintDurationEvent.PercentageIncrease = PercentageIncrease;
			getSprintDurationEvent.LinearIncrease = LinearIncrease;
			getSprintDurationEvent.PercentageReduction = PercentageReduction;
			getSprintDurationEvent.LinearReduction = LinearReduction;
			getSprintDurationEvent.Stats = Stats;
			return getSprintDurationEvent;
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x00182EDC File Offset: 0x001810DC
		public static int GetFor(GameObject Object, int Base = 0, int PercentageIncrease = 0, int LinearIncrease = 0, int PercentageReduction = 0, int LinearReduction = 0, Templates.StatCollector Stats = null)
		{
			int num;
			if (Object != null && Object.WantEvent(PooledEvent<GetSprintDurationEvent>.ID, MinEvent.CascadeLevel))
			{
				GetSprintDurationEvent getSprintDurationEvent = GetSprintDurationEvent.FromPool(Object, Base, PercentageIncrease, LinearIncrease, PercentageReduction, LinearReduction, Stats);
				Object.HandleEvent(getSprintDurationEvent);
				num = (Base + getSprintDurationEvent.LinearIncrease) * (100 + getSprintDurationEvent.PercentageIncrease) * (100 - getSprintDurationEvent.PercentageReduction) / 10000 - getSprintDurationEvent.LinearReduction;
				if (Stats != null)
				{
					Stats.CollectBonusModifiers("Duration", Base, null, true);
				}
			}
			else
			{
				num = (Base + LinearIncrease) * (100 + PercentageIncrease) * (100 - PercentageReduction) / 10000 - LinearReduction;
				if (Stats != null)
				{
					Stats.CollectBonusModifiers("Duration", Base, null, true);
				}
			}
			return Math.Max(num, 0);
		}

		// Token: 0x040026EA RID: 9962
		public GameObject Object;

		// Token: 0x040026EB RID: 9963
		public int Base;

		// Token: 0x040026EC RID: 9964
		public int PercentageIncrease;

		// Token: 0x040026ED RID: 9965
		public int LinearIncrease;

		// Token: 0x040026EE RID: 9966
		public int PercentageReduction;

		// Token: 0x040026EF RID: 9967
		public int LinearReduction;

		// Token: 0x040026F0 RID: 9968
		public Templates.StatCollector Stats;
	}
}
