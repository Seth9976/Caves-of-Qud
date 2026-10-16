using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010F1 RID: 4337
	[Serializable]
	public class RealityStabilizeOnHit : IPoweredPart
	{
		// Token: 0x0600ABCD RID: 43981 RVA: 0x003EEF34 File Offset: 0x003ED134
		public RealityStabilizeOnHit()
		{
			this.ChargeUse = 100;
			this.WorksOnSelf = true;
			this.IsPowerLoadSensitive = true;
			this.NameForStatus = "NormalityProjection";
		}

		// Token: 0x0600ABCE RID: 43982 RVA: 0x003EEF94 File Offset: 0x003ED194
		public override bool SameAs(IPart p)
		{
			RealityStabilizeOnHit realityStabilizeOnHit = p as RealityStabilizeOnHit;
			return realityStabilizeOnHit.Chance == this.Chance && !(realityStabilizeOnHit.Strength != this.Strength) && !(realityStabilizeOnHit.Duration != this.Duration) && realityStabilizeOnHit.Linked == this.Linked && realityStabilizeOnHit.ComputePowerFactor == this.ComputePowerFactor && base.SameAs(p);
		}

		// Token: 0x0600ABCF RID: 43983 RVA: 0x003EF009 File Offset: 0x003ED209
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600ABD0 RID: 43984 RVA: 0x003EF00C File Offset: 0x003ED20C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileHit");
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABD1 RID: 43985 RVA: 0x003EF038 File Offset: 0x003ED238
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "WeaponThrowHit" || E.ID == "ProjectileHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Projectile");
				GameObject gameObject = gameObjectParameter;
				GameObject parentObject = this.ParentObject;
				string text = "Part RealityStabilizeOnHit Activation";
				GameObject gameObject2 = gameObjectParameter2;
				GameObject gameObject3 = gameObjectParameter3;
				GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, gameObject3, true, false);
				if (this.Chance.in100())
				{
					this.CheckRealityStabilization(gameObjectParameter, gameObjectParameter2, E.ID == "ProjectileHit");
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600ABD2 RID: 43986 RVA: 0x003EF0F0 File Offset: 0x003ED2F0
		public void CheckRealityStabilization(GameObject who, GameObject target, bool asProjectile = false)
		{
			GameObject gameObject;
			if (!asProjectile)
			{
				gameObject = this.ParentObject;
			}
			else
			{
				Projectile part = this.ParentObject.GetPart<Projectile>();
				gameObject = ((part != null) ? part.Launcher : null);
			}
			GameObject gameObject2 = gameObject;
			int num = ((gameObject2 != null) ? gameObject2.GetPowerLoadLevel() : 100);
			RealityStabilization realityStabilization = null;
			int num2;
			if (this.Linked)
			{
				realityStabilization = ((gameObject2 != null) ? gameObject2.GetPart<RealityStabilization>() : null);
				if (realityStabilization == null)
				{
					return;
				}
				if (!string.IsNullOrEmpty(this.Strength))
				{
					realityStabilization.FlushEffectiveStrengthCache();
					if (!realityStabilization.HasAnyWorksOn())
					{
						realityStabilization.LastStatus = new ActivePartStatus?(ActivePartStatus.Operational);
					}
					realityStabilization.Strength = GetAvailableComputePowerEvent.AdjustUp(who, this.Strength.RollCached(), this.ComputePowerFactor) + IComponent<GameObject>.PowerLoadBonus(num, 100, 30);
				}
				num2 = Math.Max(realityStabilization.EffectiveStrength, 0);
			}
			else
			{
				num2 = this.Strength.RollCached();
			}
			if (num2 <= 0)
			{
				return;
			}
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
			int num3 = 1;
			int? num4 = new int?(num);
			if (!base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num3, null, false, 0L, num4))
			{
				return;
			}
			if (realityStabilization != null)
			{
				IActivePart activePart = realityStabilization;
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
				int num5 = 1;
				num4 = new int?(num);
				if (!activePart.IsReady(flag11, flag12, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, num5, null, false, 0L, num4))
				{
					return;
				}
			}
			num4 = new int?(num);
			base.ConsumeCharge(null, num4);
			if (realityStabilization != null)
			{
				IActivePart activePart2 = realityStabilization;
				num4 = new int?(num);
				activePart2.ConsumeCharge(null, num4);
			}
			int num6 = GetAvailableComputePowerEvent.AdjustUp(who, this.Duration.RollCached() * (100 + IComponent<GameObject>.PowerLoadBonus(num, 100, 10)) / 100, this.ComputePowerFactor);
			RealityStabilized realityStabilized = target.GetEffect<RealityStabilized>();
			if (realityStabilized == null)
			{
				realityStabilized = new RealityStabilized();
				if (!target.ForceApplyEffect(realityStabilized, null))
				{
					return;
				}
			}
			if (realityStabilized.IndependentStrength < num2)
			{
				realityStabilized.IndependentStrength = num2;
			}
			if (realityStabilized.Duration < num6)
			{
				realityStabilized.Duration = num6;
			}
			if (!GameObject.Validate(ref realityStabilized.Owner))
			{
				realityStabilized.Owner = who;
			}
			realityStabilized.Stabilize(false);
		}

		// Token: 0x040046FE RID: 18174
		public int Chance = 100;

		// Token: 0x040046FF RID: 18175
		public string Strength = "100";

		// Token: 0x04004700 RID: 18176
		public string Duration = "20-30";

		// Token: 0x04004701 RID: 18177
		public bool Linked;

		// Token: 0x04004702 RID: 18178
		public float ComputePowerFactor = 1f;
	}
}
