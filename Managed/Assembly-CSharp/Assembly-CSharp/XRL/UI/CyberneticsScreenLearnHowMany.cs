using System;

namespace XRL.UI
{
	// Token: 0x02001487 RID: 5255
	public class CyberneticsScreenLearnHowMany : CyberneticsScreen
	{
		// Token: 0x0600CEC4 RID: 52932 RVA: 0x004B3F34 File Offset: 0x004B2134
		protected override void OnUpdate()
		{
			this.MainText = "Insightful question, Aristocrat.\n\nEach implant has a point cost, and the total point cost of your installed implants can't exceed your license tier (displayed at the bottom of this screen). You can upgrade your license at a nook by spending cybernetic credits.";
			this.ClearOptions();
			this.Options.Add("<back>");
			this.Options.Add("Can I uninstall implants?");
			this.Options.Add("Return To Main Menu");
		}

		// Token: 0x0600CEC5 RID: 52933 RVA: 0x004B3F82 File Offset: 0x004B2182
		public override void Back()
		{
			base.Terminal.CheckSecurity(1, new CyberneticsScreenMainMenu(), 1);
		}

		// Token: 0x0600CEC6 RID: 52934 RVA: 0x004B3F98 File Offset: 0x004B2198
		public override void Activate()
		{
			switch (base.Terminal.Selected)
			{
			case 0:
				base.Terminal.CheckSecurity(1, new CyberneticsScreenLearn(), 1);
				return;
			case 1:
				base.Terminal.CheckSecurity(1, new CyberneticsScreenLearnUninstall(), 1);
				return;
			case 2:
				this.Back();
				return;
			default:
				return;
			}
		}
	}
}
