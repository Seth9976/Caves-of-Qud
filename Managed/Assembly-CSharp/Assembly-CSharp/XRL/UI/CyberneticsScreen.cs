using System;
using ConsoleLib.Console;
using XRL.Language;

namespace XRL.UI
{
	// Token: 0x02001483 RID: 5251
	public class CyberneticsScreen : TerminalScreen
	{
		// Token: 0x0600CEBB RID: 52923 RVA: 0x004B3968 File Offset: 0x004B1B68
		public override void BeforeRender(ScreenBuffer buffer, ref string footerText)
		{
			footerText = string.Format(" Credits: {0}  License Tier: {1}  Points Used: {2}", base.Terminal.Credits, base.Terminal.Licenses, base.Terminal.LicensesUsed);
			if (buffer == null)
			{
				return;
			}
			int num = 3;
			buffer.Goto(num + 3, 24);
			buffer.Write(" Credits: " + base.Terminal.Credits.ToString() + " ", true, false, false, null, -1);
			buffer.Goto(num + 38, 24);
			buffer.Write(" License Tier: " + base.Terminal.Licenses.ToString() + " ", true, false, false, null, -1);
			buffer.Write(" Points Used: " + base.Terminal.LicensesUsed.ToString() + " ", true, false, false, null, -1);
			bool hackActive = base.Terminal.HackActive;
			int hackLevel = base.Terminal.HackLevel;
			bool lowLevelHack = base.Terminal.LowLevelHack;
			if (hackActive || hackLevel > 0)
			{
				string text = ((hackActive && lowLevelHack) ? TextFilters.Leet("HACK LEVEL", true) : "Hack Level");
				buffer.Goto(num + 3, 2);
				buffer.Write(string.Concat(new string[]
				{
					" {{G|",
					text,
					": ",
					hackLevel.ToString(),
					"}} "
				}), true, false, false, null, -1);
			}
			int securityAlertLevel = base.Terminal.SecurityAlertLevel;
			if (hackActive || securityAlertLevel > 0)
			{
				string text2;
				if (securityAlertLevel <= 0)
				{
					text2 = "y";
				}
				else if (securityAlertLevel >= hackLevel - 1)
				{
					text2 = "R";
				}
				else if (securityAlertLevel <= hackLevel - 3)
				{
					text2 = "W";
				}
				else
				{
					text2 = "r";
				}
				string text3 = ((hackActive && lowLevelHack) ? TextFilters.Leet("SECURITY ALERT LEVEL", true) : "Security Alert Level");
				buffer.Goto(72 - num - text3.StrippedLength(), 2);
				buffer.Write(string.Concat(new string[]
				{
					" {{",
					text2,
					"|",
					text3,
					": ",
					securityAlertLevel.ToString(),
					"}} "
				}), true, false, false, null, -1);
			}
		}
	}
}
