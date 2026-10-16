using System;

namespace XRL.World.Parts
{
	// Token: 0x020012B9 RID: 4793
	[Serializable]
	public class VehicleRepair : IPoweredPart
	{
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600BD67 RID: 48487 RVA: 0x00455690 File Offset: 0x00453890
		public string VehicleTerm
		{
			get
			{
				if (this._VehicleTerm == null)
				{
					if (this.ParentObject.Property.TryGetValue("VehicleTerm", out this._VehicleTerm))
					{
						return this._VehicleTerm;
					}
					GameObjectBlueprint gameObjectBlueprint;
					if (!this.RequiredBlueprint.IsNullOrEmpty() && GameObjectFactory.Factory.Blueprints.TryGetValue(this.RequiredBlueprint, out gameObjectBlueprint))
					{
						this._VehicleTerm = gameObjectBlueprint.CachedDisplayNameStripped;
					}
					else
					{
						this._VehicleTerm = "vehicle";
					}
				}
				return this._VehicleTerm;
			}
		}

		// Token: 0x0600BD68 RID: 48488 RVA: 0x0045570E File Offset: 0x0045390E
		public VehicleRepair()
		{
			this.WorksOnAdjacentCellContents = true;
		}

		// Token: 0x0600BD69 RID: 48489 RVA: 0x0045571D File Offset: 0x0045391D
		public override bool WorksForEveryone()
		{
			return false;
		}

		// Token: 0x0600BD6A RID: 48490 RVA: 0x00455720 File Offset: 0x00453920
		public override bool WorksFor(GameObject Object)
		{
			return Object.Blueprint == this.AdjacentBlueprint;
		}

		// Token: 0x0600BD6B RID: 48491 RVA: 0x00455733 File Offset: 0x00453933
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEarlyEvent.ID;
		}

		// Token: 0x0600BD6C RID: 48492 RVA: 0x00455761 File Offset: 0x00453961
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x0600BD6D RID: 48493 RVA: 0x00455779 File Offset: 0x00453979
		public override bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				this.ParentObject.Twiddle(false);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD6E RID: 48494 RVA: 0x004557A0 File Offset: 0x004539A0
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("RepairVehicle", "repair " + this.VehicleTerm, "RepairVehicle", null, 'r', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD6F RID: 48495 RVA: 0x004557E4 File Offset: 0x004539E4
		public bool IsValidVehicle(GameObject Object)
		{
			Vehicle vehicle;
			return Object.TryGetPart<Vehicle>(out vehicle) && (this.RequiredBlueprint == null || Object.Blueprint == this.RequiredBlueprint) && (this.RequiredType == null || vehicle.Type == this.RequiredType);
		}

		// Token: 0x0600BD70 RID: 48496 RVA: 0x00455834 File Offset: 0x00453A34
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "RepairVehicle")
			{
				ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
				if (activePartStatus != ActivePartStatus.Operational)
				{
					if (activePartStatus == ActivePartStatus.Unpowered)
					{
						return E.Actor.ShowFailure(this.ParentObject.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " not have enough charge to operate.");
					}
					return E.Actor.ShowFailure(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " merely" + this.ParentObject.GetVerb("click", true, false, false, true, false) + ".");
				}
				else
				{
					GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
					if (activePartFirstSubject == null)
					{
						return false;
					}
					GameObject firstObject = activePartFirstSubject.CurrentCell.GetFirstObject(new Predicate<GameObject>(this.IsValidVehicle));
					if (firstObject == null)
					{
						return E.Actor.ShowFailure(string.Concat(new string[]
						{
							"There is no ",
							this.VehicleTerm,
							" on ",
							activePartFirstSubject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							"."
						}));
					}
					if (!IsRepairableEvent.Check(E.Actor, firstObject, this.ParentObject, null, null) && this.ParentObject.Body.DismemberedParts.IsNullOrEmpty<Body.DismemberedPart>() && (this.ParentObject.HasPart(typeof(Stomach)) || this.ParentObject.GetPercentDamaged() == 0))
					{
						return E.Actor.ShowFailure(firstObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " in no need of repairs.");
					}
					if (!E.Actor.UseDrams(1, "sunslag", null, null, null, null, false))
					{
						return E.Actor.ShowFailure("You do not have 1 dram of " + LiquidVolume.GetLiquid("sunslag").GetName() + ".");
					}
					if (base.ConsumeCharge(null, null))
					{
						E.Actor.ShowSuccess("Claystuff is reprinted across the fissures and scars on " + firstObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true);
						E.RequestInterfaceExit();
						firstObject.RestorePristineHealth(true, false);
						firstObject.FireEvent(Event.New("Regenera", "Level", 10, "Source", this.ParentObject));
						firstObject.DustPuff("&y", 15);
						RepairedEvent.Send(E.Actor, firstObject, this.ParentObject, null, 0);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004BEB RID: 19435
		public string AdjacentBlueprint;

		// Token: 0x04004BEC RID: 19436
		public string RequiredBlueprint;

		// Token: 0x04004BED RID: 19437
		public string RequiredType;

		// Token: 0x04004BEE RID: 19438
		[NonSerialized]
		private string _VehicleTerm;
	}
}
