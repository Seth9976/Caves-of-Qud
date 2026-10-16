using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200064D RID: 1613
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AddedToInventoryEvent : IActOnItemEvent
	{
		// Token: 0x060045E6 RID: 17894 RVA: 0x001654B7 File Offset: 0x001636B7
		public AddedToInventoryEvent()
		{
			this.ID = AddedToInventoryEvent.ID;
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x001654CA File Offset: 0x001636CA
		public static int CountPool()
		{
			if (AddedToInventoryEvent.Pool != null)
			{
				return AddedToInventoryEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x001654DF File Offset: 0x001636DF
		public static void ResetPool()
		{
			while (AddedToInventoryEvent.PoolCounter > 0)
			{
				AddedToInventoryEvent.Pool[--AddedToInventoryEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x00165507 File Offset: 0x00163707
		public static void ResetTo(ref AddedToInventoryEvent E)
		{
			MinEvent.ResetTo<AddedToInventoryEvent>(E, AddedToInventoryEvent.Pool, ref AddedToInventoryEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x0016551D File Offset: 0x0016371D
		public static AddedToInventoryEvent FromPool()
		{
			return MinEvent.FromPool<AddedToInventoryEvent>(ref AddedToInventoryEvent.Pool, ref AddedToInventoryEvent.PoolCounter, 8192);
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x00165533 File Offset: 0x00163733
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x00165547 File Offset: 0x00163747
		public override void Reset()
		{
			base.Reset();
			this.Silent = false;
			this.NoStack = false;
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x00165560 File Offset: 0x00163760
		public static void Send(GameObject Actor, GameObject Item, bool Silent = false, bool NoStack = false, IEvent ParentEvent = null)
		{
			bool flag = true;
			if (GameObject.Validate(ref Item) && Item.HasRegisteredEvent("AddedToInventory"))
			{
				Event @event = Event.New("AddedToInventory", 0, 0, 0);
				@event.SetParameter("TakingObject", Actor);
				@event.SetParameter("Object", Item);
				@event.SetFlag("NoStack", NoStack);
				@event.SetSilent(Silent);
				flag = Item.FireEvent(@event);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(@event);
				}
			}
			if (flag && GameObject.Validate(ref Item) && Item.WantEvent(AddedToInventoryEvent.ID, MinEvent.CascadeLevel))
			{
				AddedToInventoryEvent addedToInventoryEvent = AddedToInventoryEvent.FromPool();
				addedToInventoryEvent.Actor = Actor;
				addedToInventoryEvent.Item = Item;
				addedToInventoryEvent.Silent = Silent;
				addedToInventoryEvent.NoStack = NoStack;
				flag = Item.HandleEvent(addedToInventoryEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(addedToInventoryEvent);
				}
			}
		}

		// Token: 0x0400212F RID: 8495
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AddedToInventoryEvent), null, new MinEvent.EventPoolCount(AddedToInventoryEvent.CountPool), new MinEvent.EventPoolReset(AddedToInventoryEvent.ResetPool));

		// Token: 0x04002130 RID: 8496
		private static List<AddedToInventoryEvent> Pool;

		// Token: 0x04002131 RID: 8497
		private static int PoolCounter;

		// Token: 0x04002132 RID: 8498
		public bool Silent;

		// Token: 0x04002133 RID: 8499
		public bool NoStack;
	}
}
