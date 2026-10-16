using System;
using System.Linq;
using XRL.Collections;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013FC RID: 5116
	[Serializable]
	public class SunderMind : BaseMutation
	{
		// Token: 0x0600C65B RID: 50779 RVA: 0x00483BEE File Offset: 0x00481DEE
		public SunderMind()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C65C RID: 50780 RVA: 0x00483C17 File Offset: 0x00481E17
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == SingletonEvent<UseEnergyEvent>.ID || ID == TookDamageEvent.ID;
		}

		// Token: 0x0600C65D RID: 50781 RVA: 0x00483C48 File Offset: 0x00481E48
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 80 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && this.activeRounds <= 0 && GameObject.Validate(E.Target) && (!E.Target.HasPart<SunderMind>() || E.Target.GetPart<SunderMind>().activeRounds <= 0) && !E.Target.HasEffect<MemberOfPsychicBattle>() && E.Target.Brain != null && !E.Target.HasPart<MentalShield>() && (E.Actor.HasLOSTo(E.Target, false, false, false, true, null) || (E.Actor.HasPart<SensePsychic>() && E.Distance < E.Actor.GetPart<SensePsychic>().Radius)) && (!this.RealityDistortionBased || (base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0) && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Target, null, E.Actor, null, this, null, 0))))
			{
				E.Add("CommandSunderMind", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C65E RID: 50782 RVA: 0x00483D78 File Offset: 0x00481F78
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.activeRounds > 0 && this.activeTarget.Object != null)
			{
				try
				{
					this.Tick();
					goto IL_0037;
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("SunderMind::BeforeTakeAction", ex, "game_exception");
					goto IL_0037;
				}
			}
			this.EndSunder();
			IL_0037:
			return base.HandleEvent(E);
		}

		// Token: 0x0600C65F RID: 50783 RVA: 0x00483DD4 File Offset: 0x00481FD4
		public override bool HandleEvent(UseEnergyEvent E)
		{
			if (this.activeRounds > 0 && (!E.Passive || (E.Type != null && !E.Type.Contains("Pass"))))
			{
				this.CancelSunder();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C660 RID: 50784 RVA: 0x00483E10 File Offset: 0x00482010
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object == this.ParentObject && this.activeRounds > 0 && (E.Damage.HasAttribute("Mental") || E.Damage.HasAttribute("Psionic")))
			{
				this.CancelSunder();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C661 RID: 50785 RVA: 0x00483E65 File Offset: 0x00482065
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandSunderMind");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C662 RID: 50786 RVA: 0x00483E7C File Offset: 0x0048207C
		public string GetDamageDice(int Level)
		{
			if (Level < 3)
			{
				if (Level % 2 == 1)
				{
					return "1d3";
				}
				return "1d4";
			}
			else
			{
				if (Level % 2 == 1)
				{
					return "1d3+" + (Level / 2).ToString();
				}
				return "1d4+" + ((Level - 1) / 2).ToString();
			}
		}

		// Token: 0x0600C663 RID: 50787 RVA: 0x00483ED4 File Offset: 0x004820D4
		public void CancelSunder()
		{
			this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_sunderMind_abort", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (this.ParentObject.IsPlayer() && this.activeTarget != null && this.activeTarget.Object != null)
			{
				IComponent<GameObject>.AddPlayerMessage("Your concentration slips and the channel between you and " + this.activeTarget.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " dissipates into aether.", null, true);
			}
			else if (this.ParentObject.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("Your concentration slips and the channel dissipates.", null, true);
			}
			this.EndSunder();
		}

		// Token: 0x0600C664 RID: 50788 RVA: 0x00483F98 File Offset: 0x00482198
		public void EndSunder()
		{
			this.activeRounds = 0;
			if (this.activeTarget != null)
			{
				GameObject @object = this.activeTarget.Object;
				if (@object != null)
				{
					MemberOfPsychicBattle effect = @object.GetEffect<MemberOfPsychicBattle>();
					if (effect != null && !effect.isAttacker)
					{
						@object.RemoveEffect<MemberOfPsychicBattle>();
					}
				}
				GameObjectReference.Free(ref this.activeTarget);
			}
			MemberOfPsychicBattle effect2 = this.ParentObject.GetEffect<MemberOfPsychicBattle>();
			if (effect2 != null && effect2.isAttacker)
			{
				this.ParentObject.RemoveEffect<MemberOfPsychicBattle>();
			}
		}

		// Token: 0x0600C665 RID: 50789 RVA: 0x0048400A File Offset: 0x0048220A
		public int GetCooldown(int Level)
		{
			return 80;
		}

		// Token: 0x0600C666 RID: 50790 RVA: 0x00484010 File Offset: 0x00482210
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("DamageIncrement", this.GetDamageDice(Level), !stats.mode.Contains("ability"), 0);
			stats.Set("Range", "sight", false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C667 RID: 50791 RVA: 0x00484070 File Offset: 0x00482270
		public override string GetDescription()
		{
			return "You sunder the mind of an enemy, leaving them reeling in pain.";
		}

		// Token: 0x0600C668 RID: 50792 RVA: 0x00484078 File Offset: 0x00482278
		public override string GetLevelText(int Level)
		{
			return "" + "For up to 10 rounds, you engage in psychic combat with an opponent, dealing damage each round.\n" + "Taking any action other than passing the turn will break the connection.\n" + "Each round you make a mental attack vs mental armor (MA).\n" + "Damage increment: {{rules|" + this.GetDamageDice(Level) + "}}\n" + "After the tenth round, you deal bonus damage equal to the total amount of damage you've done so far.\n" + "Range: sight\n" + "Cooldown: " + this.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600C669 RID: 50793 RVA: 0x004840F0 File Offset: 0x004822F0
		public void BeginSunder(GameObject target)
		{
			this.accruedDamage = 0;
			if (target == null)
			{
				return;
			}
			if (!target.HasPart<Brain>())
			{
				return;
			}
			if (target.HasEffect<MemberOfPsychicBattle>())
			{
				return;
			}
			SoundManager.PreloadClipSet("Sounds/Abilities/sfx_ability_sunderMind_abort");
			SoundManager.PreloadClipSet("Sounds/Abilities/sfx_ability_sunderMind_dig");
			SoundManager.PreloadClipSet("Sounds/Abilities/sfx_ability_sunderMind_final");
			SoundManager.PreloadClipSet("Sounds/Abilities/sfx_ability_sunderMind_headExplode");
			this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_sunderMind_attack", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			target.PlayWorldSound("Sounds/Abilities/sfx_ability_sunderMind_attack", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (target.ApplyEffect(new MemberOfPsychicBattle(this.ParentObject, false), null))
			{
				this.activeRounds = 10;
				this.activeTarget = target.Reference();
				this.ParentObject.ApplyEffect(new MemberOfPsychicBattle(target, true), null);
				if (this.ParentObject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						"You burrow a channel through the psychic aether to ",
						target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" and begin to sunder ",
						target.its,
						" mind!"
					}), null, true);
				}
				else if (target.IsPlayer())
				{
					Popup.Show(string.Concat(new string[]
					{
						this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" ",
						The.Player.DescribeDirectionToward(this.ParentObject, false, false),
						this.ParentObject.GetVerb("burrow", true, false, false, true, false),
						" a channel through the psychic aether and",
						this.ParentObject.GetVerb("begin", true, false, false, true, false),
						" to sunder your mind!"
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					AutoAct.Interrupt(null, null, this.ParentObject, true);
				}
				this.Tick();
			}
		}

		// Token: 0x0600C66A RID: 50794 RVA: 0x0048430C File Offset: 0x0048250C
		public void Tick()
		{
			Rack<Effect> effects = this.ParentObject.Effects;
			bool flag;
			if (effects == null)
			{
				flag = false;
			}
			else
			{
				flag = effects.Any<Effect>((Effect e) => e.IsOfTypes(33554434));
			}
			if (flag)
			{
				this.CancelSunder();
				return;
			}
			MemberOfPsychicBattle effect = this.ParentObject.GetEffect<MemberOfPsychicBattle>();
			if (effect == null)
			{
				this.CancelSunder();
				return;
			}
			effect.turnsRemaining = this.activeRounds;
			this.Nosebleed(this.ParentObject, effect);
			GameObject @object = this.activeTarget.Object;
			if (@object == null || @object.IsInvalid())
			{
				this.CancelSunder();
				return;
			}
			if (!@object.InSameZone(this.ParentObject))
			{
				if (!@object.IsValid())
				{
					this.EndSunder();
					return;
				}
				this.CancelSunder();
				return;
			}
			else
			{
				MemberOfPsychicBattle effect2 = this.ParentObject.GetEffect<MemberOfPsychicBattle>();
				if (effect2 != null)
				{
					effect2.turnsRemaining = this.activeRounds;
					this.Nosebleed(@object, effect2);
					if (this.activeRounds > 1)
					{
						this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_sunderMind_dig", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						@object.PlayWorldSound("Sounds/Abilities/sfx_ability_sunderMind_dig", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						if (this.ParentObject.IsPlayer() || base.Visible())
						{
							this.ParentObject.ParticleBlip("&Y~", (10 - this.activeRounds) / 2, 0L, false);
						}
						base.PerformMentalAttack(new Mental.Attack(this.Blast), this.ParentObject, @object, null, "SunderMind Blast", null, 8388609, 100, int.MinValue, Math.Max(this.ParentObject.StatMod("Ego", 0), base.Level - 2), 0);
					}
					else
					{
						int num = this.accruedDamage;
						if (Options.SifrahPsychicCombat)
						{
							if (this.ParentObject.IsPlayer())
							{
								PsychicCombatSifrah psychicCombatSifrah = new PsychicCombatSifrah(@object, "SunderMind", base.Level, @object.Stat("Level", 0) / 5 + @object.StatMod("Willpower", 0), "sundering the mind of " + @object.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
								psychicCombatSifrah.Play(@object);
								num = num * (psychicCombatSifrah.Performance + 100) / 100;
							}
							else if (@object.IsPlayer())
							{
								PsychicCombatSifrah psychicCombatSifrah2 = new PsychicCombatSifrah(this.ParentObject, "SunderMind", this.ParentObject.Stat("Level", 0) / 5 + this.ParentObject.StatMod("Ego", 0), base.Level, "defending against having your mind sundered by " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
								psychicCombatSifrah2.Play(this.ParentObject);
								num = num * (100 - psychicCombatSifrah2.Performance) / 100;
							}
						}
						if (num > @object.hitpoints)
						{
							this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_sunderMind_headExplode", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							@object.PlayWorldSound("Sounds/Abilities/sfx_ability_sunderMind_headExplode", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							if (@object.IsPlayer())
							{
								Popup.Show("Your sense of self is pulled apart by what feels like a billion years of geologic pressure.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								Popup.Show("Your head explodes!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								Achievement.HEAD_EXPLODE.Unlock();
							}
							else if (@object.IsVisible())
							{
								IComponent<GameObject>.AddPlayerMessage(@object.Poss("head", true) + " explodes!", null, true);
							}
							@object.BigBloodsplatter(true);
						}
						if (num > 0)
						{
							this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_sunderMind_final", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							@object.PlayWorldSound("Sounds/Abilities/sfx_ability_sunderMind_final", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							GameObject gameObject = @object;
							int num2 = num;
							string text = "from the cumulative trauma of %t mental assault!";
							string text2 = "Mental Psionic Unavoidable";
							string text3 = "Your head was exploded by " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".";
							string text4 = @object.Its + " head was exploded by " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".";
							GameObject parentObject = this.ParentObject;
							gameObject.TakeDamage(num2, text, text2, text3, text4, this.ParentObject, parentObject, this.ParentObject, null, null, false, false, false, false, false, false, false, false, false, 5, null);
						}
					}
					this.activeRounds--;
					if (this.activeRounds <= 0)
					{
						this.EndSunder();
					}
					return;
				}
				this.CancelSunder();
				return;
			}
		}

		// Token: 0x0600C66B RID: 50795 RVA: 0x0048480C File Offset: 0x00482A0C
		public void Nosebleed(GameObject Target, MemberOfPsychicBattle TargetEffect)
		{
			if (!Target.IsValid() || Target.HasPart<MentalShield>())
			{
				return;
			}
			if (TargetEffect.turnsRemaining <= 5)
			{
				string text;
				if (Target.CanHaveNosebleed())
				{
					if (TargetEffect.turnsRemaining == 5)
					{
						IComponent<GameObject>.EmitMessage(Target, Target.Poss("nose begins to bleed.", true), ' ', false, false, false, null, null);
					}
					text = "nosebleed";
				}
				else if (Target.HasPart<Robot>())
				{
					if (TargetEffect.turnsRemaining == 5)
					{
						IComponent<GameObject>.EmitMessage(Target, Target.Poss("core begins to leak.", true), ' ', false, false, false, null, null);
					}
					text = "processor leak";
				}
				else
				{
					if (TargetEffect.turnsRemaining == 5)
					{
						IComponent<GameObject>.EmitMessage(Target, Target.Poss("brain begins to hemorrhage.", true), ' ', false, false, false, null, null);
					}
					text = "hemorrhage";
				}
				text = Grammar.A(text, false);
				int num = 1;
				string text2 = "You died from " + text + ".";
				GameObject parentObject = this.ParentObject;
				GameObject parentObject2 = this.ParentObject;
				Target.TakeDamage(num, "from " + text + ".", "Physical Circulatory Unavoidable", text2, null, parentObject2, parentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
			}
		}

		// Token: 0x0600C66C RID: 50796 RVA: 0x00484920 File Offset: 0x00482B20
		public int RollDamage(GameObject GO, int Penetrations)
		{
			int num = 0;
			string damageDice = this.GetDamageDice(base.Level);
			for (int i = 0; i < Penetrations; i++)
			{
				num += damageDice.RollCached();
			}
			return num;
		}

		// Token: 0x0600C66D RID: 50797 RVA: 0x00484952 File Offset: 0x00482B52
		public void PenetrationFailure(GameObject GO)
		{
			if (this.ParentObject.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("Your attack fails to penetrate " + GO.poss("mental defenses", true) + ".", 'r', true);
			}
		}

		// Token: 0x0600C66E RID: 50798 RVA: 0x00484984 File Offset: 0x00482B84
		public bool Blast(MentalAttackEvent E)
		{
			GameObject defender = E.Defender;
			int penetrations = E.Penetrations;
			if (penetrations == 0)
			{
				this.PenetrationFailure(defender);
				return false;
			}
			int num = this.RollDamage(defender, penetrations) * 100 / E.Magnitude;
			if (num <= 0)
			{
				this.PenetrationFailure(defender);
				return false;
			}
			this.accruedDamage += num;
			Damage damage = new Damage(num);
			damage.AddAttribute("Mental");
			damage.AddAttribute("Psionic");
			string text = Stat.GetResultColor(penetrations);
			text = text.Replace("&", "");
			Cell currentCell = defender.CurrentCell;
			Event @event = Event.New("TakeDamage", 0, 0, 0);
			@event.SetParameter("Damage", damage);
			@event.SetParameter("Owner", this.ParentObject);
			@event.SetParameter("Attacker", this.ParentObject);
			@event.SetParameter("Perspective", this.ParentObject);
			@event.SetParameter("Phase", 5);
			@event.SetFlag("NoDamageMessage", true);
			@event.SetFlag("SilentIfNoDamage", true);
			if (E.IsPlayerInvolved())
			{
				@event.SetParameter("Message", Event.NewStringBuilder("{{").Append(ColorCoding.ConsequentialColorChar(E.Attacker, E.Defender)).Append('|')
					.Append(E.Attacker.IsPlayer() ? "You" : E.Attacker.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false))
					.Append(' ')
					.Append(E.Attacker.GetVerb("sunder", false, false, false, true, false))
					.Append(' ')
					.Append(E.Defender.poss("mind", true))
					.Append("{{")
					.Append(text)
					.Append("|(x")
					.Append(penetrations)
					.Append(")}} for %d damage!}}")
					.ToString());
			}
			if (!defender.FireEvent(@event) || damage.Amount == 0)
			{
				if (E.IsPlayerInvolved())
				{
					IComponent<GameObject>.XDidYToZ(E.Attacker, "attempt", "to burrow a channel through the psychic aether and sunder", E.Defender, "mind, but the attack has no effect", ".", null, null, E.Defender, E.Attacker, false, false, false, false, true, null, null, null, false, false, false, false, false, null);
				}
			}
			else if (currentCell != null)
			{
				currentCell.PsychicPulse();
				if (!defender.IsValid())
				{
					this.EndSunder();
				}
			}
			return true;
		}

		// Token: 0x0600C66F RID: 50799 RVA: 0x00484C04 File Offset: 0x00482E04
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandSunderMind")
			{
				if (this.RealityDistortionBased && !this.ParentObject.IsRealityDistortionUsable(null, null))
				{
					RealityStabilized.ShowGenericInterdictMessage(this.ParentObject, null);
					return false;
				}
				Cell cell = base.PickDestinationCell(80, AllowVis.OnlyVisible, true, false, true, true, PickTarget.PickStyle.EmptyCell, "Sunder Mind", true, new Predicate<GameObject>(this.HasOurSensePsychic));
				if (cell == null)
				{
					return false;
				}
				GameObject gameObject = cell.GetCombatTarget(this.ParentObject, true, false, false, 5, null, null, null, null, null, true, false, null);
				if (gameObject != null && gameObject.Brain == null)
				{
					gameObject = null;
				}
				if (gameObject == null)
				{
					gameObject = cell.GetFirstObjectWithPart("Brain");
				}
				bool flag = false;
				if (gameObject != null && !gameObject.IsVisible())
				{
					SensePsychicEffect ourSensePsychic = this.GetOurSensePsychic(gameObject);
					if (ourSensePsychic == null)
					{
						gameObject = null;
					}
					else if (!ourSensePsychic.Identified)
					{
						flag = true;
						gameObject = null;
					}
				}
				if (gameObject == this.ParentObject && gameObject.IsPlayer() && Popup.ShowYesNo("Are you sure you want to sunder your own mind?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.No)
				{
					return false;
				}
				if (gameObject == null)
				{
					if (this.ParentObject.IsPlayer())
					{
						if (flag)
						{
							Popup.ShowFail("You cannot sufficiently grasp the mind there to sunder it.", true, true, true);
						}
						else
						{
							Popup.ShowFail("There's no target with a mind there.", true, true, true);
						}
					}
					return false;
				}
				this.EndSunder();
				if (gameObject.HasEffect<MemberOfPsychicBattle>() || (gameObject.HasPart<SunderMind>() && gameObject.GetPart<SunderMind>().activeRounds > 0))
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("That target is already locked in psychic battle!", true, true, true);
					}
					return false;
				}
				if (this.RealityDistortionBased)
				{
					if (cell != this.ParentObject.CurrentCell)
					{
						Event @event = Event.New("InitiateRealityDistortionTransit", "Object", this.ParentObject, "Mutation", this, "Cell", cell);
						if (!this.ParentObject.FireEvent(@event, E) || !cell.FireEvent(@event, E))
						{
							return false;
						}
					}
					else if (!this.ParentObject.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", this.ParentObject, "Mutation", this), E))
					{
						return false;
					}
				}
				base.UseEnergy(1000, "Mental Mutation SunderMind");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
				this.BeginSunder(gameObject);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C670 RID: 50800 RVA: 0x00484E22 File Offset: 0x00483022
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C671 RID: 50801 RVA: 0x00484E2C File Offset: 0x0048302C
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Sunder Mind", "CommandSunderMind", "Mental Mutations", null, "\u000e", null, false, false, false, false, this.RealityDistortionBased, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C672 RID: 50802 RVA: 0x00484E7A File Offset: 0x0048307A
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C673 RID: 50803 RVA: 0x00484E94 File Offset: 0x00483094
		private bool IsOurSensePsychic(Effect FX)
		{
			SensePsychicEffect sensePsychicEffect = FX as SensePsychicEffect;
			return sensePsychicEffect != null && sensePsychicEffect.Listener == this.ParentObject;
		}

		// Token: 0x0600C674 RID: 50804 RVA: 0x00484EBB File Offset: 0x004830BB
		private bool HasOurSensePsychic(GameObject GO)
		{
			return GO.HasEffect(typeof(SensePsychicEffect), new Predicate<Effect>(this.IsOurSensePsychic));
		}

		// Token: 0x0600C675 RID: 50805 RVA: 0x00484ED9 File Offset: 0x004830D9
		private SensePsychicEffect GetOurSensePsychic(GameObject GO)
		{
			return GO.GetEffect(typeof(SensePsychicEffect), new Predicate<Effect>(this.IsOurSensePsychic)) as SensePsychicEffect;
		}

		// Token: 0x04004E39 RID: 20025
		public bool RealityDistortionBased;

		// Token: 0x04004E3A RID: 20026
		public string SuccessSound = "Sounds/Abilities/sfx_ability_sunderMind_dig_success";

		// Token: 0x04004E3B RID: 20027
		public string FailureSound = "Sounds/Abilities/sfx_ability_sunderMind_dig_fail";

		// Token: 0x04004E3C RID: 20028
		public GameObjectReference activeTarget;

		// Token: 0x04004E3D RID: 20029
		public int activeRounds;

		// Token: 0x04004E3E RID: 20030
		public int totalDamage;

		// Token: 0x04004E3F RID: 20031
		public int accruedDamage;
	}
}
