using System;

namespace XRL.UI
{
	// Token: 0x0200148B RID: 5259
	public class CyberneticsScreenSimpleText : CyberneticsScreen
	{
		// Token: 0x0600CED6 RID: 52950 RVA: 0x004B45A9 File Offset: 0x004B27A9
		public CyberneticsScreenSimpleText(string Text, TerminalScreen BackTo, int SecurityChance = 0)
		{
			this.BackTo = BackTo;
			this.SecurityChance = SecurityChance;
			this.MainText = Text;
			this.Options.Add("<back>");
		}

		// Token: 0x0600CED7 RID: 52951 RVA: 0x004B45D6 File Offset: 0x004B27D6
		public override void Back()
		{
			base.Terminal.CheckSecurity(this.SecurityChance, this.BackTo, 1);
		}

		// Token: 0x0600CED8 RID: 52952 RVA: 0x004B45F1 File Offset: 0x004B27F1
		public override void Activate()
		{
			this.Back();
		}

		// Token: 0x04005050 RID: 20560
		private TerminalScreen BackTo;

		// Token: 0x04005051 RID: 20561
		private int SecurityChance;
	}
}
