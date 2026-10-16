using System;

namespace XRL.UI
{
	// Token: 0x02001495 RID: 5269
	public class GritGateTerminalScreenWhoAreYou : GritGateTerminalScreen
	{
		// Token: 0x0600CF19 RID: 53017 RVA: 0x004B65C0 File Offset: 0x004B47C0
		public GritGateTerminalScreenWhoAreYou()
		{
			this.MainText = "I am Ereshkigal, your liaison with the Thin World. Ask of me.";
			this.Options.Add("Are you sapient?");
			this.Options.Add("What is the Thin World?");
			this.Options.Add("Deliver me a bit of knowledge from the Thin World.");
			this.Options.Add("Exit.");
		}

		// Token: 0x0600CF1A RID: 53018 RVA: 0x004B661E File Offset: 0x004B481E
		public override void Back()
		{
			base.Terminal.CurrentScreen = new GritGateTerminalScreenBasicAccess();
		}

		// Token: 0x0600CF1B RID: 53019 RVA: 0x004B6630 File Offset: 0x004B4830
		public override void Activate()
		{
			if (base.Terminal.Selected == 0)
			{
				base.Terminal.CurrentScreen = new GritGateTerminalScreenSapient();
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
