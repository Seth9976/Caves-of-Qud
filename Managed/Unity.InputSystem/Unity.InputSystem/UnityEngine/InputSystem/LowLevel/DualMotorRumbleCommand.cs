using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000CA RID: 202
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct DualMotorRumbleCommand : IInputDeviceCommandInfo
	{
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0004503C File Offset: 0x0004323C
		public static FourCC Type
		{
			get
			{
				return new FourCC('R', 'M', 'B', 'L');
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0004504B File Offset: 0x0004324B
		public FourCC typeStatic
		{
			get
			{
				return DualMotorRumbleCommand.Type;
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00045054 File Offset: 0x00043254
		public static DualMotorRumbleCommand Create(float lowFrequency, float highFrequency)
		{
			return new DualMotorRumbleCommand
			{
				baseCommand = new InputDeviceCommand(DualMotorRumbleCommand.Type, 16),
				lowFrequencyMotorSpeed = lowFrequency,
				highFrequencyMotorSpeed = highFrequency
			};
		}

		// Token: 0x0400055C RID: 1372
		internal const int kSize = 16;

		// Token: 0x0400055D RID: 1373
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400055E RID: 1374
		[FieldOffset(8)]
		public float lowFrequencyMotorSpeed;

		// Token: 0x0400055F RID: 1375
		[FieldOffset(12)]
		public float highFrequencyMotorSpeed;
	}
}
