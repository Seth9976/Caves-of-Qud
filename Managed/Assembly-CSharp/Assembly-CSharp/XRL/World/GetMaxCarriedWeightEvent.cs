using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000762 RID: 1890
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class GetMaxCarriedWeightEvent : IWeightEvent
	{
		// Token: 0x06004DE5 RID: 19941 RVA: 0x0017E3AB File Offset: 0x0017C5AB
		public GetMaxCarriedWeightEvent()
		{
			this.ID = GetMaxCarriedWeightEvent.ID;
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x0017E3BE File Offset: 0x0017C5BE
		public override int GetCascadeLevel()
		{
			return GetMaxCarriedWeightEvent.CascadeLevel;
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x0017E3C5 File Offset: 0x0017C5C5
		public static int CountPool()
		{
			if (GetMaxCarriedWeightEvent.Pool != null)
			{
				return GetMaxCarriedWeightEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x0017E3DA File Offset: 0x0017C5DA
		public static void ResetPool()
		{
			while (GetMaxCarriedWeightEvent.PoolCounter > 0)
			{
				GetMaxCarriedWeightEvent.Pool[--GetMaxCarriedWeightEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x0017E402 File Offset: 0x0017C602
		public static void ResetTo(ref GetMaxCarriedWeightEvent E)
		{
			MinEvent.ResetTo<GetMaxCarriedWeightEvent>(E, GetMaxCarriedWeightEvent.Pool, ref GetMaxCarriedWeightEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x0017E418 File Offset: 0x0017C618
		public static GetMaxCarriedWeightEvent FromPool()
		{
			return MinEvent.FromPool<GetMaxCarriedWeightEvent>(ref GetMaxCarriedWeightEvent.Pool, ref GetMaxCarriedWeightEvent.PoolCounter, 8192);
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x0017E42E File Offset: 0x0017C62E
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x0017E442 File Offset: 0x0017C642
		public void AdjustWeight(double Factor)
		{
			this.Weight *= Factor;
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x0017E454 File Offset: 0x0017C654
		public static int GetFor(GameObject Object, double BaseWeight)
		{
			double num = BaseWeight;
			if (Object != null)
			{
				bool flag = true;
				if (Object.IsGiganticCreature)
				{
					num *= 2.0;
				}
				if (Object.HasRegisteredEvent("GetMaxWeight"))
				{
					Event @event = Event.New("GetMaxWeight", "BaseWeight", (int)BaseWeight, "Weight", (int)num);
					flag = Object.FireEvent(@event);
					BaseWeight = (double)@event.GetIntParameter("BaseWeight", 0);
					num = (double)@event.GetIntParameter("Weight", 0);
				}
				if (flag && Object.WantEvent(GetMaxCarriedWeightEvent.ID, GetMaxCarriedWeightEvent.CascadeLevel))
				{
					GetMaxCarriedWeightEvent getMaxCarriedWeightEvent = GetMaxCarriedWeightEvent.FromPool();
					getMaxCarriedWeightEvent.Object = Object;
					getMaxCarriedWeightEvent.BaseWeight = BaseWeight;
					getMaxCarriedWeightEvent.Weight = num;
					Object.HandleEvent(getMaxCarriedWeightEvent);
					num = getMaxCarriedWeightEvent.Weight;
				}
			}
			return (int)num;
		}

		// Token: 0x040025DF RID: 9695
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetMaxCarriedWeightEvent), null, new MinEvent.EventPoolCount(GetMaxCarriedWeightEvent.CountPool), new MinEvent.EventPoolReset(GetMaxCarriedWeightEvent.ResetPool));

		// Token: 0x040025E0 RID: 9696
		public new static readonly int CascadeLevel = 1;

		// Token: 0x040025E1 RID: 9697
		private static List<GetMaxCarriedWeightEvent> Pool;

		// Token: 0x040025E2 RID: 9698
		private static int PoolCounter;
	}
}
