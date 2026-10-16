using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000745 RID: 1861
	[StructLayout(LayoutKind.Explicit)]
	internal struct AlignmentUnion
	{
		// Token: 0x040022E8 RID: 8936
		[FieldOffset(0)]
		public ulong Alignment;

		// Token: 0x040022E9 RID: 8937
		[FieldOffset(0)]
		public int Length;

		// Token: 0x040022EA RID: 8938
		[FieldOffset(4)]
		public int IfIndex;
	}
}
