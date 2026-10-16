using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200004B RID: 75
	public interface IKeyboard
	{
		// Token: 0x060000FB RID: 251
		void ShowKeyboard(KeyboardArgs keyboardArgs, string initialText, Action<KeyboardResult, string> onComplete);
	}
}
