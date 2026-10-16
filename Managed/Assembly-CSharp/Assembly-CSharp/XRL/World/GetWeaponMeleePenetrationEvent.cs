using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007AA RID: 1962
	[GameEvent(Cascade = 0, Cache = MinEvent.Cache.Pool)]
	public class GetWeaponMeleePenetrationEvent : IMeleePenetrationEvent
	{
		// Token: 0x06004FDC RID: 20444 RVA: 0x00185704 File Offset: 0x00183904
		public GetWeaponMeleePenetrationEvent()
		{
			this.ID = GetWeaponMeleePenetrationEvent.ID;
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x00185717 File Offset: 0x00183917
		public override int GetCascadeLevel()
		{
			return GetWeaponMeleePenetrationEvent.CascadeLevel;
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x0018571E File Offset: 0x0018391E
		public static int CountPool()
		{
			if (GetWeaponMeleePenetrationEvent.Pool != null)
			{
				return GetWeaponMeleePenetrationEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x00185733 File Offset: 0x00183933
		public static void ResetPool()
		{
			while (GetWeaponMeleePenetrationEvent.PoolCounter > 0)
			{
				GetWeaponMeleePenetrationEvent.Pool[--GetWeaponMeleePenetrationEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x0018575B File Offset: 0x0018395B
		public static void ResetTo(ref GetWeaponMeleePenetrationEvent E)
		{
			MinEvent.ResetTo<GetWeaponMeleePenetrationEvent>(E, GetWeaponMeleePenetrationEvent.Pool, ref GetWeaponMeleePenetrationEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x00185771 File Offset: 0x00183971
		public static GetWeaponMeleePenetrationEvent FromPool()
		{
			return MinEvent.FromPool<GetWeaponMeleePenetrationEvent>(ref GetWeaponMeleePenetrationEvent.Pool, ref GetWeaponMeleePenetrationEvent.PoolCounter, 8192);
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x00185787 File Offset: 0x00183987
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x0018579C File Offset: 0x0018399C
		public static bool Process(ref int Penetrations, ref int StatBonus, ref int MaxStatBonus, ref int PenetrationBonus, ref int MaxPenetrationBonus, int AV, bool Critical, string Properties, string Hand, GameObject Attacker, GameObject Defender, GameObject Weapon)
		{
			return IMeleePenetrationEvent.Process(ref Penetrations, ref StatBonus, ref MaxStatBonus, ref PenetrationBonus, ref MaxPenetrationBonus, AV, Critical, Properties, Hand, Attacker, Defender, Weapon, "GetWeaponPenModifier", Weapon, GetWeaponMeleePenetrationEvent.ID, GetWeaponMeleePenetrationEvent.CascadeLevel, new Func<IMeleePenetrationEvent>(GetWeaponMeleePenetrationEvent.FromPool));
		}

		// Token: 0x04002788 RID: 10120
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetWeaponMeleePenetrationEvent), null, new MinEvent.EventPoolCount(GetWeaponMeleePenetrationEvent.CountPool), new MinEvent.EventPoolReset(GetWeaponMeleePenetrationEvent.ResetPool));

		// Token: 0x04002789 RID: 10121
		public new static readonly int CascadeLevel = 0;

		// Token: 0x0400278A RID: 10122
		private static List<GetWeaponMeleePenetrationEvent> Pool;

		// Token: 0x0400278B RID: 10123
		private static int PoolCounter;
	}
}
