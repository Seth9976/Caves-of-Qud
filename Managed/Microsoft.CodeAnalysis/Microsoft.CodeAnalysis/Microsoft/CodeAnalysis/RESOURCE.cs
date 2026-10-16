using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200015E RID: 350
	[NullableContext(2)]
	[Nullable(0)]
	internal class RESOURCE
	{
		// Token: 0x040003EC RID: 1004
		internal RESOURCE_STRING pstringType;

		// Token: 0x040003ED RID: 1005
		internal RESOURCE_STRING pstringName;

		// Token: 0x040003EE RID: 1006
		internal uint DataSize;

		// Token: 0x040003EF RID: 1007
		internal uint HeaderSize;

		// Token: 0x040003F0 RID: 1008
		internal uint DataVersion;

		// Token: 0x040003F1 RID: 1009
		internal ushort MemoryFlags;

		// Token: 0x040003F2 RID: 1010
		internal ushort LanguageId;

		// Token: 0x040003F3 RID: 1011
		internal uint Version;

		// Token: 0x040003F4 RID: 1012
		internal uint Characteristics;

		// Token: 0x040003F5 RID: 1013
		internal byte[] data;
	}
}
