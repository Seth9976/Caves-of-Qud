using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000789 RID: 1929
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetShortDescriptionEvent : IShortDescriptionEvent
	{
		// Token: 0x06004EF9 RID: 20217 RVA: 0x001821BA File Offset: 0x001803BA
		public GetShortDescriptionEvent()
		{
			this.ID = GetShortDescriptionEvent.ID;
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x001821CD File Offset: 0x001803CD
		public static int CountPool()
		{
			if (GetShortDescriptionEvent.Pool != null)
			{
				return GetShortDescriptionEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x001821E2 File Offset: 0x001803E2
		public static void ResetPool()
		{
			while (GetShortDescriptionEvent.PoolCounter > 0)
			{
				GetShortDescriptionEvent.Pool[--GetShortDescriptionEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004EFC RID: 20220 RVA: 0x0018220A File Offset: 0x0018040A
		public static void ResetTo(ref GetShortDescriptionEvent E)
		{
			MinEvent.ResetTo<GetShortDescriptionEvent>(E, GetShortDescriptionEvent.Pool, ref GetShortDescriptionEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004EFD RID: 20221 RVA: 0x00182220 File Offset: 0x00180420
		public static GetShortDescriptionEvent FromPool()
		{
			return MinEvent.FromPool<GetShortDescriptionEvent>(ref GetShortDescriptionEvent.Pool, ref GetShortDescriptionEvent.PoolCounter, 8192);
		}

		// Token: 0x06004EFE RID: 20222 RVA: 0x00182236 File Offset: 0x00180436
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x0018224A File Offset: 0x0018044A
		public static GetShortDescriptionEvent FromPool(GameObject Object, string Base, string Context = null, bool AsIfKnown = false)
		{
			GetShortDescriptionEvent getShortDescriptionEvent = GetShortDescriptionEvent.FromPool();
			getShortDescriptionEvent.Object = Object;
			getShortDescriptionEvent.Context = Context;
			getShortDescriptionEvent.AsIfKnown = AsIfKnown;
			getShortDescriptionEvent.Base.Append(Base);
			return getShortDescriptionEvent;
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x00182273 File Offset: 0x00180473
		public override string GetRegisteredEventID()
		{
			return "GetShortDescription";
		}

		// Token: 0x040026B7 RID: 9911
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetShortDescriptionEvent), null, new MinEvent.EventPoolCount(GetShortDescriptionEvent.CountPool), new MinEvent.EventPoolReset(GetShortDescriptionEvent.ResetPool));

		// Token: 0x040026B8 RID: 9912
		private static List<GetShortDescriptionEvent> Pool;

		// Token: 0x040026B9 RID: 9913
		private static int PoolCounter;
	}
}
