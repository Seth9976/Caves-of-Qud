using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C4 RID: 196
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct RequestSyncCommand : IInputDeviceCommandInfo
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00044E7C File Offset: 0x0004307C
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'Y', 'N', 'C');
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00044E8B File Offset: 0x0004308B
		public FourCC typeStatic
		{
			get
			{
				return RequestSyncCommand.Type;
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00044E94 File Offset: 0x00043094
		public static RequestSyncCommand Create()
		{
			return new RequestSyncCommand
			{
				baseCommand = new InputDeviceCommand(RequestSyncCommand.Type, 8)
			};
		}

		// Token: 0x0400052F RID: 1327
		internal const int kSize = 8;

		// Token: 0x04000530 RID: 1328
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
