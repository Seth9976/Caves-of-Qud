using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B7 RID: 183
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct InitiateUserAccountPairingCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0004497B File Offset: 0x00042B7B
		public static FourCC Type
		{
			get
			{
				return new FourCC('P', 'A', 'I', 'R');
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0004498A File Offset: 0x00042B8A
		public FourCC typeStatic
		{
			get
			{
				return InitiateUserAccountPairingCommand.Type;
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00044994 File Offset: 0x00042B94
		public static InitiateUserAccountPairingCommand Create()
		{
			return new InitiateUserAccountPairingCommand
			{
				baseCommand = new InputDeviceCommand(InitiateUserAccountPairingCommand.Type, 8)
			};
		}

		// Token: 0x04000506 RID: 1286
		internal const int kSize = 8;

		// Token: 0x04000507 RID: 1287
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x02000207 RID: 519
		public enum Result
		{
			// Token: 0x04000B89 RID: 2953
			SuccessfullyInitiated = 1,
			// Token: 0x04000B8A RID: 2954
			ErrorNotSupported = -1,
			// Token: 0x04000B8B RID: 2955
			ErrorAlreadyInProgress = -2
		}
	}
}
