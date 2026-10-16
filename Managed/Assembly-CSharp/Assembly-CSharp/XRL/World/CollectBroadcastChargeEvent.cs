using System;

namespace XRL.World
{
	// Token: 0x020006E7 RID: 1767
	[GameEvent(Cascade = 32, Cache = MinEvent.Cache.Pool)]
	public class CollectBroadcastChargeEvent : PooledEvent<CollectBroadcastChargeEvent>
	{
		// Token: 0x06004A34 RID: 18996 RVA: 0x00173B13 File Offset: 0x00171D13
		public override int GetCascadeLevel()
		{
			return CollectBroadcastChargeEvent.CascadeLevel;
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x00173B1A File Offset: 0x00171D1A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x00173B23 File Offset: 0x00171D23
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Zone = null;
			this.Cell = null;
			this.Charge = 0;
			this.MultipleCharge = 0;
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x00173B50 File Offset: 0x00171D50
		public static int GetFor(GameObject Object, Zone Zone, Cell Cell, int Charge, int MultipleCharge = 1)
		{
			if (Zone != null)
			{
				CollectBroadcastChargeEvent collectBroadcastChargeEvent = PooledEvent<CollectBroadcastChargeEvent>.FromPool();
				collectBroadcastChargeEvent.Object = Object;
				collectBroadcastChargeEvent.Zone = Zone;
				collectBroadcastChargeEvent.Cell = Cell;
				collectBroadcastChargeEvent.Charge = Charge;
				collectBroadcastChargeEvent.MultipleCharge = MultipleCharge;
				Zone.HandleEvent(collectBroadcastChargeEvent);
				return collectBroadcastChargeEvent.Charge;
			}
			return Charge;
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x00173B9C File Offset: 0x00171D9C
		public static int GetFor(GameObject Object, int Charge, int MultipleCharge = 1)
		{
			Cell currentCell = Object.CurrentCell;
			if (currentCell != null)
			{
				return CollectBroadcastChargeEvent.GetFor(Object, currentCell.ParentZone, currentCell, Charge, MultipleCharge);
			}
			return Charge;
		}

		// Token: 0x040023B2 RID: 9138
		public new static readonly int CascadeLevel = 32;

		// Token: 0x040023B3 RID: 9139
		public GameObject Object;

		// Token: 0x040023B4 RID: 9140
		public Zone Zone;

		// Token: 0x040023B5 RID: 9141
		public Cell Cell;

		// Token: 0x040023B6 RID: 9142
		public int Charge;

		// Token: 0x040023B7 RID: 9143
		public int MultipleCharge;
	}
}
