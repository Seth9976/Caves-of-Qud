using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000076 RID: 118
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct GetCurrentHapticStateCommand : IInputDeviceCommandInfo
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00039234 File Offset: 0x00037434
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'S', '0');
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00039243 File Offset: 0x00037443
		public FourCC typeStatic
		{
			get
			{
				return GetCurrentHapticStateCommand.Type;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0003924A File Offset: 0x0003744A
		public HapticState currentState
		{
			get
			{
				return new HapticState(this.samplesQueued, this.samplesAvailable);
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00039260 File Offset: 0x00037460
		public static GetCurrentHapticStateCommand Create()
		{
			return new GetCurrentHapticStateCommand
			{
				baseCommand = new InputDeviceCommand(GetCurrentHapticStateCommand.Type, 16)
			};
		}

		// Token: 0x04000399 RID: 921
		private const int kSize = 16;

		// Token: 0x0400039A RID: 922
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x0400039B RID: 923
		[FieldOffset(8)]
		public uint samplesQueued;

		// Token: 0x0400039C RID: 924
		[FieldOffset(12)]
		public uint samplesAvailable;
	}
}
