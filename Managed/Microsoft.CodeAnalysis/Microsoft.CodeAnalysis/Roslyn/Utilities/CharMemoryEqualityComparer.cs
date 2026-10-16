using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000031 RID: 49
	internal sealed class CharMemoryEqualityComparer : IEqualityComparer<ReadOnlyMemory<char>>
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00004650 File Offset: 0x00002850
		private CharMemoryEqualityComparer()
		{
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004658 File Offset: 0x00002858
		public bool Equals(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y)
		{
			return MemoryExtensions.SequenceEqual<char>(x.Span, y.Span);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000466D File Offset: 0x0000286D
		public int GetHashCode(ReadOnlyMemory<char> mem)
		{
			return Hash.GetFNVHashCode(mem.Span);
		}

		// Token: 0x04000035 RID: 53
		[Nullable(1)]
		public static readonly CharMemoryEqualityComparer Instance = new CharMemoryEqualityComparer();
	}
}
