using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E2 RID: 226
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceResetEvent : IInputEventTypeInfo
	{
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00045954 File Offset: 0x00043B54
		public FourCC typeStatic
		{
			get
			{
				return 1146245972;
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00045960 File Offset: 0x00043B60
		public static DeviceResetEvent Create(int deviceId, bool hardReset = false, double time = -1.0)
		{
			DeviceResetEvent deviceResetEvent = new DeviceResetEvent
			{
				baseEvent = new InputEvent(1146245972, 20, deviceId, time)
			};
			deviceResetEvent.hardReset = hardReset;
			return deviceResetEvent;
		}

		// Token: 0x040005AD RID: 1453
		public const int Type = 1146245972;

		// Token: 0x040005AE RID: 1454
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x040005AF RID: 1455
		[FieldOffset(8)]
		public bool hardReset;
	}
}
