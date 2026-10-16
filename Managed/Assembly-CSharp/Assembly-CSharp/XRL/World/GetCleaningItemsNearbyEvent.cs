using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000731 RID: 1841
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetCleaningItemsNearbyEvent : PooledEvent<GetCleaningItemsNearbyEvent>
	{
		// Token: 0x06004C55 RID: 19541 RVA: 0x00179767 File Offset: 0x00177967
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x00179770 File Offset: 0x00177970
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Item = null;
			this.Objects = null;
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x0017978D File Offset: 0x0017798D
		public static GetCleaningItemsNearbyEvent FromPool(GameObject Actor, GameObject Item, List<GameObject> Objects)
		{
			GetCleaningItemsNearbyEvent getCleaningItemsNearbyEvent = PooledEvent<GetCleaningItemsNearbyEvent>.FromPool();
			getCleaningItemsNearbyEvent.Actor = Actor;
			getCleaningItemsNearbyEvent.Item = Item;
			getCleaningItemsNearbyEvent.Objects = Objects;
			return getCleaningItemsNearbyEvent;
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x001797AC File Offset: 0x001779AC
		public static void Send(GameObject Actor, GameObject Item, ref List<GameObject> Objects)
		{
			Cell cell = ((Actor != null) ? Actor.CurrentCell : null);
			if (cell != null)
			{
				GetCleaningItemsNearbyEvent getCleaningItemsNearbyEvent = null;
				if (cell.WantEvent(PooledEvent<GetCleaningItemsNearbyEvent>.ID, MinEvent.CascadeLevel))
				{
					if (getCleaningItemsNearbyEvent == null)
					{
						if (Objects == null)
						{
							Objects = new List<GameObject>();
						}
						getCleaningItemsNearbyEvent = GetCleaningItemsNearbyEvent.FromPool(Actor, Item, Objects);
					}
					if (!cell.HandleEvent(getCleaningItemsNearbyEvent))
					{
						return;
					}
				}
				foreach (Cell cell2 in cell.GetLocalAdjacentCells())
				{
					if (cell2.WantEvent(PooledEvent<GetCleaningItemsNearbyEvent>.ID, MinEvent.CascadeLevel))
					{
						if (getCleaningItemsNearbyEvent == null)
						{
							if (Objects == null)
							{
								Objects = new List<GameObject>();
							}
							getCleaningItemsNearbyEvent = GetCleaningItemsNearbyEvent.FromPool(Actor, Item, Objects);
						}
						if (!cell2.HandleEvent(getCleaningItemsNearbyEvent))
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x040024ED RID: 9453
		public GameObject Actor;

		// Token: 0x040024EE RID: 9454
		public GameObject Item;

		// Token: 0x040024EF RID: 9455
		public List<GameObject> Objects;
	}
}
