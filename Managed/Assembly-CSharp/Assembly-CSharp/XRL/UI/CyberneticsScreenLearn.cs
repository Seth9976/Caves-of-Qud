using System;

namespace XRL.UI
{
	// Token: 0x02001486 RID: 5254
	public class CyberneticsScreenLearn : CyberneticsScreen
	{
		// Token: 0x0600CEC1 RID: 52929 RVA: 0x004B3E74 File Offset: 0x004B2074
		public CyberneticsScreenLearn()
		{
			this.MainText = "Your curiosity is admirable, aristocrat.\n\nCybernetics are bionic augmentations implanted in your body to assist in your self-actualization. You can have implants installed at becoming nooks such as this one. Either load them in the rack or carry them on your person.";
			this.Options.Add("How many implants can I install?");
			this.Options.Add("Can I uninstall implants?");
			this.Options.Add("Return To Main Menu");
		}

		// Token: 0x0600CEC2 RID: 52930 RVA: 0x004B3EC2 File Offset: 0x004B20C2
		public override void Back()
		{
			base.Terminal.CheckSecurity(1, new CyberneticsScreenMainMenu(), 1);
		}

		// Token: 0x0600CEC3 RID: 52931 RVA: 0x004B3ED8 File Offset: 0x004B20D8
		public override void Activate()
		{
			switch (base.Terminal.Selected)
			{
			case 0:
				base.Terminal.CheckSecurity(1, new CyberneticsScreenLearnHowMany(), 1);
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
