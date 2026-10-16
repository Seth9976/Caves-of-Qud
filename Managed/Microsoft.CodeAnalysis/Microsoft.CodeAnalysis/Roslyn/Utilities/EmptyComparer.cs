using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class EmptyComparer : IEqualityComparer<object>
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00004CAA File Offset: 0x00002EAA
		private EmptyComparer()
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004CB2 File Offset: 0x00002EB2
		[NullableContext(2)]
		bool IEqualityComparer<object>.Equals(object a, object b)
		{
			return false;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004CB5 File Offset: 0x00002EB5
		int IEqualityComparer<object>.GetHashCode(object s)
		{
			return 0;
		}

		// Token: 0x04000062 RID: 98
		public static readonly EmptyComparer Instance = new EmptyComparer();
	}
}
