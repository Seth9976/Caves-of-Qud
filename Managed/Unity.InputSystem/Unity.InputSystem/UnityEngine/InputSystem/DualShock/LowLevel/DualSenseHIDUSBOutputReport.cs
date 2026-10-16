using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x020000AA RID: 170
	[StructLayout(LayoutKind.Explicit, Size = 56)]
	internal struct DualSenseHIDUSBOutputReport : IInputDeviceCommandInfo
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0004449C File Offset: 0x0004269C
		public static FourCC Type
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'O');
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x000444AB File Offset: 0x000426AB
		public FourCC typeStatic
		{
			get
			{
				return DualSenseHIDUSBOutputReport.Type;
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000444B4 File Offset: 0x000426B4
		public static DualSenseHIDUSBOutputReport Create(DualSenseHIDOutputReportPayload payload, int outputReportSize)
		{
			return new DualSenseHIDUSBOutputReport
			{
				baseCommand = new InputDeviceCommand(DualSenseHIDUSBOutputReport.Type, 8 + outputReportSize),
				reportId = 2,
				payload = payload
			};
		}

		// Token: 0x040004CB RID: 1227
		internal const int kSize = 56;

		// Token: 0x040004CC RID: 1228
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004CD RID: 1229
		[FieldOffset(8)]
		public byte reportId;

		// Token: 0x040004CE RID: 1230
		[FieldOffset(9)]
		public DualSenseHIDOutputReportPayload payload;
	}
}
