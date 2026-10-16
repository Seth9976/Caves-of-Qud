using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007EB RID: 2027
	[GameEvent(Cascade = 256, Cache = MinEvent.Cache.Pool)]
	public class InventoryActionEvent : IActOnItemEvent
	{
		// Token: 0x0600516C RID: 20844 RVA: 0x001897A0 File Offset: 0x001879A0
		public InventoryActionEvent()
		{
			this.ID = InventoryActionEvent.ID;
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x001897BE File Offset: 0x001879BE
		public override int GetCascadeLevel()
		{
			return InventoryActionEvent.CascadeLevel;
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x001897C5 File Offset: 0x001879C5
		public static int CountPool()
		{
			if (InventoryActionEvent.Pool != null)
			{
				return InventoryActionEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x001897DA File Offset: 0x001879DA
		public static void ResetPool()
		{
			while (InventoryActionEvent.PoolCounter > 0)
			{
				InventoryActionEvent.Pool[--InventoryActionEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x00189802 File Offset: 0x00187A02
		public static void ResetTo(ref InventoryActionEvent E)
		{
			MinEvent.ResetTo<InventoryActionEvent>(E, InventoryActionEvent.Pool, ref InventoryActionEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x00189818 File Offset: 0x00187A18
		public static InventoryActionEvent FromPool()
		{
			return MinEvent.FromPool<InventoryActionEvent>(ref InventoryActionEvent.Pool, ref InventoryActionEvent.PoolCounter, 8192);
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0018982E File Offset: 0x00187A2E
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x00189844 File Offset: 0x00187A44
		public override void Reset()
		{
			base.Reset();
			this.Command = null;
			this.OwnershipHandled = false;
			this.Auto = false;
			this.OverrideEnergyCost = false;
			this.Forced = false;
			this.Silent = false;
			this.EnergyCostOverride = 0;
			this.MinimumCharge = 0;
			this.StandoffDistance = 0;
			this.ObjectTarget = null;
			this.CellTarget = null;
			this.FromCell = null;
			this.InventoryTarget = null;
			this.Generated.Clear();
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x001898C0 File Offset: 0x00187AC0
		public static bool Check(GameObject Object, GameObject Actor, GameObject Item, string Command, bool Auto = false, bool OwnershipHandled = false, bool OverrideEnergyCost = false, bool Forced = false, bool Silent = false, int EnergyCostOverride = 0, int MinimumCharge = 0, int StandoffDistance = 0, GameObject ObjectTarget = null, Cell CellTarget = null, Cell FromCell = null, IInventory InventoryTarget = null)
		{
			if (GameObject.Validate(ref Object))
			{
				InventoryActionEvent inventoryActionEvent = InventoryActionEvent.FromPool();
				inventoryActionEvent.Actor = Actor;
				inventoryActionEvent.Item = Item;
				inventoryActionEvent.Command = Command;
				inventoryActionEvent.Auto = Auto;
				inventoryActionEvent.OwnershipHandled = OwnershipHandled;
				inventoryActionEvent.OverrideEnergyCost = OverrideEnergyCost;
				inventoryActionEvent.Forced = Forced;
				inventoryActionEvent.Silent = Silent;
				inventoryActionEvent.EnergyCostOverride = EnergyCostOverride;
				inventoryActionEvent.MinimumCharge = MinimumCharge;
				inventoryActionEvent.StandoffDistance = StandoffDistance;
				inventoryActionEvent.ObjectTarget = ObjectTarget;
				inventoryActionEvent.CellTarget = CellTarget;
				inventoryActionEvent.FromCell = FromCell;
				inventoryActionEvent.InventoryTarget = InventoryTarget;
				if (!Object.HandleEvent(inventoryActionEvent))
				{
					return false;
				}
				AfterInventoryActionEvent.Send(Object, inventoryActionEvent);
				OwnerAfterInventoryActionEvent.Send(Actor, inventoryActionEvent);
			}
			return true;
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x00189970 File Offset: 0x00187B70
		public static bool Check(ref bool InterfaceExitRequested, GameObject Object, GameObject Actor, GameObject Item, string Command, bool Auto = false, bool OwnershipHandled = false, bool OverrideEnergyCost = false, bool Forced = false, bool Silent = false, int EnergyCostOverride = 0, int MinimumCharge = 0, int StandoffDistance = 0, GameObject ObjectTarget = null, Cell CellTarget = null, Cell FromCell = null, IInventory InventoryTarget = null)
		{
			if (GameObject.Validate(ref Object))
			{
				InventoryActionEvent inventoryActionEvent = InventoryActionEvent.FromPool();
				inventoryActionEvent.Actor = Actor;
				inventoryActionEvent.Item = Item;
				inventoryActionEvent.Command = Command;
				inventoryActionEvent.Auto = Auto;
				inventoryActionEvent.OwnershipHandled = OwnershipHandled;
				inventoryActionEvent.OverrideEnergyCost = OverrideEnergyCost;
				inventoryActionEvent.Forced = Forced;
				inventoryActionEvent.Silent = Silent;
				inventoryActionEvent.EnergyCostOverride = EnergyCostOverride;
				inventoryActionEvent.MinimumCharge = MinimumCharge;
				inventoryActionEvent.StandoffDistance = StandoffDistance;
				inventoryActionEvent.ObjectTarget = ObjectTarget;
				inventoryActionEvent.CellTarget = CellTarget;
				inventoryActionEvent.FromCell = FromCell;
				inventoryActionEvent.InventoryTarget = InventoryTarget;
				bool flag = Object.HandleEvent(inventoryActionEvent);
				if (inventoryActionEvent.InterfaceExitRequested())
				{
					InterfaceExitRequested = true;
				}
				if (!flag)
				{
					return false;
				}
				AfterInventoryActionEvent.Send(ref InterfaceExitRequested, Object, inventoryActionEvent);
				OwnerAfterInventoryActionEvent.Send(ref InterfaceExitRequested, Actor, inventoryActionEvent);
			}
			return true;
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x00189A2C File Offset: 0x00187C2C
		public static bool Check(out IEvent GeneratedEvent, GameObject Object, GameObject Actor, GameObject Item, string Command, bool Auto = false, bool OwnershipHandled = false, bool OverrideEnergyCost = false, bool Forced = false, bool Silent = false, int EnergyCostOverride = 0, int MinimumCharge = 0, int StandoffDistance = 0, GameObject ObjectTarget = null, Cell CellTarget = null, Cell FromCell = null, IInventory InventoryTarget = null)
		{
			if (GameObject.Validate(ref Object))
			{
				InventoryActionEvent inventoryActionEvent = InventoryActionEvent.FromPool();
				inventoryActionEvent.Actor = Actor;
				inventoryActionEvent.Item = Item;
				inventoryActionEvent.Command = Command;
				inventoryActionEvent.Auto = Auto;
				inventoryActionEvent.OwnershipHandled = OwnershipHandled;
				inventoryActionEvent.OverrideEnergyCost = OverrideEnergyCost;
				inventoryActionEvent.Forced = Forced;
				inventoryActionEvent.Silent = Silent;
				inventoryActionEvent.EnergyCostOverride = EnergyCostOverride;
				inventoryActionEvent.MinimumCharge = MinimumCharge;
				inventoryActionEvent.ObjectTarget = ObjectTarget;
				inventoryActionEvent.CellTarget = CellTarget;
				inventoryActionEvent.StandoffDistance = StandoffDistance;
				inventoryActionEvent.FromCell = FromCell;
				inventoryActionEvent.InventoryTarget = InventoryTarget;
				GeneratedEvent = inventoryActionEvent;
				if (!Object.HandleEvent(inventoryActionEvent))
				{
					return false;
				}
				AfterInventoryActionEvent.Send(Object, inventoryActionEvent);
				OwnerAfterInventoryActionEvent.Send(Actor, inventoryActionEvent);
			}
			else
			{
				GeneratedEvent = null;
			}
			return true;
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x00189AE4 File Offset: 0x00187CE4
		public static bool Check(out InventoryActionEvent E, GameObject Object, GameObject Actor, GameObject Item, string Command, bool Auto = false, bool OwnershipHandled = false, bool OverrideEnergyCost = false, bool Forced = false, bool Silent = false, int EnergyCostOverride = 0, int MinimumCharge = 0, int StandoffDistance = 0, GameObject ObjectTarget = null, Cell CellTarget = null, Cell FromCell = null, IInventory InventoryTarget = null)
		{
			if (GameObject.Validate(ref Object))
			{
				E = InventoryActionEvent.FromPool();
				E.Actor = Actor;
				E.Item = Item;
				E.Command = Command;
				E.Auto = Auto;
				E.OwnershipHandled = OwnershipHandled;
				E.OverrideEnergyCost = OverrideEnergyCost;
				E.Forced = Forced;
				E.Silent = Silent;
				E.EnergyCostOverride = EnergyCostOverride;
				E.MinimumCharge = MinimumCharge;
				E.StandoffDistance = StandoffDistance;
				E.ObjectTarget = ObjectTarget;
				E.CellTarget = CellTarget;
				E.FromCell = FromCell;
				E.InventoryTarget = InventoryTarget;
				if (!Object.HandleEvent(E))
				{
					return false;
				}
				AfterInventoryActionEvent.Send(Object, E);
				OwnerAfterInventoryActionEvent.Send(Actor, E);
			}
			else
			{
				E = null;
			}
			return true;
		}

		// Token: 0x040028C7 RID: 10439
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(InventoryActionEvent), null, new MinEvent.EventPoolCount(InventoryActionEvent.CountPool), new MinEvent.EventPoolReset(InventoryActionEvent.ResetPool));

		// Token: 0x040028C8 RID: 10440
		public new static readonly int CascadeLevel = 256;

		// Token: 0x040028C9 RID: 10441
		private static List<InventoryActionEvent> Pool;

		// Token: 0x040028CA RID: 10442
		private static int PoolCounter;

		// Token: 0x040028CB RID: 10443
		public string Command;

		// Token: 0x040028CC RID: 10444
		public bool OwnershipHandled;

		// Token: 0x040028CD RID: 10445
		public bool Auto;

		// Token: 0x040028CE RID: 10446
		public bool OverrideEnergyCost;

		// Token: 0x040028CF RID: 10447
		public bool Forced;

		// Token: 0x040028D0 RID: 10448
		public bool Silent;

		// Token: 0x040028D1 RID: 10449
		public int EnergyCostOverride;

		// Token: 0x040028D2 RID: 10450
		public int MinimumCharge;

		// Token: 0x040028D3 RID: 10451
		public int StandoffDistance;

		// Token: 0x040028D4 RID: 10452
		public GameObject ObjectTarget;

		// Token: 0x040028D5 RID: 10453
		public Cell CellTarget;

		// Token: 0x040028D6 RID: 10454
		public Cell FromCell;

		// Token: 0x040028D7 RID: 10455
		public IInventory InventoryTarget;

		// Token: 0x040028D8 RID: 10456
		public List<GameObject> Generated = new List<GameObject>();
	}
}
