using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000758 RID: 1880
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetInventoryActionsEvent : IInventoryActionsEvent
	{
		// Token: 0x06004DA1 RID: 19873 RVA: 0x0017D773 File Offset: 0x0017B973
		public GetInventoryActionsEvent()
		{
			this.ID = GetInventoryActionsEvent.ID;
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x0017D786 File Offset: 0x0017B986
		public static int CountPool()
		{
			if (GetInventoryActionsEvent.Pool != null)
			{
				return GetInventoryActionsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x0017D79B File Offset: 0x0017B99B
		public static void ResetPool()
		{
			while (GetInventoryActionsEvent.PoolCounter > 0)
			{
				GetInventoryActionsEvent.Pool[--GetInventoryActionsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x0017D7C3 File Offset: 0x0017B9C3
		public static void ResetTo(ref GetInventoryActionsEvent E)
		{
			MinEvent.ResetTo<GetInventoryActionsEvent>(E, GetInventoryActionsEvent.Pool, ref GetInventoryActionsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x0017D7D9 File Offset: 0x0017B9D9
		public static GetInventoryActionsEvent FromPool()
		{
			return MinEvent.FromPool<GetInventoryActionsEvent>(ref GetInventoryActionsEvent.Pool, ref GetInventoryActionsEvent.PoolCounter, 8192);
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x0017D7EF File Offset: 0x0017B9EF
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x0017D803 File Offset: 0x0017BA03
		public static GetInventoryActionsEvent FromPool(GameObject Actor, GameObject Object, Dictionary<string, InventoryAction> Actions)
		{
			GetInventoryActionsEvent getInventoryActionsEvent = GetInventoryActionsEvent.FromPool();
			getInventoryActionsEvent.Actor = Actor;
			getInventoryActionsEvent.Object = Object;
			getInventoryActionsEvent.Actions = Actions;
			return getInventoryActionsEvent;
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x0017D81F File Offset: 0x0017BA1F
		public static void Send(GameObject Actor, GameObject Object, Dictionary<string, InventoryAction> Actions)
		{
			if (Object.WantEvent(GetInventoryActionsEvent.ID, MinEvent.CascadeLevel))
			{
				Object.HandleEvent(GetInventoryActionsEvent.FromPool(Actor, Object, Actions));
			}
		}

		// Token: 0x040025AC RID: 9644
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetInventoryActionsEvent), null, new MinEvent.EventPoolCount(GetInventoryActionsEvent.CountPool), new MinEvent.EventPoolReset(GetInventoryActionsEvent.ResetPool));

		// Token: 0x040025AD RID: 9645
		private static List<GetInventoryActionsEvent> Pool;

		// Token: 0x040025AE RID: 9646
		private static int PoolCounter;
	}
}
