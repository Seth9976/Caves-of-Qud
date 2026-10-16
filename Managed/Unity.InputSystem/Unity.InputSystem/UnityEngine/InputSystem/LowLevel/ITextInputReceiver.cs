using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000CF RID: 207
	public interface ITextInputReceiver
	{
		// Token: 0x06000D61 RID: 3425
		void OnTextInput(char character);

		// Token: 0x06000D62 RID: 3426
		void OnIMECompositionChanged(IMECompositionString compositionString);
	}
}
