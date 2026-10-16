using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B4 RID: 180
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct EnableDeviceCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x000448DC File Offset: 0x00042ADC
		public static FourCC Type
		{
			get
			{
				return new FourCC('E', 'N', 'B', 'L');
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x000448EB File Offset: 0x00042AEB
		public FourCC typeStatic
		{
			get
			{
				return EnableDeviceCommand.Type;
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000448F4 File Offset: 0x00042AF4
		public static EnableDeviceCommand Create()
		{
			return new EnableDeviceCommand
			{
				baseCommand = new InputDeviceCommand(EnableDeviceCommand.Type, 8)
			};
		}

		// Token: 0x04000501 RID: 1281
		internal const int kSize = 8;

		// Token: 0x04000502 RID: 1282
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
