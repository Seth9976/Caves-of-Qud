using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200081D RID: 2077
	[GameEvent(Cascade = 273, Cache = MinEvent.Cache.Pool)]
	public class OwnerGetInventoryActionsEvent : IInventoryActionsEvent
	{
		// Token: 0x060052C5 RID: 21189 RVA: 0x0018DF0D File Offset: 0x0018C10D
		public OwnerGetInventoryActionsEvent()
		{
			this.ID = OwnerGetInventoryActionsEvent.ID;
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x0018DF20 File Offset: 0x0018C120
		public override int GetCascadeLevel()
		{
			return OwnerGetInventoryActionsEvent.CascadeLevel;
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x0018DF27 File Offset: 0x0018C127
		public static int CountPool()
		{
			if (OwnerGetInventoryActionsEvent.Pool != null)
			{
				return OwnerGetInventoryActionsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x0018DF3C File Offset: 0x0018C13C
		public static void ResetPool()
		{
			while (OwnerGetInventoryActionsEvent.PoolCounter > 0)
			{
				OwnerGetInventoryActionsEvent.Pool[--OwnerGetInventoryActionsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x0018DF64 File Offset: 0x0018C164
		public static void ResetTo(ref OwnerGetInventoryActionsEvent E)
		{
			MinEvent.ResetTo<OwnerGetInventoryActionsEvent>(E, OwnerGetInventoryActionsEvent.Pool, ref OwnerGetInventoryActionsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x0018DF7A File Offset: 0x0018C17A
		public static OwnerGetInventoryActionsEvent FromPool()
		{
			return MinEvent.FromPool<OwnerGetInventoryActionsEvent>(ref OwnerGetInventoryActionsEvent.Pool, ref OwnerGetInventoryActionsEvent.PoolCounter, 8192);
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x0018DF90 File Offset: 0x0018C190
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x0018DFA4 File Offset: 0x0018C1A4
		public static void Send(GameObject Actor, GameObject Object, Dictionary<string, InventoryAction> Actions)
		{
			OwnerGetInventoryActionsEvent ownerGetInventoryActionsEvent = OwnerGetInventoryActionsEvent.FromPool();
			ownerGetInventoryActionsEvent.Actor = Actor;
			ownerGetInventoryActionsEvent.Object = Object;
			ownerGetInventoryActionsEvent.Actions = Actions;
			Actor.HandleEvent(ownerGetInventoryActionsEvent);
			OwnerGetInventoryActionsEvent.ResetTo(ref ownerGetInventoryActionsEvent);
		}

		// Token: 0x0400299F RID: 10655
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(OwnerGetInventoryActionsEvent), null, new MinEvent.EventPoolCount(OwnerGetInventoryActionsEvent.CountPool), new MinEvent.EventPoolReset(OwnerGetInventoryActionsEvent.ResetPool));

		// Token: 0x040029A0 RID: 10656
		public new static readonly int CascadeLevel = 273;

		// Token: 0x040029A1 RID: 10657
		private static List<OwnerGetInventoryActionsEvent> Pool;

		// Token: 0x040029A2 RID: 10658
		private static int PoolCounter;
	}
}
