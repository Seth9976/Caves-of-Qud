using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F8 RID: 248
	public interface IInputStateChangeMonitor
	{
		// Token: 0x06000EB8 RID: 3768
		void NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex);

		// Token: 0x06000EB9 RID: 3769
		void NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex);
	}
}
