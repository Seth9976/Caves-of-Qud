using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000064 RID: 100
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class StringOrdinalComparer : IEqualityComparer<string>
	{
		// Token: 0x06000236 RID: 566 RVA: 0x0000784C File Offset: 0x00005A4C
		private StringOrdinalComparer()
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007854 File Offset: 0x00005A54
		[NullableContext(2)]
		bool IEqualityComparer<string>.Equals(string a, string b)
		{
			return StringOrdinalComparer.Equals(a, b);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000785D File Offset: 0x00005A5D
		[NullableContext(2)]
		public static bool Equals(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007866 File Offset: 0x00005A66
		int IEqualityComparer<string>.GetHashCode(string s)
		{
			return Hash.GetFNVHashCode(s);
		}

		// Token: 0x0400008F RID: 143
		public static readonly StringOrdinalComparer Instance = new StringOrdinalComparer();
	}
}
