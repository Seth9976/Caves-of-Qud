using System;

namespace XRL.UI
{
	// Token: 0x02001493 RID: 5267
	public class GritGateTerminalScreenGoodbye : GritGateTerminalScreen
	{
		// Token: 0x0600CF13 RID: 53011 RVA: 0x004B6492 File Offset: 0x004B4692
		public GritGateTerminalScreenGoodbye()
		{
			this.MainText = "Access denied. Continue on your path through the Thick World.";
			this.Options.Add("...");
		}

		// Token: 0x0600CF14 RID: 53012 RVA: 0x004B64B5 File Offset: 0x004B46B5
		public override void Back()
		{
			base.Terminal.CurrentScreen = null;
		}

		// Token: 0x0600CF15 RID: 53013 RVA: 0x004B64C3 File Offset: 0x004B46C3
		public override void Activate()
		{
			base.Terminal.CurrentScreen = null;
		}
	}
}
