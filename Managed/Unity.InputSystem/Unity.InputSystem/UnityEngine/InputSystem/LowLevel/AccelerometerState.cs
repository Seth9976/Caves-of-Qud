using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D6 RID: 214
	internal struct AccelerometerState : IInputStateTypeInfo
	{
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00045241 File Offset: 0x00043441
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('A', 'C', 'C', 'L');
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00045250 File Offset: 0x00043450
		public FourCC format
		{
			get
			{
				return AccelerometerState.kFormat;
			}
		}

		// Token: 0x0400057F RID: 1407
		[InputControl(displayName = "Acceleration", processors = "CompensateDirection", noisy = true)]
		public Vector3 acceleration;
	}
}
