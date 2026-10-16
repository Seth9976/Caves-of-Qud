using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000749 RID: 1865
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetExtrinsicValueEvent : IValueEvent
	{
		// Token: 0x06004D2F RID: 19759 RVA: 0x0017C549 File Offset: 0x0017A749
		public GetExtrinsicValueEvent()
		{
			this.ID = GetExtrinsicValueEvent.ID;
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x0017C55C File Offset: 0x0017A75C
		public static int CountPool()
		{
			if (GetExtrinsicValueEvent.Pool != null)
			{
				return GetExtrinsicValueEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x0017C571 File Offset: 0x0017A771
		public static void ResetPool()
		{
			while (GetExtrinsicValueEvent.PoolCounter > 0)
			{
				GetExtrinsicValueEvent.Pool[--GetExtrinsicValueEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0017C599 File Offset: 0x0017A799
		public static void ResetTo(ref GetExtrinsicValueEvent E)
		{
			MinEvent.ResetTo<GetExtrinsicValueEvent>(E, GetExtrinsicValueEvent.Pool, ref GetExtrinsicValueEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0017C5AF File Offset: 0x0017A7AF
		public static GetExtrinsicValueEvent FromPool()
		{
			return MinEvent.FromPool<GetExtrinsicValueEvent>(ref GetExtrinsicValueEvent.Pool, ref GetExtrinsicValueEvent.PoolCounter, 8192);
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x0017C5C5 File Offset: 0x0017A7C5
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x0017C5D9 File Offset: 0x0017A7D9
		public static GetExtrinsicValueEvent FromPool(GameObject Object, double Value)
		{
			GetExtrinsicValueEvent getExtrinsicValueEvent = GetExtrinsicValueEvent.FromPool();
			getExtrinsicValueEvent.Object = Object;
			getExtrinsicValueEvent.Value = Value;
			return getExtrinsicValueEvent;
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x0017C5EE File Offset: 0x0017A7EE
		public static GetExtrinsicValueEvent FromPool(IValueEvent PE)
		{
			GetExtrinsicValueEvent getExtrinsicValueEvent = GetExtrinsicValueEvent.FromPool();
			getExtrinsicValueEvent.Object = PE.Object;
			getExtrinsicValueEvent.Value = PE.Value;
			return getExtrinsicValueEvent;
		}

		// Token: 0x0400256D RID: 9581
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetExtrinsicValueEvent), null, new MinEvent.EventPoolCount(GetExtrinsicValueEvent.CountPool), new MinEvent.EventPoolReset(GetExtrinsicValueEvent.ResetPool));

		// Token: 0x0400256E RID: 9582
		private static List<GetExtrinsicValueEvent> Pool;

		// Token: 0x0400256F RID: 9583
		private static int PoolCounter;
	}
}
