using System;
using UnityEngine.InputSystem.XR.Haptics;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000067 RID: 103
	public class XRControllerWithRumble : XRController
	{
		// Token: 0x06000A01 RID: 2561 RVA: 0x000378E0 File Offset: 0x00035AE0
		public void SendImpulse(float amplitude, float duration)
		{
			SendHapticImpulseCommand sendHapticImpulseCommand = SendHapticImpulseCommand.Create(0, amplitude, duration);
			base.ExecuteCommand<SendHapticImpulseCommand>(ref sendHapticImpulseCommand);
		}
	}
}
