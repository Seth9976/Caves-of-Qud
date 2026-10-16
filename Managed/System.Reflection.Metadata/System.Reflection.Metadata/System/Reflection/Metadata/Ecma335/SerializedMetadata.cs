using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E2 RID: 226
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SerializedMetadata
	{
		// Token: 0x06000895 RID: 2197 RVA: 0x00017B21 File Offset: 0x00015D21
		public SerializedMetadata(MetadataSizes sizes, BlobBuilder stringHeap, [Nullable(0)] ImmutableArray<int> stringMap)
		{
			this.Sizes = sizes;
			this.StringHeap = stringHeap;
			this.StringMap = stringMap;
		}

		// Token: 0x0400053B RID: 1339
		[Nullable(0)]
		internal readonly ImmutableArray<int> StringMap;

		// Token: 0x0400053C RID: 1340
		internal readonly BlobBuilder StringHeap;

		// Token: 0x0400053D RID: 1341
		internal readonly MetadataSizes Sizes;
	}
}
