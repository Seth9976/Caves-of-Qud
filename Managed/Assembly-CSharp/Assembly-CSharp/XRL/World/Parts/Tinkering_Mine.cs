using System;
using System.Collections.Generic;
using System.Text;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02001236 RID: 4662
	[Serializable]
	public class Tinkering_Mine : IPart, IDisarmingSifrahHandler
	{
		// Token: 0x0600B931 RID: 47409 RVA: 0x00439714 File Offset: 0x00437914
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			Tinkering_Mine tinkering_Mine = new Tinkering_Mine();
			tinkering_Mine.Timer = this.Timer;
			tinkering_Mine.PlayerMine = this.PlayerMine;
			if (GameObject.Validate(ref this.Explosive))
			{
				tinkering_Mine.Explosive = ((MapInv != null) ? MapInv(this.Explosive) : null) ?? this.Explosive.DeepCopy(false, false, MapInv);
				if (tinkering_Mine.Explosive != null)
				{
					tinkering_Mine.Explosive.ForeachPartDescendedFrom<Tinkering_Layable>(delegate(Tinkering_Layable p)
					{
						p.ComponentOf = Parent;
					});
				}
			}
			tinkering_Mine.Owner = this.Owner;
			tinkering_Mine.OwnerAllegiance = this.OwnerAllegiance;
			tinkering_Mine.Message = this.Message;
			tinkering_Mine.Armed = this.Armed;
			tinkering_Mine.ArmedDetailColor = this.ArmedDetailColor;
			tinkering_Mine.DisarmedDetailColor = this.DisarmedDetailColor;
			tinkering_Mine.ParentObject = Parent;
			return tinkering_Mine;
		}

		// Token: 0x0600B932 RID: 47410 RVA: 0x004397F7 File Offset: 0x004379F7
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (GameObject.Validate(ref this.Explosive))
			{
				this.Explosive.TurnTick(TimeTick, Amount);
			}
		}

		// Token: 0x0600B933 RID: 47411 RVA: 0x00439814 File Offset: 0x00437A14
		public void SetExplosive(GameObject Object)
		{
			if (Object == this.Explosive)
			{
				return;
			}
			if (this.Explosive != null)
			{
				this.Explosive.ForeachPartDescendedFrom<Tinkering_Layable>(delegate(Tinkering_Layable p)
				{
					p.ComponentOf = null;
				});
			}
			this.Explosive = Object;
			if (Object != null)
			{
				Object.ForeachPartDescendedFrom<Tinkering_Layable>(delegate(Tinkering_Layable p)
				{
					p.ComponentOf = this.ParentObject;
				});
			}
			if (Sidebar.CurrentTarget == Object)
			{
				Sidebar.CurrentTarget = null;
			}
			base.FlushTransientCaches();
		}

		// Token: 0x0600B934 RID: 47412 RVA: 0x0043988D File Offset: 0x00437A8D
		private bool ExplosiveWantsEvent(int ID, int cascade)
		{
			return GameObject.Validate(ref this.Explosive) && MinEvent.CascadeTo(cascade, 8) && this.Explosive.WantEvent(ID, cascade);
		}

		// Token: 0x0600B935 RID: 47413 RVA: 0x004398B8 File Offset: 0x00437AB8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == EffectAppliedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetExtrinsicValueEvent.ID || ID == GetExtrinsicWeightEvent.ID || ID == GetInventoryActionsEvent.ID || (ID == GetAdjacentNavigationWeightEvent.ID && this.Timer > 0) || ID == GetNavigationWeightEvent.ID || ID == GetShortDescriptionEvent.ID || (ID == PooledEvent<InterruptAutowalkEvent>.ID && this.Armed && this.Timer <= 0) || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID || ID == ObjectEnteredCellEvent.ID || ID == OnDestroyObjectEvent.ID || ID == TookDamageEvent.ID || this.ExplosiveWantsEvent(ID, cascade);
		}

		// Token: 0x0600B936 RID: 47414 RVA: 0x004399C4 File Offset: 0x00437BC4
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return false;
		}

		// Token: 0x0600B937 RID: 47415 RVA: 0x004399C7 File Offset: 0x00437BC7
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			this.ParentObject.Twiddle(false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B938 RID: 47416 RVA: 0x004399E0 File Offset: 0x00437BE0
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Timer", this.Timer);
			E.AddEntry(this, "PlayerMine", this.PlayerMine);
			E.AddEntry(this, "Explosive", this.Explosive);
			E.AddEntry(this, "Owner", this.Owner);
			if (this.OwnerAllegiance != null)
			{
				E.AddEntry(this, "OwnerAllegiance", this.OwnerAllegiance.GetDebugInternalsEntry());
			}
			E.AddEntry(this, "Message", this.Message);
			E.AddEntry(this, "Armed", this.Armed);
			E.AddEntry(this, "ArmedDetailColor", this.ArmedDetailColor);
			E.AddEntry(this, "DisarmedDetailColor", this.DisarmedDetailColor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B939 RID: 47417 RVA: 0x00439AA4 File Offset: 0x00437CA4
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (this.Armed && Options.SifrahDisarming && Options.SifrahDisarmingAuto == "Always" && E.Actor.IsPlayer() && this.ParentObject.Understood() && !this.ParentObject.OnWorldMap() && TinkeringHelpers.ConsiderScrap(this.ParentObject, E.Actor))
			{
				int sifrahDisarmingDifficulty = Tinkering_Mine.GetSifrahDisarmingDifficulty(this.Explosive);
				int sifrahDisarmingRating = Tinkering_Mine.GetSifrahDisarmingRating(E.Actor);
				if (DisarmingSifrah.IsMastered(this.ParentObject, sifrahDisarmingDifficulty, sifrahDisarmingRating))
				{
					E.Command = "DisarmMine";
					E.AllowRetry = false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B93A RID: 47418 RVA: 0x00439B54 File Offset: 0x00437D54
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (E.Smart && this.Armed && E.PhaseMatches(this.ParentObject) && GameObject.Validate(ref this.Explosive) && this.Avoidable(E.Actor))
			{
				GetComponentNavigationWeightEvent.Process(this.Explosive, E);
				if (E.Weight < 7 && this.WillTrigger(E.Actor))
				{
					E.MinWeight(7);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B93B RID: 47419 RVA: 0x00439BCC File Offset: 0x00437DCC
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (E.Smart && this.Armed && E.PhaseMatches(this.ParentObject))
			{
				if (this.Timer >= 0)
				{
					if (GameObject.Validate(ref this.Explosive) && this.Avoidable(E.Actor))
					{
						GetComponentAdjacentNavigationWeightEvent.Process(this.Explosive, E);
						if (E.Weight < 3 && this.WillTrigger(E.Actor))
						{
							E.MinWeight(3);
						}
					}
				}
				else if (GameObject.Validate(ref this.Explosive) && this.Avoidable(E.Actor))
				{
					GetComponentAdjacentNavigationWeightEvent.Process(this.Explosive, E);
					if (E.Weight < 2 && this.WillTrigger(E.Actor))
					{
						E.MinWeight(2);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B93C RID: 47420 RVA: 0x00439C9C File Offset: 0x00437E9C
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Owner = null;
			this.OwnerAllegiance = null;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B93D RID: 47421 RVA: 0x00439CB3 File Offset: 0x00437EB3
		public override bool HandleEvent(MinEvent E)
		{
			return base.HandleEvent(E) && (!E.CascadeTo(8) || !GameObject.Validate(ref this.Explosive) || E.Dispatch(this.Explosive));
		}

		// Token: 0x0600B93E RID: 47422 RVA: 0x00439CE7 File Offset: 0x00437EE7
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (this.Armed)
			{
				if (base.IsEMPed())
				{
					this.Disarm();
				}
				else if (base.IsBroken() || base.IsRusted())
				{
					this.Boom();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B93F RID: 47423 RVA: 0x00439D1F File Offset: 0x00437F1F
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (this.Armed && E.Object == this.ParentObject && E.Damage.Amount > 0)
			{
				this.Boom();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B940 RID: 47424 RVA: 0x00439D53 File Offset: 0x00437F53
		public override bool HandleEvent(GetExtrinsicValueEvent E)
		{
			if (GameObject.Validate(ref this.Explosive))
			{
				E.Value += this.Explosive.Value;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B941 RID: 47425 RVA: 0x00439D81 File Offset: 0x00437F81
		public override bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			if (GameObject.Validate(ref this.Explosive))
			{
				E.Weight += this.Explosive.GetWeight();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B942 RID: 47426 RVA: 0x00439DB0 File Offset: 0x00437FB0
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Armed && this.Timer > 0)
			{
				if (!this.ParentObject.HasEffect(typeof(Stasis)))
				{
					this.Timer--;
				}
				if (this.Timer <= 0)
				{
					this.Boom();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B943 RID: 47427 RVA: 0x00439E0C File Offset: 0x0043800C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!this.RouteEventToExplosiveModifications(E))
			{
				return false;
			}
			if (E.Understood())
			{
				if (!GameObject.Validate(ref this.Explosive))
				{
					E.AddAdjective("empty", -40);
				}
				else if (this.Armed)
				{
					if (this.Timer > 0)
					{
						E.AddTag("{{y|[{{R|" + this.Timer.ToString() + " sec}}]}}", 0);
					}
				}
				else
				{
					E.AddAdjective("disarmed", -40);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B944 RID: 47428 RVA: 0x00439E90 File Offset: 0x00438090
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.RouteEventToExplosiveModifications(E))
			{
				return false;
			}
			if (!this.Armed)
			{
				E.Base.Compound(this.ParentObject.Ithas).Append(" been disarmed.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B945 RID: 47429 RVA: 0x00439ED0 File Offset: 0x004380D0
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.Understood())
			{
				if (this.Armed)
				{
					E.AddAction("Detonate", "detonate", "Detonate", null, 'n', false, 0, 0, false, false, true, false, true, null, false);
				}
				if (this.ParentObject.CurrentCell != null && !this.ParentObject.OnWorldMap())
				{
					if (this.Armed)
					{
						E.AddAction("Disarm", "disarm", "DisarmMine", null, 'd', false, 100, 0, false, false, true, false, true, null, false);
					}
					else
					{
						E.AddAction("Arm", "arm", "ArmMine", null, 'a', false, 0, 0, false, false, true, false, true, null, false);
					}
				}
				else
				{
					GameObject inInventory = this.ParentObject.InInventory;
					if (inInventory != null && inInventory.IsPlayer() && !this.ParentObject.InInventory.OnWorldMap())
					{
						if (this.Timer > 0)
						{
							E.AddAction("SetBomb", "set", "LayMine", null, 's', false, 100, 0, false, false, true, false, true, null, false);
						}
						else
						{
							E.AddAction("LayMine", "lay", "LayMine", null, 'L', false, 100, 0, false, false, true, false, true, null, false);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B946 RID: 47430 RVA: 0x0043A010 File Offset: 0x00438210
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "LayMine")
			{
				if (!this.CheckInteraction(E.Actor))
				{
					return false;
				}
				if (!this.AttemptLay(E.Actor))
				{
					return false;
				}
				E.RequestInterfaceExit();
			}
			else if (E.Command == "ArmMine")
			{
				if (!this.CheckInteraction(E.Actor))
				{
					return false;
				}
				if (!this.AttemptArm(E.Actor))
				{
					return false;
				}
				E.RequestInterfaceExit();
			}
			else if (E.Command == "DisarmMine")
			{
				if (!this.CheckInteraction(E.Actor))
				{
					return false;
				}
				if (!this.AttemptDisarm(E.Actor, E, E.Auto))
				{
					return false;
				}
				E.RequestInterfaceExit();
			}
			else if (E.Command == "Detonate")
			{
				if (!this.CheckInteraction(E.Actor))
				{
					return false;
				}
				if (!this.Armed)
				{
					return false;
				}
				if (!this.Boom())
				{
					return false;
				}
				E.Actor.UseEnergy(1000, "Item Tinkering " + ((this.Timer > 0) ? "Bomb" : "Mine") + " Detonate", null, null, false);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B947 RID: 47431 RVA: 0x0043A15B File Offset: 0x0043835B
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.Disarm();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B948 RID: 47432 RVA: 0x0043A16A File Offset: 0x0043836A
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (this.Armed && this.Timer <= 0 && this.WillTrigger(E.Object))
			{
				this.Boom();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B949 RID: 47433 RVA: 0x0043A199 File Offset: 0x00438399
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			if (GameObject.Validate(ref this.Explosive))
			{
				this.Explosive.Obliterate(null, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B94A RID: 47434 RVA: 0x0043A1C0 File Offset: 0x004383C0
		public override bool HandleEvent(InterruptAutowalkEvent E)
		{
			if (this.Armed && this.Timer <= 0 && GameObject.Validate(ref this.Explosive) && this.Avoidable(E.Actor) && this.WillTrigger(E.Actor))
			{
				E.IndicateObject = this.ParentObject;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B94B RID: 47435 RVA: 0x0043A21C File Offset: 0x0043841C
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600B94C RID: 47436 RVA: 0x0043A232 File Offset: 0x00438432
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 75) && base.HandleEvent(E);
		}

		// Token: 0x0600B94D RID: 47437 RVA: 0x0043A248 File Offset: 0x00438448
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100())
			{
				E.IdentifyIfDestroyed = true;
				if (this.Boom())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600B94E RID: 47438 RVA: 0x0043A27C File Offset: 0x0043847C
		private bool RouteEventToExplosiveModifications(MinEvent E)
		{
			if (GameObject.Validate(ref this.Explosive))
			{
				List<IModification> partsDescendedFrom = this.Explosive.GetPartsDescendedFrom<IModification>();
				if (partsDescendedFrom.Count > 0)
				{
					int id = E.ID;
					int cascadeLevel = E.GetCascadeLevel();
					int i = 0;
					int count = partsDescendedFrom.Count;
					while (i < count)
					{
						IModification modification = partsDescendedFrom[i];
						if (modification.WantEvent(id, cascadeLevel))
						{
							if (!E.Dispatch(modification))
							{
								return false;
							}
							if (!modification.HandleEvent(E))
							{
								return false;
							}
						}
						i++;
					}
				}
			}
			return true;
		}

		// Token: 0x0600B94F RID: 47439 RVA: 0x0043A2FB File Offset: 0x004384FB
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B950 RID: 47440 RVA: 0x0043A2FE File Offset: 0x004384FE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanBeDisassembled");
			Registrar.Register("CanBeTaken");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B951 RID: 47441 RVA: 0x0043A31E File Offset: 0x0043851E
		public bool WillTrigger(GameObject Actor)
		{
			return GameObject.Validate(ref Actor) && GameObject.Validate(ref this.Explosive) && Actor.IsCombatObject() && Actor.PhaseAndFlightMatches(this.ParentObject) && this.ConsiderHostile(Actor);
		}

		// Token: 0x0600B952 RID: 47442 RVA: 0x0043A358 File Offset: 0x00438558
		public override bool FireEvent(Event E)
		{
			return ((!(E.ID == "CanBeTaken") && !(E.ID == "CanBeDisassembled")) || !this.Armed) && base.FireEvent(E);
		}

		// Token: 0x0600B953 RID: 47443 RVA: 0x0043A390 File Offset: 0x00438590
		public bool Boom()
		{
			if (!GameObject.Validate(ref this.Explosive))
			{
				return false;
			}
			GameObject.Validate(ref this.Owner);
			GameObject explosive = this.Explosive;
			this.SetExplosive(null);
			Cell C = this.ParentObject.GetCurrentCell();
			this.ParentObject.RemoveFromContext(null);
			if (C != null)
			{
				C.AddObject(explosive, true, false, true, false, false, true, true, null, null, null, null, null, null, null, null);
				if (explosive.Render != null && this.ParentObject.Render != null)
				{
					explosive.Render.DisplayName = this.ParentObject.Render.DisplayName;
				}
				explosive.SetStringProperty("DetonatedSound", "Sounds/Interact/sfx_interact_mine_detonate", false);
				Temporary.CarryOver(this.ParentObject, explosive, false);
				Hidden.CarryOver(this.ParentObject, explosive);
				Phase.carryOver(this.ParentObject, explosive);
				if (explosive.ForeachPartDescendedFrom<IGrenade>((IGrenade p) => !p.Detonate(C, this.Owner, null, true)))
				{
					Event @event = Event.New(this.Message, 0, 0, 0);
					if (GameObject.Validate(ref this.Owner))
					{
						@event.SetParameter("Owner", this.Owner);
					}
					explosive.FireEvent(@event);
				}
			}
			this.ParentObject.Destroy(null, true, false, null);
			return true;
		}

		// Token: 0x0600B954 RID: 47444 RVA: 0x0043A4DC File Offset: 0x004386DC
		public bool AttemptArm(GameObject Actor)
		{
			if (this.Armed)
			{
				return false;
			}
			if (Actor.IsPlayer() && !this.ParentObject.Understood())
			{
				return false;
			}
			if (Actor.OnWorldMap())
			{
				return Actor.Fail("You cannot do that on the world map.");
			}
			if (!Actor.CheckFrozen(false, true, false, null))
			{
				return false;
			}
			this.Arm(Actor);
			IComponent<GameObject>.XDidYToZ(Actor, "arm", this.ParentObject, null, null, null, null, Actor, null, false, false, false, true, false, null, null, null, false, false, false, false, false, null);
			Actor.UseEnergy(1000, "Skill Tinkering " + ((this.Timer > 0) ? "Bomb" : "Mine") + " Arm", null, null, false);
			base.PlayWorldSound("Sounds/Interact/sfx_interact_mine_arm", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			return true;
		}

		// Token: 0x0600B955 RID: 47445 RVA: 0x0043A5C0 File Offset: 0x004387C0
		public bool AttemptLay(GameObject Actor)
		{
			if (this.ParentObject.InInventory != Actor)
			{
				return false;
			}
			if (this.Armed)
			{
				return false;
			}
			if (Actor.IsPlayer() && !this.ParentObject.Understood())
			{
				return false;
			}
			if (Actor.OnWorldMap())
			{
				return Actor.Fail("You cannot do that on the world map.");
			}
			if (!Actor.CheckFrozen(false, true, false, null))
			{
				return false;
			}
			Cell cell;
			if (Actor.IsPlayer())
			{
				string text = XRL.UI.PickDirection.ShowPicker("Lay Mine");
				if (text == null)
				{
					return false;
				}
				cell = Actor.CurrentCell.GetCellFromDirection(text, true);
			}
			else
			{
				cell = Actor.CurrentCell.GetEmptyAdjacentCells().GetRandomElement<Cell>(null);
			}
			if (cell == null || !cell.IsEmpty())
			{
				return Actor.Fail("You can't deploy there!");
			}
			GameObject gameObject = this.ParentObject.RemoveOne();
			gameObject.RemoveFromContext(null);
			gameObject.GetPart<Tinkering_Mine>().Arm(Actor);
			string text2 = ((this.Timer > 0) ? "set" : "lay");
			IComponent<GameObject>.XDidYToZ(Actor, text2, gameObject, null, null, null, null, null, null, false, false, !Actor.IsPlayer(), false, false, null, null, null, false, false, false, false, false, null);
			cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			Actor.UseEnergy(1000, "Skill Tinkering " + ((this.Timer > 0) ? "Mine Lay" : "Bomb Set"), null, null, false);
			gameObject.PlayWorldSound("Sounds/Interact/sfx_interact_mine_lay", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			return true;
		}

		// Token: 0x0600B956 RID: 47446 RVA: 0x0043A743 File Offset: 0x00438943
		public static int GetSifrahDisarmingDifficulty(GameObject Explosive)
		{
			return Explosive.GetTier();
		}

		// Token: 0x0600B957 RID: 47447 RVA: 0x0043A74C File Offset: 0x0043894C
		public static int GetSifrahDisarmingRating(GameObject Actor)
		{
			int num = Actor.Stat("Intelligence", 0);
			if (Actor.HasSkill("Tinkering_GadgetInspector"))
			{
				num += 4;
			}
			if (Actor.HasSkill("Tinkering_LayMine"))
			{
				num += 4;
			}
			return num;
		}

		// Token: 0x0600B958 RID: 47448 RVA: 0x0043A78C File Offset: 0x0043898C
		public bool AttemptDisarm(GameObject Actor, IEvent FromEvent = null, bool Auto = false)
		{
			if (!this.Armed)
			{
				return false;
			}
			if (Actor.IsPlayer() && !this.ParentObject.Understood())
			{
				return false;
			}
			if (Actor.OnWorldMap())
			{
				return Actor.Fail("You cannot do that on the world map.");
			}
			if (!Actor.CheckFrozen(false, true, false, null))
			{
				return false;
			}
			if (Options.SifrahDisarming)
			{
				int sifrahDisarmingDifficulty = Tinkering_Mine.GetSifrahDisarmingDifficulty(this.Explosive);
				int sifrahDisarmingRating = Tinkering_Mine.GetSifrahDisarmingRating(Actor);
				DisarmingSifrah disarmingSifrah = new DisarmingSifrah(this.ParentObject, sifrahDisarmingDifficulty, sifrahDisarmingRating, Auto);
				disarmingSifrah.HandlerID = this.ParentObject.ID;
				disarmingSifrah.HandlerPartName = base.Name;
				disarmingSifrah.Play(this.ParentObject);
				if (disarmingSifrah.InterfaceExitRequested && FromEvent != null)
				{
					FromEvent.RequestInterfaceExit();
				}
			}
			else
			{
				int num = 9 + this.Explosive.GetTier() + this.Explosive.GetMark();
				if (Actor.HasSkill("Tinkering_GadgetInspector"))
				{
					num -= 4;
				}
				if (Actor.HasSkill("Tinkering_LayMine"))
				{
					num -= 4;
				}
				string text = ((this.Timer <= 0) ? "Tinkering Mine Disarm" : "Tinkering Bomb Disarm");
				int num2 = Actor.SaveChance("Intelligence", num, null, null, text, false, false, false, false, null);
				int num3 = Actor.Stat("Intelligence", 0);
				Mutations mutations;
				if (Actor.TryGetPart<Mutations>(out mutations))
				{
					if (mutations.HasMutation("Intuition"))
					{
						num3 += 10;
					}
					if (mutations.HasMutation("Precognition"))
					{
						num3 += 5;
					}
					if (mutations.HasMutation("Skittish"))
					{
						num2 -= 10;
					}
				}
				if (Actor.HasSkill("Discipline_IronMind"))
				{
					num3 += 2;
				}
				if (Actor.HasSkill("Discipline_Lionheart"))
				{
					num2 += 3;
				}
				if (num3 <= 10)
				{
					num2 += 20;
				}
				else if (num3 <= 15)
				{
					num2 += 10;
				}
				else if (num3 <= 20)
				{
					num2 -= 10;
				}
				else if (num3 <= 25)
				{
					num2 -= 5;
				}
				int num4;
				if (num3 <= 10)
				{
					num4 = 20;
				}
				else if (num3 <= 20)
				{
					num4 = 10;
				}
				else if (num3 <= 30)
				{
					num4 = 5;
				}
				else
				{
					num4 = 1;
				}
				if (num4 > 1)
				{
					num2 -= num2 % num4;
				}
				if (num2 > 100 - num4)
				{
					num2 = 100 - num4;
				}
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (Actor.IsPlayer())
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					string chanceColor = Stat.GetChanceColor(num2);
					stringBuilder.Append("Failing to disarm ").Append(this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false)).Append(" will detonate ")
						.Append(this.ParentObject.it)
						.Append(". You estimate you have");
					if (num2 < num4)
					{
						stringBuilder.Append(" less than a ").Append(chanceColor).Append(num4.ToString())
							.Append("%");
					}
					else
					{
						stringBuilder.Append(" about a ").Append(chanceColor).Append(num2.ToString())
							.Append("%");
					}
					stringBuilder.Append(chanceColor).Append(" chance of success. Do you want to make the attempt?");
					if (Popup.ShowYesNo(stringBuilder.ToString(), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
					{
						return false;
					}
				}
				else if (!num2.in100() && !num2.in100())
				{
					return false;
				}
				if (Actor.MakeSave("Intelligence", num, null, null, text, false, false, false, false, this.ParentObject))
				{
					IComponent<GameObject>.XDidYToZ(Actor, "disarm", this.ParentObject, null, null, null, null, Actor, null, false, false, false, true, false, null, null, null, false, false, false, false, false, null);
					this.Disarm();
				}
				else
				{
					this.Boom();
				}
			}
			Actor.UseEnergy(1000, "Skill Tinkering Mine Disarm", null, null, false);
			return true;
		}

		// Token: 0x0600B959 RID: 47449 RVA: 0x0043AB3C File Offset: 0x00438D3C
		public void DisarmingResultSuccess(GameObject Actor, GameObject Object, bool Auto)
		{
			if (Actor.IsPlayer())
			{
				IComponent<GameObject>.EmitMessage(Actor, "You disarm " + Object.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", ' ', false, !Auto, false, null, null);
			}
			this.Disarm();
		}

		// Token: 0x0600B95A RID: 47450 RVA: 0x0043AB9C File Offset: 0x00438D9C
		public void DisarmingResultExceptionalSuccess(GameObject Actor, GameObject Object, bool Auto)
		{
			this.DisarmingResultSuccess(Actor, Object, Auto);
			string randomBits = BitType.GetRandomBits("1d4".Roll(), this.Explosive.GetTier());
			if (!randomBits.IsNullOrEmpty())
			{
				Actor.RequirePart<BitLocker>(false).AddBits(randomBits);
				if (Actor.IsPlayer())
				{
					IComponent<GameObject>.EmitMessage(Actor, "You receive tinkering bits <{{|" + BitType.GetDisplayString(randomBits) + "}}>", ' ', false, !Auto, false, null, null);
				}
			}
		}

		// Token: 0x0600B95B RID: 47451 RVA: 0x0043AC10 File Offset: 0x00438E10
		public void DisarmingResultPartialSuccess(GameObject Actor, GameObject Object, bool Auto)
		{
			if (Actor.IsPlayer())
			{
				IComponent<GameObject>.EmitMessage(Actor, "You make some progress disarming " + Object.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", ' ', false, !Auto, false, null, null);
			}
		}

		// Token: 0x0600B95C RID: 47452 RVA: 0x0043AC69 File Offset: 0x00438E69
		public void DisarmingResultFailure(GameObject Actor, GameObject Object, bool Auto)
		{
			this.Boom();
		}

		// Token: 0x0600B95D RID: 47453 RVA: 0x0043AC74 File Offset: 0x00438E74
		public void DisarmingResultCriticalFailure(GameObject Actor, GameObject Object, bool Auto)
		{
			List<Cell> localAdjacentCells = this.ParentObject.CurrentCell.GetLocalAdjacentCells();
			Cell currentCell = Actor.CurrentCell;
			this.ParentObject.Discharge((currentCell != null && localAdjacentCells.Contains(currentCell)) ? currentCell : localAdjacentCells.GetRandomElement<Cell>(null), "3d8".Roll(), 0, "2d4", null, Actor, Object, null, null, null, null, null, 0, false, false, false);
			this.Boom();
		}

		// Token: 0x0600B95E RID: 47454 RVA: 0x0043ACE0 File Offset: 0x00438EE0
		public void Disarm()
		{
			if (!this.Armed)
			{
				return;
			}
			base.PlayWorldSound("Sounds/Interact/sfx_interact_mine_disarm", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			this.Armed = false;
			if (!this.DisarmedDetailColor.IsNullOrEmpty() && this.ParentObject.Render != null)
			{
				this.ParentObject.Render.DetailColor = this.DisarmedDetailColor;
			}
			this.ParentObject.RemoveIntProperty("AutoexploreActionAutoget");
		}

		// Token: 0x0600B95F RID: 47455 RVA: 0x0043AD68 File Offset: 0x00438F68
		public void Arm(GameObject Actor = null)
		{
			this.Armed = true;
			this.Owner = Actor;
			if (((Actor != null) ? Actor.Brain : null) != null)
			{
				if (this.OwnerAllegiance == null)
				{
					this.OwnerAllegiance = new AllegianceSet();
				}
				this.OwnerAllegiance.Copy(Actor.Brain.Allegiance);
				this.OwnerAllegiance.SourceID = Actor._BaseID;
			}
			this.PlayerMine = this.Owner != null && this.Owner.IsPlayer();
			if (!this.ArmedDetailColor.IsNullOrEmpty() && this.ParentObject.Render != null)
			{
				this.ParentObject.Render.DetailColor = this.ArmedDetailColor;
			}
		}

		// Token: 0x0600B960 RID: 47456 RVA: 0x0043AE17 File Offset: 0x00439017
		public bool ConsiderNonHostile(GameObject Actor)
		{
			return !this.ConsiderHostile(Actor);
		}

		// Token: 0x0600B961 RID: 47457 RVA: 0x0043AE24 File Offset: 0x00439024
		public bool ConsiderHostile(GameObject Actor)
		{
			return (!this.PlayerMine || !Actor.IsPlayerControlled()) && ((GameObject.Validate(this.Owner) && Actor.IsHostileTowards(this.Owner)) || (!this.OwnerAllegiance.IsNullOrEmpty<KeyValuePair<string, int>>() && Brain.GetFeelingLevel(this.OwnerAllegiance.GetBaseFeeling(Actor, null)) == Brain.FeelingLevel.Hostile));
		}

		// Token: 0x0600B962 RID: 47458 RVA: 0x0043AE84 File Offset: 0x00439084
		public bool ConsiderAllied(GameObject Actor)
		{
			return (this.PlayerMine && Actor.IsPlayerControlled()) || (GameObject.Validate(this.Owner) && Actor.IsAlliedTowards(this.Owner)) || (!this.OwnerAllegiance.IsNullOrEmpty<KeyValuePair<string, int>>() && Brain.GetFeelingLevel(this.OwnerAllegiance.GetBaseFeeling(Actor, null)) == Brain.FeelingLevel.Allied);
		}

		// Token: 0x0600B963 RID: 47459 RVA: 0x0043AEE8 File Offset: 0x004390E8
		public bool Avoidable(GameObject Actor)
		{
			if (Actor == null)
			{
				return false;
			}
			if (Actor.IsPlayer() && !this.ParentObject.Understood())
			{
				return false;
			}
			Hidden hidden;
			if (this.ParentObject.TryGetPart<Hidden>(out hidden))
			{
				if (hidden.Found)
				{
					return true;
				}
				if (!Actor.IsPlayerControlled() && this.ConsiderAllied(Actor))
				{
					return true;
				}
			}
			else
			{
				if (this.PlayerMine && !Actor.IsHostileTowards(IComponent<GameObject>.ThePlayer))
				{
					return true;
				}
				if (this.ConsiderNonHostile(Actor))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600B964 RID: 47460 RVA: 0x0043AF60 File Offset: 0x00439160
		private bool CheckInteraction(GameObject Actor)
		{
			if (this.ParentObject.InInventory != Actor && this.ParentObject.Equipped != Actor)
			{
				if (!Actor.FlightMatches(this.ParentObject))
				{
					return Actor.Fail("You cannot reach " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				if (!Actor.PhaseMatches(this.ParentObject))
				{
					string[] array = new string[5];
					array[0] = "Your ";
					int num = 1;
					BodyPart firstBodyPart = Actor.GetFirstBodyPart("Hands");
					array[num] = ((firstBodyPart != null) ? firstBodyPart.GetOrdinalName() : null) ?? "appendages";
					array[2] = " pass through ";
					array[3] = this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
					array[4] = ".";
					return Actor.Fail(string.Concat(array));
				}
			}
			return true;
		}

		// Token: 0x04004A7E RID: 19070
		public int Timer = -1;

		// Token: 0x04004A7F RID: 19071
		public bool PlayerMine = true;

		// Token: 0x04004A80 RID: 19072
		public GameObject Explosive;

		// Token: 0x04004A81 RID: 19073
		public GameObject Owner;

		// Token: 0x04004A82 RID: 19074
		public AllegianceSet OwnerAllegiance;

		// Token: 0x04004A83 RID: 19075
		public string Message = "AfterThrown";

		// Token: 0x04004A84 RID: 19076
		public bool Armed = true;

		// Token: 0x04004A85 RID: 19077
		public string ArmedDetailColor = "R";

		// Token: 0x04004A86 RID: 19078
		public string DisarmedDetailColor = "y";
	}
}
