using System;
using XRL.Core;

namespace XRL.World.Capabilities
{
	// Token: 0x020009FB RID: 2555
	public static class WeaponUsageTracking
	{
		// Token: 0x06006CE7 RID: 27879 RVA: 0x002375C4 File Offset: 0x002357C4
		public static void TrackMeleeWeaponHit(GameObject Actor, GameObject Weapon, bool DefenderIsCreature, string DefenderBlueprint)
		{
			if (!GameObject.Validate(ref Weapon) || Weapon.Equipped == null)
			{
				return;
			}
			if (DefenderIsCreature)
			{
				Weapon.ModIntProperty("HitsAsMeleeWeapon", 1, false);
				Weapon.SetLongProperty("LastHitAsMeleeWeaponTurn", XRLCore.CurrentTurn);
				Weapon.SetStringProperty("LastHitAsMeleeWeaponBlueprint", DefenderBlueprint, false);
				if (Actor != null && Actor.IsPlayer())
				{
					Weapon.ModIntProperty("HitsAsMeleeWeaponByPlayer", 1, false);
					Weapon.SetLongProperty("LastHitAsMeleeWeaponByPlayerTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastHitAsMeleeWeaponByPlayerBlueprint", DefenderBlueprint, false);
					return;
				}
			}
			else
			{
				Weapon.ModIntProperty("ImpactsAsMeleeWeapon", 1, false);
				Weapon.SetLongProperty("LastImpactAsMeleeWeaponTurn", XRLCore.CurrentTurn);
				Weapon.SetStringProperty("LastImpactAsMeleeWeaponBlueprint", DefenderBlueprint, false);
				if (Actor != null && Actor.IsPlayer())
				{
					Weapon.ModIntProperty("ImpactsAsMeleeWeaponByPlayer", 1, false);
					Weapon.SetLongProperty("LastImpactAsMeleeWeaponByPlayerTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastImpactAsMeleeWeaponByPlayerBlueprint", DefenderBlueprint, false);
				}
			}
		}

		// Token: 0x06006CE8 RID: 27880 RVA: 0x002376B0 File Offset: 0x002358B0
		public static void TrackMeleeWeaponDamage(GameObject Actor, GameObject Weapon, bool DefenderIsCreature, string DefenderBlueprint, Damage Dmg)
		{
			if (!GameObject.Validate(ref Weapon) || Weapon.Equipped == null)
			{
				return;
			}
			if (Dmg == null || Dmg.Amount <= 0)
			{
				return;
			}
			if (DefenderIsCreature)
			{
				Weapon.ModIntProperty("InjuryAsMeleeWeapon", Dmg.Amount, false);
				Weapon.SetIntProperty("LastInjuryAsMeleeWeapon", Dmg.Amount, false);
				Weapon.SetLongProperty("LastInjuryAsMeleeWeaponTurn", XRLCore.CurrentTurn);
				Weapon.SetStringProperty("LastInjuryAsMeleeWeaponBlueprint", DefenderBlueprint, false);
				if (Actor != null && Actor.IsPlayer())
				{
					Weapon.ModIntProperty("InjuryAsMeleeWeaponByPlayer", Dmg.Amount, false);
					Weapon.SetIntProperty("LastInjuryAsMeleeWeaponByPlayer", Dmg.Amount, false);
					Weapon.SetLongProperty("LastInjuryAsMeleeWeaponByPlayerTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastInjuryAsMeleeWeaponByPlayerBlueprint", DefenderBlueprint, false);
					return;
				}
			}
			else
			{
				Weapon.ModIntProperty("DamageAsMeleeWeapon", Dmg.Amount, false);
				Weapon.SetIntProperty("LastDamageAsMeleeWeapon", Dmg.Amount, false);
				Weapon.SetLongProperty("LastDamageAsMeleeWeaponTurn", XRLCore.CurrentTurn);
				Weapon.SetStringProperty("LastDamageAsMeleeWeaponBlueprint", DefenderBlueprint, false);
				if (Actor != null && Actor.IsPlayer())
				{
					Weapon.ModIntProperty("DamageAsMeleeWeaponByPlayer", Dmg.Amount, false);
					Weapon.SetIntProperty("LastDamageAsMeleeWeaponByPlayer", Dmg.Amount, false);
					Weapon.SetLongProperty("LastDamageAsMeleeWeaponByPlayerTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastDamageAsMeleeWeaponByPlayerBlueprint", DefenderBlueprint, false);
				}
			}
		}

		// Token: 0x06006CE9 RID: 27881 RVA: 0x00237818 File Offset: 0x00235A18
		public static void TrackThrownWeaponHit(GameObject Actor, GameObject Weapon, bool DefenderIsCreature, string DefenderBlueprint, bool Accidental)
		{
			if (!GameObject.Validate(ref Weapon))
			{
				return;
			}
			if (DefenderIsCreature)
			{
				Weapon.ModIntProperty("HitsAsThrownWeapon", 1, false);
				Weapon.SetLongProperty("LastHitAsThrownWeaponTurn", XRLCore.CurrentTurn);
				Weapon.SetStringProperty("LastHitAsThrownWeaponBlueprint", DefenderBlueprint, false);
				if (Actor != null && Actor.IsPlayer())
				{
					Weapon.ModIntProperty("HitsAsThrownWeaponByPlayer", 1, false);
					Weapon.SetLongProperty("LastHitAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastHitAsThrownWeaponByPlayerBlueprint", DefenderBlueprint, false);
				}
				if (Accidental)
				{
					Weapon.ModIntProperty("AccidentalHitsAsThrownWeapon", 1, false);
					Weapon.SetLongProperty("LastAccidentalHitAsThrownWeaponTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastAccidentalHitAsThrownWeaponBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Weapon.ModIntProperty("AccidentalHitsAsThrownWeaponOnByPlayer", 1, false);
						Weapon.SetLongProperty("LastAccidentalHitAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
						Weapon.SetStringProperty("LastAccidentalHitAsThrownWeaponByPlayerBlueprint", DefenderBlueprint, false);
						return;
					}
				}
			}
			else
			{
				Weapon.ModIntProperty("ImpactsAsThrownWeapon", 1, false);
				Weapon.SetLongProperty("LastImpactAsThrownWeaponTurn", XRLCore.CurrentTurn);
				Weapon.SetStringProperty("LastImpactAsThrownWeaponBlueprint", DefenderBlueprint, false);
				if (Actor != null && Actor.IsPlayer())
				{
					Weapon.ModIntProperty("ImpactsAsThrownWeaponByPlayer", 1, false);
					Weapon.SetLongProperty("LastImpactAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastImpactAsThrownWeaponByPlayerBlueprint", DefenderBlueprint, false);
				}
				if (Accidental)
				{
					Weapon.ModIntProperty("AccidentalImpactsAsThrownWeapon", 1, false);
					Weapon.SetLongProperty("LastAccidentalImpactAsThrownWeaponTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastAccidentalImpactAsThrownWeaponBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Weapon.ModIntProperty("AccidentalImpactsAsThrownWeaponByPlayer", 1, false);
						Weapon.SetLongProperty("LastAccidentalImpactAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
						Weapon.SetStringProperty("LastAccidentalImpactAsThrownWeaponByPlayerBlueprint", DefenderBlueprint, false);
					}
				}
			}
		}

		// Token: 0x06006CEA RID: 27882 RVA: 0x002379CC File Offset: 0x00235BCC
		public static void TrackThrownWeaponDamage(GameObject Actor, GameObject Weapon, bool DefenderIsCreature, string DefenderBlueprint, bool Accidental, int Amount)
		{
			if (!GameObject.Validate(ref Weapon))
			{
				return;
			}
			if (Amount <= 0)
			{
				return;
			}
			if (DefenderIsCreature)
			{
				Weapon.ModIntProperty("InjuryAsThrownWeapon", Amount, false);
				Weapon.SetIntProperty("LastInjuryAsThrownWeapon", Amount, false);
				Weapon.SetLongProperty("LastInjuryAsThrownWeaponTurn", XRLCore.CurrentTurn);
				Weapon.SetStringProperty("LastInjuryAsThrownWeaponBlueprint", DefenderBlueprint, false);
				if (Actor != null && Actor.IsPlayer())
				{
					Weapon.ModIntProperty("InjuryAsThrownWeaponByPlayer", Amount, false);
					Weapon.SetIntProperty("LastInjuryAsThrownWeaponByPlayer", Amount, false);
					Weapon.SetLongProperty("LastInjuryAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastInjuryAsThrownWeaponByPlayerBlueprint", DefenderBlueprint, false);
				}
				if (Accidental)
				{
					Weapon.ModIntProperty("AccidentalInjuryAsThrownWeapon", Amount, false);
					Weapon.SetIntProperty("LastAccidentalInjuryAsThrownWeapon", Amount, false);
					Weapon.SetLongProperty("LastAccidentalInjuryAsThrownWeaponTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastAccidentalInjuryAsThrownWeaponBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Weapon.ModIntProperty("AccidentalInjuryAsThrownWeaponByPlayer", Amount, false);
						Weapon.SetIntProperty("LastAccidentalInjuryAsThrownWeaponByPlayer", Amount, false);
						Weapon.SetLongProperty("LastAccidentalInjuryAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
						Weapon.SetStringProperty("LastAccidentalInjuryAsThrownWeaponByPlayerBlueprint", DefenderBlueprint, false);
						return;
					}
				}
			}
			else
			{
				Weapon.ModIntProperty("DamageAsThrownWeapon", Amount, false);
				Weapon.SetIntProperty("LastDamageAsThrownWeapon", Amount, false);
				Weapon.SetLongProperty("LastDamageAsThrownWeaponTurn", XRLCore.CurrentTurn);
				Weapon.SetStringProperty("LastDamageAsThrownWeaponBlueprint", DefenderBlueprint, false);
				if (Actor != null && Actor.IsPlayer())
				{
					Weapon.ModIntProperty("DamageAsThrownWeaponByPlayer", Amount, false);
					Weapon.SetIntProperty("LastDamageAsThrownWeaponByPlayer", Amount, false);
					Weapon.SetLongProperty("LastDamageAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastDamageAsThrownWeaponByPlayerBlueprint", DefenderBlueprint, false);
				}
				if (Accidental)
				{
					Weapon.ModIntProperty("AccidentalDamageAsThrownWeapon", Amount, false);
					Weapon.SetIntProperty("LastAccidentalDamageAsThrownWeapon", Amount, false);
					Weapon.SetLongProperty("LastAccidentalDamageAsThrownWeaponTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastAccidentalDamageAsThrownWeaponBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Weapon.ModIntProperty("AccidentalDamageAsThrownWeaponByPlayer", Amount, false);
						Weapon.SetIntProperty("LastAccidentalDamageAsThrownWeaponByPlayer", Amount, false);
						Weapon.SetLongProperty("LastAccidentalDamageAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
						Weapon.SetStringProperty("LastAccidentalDamageAsThrownWeaponByPlayerBlueprint", DefenderBlueprint, false);
					}
				}
			}
		}

		// Token: 0x06006CEB RID: 27883 RVA: 0x00237C08 File Offset: 0x00235E08
		public static void TrackThrownWeaponDamage(GameObject Actor, GameObject Weapon, bool DefenderIsCreature, string DefenderBlueprint, bool Accidental, Damage Dmg)
		{
			WeaponUsageTracking.TrackThrownWeaponDamage(Actor, Weapon, DefenderIsCreature, DefenderBlueprint, Accidental, (Dmg != null) ? Dmg.Amount : 0);
		}

		// Token: 0x06006CEC RID: 27884 RVA: 0x00237C24 File Offset: 0x00235E24
		public static void TrackMissileWeaponHit(GameObject Actor, GameObject Launcher, GameObject Projectile, bool DefenderIsCreature, string DefenderBlueprint, bool Accidental)
		{
			if (DefenderIsCreature)
			{
				if (GameObject.Validate(ref Launcher))
				{
					Launcher.ModIntProperty("HitsAsLauncher", 1, false);
					Launcher.SetLongProperty("LastHitAsLauncherTurn", XRLCore.CurrentTurn);
					Launcher.SetStringProperty("LastHitAsLauncherBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Launcher.ModIntProperty("HitsAsLauncherByPlayer", 1, false);
						Launcher.SetLongProperty("LastHitAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
						Launcher.SetStringProperty("LastHitAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
					}
					if (Accidental)
					{
						Launcher.ModIntProperty("AccidentalHitsAsLauncher", 1, false);
						Launcher.SetLongProperty("LastAccidentalHitAsLauncherTurn", XRLCore.CurrentTurn);
						Launcher.SetStringProperty("LastAccidentalHitAsLauncherBlueprint", DefenderBlueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Launcher.ModIntProperty("AccidentalHitsAsLauncherByPlayer", 1, false);
							Launcher.SetLongProperty("LastAccidentalHitAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
							Launcher.SetStringProperty("LastAccidentalHitAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
						}
					}
				}
				if (GameObject.Validate(ref Projectile))
				{
					Projectile.ModIntProperty("HitsAsProjectile", 1, false);
					Projectile.SetLongProperty("LastHitAsProjectileTurn", XRLCore.CurrentTurn);
					Projectile.SetStringProperty("LastHitAsProjectileBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Projectile.ModIntProperty("HitsAsProjectileByPlayer", 1, false);
						Projectile.SetLongProperty("LastHitAsProjectileByPlayerTurn", XRLCore.CurrentTurn);
						Projectile.SetStringProperty("LastHitAsProjectileByPlayerBlueprint", DefenderBlueprint, false);
					}
					if (Accidental)
					{
						Projectile.ModIntProperty("AccidentalHitsAsProjectile", 1, false);
						Projectile.SetLongProperty("LastAccidentalHitAsProjectileTurn", XRLCore.CurrentTurn);
						Projectile.SetStringProperty("LastAccidentalHitAsProjectileBlueprint", DefenderBlueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Projectile.ModIntProperty("AccidentalHitsAsProjectileByPlayer", 1, false);
							Projectile.SetLongProperty("LastAccidentalHitAsProjectileByPlayerTurn", XRLCore.CurrentTurn);
							Projectile.SetStringProperty("LastAccidentalHitAsProjectileByPlayerBlueprint", DefenderBlueprint, false);
							return;
						}
					}
				}
			}
			else
			{
				if (GameObject.Validate(ref Launcher))
				{
					Launcher.ModIntProperty("ImpactsAsLauncher", 1, false);
					Launcher.SetLongProperty("LastImpactAsLauncherTurn", XRLCore.CurrentTurn);
					Launcher.SetStringProperty("LastImpactAsLauncherBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Launcher.ModIntProperty("ImpactsAsLauncherByPlayer", 1, false);
						Launcher.SetLongProperty("LastImpactAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
						Launcher.SetStringProperty("LastImpactAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
					}
					if (Accidental)
					{
						Launcher.ModIntProperty("AccidentalImpactsAsLauncher", 1, false);
						Launcher.SetLongProperty("LastAccidentalImpactAsLauncherTurn", XRLCore.CurrentTurn);
						Launcher.SetStringProperty("LastAccidentalImpactAsLauncherBlueprint", DefenderBlueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Launcher.ModIntProperty("AccidentalImpactsAsLauncherByPlayer", 1, false);
							Launcher.SetLongProperty("LastAccidentalImpactAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
							Launcher.SetStringProperty("LastAccidentalImpactAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
						}
					}
				}
				if (GameObject.Validate(ref Projectile))
				{
					Projectile.ModIntProperty("ImpactsAsProjectile", 1, false);
					Projectile.SetLongProperty("LastImpactAsProjectileTurn", XRLCore.CurrentTurn);
					Projectile.SetStringProperty("LastImpactAsProjectileBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Projectile.ModIntProperty("ImpactsAsProjectileByPlayer", 1, false);
						Projectile.SetLongProperty("LastImpactAsProjectileByPlayerTurn", XRLCore.CurrentTurn);
						Projectile.SetStringProperty("LastImpactAsProjectileByPlayerBlueprint", DefenderBlueprint, false);
					}
					if (Accidental)
					{
						Projectile.ModIntProperty("AccidentalImpactsAsProjectile", 1, false);
						Projectile.SetLongProperty("LastAccidentalImpactAsProjectileTurn", XRLCore.CurrentTurn);
						Projectile.SetStringProperty("LastAccidentalImpactAsProjectileBlueprint", DefenderBlueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Projectile.ModIntProperty("AccidentalImpactsAsProjectileByPlayer", 1, false);
							Projectile.SetLongProperty("LastAccidentalImpactAsProjectileByPlayerTurn", XRLCore.CurrentTurn);
							Projectile.SetStringProperty("LastAccidentalImpactAsProjectileByPlayerBlueprint", DefenderBlueprint, false);
						}
					}
				}
			}
		}

		// Token: 0x06006CED RID: 27885 RVA: 0x00237F9C File Offset: 0x0023619C
		public static void TrackMissileWeaponDamage(GameObject Actor, GameObject Launcher, GameObject Projectile, bool DefenderIsCreature, string DefenderBlueprint, bool Accidental, Damage Dmg)
		{
			if (Dmg == null || Dmg.Amount <= 0)
			{
				return;
			}
			if (DefenderIsCreature)
			{
				if (GameObject.Validate(ref Launcher))
				{
					Launcher.ModIntProperty("InjuryAsLauncher", Dmg.Amount, false);
					Launcher.SetIntProperty("LastInjuryAsLauncher", Dmg.Amount, false);
					Launcher.SetLongProperty("LastInjuryAsLauncherTurn", XRLCore.CurrentTurn);
					Launcher.SetStringProperty("LastInjuryAsLauncherBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Launcher.ModIntProperty("InjuryAsLauncherByPlayer", Dmg.Amount, false);
						Launcher.SetIntProperty("LastInjuryAsLauncherByPlayer", Dmg.Amount, false);
						Launcher.SetLongProperty("LastInjuryAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
						Launcher.SetStringProperty("LastInjuryAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
					}
					if (Accidental)
					{
						Launcher.ModIntProperty("AccidentalInjuryAsLauncher", Dmg.Amount, false);
						Launcher.SetIntProperty("LastAccidentalInjuryAsLauncher", Dmg.Amount, false);
						Launcher.SetLongProperty("LastAccidentalInjuryAsLauncherTurn", XRLCore.CurrentTurn);
						Launcher.SetStringProperty("LastAccidentalInjuryAsLauncherBlueprint", DefenderBlueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Launcher.ModIntProperty("AccidentalInjuryAsLauncherByPlayer", Dmg.Amount, false);
							Launcher.SetIntProperty("LastAccidentalInjuryAsLauncherByPlayer", Dmg.Amount, false);
							Launcher.SetLongProperty("LastAccidentalInjuryAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
							Launcher.SetStringProperty("LastAccidentalInjuryAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
						}
					}
				}
				if (Projectile != null)
				{
					Projectile.ModIntProperty("InjuryAsProjectile", Dmg.Amount, false);
					Projectile.SetIntProperty("LastInjuryAsLauncher", Dmg.Amount, false);
					Projectile.SetLongProperty("LastInjuryAsLauncherTurn", XRLCore.CurrentTurn);
					Projectile.SetStringProperty("LastInjuryAsLauncherBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Projectile.ModIntProperty("InjuryAsProjectileByPlayer", Dmg.Amount, false);
						Projectile.SetIntProperty("LastInjuryAsLauncherByPlayer", Dmg.Amount, false);
						Projectile.SetLongProperty("LastInjuryAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
						Projectile.SetStringProperty("LastInjuryAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
					}
					if (Accidental)
					{
						Projectile.ModIntProperty("AccidentalInjuryAsProjectile", Dmg.Amount, false);
						Projectile.SetIntProperty("LastAccidentalInjuryAsLauncher", Dmg.Amount, false);
						Projectile.SetLongProperty("LastAccidentalInjuryAsLauncherTurn", XRLCore.CurrentTurn);
						Projectile.SetStringProperty("LastAccidentalInjuryAsLauncherBlueprint", DefenderBlueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Projectile.ModIntProperty("AccidentalInjuryAsProjectileByPlayer", Dmg.Amount, false);
							Projectile.SetIntProperty("LastAccidentalInjuryAsLauncherByPlayer", Dmg.Amount, false);
							Projectile.SetLongProperty("LastAccidentalInjuryAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
							Projectile.SetStringProperty("LastAccidentalInjuryAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
							return;
						}
					}
				}
			}
			else
			{
				if (GameObject.Validate(ref Launcher))
				{
					Launcher.ModIntProperty("DamageAsLauncher", Dmg.Amount, false);
					Launcher.SetIntProperty("LastDamageAsLauncher", Dmg.Amount, false);
					Launcher.SetLongProperty("LastDamageAsLauncherTurn", XRLCore.CurrentTurn);
					Launcher.SetStringProperty("LastDamageAsLauncherBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Launcher.ModIntProperty("DamageAsLauncherByPlayer", Dmg.Amount, false);
						Launcher.SetIntProperty("LastDamageAsLauncherByPlayer", Dmg.Amount, false);
						Launcher.SetLongProperty("LastDamageAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
						Launcher.SetStringProperty("LastDamageAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
					}
					if (Accidental)
					{
						Launcher.ModIntProperty("AccidentalDamageAsLauncher", Dmg.Amount, false);
						Launcher.SetIntProperty("LastAccidentalDamageAsLauncher", Dmg.Amount, false);
						Launcher.SetLongProperty("LastAccidentalDamageAsLauncherTurn", XRLCore.CurrentTurn);
						Launcher.SetStringProperty("LastAccidentalDamageAsLauncherBlueprint", DefenderBlueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Launcher.ModIntProperty("AccidentalDamageAsLauncherByPlayer", Dmg.Amount, false);
							Launcher.SetIntProperty("LastAccidentalDamageAsLauncherByPlayer", Dmg.Amount, false);
							Launcher.SetLongProperty("LastAccidentalDamageAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
							Launcher.SetStringProperty("LastAccidentalDamageAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
						}
					}
				}
				if (GameObject.Validate(ref Projectile))
				{
					Projectile.ModIntProperty("DamageAsProjectile", Dmg.Amount, false);
					Projectile.SetIntProperty("LastDamageAsLauncher", Dmg.Amount, false);
					Projectile.SetLongProperty("LastDamageAsLauncherTurn", XRLCore.CurrentTurn);
					Projectile.SetStringProperty("LastDamageAsLauncherBlueprint", DefenderBlueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Projectile.ModIntProperty("DamageAsProjectileByPlayer", Dmg.Amount, false);
						Projectile.SetIntProperty("LastDamageAsLauncherByPlayer", Dmg.Amount, false);
						Projectile.SetLongProperty("LastDamageAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
						Projectile.SetStringProperty("LastDamageAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
					}
					if (Accidental)
					{
						Projectile.ModIntProperty("AccidentalDamageAsProjectile", Dmg.Amount, false);
						Projectile.SetIntProperty("LastAccidentalDamageAsLauncher", Dmg.Amount, false);
						Projectile.SetLongProperty("LastAccidentalDamageAsLauncherTurn", XRLCore.CurrentTurn);
						Projectile.SetStringProperty("LastAccidentalDamageAsLauncherBlueprint", DefenderBlueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Projectile.ModIntProperty("AccidentalDamageAsProjectileByPlayer", Dmg.Amount, false);
							Projectile.SetIntProperty("LastAccidentalDamageAsLauncherByPlayer", Dmg.Amount, false);
							Projectile.SetLongProperty("LastAccidentalDamageAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
							Projectile.SetStringProperty("LastAccidentalDamageAsLauncherByPlayerBlueprint", DefenderBlueprint, false);
						}
					}
				}
			}
		}

		// Token: 0x06006CEE RID: 27886 RVA: 0x002384C4 File Offset: 0x002366C4
		public static void TrackKill(GameObject Actor, GameObject Defender, GameObject Weapon, GameObject Projectile, bool Accidental)
		{
			if (!GameObject.Validate(ref Weapon) && !GameObject.Validate(ref Projectile))
			{
				return;
			}
			if (Defender.IsCreature)
			{
				if (GameObject.Validate(ref Projectile))
				{
					if (Weapon == Projectile)
					{
						Weapon.ModIntProperty("KillsAsThrownWeapon", 1, false);
						Weapon.SetLongProperty("LastKillAsThrownWeaponTurn", XRLCore.CurrentTurn);
						Weapon.SetStringProperty("LastKillAsThrownWeaponBlueprint", Defender.Blueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Weapon.ModIntProperty("KillsAsThrownWeaponByPlayer", 1, false);
							Weapon.SetLongProperty("LastKillAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
							Weapon.SetStringProperty("LastKillAsThrownWeaponByPlayerBlueprint", Defender.Blueprint, false);
						}
						if (Accidental)
						{
							Weapon.ModIntProperty("AccidentalKillsAsThrownWeapon", 1, false);
							Weapon.SetLongProperty("LastAccidentalKillAsThrownWeaponTurn", XRLCore.CurrentTurn);
							Weapon.SetStringProperty("LastAccidentalKillAsThrownWeaponBlueprint", Defender.Blueprint, false);
							if (Actor != null && Actor.IsPlayer())
							{
								Weapon.ModIntProperty("AccidentalKillsAsThrownWeaponByPlayer", 1, false);
								Weapon.SetLongProperty("LastAccidentalKillAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
								Weapon.SetStringProperty("LastAccidentalKillAsThrownWeaponByPlayerBlueprint", Defender.Blueprint, false);
								return;
							}
						}
					}
					else
					{
						Projectile.ModIntProperty("KillsAsProjectile", 1, false);
						Projectile.SetLongProperty("LastKillAsProjectileTurn", XRLCore.CurrentTurn);
						Projectile.SetStringProperty("LastKillAsProjectileBlueprint", Defender.Blueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Projectile.ModIntProperty("KillsAsProjectileByPlayer", 1, false);
							Projectile.SetLongProperty("LastKillAsProjectileByPlayerTurn", XRLCore.CurrentTurn);
							Projectile.SetStringProperty("LastKillAsProjectileByPlayerBlueprint", Defender.Blueprint, false);
						}
						if (GameObject.Validate(ref Weapon))
						{
							Weapon.ModIntProperty("KillsAsLauncher", 1, false);
							Weapon.SetLongProperty("LastKillAsLauncherTurn", XRLCore.CurrentTurn);
							Weapon.SetStringProperty("LastKillAsLauncherBlueprint", Defender.Blueprint, false);
							if (Actor != null && Actor.IsPlayer())
							{
								Weapon.ModIntProperty("KillsAsLauncherByPlayer", 1, false);
								Weapon.SetLongProperty("LastKillAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
								Weapon.SetStringProperty("LastKillAsLauncherByPlayerBlueprint", Defender.Blueprint, false);
							}
						}
						if (Accidental)
						{
							Projectile.ModIntProperty("AccidentalKillsAsProjectile", 1, false);
							Projectile.SetLongProperty("LastAccidentalKillAsProjectileTurn", XRLCore.CurrentTurn);
							Projectile.SetStringProperty("LastAccidentalKillAsProjectileBlueprint", Defender.Blueprint, false);
							if (Actor != null && Actor.IsPlayer())
							{
								Projectile.ModIntProperty("AccidentalKillsAsProjectileByPlayer", 1, false);
								Projectile.SetLongProperty("LastAccidentalKillAsProjectileByPlayerTurn", XRLCore.CurrentTurn);
								Projectile.SetStringProperty("LastAccidentalKillAsProjectileByPlayerBlueprint", Defender.Blueprint, false);
							}
							if (GameObject.Validate(ref Weapon))
							{
								Weapon.ModIntProperty("AccidentalKillsAsLauncher", 1, false);
								Weapon.SetLongProperty("LastAccidentalKillAsLauncherTurn", XRLCore.CurrentTurn);
								Weapon.SetStringProperty("LastAccidentalKillAsLauncherBlueprint", Defender.Blueprint, false);
								if (Actor != null && Actor.IsPlayer())
								{
									Weapon.ModIntProperty("AccidentalKillsAsLauncherByPlayer", 1, false);
									Weapon.SetLongProperty("LastAccidentalKillAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
									Weapon.SetStringProperty("LastAccidentalKillAsLauncherByPlayerBlueprint", Defender.Blueprint, false);
									return;
								}
							}
						}
					}
				}
				else if (GameObject.Validate(ref Weapon) && Weapon.Equipped != null)
				{
					Weapon.ModIntProperty("KillsAsMeleeWeapon", 1, false);
					Weapon.SetLongProperty("LastKillAsMeleeWeaponTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastKillAsMeleeWeaponBlueprint", Defender.Blueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Weapon.ModIntProperty("KillsAsMeleeWeaponByPlayer", 1, false);
						Weapon.SetLongProperty("LastKillAsMeleeWeaponByPlayerTurn", XRLCore.CurrentTurn);
						Weapon.SetStringProperty("LastKillAsMeleeWeaponByPlayerBlueprint", Defender.Blueprint, false);
					}
					if (Accidental)
					{
						Weapon.ModIntProperty("AccidentalKillsAsMeleeWeapon", 1, false);
						Weapon.SetLongProperty("LastAccidentalKillAsMeleeWeaponTurn", XRLCore.CurrentTurn);
						Weapon.SetStringProperty("LastAccidentalKillAsMeleeWeaponBlueprint", Defender.Blueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Weapon.ModIntProperty("AccidentalKillsAsMeleeWeaponByPlayer", 1, false);
							Weapon.SetLongProperty("LastAccidentalKillAsMeleeWeaponByPlayerTurn", XRLCore.CurrentTurn);
							Weapon.SetStringProperty("LastAccidentalKillAsMeleeWeaponByPlayerBlueprint", Defender.Blueprint, false);
							return;
						}
					}
				}
			}
			else if (GameObject.Validate(ref Projectile))
			{
				if (Weapon == Projectile)
				{
					Weapon.ModIntProperty("DestroysAsThrownWeapon", 1, false);
					Weapon.SetLongProperty("LastDestroyAsThrownWeaponTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastDestroyAsThrownWeaponBlueprint", Defender.Blueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Weapon.ModIntProperty("DestroysAsThrownWeaponByPlayer", 1, false);
						Weapon.SetLongProperty("LastDestroyAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
						Weapon.SetStringProperty("LastDestroyAsThrownWeaponByPlayerBlueprint", Defender.Blueprint, false);
					}
					if (Accidental)
					{
						Weapon.ModIntProperty("AccidentalDestroysAsThrownWeapon", 1, false);
						Weapon.SetLongProperty("LastAccidentalDestroyAsThrownWeaponTurn", XRLCore.CurrentTurn);
						Weapon.SetStringProperty("LastAccidentalDestroyAsThrownWeaponBlueprint", Defender.Blueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Weapon.ModIntProperty("AccidentalDestroysAsThrownWeaponByPlayer", 1, false);
							Weapon.SetLongProperty("LastAccidentalDestroyAsThrownWeaponByPlayerTurn", XRLCore.CurrentTurn);
							Weapon.SetStringProperty("LastAccidentalDestroyAsThrownWeaponByPlayerBlueprint", Defender.Blueprint, false);
							return;
						}
					}
				}
				else
				{
					Projectile.ModIntProperty("AccidentalDestroysAsProjectile", 1, false);
					Projectile.SetLongProperty("LastAccidentalDestroyAsProjectileTurn", XRLCore.CurrentTurn);
					Projectile.SetStringProperty("LastAccidentalDestroyAsProjectileBlueprint", Defender.Blueprint, false);
					if (Actor != null && Actor.IsPlayer())
					{
						Projectile.ModIntProperty("AccidentalDestroysAsProjectileByPlayer", 1, false);
						Projectile.SetLongProperty("LastAccidentalDestroyAsProjectileByPlayerTurn", XRLCore.CurrentTurn);
						Projectile.SetStringProperty("LastAccidentalDestroyAsProjectileByPlayerBlueprint", Defender.Blueprint, false);
					}
					if (GameObject.Validate(ref Weapon))
					{
						Weapon.ModIntProperty("AccidentalDestroysAsLauncher", 1, false);
						Weapon.SetLongProperty("LastAccidentalDestroyAsLauncherTurn", XRLCore.CurrentTurn);
						Weapon.SetStringProperty("LastAccidentalDestroyAsLauncherBlueprint", Defender.Blueprint, false);
						if (Actor != null && Actor.IsPlayer())
						{
							Weapon.ModIntProperty("AccidentalDestroysAsLauncherByPlayer", 1, false);
							Weapon.SetLongProperty("LastAccidentalDestroyAsLauncherByPlayerTurn", XRLCore.CurrentTurn);
							Weapon.SetStringProperty("LastAccidentalDestroyAsLauncherByPlayerBlueprint", Defender.Blueprint, false);
							return;
						}
					}
				}
			}
			else if (GameObject.Validate(ref Weapon) && Weapon.Equipped != null)
			{
				Weapon.ModIntProperty("DestroysAsMeleeWeapon", 1, false);
				Weapon.SetLongProperty("LastDestroyAsMeleeWeaponTurn", XRLCore.CurrentTurn);
				Weapon.SetStringProperty("LastDestroyAsMeleeWeaponBlueprint", Defender.Blueprint, false);
				if (Actor != null && Actor.IsPlayer())
				{
					Weapon.ModIntProperty("DestroysAsMeleeWeaponByPlayer", 1, false);
					Weapon.SetLongProperty("LastDestroyAsMeleeWeaponByPlayerTurn", XRLCore.CurrentTurn);
					Weapon.SetStringProperty("LastDestroyAsMeleeWeaponByPlayerBlueprint", Defender.Blueprint, false);
				}
			}
		}
	}
}
