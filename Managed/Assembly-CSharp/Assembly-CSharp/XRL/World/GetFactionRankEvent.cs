using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200074B RID: 1867
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 17)]
	public class GetFactionRankEvent : MinEvent
	{
		// Token: 0x06004D41 RID: 19777 RVA: 0x0017C742 File Offset: 0x0017A942
		public GetFactionRankEvent()
		{
			this.ID = GetFactionRankEvent.ID;
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x0017C755 File Offset: 0x0017A955
		public override int GetCascadeLevel()
		{
			return GetFactionRankEvent.CascadeLevel;
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x0017C75C File Offset: 0x0017A95C
		public static int CountPool()
		{
			if (GetFactionRankEvent.Pool != null)
			{
				return GetFactionRankEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x0017C771 File Offset: 0x0017A971
		public static void ResetPool()
		{
			while (GetFactionRankEvent.PoolCounter > 0)
			{
				GetFactionRankEvent.Pool[--GetFactionRankEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x0017C799 File Offset: 0x0017A999
		public static void ResetTo(ref GetFactionRankEvent E)
		{
			MinEvent.ResetTo<GetFactionRankEvent>(E, GetFactionRankEvent.Pool, ref GetFactionRankEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x0017C7AF File Offset: 0x0017A9AF
		public static GetFactionRankEvent FromPool()
		{
			return MinEvent.FromPool<GetFactionRankEvent>(ref GetFactionRankEvent.Pool, ref GetFactionRankEvent.PoolCounter, 8192);
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x0017C7C5 File Offset: 0x0017A9C5
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x0017C7CE File Offset: 0x0017A9CE
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Faction = null;
			this.Rank = null;
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x0017C7EC File Offset: 0x0017A9EC
		public static string GetFor(GameObject Object, string Faction, string Rank = null)
		{
			if (GameObject.Validate(ref Object) && Object.WantEvent(GetFactionRankEvent.ID, GetFactionRankEvent.CascadeLevel))
			{
				GetFactionRankEvent getFactionRankEvent = GetFactionRankEvent.FromPool();
				getFactionRankEvent.Object = Object;
				getFactionRankEvent.Faction = Faction;
				getFactionRankEvent.Rank = Rank;
				Object.HandleEvent(getFactionRankEvent);
				Rank = getFactionRankEvent.Rank;
			}
			return Rank;
		}

		// Token: 0x04002573 RID: 9587
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetFactionRankEvent), null, new MinEvent.EventPoolCount(GetFactionRankEvent.CountPool), new MinEvent.EventPoolReset(GetFactionRankEvent.ResetPool));

		// Token: 0x04002574 RID: 9588
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002575 RID: 9589
		private static List<GetFactionRankEvent> Pool;

		// Token: 0x04002576 RID: 9590
		private static int PoolCounter;

		// Token: 0x04002577 RID: 9591
		public GameObject Object;

		// Token: 0x04002578 RID: 9592
		public string Faction;

		// Token: 0x04002579 RID: 9593
		public string Rank;
	}
}
