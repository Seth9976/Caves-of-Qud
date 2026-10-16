using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000661 RID: 1633
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterInventoryActionEvent : IActOnItemEvent
	{
		// Token: 0x06004681 RID: 18049 RVA: 0x00166D65 File Offset: 0x00164F65
		public AfterInventoryActionEvent()
		{
			this.ID = AfterInventoryActionEvent.ID;
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x00166D83 File Offset: 0x00164F83
		public static int CountPool()
		{
			if (AfterInventoryActionEvent.Pool != null)
			{
				return AfterInventoryActionEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x00166D98 File Offset: 0x00164F98
		public static void ResetPool()
		{
			while (AfterInventoryActionEvent.PoolCounter > 0)
			{
				AfterInventoryActionEvent.Pool[--AfterInventoryActionEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x00166DC0 File Offset: 0x00164FC0
		public static void ResetTo(ref AfterInventoryActionEvent E)
		{
			MinEvent.ResetTo<AfterInventoryActionEvent>(E, AfterInventoryActionEvent.Pool, ref AfterInventoryActionEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x00166DD6 File Offset: 0x00164FD6
		public static AfterInventoryActionEvent FromPool()
		{
			return MinEvent.FromPool<AfterInventoryActionEvent>(ref AfterInventoryActionEvent.Pool, ref AfterInventoryActionEvent.PoolCounter, 8192);
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x00166DEC File Offset: 0x00164FEC
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x00166E00 File Offset: 0x00165000
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

		// Token: 0x06004688 RID: 18056 RVA: 0x00166E6C File Offset: 0x0016506C
		public static void Send(GameObject Object, InventoryActionEvent Source)
		{
			if (GameObject.Validate(ref Object) && Object.WantEvent(AfterInventoryActionEvent.ID, MinEvent.CascadeLevel))
			{
				AfterInventoryActionEvent afterInventoryActionEvent = AfterInventoryActionEvent.FromPool();
				afterInventoryActionEvent.Actor = Source.Actor;
				afterInventoryActionEvent.Item = Source.Item;
				afterInventoryActionEvent.Command = Source.Command;
				afterInventoryActionEvent.Auto = Source.Auto;
				afterInventoryActionEvent.OwnershipHandled = Source.OwnershipHandled;
				afterInventoryActionEvent.OverrideEnergyCost = Source.OverrideEnergyCost;
				afterInventoryActionEvent.Forced = Source.Forced;
				afterInventoryActionEvent.Silent = Source.Silent;
				afterInventoryActionEvent.EnergyCostOverride = Source.EnergyCostOverride;
				afterInventoryActionEvent.MinimumCharge = Source.MinimumCharge;
				afterInventoryActionEvent.ObjectTarget = Source.ObjectTarget;
				afterInventoryActionEvent.CellTarget = Source.CellTarget;
				afterInventoryActionEvent.FromCell = Source.FromCell;
				Object.HandleEvent(afterInventoryActionEvent);
				Source.ProcessChildEvent(afterInventoryActionEvent);
				Source.Actor = afterInventoryActionEvent.Actor;
				Source.Item = afterInventoryActionEvent.Item;
				Source.Command = afterInventoryActionEvent.Command;
				Source.Auto = afterInventoryActionEvent.Auto;
				Source.OwnershipHandled = afterInventoryActionEvent.OwnershipHandled;
				Source.OverrideEnergyCost = afterInventoryActionEvent.OverrideEnergyCost;
				Source.Forced = afterInventoryActionEvent.Forced;
				Source.Silent = afterInventoryActionEvent.Silent;
				Source.EnergyCostOverride = afterInventoryActionEvent.EnergyCostOverride;
				Source.MinimumCharge = afterInventoryActionEvent.MinimumCharge;
				Source.ObjectTarget = afterInventoryActionEvent.ObjectTarget;
				Source.CellTarget = afterInventoryActionEvent.CellTarget;
				Source.FromCell = afterInventoryActionEvent.FromCell;
			}
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x00166FE8 File Offset: 0x001651E8
		public static void Send(ref bool InterfaceExitRequested, GameObject Object, InventoryActionEvent Source)
		{
			if (GameObject.Validate(ref Object) && Object.WantEvent(AfterInventoryActionEvent.ID, MinEvent.CascadeLevel))
			{
				AfterInventoryActionEvent afterInventoryActionEvent = AfterInventoryActionEvent.FromPool();
				afterInventoryActionEvent.Actor = Source.Actor;
				afterInventoryActionEvent.Item = Source.Item;
				afterInventoryActionEvent.Command = Source.Command;
				afterInventoryActionEvent.Auto = Source.Auto;
				afterInventoryActionEvent.OwnershipHandled = Source.OwnershipHandled;
				afterInventoryActionEvent.OverrideEnergyCost = Source.OverrideEnergyCost;
				afterInventoryActionEvent.Forced = Source.Forced;
				afterInventoryActionEvent.Silent = Source.Silent;
				afterInventoryActionEvent.EnergyCostOverride = Source.EnergyCostOverride;
				afterInventoryActionEvent.MinimumCharge = Source.MinimumCharge;
				afterInventoryActionEvent.ObjectTarget = Source.ObjectTarget;
				afterInventoryActionEvent.CellTarget = Source.CellTarget;
				afterInventoryActionEvent.FromCell = Source.FromCell;
				Object.HandleEvent(afterInventoryActionEvent);
				Source.ProcessChildEvent(afterInventoryActionEvent);
				Source.Actor = afterInventoryActionEvent.Actor;
				Source.Item = afterInventoryActionEvent.Item;
				Source.Command = afterInventoryActionEvent.Command;
				Source.Auto = afterInventoryActionEvent.Auto;
				Source.OwnershipHandled = afterInventoryActionEvent.OwnershipHandled;
				Source.OverrideEnergyCost = afterInventoryActionEvent.OverrideEnergyCost;
				Source.Forced = afterInventoryActionEvent.Forced;
				Source.Silent = afterInventoryActionEvent.Silent;
				Source.EnergyCostOverride = afterInventoryActionEvent.EnergyCostOverride;
				Source.MinimumCharge = afterInventoryActionEvent.MinimumCharge;
				Source.ObjectTarget = afterInventoryActionEvent.ObjectTarget;
				Source.CellTarget = afterInventoryActionEvent.CellTarget;
				Source.FromCell = afterInventoryActionEvent.FromCell;
				if (afterInventoryActionEvent.InterfaceExitRequested())
				{
					InterfaceExitRequested = true;
				}
			}
		}

		// Token: 0x0400217D RID: 8573
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterInventoryActionEvent), null, new MinEvent.EventPoolCount(AfterInventoryActionEvent.CountPool), new MinEvent.EventPoolReset(AfterInventoryActionEvent.ResetPool));

		// Token: 0x0400217E RID: 8574
		private static List<AfterInventoryActionEvent> Pool;

		// Token: 0x0400217F RID: 8575
		private static int PoolCounter;

		// Token: 0x04002180 RID: 8576
		public string Command;

		// Token: 0x04002181 RID: 8577
		public bool OwnershipHandled;

		// Token: 0x04002182 RID: 8578
		public bool Auto;

		// Token: 0x04002183 RID: 8579
		public bool OverrideEnergyCost;

		// Token: 0x04002184 RID: 8580
		public bool Forced;

		// Token: 0x04002185 RID: 8581
		public bool Silent;

		// Token: 0x04002186 RID: 8582
		public int EnergyCostOverride;

		// Token: 0x04002187 RID: 8583
		public int MinimumCharge;

		// Token: 0x04002188 RID: 8584
		public GameObject ObjectTarget;

		// Token: 0x04002189 RID: 8585
		public Cell CellTarget;

		// Token: 0x0400218A RID: 8586
		public Cell FromCell;

		// Token: 0x0400218B RID: 8587
		public List<GameObject> Generated = new List<GameObject>();
	}
}
