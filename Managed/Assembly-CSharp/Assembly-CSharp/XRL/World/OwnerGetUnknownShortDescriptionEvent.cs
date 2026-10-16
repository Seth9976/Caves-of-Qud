using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200081F RID: 2079
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class OwnerGetUnknownShortDescriptionEvent : IShortDescriptionEvent
	{
		// Token: 0x060052D7 RID: 21207 RVA: 0x0018E0FC File Offset: 0x0018C2FC
		public OwnerGetUnknownShortDescriptionEvent()
		{
			this.ID = OwnerGetUnknownShortDescriptionEvent.ID;
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x0018E10F File Offset: 0x0018C30F
		public static int CountPool()
		{
			if (OwnerGetUnknownShortDescriptionEvent.Pool != null)
			{
				return OwnerGetUnknownShortDescriptionEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x0018E124 File Offset: 0x0018C324
		public static void ResetPool()
		{
			while (OwnerGetUnknownShortDescriptionEvent.PoolCounter > 0)
			{
				OwnerGetUnknownShortDescriptionEvent.Pool[--OwnerGetUnknownShortDescriptionEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x0018E14C File Offset: 0x0018C34C
		public static void ResetTo(ref OwnerGetUnknownShortDescriptionEvent E)
		{
			MinEvent.ResetTo<OwnerGetUnknownShortDescriptionEvent>(E, OwnerGetUnknownShortDescriptionEvent.Pool, ref OwnerGetUnknownShortDescriptionEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x0018E162 File Offset: 0x0018C362
		public static OwnerGetUnknownShortDescriptionEvent FromPool()
		{
			return MinEvent.FromPool<OwnerGetUnknownShortDescriptionEvent>(ref OwnerGetUnknownShortDescriptionEvent.Pool, ref OwnerGetUnknownShortDescriptionEvent.PoolCounter, 8192);
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x0018E178 File Offset: 0x0018C378
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x0018E18C File Offset: 0x0018C38C
		public static OwnerGetUnknownShortDescriptionEvent FromPool(string Base, string Context = null, bool AsIfKnown = false)
		{
			OwnerGetUnknownShortDescriptionEvent ownerGetUnknownShortDescriptionEvent = OwnerGetUnknownShortDescriptionEvent.FromPool();
			ownerGetUnknownShortDescriptionEvent.Context = Context;
			ownerGetUnknownShortDescriptionEvent.AsIfKnown = AsIfKnown;
			ownerGetUnknownShortDescriptionEvent.Base.Append(Base);
			return ownerGetUnknownShortDescriptionEvent;
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x0018E1AE File Offset: 0x0018C3AE
		public override string GetRegisteredEventID()
		{
			return "OwnerGetUnknownShortDescription";
		}

		// Token: 0x040029A6 RID: 10662
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(OwnerGetUnknownShortDescriptionEvent), null, new MinEvent.EventPoolCount(OwnerGetUnknownShortDescriptionEvent.CountPool), new MinEvent.EventPoolReset(OwnerGetUnknownShortDescriptionEvent.ResetPool));

		// Token: 0x040029A7 RID: 10663
		private static List<OwnerGetUnknownShortDescriptionEvent> Pool;

		// Token: 0x040029A8 RID: 10664
		private static int PoolCounter;
	}
}
