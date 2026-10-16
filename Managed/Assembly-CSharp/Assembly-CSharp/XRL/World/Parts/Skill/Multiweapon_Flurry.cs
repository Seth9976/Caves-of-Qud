using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001354 RID: 4948
	[Serializable]
	public class Multiweapon_Flurry : BaseSkill
	{
		// Token: 0x0600C0BF RID: 49343 RVA: 0x00468658 File Offset: 0x00466858
		public void CollectStats(Templates.StatCollector stats)
		{
			int num = Multiweapon_Flurry.COOLDOWN;
			if (this.ParentObject.HasSkill("Multiweapon_Expertise"))
			{
				stats.AddChangePostfix("Cooldown", -10, "Multiweapon Expertise");
				num -= 10;
			}
			if (this.ParentObject.HasSkill("Multiweapon_Mastery"))
			{
				stats.AddChangePostfix("Cooldown", -10, "Multiweapon Mastery");
				num -= 10;
			}
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), num, num - Multiweapon_Flurry.COOLDOWN);
		}

		// Token: 0x0600C0C0 RID: 49344 RVA: 0x004686DA File Offset: 0x004668DA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetMeleeAttackChanceEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C0C1 RID: 49345 RVA: 0x00468700 File Offset: 0x00466900
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0C2 RID: 49346 RVA: 0x00468724 File Offset: 0x00466924
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance == 1 && !this.ParentObject.HasEffect<Burrowed>() && this.ParentObject.CanMoveExtremities("Flurry", false, false, false) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandFlurry", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0C3 RID: 49347 RVA: 0x00468783 File Offset: 0x00466983
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (E.Properties.HasDelimitedSubstring(',', "Flurrying", StringComparison.Ordinal))
			{
				E.SetFinalizedChance((E.Intrinsic && !E.Consecutive) ? 100 : 0);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C0C4 RID: 49348 RVA: 0x004687BE File Offset: 0x004669BE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandFlurry");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C0C5 RID: 49349 RVA: 0x004687D4 File Offset: 0x004669D4
		public bool PerformFlurry()
		{
			if (!this.ParentObject.CheckFrozen(false, false, false, null))
			{
				return false;
			}
			if (base.OnWorldMap)
			{
				this.ParentObject.Fail("You cannot do that on the world map.");
				return false;
			}
			if (this.ParentObject.HasEffect<Burrowed>())
			{
				this.ParentObject.Fail("You cannot do that while burrowed.");
				return false;
			}
			if (!this.ParentObject.CanMoveExtremities("Flurry", true, false, false))
			{
				return false;
			}
			Cell cell = base.PickDirection("Flurry", null);
			if (cell == null)
			{
				return false;
			}
			GameObject gameObject = cell.GetCombatTarget(this.ParentObject, false, false, true, 0, null, null, null, null, null, true, false, null);
			if (gameObject == null)
			{
				gameObject = cell.GetCombatTarget(this.ParentObject, true, false, true, 0, null, null, null, null, null, true, false, null);
				if (gameObject != null)
				{
					this.ParentObject.Fail(string.Concat(new string[]
					{
						"You cannot reach ",
						gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" to attack ",
						gameObject.them,
						"."
					}));
				}
				else
				{
					this.ParentObject.Fail("There is nothing there you can attack.");
				}
				return false;
			}
			Event @event = Event.New("BeginAttack", 0, 0, 0);
			@event.SetParameter("TargetObject", gameObject);
			@event.SetParameter("TargetCell", (gameObject != null) ? gameObject.CurrentCell : null);
			if (!this.ParentObject.FireEvent(@event))
			{
				return false;
			}
			this.ParentObject.PlayWorldOrUISound("Sounds/Abilities/sfx_ability_skill_attack_generic_activate", null);
			int num = Multiweapon_Flurry.COOLDOWN;
			if (this.ParentObject.HasSkill("Multiweapon_Expertise"))
			{
				num -= 10;
			}
			if (this.ParentObject.HasSkill("Multiweapon_Mastery"))
			{
				num -= 10;
			}
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, num, null, "Agility", false);
			base.DidX("launch", "into a flurry of attacks", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			return Combat.PerformMeleeAttack(this.ParentObject, gameObject, 1000, 0, 0, 0, "Flurrying", false);
		}

		// Token: 0x0600C0C6 RID: 49350 RVA: 0x004689F1 File Offset: 0x00466BF1
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "CommandFlurry") || this.PerformFlurry()) && base.FireEvent(E);
		}

		// Token: 0x0600C0C7 RID: 49351 RVA: 0x00468A18 File Offset: 0x00466C18
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Flurry", "CommandFlurry", "Skills", null, "ð", null, false, false, false, true, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600C0C8 RID: 49352 RVA: 0x00468A5A File Offset: 0x00466C5A
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x04004D56 RID: 19798
		public const string ABL_CMD = "CommandFlurry";

		// Token: 0x04004D57 RID: 19799
		public static readonly int COOLDOWN = 60;

		// Token: 0x04004D58 RID: 19800
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
