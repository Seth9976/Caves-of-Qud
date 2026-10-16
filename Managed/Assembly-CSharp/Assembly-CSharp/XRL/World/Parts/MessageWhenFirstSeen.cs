using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F6C RID: 3948
	[Serializable]
	public class MessageWhenFirstSeen : IPart
	{
		// Token: 0x06009D23 RID: 40227 RVA: 0x0038E670 File Offset: 0x0038C870
		public override bool SameAs(IPart p)
		{
			MessageWhenFirstSeen messageWhenFirstSeen = p as MessageWhenFirstSeen;
			return !(messageWhenFirstSeen.Message != this.Message) && !(messageWhenFirstSeen.Mode != this.Mode) && base.SameAs(p);
		}

		// Token: 0x06009D24 RID: 40228 RVA: 0x0038E6B8 File Offset: 0x0038C8B8
		public override bool Render(RenderEvent E)
		{
			if (!this.Seen)
			{
				this.Seen = true;
				string text = GameText.VariableReplace(this.Message, this.ParentObject, null, true);
				if (this.Mode == "Popup")
				{
					Popup.ShowSpace(text, null, "Sounds/UI/ui_notification", null, true, true, null);
				}
				if (this.Mode == "Log")
				{
					IComponent<GameObject>.AddPlayerMessage(text, null, true);
				}
			}
			return base.Render(E);
		}

		// Token: 0x040040EE RID: 16622
		public string Message = "[unset]";

		// Token: 0x040040EF RID: 16623
		public bool Seen;

		// Token: 0x040040F0 RID: 16624
		public string Mode = "Log";
	}
}
