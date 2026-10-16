using System;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000C34 RID: 3124
	[Serializable]
	public class Asleep : Effect, ITierInitialized
	{
		// Token: 0x06007F36 RID: 32566 RVA: 0x002EF185 File Offset: 0x002ED385
		public Asleep()
		{
			this.DisplayName = "{{c|asleep}}";
		}

		// Token: 0x06007F37 RID: 32567 RVA: 0x002EF19F File Offset: 0x002ED39F
		public Asleep(int Duration, bool forced = false, bool quicksleep = false, bool Voluntary = false, bool Prone = true)
			: this()
		{
			this.Duration = Duration;
			this.forced = forced;
			this.quicksleep = quicksleep;
			this.Voluntary = Voluntary;
			this.ApplyProne = Prone;
			this.TurnWentToSleep = The.Game.Turns;
		}

		// Token: 0x06007F38 RID: 32568 RVA: 0x002EF1DC File Offset: 0x002ED3DC
		public Asleep(GameObject AsleepOn, int Duration, bool forced = false, bool quicksleep = false, bool Voluntary = false, bool Prone = true)
			: this(Duration, forced, quicksleep, Voluntary, Prone)
		{
			this.AsleepOn = AsleepOn;
			this.TurnWentToSleep = The.Game.Turns;
		}

		// Token: 0x06007F39 RID: 32569 RVA: 0x002EF203 File Offset: 0x002ED403
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(40, 200);
		}

		// Token: 0x06007F3A RID: 32570 RVA: 0x002EF217 File Offset: 0x002ED417
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06007F3B RID: 32571 RVA: 0x002EF21C File Offset: 0x002ED41C
		public override int GetEffectType()
		{
			int num = 117440514;
			if (this.Voluntary)
			{
				num |= 134217728;
			}
			return num;
		}

		// Token: 0x06007F3C RID: 32572 RVA: 0x002EF240 File Offset: 0x002ED440
		public static bool UsesSleepMode(GameObject obj)
		{
			return obj != null && obj.HasTag("Robot");
		}

		// Token: 0x06007F3D RID: 32573 RVA: 0x002EF252 File Offset: 0x002ED452
		public bool UsesSleepMode()
		{
			return Asleep.UsesSleepMode(base.Object);
		}

		// Token: 0x06007F3E RID: 32574 RVA: 0x002EF25F File Offset: 0x002ED45F
		public override string GetDescription()
		{
			if (this.UsesSleepMode())
			{
				return "{{c|sleep mode}}";
			}
			return base.GetDescription();
		}

		// Token: 0x06007F3F RID: 32575 RVA: 0x002EF275 File Offset: 0x002ED475
		public override string GetDetails()
		{
			if (this.UsesSleepMode())
			{
				return "In power conservation mode.\n-12 DV.\nAttackers receive +4 to penetration rolls.\nWill return to normal operations dazed if damage is taken.";
			}
			return "Unconscious.\n-12 DV.\nAttackers receive +4 to penetration rolls.\nWill wake up dazed if damage is taken.";
		}

		// Token: 0x06007F40 RID: 32576 RVA: 0x002EF28C File Offset: 0x002ED48C
		public override bool Apply(GameObject Object)
		{
			if (!Object.HasPart<Brain>())
			{
				return false;
			}
			if (Object.HasEffect<Asleep>())
			{
				return false;
			}
			if (((this.Voluntary || Object.FireEvent("CanApplyInvoluntarySleep")) && (Object.FireEvent("CanApplySleep") && (this.Voluntary || Object.FireEvent("ApplyInvoluntarySleep"))) && Object.FireEvent("ApplySleep") && ApplyEffectEvent.Check(Object, "Sleep", this, null)) || this.forced)
			{
				if (!Asleep.UsesSleepMode(Object) && this.ApplyProne)
				{
					GameObject asleepOn = this.AsleepOn;
					Object.ApplyEffect(new Prone(this.Voluntary, false, false, asleepOn), null);
				}
				Object.MovementModeChanged("Asleep", !this.Voluntary);
				if (this.Voluntary)
				{
					base.PlayWorldSound("sfx_characterTrigger_sleep_idle", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				if (Object.IsPlayer())
				{
					if (Asleep.UsesSleepMode(Object))
					{
						IComponent<GameObject>.AddPlayerMessage("You enter {{C|sleep mode}}.", null, true);
					}
					else
					{
						IComponent<GameObject>.AddPlayerMessage("You fall {{C|asleep}}!", null, true);
					}
				}
				else if (base.Visible())
				{
					if (Asleep.UsesSleepMode(Object))
					{
						IComponent<GameObject>.AddPlayerMessage(Object.Does("go", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " into {{C|sleep mode}}.", null, true);
					}
					else
					{
						IComponent<GameObject>.AddPlayerMessage(Object.Does("fall", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " {{C|asleep}}.", null, true);
					}
				}
				if (Object.Brain != null)
				{
					Object.Brain.Goals.Clear();
				}
				Object.ForfeitTurn(false);
				if (Object.IsPlayer())
				{
					AutoAct.Interrupt(null, null, null, false);
				}
				this.ApplyStats();
				return true;
			}
			return false;
		}

		// Token: 0x06007F41 RID: 32577 RVA: 0x002EF47C File Offset: 0x002ED67C
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			if (!this.WakeMessageDone)
			{
				if (Asleep.UsesSleepMode(Object))
				{
					base.DidX("exit", "sleep mode", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				}
				else
				{
					base.DidX("wake", "up", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				}
			}
			if (!this.Voluntary)
			{
				Object.ApplyEffect(new Wakeful(Stat.Random(3, 5)), null);
			}
			base.Remove(Object);
		}

		// Token: 0x06007F42 RID: 32578 RVA: 0x002EF502 File Offset: 0x002ED702
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift("DV", -12, false);
		}

		// Token: 0x06007F43 RID: 32579 RVA: 0x002EF518 File Offset: 0x002ED718
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06007F44 RID: 32580 RVA: 0x002EF52C File Offset: 0x002ED72C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID || ID == GetDefenderHitDiceEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == PooledEvent<IsConversationallyResponsiveEvent>.ID;
		}

		// Token: 0x06007F45 RID: 32581 RVA: 0x002EF595 File Offset: 0x002ED795
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			return (E.Involuntary || E.Object != base.Object) && base.HandleEvent(E);
		}

		// Token: 0x06007F46 RID: 32582 RVA: 0x002EF5B6 File Offset: 0x002ED7B6
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("asleep", 100);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x002EF5DA File Offset: 0x002ED7DA
		public override bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			if (E.Speaker == base.Object)
			{
				E.Message = Asleep.GetSleepMessage(base.Object, E.Physical, E.Mental);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F48 RID: 32584 RVA: 0x002EF610 File Offset: 0x002ED810
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				if (this.Duration == 9999)
				{
					if (base.Object.IsPlayer() && (1.in1000() || this.IndefinitePlayerSleepTurns == 2147483647))
					{
						this.Duration = 0;
					}
					else
					{
						this.IndefinitePlayerSleepTurns++;
						if (this.IndefinitePlayerSleepTurns % 10 == 0)
						{
							XRLCore.TenPlayerTurnsPassed();
							The.Core.RenderBase(false, false);
						}
						if (base.Object.IsPlayer() && The.Game.Player.Messages.LastLine != "You are asleep.")
						{
							IComponent<GameObject>.AddPlayerMessage("You are asleep.", null, true);
						}
					}
				}
				else
				{
					this.Duration--;
				}
				if (this.Duration > 0)
				{
					if (this.Duration % 10 == 0)
					{
						XRLCore.TenPlayerTurnsPassed();
						XRLCore.Core.RenderBase(true, false);
					}
					if (base.Object.IsPlayer() && The.Game.Player.Messages.LastLine != "You are asleep.")
					{
						IComponent<GameObject>.AddPlayerMessage("You are asleep.", null, true);
					}
					E.PreventAction = true;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F49 RID: 32585 RVA: 0x002EF743 File Offset: 0x002ED943
		public override bool HandleEvent(GetDefenderHitDiceEvent E)
		{
			E.PenetrationBonus += 4;
			return base.HandleEvent(E);
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x002EF75C File Offset: 0x002ED95C
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Duration > 0)
			{
				this.CheckAsleepOn();
				this.TurnsAsleep++;
				Bed bed;
				if (this.AsleepOn != null && this.AsleepOn.TryGetPart<Bed>(out bed))
				{
					bed.ProcessTurnAsleep(base.Object, this.TurnsAsleep);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x002EF7B8 File Offset: 0x002ED9B8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.CanWake(E.Actor))
			{
				E.AddAction("Wake", "wake", "WakeSleeper", null, 'w', false, 100, 0, false, false, true, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x002EF800 File Offset: 0x002EDA00
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "WakeSleeper")
			{
				GameObject actor = E.Actor;
				if (this.CanWake(actor) && actor.CheckFrozen(false, true, false, null))
				{
					base.PlayWorldSound("Sounds/Interact/sfx_interact_creaturesleeping_wake", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					this.Duration = 0;
					if (actor.IsPlayer())
					{
						if (Asleep.UsesSleepMode(base.Object))
						{
							Popup.Show("You press " + base.Object.poss("activation panel", true) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						else
						{
							Popup.Show("You gently shake " + base.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " awake.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							this.WakeMessageDone = true;
						}
					}
					else if (base.Object.IsPlayer())
					{
						if (Asleep.UsesSleepMode(base.Object))
						{
							IComponent<GameObject>.AddPlayerMessage(actor.Does("press", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " your activation panel.", null, true);
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage(actor.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " gently" + actor.GetVerb("shake", true, false, false, true, false) + " you awake.", null, true);
							this.WakeMessageDone = true;
						}
					}
					else if (base.Visible())
					{
						if (Asleep.UsesSleepMode(base.Object))
						{
							IComponent<GameObject>.AddPlayerMessage(actor.Does("press", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + base.Object.poss("activation panel", true) + ".", null, true);
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								actor.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" gently",
								actor.GetVerb("shake", true, false, false, true, false),
								" ",
								base.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" awake."
							}), null, true);
							this.WakeMessageDone = true;
						}
					}
					base.Object.RemoveEffect(this, true);
					actor.UseEnergy(1000, "Physical Action Wake", null, null, false);
					E.RequestInterfaceExit();
				}
				else if (actor.IsPlayer())
				{
					Popup.Show("You can't figure out how to wake " + base.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F4D RID: 32589 RVA: 0x002EFB61 File Offset: 0x002EDD61
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x06007F4E RID: 32590 RVA: 0x002EFB79 File Offset: 0x002EDD79
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				base.Object.Twiddle(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F4F RID: 32591 RVA: 0x002EFB9C File Offset: 0x002EDD9C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("CanChangeBodyPosition");
			Registrar.Register("CanHaveSmartUseConversation");
			Registrar.Register("CanMoveExtremities");
			Registrar.Register("IsMobile");
			Registrar.Register("TakeDamage");
			Registrar.Register("WakeUp");
			base.Register(Object, Registrar);
		}

		// Token: 0x06007F50 RID: 32592 RVA: 0x002EFC0C File Offset: 0x002EDE0C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "IsMobile")
			{
				if (this.Duration > 0)
				{
					return false;
				}
			}
			else if (E.ID == "TakeDamage" || E.ID == "WakeUp")
			{
				if (this.TurnWentToSleep == The.Game.Turns)
				{
					return true;
				}
				this.Duration = 0;
				if (Asleep.UsesSleepMode(base.Object))
				{
					base.DidX("exit", "sleep mode by emergency interrupt", null, null, null, null, base.Object, false, false, null, null, false, false, false, false, false, null);
				}
				else
				{
					base.DidX("wake", "up in a daze", null, null, null, null, base.Object, false, false, null, null, false, false, false, false, false, null);
				}
				this.WakeMessageDone = true;
				base.Object.ApplyEffect(new Dazed(Stat.Random(3, 4)), null);
				base.Object.RemoveEffect(this, true);
			}
			else if (E.ID == "CanChangeBodyPosition" || E.ID == "CanMoveExtremities")
			{
				if (!E.HasFlag("Involuntary"))
				{
					return false;
				}
			}
			else
			{
				if (E.ID == "CanHaveSmartUseConversation")
				{
					return false;
				}
				if (E.ID == "BeforeDeepCopyWithoutEffects")
				{
					this.UnapplyStats();
				}
				else if (E.ID == "AfterDeepCopyWithoutEffects")
				{
					this.ApplyStats();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06007F51 RID: 32593 RVA: 0x002EFD84 File Offset: 0x002EDF84
		public override bool Render(RenderEvent E)
		{
			Render render = base.Object.Render;
			int num = XRLCore.CurrentFrame % 60;
			if (num > 10 && num < 25)
			{
				E.RenderString = "z";
				E.ColorString = "&C^c";
				return false;
			}
			return true;
		}

		// Token: 0x06007F52 RID: 32594 RVA: 0x002EFDCC File Offset: 0x002EDFCC
		public static string GetSleepMessage(GameObject Object, bool Physical = true, bool Mental = false)
		{
			if (Mental && !Physical)
			{
				string text = (20.in100() ? GameText.GenerateMarkovMessageParagraph() : GameText.GenerateMarkovMessageSentence());
				text = GameText.VariableReplace(text.Trim(), Object, null, false);
				if (Object.GetPrimaryFaction(false) == "Birds")
				{
					if (Object.GetTag("Species", null) == "flamingo")
					{
						text = TextFilters.WaterBird(text);
					}
					else
					{
						text = TextFilters.Corvid(text);
					}
				}
				Brain brain = Object.Brain;
				if (brain != null && brain.Aquatic)
				{
					text = TextFilters.Fish(text);
				}
				return "{{c|" + text + "}}";
			}
			if (Asleep.UsesSleepMode(Object))
			{
				return Object.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + Object.Is + " utterly unresponsive.";
			}
			return Object.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + Object.GetVerb("snore", true, false, false, true, false) + " loudly.";
		}

		// Token: 0x06007F53 RID: 32595 RVA: 0x002EFEF0 File Offset: 0x002EE0F0
		public bool IsAsleepOnValid()
		{
			return GameObject.Validate(ref this.AsleepOn) && this.AsleepOn.CurrentCell != null && base.Object != null && base.Object.CurrentCell != null && this.AsleepOn.CurrentCell == base.Object.CurrentCell;
		}

		// Token: 0x06007F54 RID: 32596 RVA: 0x002EFF4F File Offset: 0x002EE14F
		public bool CheckAsleepOn()
		{
			if (this.IsAsleepOnValid())
			{
				return true;
			}
			this.AsleepOn = null;
			return false;
		}

		// Token: 0x06007F55 RID: 32597 RVA: 0x002EFF63 File Offset: 0x002EE163
		public bool CanWake(GameObject who)
		{
			return !Asleep.UsesSleepMode(base.Object) || (who.HasSkill("Tinkering_Repair") || who.HasSkill("Tinkering_Tinker1"));
		}

		// Token: 0x040038C1 RID: 14529
		public bool quicksleep;

		// Token: 0x040038C2 RID: 14530
		public bool forced;

		// Token: 0x040038C3 RID: 14531
		public GameObject AsleepOn;

		// Token: 0x040038C4 RID: 14532
		public int TurnsAsleep;

		// Token: 0x040038C5 RID: 14533
		public bool Voluntary;

		// Token: 0x040038C6 RID: 14534
		public int IndefinitePlayerSleepTurns;

		// Token: 0x040038C7 RID: 14535
		[NonSerialized]
		private long TurnWentToSleep;

		// Token: 0x040038C8 RID: 14536
		[NonSerialized]
		private bool WakeMessageDone;

		// Token: 0x040038C9 RID: 14537
		[NonSerialized]
		private bool ApplyProne = true;
	}
}
