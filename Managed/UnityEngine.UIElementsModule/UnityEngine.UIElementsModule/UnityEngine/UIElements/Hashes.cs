using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020004C5 RID: 1221
	internal struct Hashes
	{
		// Token: 0x040010A8 RID: 4264
		public const int kSize = 4;

		// Token: 0x040010A9 RID: 4265
		[FixedBuffer(typeof(int), 4)]
		public Hashes.<hashes>e__FixedBuffer hashes;

		// Token: 0x020004C6 RID: 1222
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <hashes>e__FixedBuffer
		{
			// Token: 0x040010AA RID: 4266
			public int FixedElementField;
		}
	}
}
