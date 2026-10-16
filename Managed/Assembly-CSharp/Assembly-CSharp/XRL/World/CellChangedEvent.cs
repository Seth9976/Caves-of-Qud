using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006D2 RID: 1746
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CellChangedEvent : IActOnItemEvent
	{
		// Token: 0x060049B6 RID: 18870 RVA: 0x001725E0 File Offset: 0x001707E0
		public CellChangedEvent()
		{
			this.ID = CellChangedEvent.ID;
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x001725F3 File Offset: 0x001707F3
		public static int CountPool()
		{
			if (CellChangedEvent.Pool != null)
			{
				return CellChangedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x00172608 File Offset: 0x00170808
		public static void ResetPool()
		{
			while (CellChangedEvent.PoolCounter > 0)
			{
				CellChangedEvent.Pool[--CellChangedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x00172630 File Offset: 0x00170830
		public static void ResetTo(ref CellChangedEvent E)
		{
			MinEvent.ResetTo<CellChangedEvent>(E, CellChangedEvent.Pool, ref CellChangedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x00172646 File Offset: 0x00170846
		public static CellChangedEvent FromPool()
		{
			return MinEvent.FromPool<CellChangedEvent>(ref CellChangedEvent.Pool, ref CellChangedEvent.PoolCounter, 8192);
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x0017265C File Offset: 0x0017085C
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x00172670 File Offset: 0x00170870
		public override void Reset()
		{
			base.Reset();
			this.OldCell = null;
			this.NewCell = null;
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x00172686 File Offset: 0x00170886
		public static CellChangedEvent FromPool(GameObject Actor, GameObject Item, GameObject OldCell, GameObject NewCell)
		{
			CellChangedEvent cellChangedEvent = CellChangedEvent.FromPool();
			cellChangedEvent.Actor = Actor;
			cellChangedEvent.Item = Item;
			cellChangedEvent.OldCell = OldCell;
			cellChangedEvent.NewCell = NewCell;
			return cellChangedEvent;
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x001726AC File Offset: 0x001708AC
		public static void Send(GameObject Actor, GameObject Item, GameObject OldCell, GameObject NewCell)
		{
			if (Item.HasRegisteredEvent("CellChanged"))
			{
				Event @event = Event.New("CellChanged", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Item", Item);
				@event.SetParameter("OldCell", OldCell);
				@event.SetParameter("NewCell", NewCell);
				if (!Item.FireEvent(@event))
				{
					return;
				}
			}
			if (Item.WantEvent(CellChangedEvent.ID, MinEvent.CascadeLevel))
			{
				Item.HandleEvent(CellChangedEvent.FromPool(Actor, Item, OldCell, NewCell));
			}
		}

		// Token: 0x0400236D RID: 9069
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CellChangedEvent), null, new MinEvent.EventPoolCount(CellChangedEvent.CountPool), new MinEvent.EventPoolReset(CellChangedEvent.ResetPool));

		// Token: 0x0400236E RID: 9070
		private static List<CellChangedEvent> Pool;

		// Token: 0x0400236F RID: 9071
		private static int PoolCounter;

		// Token: 0x04002370 RID: 9072
		public GameObject OldCell;

		// Token: 0x04002371 RID: 9073
		public GameObject NewCell;
	}
}
