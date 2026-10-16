using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000737 RID: 1847
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetComponentNavigationWeightEvent : INavigationWeightEvent
	{
		// Token: 0x06004C78 RID: 19576 RVA: 0x00179ECC File Offset: 0x001780CC
		public GetComponentNavigationWeightEvent()
		{
			this.ID = GetComponentNavigationWeightEvent.ID;
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x00179EDF File Offset: 0x001780DF
		public static int CountPool()
		{
			if (GetComponentNavigationWeightEvent.Pool != null)
			{
				return GetComponentNavigationWeightEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x00179EF4 File Offset: 0x001780F4
		public static void ResetPool()
		{
			while (GetComponentNavigationWeightEvent.PoolCounter > 0)
			{
				GetComponentNavigationWeightEvent.Pool[--GetComponentNavigationWeightEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004C7B RID: 19579 RVA: 0x00179F1C File Offset: 0x0017811C
		public static void ResetTo(ref GetComponentNavigationWeightEvent E)
		{
			MinEvent.ResetTo<GetComponentNavigationWeightEvent>(E, GetComponentNavigationWeightEvent.Pool, ref GetComponentNavigationWeightEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x00179F32 File Offset: 0x00178132
		public static GetComponentNavigationWeightEvent FromPool()
		{
			return MinEvent.FromPool<GetComponentNavigationWeightEvent>(ref GetComponentNavigationWeightEvent.Pool, ref GetComponentNavigationWeightEvent.PoolCounter, 8192);
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x00179F48 File Offset: 0x00178148
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x00179F5C File Offset: 0x0017815C
		public static GetComponentNavigationWeightEvent FromPool(INavigationWeightEvent Source)
		{
			GetComponentNavigationWeightEvent getComponentNavigationWeightEvent = GetComponentNavigationWeightEvent.FromPool();
			Source.ApplyTo(getComponentNavigationWeightEvent);
			return getComponentNavigationWeightEvent;
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x00179F78 File Offset: 0x00178178
		public static void Process(GameObject Object, INavigationWeightEvent ParentEvent)
		{
			if (GameObject.Validate(ref Object) && Object.WantEvent(GetComponentNavigationWeightEvent.ID, MinEvent.CascadeLevel))
			{
				GetComponentNavigationWeightEvent getComponentNavigationWeightEvent = GetComponentNavigationWeightEvent.FromPool(ParentEvent);
				getComponentNavigationWeightEvent.Object = Object;
				getComponentNavigationWeightEvent.PriorWeight = getComponentNavigationWeightEvent.Weight;
				Object.HandleEvent(getComponentNavigationWeightEvent);
				getComponentNavigationWeightEvent.ApplyTo(ParentEvent);
			}
		}

		// Token: 0x04002507 RID: 9479
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetComponentNavigationWeightEvent), null, new MinEvent.EventPoolCount(GetComponentNavigationWeightEvent.CountPool), new MinEvent.EventPoolReset(GetComponentNavigationWeightEvent.ResetPool));

		// Token: 0x04002508 RID: 9480
		private static List<GetComponentNavigationWeightEvent> Pool;

		// Token: 0x04002509 RID: 9481
		private static int PoolCounter;
	}
}
