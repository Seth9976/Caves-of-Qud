using System;
using XRL.Messages;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001371 RID: 4977
	[Serializable]
	public class Rifle_DrawABead : BaseSkill
	{
		// Token: 0x0600C16B RID: 49515 RVA: 0x0046BC74 File Offset: 0x00469E74
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<EarlyBeforeBeginTakeActionEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == SuspendingEvent.ID || ID == ApplyEffectEvent.ID;
		}

		// Token: 0x0600C16C RID: 49516 RVA: 0x0046BCAC File Offset: 0x00469EAC
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance >= 3 && GameObject.Validate(E.Target) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && !this.ParentObject.IsConfused && this.ParentObject.CanMoveExtremities(null, false, false, false) && !E.Target.HasEffect<RifleMark>((RifleMark fx) => fx.Marker == this.ParentObject) && E.Distance <= this.ParentObject.GetVisibilityRadius())
			{
				if (this.ParentObject.HasMissileWeapon(null, (MissileWeapon mw) => Rifle_DrawABead.IsCompatibleMissileWeapon(mw) && mw.ReadyToFire()) && this.ParentObject.HasLOSTo(E.Target, true, false, true, true, null))
				{
					E.Add(Rifle_DrawABead.COMMAND_NAME, 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C16D RID: 49517 RVA: 0x0046BD94 File Offset: 0x00469F94
		public override bool HandleEvent(EarlyBeforeBeginTakeActionEvent E)
		{
			this.ValidateMark();
			return true;
		}

		// Token: 0x0600C16E RID: 49518 RVA: 0x0046BDA0 File Offset: 0x00469FA0
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Rifle_DrawABead.COMMAND_NAME)
			{
				if (this.ParentObject.IsConfused)
				{
					return this.ParentObject.Fail("You cannot mark a target while you are confused.");
				}
				if (!this.ParentObject.CanMoveExtremities(null, true, false, false))
				{
					return false;
				}
				Cell cell = base.PickDestinationCell(80, AllowVis.OnlyVisible, true, false, true, true, PickTarget.PickStyle.EmptyCell, "Mark Target", true, null);
				if (cell == null)
				{
					return true;
				}
				GameObject gameObject = cell.GetCombatTarget(this.ParentObject, true, false, false, 0, null, null, null, null, null, true, false, null) ?? cell.GetCombatTarget(this.ParentObject, true, false, false, 5, null, null, null, null, null, true, false, null);
				if (gameObject != null)
				{
					this.SetMark(gameObject);
				}
				this.ParentObject.UseEnergy(1000, "Physical Skill", null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C16F RID: 49519 RVA: 0x0046BE76 File Offset: 0x0046A076
		public override bool HandleEvent(SuspendingEvent E)
		{
			this.ClearMark();
			return true;
		}

		// Token: 0x0600C170 RID: 49520 RVA: 0x0046BE7F File Offset: 0x0046A07F
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			if (E.Name == "Confusion")
			{
				this.ClearMark();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C171 RID: 49521 RVA: 0x0046BEA0 File Offset: 0x0046A0A0
		public bool SetMark(GameObject Target)
		{
			if (!Target.ApplyEffect(new RifleMark(this.ParentObject), null))
			{
				return false;
			}
			if (this.ParentObject.IsPlayer())
			{
				IComponent<GameObject>.PlayUISound("Sounds/Abilities/sfx_ability_rifle_markTarget", 1f, false, SoundRequest.SoundEffectType.None);
			}
			else
			{
				Target.PlayWorldSound("Sounds/Abilities/sfx_ability_rifle_markTarget", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			this.ClearMark();
			this.Mark = Target;
			if (this.ParentObject.IsPlayer())
			{
				Sidebar.CurrentTarget = Target;
			}
			if (base.Visible())
			{
				base.DidXToY("draw", "a bead on", Target, ((Target.IsPlayer() || this.ParentObject.IsPlayer()) ? "" : The.Player.DescribeDirectionToward(Target, false, false)) + ". " + Target.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, false, true, false, false, null, false, false, true, null, true, null, false) + " marked", null, null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
			return true;
		}

		// Token: 0x0600C172 RID: 49522 RVA: 0x0046BFC8 File Offset: 0x0046A1C8
		public void ClearMark()
		{
			if (GameObject.Validate(ref this.Mark))
			{
				RifleMark effect = this.Mark.GetEffect<RifleMark>((RifleMark fx) => fx.Marker == this.ParentObject);
				if (effect != null)
				{
					this.Mark.RemoveEffect(effect, true);
				}
				this.Mark = null;
			}
		}

		// Token: 0x0600C173 RID: 49523 RVA: 0x0046C014 File Offset: 0x0046A214
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Mark Target", Rifle_DrawABead.COMMAND_NAME, "Skills", null, "Î", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600C174 RID: 49524 RVA: 0x0046C056 File Offset: 0x0046A256
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x0600C175 RID: 49525 RVA: 0x0046C068 File Offset: 0x0046A268
		public void ValidateMark()
		{
			if (GameObject.Validate(ref this.Mark))
			{
				if (this.Mark.IsNowhere())
				{
					this.ClearMark();
					return;
				}
				if (this.ParentObject.IsPlayer() && !this.Mark.IsVisible())
				{
					MessageQueue.AddPlayerMessage("You lose sight of your mark.", 'R', true);
					this.ClearMark();
					return;
				}
				if (!this.ParentObject.IsPlayer() && (!this.ParentObject.HasLOSTo(this.Mark, true, false, true, true, null) || this.ParentObject.DistanceTo(this.Mark) > this.ParentObject.GetVisibilityRadius()))
				{
					this.ClearMark();
					return;
				}
				if (!this.Mark.HasEffect<RifleMark>((RifleMark fx) => fx.Marker == this.ParentObject))
				{
					if (this.ParentObject.IsPlayer())
					{
						MessageQueue.AddPlayerMessage("Your tracking of your mark has been disrupted.", null, true);
					}
					this.ClearMark();
				}
			}
		}

		// Token: 0x0600C176 RID: 49526 RVA: 0x0046C149 File Offset: 0x0046A349
		public static bool IsCompatibleMissileWeapon(string Skill)
		{
			return Skill == "Rifle" || Skill == "Bow";
		}

		// Token: 0x0600C177 RID: 49527 RVA: 0x0046C165 File Offset: 0x0046A365
		public static bool IsCompatibleMissileWeapon(MissileWeapon MW)
		{
			return Rifle_DrawABead.IsCompatibleMissileWeapon((MW != null) ? MW.Skill : null);
		}

		// Token: 0x0600C178 RID: 49528 RVA: 0x0046C178 File Offset: 0x0046A378
		public static bool IsCompatibleMissileWeapon(GameObject Object)
		{
			return Rifle_DrawABead.IsCompatibleMissileWeapon((Object != null) ? Object.GetPart<MissileWeapon>() : null);
		}

		// Token: 0x04004D76 RID: 19830
		public static readonly string COMMAND_NAME = "CommandMarkTarget";

		// Token: 0x04004D77 RID: 19831
		public GameObject Mark;

		// Token: 0x04004D78 RID: 19832
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
