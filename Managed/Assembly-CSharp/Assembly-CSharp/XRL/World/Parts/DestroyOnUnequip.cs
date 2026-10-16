using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001017 RID: 4119
	[Serializable]
	public class DestroyOnUnequip : IPart
	{
		// Token: 0x0600A41E RID: 42014 RVA: 0x003C05AC File Offset: 0x003BE7AC
		public override bool SameAs(IPart p)
		{
			return !((p as DestroyOnUnequip).Message != this.Message) && base.SameAs(p);
		}

		// Token: 0x0600A41F RID: 42015 RVA: 0x003C05CF File Offset: 0x003BE7CF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeginBeingUnequippedEvent>.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A420 RID: 42016 RVA: 0x003C05F0 File Offset: 0x003BE7F0
		public override bool HandleEvent(UnequippedEvent E)
		{
			if (E.Actor.IsPlayer() && !this.Message.IsNullOrEmpty())
			{
				Popup.Show(GameText.VariableReplace(this.Message, this.ParentObject, null, true), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.ParentObject.Destroy(null, false, false, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A421 RID: 42017 RVA: 0x003C0654 File Offset: 0x003BE854
		public override bool HandleEvent(BeginBeingUnequippedEvent E)
		{
			if (E.DestroyOnUnequipDeclined)
			{
				return false;
			}
			if (E.AutoEquipTry > 0)
			{
				GameObject actor = E.Actor;
				if (actor != null && actor.IsPlayer() && Popup.ShowYesNoCancel(this.ParentObject.T(2147483647, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " will be destroyed if " + this.ParentObject.itis + " unequipped. Do you want to continue?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
				{
					E.DestroyOnUnequipDeclined = true;
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004401 RID: 17409
		public string Message = "The mote of light is extinguished.";
	}
}
