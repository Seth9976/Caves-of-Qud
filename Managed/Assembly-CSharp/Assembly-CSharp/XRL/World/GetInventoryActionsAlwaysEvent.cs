using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000757 RID: 1879
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetInventoryActionsAlwaysEvent : IInventoryActionsEvent
	{
		// Token: 0x06004D98 RID: 19864 RVA: 0x0017D675 File Offset: 0x0017B875
		public GetInventoryActionsAlwaysEvent()
		{
			this.ID = GetInventoryActionsAlwaysEvent.ID;
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x0017D688 File Offset: 0x0017B888
		public static int CountPool()
		{
			if (GetInventoryActionsAlwaysEvent.Pool != null)
			{
				return GetInventoryActionsAlwaysEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x0017D69D File Offset: 0x0017B89D
		public static void ResetPool()
		{
			while (GetInventoryActionsAlwaysEvent.PoolCounter > 0)
			{
				GetInventoryActionsAlwaysEvent.Pool[--GetInventoryActionsAlwaysEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x0017D6C5 File Offset: 0x0017B8C5
		public static void ResetTo(ref GetInventoryActionsAlwaysEvent E)
		{
			MinEvent.ResetTo<GetInventoryActionsAlwaysEvent>(E, GetInventoryActionsAlwaysEvent.Pool, ref GetInventoryActionsAlwaysEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x0017D6DB File Offset: 0x0017B8DB
		public static GetInventoryActionsAlwaysEvent FromPool()
		{
			return MinEvent.FromPool<GetInventoryActionsAlwaysEvent>(ref GetInventoryActionsAlwaysEvent.Pool, ref GetInventoryActionsAlwaysEvent.PoolCounter, 8192);
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x0017D6F1 File Offset: 0x0017B8F1
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x0017D705 File Offset: 0x0017B905
		public static GetInventoryActionsAlwaysEvent FromPool(GameObject Actor, GameObject Object, Dictionary<string, InventoryAction> Actions)
		{
			GetInventoryActionsAlwaysEvent getInventoryActionsAlwaysEvent = GetInventoryActionsAlwaysEvent.FromPool();
			getInventoryActionsAlwaysEvent.Actor = Actor;
			getInventoryActionsAlwaysEvent.Object = Object;
			getInventoryActionsAlwaysEvent.Actions = Actions;
			return getInventoryActionsAlwaysEvent;
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x0017D721 File Offset: 0x0017B921
		public static void Send(GameObject Actor, GameObject Object, Dictionary<string, InventoryAction> Actions)
		{
			if (Object.WantEvent(GetInventoryActionsAlwaysEvent.ID, MinEvent.CascadeLevel))
			{
				Object.HandleEvent(GetInventoryActionsAlwaysEvent.FromPool(Actor, Object, Actions));
			}
		}

		// Token: 0x040025A9 RID: 9641
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetInventoryActionsAlwaysEvent), null, new MinEvent.EventPoolCount(GetInventoryActionsAlwaysEvent.CountPool), new MinEvent.EventPoolReset(GetInventoryActionsAlwaysEvent.ResetPool));

		// Token: 0x040025AA RID: 9642
		private static List<GetInventoryActionsAlwaysEvent> Pool;

		// Token: 0x040025AB RID: 9643
		private static int PoolCounter;
	}
}
