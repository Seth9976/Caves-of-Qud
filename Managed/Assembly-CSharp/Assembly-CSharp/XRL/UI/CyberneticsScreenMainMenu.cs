using System;
using XRL.Language;

namespace XRL.UI
{
	// Token: 0x02001484 RID: 5252
	public class CyberneticsScreenMainMenu : CyberneticsScreen
	{
		// Token: 0x0600CEBD RID: 52925 RVA: 0x004B3BAC File Offset: 0x004B1DAC
		public CyberneticsScreenMainMenu()
		{
			this.MainText = "Welcome, Aristocrat, to a becoming nook. " + base.Terminal.Subject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, true) + " one step closer to the Grand Unification. Please choose from the following options.";
			this.Options.Add("Learn About Cybernetics");
			this.Options.Add("Install Cybernetics");
			this.Options.Add("Uninstall Cybernetics");
			this.Options.Add("Upgrade Your License");
			if (XRL.UI.Options.SifrahHacking)
			{
				this.SelectOption = -1;
				this.HackOption = -1;
				int possibleSubjectCount = base.Terminal.GetPossibleSubjectCount();
				if (possibleSubjectCount > 1)
				{
					int authorizedSubjectCount = base.Terminal.GetAuthorizedSubjectCount();
					if (authorizedSubjectCount > 1)
					{
						this.SelectOption = this.Options.Count;
						this.Options.Add("Select Subject");
					}
					if (!base.Terminal.HackActive && possibleSubjectCount > authorizedSubjectCount)
					{
						this.HackOption = this.Options.Count;
						this.Options.Add(TextFilters.Leet("Attempt Hack to Select Subject", true).Color("R"));
					}
				}
			}
		}

		// Token: 0x0600CEBE RID: 52926 RVA: 0x004B3CEC File Offset: 0x004B1EEC
		public override void Activate()
		{
			switch (base.Terminal.Selected)
			{
			case 0:
				base.Terminal.CurrentScreen = new CyberneticsScreenLearn();
				return;
			case 1:
				base.Terminal.CurrentScreen = new CyberneticsScreenInstall();
				return;
			case 2:
				base.Terminal.CurrentScreen = new CyberneticsScreenRemove();
				return;
			case 3:
				base.Terminal.CurrentScreen = new CyberneticsScreenUpgrade();
				return;
			default:
				if (base.Terminal.Selected == this.SelectOption)
				{
					base.Terminal.CurrentScreen = new CyberneticsScreenSelectSubject();
					return;
				}
				if (base.Terminal.Selected == this.HackOption)
				{
					if (base.Terminal.HackActive || base.Terminal.AttemptHack())
					{
						base.Terminal.CurrentScreen = new CyberneticsScreenSelectSubject();
						return;
					}
					base.Terminal.CurrentScreen = new CyberneticsScreenGoodbye(false);
				}
				return;
			}
		}

		// Token: 0x0400504C RID: 20556
		private int SelectOption = -1;
	}
}
