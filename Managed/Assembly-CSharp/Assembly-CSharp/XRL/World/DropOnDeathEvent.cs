using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006FB RID: 1787
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class DropOnDeathEvent : IObjectInventoryInteractionEvent
	{
		// Token: 0x06004ABF RID: 19135 RVA: 0x001751B3 File Offset: 0x001733B3
		public DropOnDeathEvent()
		{
			this.ID = DropOnDeathEvent.ID;
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x001751C6 File Offset: 0x001733C6
		public static int CountPool()
		{
			if (DropOnDeathEvent.Pool != null)
			{
				return DropOnDeathEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x001751DB File Offset: 0x001733DB
		public static void ResetPool()
		{
			while (DropOnDeathEvent.PoolCounter > 0)
			{
				DropOnDeathEvent.Pool[--DropOnDeathEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x00175203 File Offset: 0x00173403
		public static void ResetTo(ref DropOnDeathEvent E)
		{
			MinEvent.ResetTo<DropOnDeathEvent>(E, DropOnDeathEvent.Pool, ref DropOnDeathEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x00175219 File Offset: 0x00173419
		public static DropOnDeathEvent FromPool()
		{
			return MinEvent.FromPool<DropOnDeathEvent>(ref DropOnDeathEvent.Pool, ref DropOnDeathEvent.PoolCounter, 8192);
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x0017522F File Offset: 0x0017342F
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x00175243 File Offset: 0x00173443
		public override void Reset()
		{
			base.Reset();
			this.WasEquipped = false;
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00175254 File Offset: 0x00173454
		public static DropOnDeathEvent FromPool(GameObject Object, IInventory Inventory, bool WasEquipped = false, bool Forced = true, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = ".", string Type = "DropOnDeath")
		{
			DropOnDeathEvent dropOnDeathEvent = DropOnDeathEvent.FromPool();
			dropOnDeathEvent.Object = Object;
			dropOnDeathEvent.Inventory = Inventory;
			dropOnDeathEvent.Forced = Forced;
			dropOnDeathEvent.IgnoreGravity = IgnoreGravity;
			dropOnDeathEvent.NoStack = NoStack;
			dropOnDeathEvent.Direction = Direction;
			dropOnDeathEvent.Type = Type;
			dropOnDeathEvent.Dragging = null;
			dropOnDeathEvent.Actor = null;
			dropOnDeathEvent.ForceSwap = null;
			dropOnDeathEvent.Ignore = null;
			dropOnDeathEvent.WasEquipped = WasEquipped;
			return dropOnDeathEvent;
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x001752C0 File Offset: 0x001734C0
		public static bool Check(GameObject Object, IInventory Inventory, bool WasEquipped = false, bool Forced = true, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = ".", string Type = "DropOnDeath")
		{
			return !GameObject.Validate(ref Object) || !Object.WantEvent(DropOnDeathEvent.ID, MinEvent.CascadeLevel) || Object.HandleEvent(DropOnDeathEvent.FromPool(Object, Inventory, WasEquipped, Forced, System, IgnoreGravity, false, Direction, Type));
		}

		// Token: 0x0400241C RID: 9244
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(DropOnDeathEvent), null, new MinEvent.EventPoolCount(DropOnDeathEvent.CountPool), new MinEvent.EventPoolReset(DropOnDeathEvent.ResetPool));

		// Token: 0x0400241D RID: 9245
		private static List<DropOnDeathEvent> Pool;

		// Token: 0x0400241E RID: 9246
		private static int PoolCounter;

		// Token: 0x0400241F RID: 9247
		public bool WasEquipped;
	}
}
