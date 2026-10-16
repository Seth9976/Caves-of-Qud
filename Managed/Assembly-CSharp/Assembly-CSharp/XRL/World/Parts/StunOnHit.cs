using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, chance to activate is increased by a
	/// percentage equal to ((power load - 100) / 10), i.e. 30% for
	/// the standard overload power load of 400, and save targets are
	/// increased by the standard power load bonus, i.e. 2 for the standard
	/// overload power load of 400.
	/// </remarks>
	// Token: 0x020010F7 RID: 4343
	[Serializable]
	public class StunOnHit : IPoweredPart
	{
		// Token: 0x0600ABF2 RID: 44018 RVA: 0x003F055F File Offset: 0x003EE75F
		public StunOnHit()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
			this.IsPowerLoadSensitive = true;
			this.NameForStatus = "StunMechanism";
		}

		// Token: 0x0600ABF3 RID: 44019 RVA: 0x003F059C File Offset: 0x003EE79C
		public override bool SameAs(IPart p)
		{
			StunOnHit stunOnHit = p as StunOnHit;
			return !(stunOnHit.Duration != this.Duration) && stunOnHit.Chance == this.Chance && stunOnHit.SaveTarget == this.SaveTarget && base.SameAs(p);
		}

		// Token: 0x0600ABF4 RID: 44020 RVA: 0x003F05EC File Offset: 0x003EE7EC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600ABF5 RID: 44021 RVA: 0x003F05EF File Offset: 0x003EE7EF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AdjustWeaponScore");
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABF6 RID: 44022 RVA: 0x003F061C File Offset: 0x003EE81C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "WeaponThrowHit")
			{
				int num = this.MyPowerLoadLevel();
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				int num2 = this.Chance;
				int num3 = IComponent<GameObject>.PowerLoadBonus(num, 100, 10);
				if (num3 != 0)
				{
					num2 = num2 * (100 + num3) / 100;
				}
				GameObject gameObject = gameObjectParameter;
				GameObject parentObject = this.ParentObject;
				string text = "Part StunOnHit Activation";
				GameObject gameObject2 = gameObjectParameter2;
				num2 = GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, num2, gameObject2, null, true, false);
				if (num2.in100() && gameObjectParameter.PhaseMatches(gameObjectParameter2))
				{
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
					int num4 = 1;
					int? num5 = new int?(num);
					if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num4, null, false, 0L, num5))
					{
						int num6 = this.Duration.RollCached();
						int num7 = this.SaveTarget + IComponent<GameObject>.PowerLoadBonus(num, 100, 150);
						gameObjectParameter2.ApplyEffect(new Stun(num6, num7, false), null);
					}
				}
			}
			else if (E.ID == "AdjustWeaponScore")
			{
				bool flag11 = false;
				bool flag12 = false;
				bool flag13 = false;
				bool flag14 = false;
				bool flag15 = false;
				bool flag16 = false;
				bool flag17 = false;
				bool flag18 = false;
				bool flag19 = false;
				bool flag20 = false;
				int num8 = 1;
				int? num5 = null;
				int? num9 = num5;
				bool flag21 = false;
				long num10 = 0L;
				num5 = null;
				if (base.IsReady(flag11, flag12, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, num8, num9, flag21, num10, num5))
				{
					int @for = GetSpecialEffectChanceEvent.GetFor(E.GetGameObjectParameter("User"), this.ParentObject, "Part StunOnHit Activation", this.Chance, null, null, true, false);
					int intParameter = E.GetIntParameter("Score", 0);
					int num11 = 8 * (this.Duration.RollMinCached() * 2 + this.Duration.RollMaxCached()) * @for * (this.IsPowerLoadSensitive ? (this.SaveTarget + this.MyPowerLoadBonus(int.MinValue, 100, 150)) : this.SaveTarget) / 2000;
					E.SetParameter("Score", intParameter + num11);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004722 RID: 18210
		public string Duration = "1";

		// Token: 0x04004723 RID: 18211
		public int Chance;

		// Token: 0x04004724 RID: 18212
		public int SaveTarget = 12;
	}
}
