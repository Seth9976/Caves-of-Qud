using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000059 RID: 89
	[NullableContext(2)]
	[Nullable(0)]
	internal class ReferenceEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00006D2C File Offset: 0x00004F2C
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006D34 File Offset: 0x00004F34
		bool IEqualityComparer<object>.Equals(object a, object b)
		{
			return a == b;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006D3A File Offset: 0x00004F3A
		int IEqualityComparer<object>.GetHashCode(object a)
		{
			return ReferenceEqualityComparer.GetHashCode(a);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006D42 File Offset: 0x00004F42
		public static int GetHashCode(object a)
		{
			return RuntimeHelpers.GetHashCode(a);
		}

		// Token: 0x04000084 RID: 132
		[Nullable(1)]
		public static readonly ReferenceEqualityComparer Instance = new ReferenceEqualityComparer();
	}
}
