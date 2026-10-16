using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000728 RID: 1832
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetAttackerHitDiceEvent : IHitDiceEvent
	{
		// Token: 0x06004C04 RID: 19460 RVA: 0x00178911 File Offset: 0x00176B11
		public GetAttackerHitDiceEvent()
		{
			this.ID = GetAttackerHitDiceEvent.ID;
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x00178924 File Offset: 0x00176B24
		public static int CountPool()
		{
			if (GetAttackerHitDiceEvent.Pool != null)
			{
				return GetAttackerHitDiceEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x00178939 File Offset: 0x00176B39
		public static void ResetPool()
		{
			while (GetAttackerHitDiceEvent.PoolCounter > 0)
			{
				GetAttackerHitDiceEvent.Pool[--GetAttackerHitDiceEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x00178961 File Offset: 0x00176B61
		public static void ResetTo(ref GetAttackerHitDiceEvent E)
		{
			MinEvent.ResetTo<GetAttackerHitDiceEvent>(E, GetAttackerHitDiceEvent.Pool, ref GetAttackerHitDiceEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x00178977 File Offset: 0x00176B77
		public static GetAttackerHitDiceEvent FromPool()
		{
			return MinEvent.FromPool<GetAttackerHitDiceEvent>(ref GetAttackerHitDiceEvent.Pool, ref GetAttackerHitDiceEvent.PoolCounter, 8192);
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x0017898D File Offset: 0x00176B8D
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x001789A1 File Offset: 0x00176BA1
		public static GetAttackerHitDiceEvent FromPool(GameObject Attacker, GameObject Defender, GameObject Weapon, int PenetrationBonus, int AV, bool ShieldBlocked)
		{
			GetAttackerHitDiceEvent getAttackerHitDiceEvent = GetAttackerHitDiceEvent.FromPool();
			getAttackerHitDiceEvent.Attacker = Attacker;
			getAttackerHitDiceEvent.Defender = Defender;
			getAttackerHitDiceEvent.Weapon = Weapon;
			getAttackerHitDiceEvent.PenetrationBonus = PenetrationBonus;
			getAttackerHitDiceEvent.AV = AV;
			getAttackerHitDiceEvent.ShieldBlocked = ShieldBlocked;
			return getAttackerHitDiceEvent;
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x001789D4 File Offset: 0x00176BD4
		public static bool Process(ref int PenetrationBonus, ref int AV, ref bool ShieldBlocked, GameObject Attacker, GameObject Defender, GameObject Weapon)
		{
			return IHitDiceEvent.Process(ref PenetrationBonus, ref AV, ref ShieldBlocked, Attacker, Defender, Weapon, "GetAttackerHitDice", Attacker, GetAttackerHitDiceEvent.ID, IHitDiceEvent.CascadeLevel, new Func<GameObject, GameObject, GameObject, int, int, bool, IHitDiceEvent>(GetAttackerHitDiceEvent.FromPool));
		}

		// Token: 0x040024C9 RID: 9417
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetAttackerHitDiceEvent), null, new MinEvent.EventPoolCount(GetAttackerHitDiceEvent.CountPool), new MinEvent.EventPoolReset(GetAttackerHitDiceEvent.ResetPool));

		// Token: 0x040024CA RID: 9418
		private static List<GetAttackerHitDiceEvent> Pool;

		// Token: 0x040024CB RID: 9419
		private static int PoolCounter;
	}
}
