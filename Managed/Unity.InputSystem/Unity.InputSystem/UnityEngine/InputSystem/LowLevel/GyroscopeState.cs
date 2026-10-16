using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D7 RID: 215
	internal struct GyroscopeState : IInputStateTypeInfo
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00045257 File Offset: 0x00043457
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('G', 'Y', 'R', 'O');
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00045266 File Offset: 0x00043466
		public FourCC format
		{
			get
			{
				return GyroscopeState.kFormat;
			}
		}

		// Token: 0x04000580 RID: 1408
		[InputControl(displayName = "Angular Velocity", processors = "CompensateDirection", noisy = true)]
		public Vector3 angularVelocity;
	}
}
