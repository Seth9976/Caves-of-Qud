using System;
using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x0200008E RID: 142
	[NullableContext(2)]
	internal interface IFileReference
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003B2 RID: 946
		bool HasMetadata { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003B3 RID: 947
		string FileName { get; }

		// Token: 0x060003B4 RID: 948
		[NullableContext(0)]
		ImmutableArray<byte> GetHashValue(AssemblyHashAlgorithm algorithmId);
	}
}
