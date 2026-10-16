using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E0 RID: 224
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceConfigurationEvent : IInputEventTypeInfo
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x000458AB File Offset: 0x00043AAB
		public FourCC typeStatic
		{
			get
			{
				return 1145259591;
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000458B8 File Offset: 0x00043AB8
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (DeviceConfigurationEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000458D0 File Offset: 0x00043AD0
		public static DeviceConfigurationEvent Create(int deviceId, double time)
		{
			return new DeviceConfigurationEvent
			{
				baseEvent = new InputEvent(1145259591, 20, deviceId, time)
			};
		}

		// Token: 0x040005A9 RID: 1449
		public const int Type = 1145259591;

		// Token: 0x040005AA RID: 1450
		[FieldOffset(0)]
		public InputEvent baseEvent;
	}
}
