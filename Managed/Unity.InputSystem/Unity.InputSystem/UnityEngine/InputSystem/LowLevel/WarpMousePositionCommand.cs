using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C7 RID: 199
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct WarpMousePositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00044F55 File Offset: 0x00043155
		public static FourCC Type
		{
			get
			{
				return new FourCC('W', 'P', 'M', 'S');
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00044F64 File Offset: 0x00043164
		public FourCC typeStatic
		{
			get
			{
				return WarpMousePositionCommand.Type;
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00044F6C File Offset: 0x0004316C
		public static WarpMousePositionCommand Create(Vector2 position)
		{
			return new WarpMousePositionCommand
			{
				baseCommand = new InputDeviceCommand(WarpMousePositionCommand.Type, 16),
				warpPositionInPlayerDisplaySpace = position
			};
		}

		// Token: 0x04000537 RID: 1335
		internal const int kSize = 16;

		// Token: 0x04000538 RID: 1336
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000539 RID: 1337
		[FieldOffset(8)]
		public Vector2 warpPositionInPlayerDisplaySpace;
	}
}
