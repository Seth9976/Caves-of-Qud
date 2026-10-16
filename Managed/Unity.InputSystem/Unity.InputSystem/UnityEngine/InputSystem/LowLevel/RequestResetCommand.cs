using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C3 RID: 195
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct RequestResetCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00044E3C File Offset: 0x0004303C
		public static FourCC Type
		{
			get
			{
				return new FourCC('R', 'S', 'E', 'T');
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00044E4B File Offset: 0x0004304B
		public FourCC typeStatic
		{
			get
			{
				return RequestResetCommand.Type;
			}
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00044E54 File Offset: 0x00043054
		public static RequestResetCommand Create()
		{
			return new RequestResetCommand
			{
				baseCommand = new InputDeviceCommand(RequestResetCommand.Type, 8)
			};
		}

		// Token: 0x0400052D RID: 1325
		internal const int kSize = 8;

		// Token: 0x0400052E RID: 1326
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
