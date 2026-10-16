using System;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x020010E2 RID: 4322
	[Serializable]
	public class GroundOnHit : IPoweredPart
	{
		// Token: 0x0600AB88 RID: 43912 RVA: 0x003EC8C0 File Offset: 0x003EAAC0
		public GroundOnHit()
		{
			this.ChargeUse = 100;
			this.WorksOnSelf = true;
			this.IsPowerLoadSensitive = true;
			this.NameForStatus = "FlightInterdiction";
		}

		// Token: 0x0600AB89 RID: 43913 RVA: 0x003EC938 File Offset: 0x003EAB38
		public override bool SameAs(IPart p)
		{
			GroundOnHit groundOnHit = p as GroundOnHit;
			return groundOnHit.Chance == this.Chance && groundOnHit.SaveTarget == this.SaveTarget && !(groundOnHit.SaveStat != this.SaveStat) && !(groundOnHit.SaveVs != this.SaveVs) && groundOnHit.FlyingLevelAidsSave == this.FlyingLevelAidsSave && !(groundOnHit.Duration != this.Duration) && groundOnHit.ComputePowerFactor == this.ComputePowerFactor && base.SameAs(p);
		}

		// Token: 0x0600AB8A RID: 43914 RVA: 0x003EC9D2 File Offset: 0x003EABD2
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AB8B RID: 43915 RVA: 0x003EC9D5 File Offset: 0x003EABD5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileHit");
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB8C RID: 43916 RVA: 0x003ECA00 File Offset: 0x003EAC00
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "WeaponThrowHit" || E.ID == "ProjectileHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Projectile");
				GameObject gameObject = gameObjectParameter;
				GameObject parentObject = this.ParentObject;
				string text = "Part GroundOnHit Activation";
				GameObject gameObject2 = gameObjectParameter2;
				GameObject gameObject3 = gameObjectParameter3;
				if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, gameObject3, true, false).in100())
				{
					this.CheckGrounding(gameObjectParameter, gameObjectParameter2, E.ID == "ProjectileHit");
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AB8D RID: 43917 RVA: 0x003ECAB0 File Offset: 0x003EACB0
		public void CheckGrounding(GameObject who, GameObject target, bool asProjectile = false)
		{
			object obj;
			if (!asProjectile)
			{
				obj = this.ParentObject;
			}
			else
			{
				Projectile part = this.ParentObject.GetPart<Projectile>();
				obj = ((part != null) ? part.Launcher : null);
			}
			object obj2 = obj;
			int num = ((obj2 != null) ? obj2.GetPowerLoadLevel() : 100);
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
			if (!base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
			{
				return;
			}
			int num4 = this.SaveTarget;
			if (this.FlyingLevelAidsSave)
			{
				int num5 = Flight.GetBestFlyingLevel(target);
				Wings part2 = target.GetPart<Wings>();
				if (part2 != null && part2.Level > num5)
				{
					num5 = part2.Level;
				}
				num4 -= num5;
			}
			if (target.MakeSave(this.SaveStat, num4, null, null, this.SaveVs, false, false, false, false, null))
			{
				return;
			}
			int num6 = GetAvailableComputePowerEvent.AdjustUp(who, this.Duration.RollCached() * (100 + IComponent<GameObject>.PowerLoadBonus(num, 100, 10)) / 100, this.ComputePowerFactor);
			Grounded grounded = target.GetEffect<Grounded>();
			if (grounded == null)
			{
				grounded = new Grounded();
				if (!target.ApplyEffect(grounded, null))
				{
					return;
				}
			}
			if (grounded.Duration < num6)
			{
				grounded.Duration = num6;
			}
		}

		// Token: 0x040046C6 RID: 18118
		public int Chance = 100;

		// Token: 0x040046C7 RID: 18119
		public int SaveTarget = 40;

		// Token: 0x040046C8 RID: 18120
		public string SaveStat = "Agility";

		// Token: 0x040046C9 RID: 18121
		public string SaveVs = "Grounding";

		// Token: 0x040046CA RID: 18122
		public bool FlyingLevelAidsSave = true;

		// Token: 0x040046CB RID: 18123
		public string Duration = "20-30";

		// Token: 0x040046CC RID: 18124
		public float ComputePowerFactor = 1f;
	}
}
