using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000079 RID: 121
	[StructLayout(LayoutKind.Explicit, Size = 1040)]
	public struct SendBufferedHapticCommand : IInputDeviceCommandInfo
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00039361 File Offset: 0x00037561
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'U', '0');
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00039370 File Offset: 0x00037570
		public FourCC typeStatic
		{
			get
			{
				return SendBufferedHapticCommand.Type;
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00039378 File Offset: 0x00037578
		public unsafe static SendBufferedHapticCommand Create(byte[] rumbleBuffer)
		{
			if (rumbleBuffer == null)
			{
				throw new ArgumentNullException("rumbleBuffer");
			}
			int num = Mathf.Min(1024, rumbleBuffer.Length);
			SendBufferedHapticCommand sendBufferedHapticCommand = new SendBufferedHapticCommand
			{
				baseCommand = new InputDeviceCommand(SendBufferedHapticCommand.Type, 1040),
				bufferSize = num
			};
			SendBufferedHapticCommand* ptr = &sendBufferedHapticCommand;
			fixed (byte[] array = rumbleBuffer)
			{
				byte* ptr2;
				if (rumbleBuffer == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				for (int i = 0; i < num; i++)
				{
					*((ref ptr->buffer.FixedElementField) + i) = ptr2[i];
				}
			}
			return sendBufferedHapticCommand;
		}

		// Token: 0x040003AB RID: 939
		private const int kMaxHapticBufferSize = 1024;

		// Token: 0x040003AC RID: 940
		private const int kSize = 1040;

		// Token: 0x040003AD RID: 941
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x040003AE RID: 942
		[FieldOffset(8)]
		private int channel;

		// Token: 0x040003AF RID: 943
		[FieldOffset(12)]
		private int bufferSize;

		// Token: 0x040003B0 RID: 944
		[FixedBuffer(typeof(byte), 1024)]
		[FieldOffset(16)]
		private SendBufferedHapticCommand.<buffer>e__FixedBuffer buffer;

		// Token: 0x020001C9 RID: 457
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1024)]
		public struct <buffer>e__FixedBuffer
		{
			// Token: 0x04000973 RID: 2419
			public byte FixedElementField;
		}
	}
}
