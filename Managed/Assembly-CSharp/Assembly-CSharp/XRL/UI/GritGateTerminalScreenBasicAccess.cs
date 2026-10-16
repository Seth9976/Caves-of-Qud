using System;

namespace XRL.UI
{
	// Token: 0x02001494 RID: 5268
	public class GritGateTerminalScreenBasicAccess : GritGateTerminalScreen
	{
		// Token: 0x0600CF16 RID: 53014 RVA: 0x004B64D4 File Offset: 0x004B46D4
		public GritGateTerminalScreenBasicAccess()
		{
			this.MainText = "Basic user access grant. What do you wish from the Thin World?";
			this.Options.Add("Who are you?");
			this.Options.Add("What is the Thin World?");
			this.Options.Add("Deliver me a bit of knowledge from the Thin World.");
			this.Options.Add("Exit.");
		}

		// Token: 0x0600CF17 RID: 53015 RVA: 0x004B6532 File Offset: 0x004B4732
		public override void Back()
		{
			base.Terminal.CurrentScreen = null;
		}

		// Token: 0x0600CF18 RID: 53016 RVA: 0x004B6540 File Offset: 0x004B4740
		public override void Activate()
		{
			if (base.Terminal.Selected == 0)
			{
				base.Terminal.CurrentScreen = new GritGateTerminalScreenWhoAreYou();
			}
			if (base.Terminal.Selected == 1)
			{
				base.Terminal.CurrentScreen = new GritGateTerminalScreenThinWorld();
			}
			if (base.Terminal.Selected == 2)
			{
				base.Terminal.CurrentScreen = new GritGateTerminalScreenKnowledge();
			}
			if (base.Terminal.Selected == 3)
			{
				base.Terminal.CurrentScreen = null;
			}
		}
	}
}
