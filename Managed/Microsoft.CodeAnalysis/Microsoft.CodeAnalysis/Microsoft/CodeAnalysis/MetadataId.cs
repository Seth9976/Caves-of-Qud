using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001B7 RID: 439
	public sealed class MetadataId
	{
		// Token: 0x06001277 RID: 4727 RVA: 0x000393C9 File Offset: 0x000375C9
		private MetadataId()
		{
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000393D1 File Offset: 0x000375D1
		[NullableContext(1)]
		internal static MetadataId CreateNewId()
		{
			return new MetadataId();
		}
	}
}
