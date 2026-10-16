using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x020000AB RID: 171
	[StructLayout(LayoutKind.Explicit, Size = 86)]
	internal struct DualSenseHIDBluetoothOutputReport : IInputDeviceCommandInfo
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x000444EE File Offset: 0x000426EE
		public static FourCC Type
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'O');
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x000444FD File Offset: 0x000426FD
		public FourCC typeStatic
		{
			get
			{
				return DualSenseHIDBluetoothOutputReport.Type;
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00044504 File Offset: 0x00042704
		public static DualSenseHIDBluetoothOutputReport Create(DualSenseHIDOutputReportPayload payload, byte outputSequenceId, int outputReportSize)
		{
			return new DualSenseHIDBluetoothOutputReport
			{
				baseCommand = new InputDeviceCommand(DualSenseHIDBluetoothOutputReport.Type, 8 + outputReportSize),
				reportId = 49,
				tag1 = (byte)((outputSequenceId & 15) << 4),
				tag2 = 16,
				payload = payload
			};
		}

		// Token: 0x040004CF RID: 1231
		internal const int kSize = 86;

		// Token: 0x040004D0 RID: 1232
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004D1 RID: 1233
		[FieldOffset(8)]
		public byte reportId;

		// Token: 0x040004D2 RID: 1234
		[FieldOffset(9)]
		public byte tag1;

		// Token: 0x040004D3 RID: 1235
		[FieldOffset(10)]
		public byte tag2;

		// Token: 0x040004D4 RID: 1236
		[FieldOffset(11)]
		public DualSenseHIDOutputReportPayload payload;

		// Token: 0x040004D5 RID: 1237
		[FieldOffset(82)]
		public uint crc32;

		// Token: 0x040004D6 RID: 1238
		[FixedBuffer(typeof(byte), 74)]
		[FieldOffset(8)]
		public DualSenseHIDBluetoothOutputReport.<rawData>e__FixedBuffer rawData;

		// Token: 0x02000202 RID: 514
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 74)]
		public struct <rawData>e__FixedBuffer
		{
			// Token: 0x04000B81 RID: 2945
			public byte FixedElementField;
		}
	}
}
