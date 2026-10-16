using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200073F RID: 1855
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetDefenderHitDiceEvent : IHitDiceEvent
	{
		// Token: 0x06004CD4 RID: 19668 RVA: 0x0017B12B File Offset: 0x0017932B
		public GetDefenderHitDiceEvent()
		{
			this.ID = GetDefenderHitDiceEvent.ID;
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x0017B13E File Offset: 0x0017933E
		public static int CountPool()
		{
			if (GetDefenderHitDiceEvent.Pool != null)
			{
				return GetDefenderHitDiceEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x0017B153 File Offset: 0x00179353
		public static void ResetPool()
		{
			while (GetDefenderHitDiceEvent.PoolCounter > 0)
			{
				GetDefenderHitDiceEvent.Pool[--GetDefenderHitDiceEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x0017B17B File Offset: 0x0017937B
		public static void ResetTo(ref GetDefenderHitDiceEvent E)
		{
			MinEvent.ResetTo<GetDefenderHitDiceEvent>(E, GetDefenderHitDiceEvent.Pool, ref GetDefenderHitDiceEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x0017B191 File Offset: 0x00179391
		public static GetDefenderHitDiceEvent FromPool()
		{
			return MinEvent.FromPool<GetDefenderHitDiceEvent>(ref GetDefenderHitDiceEvent.Pool, ref GetDefenderHitDiceEvent.PoolCounter, 8192);
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x0017B1A7 File Offset: 0x001793A7
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x0017B1BB File Offset: 0x001793BB
		public static GetDefenderHitDiceEvent FromPool(GameObject Attacker, GameObject Defender, GameObject Weapon, int PenetrationBonus, int AV, bool ShieldBlocked)
		{
			GetDefenderHitDiceEvent getDefenderHitDiceEvent = GetDefenderHitDiceEvent.FromPool();
			getDefenderHitDiceEvent.Attacker = Attacker;
			getDefenderHitDiceEvent.Defender = Defender;
			getDefenderHitDiceEvent.Weapon = Weapon;
			getDefenderHitDiceEvent.PenetrationBonus = PenetrationBonus;
			getDefenderHitDiceEvent.AV = AV;
			getDefenderHitDiceEvent.ShieldBlocked = ShieldBlocked;
			return getDefenderHitDiceEvent;
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x0017B1F0 File Offset: 0x001793F0
		public static bool Process(ref int PenetrationBonus, ref int AV, ref bool ShieldBlocked, GameObject Attacker, GameObject Defender, GameObject Weapon)
		{
			return IHitDiceEvent.Process(ref PenetrationBonus, ref AV, ref ShieldBlocked, Attacker, Defender, Weapon, "GetDefenderHitDice", Defender, GetDefenderHitDiceEvent.ID, IHitDiceEvent.CascadeLevel, new Func<GameObject, GameObject, GameObject, int, int, bool, IHitDiceEvent>(GetDefenderHitDiceEvent.FromPool));
		}

		// Token: 0x0400252B RID: 9515
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetDefenderHitDiceEvent), null, new MinEvent.EventPoolCount(GetDefenderHitDiceEvent.CountPool), new MinEvent.EventPoolReset(GetDefenderHitDiceEvent.ResetPool));

		// Token: 0x0400252C RID: 9516
		private static List<GetDefenderHitDiceEvent> Pool;

		// Token: 0x0400252D RID: 9517
		private static int PoolCounter;
	}
}
