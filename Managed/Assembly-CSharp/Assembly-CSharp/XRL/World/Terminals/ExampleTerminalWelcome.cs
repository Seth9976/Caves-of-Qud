using System;
using XRL.UI;

namespace XRL.World.Terminals
{
	// Token: 0x020009BF RID: 2495
	public class ExampleTerminalWelcome : GenericTerminalScreen
	{
		// Token: 0x06006B32 RID: 27442 RVA: 0x00228922 File Offset: 0x00226B22
		public ExampleTerminalWelcome()
		{
			this.mainText = "<Welcome to this example terminal>";
			this.Options.Add("Goto 10");
			this.Options.Add("Exit.");
		}

		// Token: 0x06006B33 RID: 27443 RVA: 0x00228955 File Offset: 0x00226B55
		public override void Back()
		{
			base.terminal.currentScreen = null;
		}

		// Token: 0x06006B34 RID: 27444 RVA: 0x00228963 File Offset: 0x00226B63
		public override void Activate()
		{
			if (base.terminal.nSelected == 0)
			{
				base.terminal.currentScreen = new ExampleTerminalWelcome();
			}
			if (base.terminal.nSelected == 1)
			{
				base.terminal.currentScreen = null;
			}
		}
	}
}
