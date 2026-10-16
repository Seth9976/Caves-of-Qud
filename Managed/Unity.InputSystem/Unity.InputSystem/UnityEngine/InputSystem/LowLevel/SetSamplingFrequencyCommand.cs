using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C6 RID: 198
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	public struct SetSamplingFrequencyCommand : IInputDeviceCommandInfo
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00044F0D File Offset: 0x0004310D
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'S', 'P', 'L');
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00044F1C File Offset: 0x0004311C
		public FourCC typeStatic
		{
			get
			{
				return SetSamplingFrequencyCommand.Type;
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00044F24 File Offset: 0x00043124
		public static SetSamplingFrequencyCommand Create(float frequency)
		{
			return new SetSamplingFrequencyCommand
			{
				baseCommand = new InputDeviceCommand(SetSamplingFrequencyCommand.Type, 12),
				frequency = frequency
			};
		}

		// Token: 0x04000534 RID: 1332
		internal const int kSize = 12;

		// Token: 0x04000535 RID: 1333
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000536 RID: 1334
		[FieldOffset(8)]
		public float frequency;
	}
}
