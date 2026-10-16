using System;
using Qud.API;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013E7 RID: 5095
	[Serializable]
	public class Domination : BaseMutation
	{
		// Token: 0x0600C4FF RID: 50431 RVA: 0x0047B750 File Offset: 0x00479950
		public Domination()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C500 RID: 50432 RVA: 0x0047B763 File Offset: 0x00479963
		public override void FinalizeCopy(GameObject Source, bool CopyEffects, bool CopyID, Func<GameObject, GameObject> MapInv)
		{
			base.FinalizeCopy(Source, CopyEffects, CopyID, MapInv);
			this.Target = null;
		}

		// Token: 0x0600C501 RID: 50433 RVA: 0x0047B777 File Offset: 0x00479977
		public override string GetDescription()
		{
			return "You garrote an adjacent creature's mind and control its actions while your own body lies dormant.";
		}

		// Token: 0x0600C502 RID: 50434 RVA: 0x0047B780 File Offset: 0x00479980
		public override string GetLevelText(int Level)
		{
			return "Mental attack versus creature with a mind\n" + "Success roll: {{rules|mutation rank}} or Ego mod (whichever is higher) + character level + 1d8 VS. Defender MA + character level\n" + "Range: 1\n" + "Duration: {{rules|" + this.GetDuration(Level).ToString() + "}} rounds\n" + "Cooldown: 75 rounds";
		}

		// Token: 0x0600C503 RID: 50435 RVA: 0x0047B7D0 File Offset: 0x004799D0
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			int num = Math.Max(this.ParentObject.StatMod("Ego", 0), Level) + this.ParentObject.GetStat("Level").Value;
			if (num == 0)
			{
				stats.Set("Attack", "1d8", !stats.mode.Contains("ability"), 0);
			}
			else if (num > 0)
			{
				stats.Set("Attack", "1d8+" + num.ToString(), !stats.mode.Contains("ability"), 0);
			}
			else
			{
				stats.Set("Attack", "1d8" + num.ToString(), !stats.mode.Contains("ability"), 0);
			}
			stats.Set("Duration", this.GetDuration(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C504 RID: 50436 RVA: 0x0047B8DB File Offset: 0x00479ADB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == PooledEvent<BeforeAITakingActionEvent>.ID || (ID == SingletonEvent<BeginTakeActionEvent>.ID && this.RealityDistortionBased) || ID == PooledEvent<CommandEvent>.ID || ID == TookDamageEvent.ID;
		}

		// Token: 0x0600C505 RID: 50437 RVA: 0x0047B919 File Offset: 0x00479B19
		public override bool HandleEvent(AIBoredEvent E)
		{
			return !GameObject.Validate(ref this.Target) && base.HandleEvent(E);
		}

		// Token: 0x0600C506 RID: 50438 RVA: 0x0047B931 File Offset: 0x00479B31
		public override bool HandleEvent(BeforeAITakingActionEvent E)
		{
			return !GameObject.Validate(ref this.Target) && base.HandleEvent(E);
		}

		// Token: 0x0600C507 RID: 50439 RVA: 0x0047B94C File Offset: 0x00479B4C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.RealityDistortionBased && GameObject.Validate(ref this.Target) && (!base.CheckMyRealityDistortionUsability(null, null, null, null, null, null, 0) || !IComponent<GameObject>.CheckRealityDistortionAccessibility(this.Target, null, this.ParentObject, null, this, 0)))
			{
				this.BreakDomination();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C508 RID: 50440 RVA: 0x0047B9AA File Offset: 0x00479BAA
		public override bool HandleEvent(TookDamageEvent E)
		{
			this.InterruptDomination();
			return base.HandleEvent(E);
		}

		// Token: 0x0600C509 RID: 50441 RVA: 0x0047B9B9 File Offset: 0x00479BB9
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandDominateCreature")
			{
				this.AttemptDomination(E.Target, E.TargetCell, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C50A RID: 50442 RVA: 0x0047B9E8 File Offset: 0x00479BE8
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C50B RID: 50443 RVA: 0x0047B9EB File Offset: 0x00479BEB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ChainInterruptDomination");
			Registrar.Register("DominationBroken");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C50C RID: 50444 RVA: 0x0047BA0C File Offset: 0x00479C0C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DominationBroken")
			{
				this.BreakDomination();
			}
			else if (E.ID == "ChainInterruptDomination" && GameObject.Validate(ref this.Target) && !this.Target.FireEvent("InterruptDomination"))
			{
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C50D RID: 50445 RVA: 0x0047BA6E File Offset: 0x00479C6E
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C50E RID: 50446 RVA: 0x0047BA78 File Offset: 0x00479C78
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Dominate Creature", "CommandDominateCreature", "Mental Mutations", this.GetDescription(), "\u0003", null, false, false, false, false, this.RealityDistortionBased, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C50F RID: 50447 RVA: 0x0047BACB File Offset: 0x00479CCB
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C510 RID: 50448 RVA: 0x0047BAE2 File Offset: 0x00479CE2
		private bool IsOurDominationEffect(Dominated FX)
		{
			return FX.Dominator == this.ParentObject;
		}

		// Token: 0x0600C511 RID: 50449 RVA: 0x0047BAF2 File Offset: 0x00479CF2
		public Dominated GetDominationEffect(GameObject Target = null)
		{
			GameObject gameObject = Target ?? this.Target;
			if (gameObject == null)
			{
				return null;
			}
			return gameObject.GetEffect<Dominated>(new Predicate<Dominated>(this.IsOurDominationEffect));
		}

		// Token: 0x0600C512 RID: 50450 RVA: 0x0047BB16 File Offset: 0x00479D16
		public int GetDuration(int Level)
		{
			return 100 * (Level + 1);
		}

		// Token: 0x0600C513 RID: 50451 RVA: 0x0047BB1E File Offset: 0x00479D1E
		public int GetCooldown(int Level)
		{
			return 75;
		}

		// Token: 0x0600C514 RID: 50452 RVA: 0x0047BB24 File Offset: 0x00479D24
		public bool BreakDomination()
		{
			if (!GameObject.Validate(ref this.Target))
			{
				return false;
			}
			Dominated dominationEffect = this.GetDominationEffect(null);
			if (dominationEffect != null && !dominationEffect.BeingRemovedBySource)
			{
				dominationEffect.BeingRemovedBySource = true;
				this.Target.RemoveEffect(dominationEffect, true);
			}
			if (this.Target.OnWorldMap())
			{
				this.Target.PullDown(false);
			}
			this.Target = null;
			The.Game.Player.Body = this.ParentObject;
			IComponent<GameObject>.ThePlayer.Target = null;
			if (this.ParentObject.IsPlayer())
			{
				Popup.Show("{{r|Your domination is broken!}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Domination.CheckMetempsychosis(this.ParentObject);
			}
			this.ParentObject.SmallTeleportSwirl(null, "&M", false, null, false);
			this.ParentObject.Brain.Goals.Clear();
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 75, null, null, false);
			Sidebar.UpdateState();
			return true;
		}

		// Token: 0x0600C515 RID: 50453 RVA: 0x0047BC18 File Offset: 0x00479E18
		public static void Metempsychosis(GameObject Subject, bool FromOriginalPlayerBody = false)
		{
			if (Subject.IsPlayer())
			{
				int intGameState = The.Game.GetIntGameState("MetempsychosisCount", 0);
				Popup.Show("{{r|Your mind is stranded here.}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				JournalAPI.AddAccomplishment("Your mind was stranded inside " + Subject.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, false, false, true, false, null, false) + ".", null, null, null, "general", MuralCategory.Generic, MuralWeight.Nil, null, -1L, true);
				The.Game.SetIntGameState("MetempsychosisCount", intGameState + 1);
				The.Game.PlayerName = Subject.Render.DisplayName;
				Subject.SetIntProperty("Renamed", 1, false);
				Subject.Brain.Factions = "";
				Subject.Brain.Allegiance.Clear();
				Subject.Brain.Allegiance["Player"] = 100;
				Subject.Brain.FactionFeelings.Clear();
				Subject.RemovePart<GivesRep>();
				return;
			}
			Subject.SetIntProperty("MetempsychosisFlag", 1, false);
			if (FromOriginalPlayerBody)
			{
				Subject.SetIntProperty("MetempsychosisOriginalPlayerBodyFlag", 1, false);
			}
		}

		// Token: 0x0600C516 RID: 50454 RVA: 0x0047BD34 File Offset: 0x00479F34
		public static void CheckMetempsychosis(GameObject Subject)
		{
			if (Subject.GetIntProperty("MetempsychosisFlag", 0) > 0)
			{
				Subject.RemoveIntProperty("MetempsychosisFlag");
				bool flag = false;
				if (Subject.GetIntProperty("MetempsychosisOriginalPlayerBodyFlag", 0) > 0)
				{
					Subject.RemoveIntProperty("MetempsychosisOriginalPlayerBodyFlag");
					flag = true;
				}
				Domination.Metempsychosis(Subject, flag);
			}
		}

		// Token: 0x0600C517 RID: 50455 RVA: 0x0047BD80 File Offset: 0x00479F80
		public void InterruptDomination()
		{
			if (GameObject.Validate(ref this.Target))
			{
				this.Target.FireEvent("InterruptDomination");
			}
		}

		// Token: 0x0600C518 RID: 50456 RVA: 0x0047BDA0 File Offset: 0x00479FA0
		public bool AttemptDomination(GameObject Target = null, Cell TargetCell = null, IEvent FromEvent = null)
		{
			if (this.RealityDistortionBased && !this.ParentObject.IsRealityDistortionUsable(null, null))
			{
				RealityStabilized.ShowGenericInterdictMessage(this.ParentObject, null);
				return false;
			}
			if (TargetCell == null)
			{
				TargetCell = ((Target != null) ? Target.CurrentCell : null) ?? base.PickDirection(true, "Dominate", null);
			}
			if (TargetCell == null)
			{
				return false;
			}
			if (this.RealityDistortionBased)
			{
				Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
				@event.SetParameter("Object", this.ParentObject);
				@event.SetParameter("Mutation", this);
				@event.SetParameter("Cell", TargetCell);
				if (!this.ParentObject.FireEvent(@event, FromEvent) || !TargetCell.FireEvent(@event, FromEvent))
				{
					return false;
				}
			}
			GameObject parentObject = this.ParentObject;
			if (parentObject != null)
			{
				parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_mental_generic_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			string text = null;
			if (Target != null)
			{
				if (this.ProcessTarget(Target, ref text))
				{
					return true;
				}
			}
			else
			{
				foreach (GameObject gameObject in TargetCell.GetObjectsWithPart("Combat"))
				{
					if (this.ProcessTarget(gameObject, ref text))
					{
						return true;
					}
				}
			}
			if (!text.IsNullOrEmpty())
			{
				this.ParentObject.Fail(text);
			}
			return false;
		}

		// Token: 0x0600C519 RID: 50457 RVA: 0x0047BF0C File Offset: 0x0047A10C
		public bool Dominate(MentalAttackEvent E)
		{
			GameObject defender = E.Defender;
			if (E.Penetrations > 0)
			{
				if (E.Defender.ApplyEffect(new Dominated(this.ParentObject, E.Magnitude, false), null))
				{
					defender.SmallTeleportSwirl(null, "&M", false, null, false);
					base.DidXToY("take", "control of", defender, null, "!", null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					this.Target = defender;
					The.Game.Player.Body = defender;
					IComponent<GameObject>.ThePlayer.Target = null;
					return true;
				}
				this.ParentObject.Fail("Something prevents you from dominating " + defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
			}
			else
			{
				IComponent<GameObject>.XDidYToZ(defender, "resist", this.ParentObject, "domination", "!", null, null, null, this.ParentObject, false, false, false, false, true, null, null, null, false, false, false, true, false, null);
				defender.AddOpinion<OpinionDominate>(this.ParentObject, 1f);
			}
			return false;
		}

		// Token: 0x0600C51A RID: 50458 RVA: 0x0047C030 File Offset: 0x0047A230
		private bool ProcessTarget(GameObject Target, ref string FailureMessage)
		{
			if (!Target.HasStat("Level"))
			{
				return false;
			}
			if (Target.Brain == null)
			{
				FailureMessage = "There seems to be no mind in " + Target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " to dominate.";
				return false;
			}
			if (Target.HasCopyRelationship(this.ParentObject))
			{
				FailureMessage = "You can't dominate " + this.ParentObject.itself + "!";
				return false;
			}
			if (this.GetDominationEffect(Target) != null)
			{
				FailureMessage = "You can't dominate someone you are already dominating.";
				return false;
			}
			if (Target.HasEffect<Dominated>())
			{
				FailureMessage = "You can't do that.";
				return false;
			}
			if (!Target.FireEvent("CanApplyDomination") || !CanApplyEffectEvent.Check(Target, "Domination", 0))
			{
				FailureMessage = Target.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " not have a consciousness you can make psychic contact with.";
				return false;
			}
			GameObject parentObject = this.ParentObject;
			if (!Target.CheckInfluence(ref FailureMessage, base.Name, parentObject, false))
			{
				if (FailureMessage.IsNullOrEmpty())
				{
					FailureMessage = "Nothing happens.";
				}
				return false;
			}
			int num = this.ParentObject.Stat("Level", 0) + Math.Max(this.ParentObject.StatMod("Ego", 0), base.Level);
			int num2 = Target.Stat("Level", 0);
			if (Options.SifrahPsychicCombat)
			{
				num = this.ParentObject.Stat("Level", 0) + this.ParentObject.StatMod("Ego", 0) + base.Level;
				num2 = Target.Stat("Level", 0) + Stats.GetCombatMA(Target);
				PsychicCombatSifrah psychicCombatSifrah = new PsychicCombatSifrah(Target, "Domination", num / 5, num2 / 5, "dominating " + Target.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
				psychicCombatSifrah.Play(Target);
				num = num * (psychicCombatSifrah.Performance + 50) / 100;
			}
			base.PerformMentalAttack(new Mental.Attack(this.Dominate), this.ParentObject, Target, null, "Domination", "1d8", 1, this.GetDuration(base.Level), int.MinValue, num, num2);
			base.UseEnergy(1000, "Mental Mutation Domination");
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
			return true;
		}

		// Token: 0x04004DEB RID: 19947
		public const string COMMAND_NAME = "CommandDominateCreature";

		// Token: 0x04004DEC RID: 19948
		public const string METEMPSYCHOSIS_COUNT_KEY = "MetempsychosisCount";

		// Token: 0x04004DED RID: 19949
		public const string METEMPSYCHOSIS_FLAG = "MetempsychosisFlag";

		// Token: 0x04004DEE RID: 19950
		public const string METEMPSYCHOSIS_ORIGINAL_PLAYER_BODY_FLAG = "MetempsychosisOriginalPlayerBodyFlag";

		// Token: 0x04004DEF RID: 19951
		public bool RealityDistortionBased;

		// Token: 0x04004DF0 RID: 19952
		public GameObject Target;
	}
}
