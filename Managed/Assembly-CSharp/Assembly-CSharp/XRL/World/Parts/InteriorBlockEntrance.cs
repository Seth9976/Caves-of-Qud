using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F72 RID: 3954
	[Serializable]
	public class InteriorBlockEntrance : IPart
	{
		// Token: 0x06009D6A RID: 40298 RVA: 0x003909CB File Offset: 0x0038EBCB
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == SingletonEvent<CanEnterInteriorEvent>.ID;
		}

		// Token: 0x06009D6B RID: 40299 RVA: 0x003909D8 File Offset: 0x0038EBD8
		public override bool HandleEvent(CanEnterInteriorEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				if (E.Actor.IsPlayer() && !this.Message.IsNullOrEmpty())
				{
					Popup.Show(this.Message.StartReplace().AddObject(E.Actor, null).AddObject(E.Object, null)
						.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				E.Status = 30;
				E.ShowMessage = false;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400411C RID: 16668
		public string Message;
	}
}
