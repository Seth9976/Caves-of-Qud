using System;

namespace XRL.World.Parts
{
	// Token: 0x020012B3 RID: 4787
	[Serializable]
	public class VehicleControl : IPart
	{
		// Token: 0x0600BD41 RID: 48449 RVA: 0x0045456C File Offset: 0x0045276C
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID;
		}

		// Token: 0x0600BD42 RID: 48450 RVA: 0x00454590 File Offset: 0x00452790
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Control", "control", "VehicleControl", null, 'c', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD43 RID: 48451 RVA: 0x004545C7 File Offset: 0x004527C7
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "VehicleControl" && this.AttemptPilot(E.Actor))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD44 RID: 48452 RVA: 0x004545F6 File Offset: 0x004527F6
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x0600BD45 RID: 48453 RVA: 0x0045460E File Offset: 0x0045280E
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			this.AttemptPilot(E.Actor);
			return false;
		}

		// Token: 0x0600BD46 RID: 48454 RVA: 0x00454620 File Offset: 0x00452820
		public bool AttemptPilot(GameObject Object)
		{
			InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
			Interior interior;
			if (interiorZone == null)
			{
				interior = null;
			}
			else
			{
				GameObject parentObject = interiorZone.ParentObject;
				interior = ((parentObject != null) ? parentObject.GetPart<Interior>() : null);
			}
			Interior interior2 = interior;
			if (interior2 != null && interior2.HasRequired(this.ParentObject.Blueprint) && interior2.GetRequired(this.ParentObject) < 0)
			{
				return false;
			}
			Vehicle vehicle = ((interiorZone != null) ? interiorZone.ParentObject.GetPart<Vehicle>() : null);
			if (vehicle == null)
			{
				return false;
			}
			if (vehicle.Pilot != null)
			{
				return Object.ShowFailure(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " is already in use by " + vehicle.Pilot.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
			}
			vehicle.Pilot = Object;
			return true;
		}
	}
}
