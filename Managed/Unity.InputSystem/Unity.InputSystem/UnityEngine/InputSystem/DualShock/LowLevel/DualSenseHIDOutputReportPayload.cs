using System;
using System.Runtime.InteropServices;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x020000A9 RID: 169
	[StructLayout(LayoutKind.Explicit, Size = 47)]
	internal struct DualSenseHIDOutputReportPayload
	{
		// Token: 0x040004C4 RID: 1220
		[FieldOffset(0)]
		public byte enableFlags1;

		// Token: 0x040004C5 RID: 1221
		[FieldOffset(1)]
		public byte enableFlags2;

		// Token: 0x040004C6 RID: 1222
		[FieldOffset(2)]
		public byte highFrequencyMotorSpeed;

		// Token: 0x040004C7 RID: 1223
		[FieldOffset(3)]
		public byte lowFrequencyMotorSpeed;

		// Token: 0x040004C8 RID: 1224
		[FieldOffset(44)]
		public byte redColor;

		// Token: 0x040004C9 RID: 1225
		[FieldOffset(45)]
		public byte greenColor;

		// Token: 0x040004CA RID: 1226
		[FieldOffset(46)]
		public byte blueColor;
	}
}
