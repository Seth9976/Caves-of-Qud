using System;
using XRL.Core;
using XRL.Messages;

namespace XRL.UI
{
	// Token: 0x0200149A RID: 5274
	public class GritGateTerminalScreenMessage : GritGateTerminalScreen
	{
		// Token: 0x0600CF30 RID: 53040 RVA: 0x004B6E04 File Offset: 0x004B5004
		public GritGateTerminalScreenMessage(string message, GritGateTerminalScreen LastScreen, Action After = null)
		{
			this.LastScreen = LastScreen;
			this.After = After;
			this.MainText = message;
			this.ClearOptions();
			this.Options.Add("Exit.");
			this.optionActions.Add(delegate
			{
				MessageQueue.AddPlayerMessage("Alarms blare across the enclave.", null, true);
				XRLCore.Core.Game.FinishQuestStep("Grave Thoughts", "Investigate the Rumbling", -1, true, null);
				base.Terminal.CurrentScreen = null;
			});
		}

		// Token: 0x0600CF31 RID: 53041 RVA: 0x004B6E59 File Offset: 0x004B5059
		public override void Back()
		{
			base.Terminal.CurrentScreen = this.LastScreen;
			if (this.After != null)
			{
				this.After();
			}
		}

		// Token: 0x0600CF32 RID: 53042 RVA: 0x004B6E7F File Offset: 0x004B507F
		public override void Activate()
		{
			this.optionActions[base.Terminal.Selected]();
		}

		// Token: 0x0400506D RID: 20589
		private GritGateTerminalScreen LastScreen;

		// Token: 0x0400506E RID: 20590
		private Action After;
	}
}
