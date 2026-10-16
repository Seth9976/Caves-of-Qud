using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007A8 RID: 1960
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetWeaponHitDiceEvent : IHitDiceEvent
	{
		// Token: 0x06004FCA RID: 20426 RVA: 0x001854BB File Offset: 0x001836BB
		public GetWeaponHitDiceEvent()
		{
			this.ID = GetWeaponHitDiceEvent.ID;
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x001854CE File Offset: 0x001836CE
		public static int CountPool()
		{
			if (GetWeaponHitDiceEvent.Pool != null)
			{
				return GetWeaponHitDiceEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x001854E3 File Offset: 0x001836E3
		public static void ResetPool()
		{
			while (GetWeaponHitDiceEvent.PoolCounter > 0)
			{
				GetWeaponHitDiceEvent.Pool[--GetWeaponHitDiceEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x0018550B File Offset: 0x0018370B
		public static void ResetTo(ref GetWeaponHitDiceEvent E)
		{
			MinEvent.ResetTo<GetWeaponHitDiceEvent>(E, GetWeaponHitDiceEvent.Pool, ref GetWeaponHitDiceEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x00185521 File Offset: 0x00183721
		public static GetWeaponHitDiceEvent FromPool()
		{
			return MinEvent.FromPool<GetWeaponHitDiceEvent>(ref GetWeaponHitDiceEvent.Pool, ref GetWeaponHitDiceEvent.PoolCounter, 8192);
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x00185537 File Offset: 0x00183737
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x0018554B File Offset: 0x0018374B
		public static GetWeaponHitDiceEvent FromPool(GameObject Attacker, GameObject Defender, GameObject Weapon, int PenetrationBonus, int AV, bool ShieldBlocked)
		{
			GetWeaponHitDiceEvent getWeaponHitDiceEvent = GetWeaponHitDiceEvent.FromPool();
			getWeaponHitDiceEvent.Attacker = Attacker;
			getWeaponHitDiceEvent.Defender = Defender;
			getWeaponHitDiceEvent.Weapon = Weapon;
			getWeaponHitDiceEvent.PenetrationBonus = PenetrationBonus;
			getWeaponHitDiceEvent.AV = AV;
			getWeaponHitDiceEvent.ShieldBlocked = ShieldBlocked;
			return getWeaponHitDiceEvent;
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x00185580 File Offset: 0x00183780
		public static bool Process(ref int PenetrationBonus, ref int AV, ref bool ShieldBlocked, GameObject Attacker, GameObject Defender, GameObject Weapon)
		{
			return IHitDiceEvent.Process(ref PenetrationBonus, ref AV, ref ShieldBlocked, Attacker, Defender, Weapon, "GetWeaponHitDice", Weapon, GetWeaponHitDiceEvent.ID, IHitDiceEvent.CascadeLevel, new Func<GameObject, GameObject, GameObject, int, int, bool, IHitDiceEvent>(GetWeaponHitDiceEvent.FromPool));
		}

		// Token: 0x0400277F RID: 10111
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetWeaponHitDiceEvent), null, new MinEvent.EventPoolCount(GetWeaponHitDiceEvent.CountPool), new MinEvent.EventPoolReset(GetWeaponHitDiceEvent.ResetPool));

		// Token: 0x04002780 RID: 10112
		private static List<GetWeaponHitDiceEvent> Pool;

		// Token: 0x04002781 RID: 10113
		private static int PoolCounter;
	}
}
