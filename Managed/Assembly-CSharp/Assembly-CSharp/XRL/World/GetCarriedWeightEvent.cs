using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200072F RID: 1839
	[GameEvent(Cascade = 0, Cache = MinEvent.Cache.Pool)]
	public class GetCarriedWeightEvent : IWeightEvent
	{
		// Token: 0x06004C44 RID: 19524 RVA: 0x00179536 File Offset: 0x00177736
		public GetCarriedWeightEvent()
		{
			this.ID = GetCarriedWeightEvent.ID;
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x00179549 File Offset: 0x00177749
		public override int GetCascadeLevel()
		{
			return GetCarriedWeightEvent.CascadeLevel;
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x00179550 File Offset: 0x00177750
		public static int CountPool()
		{
			if (GetCarriedWeightEvent.Pool != null)
			{
				return GetCarriedWeightEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x00179565 File Offset: 0x00177765
		public static void ResetPool()
		{
			while (GetCarriedWeightEvent.PoolCounter > 0)
			{
				GetCarriedWeightEvent.Pool[--GetCarriedWeightEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0017958D File Offset: 0x0017778D
		public static void ResetTo(ref GetCarriedWeightEvent E)
		{
			MinEvent.ResetTo<GetCarriedWeightEvent>(E, GetCarriedWeightEvent.Pool, ref GetCarriedWeightEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x001795A3 File Offset: 0x001777A3
		public static GetCarriedWeightEvent FromPool()
		{
			return MinEvent.FromPool<GetCarriedWeightEvent>(ref GetCarriedWeightEvent.Pool, ref GetCarriedWeightEvent.PoolCounter, 8192);
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x001795B9 File Offset: 0x001777B9
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x001795CD File Offset: 0x001777CD
		public void AdjustWeight(double Factor)
		{
			this.Weight *= Factor;
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x001795E0 File Offset: 0x001777E0
		public static int GetFor(GameObject Object)
		{
			double num = 0.0;
			if (Object != null)
			{
				if (Object.HasRegisteredEvent("GetCarriedWeight"))
				{
					Event @event = Event.New("GetCarriedWeight", "Weight", (int)num);
					if (!Object.FireEvent(@event))
					{
						return @event.GetIntParameter("Weight", 0);
					}
					num = (double)@event.GetIntParameter("Weight", 0);
				}
				if (Object.WantEvent(GetCarriedWeightEvent.ID, GetCarriedWeightEvent.CascadeLevel))
				{
					GetCarriedWeightEvent getCarriedWeightEvent = GetCarriedWeightEvent.FromPool();
					getCarriedWeightEvent.Weight = num;
					if (!Object.HandleEvent(getCarriedWeightEvent))
					{
						return (int)getCarriedWeightEvent.Weight;
					}
					num = getCarriedWeightEvent.Weight;
				}
			}
			return (int)num;
		}

		// Token: 0x040024E5 RID: 9445
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetCarriedWeightEvent), null, new MinEvent.EventPoolCount(GetCarriedWeightEvent.CountPool), new MinEvent.EventPoolReset(GetCarriedWeightEvent.ResetPool));

		// Token: 0x040024E6 RID: 9446
		public new static readonly int CascadeLevel = 0;

		// Token: 0x040024E7 RID: 9447
		private static List<GetCarriedWeightEvent> Pool;

		// Token: 0x040024E8 RID: 9448
		private static int PoolCounter;
	}
}
