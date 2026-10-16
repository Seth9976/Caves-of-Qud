using System;
using System.Text;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is not by default, penetration bonus is increased by half of
	/// the standard power load bonus, i.e. 1 for the standard overload
	/// power load of 400.
	/// </remarks>
	// Token: 0x020010CD RID: 4301
	[Serializable]
	public class AfferentWeapon : IActivePart
	{
		// Token: 0x0600AB11 RID: 43793 RVA: 0x003E9DD4 File Offset: 0x003E7FD4
		public AfferentWeapon()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AB12 RID: 43794 RVA: 0x003E9DEC File Offset: 0x003E7FEC
		public override bool SameAs(IPart p)
		{
			VibroWeapon vibroWeapon = p as VibroWeapon;
			return vibroWeapon.PenetrationBonus == this.PenetrationBonus && vibroWeapon.WorksWhenThrown == this.WorksWhenThrown && base.SameAs(p);
		}

		// Token: 0x0600AB13 RID: 43795 RVA: 0x003E9E27 File Offset: 0x003E8027
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<IsAdaptivePenetrationActiveEvent>.ID || (ID == PooledEvent<GetThrownWeaponPerformanceEvent>.ID && this.WorksWhenThrown) || ID == GetWeaponMeleePenetrationEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AB14 RID: 43796 RVA: 0x003E9E60 File Offset: 0x003E8060
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
				E.Symbol = "à";
				E.Bonus += this.PenetrationBonus + IComponent<GameObject>.PowerLoadBonus(num, 100, 300);
				E.Active = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB15 RID: 43797 RVA: 0x003E9ED8 File Offset: 0x003E80D8
		public override bool HandleEvent(GetWeaponMeleePenetrationEvent E)
		{
			E.StatBonus = 0;
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
				MeleeWeapon part = this.ParentObject.GetPart<MeleeWeapon>();
				if (part != null && E.Defender != null)
				{
					E.StatBonus = E.Defender.StatMod(part.Stat, 0) + IComponent<GameObject>.PowerLoadBonus(num, 100, 300);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB16 RID: 43798 RVA: 0x003E9F60 File Offset: 0x003E8160
		public override bool HandleEvent(GetThrownWeaponPerformanceEvent E)
		{
			E.PenetrationModifier = 0;
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
				MeleeWeapon part = this.ParentObject.GetPart<MeleeWeapon>();
				if (part != null && E.Defender != null)
				{
					E.PenetrationModifier = E.Defender.StatMod(part.Stat, 0);
				}
				E.PenetrationBonus += this.PenetrationBonus + IComponent<GameObject>.PowerLoadBonus(num, 100, 300);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB17 RID: 43799 RVA: 0x003EA002 File Offset: 0x003E8202
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB18 RID: 43800 RVA: 0x003EA024 File Offset: 0x003E8224
		public void AppendEffect(StringBuilder SB)
		{
			MeleeWeapon part = this.ParentObject.GetPart<MeleeWeapon>();
			if (part == null || part.Stat.IsNullOrEmpty())
			{
				return;
			}
			SB.Append("This weapon uses its victim's ").Append(Statistic.GetStatDisplayName(part.Stat)).Append(" modifier for penetration bonus.");
		}

		// Token: 0x04004686 RID: 18054
		public int PenetrationBonus;

		// Token: 0x04004687 RID: 18055
		public bool WorksWhenThrown = true;
	}
}
