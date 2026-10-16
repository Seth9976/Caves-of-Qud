using System;

namespace XRL.World
{
	// Token: 0x02000791 RID: 1937
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetSpringinessEvent : PooledEvent<GetSpringinessEvent>
	{
		// Token: 0x06004F32 RID: 20274 RVA: 0x00182D51 File Offset: 0x00180F51
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x00182D5A File Offset: 0x00180F5A
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

		// Token: 0x06004F34 RID: 20276 RVA: 0x00182D8C File Offset: 0x00180F8C
		public static GetSpringinessEvent FromPool(GameObject Object, int Base, int PercentageIncrease = 0, int LinearIncrease = 0, int PercentageReduction = 0, int LinearReduction = 0)
		{
			GetSpringinessEvent getSpringinessEvent = PooledEvent<GetSpringinessEvent>.FromPool();
			getSpringinessEvent.Object = Object;
			getSpringinessEvent.Base = Base;
			getSpringinessEvent.PercentageIncrease = PercentageIncrease;
			getSpringinessEvent.LinearIncrease = LinearIncrease;
			getSpringinessEvent.PercentageReduction = PercentageReduction;
			getSpringinessEvent.LinearReduction = LinearReduction;
			return getSpringinessEvent;
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x00182DC0 File Offset: 0x00180FC0
		public static int GetFor(GameObject Object, int PercentageIncrease = 0, int LinearIncrease = 0, int PercentageReduction = 0, int LinearReduction = 0)
		{
			int intProperty = Object.GetIntProperty("Springiness", 0);
			int num;
			if (GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetSpringinessEvent>.ID, MinEvent.CascadeLevel))
			{
				GetSpringinessEvent getSpringinessEvent = GetSpringinessEvent.FromPool(Object, intProperty, PercentageIncrease, LinearIncrease, PercentageReduction, LinearReduction);
				Object.HandleEvent(getSpringinessEvent);
				num = (intProperty + getSpringinessEvent.LinearIncrease) * (100 + getSpringinessEvent.PercentageIncrease) * (100 - getSpringinessEvent.PercentageReduction) / 10000 - getSpringinessEvent.LinearReduction;
			}
			else
			{
				num = (intProperty + LinearIncrease) * (100 + PercentageIncrease) * (100 - PercentageReduction) / 10000 - LinearReduction;
			}
			return Math.Max(num, 0);
		}

		// Token: 0x040026E4 RID: 9956
		public GameObject Object;

		// Token: 0x040026E5 RID: 9957
		public int Base;

		// Token: 0x040026E6 RID: 9958
		public int PercentageIncrease;

		// Token: 0x040026E7 RID: 9959
		public int LinearIncrease;

		// Token: 0x040026E8 RID: 9960
		public int PercentageReduction;

		// Token: 0x040026E9 RID: 9961
		public int LinearReduction;
	}
}
