using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000730 RID: 1840
	[GameEvent(Cascade = 7, Cache = MinEvent.Cache.Pool)]
	public class GetCleaningItemsEvent : PooledEvent<GetCleaningItemsEvent>
	{
		// Token: 0x06004C4E RID: 19534 RVA: 0x001796AB File Offset: 0x001778AB
		public override int GetCascadeLevel()
		{
			return GetCleaningItemsEvent.CascadeLevel;
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x001796B2 File Offset: 0x001778B2
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x001796BB File Offset: 0x001778BB
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Item = null;
			this.Objects.Clear();
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x001796DC File Offset: 0x001778DC
		public static GetCleaningItemsEvent FromPool(GameObject Actor, GameObject Item)
		{
			GetCleaningItemsEvent getCleaningItemsEvent = PooledEvent<GetCleaningItemsEvent>.FromPool();
			getCleaningItemsEvent.Actor = Actor;
			getCleaningItemsEvent.Item = Item;
			getCleaningItemsEvent.Objects.Clear();
			return getCleaningItemsEvent;
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x001796FC File Offset: 0x001778FC
		public static List<GameObject> GetFor(GameObject Actor, GameObject Item)
		{
			GetCleaningItemsEvent getCleaningItemsEvent = null;
			List<GameObject> list = null;
			if (GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetCleaningItemsEvent>.ID, GetCleaningItemsEvent.CascadeLevel))
			{
				if (getCleaningItemsEvent == null)
				{
					getCleaningItemsEvent = GetCleaningItemsEvent.FromPool(Actor, Item);
					list = getCleaningItemsEvent.Objects;
				}
				Actor.HandleEvent(getCleaningItemsEvent);
			}
			GetCleaningItemsNearbyEvent.Send(Actor, Item, ref list);
			return list;
		}

		// Token: 0x040024E9 RID: 9449
		public new static readonly int CascadeLevel = 7;

		// Token: 0x040024EA RID: 9450
		public GameObject Actor;

		// Token: 0x040024EB RID: 9451
		public GameObject Item;

		// Token: 0x040024EC RID: 9452
		public List<GameObject> Objects = new List<GameObject>();
	}
}
