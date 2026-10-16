using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000768 RID: 1896
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class GetMissileWeaponPerformanceEvent : PooledEvent<GetMissileWeaponPerformanceEvent>
	{
		// Token: 0x06004E15 RID: 19989 RVA: 0x0017EE97 File Offset: 0x0017D097
		public override int GetCascadeLevel()
		{
			return GetMissileWeaponPerformanceEvent.CascadeLevel;
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x0017EE9E File Offset: 0x0017D09E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x0017EEA8 File Offset: 0x0017D0A8
		public override void Reset()
		{
			base.Reset();
			this.Subject = null;
			this.Actor = null;
			this.Launcher = null;
			this.Projectile = null;
			this.BasePenetration = 0;
			this.PenetrationCap = 0;
			this.PenetrationBonus = 0;
			this.BaseDamage = null;
			this.Attributes = null;
			this.DamageColor = null;
			this.PenetrateCreatures = false;
			this.PenetrateWalls = false;
			this.Quiet = false;
			this.DamageRoll = null;
			this.Active = false;
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06004E18 RID: 19992 RVA: 0x0017EF24 File Offset: 0x0017D124
		public int Penetration
		{
			get
			{
				return Math.Min(this.BasePenetration, this.PenetrationCap) + this.PenetrationBonus;
			}
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x0017EF40 File Offset: 0x0017D140
		public static GetMissileWeaponPerformanceEvent FromPool(GameObject Actor, GameObject Launcher, GameObject Projectile = null, int? BasePenetration = null, int? PenetrationCap = null, int? PenetrationBonus = null, string BaseDamage = null, string Attributes = null, string DamageColor = null, bool? PenetrateCreatures = null, bool? PenetrateWalls = null, bool? Quiet = null, DieRoll DamageRoll = null, string ProjectileBlueprint = null, bool Active = false)
		{
			GetMissileWeaponPerformanceEvent getMissileWeaponPerformanceEvent = PooledEvent<GetMissileWeaponPerformanceEvent>.FromPool();
			getMissileWeaponPerformanceEvent.Actor = Actor;
			getMissileWeaponPerformanceEvent.Launcher = Launcher;
			if (Projectile == null && ProjectileBlueprint == null && Launcher != null)
			{
				GetMissileWeaponProjectileEvent.GetFor(Launcher, ref Projectile, ref ProjectileBlueprint);
			}
			getMissileWeaponPerformanceEvent.Projectile = Projectile;
			if (BaseDamage == null || BasePenetration == null || PenetrationCap == null || PenetrationBonus == null || Attributes == null || PenetrateCreatures == null || PenetrateWalls == null || Quiet == null)
			{
				if (Projectile != null)
				{
					Projectile part = Projectile.GetPart<Projectile>();
					if (part != null)
					{
						int num = BasePenetration.GetValueOrDefault();
						if (BasePenetration == null)
						{
							num = part.BasePenetration;
							BasePenetration = new int?(num);
							if (Actor != null)
							{
								MissileWeapon missileWeapon = ((Launcher != null) ? Launcher.GetPart<MissileWeapon>() : null);
								if (missileWeapon != null && !missileWeapon.ProjectilePenetrationStat.IsNullOrEmpty())
								{
									BasePenetration += Actor.StatMod(missileWeapon.ProjectilePenetrationStat, 0);
								}
							}
						}
						if (PenetrationCap == null)
						{
							PenetrationCap = new int?(num + part.StrengthPenetration);
						}
						if (PenetrationBonus == null)
						{
							PenetrationBonus = new int?(0);
						}
						if (BaseDamage == null)
						{
							BaseDamage = part.BaseDamage;
						}
						if (Attributes == null)
						{
							Attributes = part.Attributes;
						}
						if (PenetrateCreatures == null)
						{
							PenetrateCreatures = new bool?(part.PenetrateCreatures);
						}
						if (PenetrateWalls == null)
						{
							PenetrateWalls = new bool?(part.PenetrateWalls);
						}
						if (Quiet == null)
						{
							Quiet = new bool?(part.Quiet);
						}
					}
				}
				else if (ProjectileBlueprint != null)
				{
					GetMissileWeaponPerformanceEvent.Perf perf;
					if (!GetMissileWeaponPerformanceEvent.ProjectilePerformance.TryGetValue(ProjectileBlueprint, out perf))
					{
						perf = (GetMissileWeaponPerformanceEvent.ProjectilePerformance[ProjectileBlueprint] = new GetMissileWeaponPerformanceEvent.Perf());
						GameObject gameObject = GameObject.CreateSample(ProjectileBlueprint);
						Projectile part2 = gameObject.GetPart<Projectile>();
						if (part2 != null)
						{
							perf.BasePenetration = part2.BasePenetration;
							perf.PenetrationCap = part2.BasePenetration + part2.StrengthPenetration;
							perf.PenetrationBonus = 0;
							perf.BaseDamage = part2.BaseDamage;
							perf.Attributes = part2.Attributes;
							perf.PenetrateCreatures = part2.PenetrateCreatures;
							perf.PenetrateWalls = part2.PenetrateWalls;
							perf.Quiet = part2.Quiet;
						}
						gameObject.Obliterate(null, false, null);
					}
					if (BasePenetration == null)
					{
						BasePenetration = new int?(perf.BasePenetration);
						if (Actor != null)
						{
							MissileWeapon missileWeapon2 = ((Launcher != null) ? Launcher.GetPart<MissileWeapon>() : null);
							if (missileWeapon2 != null && !missileWeapon2.ProjectilePenetrationStat.IsNullOrEmpty())
							{
								BasePenetration += Actor.StatMod(missileWeapon2.ProjectilePenetrationStat, 0);
							}
						}
					}
					if (PenetrationCap == null)
					{
						PenetrationCap = new int?(perf.PenetrationCap);
					}
					if (PenetrationBonus == null)
					{
						PenetrationBonus = new int?(perf.PenetrationBonus);
					}
					if (BaseDamage == null)
					{
						BaseDamage = perf.BaseDamage;
					}
					if (Attributes == null)
					{
						Attributes = perf.Attributes;
					}
					if (PenetrateCreatures == null)
					{
						PenetrateCreatures = new bool?(perf.PenetrateCreatures);
					}
					if (PenetrateWalls == null)
					{
						PenetrateWalls = new bool?(perf.PenetrateWalls);
					}
					if (Quiet == null)
					{
						Quiet = new bool?(perf.Quiet);
					}
				}
			}
			if (Attributes != null && Attributes.Contains("Psionic") && getMissileWeaponPerformanceEvent.Actor != null)
			{
				int num2 = getMissileWeaponPerformanceEvent.Actor.StatMod("Ego", 0);
				PenetrationBonus = new int?(PenetrationBonus.GetValueOrDefault() + num2);
			}
			getMissileWeaponPerformanceEvent.Subject = null;
			getMissileWeaponPerformanceEvent.BaseDamage = BaseDamage;
			getMissileWeaponPerformanceEvent.BasePenetration = BasePenetration.GetValueOrDefault();
			getMissileWeaponPerformanceEvent.PenetrationCap = PenetrationCap ?? getMissileWeaponPerformanceEvent.BasePenetration;
			getMissileWeaponPerformanceEvent.PenetrationBonus = PenetrationBonus.GetValueOrDefault();
			getMissileWeaponPerformanceEvent.Attributes = Attributes;
			getMissileWeaponPerformanceEvent.DamageColor = DamageColor;
			getMissileWeaponPerformanceEvent.PenetrateCreatures = PenetrateCreatures.GetValueOrDefault();
			getMissileWeaponPerformanceEvent.PenetrateWalls = PenetrateWalls.GetValueOrDefault();
			getMissileWeaponPerformanceEvent.Quiet = Quiet.GetValueOrDefault();
			getMissileWeaponPerformanceEvent.DamageRoll = DamageRoll;
			getMissileWeaponPerformanceEvent.Active = Active;
			return getMissileWeaponPerformanceEvent;
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x0017F361 File Offset: 0x0017D561
		public DieRoll GetDamageRoll()
		{
			if (this.DamageRoll == null && this.BaseDamage != null)
			{
				this.DamageRoll = new DieRoll(this.BaseDamage);
			}
			return this.DamageRoll;
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x0017F38A File Offset: 0x0017D58A
		public DieRoll GetPossiblyCachedDamageRoll()
		{
			return this.DamageRoll ?? this.BaseDamage.GetCachedDieRoll();
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x0017F3A1 File Offset: 0x0017D5A1
		public int RollDamagePenetrations(int TargetInclusive)
		{
			return Stat.RollDamagePenetrations(TargetInclusive, this.BasePenetration + this.PenetrationBonus, this.PenetrationCap + this.PenetrationBonus);
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x0017F3C3 File Offset: 0x0017D5C3
		public string GetDamageColor()
		{
			if (!this.DamageColor.IsNullOrEmpty())
			{
				return this.DamageColor;
			}
			return Damage.GetDamageColor(this.Attributes);
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x0017F3E4 File Offset: 0x0017D5E4
		public static GetMissileWeaponPerformanceEvent GetFor(GameObject Actor, GameObject Launcher, GameObject Projectile = null, int? BasePenetration = null, int? PenetrationCap = null, string BaseDamage = null, string Attributes = null, string DamageColor = null, bool? PenetrateCreatures = null, bool? PenetrateWalls = null, bool? Quiet = null, DieRoll DamageRoll = null, string ProjectileBlueprint = null, bool Active = false)
		{
			GetMissileWeaponPerformanceEvent getMissileWeaponPerformanceEvent = GetMissileWeaponPerformanceEvent.FromPool(Actor, Launcher, Projectile, BasePenetration, PenetrationCap, null, BaseDamage, Attributes, DamageColor, PenetrateCreatures, PenetrateWalls, Quiet, DamageRoll, ProjectileBlueprint, Active);
			Projectile = getMissileWeaponPerformanceEvent.Projectile;
			bool flag = true;
			if (flag && GameObject.Validate(ref Launcher) && Launcher.WantEvent(PooledEvent<GetMissileWeaponPerformanceEvent>.ID, GetMissileWeaponPerformanceEvent.CascadeLevel))
			{
				getMissileWeaponPerformanceEvent.Subject = Launcher;
				flag = Launcher.HandleEvent(getMissileWeaponPerformanceEvent);
			}
			if (flag && GameObject.Validate(ref Projectile) && Projectile.WantEvent(PooledEvent<GetMissileWeaponPerformanceEvent>.ID, GetMissileWeaponPerformanceEvent.CascadeLevel))
			{
				getMissileWeaponPerformanceEvent.Subject = Projectile;
				flag = Projectile.HandleEvent(getMissileWeaponPerformanceEvent);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetMissileWeaponPerformanceEvent>.ID, GetMissileWeaponPerformanceEvent.CascadeLevel))
			{
				getMissileWeaponPerformanceEvent.Subject = Actor;
				flag = Actor.HandleEvent(getMissileWeaponPerformanceEvent);
			}
			return getMissileWeaponPerformanceEvent;
		}

		// Token: 0x04002604 RID: 9732
		public new static readonly int CascadeLevel = 1;

		// Token: 0x04002605 RID: 9733
		public GameObject Subject;

		// Token: 0x04002606 RID: 9734
		public GameObject Actor;

		// Token: 0x04002607 RID: 9735
		public GameObject Launcher;

		// Token: 0x04002608 RID: 9736
		public GameObject Projectile;

		// Token: 0x04002609 RID: 9737
		public int BasePenetration;

		// Token: 0x0400260A RID: 9738
		public int PenetrationCap;

		// Token: 0x0400260B RID: 9739
		public int PenetrationBonus;

		// Token: 0x0400260C RID: 9740
		public string BaseDamage;

		// Token: 0x0400260D RID: 9741
		public string Attributes;

		// Token: 0x0400260E RID: 9742
		public string DamageColor;

		// Token: 0x0400260F RID: 9743
		public bool PenetrateCreatures;

		// Token: 0x04002610 RID: 9744
		public bool PenetrateWalls;

		// Token: 0x04002611 RID: 9745
		public bool Quiet;

		// Token: 0x04002612 RID: 9746
		public DieRoll DamageRoll;

		// Token: 0x04002613 RID: 9747
		public bool Active;

		// Token: 0x04002614 RID: 9748
		private static Dictionary<string, GetMissileWeaponPerformanceEvent.Perf> ProjectilePerformance = new Dictionary<string, GetMissileWeaponPerformanceEvent.Perf>(8);

		// Token: 0x02001A1F RID: 6687
		private class Perf
		{
			// Token: 0x04006823 RID: 26659
			public int BasePenetration;

			// Token: 0x04006824 RID: 26660
			public int PenetrationCap;

			// Token: 0x04006825 RID: 26661
			public int PenetrationBonus;

			// Token: 0x04006826 RID: 26662
			public string BaseDamage;

			// Token: 0x04006827 RID: 26663
			public string Attributes;

			// Token: 0x04006828 RID: 26664
			public bool PenetrateCreatures;

			// Token: 0x04006829 RID: 26665
			public bool PenetrateWalls;

			// Token: 0x0400682A RID: 26666
			public bool Quiet;
		}
	}
}
