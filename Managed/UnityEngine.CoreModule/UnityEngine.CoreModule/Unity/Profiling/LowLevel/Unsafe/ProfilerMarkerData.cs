using System;
using System.Runtime.InteropServices;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000078 RID: 120
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct ProfilerMarkerData
	{
		// Token: 0x040001C0 RID: 448
		[FieldOffset(0)]
		public byte Type;

		// Token: 0x040001C1 RID: 449
		[FieldOffset(1)]
		private readonly byte reserved0;

		// Token: 0x040001C2 RID: 450
		[FieldOffset(2)]
		private readonly ushort reserved1;

		// Token: 0x040001C3 RID: 451
		[FieldOffset(4)]
		public uint Size;

		// Token: 0x040001C4 RID: 452
		[FieldOffset(8)]
		public unsafe void* Ptr;
	}
}
