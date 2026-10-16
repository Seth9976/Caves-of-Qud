using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000729 RID: 1833
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetAttackerMeleePenetrationEvent : IMeleePenetrationEvent
	{
		// Token: 0x06004C0D RID: 19469 RVA: 0x00178A39 File Offset: 0x00176C39
		public GetAttackerMeleePenetrationEvent()
		{
			this.ID = GetAttackerMeleePenetrationEvent.ID;
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x00178A4C File Offset: 0x00176C4C
		public static int CountPool()
		{
			if (GetAttackerMeleePenetrationEvent.Pool != null)
			{
				return GetAttackerMeleePenetrationEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x00178A61 File Offset: 0x00176C61
		public static void ResetPool()
		{
			while (GetAttackerMeleePenetrationEvent.PoolCounter > 0)
			{
				GetAttackerMeleePenetrationEvent.Pool[--GetAttackerMeleePenetrationEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x00178A89 File Offset: 0x00176C89
		public static void ResetTo(ref GetAttackerMeleePenetrationEvent E)
		{
			MinEvent.ResetTo<GetAttackerMeleePenetrationEvent>(E, GetAttackerMeleePenetrationEvent.Pool, ref GetAttackerMeleePenetrationEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x00178A9F File Offset: 0x00176C9F
		public static GetAttackerMeleePenetrationEvent FromPool()
		{
			return MinEvent.FromPool<GetAttackerMeleePenetrationEvent>(ref GetAttackerMeleePenetrationEvent.Pool, ref GetAttackerMeleePenetrationEvent.PoolCounter, 8192);
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x00178AB5 File Offset: 0x00176CB5
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x00178ACC File Offset: 0x00176CCC
		public static bool Process(ref int Penetrations, ref int StatBonus, ref int MaxStatBonus, ref int PenetrationBonus, ref int MaxPenetrationBonus, int AV, bool Critical, string Properties, string Hand, GameObject Attacker, GameObject Defender, GameObject Weapon)
		{
			return IMeleePenetrationEvent.Process(ref Penetrations, ref StatBonus, ref MaxStatBonus, ref PenetrationBonus, ref MaxPenetrationBonus, AV, Critical, Properties, Hand, Attacker, Defender, Weapon, "AttackerGetWeaponPenModifier", Attacker, GetAttackerMeleePenetrationEvent.ID, IMeleePenetrationEvent.CascadeLevel, new Func<IMeleePenetrationEvent>(GetAttackerMeleePenetrationEvent.FromPool));
		}

		// Token: 0x040024CC RID: 9420
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetAttackerMeleePenetrationEvent), null, new MinEvent.EventPoolCount(GetAttackerMeleePenetrationEvent.CountPool), new MinEvent.EventPoolReset(GetAttackerMeleePenetrationEvent.ResetPool));

		// Token: 0x040024CD RID: 9421
		private static List<GetAttackerMeleePenetrationEvent> Pool;

		// Token: 0x040024CE RID: 9422
		private static int PoolCounter;
	}
}
