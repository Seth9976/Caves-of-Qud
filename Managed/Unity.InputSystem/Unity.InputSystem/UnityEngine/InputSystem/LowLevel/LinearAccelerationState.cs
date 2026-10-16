using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DA RID: 218
	internal struct LinearAccelerationState : IInputStateTypeInfo
	{
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00045299 File Offset: 0x00043499
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('L', 'A', 'A', 'C');
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x000452A8 File Offset: 0x000434A8
		public FourCC format
		{
			get
			{
				return LinearAccelerationState.kFormat;
			}
		}

		// Token: 0x04000583 RID: 1411
		[InputControl(displayName = "Acceleration", processors = "CompensateDirection", noisy = true)]
		public Vector3 acceleration;
	}
}
