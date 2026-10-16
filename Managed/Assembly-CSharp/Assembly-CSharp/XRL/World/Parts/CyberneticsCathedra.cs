using System;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F1E RID: 3870
	[Serializable]
	public class CyberneticsCathedra : IPart, IFlightSource
	{
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06009A43 RID: 39491 RVA: 0x0038004E File Offset: 0x0037E24E
		public int FlightLevel
		{
			get
			{
				return this.GetLevel(null);
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06009A44 RID: 39492 RVA: 0x00380057 File Offset: 0x0037E257
		public int FlightBaseFallChance
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06009A45 RID: 39493 RVA: 0x0038005A File Offset: 0x0037E25A
		public bool FlightRequiresOngoingEffort
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06009A46 RID: 39494 RVA: 0x0038005D File Offset: 0x0037E25D
		public string FlightEvent
		{
			get
			{
				return "CommandActivateCathedraFlight";
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06009A47 RID: 39495 RVA: 0x00380064 File Offset: 0x0037E264
		public string FlightActivatedAbilityClass
		{
			get
			{
				return "Cybernetics";
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06009A48 RID: 39496 RVA: 0x0038006B File Offset: 0x0037E26B
		public string FlightSourceDescription
		{
			get
			{
				return "Cathedra";
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06009A4A RID: 39498 RVA: 0x0038007B File Offset: 0x0037E27B
		// (set) Token: 0x06009A49 RID: 39497 RVA: 0x00380072 File Offset: 0x0037E272
		public bool FlightFlying { get; set; }

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06009A4C RID: 39500 RVA: 0x0038008C File Offset: 0x0037E28C
		// (set) Token: 0x06009A4B RID: 39499 RVA: 0x00380083 File Offset: 0x0037E283
		public Guid FlightActivatedAbilityID { get; set; }

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06009A4D RID: 39501 RVA: 0x00380094 File Offset: 0x0037E294
		public GameObject User
		{
			get
			{
				GameObject gameObject;
				if ((gameObject = this._User) == null)
				{
					gameObject = (this._User = this.ParentObject.Implantee);
				}
				return gameObject;
			}
		}

		// Token: 0x06009A4E RID: 39502 RVA: 0x003800BF File Offset: 0x0037E2BF
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			base.Write(Basis, Writer);
			Writer.Write(this.FlightFlying);
			Writer.Write(this.FlightActivatedAbilityID);
		}

		// Token: 0x06009A4F RID: 39503 RVA: 0x003800E1 File Offset: 0x0037E2E1
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			this.FlightFlying = Reader.ReadBoolean();
			this.FlightActivatedAbilityID = Reader.ReadGuid();
		}

		// Token: 0x06009A50 RID: 39504 RVA: 0x00380103 File Offset: 0x0037E303
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009A51 RID: 39505 RVA: 0x00380108 File Offset: 0x0037E308
		public virtual void CollectStats(Templates.StatCollector stats)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, this.User), this.GetCooldown(null), 0);
			stats.Set("CrashChance", Flight.GetMoveFallChance(this.User, this), false, 0);
			stats.Set("SwoopCrashChance", Flight.GetSwoopFallChance(this.User, this), false, 0);
		}

		// Token: 0x06009A52 RID: 39506 RVA: 0x00380168 File Offset: 0x0037E368
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AttemptToLandEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<BodyPositionChangedEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == PooledEvent<MovementModeChangedEvent>.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009A53 RID: 39507 RVA: 0x003801CC File Offset: 0x0037E3CC
		public override bool HandleEvent(GetTinkeringBonusEvent E)
		{
			if (E.Type == "Inspect" || E.Type == "Disassemble")
			{
				int num = 5;
				int num2 = Math.Min(2 + num, 4);
				if (num2 != 0)
				{
					E.Bonus += num2;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A54 RID: 39508 RVA: 0x00380224 File Offset: 0x0037E424
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.User);
			base.DescribeMyActivatedAbility(this.FlightActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.User);
			GameObject user = this.User;
			ActivatedAbilityEntry activatedAbilityEntry = ((user != null) ? user.GetActivatedAbilityByCommand(Flight.SWOOP_ATTACK_COMMAND_NAME) : null);
			if (activatedAbilityEntry != null)
			{
				base.DescribeMyActivatedAbility(activatedAbilityEntry.ID, new Action<Templates.StatCollector>(this.CollectStats), this.User);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A55 RID: 39509 RVA: 0x003802B3 File Offset: 0x0037E4B3
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.OnImplanted(E.Implantee);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A56 RID: 39510 RVA: 0x003802C8 File Offset: 0x0037E4C8
		public override bool HandleEvent(UnimplantedEvent E)
		{
			this.OnUnimplanted(E.Implantee);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A57 RID: 39511 RVA: 0x003802DD File Offset: 0x0037E4DD
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
			return base.HandleEvent(E);
		}

		// Token: 0x06009A58 RID: 39512 RVA: 0x003802F8 File Offset: 0x0037E4F8
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandActivateCathedra")
			{
				if (!E.Actor.OnWorldMap())
				{
					this.Activate(E.Actor);
					this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
				}
			}
			else if (E.Command == this.FlightEvent)
			{
				if (E.Actor.IsActivatedAbilityToggledOn(this.FlightActivatedAbilityID))
				{
					if (E.Actor.IsPlayer() && E.Actor.CurrentCell != null && E.Actor.GetEffectCount(typeof(Flying)) <= 1)
					{
						foreach (GameObject gameObject in E.Actor.CurrentCell.Objects)
						{
							StairsDown stairsDown;
							if (gameObject.TryGetPart<StairsDown>(out stairsDown) && stairsDown.IsLongFall() && Popup.WarnYesNo("It looks like a long way down " + gameObject.the + gameObject.ShortDisplayName + " you're above. Are you sure you want to stop flying?", "Sounds/UI/ui_notification_warning", true, DialogResult.Cancel) != DialogResult.Yes)
							{
								return false;
							}
						}
					}
					Flight.StopFlying(this.ParentObject, E.Actor, this, false, false);
				}
				else
				{
					Flight.StartFlying(this.ParentObject, E.Actor, this);
				}
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A59 RID: 39513 RVA: 0x00380480 File Offset: 0x0037E680
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.FlightFlying)
			{
				Flight.MaintainFlight(this.ParentObject, this.User, this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A5A RID: 39514 RVA: 0x003804A4 File Offset: 0x0037E6A4
		public override bool HandleEvent(BodyPositionChangedEvent E)
		{
			if (this.FlightFlying && E.To != "Flying")
			{
				if (E.Involuntary)
				{
					Flight.FailFlying(this.ParentObject, E.Object, this);
				}
				else
				{
					Flight.StopFlying(this.ParentObject, E.Object, this, false, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A5B RID: 39515 RVA: 0x00380504 File Offset: 0x0037E704
		public override bool HandleEvent(MovementModeChangedEvent E)
		{
			if (this.FlightFlying && E.To != "Flying")
			{
				if (E.Involuntary)
				{
					Flight.FailFlying(this.ParentObject, E.Object, this);
				}
				else
				{
					Flight.StopFlying(this.ParentObject, E.Object, this, false, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A5C RID: 39516 RVA: 0x00380564 File Offset: 0x0037E764
		public override bool HandleEvent(AttemptToLandEvent E)
		{
			return (!this.FlightFlying || !Flight.StopFlying(this.ParentObject, E.Actor, this, false, false)) && base.HandleEvent(E);
		}

		// Token: 0x06009A5D RID: 39517 RVA: 0x0038058D File Offset: 0x0037E78D
		public override bool HandleEvent(GetMaxCarriedWeightEvent E)
		{
			E.Weight += 100.0;
			return base.HandleEvent(E);
		}

		// Token: 0x06009A5E RID: 39518 RVA: 0x003805AC File Offset: 0x0037E7AC
		public override bool HandleEvent(EnteredCellEvent E)
		{
			Flight.CheckFlight(this.ParentObject, E.Object, this);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A5F RID: 39519 RVA: 0x003805C8 File Offset: 0x0037E7C8
		public virtual void OnImplanted(GameObject Object)
		{
			this._User = Object;
			this.RegisterImplantee(Object, EventRegistrar.Get(this.ParentObject, this));
			Flight.AbilitySetup(this.ParentObject, Object, this);
			CarryingCapacityChangedEvent.Send(Object);
		}

		// Token: 0x06009A60 RID: 39520 RVA: 0x003805F8 File Offset: 0x0037E7F8
		public virtual void OnUnimplanted(GameObject Object)
		{
			this._User = null;
			this.RegisterImplantee(Object, EventUnregistrar.Get(this.ParentObject, this));
			Object.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			Flight.FailFlying(this.ParentObject, Object, this);
			Flight.AbilityTeardown(this.ParentObject, Object, this);
			CarryingCapacityChangedEvent.Send(Object);
		}

		// Token: 0x06009A61 RID: 39521 RVA: 0x0038064E File Offset: 0x0037E84E
		public virtual void RegisterImplantee(GameObject Implantee, IEventRegistrar Registrar)
		{
			Registrar.Register(Implantee, EnteredCellEvent.ID, 0, false);
			Registrar.Register(Implantee, GetMaxCarriedWeightEvent.ID, 0, false);
			Registrar.Register(Implantee, GetTinkeringBonusEvent.ID, 0, false);
		}

		// Token: 0x06009A62 RID: 39522 RVA: 0x0038067C File Offset: 0x0037E87C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
			GameObject implantee = this.ParentObject.Implantee;
			if (implantee.IsValid())
			{
				this.RegisterImplantee(implantee, Registrar);
			}
		}

		// Token: 0x06009A63 RID: 39523 RVA: 0x003806AD File Offset: 0x0037E8AD
		public virtual void Activate(GameObject Actor)
		{
			Actor.CooldownActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(Actor), null, false);
		}

		// Token: 0x06009A64 RID: 39524 RVA: 0x003806C5 File Offset: 0x0037E8C5
		public int GetLevel(GameObject Actor = null)
		{
			return GetAvailableComputePowerEvent.AdjustUp(Actor ?? this.User, this.BaseLevel);
		}

		// Token: 0x06009A65 RID: 39525 RVA: 0x003806DD File Offset: 0x0037E8DD
		public int GetCooldown(GameObject Actor = null)
		{
			return GetAvailableComputePowerEvent.AdjustDown(Actor ?? this.User, this.BaseCooldown, 2);
		}

		// Token: 0x04004037 RID: 16439
		public const string ABL_CMD = "CommandActivateCathedra";

		// Token: 0x04004038 RID: 16440
		public const string ABL_CLASS = "Cybernetics";

		// Token: 0x04004039 RID: 16441
		public int BaseLevel = 10;

		// Token: 0x0400403A RID: 16442
		public int BaseCooldown = 100;

		// Token: 0x0400403B RID: 16443
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x0400403E RID: 16446
		[NonSerialized]
		private GameObject _User;
	}
}
