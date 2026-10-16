using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D9 RID: 217
	internal struct AttitudeState : IInputStateTypeInfo
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00045283 File Offset: 0x00043483
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('A', 'T', 'T', 'D');
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00045292 File Offset: 0x00043492
		public FourCC format
		{
			get
			{
				return AttitudeState.kFormat;
			}
		}

		// Token: 0x04000582 RID: 1410
		[InputControl(displayName = "Attitude", processors = "CompensateRotation", noisy = true)]
		public Quaternion attitude;
	}
}
