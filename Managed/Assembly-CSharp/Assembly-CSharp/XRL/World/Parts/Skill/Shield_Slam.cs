using System;
using System.Text;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001385 RID: 4997
	[Serializable]
	public class Shield_Slam : BaseSkill
	{
		// Token: 0x0600C1C8 RID: 49608 RVA: 0x0046CE64 File Offset: 0x0046B064
		public void CollectStats(Templates.StatCollector stats)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			int num = this.ParentObject.StatMod("Strength", 0);
			if (num > 0)
			{
				stringBuilder.Append(num).Append("d2");
			}
			GameObject shieldWithHighestAV = this.ParentObject.GetShieldWithHighestAV(null, null);
			if (shieldWithHighestAV == null)
			{
				stringBuilder.Compound("shield AV", '+');
			}
			else
			{
				Shield part = shieldWithHighestAV.GetPart<Shield>();
				int av = part.AV;
				string text = "Bludgeoning";
				string text2 = "";
				GetShieldSlamDamageEvent.GetFor(this.ParentObject, null, shieldWithHighestAV, ref text, ref text2, ref av, part.AV, num, true);
				stringBuilder.CompoundSigned(av);
				if (!text2.IsNullOrEmpty())
				{
					stringBuilder.Append(text2);
				}
			}
			stats.Set("Damage", stringBuilder.ToString(), num > 0, num);
			if (num > 0)
			{
				stats.AddChangePostfix("Damage", num, num * 2, "high strength");
			}
			stats.Set("KnockdownSave", "Strength " + (Shield_Slam.SlamSave + num).ToString(), num != 0, num);
			stats.AddChangePostfix("Knockdown save", num, (num > 0) ? "high strength" : "low strength");
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Shield_Slam.Cooldown, 0);
		}

		// Token: 0x0600C1C9 RID: 49609 RVA: 0x0046CFA3 File Offset: 0x0046B1A3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600C1CA RID: 49610 RVA: 0x0046CFC9 File Offset: 0x0046B1C9
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C1CB RID: 49611 RVA: 0x0046CFEC File Offset: 0x0046B1EC
		public override bool HandleEvent(CommandEvent E)
		{
			return (!(E.Command == Shield_Slam.COMMAND_NAME) || this.PerformShieldSlam()) && base.HandleEvent(E);
		}

		// Token: 0x0600C1CC RID: 49612 RVA: 0x0046D014 File Offset: 0x0046B214
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (GameObject.Validate(E.Target) && E.Distance <= 1 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.CanMoveExtremities("ShieldSlam", false, false, false) && E.Actor.GetShield(null, null) != null && !E.Target.HasEffect<Prone>() && E.Actor.PhaseAndFlightMatches(E.Target))
			{
				E.Add(Shield_Slam.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C1CD RID: 49613 RVA: 0x0046D0A2 File Offset: 0x0046B2A2
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C1CE RID: 49614 RVA: 0x0046D0A5 File Offset: 0x0046B2A5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ChargedTarget");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C1CF RID: 49615 RVA: 0x0046D0BC File Offset: 0x0046B2BC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ChargedTarget")
			{
				GameObject shieldWithHighestAV = this.ParentObject.GetShieldWithHighestAV(null, null);
				if (shieldWithHighestAV != null)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
					if (GameObject.Validate(ref gameObjectParameter) && !gameObjectParameter.IsNowhere())
					{
						this.Slam(gameObjectParameter, shieldWithHighestAV, null, true);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C1D0 RID: 49616 RVA: 0x0046D11C File Offset: 0x0046B31C
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Shield Slam", Shield_Slam.COMMAND_NAME, "Skills", null, "\u0011", null, false, false, false, true, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600C1D1 RID: 49617 RVA: 0x0046D15E File Offset: 0x0046B35E
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x0600C1D2 RID: 49618 RVA: 0x0046D170 File Offset: 0x0046B370
		public bool Slam(GameObject TargetObject, GameObject ShieldObj, Cell TargetCell = null, bool Free = false)
		{
			if (ShieldObj == null || !GameObject.Validate(this.ParentObject))
			{
				return false;
			}
			if (TargetCell == null)
			{
				TargetCell = TargetObject.CurrentCell;
				if (TargetCell == null)
				{
					return false;
				}
			}
			Event @event = Event.New("BeginAttack", 0, 0, 0);
			@event.SetParameter("TargetObject", TargetObject);
			@event.SetParameter("TargetCell", TargetCell);
			if (!this.ParentObject.FireEvent(@event))
			{
				return false;
			}
			if (!Free)
			{
				this.ParentObject.UseEnergy(1000, "Combat Melee Skill ShieldSlam", null, null, false);
			}
			if (this.ParentObject.IsPlayer())
			{
				this.ParentObject.Target = TargetObject;
			}
			if (!this.ParentObject.PhaseMatches(TargetObject))
			{
				base.DidXToY("try", "to shield slam", TargetObject, string.Concat(new string[]
				{
					", but ",
					this.ParentObject.its_(ShieldObj, false),
					ShieldObj.GetVerb("pass", true, false, false, true, false),
					" through ",
					TargetObject.them
				}), "!", null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
			else if (TargetObject.MakeSave("Strength", Shield_Slam.SlamSave, this.ParentObject, null, "ShieldSlam Slam", false, false, false, false, ShieldObj))
			{
				string text = "slam";
				string text2 = "into";
				string text3 = "with";
				string text4 = null;
				string text5 = "!";
				string text6 = null;
				string text7 = null;
				GameObject gameObject = this.ParentObject;
				base.DidXToYWithZ(text, text2, TargetObject, text3, ShieldObj, text4, text5, text6, text7, this.ParentObject, null, false, false, false, false, false, false, false, false, null, null, gameObject, null, false, false, false, false, false, null);
				Brain brain = TargetObject.Brain;
				if (brain != null)
				{
					brain.Attacked(this.ParentObject, ShieldObj, 1f);
				}
				if (IComponent<GameObject>.Visible(TargetObject))
				{
					TargetObject.ParticleText("*resisted*", IComponent<GameObject>.ConsequentialColorChar(TargetObject, null), false, 1.5f, -8f);
				}
				if (this.ParentObject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage(TargetObject.Does("resist", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " your shield slam.", 'r', true);
				}
				else if (TargetObject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You resist " + this.ParentObject.poss("shield slam", true) + ".", 'g', true);
				}
			}
			else
			{
				string text8 = "slam";
				string text9 = "into";
				string text10 = "with";
				string text11 = null;
				string text12 = "!";
				string text13 = null;
				string text14 = null;
				GameObject gameObject = this.ParentObject;
				base.DidXToYWithZ(text8, text9, TargetObject, text10, ShieldObj, text11, text12, text13, text14, this.ParentObject, null, false, false, false, false, false, false, false, false, null, null, gameObject, null, false, false, false, false, false, null);
				Event event2 = Event.New("ObjectAttacking", 0, 0, 0);
				event2.SetParameter("Object", this.ParentObject);
				event2.SetParameter("TargetObject", TargetObject);
				event2.SetParameter("TargetCell", TargetCell);
				if (TargetCell.FireEvent(event2) && !TargetObject.IsNowhere())
				{
					Shield part = ShieldObj.GetPart<Shield>();
					int num = this.ParentObject.StatMod("Strength", 0);
					int num2 = ((num > 0) ? (num.ToString() + "d2").RollCached() : 0) + part.AV;
					string text15 = "Bludgeoning";
					string text16 = null;
					GetShieldSlamDamageEvent.GetFor(this.ParentObject, TargetObject, ShieldObj, ref text15, ref text16, ref num2, part.AV, num, false);
					TargetObject.ApplyEffect(new Prone(), null);
					TargetObject.TakeDamage(ref num2, text15, null, null, null, this.ParentObject, null, null, null, "from %t shield slam!", false, false, false, false, false, false, false, false, false, 5, null);
					ShieldSlamPerformedEvent.Send(this.ParentObject, TargetObject, ShieldObj, part.AV, num2);
				}
			}
			return true;
		}

		// Token: 0x0600C1D3 RID: 49619 RVA: 0x0046D518 File Offset: 0x0046B718
		public bool PerformShieldSlam()
		{
			GameObject shieldWithHighestAV = this.ParentObject.GetShieldWithHighestAV(null, null);
			if (shieldWithHighestAV == null)
			{
				return this.ParentObject.Fail("You must have a shield equipped to perform a shield slam.");
			}
			if (!this.ParentObject.CanMoveExtremities("ShieldSlam", true, false, false))
			{
				return false;
			}
			string text = base.PickDirectionS("Shield Slam", null);
			if (text.IsNullOrEmpty())
			{
				return false;
			}
			Cell cellFromDirection = this.ParentObject.CurrentCell.GetCellFromDirection(text, true);
			if (cellFromDirection == null)
			{
				return false;
			}
			GameObject combatTarget = cellFromDirection.GetCombatTarget(this.ParentObject, false, false, true, 0, null, null, null, null, null, true, false, null);
			if (combatTarget == null)
			{
				GameObject combatTarget2 = cellFromDirection.GetCombatTarget(this.ParentObject, true, false, true, 0, null, null, null, null, null, true, false, null);
				if (combatTarget2 != null)
				{
					return this.ParentObject.Fail(string.Concat(new string[]
					{
						"You cannot reach ",
						combatTarget2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" to shield slam ",
						combatTarget2.them,
						"."
					}));
				}
				return this.ParentObject.Fail("There's nothing there you can shield slam.");
			}
			else
			{
				if (combatTarget == this.ParentObject)
				{
					return this.ParentObject.Fail("You cannot shield slam " + this.ParentObject.itself + ".");
				}
				if (!this.Slam(combatTarget, shieldWithHighestAV, cellFromDirection, false))
				{
					return false;
				}
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Shield_Slam.Cooldown, null, null, false);
				return true;
			}
		}

		// Token: 0x04004D81 RID: 19841
		public static readonly string COMMAND_NAME = "CommandShieldSlam";

		// Token: 0x04004D82 RID: 19842
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D83 RID: 19843
		public static readonly int Cooldown = 40;

		// Token: 0x04004D84 RID: 19844
		public static readonly int SlamSave = 20;
	}
}
