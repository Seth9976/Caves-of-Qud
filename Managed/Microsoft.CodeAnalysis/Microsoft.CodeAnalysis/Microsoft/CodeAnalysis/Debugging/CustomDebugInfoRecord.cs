using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.Debugging
{
	// Token: 0x0200035E RID: 862
	internal struct CustomDebugInfoRecord
	{
		// Token: 0x060024F2 RID: 9458 RVA: 0x0007318D File Offset: 0x0007138D
		public CustomDebugInfoRecord(CustomDebugInfoKind kind, byte version, ImmutableArray<byte> data)
		{
			this.Kind = kind;
			this.Version = version;
			this.Data = data;
		}

		// Token: 0x04000FD5 RID: 4053
		public readonly CustomDebugInfoKind Kind;

		// Token: 0x04000FD6 RID: 4054
		public readonly byte Version;

		// Token: 0x04000FD7 RID: 4055
		public readonly ImmutableArray<byte> Data;
	}
}
