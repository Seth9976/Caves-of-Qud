using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E1 RID: 225
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceRemoveEvent : IInputEventTypeInfo
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00045900 File Offset: 0x00043B00
		public FourCC typeStatic
		{
			get
			{
				return 1146242381;
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0004590C File Offset: 0x00043B0C
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (DeviceRemoveEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00045924 File Offset: 0x00043B24
		public static DeviceRemoveEvent Create(int deviceId, double time = -1.0)
		{
			return new DeviceRemoveEvent
			{
				baseEvent = new InputEvent(1146242381, 20, deviceId, time)
			};
		}

		// Token: 0x040005AB RID: 1451
		public const int Type = 1146242381;

		// Token: 0x040005AC RID: 1452
		[FieldOffset(0)]
		public InputEvent baseEvent;
	}
}
