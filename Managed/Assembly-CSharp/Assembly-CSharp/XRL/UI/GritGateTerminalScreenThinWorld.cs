using System;

namespace XRL.UI
{
	// Token: 0x02001497 RID: 5271
	public class GritGateTerminalScreenThinWorld : GritGateTerminalScreen
	{
		// Token: 0x0600CF1F RID: 53023 RVA: 0x004B6774 File Offset: 0x004B4974
		public GritGateTerminalScreenThinWorld()
		{
			this.MainText = "The Thin World is where I traverse, as the Thick World is where you do. I've no domain in your world, and but for me, you've none in mine.";
			this.Options.Add("Who are you?");
			this.Options.Add("Deliver me a bit of knowledge from the Thin World.");
			this.Options.Add("Exit.");
		}

		// Token: 0x0600CF20 RID: 53024 RVA: 0x004B67C2 File Offset: 0x004B49C2
		public override void Back()
		{
			base.Terminal.CurrentScreen = new GritGateTerminalScreenBasicAccess();
		}

		// Token: 0x0600CF21 RID: 53025 RVA: 0x004B67D4 File Offset: 0x004B49D4
		public override void Activate()
		{
			if (base.Terminal.Selected == 0)
			{
				base.Terminal.CurrentScreen = new GritGateTerminalScreenWhoAreYou();
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
