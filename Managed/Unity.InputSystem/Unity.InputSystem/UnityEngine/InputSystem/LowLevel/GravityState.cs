using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D8 RID: 216
	internal struct GravityState : IInputStateTypeInfo
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0004526D File Offset: 0x0004346D
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('G', 'R', 'V', ' ');
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0004527C File Offset: 0x0004347C
		public FourCC format
		{
			get
			{
				return GravityState.kFormat;
			}
		}

		// Token: 0x04000581 RID: 1409
		[InputControl(displayName = "Gravity", processors = "CompensateDirection", noisy = true)]
		public Vector3 gravity;
	}
}
