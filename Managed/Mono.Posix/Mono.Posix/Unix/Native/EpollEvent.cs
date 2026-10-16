using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000060 RID: 96
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 12)]
	public struct EpollEvent
	{
		// Token: 0x04000460 RID: 1120
		[FieldOffset(0)]
		public EpollEvents events;

		// Token: 0x04000461 RID: 1121
		[FieldOffset(4)]
		public int fd;

		// Token: 0x04000462 RID: 1122
		[FieldOffset(4)]
		public IntPtr ptr;

		// Token: 0x04000463 RID: 1123
		[FieldOffset(4)]
		public uint u32;

		// Token: 0x04000464 RID: 1124
		[FieldOffset(4)]
		public ulong u64;
	}
}
