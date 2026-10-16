using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F3E RID: 3902
	[Serializable]
	public class CyberneticsPhaseAdaptiveScope : IPoweredPart, IThrownWeaponFlexPhaseProvider
	{
		// Token: 0x06009B9F RID: 39839 RVA: 0x00386E4B File Offset: 0x0038504B
		public CyberneticsPhaseAdaptiveScope()
		{
			this.WorksOnImplantee = true;
			this.ChargeUse = 0;
			this.NameForStatus = "PhaseAdaptiveScope";
		}

		// Token: 0x06009BA0 RID: 39840 RVA: 0x00386E77 File Offset: 0x00385077
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009BA1 RID: 39841 RVA: 0x00386E7C File Offset: 0x0038507C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID || ID == PooledEvent<GetThrownWeaponFlexPhaseProviderEvent>.ID || ID == PooledEvent<BeforeProjectileHitEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == PooledEvent<ProjectileMovingEvent>.ID;
		}

		// Token: 0x06009BA2 RID: 39842 RVA: 0x00386ED0 File Offset: 0x003850D0
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddActivatedAbility("Phase-Adaptive Projectiles", CyberneticsPhaseAdaptiveScope.COMMAND_NAME, "Cybernetics", null, "\a", null, true, true, false, false, false, true, false, false, true, true, false, false, -1, null, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009BA3 RID: 39843 RVA: 0x00386F1D File Offset: 0x0038511D
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009BA4 RID: 39844 RVA: 0x00386F38 File Offset: 0x00385138
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == CyberneticsPhaseAdaptiveScope.COMMAND_NAME && E.Actor == this.ParentObject.Implantee)
			{
				this.ParentObject.Implantee.ToggleActivatedAbility(this.ActivatedAbilityID, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BA5 RID: 39845 RVA: 0x00386F94 File Offset: 0x00385194
		public override bool HandleEvent(GetThrownWeaponFlexPhaseProviderEvent E)
		{
			if (this.SafetyIncidentTurn < The.Game.TimeTicks && E.Priority < 100 && this.IsObjectActivePartSubject(E.Actor) && E.Actor.IsActivatedAbilityUsable(this.ActivatedAbilityID) && E.Actor.IsActivatedAbilityToggledOn(this.ActivatedAbilityID) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Provider = this;
				E.Priority = 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BA6 RID: 39846 RVA: 0x00387030 File Offset: 0x00385230
		public override bool HandleEvent(BeforeProjectileHitEvent E)
		{
			if (!E.Prospective && this.SafetyIncidentTurn < The.Game.TimeTicks && GameObject.Validate(E.Projectile) && GameObject.Validate(E.Attacker) && GameObject.Validate(E.ApparentTarget) && GameObject.Validate(E.Object) && this.IsObjectActivePartSubject(E.Attacker) && E.Object != E.ApparentTarget && E.Attacker.DistanceTo(E.Object) <= E.Attacker.DistanceTo(E.ApparentTarget) && E.Attacker.IsActivatedAbilityUsable(this.ActivatedAbilityID) && E.Attacker.IsActivatedAbilityToggledOn(this.ActivatedAbilityID) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				Cell cell = E.Cell ?? E.Projectile.CurrentCell;
				Cell currentCell = E.ApparentTarget.CurrentCell;
				if (cell != null && cell != currentCell)
				{
					bool flag;
					bool flag2;
					bool flag3;
					this.TryDesyncPhase(E.Projectile, E.Object, cell, E.PenetrateCreatures, E.PenetrateWalls, out flag, out flag2, out flag3);
					if (flag)
					{
						E.Recheck = true;
					}
					if (flag2)
					{
						E.RecheckPhase = true;
					}
					if (flag3)
					{
						this.SafetyIncidentTurn = The.Game.TimeTicks;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BA7 RID: 39847 RVA: 0x003871B8 File Offset: 0x003853B8
		public override bool HandleEvent(ProjectileMovingEvent E)
		{
			if (this.SafetyIncidentTurn < The.Game.TimeTicks && GameObject.Validate(E.Projectile) && GameObject.Validate(E.Attacker) && GameObject.Validate(E.ApparentTarget) && E.PathIndex < E.Path.Count - 1 && this.IsObjectActivePartSubject(E.Attacker) && E.Attacker.IsActivatedAbilityUsable(this.ActivatedAbilityID) && E.Attacker.IsActivatedAbilityToggledOn(this.ActivatedAbilityID) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				Cell cell = E.Cell ?? E.Projectile.CurrentCell;
				Cell currentCell = E.ApparentTarget.CurrentCell;
				if (cell != null && currentCell != null)
				{
					Point point = E.Path[E.PathIndex + 1];
					if (point.X == currentCell.X && point.Y == currentCell.Y)
					{
						bool flag;
						bool flag2;
						bool flag3;
						this.TrySyncPhase(E.Projectile, E.ApparentTarget, cell, out flag, out flag2, out flag3);
						if (flag2)
						{
							E.RecheckPhase = true;
						}
						if (flag3)
						{
							this.SafetyIncidentTurn = The.Game.TimeTicks;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BA8 RID: 39848 RVA: 0x00387321 File Offset: 0x00385521
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009BA9 RID: 39849 RVA: 0x00387324 File Offset: 0x00385524
		public void ThrownWeaponFlexPhaseStart(GameObject Weapon)
		{
		}

		// Token: 0x06009BAA RID: 39850 RVA: 0x00387326 File Offset: 0x00385526
		public bool ThrownWeaponFlexPhaseIsActive(GameObject Weapon)
		{
			return this.SafetyIncidentTurn < The.Game.TimeTicks;
		}

		// Token: 0x06009BAB RID: 39851 RVA: 0x0038733C File Offset: 0x0038553C
		public bool ThrownWeaponFlexPhaseTraversal(GameObject Actor, GameObject WillHit, GameObject Target, GameObject Weapon, int Phase, Cell FromCell, Cell ToCell, out bool RecheckHit, out bool RecheckPhase, bool HasDynamicTargets = false)
		{
			bool flag = false;
			RecheckHit = false;
			RecheckPhase = false;
			if (Target != null && Actor.IsActivatedAbilityUsable(this.ActivatedAbilityID) && Actor.IsActivatedAbilityToggledOn(this.ActivatedAbilityID) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (Target == WillHit)
				{
					this.TrySyncPhase(Weapon, Target, FromCell, out RecheckHit, out RecheckPhase, out flag);
				}
				else if (WillHit != null && (HasDynamicTargets || Actor.DistanceTo(WillHit) < Actor.DistanceTo(Target)))
				{
					this.TryDesyncPhase(Weapon, WillHit, FromCell, false, false, out RecheckHit, out RecheckPhase, out flag);
				}
				else
				{
					GameObject combatTarget = ToCell.GetCombatTarget(Actor, true, false, false, Phase, Weapon, null, null, null, null, true, true, (GameObject o) => !o.IsCreature);
					if (combatTarget != null && (HasDynamicTargets || Actor.DistanceTo(combatTarget) < Actor.DistanceTo(Target)))
					{
						this.TryDesyncPhase(Weapon, combatTarget, FromCell, false, false, out RecheckHit, out RecheckPhase, out flag);
					}
				}
				if (flag)
				{
					this.SafetyIncidentTurn = The.Game.TimeTicks;
				}
			}
			return true;
		}

		// Token: 0x06009BAC RID: 39852 RVA: 0x00387461 File Offset: 0x00385661
		public void ThrownWeaponFlexPhaseEnd(GameObject Weapon)
		{
		}

		// Token: 0x06009BAD RID: 39853 RVA: 0x00387464 File Offset: 0x00385664
		private void TryDesyncPhase(GameObject Projectile, GameObject Subject, Cell Cell, bool PenetrateCreatures, bool PenetrateWalls, out bool RecheckHit, out bool RecheckPhase, out bool SafetyIncident)
		{
			RecheckHit = false;
			RecheckPhase = false;
			SafetyIncident = false;
			if (Cell == null)
			{
				return;
			}
			if (PenetrateCreatures && Subject.IsCreature)
			{
				return;
			}
			if (PenetrateWalls && Subject.IsWall())
			{
				return;
			}
			int phase = Subject.GetPhase();
			if (phase == 3 || phase == 4 || phase == 0 || phase == 5)
			{
				return;
			}
			if (this.SafetyIncidentTurn == The.Game.TimeTicks)
			{
				return;
			}
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (Projectile.HasEffect<Omniphase>())
			{
				base.ConsumeCharge(null, null);
				Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
				@event.SetParameter("Object", activePartFirstSubject);
				@event.SetParameter("Device", this.ParentObject);
				@event.SetParameter("Operator", activePartFirstSubject);
				@event.SetParameter("Cell", Cell);
				@event.SetParameter("Purpose", "alter the phase of " + Projectile.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
				if (!activePartFirstSubject.FireEvent(@event) || !Cell.FireEvent(@event))
				{
					SafetyIncident = true;
					return;
				}
				Projectile.RemoveAllEffects<Omniphase>(true);
				RecheckHit = true;
				RecheckPhase = true;
				this.PhaseChangeVisual(Cell);
			}
			if (phase == 1)
			{
				if (!Projectile.HasEffect<Phased>())
				{
					base.ConsumeCharge(null, null);
					Event event2 = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
					event2.SetParameter("Object", activePartFirstSubject);
					event2.SetParameter("Device", this.ParentObject);
					event2.SetParameter("Operator", activePartFirstSubject);
					event2.SetParameter("Cell", Cell);
					event2.SetParameter("Purpose", "alter the phase of " + Projectile.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
					if (!activePartFirstSubject.FireEvent(event2) || !Cell.FireEvent(event2))
					{
						SafetyIncident = true;
						return;
					}
					if (Projectile.ForceApplyEffect(new Phased(1), null))
					{
						RecheckHit = true;
						RecheckPhase = true;
						this.PhaseChangeVisual(Cell);
						return;
					}
				}
			}
			else if (phase == 2 && Projectile.HasEffect<Phased>())
			{
				base.ConsumeCharge(null, null);
				Event event3 = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
				event3.SetParameter("Object", activePartFirstSubject);
				event3.SetParameter("Device", this.ParentObject);
				event3.SetParameter("Operator", activePartFirstSubject);
				event3.SetParameter("Cell", Cell);
				event3.SetParameter("Purpose", "alter the phase of " + Projectile.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
				if (!activePartFirstSubject.FireEvent(event3) || !Cell.FireEvent(event3))
				{
					SafetyIncident = true;
					return;
				}
				Projectile.RemoveAllEffects<Phased>(true);
				RecheckHit = true;
				RecheckPhase = true;
				this.PhaseChangeVisual(Cell);
			}
		}

		// Token: 0x06009BAE RID: 39854 RVA: 0x00387774 File Offset: 0x00385974
		private void TrySyncPhase(GameObject Projectile, GameObject Subject, Cell Cell, out bool RecheckHit, out bool RecheckPhase, out bool SafetyIncident)
		{
			RecheckHit = false;
			RecheckPhase = false;
			SafetyIncident = false;
			if (Cell == null)
			{
				return;
			}
			int phase = Subject.GetPhase();
			if (phase == 3 || phase == 4 || phase == 0 || phase == 5)
			{
				return;
			}
			if (Projectile.HasEffect<Omniphase>())
			{
				return;
			}
			if (this.SafetyIncidentTurn == The.Game.TimeTicks)
			{
				return;
			}
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (phase == 1)
			{
				if (Projectile.HasEffect<Phased>())
				{
					base.ConsumeCharge(null, null);
					Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
					@event.SetParameter("Object", activePartFirstSubject);
					@event.SetParameter("Device", this.ParentObject);
					@event.SetParameter("Operator", activePartFirstSubject);
					@event.SetParameter("Cell", Cell);
					@event.SetParameter("Purpose", "alter the phase of " + Projectile.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
					if (!activePartFirstSubject.FireEvent(@event) || !Cell.FireEvent(@event))
					{
						SafetyIncident = true;
						return;
					}
					Projectile.RemoveAllEffects<Phased>(true);
					RecheckHit = true;
					RecheckPhase = true;
					this.PhaseChangeVisual(Cell);
					return;
				}
			}
			else if (phase == 2 && !Projectile.HasEffect<Phased>())
			{
				base.ConsumeCharge(null, null);
				Event event2 = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
				event2.SetParameter("Object", activePartFirstSubject);
				event2.SetParameter("Device", this.ParentObject);
				event2.SetParameter("Operator", activePartFirstSubject);
				event2.SetParameter("Cell", Cell);
				event2.SetParameter("Purpose", "alter the phase of " + Projectile.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
				if (!activePartFirstSubject.FireEvent(event2) || !Cell.FireEvent(event2))
				{
					SafetyIncident = true;
					return;
				}
				if (Projectile.ForceApplyEffect(new Phased(1), null))
				{
					RecheckHit = true;
					RecheckPhase = true;
					this.PhaseChangeVisual(Cell);
				}
			}
		}

		// Token: 0x06009BAF RID: 39855 RVA: 0x00387994 File Offset: 0x00385B94
		public void PhaseChangeVisual(Cell Cell)
		{
			if (Options.UseTiles)
			{
				Cell.TileParticleBlip("Tiles2/status_phase_change.bmp", "&Y", "y", 10, false, false, false, 0L);
				return;
			}
			Cell.ParticleBlip("&Y÷", 10, 0L, false);
		}

		// Token: 0x04004083 RID: 16515
		public static readonly string COMMAND_NAME = "CommandTogglePhaseAdaptiveScope";

		// Token: 0x04004084 RID: 16516
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004085 RID: 16517
		[NonSerialized]
		private long SafetyIncidentTurn;
	}
}
