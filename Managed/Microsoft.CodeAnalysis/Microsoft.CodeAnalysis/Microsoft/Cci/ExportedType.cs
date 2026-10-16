using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x02000088 RID: 136
	[NullableContext(1)]
	[Nullable(0)]
	internal struct ExportedType
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000D000 File Offset: 0x0000B200
		public ExportedType(ITypeReference type, int parentIndex, bool isForwarder)
		{
			this.Type = type;
			this.IsForwarder = isForwarder;
			this.ParentIndex = parentIndex;
		}

		// Token: 0x04000178 RID: 376
		public readonly ITypeReference Type;

		// Token: 0x04000179 RID: 377
		public readonly bool IsForwarder;

		// Token: 0x0400017A RID: 378
		public readonly int ParentIndex;
	}
}
