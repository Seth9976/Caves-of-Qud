using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001345 RID: 4933
	[Serializable]
	public class HeavyWeapons_Sweep : BaseSkill
	{
		// Token: 0x0600C06F RID: 49263 RVA: 0x00467BB9 File Offset: 0x00465DB9
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Shots", HeavyWeapons_Sweep.BASE_SHOTS, false, 0);
			stats.Set("ConeWidth", HeavyWeapons_Sweep.BASE_WIDTH, false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), HeavyWeapons_Sweep.COOLDOWN, 0);
		}

		// Token: 0x0600C070 RID: 49264 RVA: 0x00467BF8 File Offset: 0x00465DF8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C071 RID: 49265 RVA: 0x00467C1E File Offset: 0x00465E1E
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C072 RID: 49266 RVA: 0x00467C44 File Offset: 0x00465E44
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Actor == this.ParentObject && E.Distance < 8 && GameObject.Validate(E.Target) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.CanMoveExtremities(null, false, false, true) && !E.Target.HasAdjacentAllyOf(E.Actor) && E.Distance < Stat.Random(3, 8) && E.Actor.HasMissileWeapon(null, delegate(MissileWeapon mw)
			{
				if (!HeavyWeapons_Sweep.IsHeavyWeapon(mw))
				{
					return false;
				}
				if (!mw.ReadyToFire())
				{
					return false;
				}
				int num = mw.AmmoPerAction * HeavyWeapons_Sweep.GetShots(E.Actor);
				if (num > 0)
				{
					int @for = GetAmmoCountAvailableEvent.GetFor(mw.ParentObject, mw);
					if (@for > 0 && Stat.Random(mw.AmmoPerAction * 2, num) > @for)
					{
						return false;
					}
				}
				return true;
			}))
			{
				E.Add(HeavyWeapons_Sweep.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C073 RID: 49267 RVA: 0x00467D31 File Offset: 0x00465F31
		public override bool HandleEvent(CommandEvent E)
		{
			return (!(E.Command == HeavyWeapons_Sweep.COMMAND_NAME) || this.PerformSweep(E.Actor)) && base.HandleEvent(E);
		}

		// Token: 0x0600C074 RID: 49268 RVA: 0x00467D5C File Offset: 0x00465F5C
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Sweep", HeavyWeapons_Sweep.COMMAND_NAME, "Skills", null, "®", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C075 RID: 49269 RVA: 0x00467DA4 File Offset: 0x00465FA4
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x0600C076 RID: 49270 RVA: 0x00467DBB File Offset: 0x00465FBB
		public static bool IsHeavyWeapon(MissileWeapon MW)
		{
			return ((MW != null) ? MW.Skill : null) == "HeavyWeapons";
		}

		// Token: 0x0600C077 RID: 49271 RVA: 0x00467DD3 File Offset: 0x00465FD3
		public static bool IsHeavyWeapon(GameObject Object)
		{
			return HeavyWeapons_Sweep.IsHeavyWeapon(Object.GetPart<MissileWeapon>());
		}

		// Token: 0x0600C078 RID: 49272 RVA: 0x00467DE0 File Offset: 0x00465FE0
		public bool PerformSweep(GameObject Actor = null)
		{
			if (Actor == null)
			{
				Actor = this.ParentObject;
			}
			if (!GameObject.Validate(ref Actor))
			{
				return false;
			}
			if (!this.ParentObject.CanMoveExtremities(null, true, false, true))
			{
				return false;
			}
			bool flag = false;
			string text = null;
			GameObject gameObject = null;
			List<GameObject> missileWeapons = this.ParentObject.GetMissileWeapons(null, new Predicate<MissileWeapon>(HeavyWeapons_Sweep.IsHeavyWeapon));
			if (missileWeapons == null || missileWeapons.Count <= 0)
			{
				return this.ParentObject.Fail("You do not have a heavy missile weapon equipped.");
			}
			this.ParentObject.PlayWorldOrUISound("Sounds/Abilities/sfx_ability_skill_attack_generic_activate", null);
			foreach (GameObject gameObject2 in missileWeapons)
			{
				MissileWeapon part = gameObject2.GetPart<MissileWeapon>();
				if (!part.ReadyToFire() && Actor != null)
				{
					CommandReloadEvent.Execute(Actor, true, false, 0);
				}
				if (part.ReadyToFire())
				{
					flag = true;
					break;
				}
				if (text == null)
				{
					text = part.GetNotReadyToFireMessage();
					gameObject = gameObject2;
				}
			}
			if (!flag)
			{
				if (this.ParentObject.IsPlayer())
				{
					SoundManager.PlaySound((gameObject != null) ? gameObject.GetSoundTag("NoAmmoSound", null) : null, 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				}
				return this.ParentObject.Fail(text ?? ("You need to reload! (" + ControlManager.getCommandInputDescription("CmdReload", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + ")"));
			}
			if (!Combat.FireMissileWeapon(this.ParentObject, null, null, FireType.Normal, "HeavyWeapons", 0, HeavyWeapons_Sweep.GetShots(Actor), HeavyWeapons_Sweep.GetWidth(Actor), null, false))
			{
				return false;
			}
			Actor.UseEnergy(1000, "Physical Skill HeavyWeapons Sweep", null, null, false);
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, HeavyWeapons_Sweep.COOLDOWN, null, null, false);
			return true;
		}

		// Token: 0x0600C079 RID: 49273 RVA: 0x00467FB4 File Offset: 0x004661B4
		public static int GetShots(GameObject Actor)
		{
			return HeavyWeapons_Sweep.BASE_SHOTS + ((Actor != null) ? Actor.GetIntProperty("HeavyWeaponsSweepModifyShots", 0) : 0);
		}

		// Token: 0x0600C07A RID: 49274 RVA: 0x00467FCE File Offset: 0x004661CE
		public int GetShots()
		{
			return HeavyWeapons_Sweep.GetShots(this.ParentObject);
		}

		// Token: 0x0600C07B RID: 49275 RVA: 0x00467FDB File Offset: 0x004661DB
		public static int GetWidth(GameObject Actor)
		{
			return HeavyWeapons_Sweep.BASE_WIDTH + ((Actor != null) ? Actor.GetIntProperty("HeavyWeaponsSweepModifyWidth", 0) : 0);
		}

		// Token: 0x0600C07C RID: 49276 RVA: 0x00467FF5 File Offset: 0x004661F5
		public int GetWidth()
		{
			return HeavyWeapons_Sweep.GetWidth(this.ParentObject);
		}

		// Token: 0x04004D48 RID: 19784
		public static readonly string COMMAND_NAME = "CommandHeavyWeaponsSweep";

		// Token: 0x04004D49 RID: 19785
		public static readonly int BASE_SHOTS = 5;

		// Token: 0x04004D4A RID: 19786
		public static readonly int BASE_WIDTH = 90;

		// Token: 0x04004D4B RID: 19787
		public static readonly int COOLDOWN = 250;

		// Token: 0x04004D4C RID: 19788
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
