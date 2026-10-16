using System;
using XRL.Language;

namespace XRL.UI
{
	// Token: 0x02001485 RID: 5253
	public class CyberneticsScreenGoodbye : CyberneticsScreen
	{
		// Token: 0x0600CEBF RID: 52927 RVA: 0x004B3DD4 File Offset: 0x004B1FD4
		public CyberneticsScreenGoodbye(bool AllowHack = true)
		{
			this.MainText = "You are no aristocrat. Goodbye.";
			this.Options.Add("...");
			if (AllowHack && XRL.UI.Options.SifrahHacking)
			{
				this.HackOption = this.Options.Count;
				this.Options.Add(TextFilters.Leet("attempt hack", true));
			}
		}

		// Token: 0x0600CEC0 RID: 52928 RVA: 0x004B3E33 File Offset: 0x004B2033
		public override void Activate()
		{
			base.Terminal.CurrentScreen = null;
			if (base.Terminal.Selected == this.HackOption && base.Terminal.AttemptHack())
			{
				base.Terminal.CurrentScreen = new CyberneticsScreenMainMenu();
			}
		}
	}
}
