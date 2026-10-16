using System;

namespace XRL.UI
{
	// Token: 0x02001496 RID: 5270
	public class GritGateTerminalScreenSapient : GritGateTerminalScreen
	{
		// Token: 0x0600CF1C RID: 53020 RVA: 0x004B66B0 File Offset: 0x004B48B0
		public GritGateTerminalScreenSapient()
		{
			this.MainText = "Our respective ways of reckoning that inquiry are irreconcilable. I have no answer.";
			this.Options.Add("What is the Thin World?");
			this.Options.Add("Deliver me a bit of knowledge from the Thin World.");
			this.Options.Add("Exit.");
		}

		// Token: 0x0600CF1D RID: 53021 RVA: 0x004B66FE File Offset: 0x004B48FE
		public override void Back()
		{
			base.Terminal.CurrentScreen = new GritGateTerminalScreenBasicAccess();
		}

		// Token: 0x0600CF1E RID: 53022 RVA: 0x004B6710 File Offset: 0x004B4910
		public override void Activate()
		{
			if (base.Terminal.Selected == 0)
			{
				base.Terminal.CurrentScreen = new GritGateTerminalScreenThinWorld();
			}
			if (base.Terminal.Selected == 1)
			{
				base.Terminal.CurrentScreen = new GritGateTerminalScreenKnowledge();
			}
			if (base.Terminal.Selected == 2)
			{
				base.Terminal.CurrentScreen = null;
			}
		}
	}
}
