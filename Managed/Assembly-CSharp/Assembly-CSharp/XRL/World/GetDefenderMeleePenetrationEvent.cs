using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000740 RID: 1856
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetDefenderMeleePenetrationEvent : IMeleePenetrationEvent
	{
		// Token: 0x06004CDD RID: 19677 RVA: 0x0017B256 File Offset: 0x00179456
		public GetDefenderMeleePenetrationEvent()
		{
			this.ID = GetDefenderMeleePenetrationEvent.ID;
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x0017B269 File Offset: 0x00179469
		public static int CountPool()
		{
			if (GetDefenderMeleePenetrationEvent.Pool != null)
			{
				return GetDefenderMeleePenetrationEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x0017B27E File Offset: 0x0017947E
		public static void ResetPool()
		{
			while (GetDefenderMeleePenetrationEvent.PoolCounter > 0)
			{
				GetDefenderMeleePenetrationEvent.Pool[--GetDefenderMeleePenetrationEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x0017B2A6 File Offset: 0x001794A6
		public static void ResetTo(ref GetDefenderMeleePenetrationEvent E)
		{
			MinEvent.ResetTo<GetDefenderMeleePenetrationEvent>(E, GetDefenderMeleePenetrationEvent.Pool, ref GetDefenderMeleePenetrationEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x0017B2BC File Offset: 0x001794BC
		public static GetDefenderMeleePenetrationEvent FromPool()
		{
			return MinEvent.FromPool<GetDefenderMeleePenetrationEvent>(ref GetDefenderMeleePenetrationEvent.Pool, ref GetDefenderMeleePenetrationEvent.PoolCounter, 8192);
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x0017B2D2 File Offset: 0x001794D2
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x0017B2E8 File Offset: 0x001794E8
		public static bool Process(ref int Penetrations, ref int StatBonus, ref int MaxStatBonus, ref int PenetrationBonus, ref int MaxPenetrationBonus, int AV, bool Critical, string Properties, string Hand, GameObject Attacker, GameObject Defender, GameObject Weapon)
		{
			return IMeleePenetrationEvent.Process(ref Penetrations, ref StatBonus, ref MaxStatBonus, ref PenetrationBonus, ref MaxPenetrationBonus, AV, Critical, Properties, Hand, Attacker, Defender, Weapon, "DefenderGetWeaponPenModifier", Defender, GetDefenderMeleePenetrationEvent.ID, IMeleePenetrationEvent.CascadeLevel, new Func<IMeleePenetrationEvent>(GetDefenderMeleePenetrationEvent.FromPool));
		}

		// Token: 0x0400252E RID: 9518
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetDefenderMeleePenetrationEvent), null, new MinEvent.EventPoolCount(GetDefenderMeleePenetrationEvent.CountPool), new MinEvent.EventPoolReset(GetDefenderMeleePenetrationEvent.ResetPool));

		// Token: 0x0400252F RID: 9519
		private static List<GetDefenderMeleePenetrationEvent> Pool;

		// Token: 0x04002530 RID: 9520
		private static int PoolCounter;
	}
}
