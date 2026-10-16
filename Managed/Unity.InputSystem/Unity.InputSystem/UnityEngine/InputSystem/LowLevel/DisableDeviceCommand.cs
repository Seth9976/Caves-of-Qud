using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B3 RID: 179
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct DisableDeviceCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0004489E File Offset: 0x00042A9E
		public static FourCC Type
		{
			get
			{
				return new FourCC('D', 'S', 'B', 'L');
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x000448AD File Offset: 0x00042AAD
		public FourCC typeStatic
		{
			get
			{
				return DisableDeviceCommand.Type;
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000448B4 File Offset: 0x00042AB4
		public static DisableDeviceCommand Create()
		{
			return new DisableDeviceCommand
			{
				baseCommand = new InputDeviceCommand(DisableDeviceCommand.Type, 8)
			};
		}

		// Token: 0x040004FF RID: 1279
		internal const int kSize = 8;

		// Token: 0x04000500 RID: 1280
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
