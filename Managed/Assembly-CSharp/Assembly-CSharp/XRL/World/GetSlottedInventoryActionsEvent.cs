using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200078C RID: 1932
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetSlottedInventoryActionsEvent : IInventoryActionsEvent
	{
		// Token: 0x06004F0E RID: 20238 RVA: 0x00182649 File Offset: 0x00180849
		public GetSlottedInventoryActionsEvent()
		{
			this.ID = GetSlottedInventoryActionsEvent.ID;
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x0018265C File Offset: 0x0018085C
		public static int CountPool()
		{
			if (GetSlottedInventoryActionsEvent.Pool != null)
			{
				return GetSlottedInventoryActionsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x00182671 File Offset: 0x00180871
		public static void ResetPool()
		{
			while (GetSlottedInventoryActionsEvent.PoolCounter > 0)
			{
				GetSlottedInventoryActionsEvent.Pool[--GetSlottedInventoryActionsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x00182699 File Offset: 0x00180899
		public static void ResetTo(ref GetSlottedInventoryActionsEvent E)
		{
			MinEvent.ResetTo<GetSlottedInventoryActionsEvent>(E, GetSlottedInventoryActionsEvent.Pool, ref GetSlottedInventoryActionsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x001826AF File Offset: 0x001808AF
		public static GetSlottedInventoryActionsEvent FromPool()
		{
			return MinEvent.FromPool<GetSlottedInventoryActionsEvent>(ref GetSlottedInventoryActionsEvent.Pool, ref GetSlottedInventoryActionsEvent.PoolCounter, 8192);
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x001826C5 File Offset: 0x001808C5
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x001826D9 File Offset: 0x001808D9
		public override void Reset()
		{
			base.Reset();
			this.Slotted = null;
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x001826E8 File Offset: 0x001808E8
		public static GetSlottedInventoryActionsEvent FromPool(GameObject Slotted, GameObject Actor, GameObject Object, Dictionary<string, InventoryAction> Actions)
		{
			GetSlottedInventoryActionsEvent getSlottedInventoryActionsEvent = GetSlottedInventoryActionsEvent.FromPool();
			getSlottedInventoryActionsEvent.Actor = Actor;
			getSlottedInventoryActionsEvent.Object = Object;
			getSlottedInventoryActionsEvent.Actions = Actions;
			return getSlottedInventoryActionsEvent;
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x00182704 File Offset: 0x00180904
		public static GetSlottedInventoryActionsEvent FromPool(GameObject Slotted, IInventoryActionsEvent Parent)
		{
			return GetSlottedInventoryActionsEvent.FromPool(Slotted, Parent.Actor, Parent.Object, Parent.Actions);
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x00182720 File Offset: 0x00180920
		public static void Send(GameObject Slotted, IInventoryActionsEvent Parent)
		{
			if (GameObject.Validate(ref Slotted) && Slotted.WantEvent(GetSlottedInventoryActionsEvent.ID, MinEvent.CascadeLevel))
			{
				GetSlottedInventoryActionsEvent getSlottedInventoryActionsEvent = GetSlottedInventoryActionsEvent.FromPool(Slotted, Parent);
				Slotted.HandleEvent(getSlottedInventoryActionsEvent);
				Parent.ProcessChildEvent(getSlottedInventoryActionsEvent);
			}
		}

		// Token: 0x040026C9 RID: 9929
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetSlottedInventoryActionsEvent), null, new MinEvent.EventPoolCount(GetSlottedInventoryActionsEvent.CountPool), new MinEvent.EventPoolReset(GetSlottedInventoryActionsEvent.ResetPool));

		// Token: 0x040026CA RID: 9930
		private static List<GetSlottedInventoryActionsEvent> Pool;

		// Token: 0x040026CB RID: 9931
		private static int PoolCounter;

		// Token: 0x040026CC RID: 9932
		public GameObject Slotted;
	}
}
