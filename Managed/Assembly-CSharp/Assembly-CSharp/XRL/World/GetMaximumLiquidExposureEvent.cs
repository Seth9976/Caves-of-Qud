using System;

namespace XRL.World
{
	// Token: 0x02000763 RID: 1891
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetMaximumLiquidExposureEvent : PooledEvent<GetMaximumLiquidExposureEvent>
	{
		// Token: 0x06004DEF RID: 19951 RVA: 0x0017E540 File Offset: 0x0017C740
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x0017E549 File Offset: 0x0017C749
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Base = 0;
			this.PercentageIncrease = 0;
			this.LinearIncrease = 0;
			this.PercentageReduction = 0;
			this.LinearReduction = 0;
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x0017E57B File Offset: 0x0017C77B
		public static GetMaximumLiquidExposureEvent FromPool(GameObject Object, int Base, int PercentageIncrease = 0, int LinearIncrease = 0, int PercentageReduction = 0, int LinearReduction = 0)
		{
			GetMaximumLiquidExposureEvent getMaximumLiquidExposureEvent = PooledEvent<GetMaximumLiquidExposureEvent>.FromPool();
			getMaximumLiquidExposureEvent.Object = Object;
			getMaximumLiquidExposureEvent.Base = Base;
			getMaximumLiquidExposureEvent.PercentageIncrease = PercentageIncrease;
			getMaximumLiquidExposureEvent.LinearIncrease = LinearIncrease;
			getMaximumLiquidExposureEvent.PercentageReduction = PercentageReduction;
			getMaximumLiquidExposureEvent.LinearReduction = LinearReduction;
			return getMaximumLiquidExposureEvent;
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x0017E5B0 File Offset: 0x0017C7B0
		public static int GetFor(GameObject Object, int PercentageIncrease = 0, int LinearIncrease = 0, int PercentageReduction = 0, int LinearReduction = 0)
		{
			int @base = GetMaximumLiquidExposureEvent.GetBase(Object);
			int num;
			if (Object != null && Object.WantEvent(PooledEvent<GetMaximumLiquidExposureEvent>.ID, MinEvent.CascadeLevel))
			{
				GetMaximumLiquidExposureEvent getMaximumLiquidExposureEvent = GetMaximumLiquidExposureEvent.FromPool(Object, @base, PercentageIncrease, LinearIncrease, PercentageReduction, LinearReduction);
				Object.HandleEvent(getMaximumLiquidExposureEvent);
				num = (int)Math.Round((double)(@base + getMaximumLiquidExposureEvent.LinearIncrease) * (100.0 + (double)getMaximumLiquidExposureEvent.PercentageIncrease) * (100.0 - (double)getMaximumLiquidExposureEvent.PercentageReduction) / 10000.0) - getMaximumLiquidExposureEvent.LinearReduction;
			}
			else
			{
				num = (int)Math.Round((double)(@base + LinearIncrease) * (100.0 + (double)PercentageIncrease) * (100.0 - (double)PercentageReduction) / 10000.0) - LinearReduction;
			}
			return Math.Max(num, 0);
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x0017E670 File Offset: 0x0017C870
		public static double GetDoubleFor(GameObject Object, int PercentageIncrease = 0, int LinearIncrease = 0, int PercentageReduction = 0, int LinearReduction = 0)
		{
			double doubleBase = GetMaximumLiquidExposureEvent.GetDoubleBase(Object);
			double num;
			if (Object != null && Object.WantEvent(PooledEvent<GetMaximumLiquidExposureEvent>.ID, MinEvent.CascadeLevel))
			{
				GetMaximumLiquidExposureEvent getMaximumLiquidExposureEvent = GetMaximumLiquidExposureEvent.FromPool(Object, (int)Math.Round(doubleBase), PercentageIncrease, LinearIncrease, PercentageReduction, LinearReduction);
				Object.HandleEvent(getMaximumLiquidExposureEvent);
				num = (doubleBase + (double)getMaximumLiquidExposureEvent.LinearIncrease) * (double)(100 + getMaximumLiquidExposureEvent.PercentageIncrease) * (double)(100 - getMaximumLiquidExposureEvent.PercentageReduction) / 10000.0 - (double)getMaximumLiquidExposureEvent.LinearReduction;
			}
			else
			{
				num = (doubleBase + (double)LinearIncrease) * (double)(100 + PercentageIncrease) * (double)(100 - PercentageReduction) / 10000.0 - (double)LinearReduction;
			}
			return Math.Max(num, 0.0);
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x0017E718 File Offset: 0x0017C918
		public static int GetBase(GameObject obj)
		{
			if (obj == null)
			{
				return 0;
			}
			if (obj.HasTag("Creature"))
			{
				return Math.Max(obj.Stat("Strength", 0) + obj.Stat("Toughness", 0) + obj.GetConcreteBodyPartCount(), 1);
			}
			if (obj.HasTag("ExcavatoryTerrainFeature"))
			{
				return 0;
			}
			return (int)Math.Round(Math.Max(obj.GetIntrinsicWeight().DiminishingReturns(1.0), 1.0));
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x0017E798 File Offset: 0x0017C998
		public static double GetDoubleBase(GameObject obj)
		{
			if (obj == null)
			{
				return 0.0;
			}
			if (obj.HasTag("Creature"))
			{
				return (double)Math.Max(obj.Stat("Strength", 0) + obj.Stat("Toughness", 0) + obj.GetConcreteBodyPartCount(), 1);
			}
			if (obj.HasTag("ExcavatoryTerrainFeature"))
			{
				return 0.0;
			}
			return Math.Max(obj.GetIntrinsicWeight().DiminishingReturns(1.0), 1.0);
		}

		// Token: 0x040025E3 RID: 9699
		public GameObject Object;

		// Token: 0x040025E4 RID: 9700
		public int Base;

		// Token: 0x040025E5 RID: 9701
		public int PercentageIncrease;

		// Token: 0x040025E6 RID: 9702
		public int LinearIncrease;

		// Token: 0x040025E7 RID: 9703
		public int PercentageReduction;

		// Token: 0x040025E8 RID: 9704
		public int LinearReduction;
	}
}
