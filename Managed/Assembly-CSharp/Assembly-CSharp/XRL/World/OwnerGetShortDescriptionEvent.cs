using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200081E RID: 2078
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class OwnerGetShortDescriptionEvent : IShortDescriptionEvent
	{
		// Token: 0x060052CE RID: 21198 RVA: 0x0018E014 File Offset: 0x0018C214
		public OwnerGetShortDescriptionEvent()
		{
			this.ID = OwnerGetShortDescriptionEvent.ID;
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x0018E027 File Offset: 0x0018C227
		public static int CountPool()
		{
			if (OwnerGetShortDescriptionEvent.Pool != null)
			{
				return OwnerGetShortDescriptionEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x0018E03C File Offset: 0x0018C23C
		public static void ResetPool()
		{
			while (OwnerGetShortDescriptionEvent.PoolCounter > 0)
			{
				OwnerGetShortDescriptionEvent.Pool[--OwnerGetShortDescriptionEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x0018E064 File Offset: 0x0018C264
		public static void ResetTo(ref OwnerGetShortDescriptionEvent E)
		{
			MinEvent.ResetTo<OwnerGetShortDescriptionEvent>(E, OwnerGetShortDescriptionEvent.Pool, ref OwnerGetShortDescriptionEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x0018E07A File Offset: 0x0018C27A
		public static OwnerGetShortDescriptionEvent FromPool()
		{
			return MinEvent.FromPool<OwnerGetShortDescriptionEvent>(ref OwnerGetShortDescriptionEvent.Pool, ref OwnerGetShortDescriptionEvent.PoolCounter, 8192);
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x0018E090 File Offset: 0x0018C290
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x0018E0A4 File Offset: 0x0018C2A4
		public static OwnerGetShortDescriptionEvent FromPool(string Base, string Context = null, bool AsIfKnown = false)
		{
			OwnerGetShortDescriptionEvent ownerGetShortDescriptionEvent = OwnerGetShortDescriptionEvent.FromPool();
			ownerGetShortDescriptionEvent.Context = Context;
			ownerGetShortDescriptionEvent.AsIfKnown = AsIfKnown;
			ownerGetShortDescriptionEvent.Base.Append(Base);
			return ownerGetShortDescriptionEvent;
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x0018E0C6 File Offset: 0x0018C2C6
		public override string GetRegisteredEventID()
		{
			return "OwnerGetShortDescription";
		}

		// Token: 0x040029A3 RID: 10659
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(OwnerGetShortDescriptionEvent), null, new MinEvent.EventPoolCount(OwnerGetShortDescriptionEvent.CountPool), new MinEvent.EventPoolReset(OwnerGetShortDescriptionEvent.ResetPool));

		// Token: 0x040029A4 RID: 10660
		private static List<OwnerGetShortDescriptionEvent> Pool;

		// Token: 0x040029A5 RID: 10661
		private static int PoolCounter;
	}
}
