using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007A9 RID: 1961
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetWeaponMeleeAttacksEvent : IMeleeAttackEvent
	{
		// Token: 0x06004FD3 RID: 20435 RVA: 0x001855E6 File Offset: 0x001837E6
		public GetWeaponMeleeAttacksEvent()
		{
			this.ID = GetWeaponMeleeAttacksEvent.ID;
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x001855F9 File Offset: 0x001837F9
		public static int CountPool()
		{
			if (GetWeaponMeleeAttacksEvent.Pool != null)
			{
				return GetWeaponMeleeAttacksEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x0018560E File Offset: 0x0018380E
		public static void ResetPool()
		{
			while (GetWeaponMeleeAttacksEvent.PoolCounter > 0)
			{
				GetWeaponMeleeAttacksEvent.Pool[--GetWeaponMeleeAttacksEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x00185636 File Offset: 0x00183836
		public static void ResetTo(ref GetWeaponMeleeAttacksEvent E)
		{
			MinEvent.ResetTo<GetWeaponMeleeAttacksEvent>(E, GetWeaponMeleeAttacksEvent.Pool, ref GetWeaponMeleeAttacksEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0018564C File Offset: 0x0018384C
		public static GetWeaponMeleeAttacksEvent FromPool()
		{
			return MinEvent.FromPool<GetWeaponMeleeAttacksEvent>(ref GetWeaponMeleeAttacksEvent.Pool, ref GetWeaponMeleeAttacksEvent.PoolCounter, 8192);
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x00185662 File Offset: 0x00183862
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x00185676 File Offset: 0x00183876
		public override void Reset()
		{
			base.Reset();
			this.Weapon = null;
			this.Primary = false;
			this.Intrinsic = false;
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x00185694 File Offset: 0x00183894
		public static GetWeaponMeleeAttacksEvent HandleFrom(GameObject Actor, GameObject Weapon, bool Primary = false, bool Intrinsic = false)
		{
			GetWeaponMeleeAttacksEvent getWeaponMeleeAttacksEvent = GetWeaponMeleeAttacksEvent.FromPool();
			getWeaponMeleeAttacksEvent.Actor = Actor;
			getWeaponMeleeAttacksEvent.Weapon = Weapon;
			getWeaponMeleeAttacksEvent.Primary = Primary;
			getWeaponMeleeAttacksEvent.Intrinsic = Intrinsic;
			if (Actor.HandleEvent(getWeaponMeleeAttacksEvent))
			{
				Weapon.HandleEvent(getWeaponMeleeAttacksEvent);
			}
			return getWeaponMeleeAttacksEvent;
		}

		// Token: 0x04002782 RID: 10114
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetWeaponMeleeAttacksEvent), null, new MinEvent.EventPoolCount(GetWeaponMeleeAttacksEvent.CountPool), new MinEvent.EventPoolReset(GetWeaponMeleeAttacksEvent.ResetPool));

		// Token: 0x04002783 RID: 10115
		private static List<GetWeaponMeleeAttacksEvent> Pool;

		// Token: 0x04002784 RID: 10116
		private static int PoolCounter;

		// Token: 0x04002785 RID: 10117
		public GameObject Weapon;

		// Token: 0x04002786 RID: 10118
		public bool Primary;

		// Token: 0x04002787 RID: 10119
		public bool Intrinsic;
	}
}
