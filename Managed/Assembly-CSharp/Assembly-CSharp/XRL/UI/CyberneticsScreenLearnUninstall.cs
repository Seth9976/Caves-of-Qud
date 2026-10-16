using System;

namespace XRL.UI
{
	// Token: 0x02001488 RID: 5256
	public class CyberneticsScreenLearnUninstall : CyberneticsScreen
	{
		// Token: 0x0600CEC8 RID: 52936 RVA: 0x004B3FFC File Offset: 0x004B21FC
		protected override void OnUpdate()
		{
			this.MainText = "Aristocrat, your question was most finely uttered.\n\nYou may freely install and uninstall implants at a nook, though some implants are destroyed when uninstalled or can't be uninstalled at all.";
			this.ClearOptions();
			this.Options.Add("<back>");
			this.Options.Add("How many implants can I install?");
			this.Options.Add("Return To Main Menu");
		}

		// Token: 0x0600CEC9 RID: 52937 RVA: 0x004B404A File Offset: 0x004B224A
		public override void Back()
		{
			base.Terminal.CheckSecurity(1, new CyberneticsScreenMainMenu(), 1);
		}

		// Token: 0x0600CECA RID: 52938 RVA: 0x004B4060 File Offset: 0x004B2260
		public override void Activate()
		{
			switch (base.Terminal.Selected)
			{
			case 0:
				base.Terminal.CheckSecurity(1, new CyberneticsScreenLearn(), 1);
				return;
			case 1:
				base.Terminal.CheckSecurity(1, new CyberneticsScreenLearnHowMany(), 1);
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
