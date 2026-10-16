using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007A0 RID: 1952
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetUnknownShortDescriptionEvent : IShortDescriptionEvent
	{
		// Token: 0x06004F96 RID: 20374 RVA: 0x00184BD6 File Offset: 0x00182DD6
		public GetUnknownShortDescriptionEvent()
		{
			this.ID = GetUnknownShortDescriptionEvent.ID;
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x00184BE9 File Offset: 0x00182DE9
		public static int CountPool()
		{
			if (GetUnknownShortDescriptionEvent.Pool != null)
			{
				return GetUnknownShortDescriptionEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x00184BFE File Offset: 0x00182DFE
		public static void ResetPool()
		{
			while (GetUnknownShortDescriptionEvent.PoolCounter > 0)
			{
				GetUnknownShortDescriptionEvent.Pool[--GetUnknownShortDescriptionEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x00184C26 File Offset: 0x00182E26
		public static void ResetTo(ref GetUnknownShortDescriptionEvent E)
		{
			MinEvent.ResetTo<GetUnknownShortDescriptionEvent>(E, GetUnknownShortDescriptionEvent.Pool, ref GetUnknownShortDescriptionEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x00184C3C File Offset: 0x00182E3C
		public static GetUnknownShortDescriptionEvent FromPool()
		{
			return MinEvent.FromPool<GetUnknownShortDescriptionEvent>(ref GetUnknownShortDescriptionEvent.Pool, ref GetUnknownShortDescriptionEvent.PoolCounter, 8192);
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x00184C52 File Offset: 0x00182E52
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x00184C66 File Offset: 0x00182E66
		public static GetUnknownShortDescriptionEvent FromPool(GameObject Object, string Base, string Context = null, bool AsIfKnown = false)
		{
			GetUnknownShortDescriptionEvent getUnknownShortDescriptionEvent = GetUnknownShortDescriptionEvent.FromPool();
			getUnknownShortDescriptionEvent.Object = Object;
			getUnknownShortDescriptionEvent.Context = Context;
			getUnknownShortDescriptionEvent.AsIfKnown = AsIfKnown;
			getUnknownShortDescriptionEvent.Base.Append(Base);
			return getUnknownShortDescriptionEvent;
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x00184C8F File Offset: 0x00182E8F
		public override string GetRegisteredEventID()
		{
			return "GetUnknownShortDescription";
		}

		// Token: 0x04002752 RID: 10066
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetUnknownShortDescriptionEvent), null, new MinEvent.EventPoolCount(GetUnknownShortDescriptionEvent.CountPool), new MinEvent.EventPoolReset(GetUnknownShortDescriptionEvent.ResetPool));

		// Token: 0x04002753 RID: 10067
		private static List<GetUnknownShortDescriptionEvent> Pool;

		// Token: 0x04002754 RID: 10068
		private static int PoolCounter;
	}
}
