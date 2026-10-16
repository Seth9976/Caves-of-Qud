using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000078 RID: 120
	[StructLayout(LayoutKind.Explicit, Size = 28)]
	public struct GetHapticCapabilitiesCommand : IInputDeviceCommandInfo
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x000392F6 File Offset: 0x000374F6
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'C', '0');
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00039305 File Offset: 0x00037505
		public FourCC typeStatic
		{
			get
			{
				return GetHapticCapabilitiesCommand.Type;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0003930C File Offset: 0x0003750C
		public HapticCapabilities capabilities
		{
			get
			{
				return new HapticCapabilities(this.numChannels, this.supportsImpulse, this.supportsBuffer, this.frequencyHz, this.maxBufferSize, this.optimalBufferSize);
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00039338 File Offset: 0x00037538
		public static GetHapticCapabilitiesCommand Create()
		{
			return new GetHapticCapabilitiesCommand
			{
				baseCommand = new InputDeviceCommand(GetHapticCapabilitiesCommand.Type, 28)
			};
		}

		// Token: 0x040003A3 RID: 931
		private const int kSize = 28;

		// Token: 0x040003A4 RID: 932
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x040003A5 RID: 933
		[FieldOffset(8)]
		public uint numChannels;

		// Token: 0x040003A6 RID: 934
		[FieldOffset(12)]
		public bool supportsImpulse;

		// Token: 0x040003A7 RID: 935
		[FieldOffset(13)]
		public bool supportsBuffer;

		// Token: 0x040003A8 RID: 936
		[FieldOffset(16)]
		public uint frequencyHz;

		// Token: 0x040003A9 RID: 937
		[FieldOffset(20)]
		public uint maxBufferSize;

		// Token: 0x040003AA RID: 938
		[FieldOffset(24)]
		public uint optimalBufferSize;
	}
}
