using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200074F RID: 1871
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetFreeDramsEvent : ILiquidEvent
	{
		// Token: 0x06004D5B RID: 19803 RVA: 0x0017CC1D File Offset: 0x0017AE1D
		public GetFreeDramsEvent()
		{
			this.ID = GetFreeDramsEvent.ID;
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x0017CC30 File Offset: 0x0017AE30
		public static int CountPool()
		{
			if (GetFreeDramsEvent.Pool != null)
			{
				return GetFreeDramsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x0017CC45 File Offset: 0x0017AE45
		public static void ResetPool()
		{
			while (GetFreeDramsEvent.PoolCounter > 0)
			{
				GetFreeDramsEvent.Pool[--GetFreeDramsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x0017CC6D File Offset: 0x0017AE6D
		public static void ResetTo(ref GetFreeDramsEvent E)
		{
			MinEvent.ResetTo<GetFreeDramsEvent>(E, GetFreeDramsEvent.Pool, ref GetFreeDramsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x0017CC83 File Offset: 0x0017AE83
		public static GetFreeDramsEvent FromPool()
		{
			return MinEvent.FromPool<GetFreeDramsEvent>(ref GetFreeDramsEvent.Pool, ref GetFreeDramsEvent.PoolCounter, 8192);
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x0017CC99 File Offset: 0x0017AE99
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x0017CCB0 File Offset: 0x0017AEB0
		public static GetFreeDramsEvent FromPool(GameObject Actor, string Liquid = "water", GameObject Skip = null, List<GameObject> SkipList = null, Predicate<GameObject> Filter = null, bool ImpureOkay = false)
		{
			GetFreeDramsEvent getFreeDramsEvent = GetFreeDramsEvent.FromPool();
			getFreeDramsEvent.Actor = Actor;
			getFreeDramsEvent.Liquid = Liquid;
			getFreeDramsEvent.LiquidVolume = null;
			getFreeDramsEvent.Drams = 0;
			getFreeDramsEvent.Skip = Skip;
			getFreeDramsEvent.SkipList = SkipList;
			getFreeDramsEvent.Filter = Filter;
			getFreeDramsEvent.Auto = false;
			getFreeDramsEvent.ImpureOkay = ImpureOkay;
			getFreeDramsEvent.SafeOnly = false;
			return getFreeDramsEvent;
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x0017CD0C File Offset: 0x0017AF0C
		public static int GetFor(GameObject Actor, string Liquid = "water", GameObject Skip = null, List<GameObject> SkipList = null, Predicate<GameObject> Filter = null, bool ImpureOkay = false)
		{
			if (Actor.WantEvent(GetFreeDramsEvent.ID, ILiquidEvent.CascadeLevel))
			{
				GetFreeDramsEvent getFreeDramsEvent = GetFreeDramsEvent.FromPool(Actor, Liquid, Skip, SkipList, Filter, ImpureOkay);
				Actor.HandleEvent(getFreeDramsEvent);
				return getFreeDramsEvent.Drams;
			}
			return 0;
		}

		// Token: 0x0400258C RID: 9612
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetFreeDramsEvent), null, new MinEvent.EventPoolCount(GetFreeDramsEvent.CountPool), new MinEvent.EventPoolReset(GetFreeDramsEvent.ResetPool));

		// Token: 0x0400258D RID: 9613
		private static List<GetFreeDramsEvent> Pool;

		// Token: 0x0400258E RID: 9614
		private static int PoolCounter;
	}
}
