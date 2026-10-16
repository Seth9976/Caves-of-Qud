using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F08 RID: 3848
	[Serializable]
	public class SupplyableIntegratedHost : IPart
	{
		// Token: 0x06009987 RID: 39303 RVA: 0x0037C1BD File Offset: 0x0037A3BD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06009988 RID: 39304 RVA: 0x0037C1DC File Offset: 0x0037A3DC
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Supply", "supply", "Supply", null, 's', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009989 RID: 39305 RVA: 0x0037C213 File Offset: 0x0037A413
		public override bool HandleEvent(InventoryActionEvent E)
		{
			return (!(E.Command == "Supply") || this.AttemptSupply(E.Actor)) && base.HandleEvent(E);
		}

		// Token: 0x0600998A RID: 39306 RVA: 0x0037C23E File Offset: 0x0037A43E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUseEarly");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600998B RID: 39307 RVA: 0x0037C260 File Offset: 0x0037A460
		public bool AttemptSupply(GameObject owner)
		{
			Event @event = Event.New("SupplyIntegratedHostWithAmmo", 0, 0, 0);
			@event.SetParameter("Host", this.ParentObject);
			@event.SetParameter("Owner", owner);
			@event.SetFlag("TrackSupply", true);
			if (this.ParentObject.FireEventOnBodyparts(@event))
			{
				if (!@event.HasFlag("AnySupplyHandler"))
				{
					Popup.Show(this.ParentObject.The + this.ParentObject.ShortDisplayName + this.ParentObject.GetVerb("need", true, false, false, true, false) + " no supplies.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else if (!@event.HasFlag("AnySupplies"))
				{
					Popup.Show(string.Concat(new string[]
					{
						"You have no supplies that ",
						this.ParentObject.the,
						this.ParentObject.ShortDisplayName,
						this.ParentObject.GetVerb("need", true, false, false, true, false),
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					base.PlayWorldSound(this.Sound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return true;
		}

		// Token: 0x0600998C RID: 39308 RVA: 0x0037C3AC File Offset: 0x0037A5AC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanSmartUse")
			{
				return false;
			}
			if (E.ID == "CommandSmartUseEarly")
			{
				if (this.ParentObject.HasPart<ConversationScript>())
				{
					return true;
				}
				if (this.AttemptSupply(E.GetGameObjectParameter("User")))
				{
					return false;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003FFB RID: 16379
		public string Sound;
	}
}
