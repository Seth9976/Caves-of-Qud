using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020010C6 RID: 4294
	[Serializable]
	public class HealMedication : IPart
	{
		// Token: 0x0600AAEF RID: 43759 RVA: 0x003E8764 File Offset: 0x003E6964
		public override bool SameAs(IPart p)
		{
			HealMedication healMedication = p as HealMedication;
			return !(healMedication.MutantHeal != this.MutantHeal) && !(healMedication.TrueManHeal != this.TrueManHeal) && !(healMedication.Message != this.Message) && base.SameAs(p);
		}

		// Token: 0x0600AAF0 RID: 43760 RVA: 0x003E87BE File Offset: 0x003E69BE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600AAF1 RID: 43761 RVA: 0x003E87D4 File Offset: 0x003E69D4
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Apply")
			{
				int num = (E.Actor.IsTrueKin() ? this.TrueManHeal : this.MutantHeal).RollCached();
				E.Actor.Heal(num, false, true, true);
				if (E.Actor.IsPlayer())
				{
					Popup.Show(this.Message, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004676 RID: 18038
		public string MutantHeal = "2d6";

		// Token: 0x04004677 RID: 18039
		public string TrueManHeal = "4d6";

		// Token: 0x04004678 RID: 18040
		public string Message = "The tube hisses against your skin, and a warm feeling washes over your body.";
	}
}
