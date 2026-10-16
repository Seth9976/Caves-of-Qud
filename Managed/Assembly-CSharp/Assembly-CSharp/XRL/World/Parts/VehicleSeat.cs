using System;
using XRL.Language;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012BA RID: 4794
	[Serializable]
	public class VehicleSeat : IPart
	{
		// Token: 0x0600BD71 RID: 48497 RVA: 0x00455B4E File Offset: 0x00453D4E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BD72 RID: 48498 RVA: 0x00455B51 File Offset: 0x00453D51
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeingSatOn");
		}

		// Token: 0x0600BD73 RID: 48499 RVA: 0x00455B5E File Offset: 0x00453D5E
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeingSatOn")
			{
				this.AttemptPilot(E.GetGameObjectParameter("Object"));
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BD74 RID: 48500 RVA: 0x00455B8C File Offset: 0x00453D8C
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
			if (!vehicle.IsOperational())
			{
				return Object.ShowFailure(vehicle.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " is not operational.");
			}
			if (!vehicle.CanBePilotedBy(Object))
			{
				if (!Object.IsPlayer() || vehicle.BindBlueprint.IsNullOrEmpty())
				{
					return Object.ShowFailure(vehicle.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " is not bound to you.");
				}
				GameObject gameObject = Object.FindObjectInInventory(vehicle.BindBlueprint);
				if (gameObject != null)
				{
					if (Popup.ShowYesNo("Accessing the pilot console requires the permanent insertion of " + gameObject.an(2147483647, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".\n\nAre you sure you want to proceed?", "Sounds/UI/ui_notification", true, DialogResult.No, null) != DialogResult.Yes)
					{
						return false;
					}
					gameObject.Destroy(null, false, false, null);
					vehicle.OwnerID = (Object.IsPlayer() ? "Player" : Object.ID);
					Popup.Show("Access diodes flash in the affirmative.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					string text = GameObjectFactory.Factory.GetBlueprint(vehicle.BindBlueprint).DisplayName();
					Popup.Show("Accessing the pilot console requires the permanent insertion of " + Grammar.A(text, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			vehicle.Pilot = Object;
			return true;
		}
	}
}
