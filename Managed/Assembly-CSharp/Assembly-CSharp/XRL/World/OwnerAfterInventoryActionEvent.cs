using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200081C RID: 2076
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class OwnerAfterInventoryActionEvent : IActOnItemEvent
	{
		// Token: 0x060052BB RID: 21179 RVA: 0x0018DAD2 File Offset: 0x0018BCD2
		public OwnerAfterInventoryActionEvent()
		{
			this.ID = OwnerAfterInventoryActionEvent.ID;
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x0018DAF0 File Offset: 0x0018BCF0
		public static int CountPool()
		{
			if (OwnerAfterInventoryActionEvent.Pool != null)
			{
				return OwnerAfterInventoryActionEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x0018DB05 File Offset: 0x0018BD05
		public static void ResetPool()
		{
			while (OwnerAfterInventoryActionEvent.PoolCounter > 0)
			{
				OwnerAfterInventoryActionEvent.Pool[--OwnerAfterInventoryActionEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x0018DB2D File Offset: 0x0018BD2D
		public static void ResetTo(ref OwnerAfterInventoryActionEvent E)
		{
			MinEvent.ResetTo<OwnerAfterInventoryActionEvent>(E, OwnerAfterInventoryActionEvent.Pool, ref OwnerAfterInventoryActionEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x0018DB43 File Offset: 0x0018BD43
		public static OwnerAfterInventoryActionEvent FromPool()
		{
			return MinEvent.FromPool<OwnerAfterInventoryActionEvent>(ref OwnerAfterInventoryActionEvent.Pool, ref OwnerAfterInventoryActionEvent.PoolCounter, 8192);
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x0018DB59 File Offset: 0x0018BD59
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x0018DB70 File Offset: 0x0018BD70
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
			this.ObjectTarget = null;
			this.CellTarget = null;
			this.FromCell = null;
			this.Generated.Clear();
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x0018DBDC File Offset: 0x0018BDDC
		public static void Send(GameObject Object, InventoryActionEvent Source)
		{
			if (GameObject.Validate(ref Object) && Object.WantEvent(OwnerAfterInventoryActionEvent.ID, MinEvent.CascadeLevel))
			{
				OwnerAfterInventoryActionEvent ownerAfterInventoryActionEvent = OwnerAfterInventoryActionEvent.FromPool();
				ownerAfterInventoryActionEvent.Actor = Source.Actor;
				ownerAfterInventoryActionEvent.Item = Source.Item;
				ownerAfterInventoryActionEvent.Command = Source.Command;
				ownerAfterInventoryActionEvent.Auto = Source.Auto;
				ownerAfterInventoryActionEvent.OwnershipHandled = Source.OwnershipHandled;
				ownerAfterInventoryActionEvent.OverrideEnergyCost = Source.OverrideEnergyCost;
				ownerAfterInventoryActionEvent.Forced = Source.Forced;
				ownerAfterInventoryActionEvent.Silent = Source.Silent;
				ownerAfterInventoryActionEvent.EnergyCostOverride = Source.EnergyCostOverride;
				ownerAfterInventoryActionEvent.MinimumCharge = Source.MinimumCharge;
				ownerAfterInventoryActionEvent.ObjectTarget = Source.ObjectTarget;
				ownerAfterInventoryActionEvent.CellTarget = Source.CellTarget;
				ownerAfterInventoryActionEvent.FromCell = Source.FromCell;
				Object.HandleEvent(ownerAfterInventoryActionEvent);
				Source.ProcessChildEvent(ownerAfterInventoryActionEvent);
				Source.Actor = ownerAfterInventoryActionEvent.Actor;
				Source.Item = ownerAfterInventoryActionEvent.Item;
				Source.Command = ownerAfterInventoryActionEvent.Command;
				Source.Auto = ownerAfterInventoryActionEvent.Auto;
				Source.OwnershipHandled = ownerAfterInventoryActionEvent.OwnershipHandled;
				Source.OverrideEnergyCost = ownerAfterInventoryActionEvent.OverrideEnergyCost;
				Source.Forced = ownerAfterInventoryActionEvent.Forced;
				Source.Silent = ownerAfterInventoryActionEvent.Silent;
				Source.EnergyCostOverride = ownerAfterInventoryActionEvent.EnergyCostOverride;
				Source.MinimumCharge = ownerAfterInventoryActionEvent.MinimumCharge;
				Source.ObjectTarget = ownerAfterInventoryActionEvent.ObjectTarget;
				Source.CellTarget = ownerAfterInventoryActionEvent.CellTarget;
				Source.FromCell = ownerAfterInventoryActionEvent.FromCell;
			}
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x0018DD58 File Offset: 0x0018BF58
		public static void Send(ref bool InterfaceExitRequested, GameObject Object, InventoryActionEvent Source)
		{
			if (GameObject.Validate(ref Object) && Object.WantEvent(OwnerAfterInventoryActionEvent.ID, MinEvent.CascadeLevel))
			{
				OwnerAfterInventoryActionEvent ownerAfterInventoryActionEvent = OwnerAfterInventoryActionEvent.FromPool();
				ownerAfterInventoryActionEvent.Actor = Source.Actor;
				ownerAfterInventoryActionEvent.Item = Source.Item;
				ownerAfterInventoryActionEvent.Command = Source.Command;
				ownerAfterInventoryActionEvent.Auto = Source.Auto;
				ownerAfterInventoryActionEvent.OwnershipHandled = Source.OwnershipHandled;
				ownerAfterInventoryActionEvent.OverrideEnergyCost = Source.OverrideEnergyCost;
				ownerAfterInventoryActionEvent.Forced = Source.Forced;
				ownerAfterInventoryActionEvent.Silent = Source.Silent;
				ownerAfterInventoryActionEvent.EnergyCostOverride = Source.EnergyCostOverride;
				ownerAfterInventoryActionEvent.MinimumCharge = Source.MinimumCharge;
				ownerAfterInventoryActionEvent.ObjectTarget = Source.ObjectTarget;
				ownerAfterInventoryActionEvent.CellTarget = Source.CellTarget;
				ownerAfterInventoryActionEvent.FromCell = Source.FromCell;
				Object.HandleEvent(ownerAfterInventoryActionEvent);
				Source.ProcessChildEvent(ownerAfterInventoryActionEvent);
				Source.Actor = ownerAfterInventoryActionEvent.Actor;
				Source.Item = ownerAfterInventoryActionEvent.Item;
				Source.Command = ownerAfterInventoryActionEvent.Command;
				Source.Auto = ownerAfterInventoryActionEvent.Auto;
				Source.OwnershipHandled = ownerAfterInventoryActionEvent.OwnershipHandled;
				Source.OverrideEnergyCost = ownerAfterInventoryActionEvent.OverrideEnergyCost;
				Source.Forced = ownerAfterInventoryActionEvent.Forced;
				Source.Silent = ownerAfterInventoryActionEvent.Silent;
				Source.EnergyCostOverride = ownerAfterInventoryActionEvent.EnergyCostOverride;
				Source.MinimumCharge = ownerAfterInventoryActionEvent.MinimumCharge;
				Source.ObjectTarget = ownerAfterInventoryActionEvent.ObjectTarget;
				Source.CellTarget = ownerAfterInventoryActionEvent.CellTarget;
				Source.FromCell = ownerAfterInventoryActionEvent.FromCell;
				if (ownerAfterInventoryActionEvent.InterfaceExitRequested())
				{
					InterfaceExitRequested = true;
				}
			}
		}

		// Token: 0x04002990 RID: 10640
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(OwnerAfterInventoryActionEvent), null, new MinEvent.EventPoolCount(OwnerAfterInventoryActionEvent.CountPool), new MinEvent.EventPoolReset(OwnerAfterInventoryActionEvent.ResetPool));

		// Token: 0x04002991 RID: 10641
		private static List<OwnerAfterInventoryActionEvent> Pool;

		// Token: 0x04002992 RID: 10642
		private static int PoolCounter;

		// Token: 0x04002993 RID: 10643
		public string Command;

		// Token: 0x04002994 RID: 10644
		public bool OwnershipHandled;

		// Token: 0x04002995 RID: 10645
		public bool Auto;

		// Token: 0x04002996 RID: 10646
		public bool OverrideEnergyCost;

		// Token: 0x04002997 RID: 10647
		public bool Forced;

		// Token: 0x04002998 RID: 10648
		public bool Silent;

		// Token: 0x04002999 RID: 10649
		public int EnergyCostOverride;

		// Token: 0x0400299A RID: 10650
		public int MinimumCharge;

		// Token: 0x0400299B RID: 10651
		public GameObject ObjectTarget;

		// Token: 0x0400299C RID: 10652
		public Cell CellTarget;

		// Token: 0x0400299D RID: 10653
		public Cell FromCell;

		// Token: 0x0400299E RID: 10654
		public List<GameObject> Generated = new List<GameObject>();
	}
}
