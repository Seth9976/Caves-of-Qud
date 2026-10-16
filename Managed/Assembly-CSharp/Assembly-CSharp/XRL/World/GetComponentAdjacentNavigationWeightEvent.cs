using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000736 RID: 1846
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetComponentAdjacentNavigationWeightEvent : IAdjacentNavigationWeightEvent
	{
		// Token: 0x06004C6F RID: 19567 RVA: 0x00179DA0 File Offset: 0x00177FA0
		public GetComponentAdjacentNavigationWeightEvent()
		{
			this.ID = GetComponentAdjacentNavigationWeightEvent.ID;
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x00179DB3 File Offset: 0x00177FB3
		public static int CountPool()
		{
			if (GetComponentAdjacentNavigationWeightEvent.Pool != null)
			{
				return GetComponentAdjacentNavigationWeightEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x00179DC8 File Offset: 0x00177FC8
		public static void ResetPool()
		{
			while (GetComponentAdjacentNavigationWeightEvent.PoolCounter > 0)
			{
				GetComponentAdjacentNavigationWeightEvent.Pool[--GetComponentAdjacentNavigationWeightEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x00179DF0 File Offset: 0x00177FF0
		public static void ResetTo(ref GetComponentAdjacentNavigationWeightEvent E)
		{
			MinEvent.ResetTo<GetComponentAdjacentNavigationWeightEvent>(E, GetComponentAdjacentNavigationWeightEvent.Pool, ref GetComponentAdjacentNavigationWeightEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x00179E06 File Offset: 0x00178006
		public static GetComponentAdjacentNavigationWeightEvent FromPool()
		{
			return MinEvent.FromPool<GetComponentAdjacentNavigationWeightEvent>(ref GetComponentAdjacentNavigationWeightEvent.Pool, ref GetComponentAdjacentNavigationWeightEvent.PoolCounter, 8192);
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x00179E1C File Offset: 0x0017801C
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004C75 RID: 19573 RVA: 0x00179E30 File Offset: 0x00178030
		public static GetComponentAdjacentNavigationWeightEvent FromPool(INavigationWeightEvent Source)
		{
			GetComponentAdjacentNavigationWeightEvent getComponentAdjacentNavigationWeightEvent = GetComponentAdjacentNavigationWeightEvent.FromPool();
			Source.ApplyTo(getComponentAdjacentNavigationWeightEvent);
			return getComponentAdjacentNavigationWeightEvent;
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x00179E4C File Offset: 0x0017804C
		public static void Process(GameObject Object, INavigationWeightEvent ParentEvent)
		{
			if (GameObject.Validate(ref Object) && Object.WantEvent(GetComponentAdjacentNavigationWeightEvent.ID, MinEvent.CascadeLevel))
			{
				GetComponentAdjacentNavigationWeightEvent getComponentAdjacentNavigationWeightEvent = GetComponentAdjacentNavigationWeightEvent.FromPool(ParentEvent);
				getComponentAdjacentNavigationWeightEvent.Object = Object;
				getComponentAdjacentNavigationWeightEvent.PriorWeight = getComponentAdjacentNavigationWeightEvent.Weight;
				Object.HandleEvent(getComponentAdjacentNavigationWeightEvent);
				getComponentAdjacentNavigationWeightEvent.ApplyTo(ParentEvent);
			}
		}

		// Token: 0x04002504 RID: 9476
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetComponentAdjacentNavigationWeightEvent), null, new MinEvent.EventPoolCount(GetComponentAdjacentNavigationWeightEvent.CountPool), new MinEvent.EventPoolReset(GetComponentAdjacentNavigationWeightEvent.ResetPool));

		// Token: 0x04002505 RID: 9477
		private static List<GetComponentAdjacentNavigationWeightEvent> Pool;

		// Token: 0x04002506 RID: 9478
		private static int PoolCounter;
	}
}
