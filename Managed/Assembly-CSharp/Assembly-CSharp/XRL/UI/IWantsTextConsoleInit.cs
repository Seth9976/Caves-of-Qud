using System;
using ConsoleLib.Console;

namespace XRL.UI
{
	/// <summary>
	/// If implemented by a class with <c>UIView</c> it will call Init passing the TextConsole and ScreenBuffer at load time.
	/// </summary>
	// Token: 0x020014D8 RID: 5336
	public interface IWantsTextConsoleInit
	{
		// Token: 0x0600D0F1 RID: 53489
		void Init(TextConsole console, ScreenBuffer buffer);
	}
}
