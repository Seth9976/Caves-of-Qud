using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007A1 RID: 1953
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetUtilityScoreEvent : IActOnItemEvent
	{
		// Token: 0x06004F9F RID: 20383 RVA: 0x00184CC5 File Offset: 0x00182EC5
		public GetUtilityScoreEvent()
		{
			this.ID = GetUtilityScoreEvent.ID;
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x00184CD8 File Offset: 0x00182ED8
		public static int CountPool()
		{
			if (GetUtilityScoreEvent.Pool != null)
			{
				return GetUtilityScoreEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x00184CED File Offset: 0x00182EED
		public static void ResetPool()
		{
			while (GetUtilityScoreEvent.PoolCounter > 0)
			{
				GetUtilityScoreEvent.Pool[--GetUtilityScoreEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x00184D15 File Offset: 0x00182F15
		public static void ResetTo(ref GetUtilityScoreEvent E)
		{
			MinEvent.ResetTo<GetUtilityScoreEvent>(E, GetUtilityScoreEvent.Pool, ref GetUtilityScoreEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x00184D2B File Offset: 0x00182F2B
		public static GetUtilityScoreEvent FromPool()
		{
			return MinEvent.FromPool<GetUtilityScoreEvent>(ref GetUtilityScoreEvent.Pool, ref GetUtilityScoreEvent.PoolCounter, 8192);
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x00184D41 File Offset: 0x00182F41
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x00184D55 File Offset: 0x00182F55
		public override void Reset()
		{
			base.Reset();
			this.Damage = null;
			this.ForPermission = false;
			this.Score = 0;
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x00184D72 File Offset: 0x00182F72
		public void ApplyScore(int Score)
		{
			if (this.Score < Score)
			{
				this.Score = Score;
			}
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x00184D84 File Offset: 0x00182F84
		public static int GetFor(GameObject Actor, GameObject Item, Damage Damage = null, bool ForPermission = false)
		{
			if (Item.WantEvent(GetUtilityScoreEvent.ID, MinEvent.CascadeLevel))
			{
				GetUtilityScoreEvent getUtilityScoreEvent = GetUtilityScoreEvent.FromPool();
				getUtilityScoreEvent.Actor = Actor;
				getUtilityScoreEvent.Item = Item;
				getUtilityScoreEvent.Damage = Damage;
				getUtilityScoreEvent.ForPermission = ForPermission;
				getUtilityScoreEvent.Score = 0;
				Item.HandleEvent(getUtilityScoreEvent);
				return getUtilityScoreEvent.Score;
			}
			return 0;
		}

		// Token: 0x04002755 RID: 10069
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetUtilityScoreEvent), null, new MinEvent.EventPoolCount(GetUtilityScoreEvent.CountPool), new MinEvent.EventPoolReset(GetUtilityScoreEvent.ResetPool));

		// Token: 0x04002756 RID: 10070
		private static List<GetUtilityScoreEvent> Pool;

		// Token: 0x04002757 RID: 10071
		private static int PoolCounter;

		// Token: 0x04002758 RID: 10072
		public Damage Damage;

		// Token: 0x04002759 RID: 10073
		public bool ForPermission;

		// Token: 0x0400275A RID: 10074
		public int Score;
	}
}
