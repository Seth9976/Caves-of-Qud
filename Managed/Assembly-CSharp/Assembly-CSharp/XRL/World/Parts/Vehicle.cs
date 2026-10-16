using System;
using System.Collections.Generic;
using System.Linq;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020012B1 RID: 4785
	[Serializable]
	public class Vehicle : IActivePart
	{
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600BD13 RID: 48403 RVA: 0x00453775 File Offset: 0x00451975
		// (set) Token: 0x0600BD14 RID: 48404 RVA: 0x00453783 File Offset: 0x00451983
		public bool Autonomous
		{
			get
			{
				return this.Flags.HasBit(1);
			}
			set
			{
				(ref this.Flags).SetBit(1, value);
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600BD15 RID: 48405 RVA: 0x00453792 File Offset: 0x00451992
		// (set) Token: 0x0600BD16 RID: 48406 RVA: 0x004537A0 File Offset: 0x004519A0
		public bool Follower
		{
			get
			{
				return this.Flags.HasBit(2);
			}
			set
			{
				(ref this.Flags).SetBit(2, value);
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600BD17 RID: 48407 RVA: 0x004537AF File Offset: 0x004519AF
		// (set) Token: 0x0600BD18 RID: 48408 RVA: 0x004537BD File Offset: 0x004519BD
		public bool Tracker
		{
			get
			{
				return this.Flags.HasBit(4);
			}
			set
			{
				(ref this.Flags).SetBit(4, value);
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x0600BD19 RID: 48409 RVA: 0x004537CC File Offset: 0x004519CC
		// (set) Token: 0x0600BD1A RID: 48410 RVA: 0x004537DA File Offset: 0x004519DA
		public bool InheritParty
		{
			get
			{
				return this.Flags.HasBit(8);
			}
			set
			{
				(ref this.Flags).SetBit(8, value);
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600BD1B RID: 48411 RVA: 0x004537E9 File Offset: 0x004519E9
		// (set) Token: 0x0600BD1C RID: 48412 RVA: 0x004537F8 File Offset: 0x004519F8
		public bool ExitAbility
		{
			get
			{
				return this.Flags.HasBit(16);
			}
			set
			{
				(ref this.Flags).SetBit(16, value);
			}
		}

		// Token: 0x0600BD1D RID: 48413 RVA: 0x00453808 File Offset: 0x00451A08
		public Vehicle()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "PrimeMover";
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600BD1E RID: 48414 RVA: 0x00453834 File Offset: 0x00451A34
		public Interior Interior
		{
			get
			{
				Interior interior;
				if ((interior = this._Interior) == null)
				{
					interior = (this._Interior = this.ParentObject.GetPart<Interior>());
				}
				return interior;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600BD1F RID: 48415 RVA: 0x00453860 File Offset: 0x00451A60
		// (set) Token: 0x0600BD20 RID: 48416 RVA: 0x00453970 File Offset: 0x00451B70
		public GameObject Pilot
		{
			get
			{
				if (!this.Failed && !GameObject.Validate(ref this._Pilot) && !this.PilotID.IsNullOrEmpty())
				{
					if (this._Pilot == null)
					{
						this._Pilot = FindObjectByIdEvent.Find(this.ParentObject, this.PilotID);
					}
					if (this._Pilot == null)
					{
						this._Pilot = The.ZoneManager.peekCachedObject(this.PilotID);
					}
					if (this._Pilot == null && this.Interior != null)
					{
						InteriorZone zone = this.Interior.Zone;
						this._Pilot = ((zone != null) ? zone.FindObjectByID(this.PilotID) : null);
					}
					if (this._Pilot == null)
					{
						this._Pilot = The.ZoneManager.FindObjectByID(this.PilotID);
					}
					if (this._Pilot != null)
					{
						Piloting piloting;
						if (!this._Pilot.TryGetEffect<Piloting>(out piloting) || piloting.Vehicle != this.ParentObject)
						{
							this._Pilot = null;
							this.PilotID = null;
							this.CheckPenalty(null);
						}
					}
					else
					{
						this.Failed = true;
					}
				}
				return this._Pilot;
			}
			set
			{
				GameObject gameObject = this.Pilot;
				if (gameObject == value)
				{
					return;
				}
				if (value != null && !this.CanBePilotedBy(value))
				{
					return;
				}
				if (!BeforePilotChangeEvent.Check(this.ParentObject, value, gameObject))
				{
					return;
				}
				try
				{
					if (this.InheritParty)
					{
						Zone currentZone = this.ParentObject.CurrentZone;
						if (currentZone != null)
						{
							foreach (GameObject gameObject2 in currentZone.IterateObjects())
							{
								if (gameObject2.IsCombatObject() && gameObject2 != this.ParentObject)
								{
									GameObject partyLeader = gameObject2.PartyLeader;
									if (partyLeader != null)
									{
										if (partyLeader == this.ParentObject)
										{
											gameObject2.PartyLeader = gameObject;
										}
										else if (partyLeader == value)
										{
											gameObject2.PartyLeader = this.ParentObject;
										}
									}
								}
							}
						}
					}
					if (this.ParentObject.IsPlayer())
					{
						if (gameObject == null)
						{
							gameObject = The.ZoneManager.FindObjectByID("OriginalPlayer");
							if (gameObject == null)
							{
								return;
							}
						}
						The.Game.Player.Body = gameObject;
						if (this.Follower)
						{
						}
					}
					else if (value != null && value.IsPlayer())
					{
						The.Game.Player.Body = this.ParentObject;
						The.Player.MakeActive(false);
						this.CheckAbility();
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("VehiclePilotEffects", ex, "game_exception");
				}
				if (gameObject != null)
				{
					gameObject.RemoveEffect(typeof(Piloting));
				}
				if (value != null)
				{
					value.ApplyEffect(new Piloting(this.ParentObject), null);
				}
				this._Pilot = value;
				this.PilotID = ((value != null) ? value.ID : null);
				this.Failed = false;
				this.CheckPenalty(value);
				AfterPilotChangeEvent.Send(this.ParentObject, value, gameObject);
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x0600BD21 RID: 48417 RVA: 0x00453B20 File Offset: 0x00451D20
		public VehicleRecord Record
		{
			get
			{
				if (!this.Tracker)
				{
					return null;
				}
				if (this._Record == null && !VehicleRecord.All.TryGetValue(this.ParentObject.ID, out this._Record) && !this.ParentObject.IsTemporary)
				{
					Dictionary<string, VehicleRecord> all = VehicleRecord.All;
					string id = this.ParentObject.ID;
					VehicleRecord vehicleRecord = new VehicleRecord();
					vehicleRecord.ID = this.ParentObject.ID;
					vehicleRecord.OwnerID = this.OwnerID;
					vehicleRecord.Blueprint = this.ParentObject.Blueprint;
					Cell currentCell = this.ParentObject.CurrentCell;
					vehicleRecord.Location = ((currentCell != null) ? currentCell.GetGlobalLocation() : null) ?? new GlobalLocation();
					vehicleRecord.Type = this.Type;
					VehicleRecord vehicleRecord2 = vehicleRecord;
					this._Record = vehicleRecord;
					all[id] = vehicleRecord2;
				}
				return this._Record;
			}
		}

		// Token: 0x0600BD22 RID: 48418 RVA: 0x00453BFB File Offset: 0x00451DFB
		public override void Initialize()
		{
			this.CheckPenalty(this.Pilot);
		}

		// Token: 0x0600BD23 RID: 48419 RVA: 0x00453C09 File Offset: 0x00451E09
		public override IPart DeepCopy(GameObject Parent)
		{
			Vehicle vehicle = (Vehicle)base.DeepCopy(Parent);
			vehicle.PilotID = null;
			vehicle.Tracker = false;
			return vehicle;
		}

		// Token: 0x0600BD24 RID: 48420 RVA: 0x00453C28 File Offset: 0x00451E28
		public virtual bool IsOperational()
		{
			return base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && IsVehicleOperationalEvent.Check(this.ParentObject);
		}

		// Token: 0x0600BD25 RID: 48421 RVA: 0x00453C6A File Offset: 0x00451E6A
		public bool CanBePilotedBy(GameObject Actor)
		{
			return Actor.Brain != null && ((this.OwnerID.IsNullOrEmpty() && this.BindBlueprint.IsNullOrEmpty()) || this.IsOwnedBy(Actor));
		}

		// Token: 0x0600BD26 RID: 48422 RVA: 0x00453C99 File Offset: 0x00451E99
		public bool IsOwnedBy(GameObject Actor)
		{
			if (Actor.IsPlayer())
			{
				return this.OwnerID == "Player";
			}
			return Actor.IDMatch(this.OwnerID);
		}

		// Token: 0x0600BD27 RID: 48423 RVA: 0x00453CC0 File Offset: 0x00451EC0
		public void CheckAbility()
		{
			if (this.ExitAbility && this.AbilityID == Guid.Empty)
			{
				this.AbilityID = this.ParentObject.AddActivatedAbility("Exit pilot seat", "CommandReleaseVehicle", "Vehicle", null, "÷", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null, null);
			}
		}

		// Token: 0x0600BD28 RID: 48424 RVA: 0x00453D20 File Offset: 0x00451F20
		public void CheckPenalty(GameObject Actor)
		{
			if (Actor != null)
			{
				this.ParentObject.RemoveEffect(typeof(Unpiloted));
				return;
			}
			if (this.ParentObject.Brain != null && !this.ParentObject.HasEffect(typeof(Unpiloted)))
			{
				this.ParentObject.ApplyEffect(new Unpiloted(100, this.Autonomous), null);
			}
		}

		// Token: 0x0600BD29 RID: 48425 RVA: 0x00453D85 File Offset: 0x00451F85
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BD2A RID: 48426 RVA: 0x00453D88 File Offset: 0x00451F88
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandReleaseVehicle");
		}

		// Token: 0x0600BD2B RID: 48427 RVA: 0x00453D95 File Offset: 0x00451F95
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandReleaseVehicle")
			{
				this.Pilot = null;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BD2C RID: 48428 RVA: 0x00453DB8 File Offset: 0x00451FB8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == AfterDieEvent.ID || ID == ObjectCreatedEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || (this.Tracker && ID == WasReplicatedEvent.ID) || (this.Tracker && ID == BeforeDestroyObjectEvent.ID) || (this.Tracker && ID == EnteredCellEvent.ID);
		}

		// Token: 0x0600BD2D RID: 48429 RVA: 0x00453E2C File Offset: 0x0045202C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.IsPlayer())
			{
				E.AddAction("Exit", "exit", "ReleaseVehicle", null, 'x', false, 0, 0, false, false, false, false, true, null, false);
			}
			if (this.ParentObject.IsPlayerControlled())
			{
				E.AddAction("CompanionEnterVehicle", "direct follower to enter", "CompanionEnterVehicle", null, 'f', false, 1000, 0, false, true, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD2E RID: 48430 RVA: 0x00453EA4 File Offset: 0x004520A4
		public bool IsValidPassenger(GameObject Object)
		{
			return Object.PartyLeader == The.Player && Object != this.ParentObject && Object.IsMobile() && !Object.HasPart<Vehicle>();
		}

		// Token: 0x0600BD2F RID: 48431 RVA: 0x00453ED0 File Offset: 0x004520D0
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ReleaseVehicle")
			{
				if (this.ParentObject.OnWorldMap())
				{
					return this.ParentObject.Fail("You cannot do that on the world map.");
				}
				this.Pilot = null;
				E.RequestInterfaceExit();
			}
			else if (E.Command == "CompanionEnterVehicle")
			{
				List<GameObject> list = Event.NewGameObjectList(The.ActiveZone.YieldObjects().Where<GameObject>(new Func<GameObject, bool>(this.IsValidPassenger)));
				if (list.Count == 0)
				{
					return E.Actor.Fail("You have no followers that can enter " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				GameObject gameObject = Popup.PickGameObject("Choose a follower", list, true, false, true, null, 0, false);
				if (gameObject == null)
				{
					return false;
				}
				if (this.Interior != null && !this.Interior.CanEnter(gameObject, false, true))
				{
					return false;
				}
				gameObject.Brain.Goals.Clear();
				gameObject.Brain.Staying = true;
				AIPassenger aipassenger = new AIPassenger(this.ParentObject, true);
				gameObject.AddPart<AIPassenger>(aipassenger, true, false);
				aipassenger.CheckPassengerSeat();
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD30 RID: 48432 RVA: 0x0045401C File Offset: 0x0045221C
		public override bool HandleEvent(AfterDieEvent E)
		{
			this.ParentObject.PullDown(false);
			GameObject pilot = this.Pilot;
			if (pilot != null)
			{
				this.Pilot = null;
				if (pilot.CurrentCell == null && pilot.HasHitpoints())
				{
					pilot.SystemMoveTo(this.ParentObject.CurrentCell, new int?(0), true, true, false, false);
					GameObject gameObject = pilot;
					Interior interior = this.Interior;
					StairsDown.InflictFallDamage(gameObject, (interior != null) ? interior.FallDistance : 0, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD31 RID: 48433 RVA: 0x00454094 File Offset: 0x00452294
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			if (this.Tracker && !E.Obliterate && !this.ParentObject.IsPlayer())
			{
				this.ParentObject.Physics.TeardownForDestroy(false, true);
				The.ZoneManager.CachedObjects[this.ParentObject.ID] = this.ParentObject;
				foreach (Effect effect in this.ParentObject.Effects)
				{
					if (effect.IsOfType(67108864))
					{
						effect.Duration = 0;
					}
				}
				this.ParentObject.CleanEffects();
				this.ParentObject.ApplyEffect(new Invulnerable
				{
					Duration = 3
				}, null);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD32 RID: 48434 RVA: 0x00454180 File Offset: 0x00452380
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (activePartStatus != ActivePartStatus.Unpowered)
				{
					return false;
				}
				if (!this.ParentObject.HasEffect(typeof(VehicleUnpowered)))
				{
					this.ParentObject.ApplyEffect(new VehicleUnpowered(Math.Max(this.ChargeUse, this.ChargeMinimum)), null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD33 RID: 48435 RVA: 0x00454201 File Offset: 0x00452401
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.CheckPenalty(this.Pilot);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD34 RID: 48436 RVA: 0x00454216 File Offset: 0x00452416
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.Tracker)
			{
				VehicleRecord record = this.Record;
				if (record != null)
				{
					record.Location.SetCell(E.Cell);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD35 RID: 48437 RVA: 0x00454244 File Offset: 0x00452444
		public override bool HandleEvent(WasReplicatedEvent E)
		{
			Vehicle vehicle;
			if (this.Tracker && !E.Temporary && E.Replica.TryGetPart<Vehicle>(out vehicle))
			{
				vehicle.Tracker = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD36 RID: 48438 RVA: 0x0045427E File Offset: 0x0045247E
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600BD37 RID: 48439 RVA: 0x00454284 File Offset: 0x00452484
		public override void TurnTick(long TimeTick, int Amount)
		{
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
		}

		// Token: 0x0600BD38 RID: 48440 RVA: 0x004542B8 File Offset: 0x004524B8
		public static GameObject CreateOwnedBy(string Blueprint, GameObject Owner, bool? Follower = null, bool? Tracker = null, bool Clear = true)
		{
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(Blueprint);
			Vehicle part = gameObject.GetPart<Vehicle>();
			part.OwnerID = (Owner.IsPlayer() ? "Player" : Owner.ID);
			if (Follower != null)
			{
				part.Follower = Follower.Value;
			}
			if (Tracker != null)
			{
				part.Tracker = Tracker.Value;
			}
			if (Clear)
			{
				gameObject.Inventory.Clear();
			}
			return gameObject;
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x0600BD39 RID: 48441 RVA: 0x0045432F File Offset: 0x0045252F
		// (set) Token: 0x0600BD3A RID: 48442 RVA: 0x00454337 File Offset: 0x00452537
		[Obsolete("Use PilotID")]
		public string DriverID
		{
			get
			{
				return this.PilotID;
			}
			set
			{
				this.PilotID = value;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x0600BD3B RID: 48443 RVA: 0x00454340 File Offset: 0x00452540
		// (set) Token: 0x0600BD3C RID: 48444 RVA: 0x00454348 File Offset: 0x00452548
		[Obsolete("Use Pilot")]
		public GameObject Driver
		{
			get
			{
				return this.Pilot;
			}
			set
			{
				this.Pilot = value;
			}
		}

		// Token: 0x04004BC0 RID: 19392
		public const int FLAG_AUTONOMOUS = 1;

		// Token: 0x04004BC1 RID: 19393
		public const int FLAG_FOLLOWER = 2;

		// Token: 0x04004BC2 RID: 19394
		public const int FLAG_TRACKER = 4;

		// Token: 0x04004BC3 RID: 19395
		public const int FLAG_INHERIT_PARTY = 8;

		// Token: 0x04004BC4 RID: 19396
		public const int FLAG_EXIT_ABILITY = 16;

		// Token: 0x04004BC5 RID: 19397
		public const string ABL_CMD = "CommandReleaseVehicle";

		// Token: 0x04004BC6 RID: 19398
		public string Type;

		// Token: 0x04004BC7 RID: 19399
		public string BindBlueprint;

		// Token: 0x04004BC8 RID: 19400
		public string OwnerID;

		// Token: 0x04004BC9 RID: 19401
		public string PilotID;

		// Token: 0x04004BCA RID: 19402
		public Guid AbilityID;

		// Token: 0x04004BCB RID: 19403
		public int Passengers = 2;

		// Token: 0x04004BCC RID: 19404
		public int Flags = 16;

		// Token: 0x04004BCD RID: 19405
		[NonSerialized]
		private Interior _Interior;

		// Token: 0x04004BCE RID: 19406
		[NonSerialized]
		private bool Failed;

		// Token: 0x04004BCF RID: 19407
		[NonSerialized]
		private GameObject _Pilot;

		// Token: 0x04004BD0 RID: 19408
		[NonSerialized]
		private VehicleRecord _Record;
	}
}
