using System;

namespace XRL.World
{
	// Token: 0x020006D8 RID: 1752
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckAnythingToCleanWithNearbyEvent : PooledEvent<CheckAnythingToCleanWithNearbyEvent>
	{
		// Token: 0x060049E8 RID: 18920 RVA: 0x00172D81 File Offset: 0x00170F81
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x00172D8A File Offset: 0x00170F8A
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Item = null;
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x00172DA0 File Offset: 0x00170FA0
		public static CheckAnythingToCleanWithNearbyEvent FromPool(GameObject Actor, GameObject Item)
		{
			CheckAnythingToCleanWithNearbyEvent checkAnythingToCleanWithNearbyEvent = PooledEvent<CheckAnythingToCleanWithNearbyEvent>.FromPool();
			checkAnythingToCleanWithNearbyEvent.Actor = Actor;
			checkAnythingToCleanWithNearbyEvent.Item = Item;
			return checkAnythingToCleanWithNearbyEvent;
		}

		// Token: 0x060049EB RID: 18923 RVA: 0x00172DB8 File Offset: 0x00170FB8
		public static bool Check(GameObject Actor, GameObject Item)
		{
			Cell cell = ((Actor != null) ? Actor.CurrentCell : null);
			if (cell != null)
			{
				CheckAnythingToCleanWithNearbyEvent checkAnythingToCleanWithNearbyEvent = null;
				if (cell.WantEvent(PooledEvent<CheckAnythingToCleanWithNearbyEvent>.ID, MinEvent.CascadeLevel))
				{
					if (checkAnythingToCleanWithNearbyEvent == null)
					{
						checkAnythingToCleanWithNearbyEvent = CheckAnythingToCleanWithNearbyEvent.FromPool(Actor, Item);
					}
					if (!cell.HandleEvent(checkAnythingToCleanWithNearbyEvent))
					{
						return true;
					}
				}
				foreach (Cell cell2 in cell.GetLocalAdjacentCells())
				{
					if (cell2.WantEvent(PooledEvent<CheckAnythingToCleanWithNearbyEvent>.ID, MinEvent.CascadeLevel))
					{
						if (checkAnythingToCleanWithNearbyEvent == null)
						{
							checkAnythingToCleanWithNearbyEvent = CheckAnythingToCleanWithNearbyEvent.FromPool(Actor, Item);
						}
						if (!cell2.HandleEvent(checkAnythingToCleanWithNearbyEvent))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x04002381 RID: 9089
		public GameObject Actor;

		// Token: 0x04002382 RID: 9090
		public GameObject Item;
	}
}
