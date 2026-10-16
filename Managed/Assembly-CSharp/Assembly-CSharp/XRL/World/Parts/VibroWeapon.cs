using System;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, penetration bonus is increased by half of
	/// the standard power load bonus, i.e. 1 for the standard overload
	/// power load of 400.
	/// </remarks>
	// Token: 0x020010FC RID: 4348
	[Serializable]
	public class VibroWeapon : IPoweredPart
	{
		// Token: 0x0600AC12 RID: 44050 RVA: 0x003F127E File Offset: 0x003EF47E
		public VibroWeapon()
		{
			this.ChargeUse = 0;
			this.IsPowerLoadSensitive = true;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AC13 RID: 44051 RVA: 0x003F12A4 File Offset: 0x003EF4A4
		public override bool SameAs(IPart p)
		{
			VibroWeapon vibroWeapon = p as VibroWeapon;
			return vibroWeapon.PenetrationBonus == this.PenetrationBonus && vibroWeapon.WorksWhenThrown == this.WorksWhenThrown && base.SameAs(p);
		}

		// Token: 0x0600AC14 RID: 44052 RVA: 0x003F12DF File Offset: 0x003EF4DF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<IsAdaptivePenetrationActiveEvent>.ID || (ID == PooledEvent<GetThrownWeaponPerformanceEvent>.ID && this.WorksWhenThrown) || ID == GetWeaponMeleePenetrationEvent.ID;
		}

		// Token: 0x0600AC15 RID: 44053 RVA: 0x003F1310 File Offset: 0x003EF510
		public override bool HandleEvent(IsAdaptivePenetrationActiveEvent E)
		{
			int num = this.MyPowerLoadLevel();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num2 = 1;
			int? num3 = new int?(num);
			if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
			{
				E.Bonus += this.PenetrationBonus + IComponent<GameObject>.PowerLoadBonus(num, 100, 300);
				E.Active = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC16 RID: 44054 RVA: 0x003F137C File Offset: 0x003EF57C
		public override bool HandleEvent(GetWeaponMeleePenetrationEvent E)
		{
			int num = this.MyPowerLoadLevel();
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num2 = 1;
			int? num3 = new int?(num);
			if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
			{
				int num4 = E.AV + this.PenetrationBonus + IComponent<GameObject>.PowerLoadBonus(num, 100, 300);
				E.StatBonus = num4;
				E.MaxStatBonus = num4;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC17 RID: 44055 RVA: 0x003F13EC File Offset: 0x003EF5EC
		public override bool HandleEvent(GetThrownWeaponPerformanceEvent E)
		{
			int num = this.MyPowerLoadLevel();
			bool flag = !E.Prospective;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num2 = 1;
			int? num3 = new int?(num);
			if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
			{
				E.PenetrationBonus += this.PenetrationBonus + IComponent<GameObject>.PowerLoadBonus(num, 100, 300);
				E.Vorpal = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400473B RID: 18235
		public int PenetrationBonus;

		// Token: 0x0400473C RID: 18236
		public bool WorksWhenThrown = true;
	}
}
