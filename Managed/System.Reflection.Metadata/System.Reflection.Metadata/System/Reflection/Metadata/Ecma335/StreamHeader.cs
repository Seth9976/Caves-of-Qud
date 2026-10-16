using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200012A RID: 298
	internal struct StreamHeader
	{
		// Token: 0x04000747 RID: 1863
		internal uint Offset;

		// Token: 0x04000748 RID: 1864
		internal int Size;

		// Token: 0x04000749 RID: 1865
		[Nullable(1)]
		internal string Name;
	}
}
