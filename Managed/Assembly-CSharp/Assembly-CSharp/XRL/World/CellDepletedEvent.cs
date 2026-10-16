using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020006D3 RID: 1747
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CellDepletedEvent : PooledEvent<CellDepletedEvent>
	{
		// Token: 0x060049C0 RID: 18880 RVA: 0x00172763 File Offset: 0x00170963
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x0017276C File Offset: 0x0017096C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Cell = null;
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x00172784 File Offset: 0x00170984
		public static void Send(GameObject Object, IEnergyCell Cell)
		{
			bool flag = Object != null && Object.WantEvent(PooledEvent<CellDepletedEvent>.ID, MinEvent.CascadeLevel);
			bool flag2 = ((Cell != null) ? Cell.SlottedIn : null) != null && Cell.SlottedIn.WantEvent(PooledEvent<CellDepletedEvent>.ID, MinEvent.CascadeLevel);
			if (flag || flag2)
			{
				CellDepletedEvent cellDepletedEvent = PooledEvent<CellDepletedEvent>.FromPool();
				cellDepletedEvent.Object = Object;
				cellDepletedEvent.Cell = Cell;
				if (flag && !Object.HandleEvent(cellDepletedEvent))
				{
					return;
				}
				if (flag2 && !Cell.SlottedIn.HandleEvent(cellDepletedEvent))
				{
					return;
				}
			}
			bool flag3 = Object != null && Object.HasRegisteredEvent("CanBeSlotted");
			bool flag4 = ((Cell != null) ? Cell.SlottedIn : null) != null && Cell.SlottedIn.HasRegisteredEvent("CanBeSlotted");
			if (flag3 || flag4)
			{
				Event @event = Event.New("CanBeSlotted", "Object", Object, "Cell", Cell);
				if (flag3 && !Object.FireEvent(@event))
				{
					return;
				}
				if (flag4)
				{
					Cell.SlottedIn.FireEvent(@event);
					return;
				}
			}
		}

		// Token: 0x04002372 RID: 9074
		public GameObject Object;

		// Token: 0x04002373 RID: 9075
		public IEnergyCell Cell;
	}
}
