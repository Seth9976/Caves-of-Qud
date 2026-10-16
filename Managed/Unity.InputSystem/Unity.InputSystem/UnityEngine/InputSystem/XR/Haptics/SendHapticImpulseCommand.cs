using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x0200007A RID: 122
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct SendHapticImpulseCommand : IInputDeviceCommandInfo
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00039413 File Offset: 0x00037613
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'I', '0');
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00039422 File Offset: 0x00037622
		public FourCC typeStatic
		{
			get
			{
				return SendHapticImpulseCommand.Type;
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0003942C File Offset: 0x0003762C
		public static SendHapticImpulseCommand Create(int motorChannel, float motorAmplitude, float motorDuration)
		{
			return new SendHapticImpulseCommand
			{
				baseCommand = new InputDeviceCommand(SendHapticImpulseCommand.Type, 20),
				channel = motorChannel,
				amplitude = motorAmplitude,
				duration = motorDuration
			};
		}

		// Token: 0x040003B1 RID: 945
		private const int kSize = 20;

		// Token: 0x040003B2 RID: 946
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x040003B3 RID: 947
		[FieldOffset(8)]
		private int channel;

		// Token: 0x040003B4 RID: 948
		[FieldOffset(12)]
		private float amplitude;

		// Token: 0x040003B5 RID: 949
		[FieldOffset(16)]
		private float duration;
	}
}
