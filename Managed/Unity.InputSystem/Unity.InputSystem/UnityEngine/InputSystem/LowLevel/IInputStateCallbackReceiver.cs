using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F7 RID: 247
	public interface IInputStateCallbackReceiver
	{
		// Token: 0x06000EB5 RID: 3765
		void OnNextUpdate();

		// Token: 0x06000EB6 RID: 3766
		void OnStateEvent(InputEventPtr eventPtr);

		// Token: 0x06000EB7 RID: 3767
		bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset);
	}
}
