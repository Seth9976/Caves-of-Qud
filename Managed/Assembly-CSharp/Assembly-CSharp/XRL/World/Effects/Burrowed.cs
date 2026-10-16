using System;
using ConsoleLib.Console;
using UnityEngine;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000C44 RID: 3140
	[Serializable]
	public class Burrowed : Effect
	{
		// Token: 0x06008029 RID: 32809 RVA: 0x002F33DE File Offset: 0x002F15DE
		public Burrowed()
		{
			this.Duration = 1;
			this.DisplayName = "{{w|burrowed}}";
		}

		// Token: 0x0600802A RID: 32810 RVA: 0x002F340B File Offset: 0x002F160B
		public Burrowed(int MSPenalty)
			: this()
		{
			this.MovePenalty = MSPenalty;
		}

		// Token: 0x0600802B RID: 32811 RVA: 0x002F341A File Offset: 0x002F161A
		public override int GetEffectType()
		{
			return 128;
		}

		// Token: 0x0600802C RID: 32812 RVA: 0x002F3421 File Offset: 0x002F1621
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600802D RID: 32813 RVA: 0x002F3424 File Offset: 0x002F1624
		public override string GetDetails()
		{
			return "Traveling underground.";
		}

		// Token: 0x0600802E RID: 32814 RVA: 0x002F342C File Offset: 0x002F162C
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Burrowed>())
			{
				return false;
			}
			Object.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_burrowingClaws_burrow", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			base.DidX("burrow", "into the ground", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			this.EndAbilityID = base.AddMyActivatedAbility("Stop Burrowing", "CommandEndBurrowing", "Physical Mutations", null, "\u0018", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, Renderable.UITile("Abilities/abil_stop_burrow.bmp", 'w', 'W', " ", '\0'), null, null, null);
			Object.DustPuff("&y", 15);
			this.ApplyChanges();
			return true;
		}

		// Token: 0x0600802F RID: 32815 RVA: 0x002F34E7 File Offset: 0x002F16E7
		public override void Remove(GameObject Object)
		{
			base.RemoveMyActivatedAbility(ref this.EndAbilityID, null);
			Object.DustPuff("&y", 15);
			this.UnapplyChanges();
		}

		// Token: 0x06008030 RID: 32816 RVA: 0x002F350A File Offset: 0x002F170A
		private void ApplyChanges()
		{
			base.Object.Render.Visible = false;
			base.StatShifter.SetStatShift(base.Object, "MoveSpeed", this.MovePenalty, false);
		}

		// Token: 0x06008031 RID: 32817 RVA: 0x002F353B File Offset: 0x002F173B
		private void UnapplyChanges()
		{
			base.Object.Render.Visible = true;
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06008032 RID: 32818 RVA: 0x002F355F File Offset: 0x002F175F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == PooledEvent<CanTravelEvent>.ID || ID == PooledEvent<AIBoredEvent>.ID;
		}

		// Token: 0x06008033 RID: 32819 RVA: 0x002F3588 File Offset: 0x002F1788
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (this.Duration > 0 && !E.Involuntary && E.Object == base.Object)
			{
				if (E.ShowMessage)
				{
					E.Object.Fail("You cannot do that while burrowed.");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008034 RID: 32820 RVA: 0x002F35D6 File Offset: 0x002F17D6
		public override bool HandleEvent(CanTravelEvent E)
		{
			if (E.Object == base.Object)
			{
				return E.Object.Fail("You cannot travel long distances while burrowed.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x002F3600 File Offset: 0x002F1800
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("AIAttackMelee");
			Registrar.Register("AIAttackRange");
			Registrar.Register("AICanAttackMelee");
			Registrar.Register("AICanAttackRange");
			Registrar.Register("AILookForTarget");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BeforeDirectMove");
			Registrar.Register("BeforeTeleport");
			Registrar.Register("BodyPositionChanged");
			Registrar.Register("CanChangeBodyPosition");
			Registrar.Register("CanMoveExtremities");
			Registrar.Register("CommandEndBurrowing");
			Registrar.Register("FiringMissile");
			Registrar.Register("LateBeforeApplyDamage");
			Registrar.Register("MovementModeChanged");
			Registrar.Register("BeginAttack");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008036 RID: 32822 RVA: 0x002F36D0 File Offset: 0x002F18D0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AILookForTarget" || E.ID == "AIAttackMelee" || E.ID == "AIAttackRange" || E.ID == "AICanAttackMelee" || E.ID == "AICanAttackRange")
			{
				if (this.Duration > 0)
				{
					return false;
				}
			}
			else if (E.ID == "BeginAttack" || E.ID == "CanFireMissileWeapon")
			{
				if (this.Duration > 0)
				{
					if (base.Object.IsPlayer())
					{
						Popup.ShowFail("You cannot do that while burrowed.", true, true, true);
					}
					return false;
				}
			}
			else if (E.ID == "LateBeforeApplyDamage")
			{
				if (this.Duration > 0)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Owner");
					Damage damage = E.GetParameter("Damage") as Damage;
					if (gameObjectParameter != null && gameObjectParameter != base.Object && damage != null && !damage.HasAttribute("Bleeding"))
					{
						base.DidX("are", "forced to the surface", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
						base.Object.RemoveEffect(this, true);
						return false;
					}
				}
			}
			else if (E.ID == "BeforeDirectMove" || E.ID == "BeforeTeleport" || E.ID == "BeginAttack" || E.ID == "FiringMissile")
			{
				if (this.Duration > 0)
				{
					base.Object.RemoveEffect(this, true);
				}
			}
			else if (E.ID == "CanChangeBodyPosition" || E.ID == "CanMoveExtremities")
			{
				if (this.Duration > 0 && !E.HasFlag("Involuntary"))
				{
					if (E.HasFlag("ShowMessage") && base.Object.IsPlayer())
					{
						Popup.ShowFail("You cannot do that while burrowed.", true, true, true);
					}
					return false;
				}
			}
			else if (E.ID == "CommandEndBurrowing" || E.ID == "MovementModeChanged" || E.ID == "BodyPositionChanged")
			{
				this.Emerge();
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyChanges();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChanges();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008037 RID: 32823 RVA: 0x002F3970 File Offset: 0x002F1B70
		public void Emerge()
		{
			base.Object.PlayWorldOrUISound("Sounds/Abilities/sfx_ability_skill_generic", null);
			base.DidX("emerge", "from the ground", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			base.Object.RemoveEffect(this, true);
		}

		// Token: 0x06008038 RID: 32824 RVA: 0x002F39C4 File Offset: 0x002F1BC4
		public override bool HandleEvent(AIBoredEvent E)
		{
			long turns = The.Game.Turns;
			if (turns > this.BoredUnburrowCheck && base.Object.Target == null)
			{
				if (this.BoredUnburrowCheck != -1L && ((int)(50f * Mathf.InverseLerp(0f, 100f, (float)(turns - this.BoredUnburrowCheck)))).in100())
				{
					this.Emerge();
				}
				this.BoredUnburrowCheck = turns;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040038ED RID: 14573
		public Guid EndAbilityID = Guid.Empty;

		// Token: 0x040038EE RID: 14574
		public int MovePenalty;

		// Token: 0x040038EF RID: 14575
		[NonSerialized]
		private long BoredUnburrowCheck = -1L;
	}
}
