using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200073A RID: 1850
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetCookingActionsEvent : IInventoryActionsEvent
	{
		// Token: 0x06004C92 RID: 19602 RVA: 0x0017A24C File Offset: 0x0017844C
		public GetCookingActionsEvent()
		{
			this.ID = GetCookingActionsEvent.ID;
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x0017A25F File Offset: 0x0017845F
		public static int CountPool()
		{
			if (GetCookingActionsEvent.Pool != null)
			{
				return GetCookingActionsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x0017A274 File Offset: 0x00178474
		public static void ResetPool()
		{
			while (GetCookingActionsEvent.PoolCounter > 0)
			{
				GetCookingActionsEvent.Pool[--GetCookingActionsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x0017A29C File Offset: 0x0017849C
		public static void ResetTo(ref GetCookingActionsEvent E)
		{
			MinEvent.ResetTo<GetCookingActionsEvent>(E, GetCookingActionsEvent.Pool, ref GetCookingActionsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x0017A2B2 File Offset: 0x001784B2
		public static GetCookingActionsEvent FromPool()
		{
			return MinEvent.FromPool<GetCookingActionsEvent>(ref GetCookingActionsEvent.Pool, ref GetCookingActionsEvent.PoolCounter, 8192);
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x0017A2C8 File Offset: 0x001784C8
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x0017A2DC File Offset: 0x001784DC
		public static GetCookingActionsEvent FromPool(GameObject Actor, GameObject Object, Dictionary<string, InventoryAction> Actions)
		{
			GetCookingActionsEvent getCookingActionsEvent = GetCookingActionsEvent.FromPool();
			getCookingActionsEvent.Actor = Actor;
			getCookingActionsEvent.Object = Object;
			getCookingActionsEvent.Actions = Actions;
			return getCookingActionsEvent;
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x0017A2F8 File Offset: 0x001784F8
		public static void SendToActorAndObject(GameObject Actor, GameObject Object, Dictionary<string, InventoryAction> Actions)
		{
			if (!Object.WantEvent(GetCookingActionsEvent.ID, MinEvent.CascadeLevel) && !Actor.WantEvent(GetCookingActionsEvent.ID, MinEvent.CascadeLevel))
			{
				return;
			}
			GetCookingActionsEvent getCookingActionsEvent = GetCookingActionsEvent.FromPool(Actor, Object, Actions);
			Object.HandleEvent(getCookingActionsEvent);
			Actor.HandleEvent(getCookingActionsEvent);
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x0017A343 File Offset: 0x00178543
		public static void Send(GameObject Actor, GameObject Object, Dictionary<string, InventoryAction> Actions)
		{
			if (Object.WantEvent(GetCookingActionsEvent.ID, MinEvent.CascadeLevel))
			{
				Object.HandleEvent(GetCookingActionsEvent.FromPool(Actor, Object, Actions));
			}
		}

		// Token: 0x04002513 RID: 9491
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetCookingActionsEvent), null, new MinEvent.EventPoolCount(GetCookingActionsEvent.CountPool), new MinEvent.EventPoolReset(GetCookingActionsEvent.ResetPool));

		// Token: 0x04002514 RID: 9492
		private static List<GetCookingActionsEvent> Pool;

		// Token: 0x04002515 RID: 9493
		private static int PoolCounter;
	}
}
