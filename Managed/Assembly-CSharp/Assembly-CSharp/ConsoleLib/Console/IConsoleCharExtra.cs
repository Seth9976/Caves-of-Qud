using System;

namespace ConsoleLib.Console
{
	// Token: 0x02001533 RID: 5427
	public abstract class IConsoleCharExtra
	{
		// Token: 0x0600D375 RID: 54133
		public abstract void Clear(bool overtyping = false);

		// Token: 0x0600D376 RID: 54134
		public abstract IConsoleCharExtra Copy();

		// Token: 0x0600D377 RID: 54135 RVA: 0x004E7F0F File Offset: 0x004E610F
		public virtual void BeforeRender(int x, int y, ConsoleChar ch, ex3DSprite2 sprite, ScreenBuffer buffer)
		{
		}

		// Token: 0x0600D378 RID: 54136 RVA: 0x004E7F11 File Offset: 0x004E6111
		public virtual void AfterRender(int x, int y, ConsoleChar ch, ex3DSprite2 sprite, ScreenBuffer buffer)
		{
		}
	}
}
