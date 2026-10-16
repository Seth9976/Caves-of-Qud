using System;

namespace XRL.World
{
	// Token: 0x0200075C RID: 1884
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetKineticResistanceEvent : PooledEvent<GetKineticResistanceEvent>
	{
		// Token: 0x06004DBE RID: 19902 RVA: 0x0017DC7A File Offset: 0x0017BE7A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x0017DC83 File Offset: 0x0017BE83
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

		// Token: 0x06004DC0 RID: 19904 RVA: 0x0017DCB5 File Offset: 0x0017BEB5
		public static GetKineticResistanceEvent FromPool(GameObject Object, int Base, int PercentageIncrease = 0, int LinearIncrease = 0, int PercentageReduction = 0, int LinearReduction = 0)
		{
			GetKineticResistanceEvent getKineticResistanceEvent = PooledEvent<GetKineticResistanceEvent>.FromPool();
			getKineticResistanceEvent.Object = Object;
			getKineticResistanceEvent.Base = Base;
			getKineticResistanceEvent.PercentageIncrease = PercentageIncrease;
			getKineticResistanceEvent.LinearIncrease = LinearIncrease;
			getKineticResistanceEvent.PercentageReduction = PercentageReduction;
			getKineticResistanceEvent.LinearReduction = LinearReduction;
			return getKineticResistanceEvent;
		}

		// Token: 0x06004DC1 RID: 19905 RVA: 0x0017DCE8 File Offset: 0x0017BEE8
		public static int GetFor(GameObject Object, int PercentageIncrease = 0, int LinearIncrease = 0, int PercentageReduction = 0, int LinearReduction = 0)
		{
			int num = Object.Weight + Object.GetIntProperty("Anchoring", 0);
			int num2;
			if (Object != null && Object.WantEvent(PooledEvent<GetKineticResistanceEvent>.ID, MinEvent.CascadeLevel))
			{
				GetKineticResistanceEvent getKineticResistanceEvent = GetKineticResistanceEvent.FromPool(Object, num, PercentageIncrease, LinearIncrease, PercentageReduction, LinearReduction);
				Object.HandleEvent(getKineticResistanceEvent);
				num2 = (num + getKineticResistanceEvent.LinearIncrease) * (100 + getKineticResistanceEvent.PercentageIncrease) * (100 - getKineticResistanceEvent.PercentageReduction) / 10000 - getKineticResistanceEvent.LinearReduction;
			}
			else
			{
				num2 = (num + LinearIncrease) * (100 + PercentageIncrease) * (100 - PercentageReduction) / 10000 - LinearReduction;
			}
			return Math.Max(num2, 0);
		}

		// Token: 0x040025BF RID: 9663
		public GameObject Object;

		// Token: 0x040025C0 RID: 9664
		public int Base;

		// Token: 0x040025C1 RID: 9665
		public int PercentageIncrease;

		// Token: 0x040025C2 RID: 9666
		public int LinearIncrease;

		// Token: 0x040025C3 RID: 9667
		public int PercentageReduction;

		// Token: 0x040025C4 RID: 9668
		public int LinearReduction;
	}
}
