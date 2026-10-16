using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010C8 RID: 4296
	[Serializable]
	public class RegenMedication : IPart
	{
		// Token: 0x0600AAF8 RID: 43768 RVA: 0x003E898B File Offset: 0x003E6B8B
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600AAF9 RID: 43769 RVA: 0x003E898E File Offset: 0x003E6B8E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600AAFA RID: 43770 RVA: 0x003E89A4 File Offset: 0x003E6BA4
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Apply")
			{
				E.Actor.FireEvent(Event.New("Regenera", "Level", 10, "Source", this.ParentObject));
				if (E.Actor.IsTrueKin())
				{
					RegenerateLimbEvent.Send(E.Actor, E.Actor, this.ParentObject, false, true, true, true, null, null, null, null, null);
					E.Actor.ApplyEffect(new Refresh(20), null);
				}
				else
				{
					RegenerateLimbEvent.Send(E.Actor, E.Actor, this.ParentObject, true, false, true, true, null, null, null, null, null);
				}
				if (E.Actor.IsPlayer())
				{
					Popup.Show(this.Message, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004679 RID: 18041
		public string Message = "The tube hisses against your skin, and a hot tingling infuses your whole body.";
	}
}
