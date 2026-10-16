using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000755 RID: 1877
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetIntrinsicValueEvent : IValueEvent
	{
		// Token: 0x06004D85 RID: 19845 RVA: 0x0017D46E File Offset: 0x0017B66E
		public GetIntrinsicValueEvent()
		{
			this.ID = GetIntrinsicValueEvent.ID;
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x0017D481 File Offset: 0x0017B681
		public static int CountPool()
		{
			if (GetIntrinsicValueEvent.Pool != null)
			{
				return GetIntrinsicValueEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x0017D496 File Offset: 0x0017B696
		public static void ResetPool()
		{
			while (GetIntrinsicValueEvent.PoolCounter > 0)
			{
				GetIntrinsicValueEvent.Pool[--GetIntrinsicValueEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x0017D4BE File Offset: 0x0017B6BE
		public static void ResetTo(ref GetIntrinsicValueEvent E)
		{
			MinEvent.ResetTo<GetIntrinsicValueEvent>(E, GetIntrinsicValueEvent.Pool, ref GetIntrinsicValueEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x0017D4D4 File Offset: 0x0017B6D4
		public static GetIntrinsicValueEvent FromPool()
		{
			return MinEvent.FromPool<GetIntrinsicValueEvent>(ref GetIntrinsicValueEvent.Pool, ref GetIntrinsicValueEvent.PoolCounter, 8192);
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x0017D4EA File Offset: 0x0017B6EA
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x0017D4FE File Offset: 0x0017B6FE
		public static GetIntrinsicValueEvent FromPool(GameObject Object, double Value)
		{
			GetIntrinsicValueEvent getIntrinsicValueEvent = GetIntrinsicValueEvent.FromPool();
			getIntrinsicValueEvent.Object = Object;
			getIntrinsicValueEvent.Value = Value;
			return getIntrinsicValueEvent;
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x0017D514 File Offset: 0x0017B714
		public static GetIntrinsicValueEvent FromPool(GameObject Object)
		{
			GetIntrinsicValueEvent getIntrinsicValueEvent = GetIntrinsicValueEvent.FromPool();
			getIntrinsicValueEvent.Object = Object;
			Commerce part = Object.GetPart<Commerce>();
			if (part != null)
			{
				getIntrinsicValueEvent.Value = part.Value;
			}
			return getIntrinsicValueEvent;
		}

		// Token: 0x040025A3 RID: 9635
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetIntrinsicValueEvent), null, new MinEvent.EventPoolCount(GetIntrinsicValueEvent.CountPool), new MinEvent.EventPoolReset(GetIntrinsicValueEvent.ResetPool));

		// Token: 0x040025A4 RID: 9636
		private static List<GetIntrinsicValueEvent> Pool;

		// Token: 0x040025A5 RID: 9637
		private static int PoolCounter;
	}
}
