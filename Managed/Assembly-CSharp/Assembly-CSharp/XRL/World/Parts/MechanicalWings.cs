using System;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001065 RID: 4197
	[Serializable]
	public class MechanicalWings : IPoweredPart, IFlightSource
	{
		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600A6FC RID: 42748 RVA: 0x003D0B97 File Offset: 0x003CED97
		public int FlightLevel
		{
			get
			{
				return this.Level;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600A6FD RID: 42749 RVA: 0x003D0B9F File Offset: 0x003CED9F
		public int FlightBaseFallChance
		{
			get
			{
				return this.BaseFallChance;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600A6FE RID: 42750 RVA: 0x003D0BA7 File Offset: 0x003CEDA7
		public bool FlightRequiresOngoingEffort
		{
			get
			{
				return this.RequiresOngoingEffort;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600A6FF RID: 42751 RVA: 0x003D0BAF File Offset: 0x003CEDAF
		public string FlightEvent
		{
			get
			{
				return this._FlightEvent;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600A700 RID: 42752 RVA: 0x003D0BB7 File Offset: 0x003CEDB7
		public string FlightActivatedAbilityClass
		{
			get
			{
				return "Items";
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600A701 RID: 42753 RVA: 0x003D0BBE File Offset: 0x003CEDBE
		// (set) Token: 0x0600A702 RID: 42754 RVA: 0x003D0BC6 File Offset: 0x003CEDC6
		public bool FlightFlying
		{
			get
			{
				return this._FlightFlying;
			}
			set
			{
				this._FlightFlying = value;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x0600A703 RID: 42755 RVA: 0x003D0BCF File Offset: 0x003CEDCF
		// (set) Token: 0x0600A704 RID: 42756 RVA: 0x003D0BD7 File Offset: 0x003CEDD7
		public Guid FlightActivatedAbilityID
		{
			get
			{
				return this._FlightActivatedAbilityID;
			}
			set
			{
				this._FlightActivatedAbilityID = value;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x0600A705 RID: 42757 RVA: 0x003D0BE0 File Offset: 0x003CEDE0
		// (set) Token: 0x0600A706 RID: 42758 RVA: 0x003D0BE8 File Offset: 0x003CEDE8
		public string FlightSourceDescription
		{
			get
			{
				return this._FlightSourceDescription;
			}
			set
			{
				this._FlightSourceDescription = value;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600A707 RID: 42759 RVA: 0x003D0BF1 File Offset: 0x003CEDF1
		// (set) Token: 0x0600A708 RID: 42760 RVA: 0x003D0BF9 File Offset: 0x003CEDF9
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this._FlightEvent = "Activate" + this.Type;
			}
		}

		// Token: 0x0600A709 RID: 42761 RVA: 0x003D0C18 File Offset: 0x003CEE18
		public MechanicalWings()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = false;
			this.MustBeUnderstood = true;
			this.WorksOnWearer = true;
		}

		// Token: 0x0600A70A RID: 42762 RVA: 0x003D0C98 File Offset: 0x003CEE98
		public override bool SameAs(IPart p)
		{
			MechanicalWings mechanicalWings = p as MechanicalWings;
			return mechanicalWings.Level == this.Level && mechanicalWings.BaseFallChance == this.BaseFallChance && mechanicalWings.CrashBreakChance == this.CrashBreakChance && mechanicalWings.RequiresOngoingEffort == this.RequiresOngoingEffort && base.SameAs(p);
		}

		// Token: 0x0600A70B RID: 42763 RVA: 0x003D0CF4 File Offset: 0x003CEEF4
		public void CheckOperation()
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				Flight.FailFlying(this.ParentObject, this.User, this);
			}
		}

		// Token: 0x0600A70C RID: 42764 RVA: 0x003D0D3C File Offset: 0x003CEF3C
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A70D RID: 42765 RVA: 0x003D0D40 File Offset: 0x003CEF40
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.FlightFlying && !base.OnWorldMap)
			{
				if (base.IsReady(true, false, false, false, false, false, false, false, false, false, Amount, null, false, 0L, null))
				{
					Flight.MaintainFlight(this.ParentObject, this.User, this);
					return;
				}
				Flight.FailFlying(this.ParentObject, this.User, this);
			}
		}

		// Token: 0x0600A70E RID: 42766 RVA: 0x003D0DAC File Offset: 0x003CEFAC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == AIGetPassiveAbilityListEvent.ID || ID == PooledEvent<AttemptToLandEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<BodyPositionChangedEvent>.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EquippedEvent.ID || ID == PooledEvent<ExamineSuccessEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetLostChanceEvent.ID || ID == GetMovementCapabilitiesEvent.ID || ID == InventoryActionEvent.ID || ID == PooledEvent<MovementModeChangedEvent>.ID || ID == ReplicaCreatedEvent.ID || ID == TravelSpeedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A70F RID: 42767 RVA: 0x003D0E69 File Offset: 0x003CF069
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("CrashChance", Flight.GetMoveFallChance(this.User, this), false, 0);
			stats.Set("SwoopCrashChance", Flight.GetSwoopFallChance(this.User, this), false, 0);
		}

		// Token: 0x0600A710 RID: 42768 RVA: 0x003D0EA0 File Offset: 0x003CF0A0
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.FlightActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.User);
			GameObject user = this.User;
			ActivatedAbilityEntry activatedAbilityEntry = ((user != null) ? user.GetActivatedAbilityByCommand(Flight.SWOOP_ATTACK_COMMAND_NAME) : null);
			if (activatedAbilityEntry != null)
			{
				base.DescribeMyActivatedAbility(activatedAbilityEntry.ID, new Action<Templates.StatCollector>(this.CollectStats), this.User);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A711 RID: 42769 RVA: 0x003D0F10 File Offset: 0x003CF110
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Actor))
			{
				ActivatedAbilityEntry activatedAbility = this.User.GetActivatedAbility(this.FlightActivatedAbilityID);
				E.Add(activatedAbility.DisplayName, this.FlightEvent, 20000, activatedAbility, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A712 RID: 42770 RVA: 0x003D0F5D File Offset: 0x003CF15D
		public override bool HandleEvent(ExamineSuccessEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				this.ApplyOperations(this.User);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A713 RID: 42771 RVA: 0x003D0F80 File Offset: 0x003CF180
		public override bool HandleEvent(AIGetPassiveAbilityListEvent E)
		{
			if (!this.FlightFlying && E.Actor == this.User && Flight.EnvironmentAllowsFlight(E.Actor) && Flight.IsAbilityAIUsable(this, E.Actor))
			{
				E.Add(this.FlightEvent, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A714 RID: 42772 RVA: 0x003D0FD8 File Offset: 0x003CF1D8
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (!this.FlightFlying && this.User == E.Actor && Flight.EnvironmentAllowsFlight(E.Actor) && Flight.IsAbilityAIUsable(this, E.Actor))
			{
				E.Add(this.FlightEvent, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A715 RID: 42773 RVA: 0x003D102F File Offset: 0x003CF22F
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.User)
			{
				Flight.SyncFlying(this.ParentObject, this.User, this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A716 RID: 42774 RVA: 0x003D1058 File Offset: 0x003CF258
		public override bool HandleEvent(GetLostChanceEvent E)
		{
			if (base.WasReady())
			{
				E.PercentageBonus += 36 + 4 * this.Level;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A717 RID: 42775 RVA: 0x003D1081 File Offset: 0x003CF281
		public override bool HandleEvent(TravelSpeedEvent E)
		{
			if (base.WasReady())
			{
				E.PercentageBonus += 50 + 50 * this.Level;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A718 RID: 42776 RVA: 0x003D10AC File Offset: 0x003CF2AC
		public override bool HandleEvent(BodyPositionChangedEvent E)
		{
			if (E.Object == this.User && this.FlightFlying)
			{
				if (E.Involuntary)
				{
					Flight.FailFlying(this.ParentObject, this.User, this);
				}
				else
				{
					Flight.StopFlying(this.ParentObject, this.User, this, false, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A719 RID: 42777 RVA: 0x003D1108 File Offset: 0x003CF308
		public override bool HandleEvent(MovementModeChangedEvent E)
		{
			if (E.Object == this.User && this.FlightFlying)
			{
				if (E.Involuntary)
				{
					Flight.FailFlying(this.ParentObject, this.User, this);
				}
				else
				{
					Flight.StopFlying(this.ParentObject, this.User, this, false, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A71A RID: 42778 RVA: 0x003D1164 File Offset: 0x003CF364
		public override bool HandleEvent(AttemptToLandEvent E)
		{
			return (!this.FlightFlying || !Flight.StopFlying(this.ParentObject, this.User, this, false, false)) && base.HandleEvent(E);
		}

		// Token: 0x0600A71B RID: 42779 RVA: 0x003D118D File Offset: 0x003CF38D
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.CheckOperation();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A71C RID: 42780 RVA: 0x003D119C File Offset: 0x003CF39C
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckOperation();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A71D RID: 42781 RVA: 0x003D11AB File Offset: 0x003CF3AB
		public override bool HandleEvent(EquippedEvent E)
		{
			this.ApplyOperations(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A71E RID: 42782 RVA: 0x003D11C0 File Offset: 0x003CF3C0
		public override bool HandleEvent(UnequippedEvent E)
		{
			Flight.FailFlying(this.ParentObject, E.Actor, this);
			this.UnapplyOperations(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A71F RID: 42783 RVA: 0x003D11E8 File Offset: 0x003CF3E8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) != ActivePartStatus.NeedsSubject)
			{
				E.AddAction("Activate", "activate", "ActivateMechanicalWings", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A720 RID: 42784 RVA: 0x003D1248 File Offset: 0x003CF448
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateMechanicalWings" && this.User != null && this.TryStartup())
			{
				bool flightFlying = this.FlightFlying;
				this.User.FireEvent(this._FlightEvent);
				if (this.FlightFlying != flightFlying)
				{
					E.RequestInterfaceExit();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A721 RID: 42785 RVA: 0x003D12A6 File Offset: 0x003CF4A6
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("travel", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A722 RID: 42786 RVA: 0x003D12C9 File Offset: 0x003CF4C9
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckOperation();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A723 RID: 42787 RVA: 0x003D12D8 File Offset: 0x003CF4D8
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A724 RID: 42788 RVA: 0x003D12DB File Offset: 0x003CF4DB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(this._FlightEvent);
			Registrar.Register("FlightFailed");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A725 RID: 42789 RVA: 0x003D12FC File Offset: 0x003CF4FC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				Flight.CheckFlight(this.ParentObject, this.User, this);
			}
			else if (E.ID == this._FlightEvent)
			{
				if (this.User.IsActivatedAbilityToggledOn(this.FlightActivatedAbilityID))
				{
					if (this.User.IsPlayer() && base.currentCell != null && this.User.GetEffectCount(typeof(Flying)) <= 1)
					{
						int i = 0;
						int count = base.currentCell.Objects.Count;
						while (i < count)
						{
							GameObject gameObject = base.currentCell.Objects[i];
							StairsDown part = gameObject.GetPart<StairsDown>();
							if (part != null && part.IsLongFall() && Popup.WarnYesNo("It looks like a long way down " + gameObject.the + gameObject.ShortDisplayName + " you're above. Are you sure you want to stop flying?", "Sounds/UI/ui_notification_warning", true, DialogResult.Cancel) != DialogResult.Yes)
							{
								return false;
							}
							i++;
						}
					}
					Flight.StopFlying(this.ParentObject, this.User, this, false, false);
				}
				else
				{
					if (!this.TryStartup())
					{
						return false;
					}
					Flight.StartFlying(this.ParentObject, this.User, this);
				}
			}
			else if (E.ID == "FlightFailed" && this.User != null && this.CrashBreakChance.in100())
			{
				this.ParentObject.ApplyEffect(new Broken(), null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x0600A726 RID: 42790 RVA: 0x003D1474 File Offset: 0x003CF674
		public GameObject User
		{
			get
			{
				return this.GetActivePartFirstSubject();
			}
		}

		// Token: 0x0600A727 RID: 42791 RVA: 0x003D147C File Offset: 0x003CF67C
		public override bool IsActivePartEngaged()
		{
			return this.FlightFlying && base.IsActivePartEngaged();
		}

		// Token: 0x0600A728 RID: 42792 RVA: 0x003D1490 File Offset: 0x003CF690
		private bool TryStartup()
		{
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (this.User != null && this.User.IsPlayer())
				{
					if (activePartStatus == ActivePartStatus.Booting && this.ParentObject.GetPart<BootSequence>().IsObvious())
					{
						Popup.ShowFail(this.ParentObject.The + this.ParentObject.ShortDisplayName + this.ParentObject.Is + " still starting up.", true, true, true);
					}
					else
					{
						Popup.ShowFail(this.ParentObject.The + this.ParentObject.ShortDisplayName + this.ParentObject.Is + " unresponsive.", true, true, true);
					}
				}
				return false;
			}
			base.PlayWorldSound("Sounds/Interact/sfx_interact_mechanicalWings_on", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			return true;
		}

		// Token: 0x0600A729 RID: 42793 RVA: 0x003D1590 File Offset: 0x003CF790
		private void ApplyOperations(GameObject who)
		{
			if (GameObject.Validate(ref who) && !this.AbilitiesAppliedToEquipper && this.IsObjectActivePartSubject(who))
			{
				this.AbilitiesAppliedToEquipper = true;
				who.RegisterPartEvent(this, this._FlightEvent);
				who.RegisterPartEvent(this, "EnteredCell");
				who.RegisterPartEvent(this, "FlightFailed");
				Flight.AbilitySetup(this.ParentObject, who, this);
			}
		}

		// Token: 0x0600A72A RID: 42794 RVA: 0x003D15F4 File Offset: 0x003CF7F4
		private void UnapplyOperations(GameObject who)
		{
			if (this.AbilitiesAppliedToEquipper)
			{
				this.AbilitiesAppliedToEquipper = false;
				if (GameObject.Validate(ref who))
				{
					who.UnregisterPartEvent(this, this._FlightEvent);
					who.UnregisterPartEvent(this, "EnteredCell");
					who.UnregisterPartEvent(this, "FlightFailed");
					Flight.AbilityTeardown(this.ParentObject, who, this);
				}
			}
		}

		// Token: 0x040044FA RID: 17658
		public int Level = 1;

		// Token: 0x040044FB RID: 17659
		public int BaseFallChance = 6;

		// Token: 0x040044FC RID: 17660
		public int CrashBreakChance = 1;

		// Token: 0x040044FD RID: 17661
		public bool RequiresOngoingEffort = true;

		// Token: 0x040044FE RID: 17662
		public bool AbilitiesAppliedToEquipper;

		// Token: 0x040044FF RID: 17663
		public string _FlightEvent = "ActivateMechanicalWings";

		// Token: 0x04004500 RID: 17664
		public bool _FlightFlying;

		// Token: 0x04004501 RID: 17665
		public Guid _FlightActivatedAbilityID = Guid.Empty;

		// Token: 0x04004502 RID: 17666
		public string _FlightSourceDescription = "Mechanical Wings";

		// Token: 0x04004503 RID: 17667
		public string _Type = "MechanicalWings";
	}
}
