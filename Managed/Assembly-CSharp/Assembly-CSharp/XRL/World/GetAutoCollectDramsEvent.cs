using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200072A RID: 1834
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetAutoCollectDramsEvent : ILiquidEvent
	{
		// Token: 0x06004C15 RID: 19477 RVA: 0x00178B3E File Offset: 0x00176D3E
		public GetAutoCollectDramsEvent()
		{
			this.ID = GetAutoCollectDramsEvent.ID;
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x00178B51 File Offset: 0x00176D51
		public static int CountPool()
		{
			if (GetAutoCollectDramsEvent.Pool != null)
			{
				return GetAutoCollectDramsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x00178B66 File Offset: 0x00176D66
		public static void ResetPool()
		{
			while (GetAutoCollectDramsEvent.PoolCounter > 0)
			{
				GetAutoCollectDramsEvent.Pool[--GetAutoCollectDramsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004C18 RID: 19480 RVA: 0x00178B8E File Offset: 0x00176D8E
		public static void ResetTo(ref GetAutoCollectDramsEvent E)
		{
			MinEvent.ResetTo<GetAutoCollectDramsEvent>(E, GetAutoCollectDramsEvent.Pool, ref GetAutoCollectDramsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004C19 RID: 19481 RVA: 0x00178BA4 File Offset: 0x00176DA4
		public static GetAutoCollectDramsEvent FromPool()
		{
			return MinEvent.FromPool<GetAutoCollectDramsEvent>(ref GetAutoCollectDramsEvent.Pool, ref GetAutoCollectDramsEvent.PoolCounter, 8192);
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x00178BBA File Offset: 0x00176DBA
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x00178BD0 File Offset: 0x00176DD0
		public static int GetFor(GameObject Actor, string Liquid = "water", GameObject Skip = null, List<GameObject> SkipList = null)
		{
			if (Actor.WantEvent(GetAutoCollectDramsEvent.ID, ILiquidEvent.CascadeLevel))
			{
				GetAutoCollectDramsEvent getAutoCollectDramsEvent = GetAutoCollectDramsEvent.FromPool();
				getAutoCollectDramsEvent.Actor = Actor;
				getAutoCollectDramsEvent.Liquid = Liquid;
				getAutoCollectDramsEvent.LiquidVolume = null;
				getAutoCollectDramsEvent.Drams = 0;
				getAutoCollectDramsEvent.Skip = Skip;
				getAutoCollectDramsEvent.SkipList = SkipList;
				getAutoCollectDramsEvent.Filter = null;
				getAutoCollectDramsEvent.Auto = true;
				getAutoCollectDramsEvent.ImpureOkay = false;
				getAutoCollectDramsEvent.SafeOnly = false;
				Actor.HandleEvent(getAutoCollectDramsEvent);
				return getAutoCollectDramsEvent.Drams;
			}
			return 0;
		}

		// Token: 0x040024CF RID: 9423
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetAutoCollectDramsEvent), null, new MinEvent.EventPoolCount(GetAutoCollectDramsEvent.CountPool), new MinEvent.EventPoolReset(GetAutoCollectDramsEvent.ResetPool));

		// Token: 0x040024D0 RID: 9424
		private static List<GetAutoCollectDramsEvent> Pool;

		// Token: 0x040024D1 RID: 9425
		private static int PoolCounter;
	}
}
